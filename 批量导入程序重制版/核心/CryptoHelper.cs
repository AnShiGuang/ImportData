using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace 批量导入程序重制版
{
    /// <summary>
    /// AES-256-GCM 加密帮助类，用于加密/解密敏感配置（如连接字符串）
    /// 密钥基于机器标识派生，配置文件无法在其他机器上解密
    /// </summary>
    internal static class CryptoHelper
    {
        private const int KeySize = 32;   // AES-256
        private const int IvSize = 16;    // AES block size
        private const int TagSize = 32;   // HMAC-SHA256 tag
        private const int SaltSize = 16;
        private const int Pbkdf2Iterations = 100000;

        /// <summary>
        /// 用 AES-256-CBC + HMAC-SHA256（Encrypt-then-MAC）加密明文
        /// </summary>
        public static string AesEncrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;

            byte[] key = DeriveKey();
            byte[] iv = new byte[IvSize];
            using (var rng = new RNGCryptoServiceProvider()) rng.GetBytes(iv);

            byte[] cipherBytes;
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var enc = aes.CreateEncryptor())
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, enc, CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.FlushFinalBlock();
                    cipherBytes = ms.ToArray();
                }
            }

            // HMAC tag = HMAC-SHA256(key, iv || ciphertext)
            byte[] tag;
            using (var hmac = new HMACSHA256(key))
            using (var ms = new MemoryStream())
            {
                ms.Write(iv, 0, iv.Length);
                ms.Write(cipherBytes, 0, cipherBytes.Length);
                tag = hmac.ComputeHash(ms.ToArray());
            }

            // output = tag(32) + iv(16) + ciphertext
            byte[] result = new byte[TagSize + IvSize + cipherBytes.Length];
            Buffer.BlockCopy(tag, 0, result, 0, TagSize);
            Buffer.BlockCopy(iv, 0, result, TagSize, IvSize);
            Buffer.BlockCopy(cipherBytes, 0, result, TagSize + IvSize, cipherBytes.Length);

            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// 解密 AES-256-CBC + HMAC-SHA256 密文
        /// </summary>
        public static string AesDecrypt(string cipherBase64)
        {
            if (string.IsNullOrEmpty(cipherBase64)) return string.Empty;

            byte[] data = Convert.FromBase64String(cipherBase64);
            if (data.Length < TagSize + IvSize + 1)
                throw new CryptographicException("密文数据过短");

            byte[] key = DeriveKey();

            // 提取 tag, iv, ciphertext
            byte[] tag = new byte[TagSize];
            byte[] iv = new byte[IvSize];
            byte[] cipherBytes = new byte[data.Length - TagSize - IvSize];
            Buffer.BlockCopy(data, 0, tag, 0, TagSize);
            Buffer.BlockCopy(data, TagSize, iv, 0, IvSize);
            Buffer.BlockCopy(data, TagSize + IvSize, cipherBytes, 0, cipherBytes.Length);

            // 验证 HMAC tag
            byte[] expectedTag;
            using (var hmac = new HMACSHA256(key))
            using (var ms = new MemoryStream())
            {
                ms.Write(iv, 0, iv.Length);
                ms.Write(cipherBytes, 0, cipherBytes.Length);
                expectedTag = hmac.ComputeHash(ms.ToArray());
            }

            if (!ConstantTimeEquals(tag, expectedTag))
                throw new CryptographicException("HMAC 验证失败，数据可能被篡改");

            // 解密
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var dec = aes.CreateDecryptor())
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, dec, CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// 解密存储值：自动识别 "AES:" 前缀使用新算法，否则回退到旧 DES 算法
        /// </summary>
        public static string DecryptStored(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.StartsWith("AES:"))
                return AesDecrypt(value.Substring(4));
            // 向后兼容旧 DES 加密
            return MD5Helper.MD5Decrypt(value);
        }

        /// <summary>
        /// 加密存储值（统一使用 AES，带 "AES:" 前缀标识）
        /// </summary>
        public static string EncryptForStore(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;
            return "AES:" + AesEncrypt(plainText);
        }

        /// <summary>
        /// 使用 PBKDF2-SHA256 对密码进行哈希（不可逆，用于密码存储）
        /// </summary>
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider()) rng.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(KeySize);
                // output = iterations(hex4) + salt(hex32) + hash(hex64)
                return Pbkdf2Iterations.ToString("X4")
                     + BitConverter.ToString(salt).Replace("-", "")
                     + BitConverter.ToString(hash).Replace("-", "");
            }
        }

        /// <summary>
        /// 验证密码是否匹配 PBKDF2 哈希
        /// </summary>
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(storedHash) || storedHash.Length < 4) return false;

            int iterations;
            if (!int.TryParse(storedHash.Substring(0, 4), System.Globalization.NumberStyles.HexNumber, null, out iterations))
                return false;

            byte[] salt = HexToBytes(storedHash.Substring(4, 32));
            byte[] expectedHash = HexToBytes(storedHash.Substring(36, 64));

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] actualHash = pbkdf2.GetBytes(expectedHash.Length);
                return ConstantTimeEquals(expectedHash, actualHash);
            }
        }

        /// <summary>
        /// 基于机器标识派生加密密钥（每台机器、每个用户唯一，配置文件无法跨机器使用）
        /// </summary>
        private static byte[] DeriveKey()
        {
            string machineId = Environment.MachineName + "|" + Environment.UserName;
            byte[] salt = Encoding.UTF8.GetBytes("批量导入程序重制版_v1.7");
            using (var pbkdf2 = new Rfc2898DeriveBytes(machineId, salt, 50000))
                return pbkdf2.GetBytes(KeySize);
        }

        /// <summary>
        /// 恒定时间比较，防止时序攻击
        /// </summary>
        private static bool ConstantTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }

        private static byte[] HexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return bytes;
        }
    }
}
