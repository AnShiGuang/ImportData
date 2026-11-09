using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 批量导入程序重制版
{
    internal sealed class 登录锁定状态
    {
        internal TimeSpan 剩余时间 { get; set; }
        internal string 服务端客户端地址 { get; set; }
    }

    internal static class 登录限制
    {
        private static readonly object EnsureTableSync = new object();
        private static bool tableEnsured;

        private const string EnsureTableSql = @"
IF OBJECT_ID(N'[dbo].[登录失败限制]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[登录失败限制]
    (
        [范围类型] NVARCHAR(16) NOT NULL,
        [范围键] CHAR(64) NOT NULL,
        [失败次数] INT NOT NULL,
        [窗口开始UTC] DATETIME2(0) NOT NULL,
        [锁定截止UTC] DATETIME2(0) NULL,
        CONSTRAINT [PK_登录失败限制] PRIMARY KEY ([范围类型], [范围键])
    );
END;";

        internal static string GetClientIp()
        {
            try
            {
                var adapters = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .OrderByDescending(n => n.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(g.Address)));
                foreach (var adapter in adapters)
                {
                    var address = adapter.GetIPProperties().UnicastAddresses
                        .Select(a => a.Address)
                        .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(a));
                    if (address != null) return address.ToString();
                }
            }
            catch { }
            return "127.0.0.1";
        }

        internal static Task<登录锁定状态> GetRemainingLockAsync(string userName)
        {
            return Task.Run(() => GetRemainingLock(userName));
        }

        internal static Task<登录锁定状态> RegisterFailureAsync(string userName)
        {
            return Task.Run(() => RegisterFailure(userName));
        }

        internal static Task ResetAsync(string userName)
        {
            return Task.Run(() => Reset(userName));
        }

        private static 登录锁定状态 GetRemainingLock(string userName)
        {
            using (var connection = OpenConnection())
            {
                var scopes = GetScopes(connection, userName);
                using (var command = new SqlCommand(@"
SELECT CONVERT(INT, ISNULL(MAX(DATEDIFF(SECOND, SYSUTCDATETIME(), [锁定截止UTC])), 0))
FROM [dbo].[登录失败限制]
WHERE (([范围类型] = N'账号' AND [范围键] = @accountKey)
   OR (@clientKey IS NOT NULL AND [范围类型] = N'地址' AND [范围键] = @clientKey))
  AND [锁定截止UTC] > SYSUTCDATETIME();", connection))
                {
                    command.CommandTimeout = 8;
                    command.Parameters.Add("@accountKey", SqlDbType.Char, 64).Value = scopes.AccountKey;
                    command.Parameters.Add("@clientKey", SqlDbType.Char, 64).Value = (object)scopes.ClientKey ?? DBNull.Value;
                    var seconds = Convert.ToInt32(command.ExecuteScalar());
                    return new 登录锁定状态
                    {
                        剩余时间 = TimeSpan.FromSeconds(Math.Max(0, seconds)),
                        服务端客户端地址 = scopes.ClientAddress
                    };
                }
            }
        }

        private static 登录锁定状态 RegisterFailure(string userName)
        {
            using (var connection = OpenConnection())
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                var scopes = GetScopes(connection, userName, transaction);
                var remaining = TimeSpan.Zero;
                foreach (var scope in scopes.Keys)
                {
                    int failures;
                    DateTime windowStart;
                    DateTime? lockedUntil;
                    using (var read = new SqlCommand(@"
SELECT [失败次数], [窗口开始UTC], [锁定截止UTC]
FROM [dbo].[登录失败限制] WITH (UPDLOCK, HOLDLOCK)
WHERE [范围类型] = @scopeType AND [范围键] = @scopeKey;", connection, transaction))
                    {
                        read.CommandTimeout = 8;
                        read.Parameters.Add("@scopeType", SqlDbType.NVarChar, 16).Value = scope.Type;
                        read.Parameters.Add("@scopeKey", SqlDbType.Char, 64).Value = scope.Key;
                        using (var reader = read.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                failures = reader.GetInt32(0);
                                windowStart = DateTime.SpecifyKind(reader.GetDateTime(1), DateTimeKind.Utc);
                                lockedUntil = reader.IsDBNull(2) ? (DateTime?)null : DateTime.SpecifyKind(reader.GetDateTime(2), DateTimeKind.Utc);
                            }
                            else
                            {
                                failures = 0;
                                windowStart = DateTime.MinValue;
                                lockedUntil = null;
                            }
                        }
                    }

                    DateTime now;
                    using (var clock = new SqlCommand("SELECT SYSUTCDATETIME();", connection, transaction))
                    {
                        clock.CommandTimeout = 8;
                        now = DateTime.SpecifyKind(Convert.ToDateTime(clock.ExecuteScalar()), DateTimeKind.Utc);
                    }
                    if (windowStart == DateTime.MinValue || now - windowStart >= TimeSpan.FromMinutes(15))
                    {
                        failures = 0;
                        windowStart = now;
                    }
                    failures++;
                    var duration = GetLockDuration(failures);
                    if (duration > TimeSpan.Zero) lockedUntil = now.Add(duration);
                    if (lockedUntil.HasValue && lockedUntil.Value > now && lockedUntil.Value - now > remaining)
                        remaining = lockedUntil.Value - now;

                    using (var write = new SqlCommand(@"
IF EXISTS (SELECT 1 FROM [dbo].[登录失败限制] WITH (UPDLOCK, HOLDLOCK) WHERE [范围类型] = @scopeType AND [范围键] = @scopeKey)
    UPDATE [dbo].[登录失败限制]
    SET [失败次数] = @failures, [窗口开始UTC] = @windowStart, [锁定截止UTC] = @lockedUntil
    WHERE [范围类型] = @scopeType AND [范围键] = @scopeKey;
ELSE
    INSERT INTO [dbo].[登录失败限制] ([范围类型], [范围键], [失败次数], [窗口开始UTC], [锁定截止UTC])
    VALUES (@scopeType, @scopeKey, @failures, @windowStart, @lockedUntil);", connection, transaction))
                    {
                        write.CommandTimeout = 8;
                        write.Parameters.Add("@scopeType", SqlDbType.NVarChar, 16).Value = scope.Type;
                        write.Parameters.Add("@scopeKey", SqlDbType.Char, 64).Value = scope.Key;
                        write.Parameters.Add("@failures", SqlDbType.Int).Value = failures;
                        write.Parameters.Add("@windowStart", SqlDbType.DateTime2).Value = windowStart;
                        write.Parameters.Add("@lockedUntil", SqlDbType.DateTime2).Value = (object)lockedUntil ?? DBNull.Value;
                        write.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
                return new 登录锁定状态 { 剩余时间 = remaining, 服务端客户端地址 = scopes.ClientAddress };
            }
        }

        private static void Reset(string userName)
        {
            using (var connection = OpenConnection())
            using (var command = new SqlCommand(@"
DELETE FROM [dbo].[登录失败限制]
WHERE [范围类型] = N'账号' AND [范围键] = @accountKey;", connection))
            {
                command.CommandTimeout = 8;
                command.Parameters.Add("@accountKey", SqlDbType.Char, 64).Value = Hash("A\0" + NormalizeAccount(userName));
                command.ExecuteNonQuery();
            }
        }

        private static SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(AppConfig.ConnectionString);
            try
            {
                connection.Open();
                if (!tableEnsured)
                {
                    lock (EnsureTableSync)
                    {
                        if (!tableEnsured)
                        {
                            using (var ensure = new SqlCommand(EnsureTableSql, connection))
                            {
                                ensure.CommandTimeout = 8;
                                ensure.ExecuteNonQuery();
                            }
                            tableEnsured = true;
                        }
                    }
                }
                return connection;
            }
            catch { connection.Dispose(); throw; }
        }

        private static ScopeSet GetScopes(SqlConnection connection, string userName, SqlTransaction transaction = null)
        {
            var account = (userName ?? string.Empty).Trim().ToUpperInvariant();
            if (account.Length == 0) throw new ArgumentException("登录账号不能为空。", "userName");
            string clientAddress;
            using (var command = new SqlCommand("SELECT CONVERT(NVARCHAR(64), CONNECTIONPROPERTY('client_net_address'));", connection, transaction))
            {
                command.CommandTimeout = 8;
                clientAddress = Convert.ToString(command.ExecuteScalar());
            }
            var normalizedAddress = IPAddress.TryParse(clientAddress, out var parsedAddress) ? parsedAddress.ToString() : null;
            var scopes = new List<ScopeKey> { new ScopeKey("账号", Hash("A\0" + account)) };
            var clientKey = normalizedAddress == null ? null : Hash("I\0" + normalizedAddress);
            if (clientKey != null) scopes.Add(new ScopeKey("地址", clientKey));
            return new ScopeSet(scopes, Hash("A\0" + account), clientKey, normalizedAddress);
        }

        private static TimeSpan GetLockDuration(int failures)
        {
            if (failures >= 10) return TimeSpan.FromMinutes(5);
            if (failures >= 4) return TimeSpan.FromMinutes(1);
            if (failures >= 3) return TimeSpan.FromSeconds(30);
            return TimeSpan.Zero;
        }

        private static string Hash(string value)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
                var result = new StringBuilder(bytes.Length * 2);
                foreach (var item in bytes) result.Append(item.ToString("x2"));
                return result.ToString();
            }
        }

        private static string NormalizeAccount(string userName)
        {
            var account = (userName ?? string.Empty).Trim().ToUpperInvariant();
            if (account.Length == 0) throw new ArgumentException("登录账号不能为空。", "userName");
            return account;
        }

        private sealed class ScopeKey
        {
            internal ScopeKey(string type, string key) { Type = type; Key = key; }
            internal string Type { get; private set; }
            internal string Key { get; private set; }
        }

        private sealed class ScopeSet
        {
            internal ScopeSet(List<ScopeKey> keys, string accountKey, string clientKey, string clientAddress)
            {
                Keys = keys;
                AccountKey = accountKey;
                ClientKey = clientKey;
                ClientAddress = clientAddress;
            }
            internal List<ScopeKey> Keys { get; private set; }
            internal string AccountKey { get; private set; }
            internal string ClientKey { get; private set; }
            internal string ClientAddress { get; private set; }
        }
    }
}
