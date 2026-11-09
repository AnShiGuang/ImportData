using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using System.IO;
using MySql.Data.MySqlClient;

namespace 批量导入程序重制版
{
    public class Database
    {
        private readonly string _dbType;
        private readonly string _connectionString;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
            _dbType = DetectDatabaseType(connectionString);
        }

        private static string DetectDatabaseType(string connectionString)
        {
            string lower = connectionString.ToLowerInvariant();

            if (lower.Contains("server=") && lower.Contains("database=") &&
                (lower.Contains("uid=") || lower.Contains("user id=")) && lower.Contains("pwd="))
                return "mysql";

            if (lower.Contains("data source=") &&
                (lower.Contains("user id=") || lower.Contains("uid=")) && lower.Contains("password=") &&
                (lower.Contains("sid=") || lower.Contains("service_name=")))
                return "oracle";

            if (lower.Contains("data source=") &&
                (lower.Contains("initial catalog=") || lower.Contains("database=")) &&
                (lower.Contains("user id=") || lower.Contains("uid=")) &&
                (lower.Contains("password=") || lower.Contains("pwd=")))
                return "sqlserver";

            throw new ArgumentException("无法识别的数据库连接字符串格式，请检查配置。");
        }

        private IDbConnection CreateConnection()
        {
            switch (_dbType)
            {
                case "mysql": return new MySqlConnection(_connectionString);
                case "oracle": return new OracleConnection(_connectionString);
                case "sqlserver": return new SqlConnection(_connectionString);
                default: throw new NotSupportedException("不支持的数据库类型");
            }
        }

        private IDbCommand CreateCommand(string sql, IDbConnection connection)
        {
            switch (_dbType)
            {
                case "mysql": return new MySqlCommand(sql, (MySqlConnection)connection);
                case "oracle": return new OracleCommand(sql, (OracleConnection)connection);
                case "sqlserver": return new SqlCommand(sql, (SqlConnection)connection);
                default: throw new NotSupportedException("不支持的数据库类型");
            }
        }

        private IDataParameter CreateParameter(string paramName, object value)
        {
            IDataParameter parameter;
            switch (_dbType)
            {
                case "mysql": parameter = new MySqlParameter(paramName, value); break;
                case "oracle": parameter = new OracleParameter(paramName, value); break;
                case "sqlserver": parameter = new SqlParameter(paramName, value); break;
                default: throw new NotSupportedException("不支持的数据库类型");
            }
            if (value is string) parameter.DbType = DbType.String;
            else if (value is int) parameter.DbType = DbType.Int32;
            return parameter;
        }

        private string GetParamPrefix()
        {
            return _dbType == "oracle" ? ":" : "@";
        }

        private string GetParamName(int index)
        {
            return GetParamPrefix() + "p" + index;
        }

        private string SanitizeForLog(string sql)
        {
            // 日志中截断过长的 SQL，避免日志膨胀
            return sql.Length > 500 ? sql.Substring(0, 500) + "..." : sql;
        }

        public void Insert(string table, string[] columns, object[] values)
        {
            string columnStr = string.Join(", ", columns);
            string[] paramNames = new string[columns.Length];
            for (int i = 0; i < columns.Length; i++) paramNames[i] = GetParamName(i);
            string paramStr = string.Join(", ", paramNames);
            string query = $"INSERT INTO {table} ({columnStr}) VALUES ({paramStr})";

            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    using (var command = CreateCommand(query, connection))
                    {
                        for (int i = 0; i < columns.Length; i++)
                            command.Parameters.Add(CreateParameter(paramNames[i], values[i]));
                        command.ExecuteNonQuery();
                    }
                }
                Log($"插入成功: {SanitizeForLog(query)}");
            }
            catch (Exception ex)
            {
                Log($"插入失败: {ex.Message} | SQL: {SanitizeForLog(query)}");
                throw;
            }
        }

        public void Delete(string table, string conditionColumn, object conditionValue)
        {
            string paramName = GetParamPrefix() + "p0";
            string query = $"DELETE FROM {table} WHERE {conditionColumn} = {paramName}";

            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    using (var command = CreateCommand(query, connection))
                    {
                        command.Parameters.Add(CreateParameter(paramName, conditionValue));
                        command.ExecuteNonQuery();
                    }
                }
                Log($"删除成功: {SanitizeForLog(query)}");
            }
            catch (Exception ex)
            {
                Log($"删除失败: {ex.Message} | SQL: {SanitizeForLog(query)}");
                throw;
            }
        }

        public void Update(string table, string[] setColumns, object[] setValues, string strWhere, string conditionColumn, object conditionValue)
        {
            string[] setClauses = new string[setColumns.Length];
            string[] paramNames = new string[setColumns.Length];
            for (int i = 0; i < setColumns.Length; i++)
            {
                paramNames[i] = GetParamName(i);
                setClauses[i] = $"{setColumns[i]} = {paramNames[i]}";
            }
            string setClause = string.Join(", ", setClauses);
            string condParamName = GetParamPrefix() + "cond";
            string query = $"UPDATE {table} SET {setClause} WHERE {strWhere} AND {conditionColumn} = {condParamName}";

            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    using (var command = CreateCommand(query, connection))
                    {
                        for (int i = 0; i < setColumns.Length; i++)
                            command.Parameters.Add(CreateParameter(paramNames[i], setValues[i]));
                        command.Parameters.Add(CreateParameter(condParamName, conditionValue));
                        command.ExecuteNonQuery();
                    }
                }
                Log($"更新成功: {SanitizeForLog(query)}");
            }
            catch (Exception ex)
            {
                Log($"更新失败: {ex.Message} | SQL: {SanitizeForLog(query)}");
                throw;
            }
        }

        public DataTable Select(string query, string[] paramNames = null, object[] paramValues = null)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    string execQuery = _dbType == "oracle" ? query.Replace("@", ":") : query;
                    using (var command = CreateCommand(execQuery, connection))
                    {
                        if (paramNames != null && paramValues != null && paramNames.Length == paramValues.Length)
                        {
                            for (int i = 0; i < paramNames.Length; i++)
                            {
                                string pName = _dbType == "oracle" ? paramNames[i].Replace("@", ":") : paramNames[i];
                                command.Parameters.Add(CreateParameter(pName, paramValues[i]));
                            }
                        }
                        using (var reader = command.ExecuteReader())
                            dataTable.Load(reader);
                    }
                }
                Log($"查询成功: {SanitizeForLog(query)}");
            }
            catch (Exception ex)
            {
                Log($"查询失败: {ex.Message} | SQL: {SanitizeForLog(query)}");
                throw;
            }
            return dataTable;
        }

        public DataInfo SelectDataInfo(string query, string[] paramNames = null, object[] paramValues = null)
        {
            var dataInfo = new DataInfo();
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    string execQuery = _dbType == "oracle" ? query.Replace("@", ":") : query;
                    using (var command = CreateCommand(execQuery, connection))
                    {
                        if (paramNames != null && paramValues != null && paramNames.Length == paramValues.Length)
                        {
                            for (int i = 0; i < paramNames.Length; i++)
                            {
                                string pName = _dbType == "oracle" ? paramNames[i].Replace("@", ":") : paramNames[i];
                                command.Parameters.Add(CreateParameter(pName, paramValues[i]));
                            }
                        }
                        dataInfo.dataTable = new DataTable();
                        using (var reader = command.ExecuteReader())
                            dataInfo.dataTable.Load(reader);
                    }
                }
                dataInfo.status = 0;
                Log($"查询成功: {SanitizeForLog(query)}");
            }
            catch (Exception ex)
            {
                dataInfo.status = -1;
                Log($"查询失败: {ex.Message} | SQL: {SanitizeForLog(query)}");
                throw; // 不吞异常，让调用方处理
            }
            return dataInfo;
        }

        /// <summary>
        /// 执行纯 SQL 查询，返回 DataTable（失败时抛出异常）
        /// </summary>
        public DataTable Query(string sql)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    using (var command = CreateCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                        dataTable.Load(reader);
                }
                Log($"查询成功: {SanitizeForLog(sql)}");
            }
            catch (Exception ex)
            {
                Log($"查询失败: {ex.Message} | SQL: {SanitizeForLog(sql)}");
                throw;
            }
            return dataTable;
        }

        /// <summary>
        /// 执行非查询 SQL（INSERT/UPDATE/DELETE），返回是否成功
        /// </summary>
        public bool ExecuteSql(string sql)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    using (var command = CreateCommand(sql, connection))
                        command.ExecuteNonQuery();
                }
                Log($"执行成功: {SanitizeForLog(sql)}");
                return true;
            }
            catch (Exception ex)
            {
                Log($"执行失败: {ex.Message} | SQL: {SanitizeForLog(sql)}");
                throw;
            }
        }

        public object GetFieldValue(string sql)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    using (var command = CreateCommand(sql, connection))
                    {
                        var result = command.ExecuteScalar();
                        return result ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"查询字段值失败: {ex.Message} | SQL: {SanitizeForLog(sql)}");
                throw;
            }
        }

        public object GetFieldValueByCondition(string table, string field, string condition)
        {
            string query = $"SELECT {field} FROM {table} WHERE 1=1 AND {condition}";
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    string execQuery = _dbType == "oracle" ? query.Replace("@", ":") : query;
                    using (var command = CreateCommand(execQuery, connection))
                    {
                        var result = command.ExecuteScalar();
                        return result ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"条件查询字段值失败: {ex.Message} | SQL: {SanitizeForLog(query)}");
                throw;
            }
        }

        public class DataInfo
        {
            public DataTable dataTable { get; set; }
            public int status { get; set; }
        }

        private void Log(string message)
        {
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            string logFile = Path.Combine(logDir, $"Log{DateTime.Now:yyyyMMdd}.txt");
            string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";

            try
            {
                File.AppendAllText(logFile, entry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"写入日志出错: {ex.Message}");
            }
        }

        public struct ConnectionTestResult
        {
            public bool IsConnected;
            public string DatabaseType;
        }

        public ConnectionTestResult TestConnection(string connectionString)
        {
            string dbType;
            try { dbType = DetectDatabaseType(connectionString); }
            catch { return new ConnectionTestResult { IsConnected = false, DatabaseType = "unknown" }; }

            var result = new ConnectionTestResult { DatabaseType = dbType };
            IDbConnection connection = null;
            try
            {
                switch (dbType)
                {
                    case "mysql": connection = new MySqlConnection(connectionString); break;
                    case "oracle": connection = new OracleConnection(connectionString); break;
                    case "sqlserver": connection = new SqlConnection(connectionString); break;
                    default: throw new NotSupportedException("不支持的数据库类型");
                }
                connection.Open();
                result.IsConnected = true;
            }
            catch
            {
                result.IsConnected = false;
            }
            finally
            {
                if (connection != null) connection.Dispose();
            }
            return result;
        }
    }
}
