using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace 批量导入程序重制版
{
    internal static class 操作日志
    {
        internal const string Program = "程序日志";
        internal const string Upload = "上传日志";
        internal const string All = "全部日志";

        internal static string FormatUserName(string account, string displayName)
        {
            var normalizedAccount = (account ?? string.Empty).Trim();
            var normalizedDisplayName = (displayName ?? string.Empty).Trim();
            if (normalizedDisplayName.Length == 0
                || string.Equals(normalizedAccount, normalizedDisplayName, StringComparison.OrdinalIgnoreCase))
                return normalizedAccount;
            if (normalizedAccount.Length == 0) return normalizedDisplayName;
            return normalizedDisplayName + "/" + normalizedAccount;
        }

        internal static async Task WriteAsync(string type, string level, string userName, string clientIp, string action, string message, string details = null)
        {
            const string sql = @"
IF OBJECT_ID(N'[dbo].[程序操作日志]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[程序操作日志]
    (
        [日志序号] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [日志类型] NVARCHAR(20) NOT NULL,
        [等级] NVARCHAR(20) NOT NULL,
        [用户名] NVARCHAR(128) NULL,
        [客户端IP] NVARCHAR(64) NULL,
        [发生时间] DATETIME NOT NULL,
        [操作] NVARCHAR(100) NOT NULL,
        [消息] NVARCHAR(1000) NOT NULL,
        [详情] NVARCHAR(MAX) NULL
    );
END;
INSERT INTO [dbo].[程序操作日志]
    ([日志类型], [等级], [用户名], [客户端IP], [发生时间], [操作], [消息], [详情])
VALUES
    (@type, @level, @userName, @clientIp, @occurredAt, @action, @message, @details);";

            using (var connection = new SqlConnection(AppConfig.ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 15;
                command.Parameters.Add("@type", SqlDbType.NVarChar, 20).Value = type;
                command.Parameters.Add("@level", SqlDbType.NVarChar, 20).Value = level;
                command.Parameters.Add("@userName", SqlDbType.NVarChar, 128).Value = (object)userName ?? DBNull.Value;
                command.Parameters.Add("@clientIp", SqlDbType.NVarChar, 64).Value = (object)clientIp ?? DBNull.Value;
                command.Parameters.Add("@occurredAt", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@action", SqlDbType.NVarChar, 100).Value = action;
                command.Parameters.Add("@message", SqlDbType.NVarChar, 1000).Value = message;
                command.Parameters.Add("@details", SqlDbType.NVarChar, -1).Value = (object)details ?? DBNull.Value;
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        internal static async Task<List<OperationLogEntry>> ReadAsync(string type)
        {
            const string ensureSql = @"
IF OBJECT_ID(N'[dbo].[程序操作日志]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[程序操作日志]
    (
        [日志序号] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [日志类型] NVARCHAR(20) NOT NULL,
        [等级] NVARCHAR(20) NOT NULL,
        [用户名] NVARCHAR(128) NULL,
        [客户端IP] NVARCHAR(64) NULL,
        [发生时间] DATETIME NOT NULL,
        [操作] NVARCHAR(100) NOT NULL,
        [消息] NVARCHAR(1000) NOT NULL,
        [详情] NVARCHAR(MAX) NULL
    );
END;";
            const string selectSql = @"
SELECT [日志序号], [日志类型], [等级], [用户名], [客户端IP], [发生时间], [操作], [消息], [详情]
FROM [dbo].[程序操作日志]
WHERE (@type = N'' OR [日志类型] = @type)
ORDER BY [发生时间] DESC, [日志序号] DESC;";

            var result = new List<OperationLogEntry>();
            using (var connection = new SqlConnection(AppConfig.ConnectionString))
            {
                await connection.OpenAsync();
                using (var ensure = new SqlCommand(ensureSql, connection))
                {
                    ensure.CommandTimeout = 15;
                    await ensure.ExecuteNonQueryAsync();
                }
                using (var command = new SqlCommand(selectSql, connection))
                {
                    command.CommandTimeout = 15;
                    command.Parameters.Add("@type", SqlDbType.NVarChar, 20).Value = type == All ? string.Empty : type;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new OperationLogEntry
                            {
                                Id = reader.GetInt64(0),
                                Type = reader.GetString(1),
                                Level = reader.GetString(2),
                                UserName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                ClientIp = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                OccurredAt = reader.GetDateTime(5).ToString("yyyy-MM-dd HH:mm:ss"),
                                Action = reader.GetString(6),
                                Message = reader.GetString(7),
                                Details = reader.IsDBNull(8) ? string.Empty : reader.GetString(8)
                            });
                        }
                    }
                }
            }
            return result;
        }
    }

    internal sealed class OperationLogEntry
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public string UserName { get; set; }
        public string ClientIp { get; set; }
        public string OccurredAt { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public string View { get; set; } = "查看";
    }
}
