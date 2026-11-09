using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    public sealed partial class ImportDataForm : PageFormBase
    {
        public event EventHandler ViewProgramLogRequested;
        private readonly string userName;
        private List<PersonRow> currentPeople = new List<PersonRow>();
        private bool synchronizingSelectAll;

        public ImportDataForm() : this(string.Empty) { }

        public ImportDataForm(string userName)
        {
            this.userName = userName;
            AntdUI.Style.SetPrimary(Color.FromArgb(35, 145, 112));
            InitializeComponent();
            startDatePicker.Value = DateTime.Today;
            endDatePicker.Value = DateTime.Today;
            Resize += ImportDataForm_Resize;
            ConfigurePeopleGrid();
            LayoutSearchControls();
            selectAllCheckBox.CheckedChanged += selectAllCheckBox_CheckedChanged;
            peopleGrid.CheckedChanged += peopleGrid_CheckedChanged;
            peopleGrid.CellButtonClick += peopleGrid_CellButtonClick;
            peopleGrid.Resize += peopleGrid_Resize;
            peopleGrid.Empty = true;
            resultCountLabel.Text = "查询到 0 人";
            LayoutSearchControls();
            statusLabel.Text = "请选择日期范围并查询人员。";
        }

        private void ImportDataForm_Resize(object sender, EventArgs e)
        {
            LayoutSearchControls();
        }

        private void peopleGrid_Resize(object sender, EventArgs e)
        {
            ConfigurePeopleGrid();
        }

        private void LayoutSearchControls()
        {
            peopleGrid.Width = Math.Max(1, ClientSize.Width - 24);

            // 顶部查询区按设计器比例排布：六个字段一行，两个按钮位于日期字段正下方。
            const int left = 12;
            const int rightMargin = 12;
            const int gap = 6;
            const int labelY = 11;
            const int inputY = 31;
            const int inputHeight = 34;
            const int buttonY = 71;
            const int buttonHeight = 43;

            var available = Math.Max(120, ClientSize.Width - left - rightMargin - gap * 5);
            // 设计器中的字段宽度：当前院区 143、系统标识 139、申请单号 153、体检编号 153、开始日期 153、结束日期 153。
            const int designTotal = 143 + 139 + 153 + 153 + 153 + 153;
            var systemWidth = (int)Math.Round(available * 143d / designTotal);
            var systemIdWidth = (int)Math.Round(available * 139d / designTotal);
            var applicationWidth = (int)Math.Round(available * 153d / designTotal);
            var examWidth = (int)Math.Round(available * 153d / designTotal);
            var startWidth = (int)Math.Round(available * 153d / designTotal);
            var endWidth = available - systemWidth - systemIdWidth - applicationWidth - examWidth - startWidth;

            var x = left;
            x = PlaceSearchField(systemLabel, systemSelect, x, systemWidth, labelY, inputY, inputHeight);
            x = PlaceSearchField(label2, input1, x, systemIdWidth, labelY, inputY, inputHeight);
            x = PlaceSearchField(applicationLabel, applicationInput, x, applicationWidth, labelY, inputY, inputHeight);
            x = PlaceSearchField(examLabel, examInput, x, examWidth, labelY, inputY, inputHeight);
            var startDateX = x;
            x = PlaceSearchField(startDateLabel, startDatePicker, x, startWidth, labelY, inputY, inputHeight);
            var endDateX = x;
            x = PlaceSearchField(endDateLabel, endDatePicker, x, endWidth, labelY, inputY, inputHeight);

            queryDataButton.Location = new Point(startDateX, buttonY);
            queryDataButton.Size = new Size(startWidth, buttonHeight);
            uploadButton.Location = new Point(endDateX, buttonY);
            uploadButton.Size = new Size(endWidth, buttonHeight);

            peopleGrid.Location = new Point(12, 120);
            peopleGrid.Height = Math.Max(120, ClientSize.Height - 153);
            PositionPeopleListHeading();
        }

        private static int PlaceSearchField(Control label, Control input, int x, int width, int labelY, int inputY, int inputHeight)
        {
            label.Location = new Point(x, labelY);
            input.Location = new Point(x, inputY);
            input.Size = new Size(width, inputHeight);
            return x + width + 6;
        }

        private void PositionPeopleListHeading()
        {
            var top = peopleGrid.Top - label1.Height - 6;
            label1.Location = new Point(10, top);
            resultCountLabel.Location = new Point(label1.Right + 6, top);
        }

        private void ConfigurePeopleGrid()
        {
            const int baseWidth = 920;
            const int internalWidthReserve = 48;
            var scale = Math.Min(1d, Math.Max(0.1d,
                (peopleGrid.ClientSize.Width - internalWidthReserve) / (double)baseWidth));
            Func<int, string> columnWidth = width => Math.Max(1, (int)Math.Floor(width * scale)).ToString();

            peopleGrid.Columns = new AntdUI.ColumnCollection
            {
                new AntdUI.ColumnCheck("Selected", string.Empty) { Width = columnWidth(72) },
                new AntdUI.Column("ExamNumber", "体检编号") { Width = columnWidth(105), Ellipsis = true },
                new AntdUI.Column("PersonName", "姓名") { Width = columnWidth(90) },
                new AntdUI.Column("Gender", "性别") { Width = columnWidth(48) },
                new AntdUI.Column("Age", "年龄") { Width = columnWidth(48) },
                new AntdUI.Column("IdCardNumber", "身份证号") { Width = columnWidth(165), Ellipsis = true },
                new AntdUI.Column("CommunityName", "社区名称") { Width = columnWidth(125), Ellipsis = true },
                new AntdUI.Column("ExamDate", "总检日期") { Width = columnWidth(95) },
                new AntdUI.Column("Status", "上传状态") { Width = columnWidth(90) },
                new AntdUI.Column("ViewLog", "操作")
                {
                    Width = columnWidth(82),
                    Align = AntdUI.ColumnAlign.Left,
                    ColAlign = AntdUI.ColumnAlign.Center,
                    Render = (value, record, index) => new AntdUI.CellButton("view-upload-log", "查看", AntdUI.TTypeMini.Primary)
                }
            };
        }

        private void selectAllCheckBox_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            if (synchronizingSelectAll) return;
            foreach (var person in currentPeople) person.Selected = selectAllCheckBox.Checked;
            peopleGrid.Binding(new AntdUI.AntList<PersonRow>(currentPeople));
        }

        private void peopleGrid_CheckedChanged(object sender, AntdUI.TableCheckEventArgs e)
        {
            if (e.ColumnIndex != 0 || e.Record == null) return;
            var person = e.Record as PersonRow;
            if (person != null) person.Selected = e.Value;
            synchronizingSelectAll = true;
            selectAllCheckBox.Checked = currentPeople.Count > 0 && currentPeople.All(item => item.Selected);
            synchronizingSelectAll = false;
        }

        private void peopleGrid_CellButtonClick(object sender, AntdUI.TableButtonEventArgs e)
        {
            if (e.Btn == null || e.Btn.Id != "view-upload-log") return;
            ViewProgramLogRequested?.Invoke(this, EventArgs.Empty);
        }

        private async void queryDataButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AppConfig.ConnectionString))
            {
                MessageBox.Show(this, "尚未配置数据库连接。请先到“配置参数”保存 tjdatabase4 连接字符串。", "数据查询", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var startDate = startDatePicker.Value;
            var endDate = endDatePicker.Value;
            var hasStartDate = startDate.HasValue;
            var hasEndDate = endDate.HasValue;
            var examNumber = examInput.Text.Trim();
            var personName = applicationInput.Text.Trim();
            var unitName = systemSelect.Text.Trim();
            var ignoreDates = examNumber.Length > 0 || personName.Length > 0;
            if (!ignoreDates && hasStartDate != hasEndDate)
            {
                const string message = "开始日期和结束日期需要同时填写。";
                await TryWriteAuditLogAsync(操作日志.Program, "警告", "查询条件校验失败", message,
                    "开始日期=" + (hasStartDate ? startDate.Value.ToString("yyyy-MM-dd") : "未填写")
                    + "；结束日期=" + (hasEndDate ? endDate.Value.ToString("yyyy-MM-dd") : "未填写"));
                MessageBox.Show(this, message, "数据查询", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hasDateRange = !ignoreDates && hasStartDate && hasEndDate;
            if (!hasDateRange && examNumber.Length == 0 && personName.Length == 0 && unitName.Length == 0)
            {
                const string message = "请至少填写体检编号、人员姓名、单位名称，或选择完整的日期范围。";
                await TryWriteAuditLogAsync(操作日志.Program, "警告", "查询条件为空", message, null);
                MessageBox.Show(this, message, "数据查询", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (hasDateRange && startDate.Value.Date > endDate.Value.Date)
            {
                const string message = "开始日期不能晚于结束日期。";
                await TryWriteAuditLogAsync(操作日志.Program, "警告", "查询日期范围无效", message,
                    "开始日期=" + startDate.Value.ToString("yyyy-MM-dd") + "；结束日期=" + endDate.Value.ToString("yyyy-MM-dd"));
                MessageBox.Show(this, message, "数据查询", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            queryDataButton.Enabled = false;
            statusLabel.Text = "正在查询数据库...";
            var start = hasDateRange ? startDate.Value.Date : DateTime.MinValue;
            var endExclusive = hasDateRange ? endDate.Value.Date.AddDays(1) : DateTime.MaxValue;
            QueryPeopleAsync(hasDateRange, start, endExclusive, examNumber, personName, unitName);
        }

        private async void QueryPeopleAsync(bool hasDateRange, DateTime startDate, DateTime endExclusive, string examNumber, string personName, string unitName)
        {
            try
            {
                var people = await FindPeopleAsync(AppConfig.ConnectionString, hasDateRange, startDate, endExclusive,
                    examNumber, personName, unitName);
                if (IsDisposed) return;
                currentPeople = people;
                synchronizingSelectAll = true;
                selectAllCheckBox.Checked = false;
                synchronizingSelectAll = false;
                peopleGrid.Binding(new AntdUI.AntList<PersonRow>(currentPeople));
                peopleGrid.Empty = people.Count == 0;
                selectAllCheckBox.Visible = currentPeople.Count > 0;
                resultCountLabel.Text = "查询到 " + people.Count + " 人";
                LayoutSearchControls();
                statusLabel.Text = "查询完成。";
                await TryWriteAuditLogAsync(操作日志.Program, "常规", "查询人员",
                    "查询完成，共找到 " + people.Count + " 人",
                    "体检编号=" + (string.IsNullOrEmpty(examNumber) ? "未填写" : examNumber)
                    + "；姓名=" + (string.IsNullOrEmpty(personName) ? "未填写" : personName)
                    + "；单位名称=" + (string.IsNullOrEmpty(unitName) ? "未填写" : unitName)
                    + "；开始日期=" + (startDatePicker.Value.HasValue ? startDatePicker.Value.Value.ToString("yyyy-MM-dd") : "未填写")
                    + "；结束日期=" + (endDatePicker.Value.HasValue ? endDatePicker.Value.Value.ToString("yyyy-MM-dd") : "未填写")
                    + "；日期筛选=" + (hasDateRange ? "已应用" : "未应用")
                    + "；返回人数=" + people.Count);
            }
            catch (Exception ex)
            {
                if (IsDisposed) return;
                statusLabel.Text = "查询失败。";
                await TryWriteAuditLogAsync(操作日志.Program, "严重", "人员查询失败", ex.Message, null);
                MessageBox.Show(this, "数据查询失败：" + ex.Message, "数据查询", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed) queryDataButton.Enabled = true;
            }
        }

        private async void uploadButton_Click(object sender, EventArgs e)
        {
            var selected = currentPeople.Where(person => person.Selected).ToList();
            if (selected.Count == 0)
            {
                MessageBox.Show(this, "请先勾选需要上传的人员。", "上传数据", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(AppConfig.ConnectionString)
                || string.IsNullOrWhiteSpace(AppConfig.UploadBaseUrl)
                || string.IsNullOrWhiteSpace(AppConfig.BatchImportUserCode))
            {
                MessageBox.Show(this, "请先在“配置参数”中保存批量导入服务地址和账号代码。", "上传数据", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var failures = new List<string>();
            var successCount = 0;
            uploadButton.Enabled = false;
            queryDataButton.Enabled = false;
            try
            {
                statusLabel.Text = "正在检查批量导入接口密钥配置…";
                try
                {
                    await BatchImportUploadService.EnsureKeysAsync();
                }
                catch (Exception keyException)
                {
                    statusLabel.Text = "接口密钥获取失败。";
                    await TryWriteAuditLogAsync(操作日志.Program, "严重", "自动获取批量导入接口密钥失败", keyException.Message,
                        "本次上传尚未提交任何人员数据");
                    MessageBox.Show(this, "自动获取接口密钥失败：" + keyException.Message, "上传数据", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (var i = 0; i < selected.Count; i++)
                {
                    var person = selected[i];
                    person.Status = "正在上传";
                    peopleGrid.Binding(new AntdUI.AntList<PersonRow>(currentPeople));
                    statusLabel.Text = "正在上传第 " + (i + 1) + " / " + selected.Count + " 人：" + person.PersonName;
                    try
                    {
                        var result = await BatchImportUploadService.UploadPersonAsync(person);
                        person.Status = result.Success ? "上传成功" : "上传失败";
                        try
                        {
                            await BatchImportUploadService.SaveUploadStatusAsync(person.ExamNumber, result.Success ? "上传成功" : "上传失败");
                        }
                        catch (Exception statusException)
                        {
                            await TryWriteAuditLogAsync(操作日志.Upload, "严重", "写入上传状态失败", statusException.Message,
                                "体检编号=" + person.ExamNumber + "；接口上传结果=" + (result.Success ? "成功" : "失败"));
                        }
                        if (result.Success) successCount++;
                        else failures.Add(person.ExamNumber + "：" + result.Message);
                        await TryWriteAuditLogAsync(操作日志.Upload, result.Success ? "常规" : "警告",
                            result.Success ? "健康体检信息上传成功" : "健康体检信息上传失败", result.Message,
                            "当前用户=" + userName + "；体检编号=" + person.ExamNumber + "；姓名=" + person.PersonName);
                    }
                    catch (Exception ex)
                    {
                        person.Status = "上传失败";
                        try { await BatchImportUploadService.SaveUploadStatusAsync(person.ExamNumber, "上传失败"); }
                        catch (Exception statusException)
                        {
                            await TryWriteAuditLogAsync(操作日志.Upload, "严重", "写入上传状态失败", statusException.Message,
                                "体检编号=" + person.ExamNumber + "；原上传错误=" + ex.Message);
                        }
                        failures.Add(person.ExamNumber + "：" + ex.Message);
                        await TryWriteAuditLogAsync(操作日志.Upload, "警告", "健康体检信息上传失败", ex.Message,
                            "当前用户=" + userName + "；体检编号=" + person.ExamNumber + "；姓名=" + person.PersonName);
                    }
                    peopleGrid.Binding(new AntdUI.AntList<PersonRow>(currentPeople));
                }

                var summary = "上传完成：成功 " + successCount + " 人，失败 " + failures.Count + " 人。";
                statusLabel.Text = summary;
                await TryWriteAuditLogAsync(操作日志.Upload, failures.Count == 0 ? "常规" : "警告", "批量上传完成", summary,
                    "当前用户=" + userName + "；选择人数=" + selected.Count + "；成功人数=" + successCount + "；失败人数=" + failures.Count);
                var detail = failures.Count == 0 ? summary : summary + Environment.NewLine + Environment.NewLine
                    + string.Join(Environment.NewLine, failures.Take(8))
                    + (failures.Count > 8 ? Environment.NewLine + "其余失败详情请查看上传日志。" : string.Empty);
                MessageBox.Show(this, detail, "上传数据", MessageBoxButtons.OK,
                    failures.Count == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            finally
            {
                if (!IsDisposed)
                {
                    uploadButton.Enabled = true;
                    queryDataButton.Enabled = true;
                }
            }
        }

        private async Task TryWriteAuditLogAsync(string type, string level, string action, string message, string details)
        {
            try
            {
                await 操作日志.WriteAsync(type, level, userName, 登录限制.GetClientIp(), action, message, details);
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    MessageBox.Show(this, "操作未能写入数据库日志：" + ex.Message, "日志记录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static async Task<List<PersonRow>> FindPeopleAsync(string connectionString, bool hasDateRange, DateTime startDate, DateTime endExclusive, string examNumber, string personName, string unitName)
        {
            const string sql = @"
WITH ExamDates AS
(
    SELECT [体检编号], MAX([总检日期]) AS [总检日期]
    FROM [dbo].[总检_结果表]
    WHERE (@hasDateRange = 0 OR ([总检日期] >= @startDate AND [总检日期] < @endExclusive))
    GROUP BY [体检编号]
)
SELECT
    e.[体检编号], r.[姓名], r.[性别], r.[年龄], r.[身份证号], u.[单位名称], e.[总检日期], r.[省平台上传状态]
FROM ExamDates e
INNER JOIN [dbo].[人员_体检登记表] r ON r.[体检编号] = e.[体检编号]
LEFT JOIN [dbo].[基础数据_单位表] u ON u.[单位序号] = r.[单位序号]
WHERE (@examNumber = N'' OR e.[体检编号] LIKE @examPattern ESCAPE N'~')
  AND (@personName = N'' OR r.[姓名] LIKE @namePattern ESCAPE N'~')
  AND (@unitName = N'' OR u.[单位名称] LIKE @unitPattern ESCAPE N'~')
ORDER BY e.[总检日期] DESC, e.[体检编号]";
            var people = new List<PersonRow>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 20;
                command.Parameters.Add("@hasDateRange", System.Data.SqlDbType.Bit).Value = hasDateRange;
                command.Parameters.Add("@startDate", System.Data.SqlDbType.DateTime).Value = hasDateRange ? (object)startDate : DBNull.Value;
                command.Parameters.Add("@endExclusive", System.Data.SqlDbType.DateTime).Value = hasDateRange ? (object)endExclusive : DBNull.Value;
                command.Parameters.Add("@examNumber", System.Data.SqlDbType.NVarChar, 100).Value = examNumber;
                command.Parameters.Add("@examPattern", System.Data.SqlDbType.NVarChar, 202).Value = "%" + EscapeLike(examNumber) + "%";
                command.Parameters.Add("@personName", System.Data.SqlDbType.NVarChar, 100).Value = personName;
                command.Parameters.Add("@namePattern", System.Data.SqlDbType.NVarChar, 202).Value = "%" + EscapeLike(personName) + "%";
                command.Parameters.Add("@unitName", System.Data.SqlDbType.NVarChar, 200).Value = unitName;
                command.Parameters.Add("@unitPattern", System.Data.SqlDbType.NVarChar, 402).Value = "%" + EscapeLike(unitName) + "%";
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var age = reader.IsDBNull(3) ? 0 : (int)Math.Round(Convert.ToDouble(reader.GetValue(3)));
                        people.Add(new PersonRow(
                            ReadText(reader, 0),
                            ReadText(reader, 1),
                            ReadText(reader, 2),
                            age,
                            ReadText(reader, 4),
                            ReadText(reader, 5),
                            reader.IsDBNull(6) ? string.Empty : Convert.ToDateTime(reader.GetValue(6)).ToString("yyyy-MM-dd"),
                            ReadText(reader, 7).Length == 0 ? "待上传" : ReadText(reader, 7)));
                    }
                }
            }
            return people;
        }

        private static string ReadText(SqlDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? string.Empty : Convert.ToString(reader.GetValue(ordinal)).Trim();
        }

        private static string EscapeLike(string value)
        {
            return (value ?? string.Empty).Replace("~", "~~").Replace("%", "~%").Replace("_", "~_").Replace("[", "~[");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }

        public sealed class PersonRow
        {
            public PersonRow(string examNumber, string personName, string gender, int age, string idCardNumber, string communityName, string examDate, string status)
            {
                ExamNumber = examNumber;
                PersonName = personName;
                Gender = gender;
                Age = age;
                IdCardNumber = idCardNumber;
                CommunityName = communityName;
                ExamDate = examDate;
                Status = status;
            }

            public bool Selected { get; set; }
            public string ExamNumber { get; set; }
            public string PersonName { get; set; }
            public string Gender { get; set; }
            public int Age { get; set; }
            public string IdCardNumber { get; set; }
            public string CommunityName { get; set; }
            public string ExamDate { get; set; }
            public string Status { get; set; }
            public string ViewLog { get; set; } = "查看日志";
        }
         
    }
}
