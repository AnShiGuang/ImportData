using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace 批量导入程序重制版
{
    internal sealed class BatchImportKeyBundle
    {
        internal string AppId { get; set; }
        internal string PublicKey { get; set; }
        internal string PrivateKey { get; set; }
    }

    internal sealed class BatchImportUploadResult
    {
        internal bool Success { get; set; }
        internal string Message { get; set; }
    }

    internal static class BatchImportUploadService
    {
        private static readonly JavaScriptSerializer Json = new JavaScriptSerializer { MaxJsonLength = 8 * 1024 * 1024 };
        private static readonly SemaphoreSlim KeyInitializationLock = new SemaphoreSlim(1, 1);
        private static readonly HashSet<string> RequiredItemFields = new HashSet<string>(StringComparer.Ordinal)
        {
            "manage_personal_record_id", "examinationDate", "responsibleDoctor", "symptom", "symptomOther",
            "temperature", "pulse", "breathe", "leftSystolicPressure", "rightSystolicPressure",
            "leftDiastolicPressure", "rightDiastolicPressure", "height", "weight", "waist", "bmi",
            "oldHealthStatusSelfAssess", "cognitiveFunction", "emotionalState", "exerciseFrequency",
            "eatingHabits", "ifSmoking", "drinkingFrequency", "haveOccupational", "lips", "dentition",
            "leftVision", "rightVision", "heartRate", "vulva", "vulvaOther", "vagina", "cervix",
            "corpusUteri", "enclosure", "physicalExaminationOther", "cervicalSmear",
            "supplementaryExaminationOther", "cerebrovascularDisease", "kidneyDisease", "vascularDisease",
            "eyeDisease", "neurologicalDisease", "otherDisease", "hospitalizationHistory", "familyBedHistory",
            "mainDrugUsage", "vaccinationHistory", "healthAssessment"
        };

        internal static async Task<BatchImportKeyBundle> GenerateKeysAsync(Uri serviceRoot)
        {
            using (var client = CreateHttpClient())
            {
                var url = new Uri(serviceRoot, "/release/ExternalInterfaceController/genKey");
                using (var response = await client.GetAsync(url).ConfigureAwait(false))
                {
                    var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    var root = DeserializeObject(body);
                    var data = FindObject(root, "data") ?? root;
                    var bundle = new BatchImportKeyBundle
                    {
                        AppId = FindText(data, "appId"),
                        PublicKey = FindText(data, "publicKeyBase64A"),
                        PrivateKey = FindText(data, "privateKeyBase64B")
                    };
                    if (string.IsNullOrWhiteSpace(bundle.AppId)
                        || string.IsNullOrWhiteSpace(bundle.PublicKey)
                        || string.IsNullOrWhiteSpace(bundle.PrivateKey))
                        throw new InvalidOperationException("接口响应未包含完整的 appId、公钥和本方私钥。");
                    return bundle;
                }
            }
        }

        internal static async Task EnsureKeysAsync()
        {
            if (HasBatchImportKeys()) return;

            await KeyInitializationLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (HasBatchImportKeys()) return;
                Uri serviceRoot;
                if (!Uri.TryCreate(AppConfig.UploadBaseUrl, UriKind.Absolute, out serviceRoot)
                    || serviceRoot.Scheme != Uri.UriSchemeHttps)
                    throw new InvalidOperationException("批量导入服务根地址未配置或不是有效的 HTTPS 地址，无法自动获取接口密钥。");

                var bundle = await GenerateKeysAsync(serviceRoot).ConfigureAwait(false);
                AppConfig.SetBatchImportCredentials(bundle.AppId, bundle.PublicKey, bundle.PrivateKey);
            }
            finally
            {
                KeyInitializationLock.Release();
            }
        }

        private static bool HasBatchImportKeys()
        {
            return !string.IsNullOrWhiteSpace(AppConfig.BatchImportAppId)
                && !string.IsNullOrWhiteSpace(AppConfig.BatchImportPublicKey)
                && !string.IsNullOrWhiteSpace(AppConfig.BatchImportPrivateKey);
        }

        internal static async Task<BatchImportUploadResult> UploadPersonAsync(导入数据.PersonRow person)
        {
            if (person == null) throw new ArgumentNullException("person");
            Uri configuredRoot;
            if (!Uri.TryCreate(AppConfig.UploadBaseUrl, UriKind.Absolute, out configuredRoot))
                throw new InvalidOperationException("批量导入服务根地址未配置或无效。");
            var root = new Uri(configuredRoot.GetLeftPart(UriPartial.Authority).TrimEnd('/') + "/", UriKind.Absolute);
            if (root.Scheme != Uri.UriSchemeHttps)
                throw new InvalidOperationException("批量导入接口必须配置为 HTTPS 地址。");
            await EnsureKeysAsync().ConfigureAwait(false);
            var appId = AppConfig.BatchImportAppId;
            var publicKey = AppConfig.BatchImportPublicKey;
            var privateKey = AppConfig.BatchImportPrivateKey;
            var userCode = AppConfig.BatchImportUserCode;
            if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(publicKey)
                || string.IsNullOrWhiteSpace(privateKey) || string.IsNullOrWhiteSpace(userCode))
                throw new InvalidOperationException("请先在配置参数页面填写并保存批量导入服务地址和账号代码。");
            if (string.IsNullOrWhiteSpace(person.IdCardNumber))
                throw new InvalidOperationException("人员身份证号为空，无法获取批量导入个人档案。");

            var source = await LoadExamSourceAsync(person.ExamNumber).ConfigureAwait(false);
            var item = BuildItem(person, source, string.Empty);
            var missing = ValidateRequiredItem(item, false);
            if (missing.Count > 0)
                throw new InvalidOperationException("以下接口必填字段缺少有效数据，未发送上传请求：" + string.Join("、", missing));

            var profileRequest = new Dictionary<string, object>
            {
                { "appId", appId }, { "appCode", 11002 }, { "IdCard", person.IdCardNumber }
            };
            var profileResponse = await PostEncryptedAsync(root, appId, publicKey, privateKey, profileRequest).ConfigureAwait(false);
            var recordNo = FindText(profileResponse, "recordNo");
            var profileId = FindText(profileResponse, "manage_personal_record_id");
            if (string.IsNullOrWhiteSpace(profileId)) profileId = FindText(profileResponse, "managePersonalRecordId");
            if (string.IsNullOrWhiteSpace(recordNo) || string.IsNullOrWhiteSpace(profileId))
                throw new InvalidOperationException("个人档案接口未返回 recordNo 和个人档案ID，未提交体检数据。");

            item["manage_personal_record_id"] = profileId;
            missing = ValidateRequiredItem(item, true);
            if (missing.Count > 0)
                throw new InvalidOperationException("以下接口必填字段缺少有效数据，未发送上传请求：" + string.Join("、", missing));

            var request = new Dictionary<string, object>
            {
                { "appId", appId }, { "appCode", 11003 }, { "recordNo", recordNo },
                { "IdCard", person.IdCardNumber }, { "userCode", userCode }, { "item", item }
            };
            var result = await PostEncryptedAsync(root, appId, publicKey, privateKey, request).ConfigureAwait(false);
            var state = FindText(result, "state");
            var message = FindText(result, "message");
            var success = state == "1" || string.Equals(state, "success", StringComparison.OrdinalIgnoreCase);
            return new BatchImportUploadResult
            {
                Success = success,
                Message = string.IsNullOrWhiteSpace(message) ? (success ? "上传成功" : "接口未确认上传成功") : message
            };
        }

        internal static async Task SaveUploadStatusAsync(string examNumber, string status)
        {
            using (var connection = new SqlConnection(AppConfig.ConnectionString))
            using (var command = new SqlCommand(
                "UPDATE [dbo].[人员_体检登记表] SET [省平台上传状态] = @status WHERE [体检编号] = @examNumber", connection))
            {
                command.CommandTimeout = 20;
                command.Parameters.Add("@status", SqlDbType.VarChar, 50).Value = status;
                command.Parameters.Add("@examNumber", SqlDbType.NVarChar, 100).Value = examNumber;
                await connection.OpenAsync().ConfigureAwait(false);
                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        private static async Task<object> PostEncryptedAsync(Uri root, string appId, string publicKey, string privateKey, object value)
        {
            var plainText = Json.Serialize(value);
            var cipherText = Sm2Encrypt(Encoding.UTF8.GetBytes(plainText), publicKey);
            using (var client = CreateHttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(root, "/release/ExternalInterfaceController/externalInterface")))
            {
                request.Headers.Add("public-health-foreign-api-appid", appId);
                request.Content = new StringContent(cipherText, Encoding.UTF8, "application/json");
                using (var response = await client.SendAsync(request).ConfigureAwait(false))
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    var encryptedResponse = ExtractCipherText(responseBody);
                    var clearText = Sm2Decrypt(encryptedResponse, privateKey);
                    return DeserializeObject(clearText);
                }
            }
        }

        private static HttpClient CreateHttpClient()
        {
            return new HttpClient { Timeout = TimeSpan.FromSeconds(45) };
        }

        private static string ExtractCipherText(string response)
        {
            var value = (response ?? string.Empty).Trim().Trim('"');
            if (value.Length > 0 && value.All(Uri.IsHexDigit)) return value;
            object parsed;
            try { parsed = Json.DeserializeObject(value); }
            catch { throw new InvalidOperationException("接口返回内容不是有效的 SM2 密文。"); }
            var cipher = FindText(parsed, "data") ?? FindText(parsed, "result") ?? FindText(parsed, "content");
            if (string.IsNullOrWhiteSpace(cipher))
                throw new InvalidOperationException("接口响应中没有找到加密数据。");
            return cipher.Trim().Trim('"');
        }

        private static string Sm2Encrypt(byte[] plain, string base64PublicKey)
        {
            try
            {
                var key = PublicKeyFactory.CreateKey(Convert.FromBase64String(base64PublicKey));
                var engine = new SM2Engine();
                engine.Init(true, new ParametersWithRandom(key, new SecureRandom()));
                return ToHex(engine.ProcessBlock(plain, 0, plain.Length));
            }
            catch (Exception ex) { throw new InvalidOperationException("SM2 公钥无效或加密失败。", ex); }
        }

        private static string Sm2Decrypt(string cipherText, string base64PrivateKey)
        {
            try
            {
                var key = PrivateKeyFactory.CreateKey(Convert.FromBase64String(base64PrivateKey));
                var engine = new SM2Engine();
                engine.Init(false, key);
                var clear = engine.ProcessBlock(FromHex(cipherText), 0, FromHex(cipherText).Length);
                return Encoding.UTF8.GetString(clear);
            }
            catch (Exception ex) { throw new InvalidOperationException("SM2 私钥无效或接口密文解密失败。", ex); }
        }

        private static string ToHex(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        private static byte[] FromHex(string value)
        {
            var text = (value ?? string.Empty).Trim();
            if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) text = text.Substring(2);
            if (text.Length == 0 || (text.Length & 1) != 0 || !text.All(Uri.IsHexDigit))
                throw new FormatException("密文不是有效的十六进制数据。");
            var result = new byte[text.Length / 2];
            for (var i = 0; i < result.Length; i++) result[i] = byte.Parse(text.Substring(i * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            return result;
        }

        private static async Task<ExamSource> LoadExamSourceAsync(string examNumber)
        {
            var source = new ExamSource();
            using (var connection = new SqlConnection(AppConfig.ConnectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                source.General = await ReadRowAsync(connection, "业务_问询_一般状况表", examNumber).ConfigureAwait(false);
                source.Lifestyle = await ReadRowAsync(connection, "业务_问询_生活方式表", examNumber).ConfigureAwait(false);
                source.Organ = await ReadRowAsync(connection, "业务_问询_脏器功能表", examNumber).ConfigureAwait(false);
                source.Physical = await ReadRowAsync(connection, "业务_问询_查体表", examNumber).ConfigureAwait(false);
                source.HealthProblems = await ReadRowAsync(connection, "业务_问询_现存主要健康问题表", examNumber).ConfigureAwait(false);
                source.SelfCare = await ReadRowAsync(connection, "业务_问询_老年人生活能力评估表", examNumber).ConfigureAwait(false);
                source.Hospitalization = await ReadRowAsync(connection, "业务_问询_住院治疗情况表", examNumber).ConfigureAwait(false);
                source.Medication = await ReadRowAsync(connection, "业务_问询_主要用药情况表", examNumber).ConfigureAwait(false);
                source.Vaccination = await ReadRowAsync(connection, "业务_问询_非免疫规划预防接种史表", examNumber).ConfigureAwait(false);
                source.Pressure = await ReadRowAsync(connection, "业务_血压脉率结果表", examNumber).ConfigureAwait(false);
                source.HeightWeight = await ReadRowAsync(connection, "业务_身高体重结果表", examNumber).ConfigureAwait(false);
                source.Final = await ReadRowAsync(connection, "总检_结果表", examNumber).ConfigureAwait(false);
                source.Registration = await ReadRowAsync(connection, "人员_体检登记表", examNumber).ConfigureAwait(false);
                source.Lab = await ReadLabRowsAsync(connection, examNumber).ConfigureAwait(false);
                source.HealthAssessment = await ReadLegacyHealthAssessmentAsync(connection, examNumber).ConfigureAwait(false);
                source.RoomResults = await ReadRowsAsync(connection, "业务_普通诊室结果表", examNumber).ConfigureAwait(false);
                source.Ecg = await ReadImagingResultAsync(connection, "业务_特检诊室结果表", "业务_特检诊室疾病表", "心电图", examNumber).ConfigureAwait(false);
                source.Chest = await ReadImagingResultAsync(connection, "业务_影像诊室结果表", "业务_影像诊室疾病表", "%胸部X%", examNumber).ConfigureAwait(false);
                source.Ultrasound = await ReadAbdominalUltrasoundAsync(connection, examNumber).ConfigureAwait(false);
                source.Traditional = await ReadRowAsync(connection, "业务_中医体质辨识结果表", examNumber).ConfigureAwait(false);
            }
            ApplyLegacyHealthAssessmentDerived(source);
            return source;
        }

        private static async Task<Dictionary<string, object>> ReadRowAsync(SqlConnection connection, string table, string examNumber)
        {
            var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            var sql = "SELECT TOP (1) * FROM [dbo].[" + table + "] WHERE [体检编号] = @examNumber";
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 20;
                command.Parameters.Add("@examNumber", SqlDbType.NVarChar, 100).Value = examNumber;
                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    if (await reader.ReadAsync().ConfigureAwait(false))
                        for (var i = 0; i < reader.FieldCount; i++) row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
            }
            return row;
        }

        private static async Task<List<Dictionary<string, object>>> ReadLabRowsAsync(SqlConnection connection, string examNumber)
        {
            var rows = new List<Dictionary<string, object>>();
            using (var command = new SqlCommand("SELECT [LIS项目编码], [LIS项目名称], [检查结果] FROM [dbo].[业务_检验结果表] WHERE [体检编号] = @examNumber ORDER BY [tid] DESC", connection))
            {
                command.CommandTimeout = 20;
                command.Parameters.Add("@examNumber", SqlDbType.NVarChar, 100).Value = examNumber;
                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        rows.Add(new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                        {
                            { "Code", reader.IsDBNull(0) ? string.Empty : reader.GetValue(0) },
                            { "Name", reader.IsDBNull(1) ? string.Empty : reader.GetValue(1) },
                            { "Value", reader.IsDBNull(2) ? string.Empty : reader.GetValue(2) }
                        });
                    }
                }
            }
            return rows;
        }

        private static async Task<List<Dictionary<string, object>>> ReadRowsAsync(SqlConnection connection, string table, string examNumber)
        {
            var rows = new List<Dictionary<string, object>>();
            var sql = "SELECT * FROM [dbo].[" + table + "] WHERE [体检编号] = @examNumber";
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 20;
                command.Parameters.Add("@examNumber", SqlDbType.NVarChar, 100).Value = examNumber;
                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                        for (var i = 0; i < reader.FieldCount; i++) row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        rows.Add(row);
                    }
            }
            return rows;
        }

        private static async Task<Dictionary<string, object>> ReadImagingResultAsync(SqlConnection connection, string resultTable, string diseaseTable, string projectLike, string examNumber)
        {
            var sql = "SELECT TOP (1) a.*, (SELECT CONVERT(varchar(100),d.[疾病名称])+';' FROM [dbo].[" + diseaseTable + "] d "
                + "WHERE d.[体检编号]=a.[体检编号] FOR XML PATH('')) AS [诊断疾病名称] FROM [dbo].[" + resultTable + "] a "
                + "INNER JOIN [dbo].[业务_申请单明细表] b ON a.[条码号]=b.[条码号] "
                + "WHERE a.[体检编号]=@examNumber AND b.[组合项目名称] LIKE @project ORDER BY a.[检查日期] DESC";
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 20;
                command.Parameters.Add("@examNumber", SqlDbType.NVarChar, 100).Value = examNumber;
                command.Parameters.Add("@project", SqlDbType.NVarChar, 100).Value = projectLike;
                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    if (!await reader.ReadAsync().ConfigureAwait(false)) return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    for (var i = 0; i < reader.FieldCount; i++) row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    return row;
                }
            }
        }

        private static async Task<Dictionary<string, object>> ReadAbdominalUltrasoundAsync(SqlConnection connection, string examNumber)
        {
            const string sql = @"SELECT TOP (1) a.[诊断] AS [检查结果], a.[异常标志], a.[检查医师], a.[审核医师], a.[检查日期],
         (SELECT CONVERT(varchar(100),d.[疾病名称])+';' FROM [dbo].[业务_超声诊室疾病表] d WHERE d.[体检编号]=a.[体检编号] FOR XML PATH('')) AS [诊断疾病名称]
         FROM [dbo].[业务_超声诊室结果表] a INNER JOIN [dbo].[业务_申请单明细表] b ON a.[条码号]=b.[条码号]
         WHERE a.[体检编号]=@examNumber AND (b.[组合项目名称] LIKE '%肝胆胰脾肾%' OR b.[组合项目名称] LIKE '%腹部%' OR b.[组合项目名称] LIKE '%肝胆胰脾双肾%')
         ORDER BY a.[检查日期] DESC";
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 20;
                command.Parameters.Add("@examNumber", SqlDbType.NVarChar, 100).Value = examNumber;
                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    if (!await reader.ReadAsync().ConfigureAwait(false)) return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    for (var i = 0; i < reader.FieldCount; i++) row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    return row;
                }
            }
        }

        private static async Task<Dictionary<string, object>> ReadLegacyHealthAssessmentAsync(SqlConnection connection, string examNumber)
        {
            // Mirrors the legacy HealthAssessment SQL sources; the schema is intentionally taken from the old uploader.
            const string sql = @"
SELECT
 ISNULL('一般检查示：' +
   ISNULL((SELECT CONVERT(varchar(100), [疾病名称]) + ' ' FROM [dbo].[业务_身高体重疾病表] WHERE [体检编号]=@examNumber FOR XML PATH('')), '') +
   ISNULL((SELECT CONVERT(varchar(100), LEFT([疾病名称]+' ;', CHARINDEX(' ',[疾病名称]+' ;'))) + ' ' FROM [dbo].[业务_血压脉率疾病表] WHERE [体检编号]=@examNumber AND [疾病名称] NOT LIKE '%心动过%' FOR XML PATH('')), '') +
   ISNULL((SELECT CONVERT(varchar(100), [疾病名称]) + ' ' FROM [dbo].[业务_普通诊室疾病表] WHERE [体检编号]=@examNumber FOR XML PATH('')), ''), '') AS A1,
 ISNULL('B超示：' + (SELECT CONVERT(varchar(100),[疾病名称])+' ' FROM [dbo].[业务_超声诊室疾病表] WHERE [体检编号]=@examNumber FOR XML PATH('')), '') AS A2,
  ISNULL('心电图示：' + (SELECT CONVERT(varchar(100),b.[疾病名称])+' ' FROM [dbo].[业务_特检诊室疾病表] b INNER JOIN [dbo].[业务_申请单明细表] c ON b.[体检编号]=c.[体检编号] AND b.[条码号]=c.[条码号] WHERE b.[体检编号]=@examNumber AND c.[组合项目名称]='心电图' AND b.[疾病名称] NOT LIKE '%干扰%' AND b.[疾病名称] NOT LIKE '%请结合临床%' AND b.[疾病名称] NOT LIKE '%建议复查%' AND b.[疾病名称] NOT LIKE '%注意复查%' AND b.[疾病名称] NOT LIKE '%肺部疾病的可能%' AND b.[疾病名称] NOT LIKE '%不能平卧%' AND b.[疾病名称] NOT LIKE '%根据固有心律QRS波形分析%' AND b.[疾病名称] NOT LIKE '%结果需医师确认%' AND b.[疾病名称] NOT LIKE '%根据固有心律QRS波形分析;结果需医师确认%' AND b.[疾病名称] NOT LIKE '%需要结合临床症状及病史完善心脏彩超检查%' AND b.[疾病名称] NOT LIKE '%请结合症状完善动态心电图评估%' AND b.[疾病名称] NOT LIKE '%请结合症状完善心脏彩超等检查并干预;需要结合临床并做24小时动态心电图评估干预%' AND b.[疾病名称] NOT LIKE '%请结合临床症状及血压%' AND b.[疾病名称] NOT LIKE '%请结合临床进一步检查%' AND b.[疾病名称] NOT LIKE '%电轴左偏%' AND b.[疾病名称] NOT LIKE '%伪差%' FOR XML PATH('')), '') AS A3,
 ISNULL('检验示：' + (SELECT CONVERT(varchar(100),[疾病名称])+' ' FROM [dbo].[业务_检验疾病表] WHERE [体检编号]=@examNumber AND ([疾病名称] LIKE '血红蛋白%' OR [疾病名称] LIKE '%白细胞%' OR [疾病名称] LIKE '血小板%' OR [疾病名称] LIKE '%酮体%' OR [疾病名称] LIKE '%谷草%' OR [疾病名称] LIKE '%谷丙%' OR [疾病名称] LIKE '%总胆红素%' OR [疾病名称] LIKE '%肌酐%' OR [疾病名称] LIKE '%尿素%' OR [疾病名称] LIKE '%总胆固醇%' OR [疾病名称] LIKE '%甘油三酯%' OR [疾病名称] LIKE '%低密度%' OR [疾病名称] LIKE '%高密度%' OR [疾病名称] LIKE '%甘油三脂%' OR [疾病名称] LIKE '%丙氨%' OR [疾病名称] LIKE '%冬氨%') AND [疾病名称] NOT LIKE '%压积%' FOR XML PATH('')), '') AS A4,
 (SELECT '现存疾病：'+[脑血管疾病]+';'+[肾脏疾病]+';'+[心血管疾病]+';'+[眼部疾病]+';'+[神经系统疾病]+';'+[其他系统疾病]+';'+[其他系统其他]+';' FROM [dbo].[业务_问询_现存主要健康问题表] WHERE [体检编号]=@examNumber) AS A5";
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 30;
                command.Parameters.Add("@examNumber", SqlDbType.NVarChar, 100).Value = examNumber;
                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    if (!await reader.ReadAsync().ConfigureAwait(false)) return new Dictionary<string, object>();
                    var labAbnormal = LegacyAbnormal(Text(reader, 3), "检验示：")
                        .Replace("1", string.Empty).Replace("0", string.Empty).Replace(";;", ";").Replace(";;", ";").Replace(";;", ";");
                    var abnormal = new[] { LegacyAbnormal(Text(reader, 0), "一般检查示："), LegacyAbnormal(Text(reader, 1), "B超示："), LegacyAbnormal(Text(reader, 2), "心电图示："), labAbnormal };
                    var present = LegacyAbnormal(Text(reader, 4), "现存疾病：").Replace("未发现;", string.Empty).Replace("其他;", string.Empty);
                    var hasAbnormal = abnormal.Any(x => x.Length > 0);
                    var compact = abnormal.Where(x => x.Length > 0).ToArray();
                    return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                    {
                        // The legacy and new API dictionaries use 1=no abnormality, 2=abnormality.
                        { "healthAssessment", hasAbnormal ? "2" : "1" },
                        { "healthAssessmentAbnormal1", compact.Length > 0 ? compact[0] : string.Empty },
                        { "healthAssessmentAbnormal2", compact.Length > 1 ? compact[1] : string.Empty },
                        { "healthAssessmentAbnormal3", compact.Length > 2 ? compact[2] : string.Empty },
                        { "healthAssessmentAbnormal4", compact.Length > 3 ? compact[3] : string.Empty },
                        { "abnormalSituation", present == "现存疾病：" || present == "现存疾病：;" ? string.Empty : present }
                    };
                }
            }
        }

        private static string Text(SqlDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? string.Empty : Convert.ToString(reader.GetValue(ordinal), CultureInfo.InvariantCulture);
        }

        private static string LegacyAbnormal(string value, string prefix)
        {
            var text = (value ?? string.Empty).Trim();
            if (text == prefix) return string.Empty;
            return text.Replace(" ", ";").Replace(";;", ";").Trim(';');
        }

        private static void ApplyLegacyHealthAssessmentDerived(ExamSource source)
        {
            var extra = string.Empty;
            var labAbnormal = new List<string>();
            var urineTests = new[]
            {
                new[] { "10027", "尿蛋白" }, new[] { "10021", "尿糖" },
                new[] { "10023", "尿酮体" }, new[] { "10025", "尿潜血" }
            };
            foreach (var test in urineTests)
            {
                var result = FirstLabRaw(source.Lab, test);
                if (result.Contains("+")) labAbnormal.Add(test[1] + ":" + result + ";");
            }
            var fastingGlucose = FirstLabRaw(source.Lab, "30521", "空腹血糖");
            double glucoseValue;
            if (double.TryParse(fastingGlucose, NumberStyles.Any, CultureInfo.InvariantCulture, out glucoseValue))
            {
                if (glucoseValue > 6.1) labAbnormal.Add("血糖偏高;");
                if (glucoseValue < 3.9) labAbnormal.Add("血糖偏低;");
            }
            if (labAbnormal.Count > 0)
            {
                var currentLabAssessment = Get(source.HealthAssessment, "healthAssessmentAbnormal4");
                source.HealthAssessment["healthAssessmentAbnormal4"] =
                    (currentLabAssessment.Length == 0 ? "检验示：" : currentLabAssessment) + string.Join(string.Empty, labAbnormal);
                source.HealthAssessment["healthAssessment"] = "2";
            }
            var selfCareText = Get(source.General, "老年人生活自理能力自我评估");
            if (selfCareText.Contains("依赖")) extra += selfCareText + ";";
            var sex = Get(source.Registration, "性别");
            var waist = First(source.General, "腰围", source.HeightWeight, "腹围");
            double waistValue;
            if (double.TryParse(waist, NumberStyles.Any, CultureInfo.InvariantCulture, out waistValue)
                && ((sex.Contains("男") && waistValue >= 90) || (sex.Contains("女") && waistValue >= 85)))
                extra += "腹型肥胖;";
            if (extra.Length > 0)
            {
                var first = Get(source.HealthAssessment, "healthAssessmentAbnormal1");
                source.HealthAssessment["healthAssessmentAbnormal1"] = (first.Length == 0 ? string.Empty : first + ";") + extra.TrimEnd(';');
                source.HealthAssessment["healthAssessment"] = "2";
            }
        }

        private static Dictionary<string, object> BuildItem(导入数据.PersonRow person, ExamSource source, string profileId)
        {
            var item = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                { "manage_personal_record_id", profileId },
                { "examinationDate", First(source.General, "体检时间", source.Final, "总检日期", source.Registration, "登记时间") },
                { "responsibleDoctor", First(source.General, "体检医生", source.Final, "总检医师") },
                { "symptom", ShiftCodeList(Get(source.General, "症状代码")) }, { "symptomOther", Get(source.General, "症状其他") },
                { "temperature", FormatOneDecimal(First(source.General, "体温", source.HeightWeight, "体温"), "体温") },
                { "pulse", First(source.Pressure, "脉率", RoomResult(source, "明细项目序号", "55"), "检查结果") },
                { "breathe", Get(source.General, "呼吸频率") },
                { "leftSystolicPressure", Get(source.Pressure, "左侧收缩压") },
                { "rightSystolicPressure", PressurePart(source.Pressure, 0) },
                { "leftDiastolicPressure", Get(source.Pressure, "左侧舒张压") },
                { "rightDiastolicPressure", PressurePart(source.Pressure, 1) },
                { "height", FormatOneDecimal(Get(source.HeightWeight, "身高"), "身高") },
                { "weight", FormatOneDecimal(Get(source.HeightWeight, "体重"), "体重") },
                { "waist", FormatOneDecimal(First(source.General, "腰围", source.HeightWeight, "腹围"), "腰围") },
                { "bmi", FormatOneDecimal(First(source.HeightWeight, "体重指数", source.HeightWeight, "BMI"), "体质指数") },
                { "oldHealthStatusSelfAssess", ShiftCode(Get(source.General, "老年人健康状况自我评估代码")) },
                { "cognitiveFunction", Get(source.General, "老年人认知功能代码") },
                { "emotionalState", Get(source.General, "老年人情感状态代码") },
                { "exerciseFrequency", ShiftCode(Get(source.Lifestyle, "锻炼频率代码")) },
                { "eatingHabits", Get(source.Lifestyle, "饮食习惯代码") },
                { "ifSmoking", ShiftCode(Get(source.Lifestyle, "吸烟状况代码")) },
                { "drinkingFrequency", ShiftCode(Get(source.Lifestyle, "饮酒频率代码")) },
                { "haveOccupational", OccupationalCode(Get(source.Lifestyle, "职业病危害因素接触史代码")) },
                { "occupationalWorkType", Get(source.Lifestyle, "职业病危害因素接触史工种") },
                { "dustMatter", string.Empty }, { "haveDustProtection", ProtectionCode(source.Lifestyle, "粉尘防护措施代码") },
                { "dustProtectionInstruction", Get(source.Lifestyle, "粉尘防护措施代码") },
                { "radioactiveSubstance", string.Empty }, { "haveRadioactiveSubstanceProtection", ProtectionCode(source.Lifestyle, "放射物质防护措施代码") },
                { "radioactiveSubstanceInstruction", Get(source.Lifestyle, "放射物质防护措施代码") },
                { "physicalFactor", string.Empty }, { "havePhysicalFactorProtection", ProtectionCode(source.Lifestyle, "物理因素防护措施代码") },
                { "physicalFactorInstruction", Get(source.Lifestyle, "物理因素防护措施代码") },
                { "chemicalSubstance", string.Empty }, { "haveChemicalSubstanceProtection", ProtectionCode(source.Lifestyle, "化学因素防护措施代码") },
                { "chemicalSubstanceInstruction", Get(source.Lifestyle, "化学因素防护措施代码") },
                { "occupationalHazardOther", string.Empty }, { "occupationalHazardOtherProtection", ProtectionCode(source.Lifestyle, "其他防护措施代码") },
                { "occupationalHazardOtherInstruction", Get(source.Lifestyle, "其他防护措施代码") },
                { "lips", Get(source.Organ, "口唇代码") }, { "dentition", Get(source.Organ, "齿列代码") },
                { "missTeeth", TeethPositions(source.Organ, "缺齿") }, { "dentalCaries", TeethPositions(source.Organ, "龋齿") },
                { "falseTooth", TeethPositions(source.Organ, "义齿") }, { "pharynx", ShiftCode(Get(source.Organ, "咽部代码")) },
                { "leftVision", First(source.Organ, "视力左眼", RoomResult(source, "明细项目名称", "裸视力:L"), "检查结果") },
                { "rightVision", First(source.Organ, "视力右眼", RoomResult(source, "明细项目名称", "裸视力:R"), "检查结果") },
                { "correctLeftVision", Get(source.Organ, "矫正视力左眼") }, { "correctRightVision", Get(source.Organ, "矫正视力右眼") },
                { "hear", ShiftCode(Get(source.Organ, "听力代码")) }, { "athleticAbility", ShiftCode(Get(source.Organ, "运动功能代码")) },
                { "pharynxOther", Get(source.Organ, "咽部其他") },
                { "oculiFundus", ShiftCode(Get(source.Physical, "眼底代码")) }, { "oculiFundusOther", Get(source.Physical, "眼底") },
                { "skin", Get(source.Physical, "皮肤代码") }, { "skinOther", Get(source.Physical, "皮肤其他") },
                { "sclera", ShiftCode(Get(source.Physical, "巩膜代码")) }, { "scleraOther", Get(source.Physical, "巩膜") },
                { "lymphGland", ShiftCode(Get(source.Physical, "淋巴结代码")) }, { "lymphGlandOther", Get(source.Physical, "淋巴结") },
                { "barrelChest", BarrelChestCode(Get(source.Physical, "桶状胸代码")) }, { "breathSound", ShiftCode(Get(source.Physical, "呼吸音代码")) },
                { "breathSoundOther", Get(source.Physical, "呼吸音") }, { "rale", ShiftCode(Get(source.Physical, "啰音代码")) },
                { "raleOther", Get(source.Physical, "啰音") }, { "heartRate", First(source.Physical, "心率", source.Pressure, "心率") },
                { "rhythm", ShiftCode(Get(source.Physical, "心律代码")) }, { "noise", ShiftCode(Get(source.Physical, "杂音代码")) },
                { "noiseOther", Get(source.Physical, "杂音") }, { "abdominalTenderness", ShiftCode(Get(source.Physical, "压痛代码")) },
                { "abdominalTendernessOther", Get(source.Physical, "压痛") }, { "abdominalMass", ShiftCode(Get(source.Physical, "包块代码")) },
                { "abdominalMassOther", Get(source.Physical, "包块") }, { "abdominalHepatomegaly", ShiftCode(Get(source.Physical, "肝大代码")) },
                { "abdominalHepatomegalyOther", Get(source.Physical, "肝大") }, { "abdominalSplenomegaly", ShiftCode(Get(source.Physical, "脾大代码")) },
                { "abdominalSplenomegalyOther", Get(source.Physical, "脾大") }, { "abdominalMobileMurmur", ShiftCode(Get(source.Physical, "移动性浊音代码")) },
                { "abdominalMobileMurmurOther", Get(source.Physical, "移动性浊音") }, { "lowerLimbEdema", ShiftCode(Get(source.Physical, "下肢水肿代码")) },
                { "dorsalisPedisArteryPulsation", ShiftCode(Get(source.Physical, "足背动脉搏动代码")) },
                { "digitalAnalExamination", ShiftCode(Get(source.Physical, "肛门指诊代码")) },
                { "digitalAnalExaminationOther", Get(source.Physical, "肛门指诊") },
                { "breast", ShiftCodeList(Get(source.Physical, "乳腺代码")) }, { "breastOther", Get(source.Physical, "乳腺") },
                { "vulva", ShiftCode(Get(source.Physical, "外阴代码")) }, { "vulvaOther", Get(source.Physical, "外阴") },
                { "vagina", ShiftCode(Get(source.Physical, "阴道代码")) }, { "vaginaOther", Get(source.Physical, "阴道") },
                { "cervix", ShiftCode(Get(source.Physical, "宫颈代码")) }, { "cervixOther", Get(source.Physical, "宫颈") },
                { "corpusUteri", ShiftCode(Get(source.Physical, "宫体代码")) }, { "corpusUteriOther", Get(source.Physical, "宫体") },
                { "enclosure", ShiftCode(Get(source.Physical, "附件代码")) }, { "enclosureOther", Get(source.Physical, "附件") },
                { "physicalExaminationOther", Get(source.Physical, "查体其他") },
                { "bloodType", FirstLab(source.Lab, "XN550ABO", "ABO血型") }, { "rh", RhCode(Get(source.Physical, "RH")) },
                { "cervicalSmear", string.Empty }, { "cervicalSmearOther", string.Empty }, { "supplementaryExaminationOther", string.Empty },
                { "cerebrovascularDisease", ShiftCodeList(Get(source.HealthProblems, "脑血管疾病代码")) },
                { "cerebrovascularDiseaseOther", Get(source.HealthProblems, "脑血管疾病其他") },
                { "kidneyDisease", ShiftCodeList(Get(source.HealthProblems, "肾脏疾病代码")) }, { "kidneyDiseaseOther", Get(source.HealthProblems, "肾脏疾病其他") },
                { "vascularDisease", ShiftCodeList(Get(source.HealthProblems, "心血管疾病代码")) }, { "vascularDiseaseOther", Get(source.HealthProblems, "心血管疾病其他") },
                { "eyeDisease", ShiftCodeList(Get(source.HealthProblems, "眼部疾病代码")) }, { "eyeDiseaseOther", Get(source.HealthProblems, "眼部疾病其他") },
                { "neurologicalDisease", ShiftCodeList(Get(source.HealthProblems, "神经系统疾病代码")) }, { "neurologicalDiseaseOther", Get(source.HealthProblems, "神经系统疾病其他") },
                { "otherDisease", ShiftCodeList(Get(source.HealthProblems, "其他系统疾病代码")) }, { "otherDiseaseOther", Get(source.HealthProblems, "其他系统其他") },
                { "hospitalizationHistory", HistoryFlag(source.Hospitalization,
                    "入院日期1", "出院日期1", "住院史原因1", "住院史医疗机构名称1", "住院史病案号1",
                    "入院日期2", "出院日期2", "住院史原因2", "住院史医疗机构名称2", "住院史病案号2") },
                { "familyBedHistory", HistoryFlag(source.Hospitalization,
                    "建床日期1", "撤床日期1", "家庭病床史原因1", "家庭病床史医疗机构名称1", "家庭病床史病案号1",
                    "建床日期2", "撤床日期2", "家庭病床史原因2", "家庭病床史医疗机构名称2", "家庭病床史病案号2") },
                { "mainDrugUsage", HistoryFlag(source.Medication,
                    "药物名称1", "用法1", "用量1", "用药时间1", "服药依从性代码1",
                    "药物名称2", "用法2", "用量2", "用药时间2", "服药依从性代码2",
                    "药物名称3", "用法3", "用量3", "用药时间3", "服药依从性代码3",
                    "药物名称4", "用法4", "用量4", "用药时间4", "服药依从性代码4",
                    "药物名称5", "用法5", "用量5", "用药时间5", "服药依从性代码5",
                    "药物名称6", "用法6", "用量6", "用药时间6", "服药依从性代码6",
                    "药物名称7", "用法7", "用量7", "用药时间7", "服药依从性代码7",
                    "药物名称8", "用法8", "用量8", "用药时间8", "服药依从性代码8",
                    "药物名称9", "用法9", "用量9", "用药时间9", "服药依从性代码9") },
                { "vaccinationHistory", HistoryFlag(source.Vaccination,
                    "名称1", "接种日期1", "接种机构1", "名称2", "接种日期2", "接种机构2",
                    "名称3", "接种日期3", "接种机构3") },
                { "healthAssessment", Get(source.HealthAssessment, "healthAssessment") },
                { "healthAssessmentAbnormal1", Get(source.HealthAssessment, "healthAssessmentAbnormal1") },
                { "healthAssessmentAbnormal2", Get(source.HealthAssessment, "healthAssessmentAbnormal2") },
                { "healthAssessmentAbnormal3", Get(source.HealthAssessment, "healthAssessmentAbnormal3") },
                { "healthAssessmentAbnormal4", Get(source.HealthAssessment, "healthAssessmentAbnormal4") },
                { "abnormalSituation", Get(source.HealthAssessment, "abnormalSituation") }
            };

            AddOptional(item, "exerciseTime", source.Lifestyle, "每次锻炼时间");
            AddOptional(item, "keepExerciseYears", source.Lifestyle, "坚持锻炼时间");
            AddOptional(item, "exerciseMode", source.Lifestyle, "锻炼方式");
            AddOptional(item, "dailySmoking", source.Lifestyle, "日吸烟量");
            AddOptional(item, "startSmokingTime", source.Lifestyle, "开始吸烟年龄");
            AddOptional(item, "quitSmokingTime", source.Lifestyle, "戒烟年龄");
            AddOptional(item, "dailyDrinking", source.Lifestyle, "日饮酒量");
            AddOptional(item, "ifQuitDrinking", source.Lifestyle == null ? string.Empty : QuitDrinkingCode(Get(source.Lifestyle, "是否戒酒代码")));
            AddOptional(item, "quitDrinkingTime", source.Lifestyle, "戒酒年龄");
            AddOptional(item, "startDrinkingTime", source.Lifestyle, "开始饮酒年龄");
            AddOptional(item, "drinkingWithinOneYear", source.Lifestyle == null ? string.Empty : DrunkWithinYearCode(Get(source.Lifestyle, "近一年是否曾醉酒代码")));
            AddOptional(item, "mainDrinkingVariety", CommaCodes(Get(source.Lifestyle, "饮酒种类代码")));
            AddOptional(item, "drinkingVarietyOhter", source.Lifestyle, "饮酒种类其他");
            AddOptionalLong(item, "workTime", Get(source.Lifestyle, "职业病危害因素接触史从业时间"));
            AddLab(item, source.Lab, "bloodRoutineHb", "20025", "血红蛋白", "血红蛋白浓度");
            AddLab(item, source.Lab, "bloodRoutineWbc", "20023", "白细胞");
            AddLab(item, source.Lab, "bloodRoutinePlt", "20031", "血小板");
            AddLab(item, source.Lab, "urineRoutineProtein", "10027", "尿蛋白");
            AddLab(item, source.Lab, "urineRoutineSugar", "10021", "尿糖");
            AddLab(item, source.Lab, "urineRoutineKetoneBody", "10023", "尿酮体");
            AddLab(item, source.Lab, "urineRoutineOccultBlood", "10025", "尿潜血");
            foreach (var urineKey in new[] { "urineRoutineProtein", "urineRoutineSugar", "urineRoutineKetoneBody", "urineRoutineOccultBlood" })
                item[urineKey] = NormalizeLegacyUrineResult(Get(item, urineKey));
            AddOptional(item, "bloodRoutineQt", string.Empty);
            AddOptional(item, "urineRoutineOther", string.Empty);
            AddLab(item, source.Lab, "fastingBloodGlucose", "30521", "空腹血糖");
            AddLab(item, source.Lab, "postprandialBloodGlucose", "餐后2h血糖");
            AddLab(item, source.Lab, "microalbuminuria", "尿微量白蛋白");
            AddLab(item, source.Lab, "fecalOccultBlood", "大便潜血");
            AddLab(item, source.Lab, "glycosylatedHemoglobin", "BS-600HbAlc", "糖化血红蛋白");
            AddLab(item, source.Lab, "surfaceAntibodyB", "乙型肝炎表面抗原");
            AddOptional(item, "spo2", string.Empty);
            AddLab(item, source.Lab, "liverFunctionAlt", "30511", "谷丙转氨酶", "ALT");
            AddLab(item, source.Lab, "liverFunctionAst", "30512", "谷草转氨酶", "AST");
            AddLab(item, source.Lab, "liverFunctionAlb", "ALB", "白蛋白");
            AddLab(item, source.Lab, "liverFunctionTbil", "30514", "总胆红素");
            AddLab(item, source.Lab, "liverFunctionDbil", "LX2000BIL11", "直接胆红素", "结合胆红素");
            AddLab(item, source.Lab, "renalFunctionScr", "30520", "肌酐");
            AddLab(item, source.Lab, "renalFunctionBun", "30519", "尿素氮", "尿素");
            AddLab(item, source.Lab, "bloodPotassiumConcentration", "血钾浓度");
            AddLab(item, source.Lab, "bloodSodiumConcentration", "血钠浓度");
            AddLab(item, source.Lab, "bloodFatCho", "30515", "总胆固醇");
            AddLab(item, source.Lab, "bloodFatTg", "30516", "甘油三酯", "甘油三脂");
            AddLab(item, source.Lab, "bloodFatLdlc", "30518", "低密度脂蛋白");
            AddLab(item, source.Lab, "bloodFatHdlc", "30517", "高密度脂蛋白");
            AddLab(item, source.Lab, "hcy", "同型半胱氨酸");
            AddCheckResultFields(item, source);
            AddOptional(item, "manageOldSelfcareAssess", BuildSelfCare(source.SelfCare));
            AddOptional(item, "oldSelfcareAssess", ShiftCode(Get(source.General, "老年人生活自理能力自我评估代码")));
            AddOptional(item, "manageHealthHospitalizationHistoryList", BuildHistoryList(source.Hospitalization, false));
            AddOptional(item, "manageHealthFamilyBedHistoryList", BuildHistoryList(source.Hospitalization, true));
            AddOptional(item, "manageHealthMainDrugUsageList", BuildDrugList(source.Medication));
            AddOptional(item, "manageHealthVaccinationHistoryList", BuildVaccinationList(source.Vaccination));
            AddOptional(item, "manageOldTraditional", BuildTraditionalAssessment(source.Traditional));
            AddDerivedGuidanceFields(item, source);
            item["heartRate"] = item["pulse"];
            RemoveEmptyOptionalFields(item, RequiredItemFields);
            return item;
        }

        private static void AddCheckResultFields(Dictionary<string, object> item, ExamSource source)
        {
            var ecgDiagnosis = Get(source.Ecg, "诊断疾病名称");
            var ecgResult = Get(source.Ecg, "检查结果");
            var ecgText = ecgDiagnosis.Length == 0 ? ecgResult : ecgDiagnosis;
            var ecgFlag = Get(source.Ecg, "异常标志");
            var ecgAbnormal = ecgFlag.Contains("是") || ecgFlag == "1" || ecgFlag == "异常";
            item["electrocardiogram"] = source.Ecg.Count == 0 ? string.Empty : (ecgAbnormal ? "1" : "0");
            item["electrocardiogramAbnormal"] = ecgAbnormal ? EcgAbnormalCodes(ecgText) : string.Empty;
            item["electrocardiogramOther"] = ecgAbnormal ? UnclassifiedEcgText(ecgText) : string.Empty;
            item["electrocardiogramInspectResult"] = ecgResult;
            item["electrocardiogramInspectConclusion"] = ecgDiagnosis;

            var chestResult = Get(source.Chest, "诊断");
            var chestDiagnosis = Get(source.Chest, "诊断疾病名称");
            var chestAbnormal = IsAbnormalFlag(Get(source.Chest, "异常标志"));
            item["chestRadiograph"] = chestAbnormal ? "1" : (source.Chest.Count == 0 ? string.Empty : "0");
            item["chestRadiographOther"] = chestAbnormal ? chestDiagnosis : string.Empty;
            item["chestRadiographInspectResult"] = chestResult;
            item["chestRadiographInspectConclusion"] = chestDiagnosis;

            var ultrasoundResult = Get(source.Ultrasound, "检查结果");
            var ultrasoundDiagnosis = Get(source.Ultrasound, "诊断疾病名称");
            var ultrasoundAbnormal = IsAbnormalFlag(Get(source.Ultrasound, "异常标志"));
            item["bMode"] = source.Ultrasound.Count == 0 ? string.Empty : (ultrasoundAbnormal ? "1" : "0");
            item["bModeOther"] = ultrasoundAbnormal ? ultrasoundDiagnosis : string.Empty;
            item["bModeAbnormal"] = string.Empty;
            item["bModeOtherAbnormal"] = string.Empty;
            item["bModeInspectResult"] = ultrasoundResult;
            item["bModeInspectConclusion"] = ultrasoundDiagnosis;
            item["electrocardiogramUrl"] = string.Empty;
            item["sternumUrl"] = string.Empty;
            item["bModeUrl"] = string.Empty;
            item["assayUrl"] = string.Empty;
        }

        private static bool IsAbnormalFlag(string value)
        {
            return value.Contains("是") || value == "1" || value == "异常";
        }

        private static string EcgAbnormalCodes(string text)
        {
            var codes = new List<string>();
            if ((text.Contains("ST") || text.Contains("T")) && text.Contains("改变")) codes.Add("1");
            if (text.Contains("陈旧") && (text.Contains("心肌梗") || text.Contains("心机梗"))) codes.Add("2");
            if (text.Contains("心动过速")) codes.Add("3");
            if (text.Contains("心动过缓")) codes.Add("4");
            if (text.Contains("早搏")) codes.Add("5");
            if (text.Contains("房颤")) codes.Add("6");
            if (text.Contains("房") && text.Contains("导阻")) codes.Add("7");
            if (codes.Count == 0 && text.Length > 0) codes.Add("-1");
            return string.Join(",", codes.Distinct());
        }

        private static string UnclassifiedEcgText(string text)
        {
            var known = new[] { "ST-T改变", "ST改变", "T波改变", "陈旧性心肌梗塞", "心动过速", "心动过缓", "早搏", "房颤", "房室传导阻滞" };
            return known.Any(text.Contains) ? string.Empty : text;
        }

        private static Dictionary<string, object> RoomResult(ExamSource source, string matchColumn, string matchValue)
        {
            return source.RoomResults.FirstOrDefault(row => string.Equals(Get(row, matchColumn), matchValue, StringComparison.OrdinalIgnoreCase))
                ?? new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        private static string PressurePart(Dictionary<string, object> row, int part)
        {
            var value = Get(row, "左侧血压");
            if (value.Length == 0) return string.Empty;
            var pieces = value.Split('/');
            return pieces.Length > part ? pieces[part].Trim() : string.Empty;
        }

        private static string OccupationalCode(string value)
        {
            if (value == "1") return "0";
            if (value == "2") return "1";
            return value;
        }

        private static string ProtectionCode(Dictionary<string, object> row, string column)
        {
            return ShiftCode(Get(row, column));
        }

        private static string QuitDrinkingCode(string value)
        {
            return ShiftCode(value);
        }

        private static string DrunkWithinYearCode(string value)
        {
            if (value == "1" || value.Contains("是")) return "Y";
            if (value == "2" || value.Contains("否")) return "N";
            return value;
        }

        private static string BarrelChestCode(string value)
        {
            if (value == "1" || value.Contains("否")) return "N";
            if (value == "2" || value.Contains("是")) return "Y";
            return value;
        }

        private static string RhCode(string value)
        {
            if (value.Contains("阳性")) return "1";
            if (value.Contains("阴性")) return "2";
            if (value.Contains("不详")) return "3";
            if (value.Contains("未检查") || value.Contains("未查")) return "4";
            return value;
        }

        private static string CommaCodes(string value)
        {
            return string.Join(",", (value ?? string.Empty).Replace(' ', ',').Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(code => code.Trim()).Distinct());
        }

        private static string FormatOneDecimal(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            double number;
            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out number))
                throw new InvalidOperationException(fieldName + "不是有效数值：" + value);
            return number.ToString("F1", CultureInfo.InvariantCulture);
        }

        private static string ShiftCode(string value)
        {
            int code;
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out code) && code > 0
                ? (code - 1).ToString(CultureInfo.InvariantCulture) : value;
        }

        private static string ShiftCodeList(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            return string.Join(",", value.Replace(' ', ',').Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(code => code.Trim() == "-1" ? "-1" : ShiftCode(code.Trim())).Distinct());
        }

        private static string FirstLab(List<Dictionary<string, object>> labs, params string[] aliases)
        {
            var value = FirstLabRaw(labs, aliases);
            if (value.Equals("A", StringComparison.OrdinalIgnoreCase)) return "1";
            if (value.Equals("B", StringComparison.OrdinalIgnoreCase)) return "2";
            if (value.Equals("O", StringComparison.OrdinalIgnoreCase)) return "3";
            if (value.Equals("AB", StringComparison.OrdinalIgnoreCase)) return "4";
            if (value.Contains("不详")) return "5";
            if (value.Contains("拒查")) return "6";
            return value;
        }

        private static string FirstLabRaw(List<Dictionary<string, object>> labs, params string[] aliases)
        {
            if (labs == null) return string.Empty;
            var row = labs.FirstOrDefault(x => aliases.Any(alias => string.Equals(Get(x, "Code"), alias, StringComparison.OrdinalIgnoreCase)
                || Get(x, "Name").IndexOf(alias, StringComparison.OrdinalIgnoreCase) >= 0));
            return row == null ? string.Empty : Get(row, "Value").Trim();
        }

        private static string NormalizeLegacyUrineResult(string value)
        {
            return (value ?? string.Empty).Trim().Replace("0", string.Empty).Replace("1", string.Empty)
                .Replace("阴性", "-").Replace("弱阳性", "+-").Replace("阳性", "+")
                .Replace(" ", string.Empty).Replace("　", string.Empty);
        }

        private static string TeethPositions(Dictionary<string, object> row, string group)
        {
            var names = new[] { group + "左上", group + "右上", group + "左下", group + "右下" };
            return string.Join(",", names.Select((name, index) => new { name, index })
                .Where(item => Get(row, item.name).Length > 0)
                .Select(item => (item.index + 1).ToString(CultureInfo.InvariantCulture)));
        }

        private static void AddDerivedGuidanceFields(Dictionary<string, object> item, ExamSource source)
        {
            var guidance = new List<string>();
            var conditions = string.Join("", new[] { "脑血管疾病", "肾脏疾病", "心血管疾病", "眼部疾病", "神经系统疾病", "其他系统疾病", "其他系统其他" }
                .Select(name => Get(source.HealthProblems, name)));
            if (new[] { "高血压", "糖尿病", "冠心病", "脑卒中", "残疾人", "精神病" }.Any(conditions.Contains)) guidance.Add("1");
            guidance.Add("2");
            var glucose = Get(item, "fastingBloodGlucose");
            double glucoseValue;
            if (double.TryParse(glucose, NumberStyles.Any, CultureInfo.InvariantCulture, out glucoseValue)
                && (glucoseValue >= 16.7 || glucoseValue <= 3.9)) guidance.Add("3");
            if (!guidance.Contains("3") && IsSeverePressure(Get(item, "leftSystolicPressure"), Get(item, "leftDiastolicPressure"))) guidance.Add("3");
            if (!guidance.Contains("3") && IsSeverePressure(Get(item, "rightSystolicPressure"), Get(item, "rightDiastolicPressure"))) guidance.Add("3");
            var risk = new List<string>();
            var abnormalText = string.Join("", Enumerable.Range(1, 4).Select(i => Get(item, "healthAssessmentAbnormal" + i)));
            if (Get(source.Lifestyle, "吸烟状况").Contains("吸烟") && !Get(source.Lifestyle, "吸烟状况").Contains("不吸烟")) risk.Add("1");
            if (Get(source.Lifestyle, "饮酒频率").Length > 0 && !Get(source.Lifestyle, "饮酒频率").Contains("从不")
                && !Get(source.Lifestyle, "是否戒酒").Contains("已戒酒")) risk.Add("2");
            var diet = Get(source.Lifestyle, "饮食习惯代码");
            if (diet.Length > 0 && diet != "1" && diet != "3") risk.Add("3");
            var exercise = Get(source.Lifestyle, "锻炼频率");
            if (exercise.Length > 0 && exercise != "每天") risk.Add("4");
            if (abnormalText.Contains("超重") || abnormalText.Contains("肥胖")) risk.Add("5");
            risk.Add("6");
            risk.Add("-1");
            var other = abnormalText.Contains("腹型肥胖") || abnormalText.Contains("中心型肥胖")
                ? "减腰围,低盐低脂低糖饮食，防跌倒防骨质疏松"
                : "低盐低脂低糖饮食，防跌倒防骨质疏松";
            var weight = Get(source.HeightWeight, "体重");
            double weightValue;
            item["healthGuidance"] = string.Join(",", guidance.Distinct());
            item["healthGuidanceOther"] = string.Empty;
            item["riskFactor"] = string.Join(",", risk.Distinct());
            item["weightLossTarget"] = double.TryParse(weight, NumberStyles.Any, CultureInfo.InvariantCulture, out weightValue)
                ? (weightValue - 2).ToString(CultureInfo.InvariantCulture) + "kg" : string.Empty;
            item["recommendVaccination"] = "23价肺炎疫苗和流感疫苗";
            item["riskFactorsControlOthers"] = other;
        }

        private static bool IsSeverePressure(string systolic, string diastolic)
        {
            int sys, dia;
            return int.TryParse(systolic, out sys) && int.TryParse(diastolic, out dia) && (sys >= 180 || dia >= 110);
        }

        private static string Get(Dictionary<string, object> row, string name)
        {
            if (row == null || !row.ContainsKey(name) || row[name] == null || row[name] == DBNull.Value) return string.Empty;
            var value = Convert.ToString(row[name], CultureInfo.InvariantCulture).Trim();
            if (row[name] is DateTime) value = ((DateTime)row[name]).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            return value;
        }

        private static string First(Dictionary<string, object> first, string firstName, Dictionary<string, object> second, string secondName)
        {
            var value = Get(first, firstName);
            return value.Length > 0 ? value : Get(second, secondName);
        }

        private static string First(Dictionary<string, object> first, string firstName, Dictionary<string, object> second, string secondName, Dictionary<string, object> third, string thirdName)
        {
            var value = First(first, firstName, second, secondName);
            return value.Length > 0 ? value : Get(third, thirdName);
        }

        private static string Teeth(Dictionary<string, object> row, string group)
        {
            var names = new[] { group + "左上", group + "右上", group + "左下", group + "右下" };
            return string.Join(",", names.Select((name, index) => Get(row, name).Length == 0 ? string.Empty : (index + 1).ToString(CultureInfo.InvariantCulture) + ":" + Get(row, name)).Where(x => x.Length > 0));
        }

        private static string HistoryFlag(Dictionary<string, object> row, params string[] names)
        {
            // Legacy used 1=no and 2=yes; the new interface uses 0=no and 1=yes.
            return names.Any(name => Get(row, name).Length > 0) ? "1" : "0";
        }

        private static string NormalizeHealthAssessment(string value)
        {
            return value == "1" || value == "2" ? value : string.Empty;
        }

        private static List<string> ValidateRequiredItem(Dictionary<string, object> item, bool includeProfileId)
        {
            return RequiredItemFields.Where(key => (includeProfileId || key != "manage_personal_record_id")
                && (!item.ContainsKey(key)
                || string.IsNullOrWhiteSpace(Convert.ToString(item[key], CultureInfo.InvariantCulture)))).ToList();
        }

        private static void AddOptional(Dictionary<string, object> target, string key, Dictionary<string, object> row, string sourceName)
        {
            var value = Get(row, sourceName);
            if (value.Length > 0) target[key] = value;
        }

        private static void AddOptional(Dictionary<string, object> target, string key, object value)
        {
            if (value != null) target[key] = value;
        }

        private static void AddOptionalLong(Dictionary<string, object> target, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            long parsed;
            if (!long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed))
                throw new InvalidOperationException("接口字段 " + key + " 要求 Long 整数，源数据值无效：" + value);
            target[key] = parsed;
        }

        private static void RemoveEmptyOptionalFields(Dictionary<string, object> values, ISet<string> requiredFields)
        {
            foreach (var pair in values.ToList())
            {
                var nested = pair.Value as Dictionary<string, object>;
                if (nested != null)
                    RemoveEmptyOptionalFields(nested, new HashSet<string>(StringComparer.Ordinal));

                var nestedRows = pair.Value as IEnumerable<Dictionary<string, object>>;
                if (nestedRows != null)
                {
                    foreach (var row in nestedRows)
                        RemoveEmptyOptionalFields(row, new HashSet<string>(StringComparer.Ordinal));
                }

                if (!requiredFields.Contains(pair.Key) && IsEmptyOptionalValue(pair.Value))
                    values.Remove(pair.Key);
            }
        }

        private static bool IsEmptyOptionalValue(object value)
        {
            if (value == null || value == DBNull.Value) return true;
            var text = value as string;
            if (text != null) return string.IsNullOrWhiteSpace(text);
            var dictionary = value as IDictionary<string, object>;
            if (dictionary != null) return dictionary.Count == 0;
            var collection = value as System.Collections.ICollection;
            return collection != null && collection.Count == 0;
        }

        private static object BuildSelfCare(Dictionary<string, object> row)
        {
            if (row == null || !Enumerable.Range(1, 5).Any(i => Get(row, "评分" + i).Length > 0)) return null;
            var result = new Dictionary<string, object>();
            for (var i = 1; i <= 5; i++)
            {
                var value = Get(row, "评分" + i);
                if (value.Length == 0)
                    throw new InvalidOperationException("生活自理能力评估已存在部分评分，但缺少评分" + i + "；该对象有任一评分时必须提供 assess1 至 assess5。");
                result["assess" + i] = value;
            }
            return result;
        }

        private static object BuildTraditionalAssessment(Dictionary<string, object> row)
        {
            var gradeColumns = new[]
            {
                "您精力充沛吗", "您容易疲乏吗", "您容易气短呼吸短促接不上气吗", "您说话声音低弱无力吗",
                "您感到闷闷不乐情绪低沉吗", "您容易精神紧张焦炉不安吗", "您因为生活状态改变而感到孤独失落吗",
                "您容易感到害怕或受到惊吓吗", "您感到身体超重不轻松吗", "您眼睛干涩吗", "您手脚发凉吗",
                "您胃脘部背部或腰膝部怕冷吗", "您比一般人耐受不了寒冷吗", "您容易患感冒吗",
                "您没有感冒时也会闭塞流鼻涕吗", "您有口粘口腻或睡眠打鼾吗", "您容易过敏吗", "您的皮肤容易起荨麻疹吗",
                "您的皮肤在不知不觉中会出现青紫瘀斑皮下出血吗", "您的皮肤一抓就红并出现抓痕吗", "您的皮肤或口唇干吗",
                "您有肢体麻木或固定部位疼痛的感觉吗", "您面部或鼻部有油腻感或者油亮发光吗", "您面色或目眶晦黯或者出现褐色斑块斑点吗",
                "您的皮肤湿疹疮痂吗", "您感到口干咽燥总想喝水吗", "您感到口苦或者嘴里有异味吗", "您腹部肥大吗",
                "您吃喝凉的东西会感到不舒服或者怕吃喝凉的东西吗", "您有大便粘滞不爽解不尽的感觉吗", "您容易大便干燥吗",
                "您舌苔厚腻或有舌苔厚厚的感觉吗", "您舌下静脉瘀紫或增粗吗"
            };
            if (row == null || !gradeColumns.Any(column => Get(row, column).Length > 0)) return null;
            var grades = gradeColumns.Select(column =>
            {
                int value;
                if (!int.TryParse(Get(row, column), NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                    throw new InvalidOperationException("中医体质辨識记录缺少有效答题值：" + column);
                return value;
            }).ToArray();

            var scores = new[]
            {
                grades[1] + grades[2] + grades[3] + grades[13],
                grades[10] + grades[11] + grades[12] + grades[28],
                grades[9] + grades[20] + grades[25] + grades[30],
                grades[8] + grades[15] + grades[27] + grades[31],
                grades[22] + grades[24] + grades[26] + grades[29],
                grades[18] + grades[21] + grades[23] + grades[32],
                grades[4] + grades[5] + grades[6] + grades[7],
                grades[14] + grades[16] + grades[17] + grades[19],
                24 + grades[0] - grades[1] - grades[4] - grades[12]
            };
            var codes = new string[9];
            for (var i = 0; i < 8; i++) codes[i] = scores[i] >= 11 ? "1" : scores[i] >= 9 ? "2" : string.Empty;
            codes[8] = scores[8] >= 17 && scores.Take(8).All(score => score < 8) ? "1"
                : scores[8] >= 17 && scores.Take(8).All(score => score < 10) ? "2" : string.Empty;

            var names = new[] { "qiDeficiency", "yangDeficiency", "yinDeficiency", "phlegmDamp", "dampHeat", "bloodStasis", "qiDepression", "special", "gentleness" };
            var result = new Dictionary<string, object>();
            for (var i = 0; i < grades.Length; i++) result["grade" + (i + 1).ToString(CultureInfo.InvariantCulture)] = grades[i].ToString(CultureInfo.InvariantCulture);
            var guideProperties = new[] { "qiDeficiencyTypeGuide", "yangDeficiencyTypeGuide", "yinDeficiencyTypeGuide", "phlegmDampTypeGuide", "dampHeatTypeGuide", "bloodStasisTypeGuide", "qiDepressionTypeGuide", "specialTypeGuide", "gentlenessTypeGuide" };
            var otherProperties = new[] { "qiDeficiencyTypeGuideOther", "yangDeficiencyTypeGuideOther", "yinDeficiencyTypeGuideOther", "phlegmDampTypeGuideOther", "dampHeatTypeGuideOther", "bloodStasisTypeGuideOther", "qiDepressionTypeGuideOther", "specialTypeGuideOther", "gentlenessTypeGuideOther" };
            for (var i = 0; i < names.Length; i++)
            {
                result[guideProperties[i]] = codes[i].Length == 0 ? string.Empty : "1,2,3,4,5";
                result[otherProperties[i]] = string.Empty;
            }
            return result;
        }

        private static object BuildHistoryList(Dictionary<string, object> row, bool familyBed)
        {
            if (row == null) return null;
            var list = new List<Dictionary<string, object>>();
            var datePrefix = familyBed ? "建床日期" : "入院日期";
            var leavePrefix = familyBed ? "撤床日期" : "出院日期";
            var reasonPrefix = familyBed ? "家庭病床史原因" : "住院史原因";
            var institutionPrefix = familyBed ? "家庭病床史医疗机构名称" : "住院史医疗机构名称";
            var recordPrefix = familyBed ? "家庭病床史病案号" : "住院史病案号";
            for (var i = 1; i <= 2; i++)
            {
                var fields = new Dictionary<string, object>();
                AddNonEmpty(fields, "hospitalizationTime", Get(row, datePrefix + i));
                AddNonEmpty(fields, "leaveHospitalTime", Get(row, leavePrefix + i));
                AddNonEmpty(fields, "reason", Get(row, reasonPrefix + i));
                AddNonEmpty(fields, "medicalInstitutionName", Get(row, institutionPrefix + i));
                AddNonEmpty(fields, "medicalRecordNumber", Get(row, recordPrefix + i));
                if (fields.Count > 0) list.Add(fields);
            }
            return list.Count == 0 ? null : (object)list;
        }

        private static object BuildDrugList(Dictionary<string, object> row)
        {
            if (row == null) return null;
            var list = new List<Dictionary<string, object>>();
            for (var i = 1; i <= 9; i++)
            {
                var fields = new Dictionary<string, object>();
                AddNonEmpty(fields, "drugName", Get(row, "药物名称" + i));
                AddNonEmpty(fields, "drugUsage", Get(row, "用法" + i));
                AddNonEmpty(fields, "drugDosage", Get(row, "用量" + i));
                AddNonEmpty(fields, "drugTime", Get(row, "用药时间" + i));
                AddNonEmpty(fields, "drugCompliance", Get(row, "服药依从性代码" + i));
                if (fields.Count > 0) list.Add(fields);
            }
            return list.Count == 0 ? null : (object)list;
        }

        private static object BuildVaccinationList(Dictionary<string, object> row)
        {
            if (row == null) return null;
            var list = new List<Dictionary<string, object>>();
            for (var i = 1; i <= 3; i++)
            {
                var fields = new Dictionary<string, object>();
                AddNonEmpty(fields, "inoculationName", Get(row, "名称" + i));
                AddNonEmpty(fields, "inoculationTime", Get(row, "接种日期" + i));
                AddNonEmpty(fields, "inoculationBrc", Get(row, "接种机构" + i));
                if (fields.Count > 0) list.Add(fields);
            }
            return list.Count == 0 ? null : (object)list;
        }

        private static void AddNonEmpty(Dictionary<string, object> target, string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(value)) target[key] = value;
        }

        private static void AddLab(Dictionary<string, object> target, List<Dictionary<string, object>> labs, string key, params string[] aliases)
        {
            var row = labs.FirstOrDefault(x => aliases.Any(alias =>
                string.Equals(Get(x, "Code"), alias, StringComparison.OrdinalIgnoreCase)
                || Get(x, "Name").IndexOf(alias, StringComparison.OrdinalIgnoreCase) >= 0));
            if (row != null) target[key] = Get(row, "Value");
        }

        private static object DeserializeObject(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) throw new InvalidOperationException("接口返回空内容。");
            return Json.DeserializeObject(json);
        }

        private static string FindText(object value, string name)
        {
            var dictionary = value as IDictionary<string, object>;
            if (dictionary != null)
            {
                foreach (var pair in dictionary)
                    if (string.Equals(pair.Key, name, StringComparison.OrdinalIgnoreCase) && pair.Value != null)
                        return Convert.ToString(pair.Value, CultureInfo.InvariantCulture);
                foreach (var pair in dictionary)
                {
                    var found = FindText(pair.Value, name);
                    if (!string.IsNullOrWhiteSpace(found)) return found;
                }
            }
            var array = value as object[];
            if (array != null)
                foreach (var item in array)
                {
                    var found = FindText(item, name);
                    if (!string.IsNullOrWhiteSpace(found)) return found;
                }
            return null;
        }

        private static object FindObject(object value, string name)
        {
            var dictionary = value as IDictionary<string, object>;
            if (dictionary == null) return null;
            foreach (var pair in dictionary)
                if (string.Equals(pair.Key, name, StringComparison.OrdinalIgnoreCase)) return pair.Value;
            foreach (var pair in dictionary)
            {
                var found = FindObject(pair.Value, name);
                if (found != null) return found;
            }
            return null;
        }

        private sealed class ExamSource
        {
            internal Dictionary<string, object> Registration, Final, General, Lifestyle, Organ, Physical, HealthProblems;
            internal Dictionary<string, object> SelfCare, Hospitalization, Medication, Vaccination, Pressure, HeightWeight, HealthAssessment;
            internal Dictionary<string, object> Ecg, Chest, Ultrasound, Traditional;
            internal List<Dictionary<string, object>> RoomResults;
            internal List<Dictionary<string, object>> Lab;
        }
    }
}
