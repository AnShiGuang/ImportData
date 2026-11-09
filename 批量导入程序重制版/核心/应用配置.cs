using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace 批量导入程序重制版
{
    internal static class AppConfig
    {
        internal static string Get(string key, string fallback = "")
        {
            return ConfigurationManager.AppSettings[key] ?? fallback;
        }

        internal static void Set(string key, string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var setting = configuration.AppSettings.Settings[key];
            if (setting == null) configuration.AppSettings.Settings.Add(key, value);
            else setting.Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        internal static string ConnectionString
        {
            get
            {
                var protectedValue = Get("DbConnectionStringProtected");
                if (!string.IsNullOrWhiteSpace(protectedValue))
                {
                    try
                    {
                        var cipher = Convert.FromBase64String(protectedValue);
                        var plain = ProtectedData.Unprotect(cipher, Encoding.UTF8.GetBytes("批量导入程序重制版.Database"), DataProtectionScope.CurrentUser);
                        return Encoding.UTF8.GetString(plain);
                    }
                    catch (CryptographicException) { }
                    catch (FormatException) { }
                }

                return Get("DbConnectionString");
            }
        }

        internal static string UnitLookupConnectionString
        {
            get { return ConnectionString; }
        }

        internal static string UploadBaseUrl { get { return Get("UploadBaseUrl"); } }
        internal static string BatchImportAppId { get { return GetProtected("BatchImportAppId", "批量导入程序重制版.AppId"); } }
        internal static string BatchImportPublicKey { get { return GetProtected("BatchImportPublicKey", "批量导入程序重制版.PublicKey"); } }
        internal static string BatchImportPrivateKey { get { return GetProtected("BatchImportPrivateKey", "批量导入程序重制版.PrivateKey"); } }
        internal static string BatchImportUserCode { get { return GetProtected("BatchImportUserCode", "批量导入程序重制版.UserCode"); } }

        internal static bool HasDatabaseConnection
        {
            get { return !string.IsNullOrWhiteSpace(ConnectionString); }
        }

        internal static void SetDatabaseConnectionString(string value)
        {
            var builder = new SqlConnectionStringBuilder(value);
            if (!string.Equals(builder.InitialCatalog, "tjdatabase4", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("数据库必须设置为 tjdatabase4。");

            builder.PersistSecurityInfo = false;
            builder.ConnectTimeout = 8;
            var plain = Encoding.UTF8.GetBytes(builder.ConnectionString);
            var cipher = ProtectedData.Protect(plain, Encoding.UTF8.GetBytes("批量导入程序重制版.Database"), DataProtectionScope.CurrentUser);
            Set("DbConnectionStringProtected", Convert.ToBase64String(cipher));
        }

        internal static void ClearDatabaseConnectionString()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var setting = configuration.AppSettings.Settings["DbConnectionStringProtected"];
            if (setting == null) return;
            configuration.AppSettings.Settings.Remove("DbConnectionStringProtected");
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        internal static void SetBatchImportCredentials(string appId, string publicKey, string privateKey)
        {
            SetProtected("BatchImportAppId", appId, "批量导入程序重制版.AppId");
            SetProtected("BatchImportPublicKey", publicKey, "批量导入程序重制版.PublicKey");
            SetProtected("BatchImportPrivateKey", privateKey, "批量导入程序重制版.PrivateKey");
        }

        internal static void SetBatchImportUserCode(string userCode)
        {
            SetProtected("BatchImportUserCode", userCode, "批量导入程序重制版.UserCode");
        }

        private static string GetProtected(string key, string entropyText)
        {
            var value = Get(key);
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            try
            {
                var cipher = Convert.FromBase64String(value);
                var plain = ProtectedData.Unprotect(cipher, Encoding.UTF8.GetBytes(entropyText), DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(plain);
            }
            catch (CryptographicException) { return string.Empty; }
            catch (FormatException) { return string.Empty; }
        }

        private static void SetProtected(string key, string value, string entropyText)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (configuration.AppSettings.Settings[key] != null)
                    configuration.AppSettings.Settings.Remove(key);
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return;
            }

            var plain = Encoding.UTF8.GetBytes(value.Trim());
            var cipher = ProtectedData.Protect(plain, Encoding.UTF8.GetBytes(entropyText), DataProtectionScope.CurrentUser);
            Set(key, Convert.ToBase64String(cipher));
        }
    }
}
