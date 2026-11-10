using System;
using System.Data;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 接苯放射设置：对应旧程序 接苯设置窗体 的后台逻辑，写入 接苯设置 表。
    /// </summary>
    public static class 接苯放射Helper
    {
        public const string 表名 = "接苯设置";

        public static void 创建表(string connectionString)
        {
            var db = new Database(connectionString);
            db.ExecuteSql(@"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'接苯设置')
BEGIN
CREATE TABLE [接苯设置] (
tid INT IDENTITY(1,1) PRIMARY KEY,
[危害因素名称] VARCHAR(500),
[危害因素序号] INT,
[组合项目名称] VARCHAR(500),
[组合项目序号] INT,
[检验明细项目名称] VARCHAR(500),
[检验项目编码] VARCHAR(500),
[参考下限] VARCHAR(2500),
[参考上限] VARCHAR(2500),
[性别] VARCHAR(500)
);
END");
        }

        private static string 转义(string value)
        {
            return (value ?? string.Empty).Replace("'", "''");
        }

        /// <summary>
        /// 删除某危害因素+性别下的全部配置
        /// </summary>
        public static void 删除配置(string connectionString, string 危害因素, string 性别)
        {
            var db = new Database(connectionString);
            db.ExecuteSql($"delete from {表名} where 危害因素名称 ='{转义(危害因素)}' and 性别 = '{转义(性别)}'");
        }

        /// <summary>
        /// 删除“需要排除的危害因素”配置（不分性别）
        /// </summary>
        public static void 删除需要排除的危害因素(string connectionString)
        {
            var db = new Database(connectionString);
            db.ExecuteSql($"delete from {表名} where 危害因素名称 ='需要排除的危害因素'");
        }

        /// <summary>
        /// 新增一条参考值配置
        /// </summary>
        public static void 添加配置(string connectionString, string 危害因素, string 组合项目名称, string 明细名称, string 参考下限, string 参考上限, string 性别)
        {
            var db = new Database(connectionString);
            db.Insert(
                表名,
                new[] { "危害因素名称", "组合项目名称", "检验明细项目名称", "检验项目编码", "参考下限", "参考上限", "性别" },
                new object[] { 危害因素 ?? string.Empty, 组合项目名称 ?? string.Empty, 明细名称 ?? string.Empty, string.Empty, 参考下限 ?? string.Empty, 参考上限 ?? string.Empty, 性别 ?? string.Empty });
        }

        /// <summary>
        /// 读取某危害因素+性别下的全部配置
        /// </summary>
        public static DataTable 读取配置(string connectionString, string 危害因素, string 性别)
        {
            var db = new Database(connectionString);
            return db.Query($"select * from {表名} where 危害因素名称 ='{转义(危害因素)}' and 性别 = '{转义(性别)}'");
        }
    }
}
