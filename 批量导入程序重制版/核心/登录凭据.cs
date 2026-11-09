using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace 批量导入程序重制版
{
    internal sealed class LoginCredential
    {
        internal string UserName;
        internal string Password;
        internal bool RememberPassword;
        internal bool AutoLogin;
    }

    internal static class 登录凭据
    {
        private const string RegistryPath = @"Software\批量导入程序重制版\Login";

        internal static LoginCredential Load()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RegistryPath, true))
            {
                if (key == null) return new LoginCredential();
                var encrypted = key.GetValue("Password", "") as string;
                var storedUserName = key.GetValue("UserName", "") as string;
                var userName = DecryptLegacyCompatible(storedUserName);

                if (!string.IsNullOrEmpty(userName))
                    key.SetValue("UserName", Encrypt(userName), RegistryValueKind.String);
                return new LoginCredential
                {
                    UserName = userName,
                    Password = Decrypt(encrypted),
                    RememberPassword = Convert.ToBoolean(key.GetValue("RememberPassword", false)),
                    AutoLogin = Convert.ToBoolean(key.GetValue("AutoLogin", false))
                };
            }
        }

        internal static void Save(string userName, string password, bool rememberPassword, bool autoLogin)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(RegistryPath))
            {
                if (key == null) return;

                key.SetValue("UserName", rememberPassword ? Encrypt(userName) : "", RegistryValueKind.String);
                key.SetValue("Password", rememberPassword ? Encrypt(password) : "", RegistryValueKind.String);
                key.SetValue("RememberPassword", rememberPassword, RegistryValueKind.DWord);
                key.SetValue("AutoLogin", autoLogin, RegistryValueKind.DWord);
            }
        }

        internal static void ClearSaved()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RegistryPath, true))
            {
                if (key != null) Clear(key);
            }
        }

        private static void Clear(RegistryKey key)
        {
            key.SetValue("UserName", "", RegistryValueKind.String);
            key.SetValue("Password", "", RegistryValueKind.String);
            key.SetValue("RememberPassword", false, RegistryValueKind.DWord);
            key.SetValue("AutoLogin", false, RegistryValueKind.DWord);
        }

        private static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            var bytes = ProtectedData.Protect(Encoding.UTF8.GetBytes(value), null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(bytes);
        }

        private static string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            try
            {
                var bytes = ProtectedData.Unprotect(Convert.FromBase64String(value), null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (CryptographicException) { return ""; }
            catch (FormatException) { return ""; }
        }

        private static string DecryptLegacyCompatible(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            var decrypted = Decrypt(value);
            // Earlier builds stored the account name as plain text. Read it once,
            // then immediately rewrite it using DPAPI in the current-user scope.
            return string.IsNullOrEmpty(decrypted) ? value : decrypted;
        }
    }
}
