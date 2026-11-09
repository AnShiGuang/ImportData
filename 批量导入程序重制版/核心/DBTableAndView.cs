using SqlSugar;
using System;
using System.Linq;
using DbType = SqlSugar.DbType;
using 批量导入程序重制版.TiJian4;

namespace 批量导入程序重制版
{
    public class DBTableAndView
    {
        private AppSettings m_settings => SettingHelper.LoadConfig();
 
        public bool CheckAndCreateTable()
        {
            try
            {
                 SqlSugarClient _db = InitSqlSugarClient();
                // ========== 核心：LINQ语法判断表是否存在 ==========
                // 获取数据库所有表名（适配不同数据库的表名查询语法）
                var allTableNames = _db.DbMaintenance.GetTableInfoList()
                                       .Select(t => t.Name)
                                       .ToList();

                // 判断表是否存在（忽略大小写，适配不同数据库的大小写敏感规则）
                bool isTableExist = allTableNames.Any(t =>
                    t.Equals("接口_结果中间表", StringComparison.OrdinalIgnoreCase));


                if (!isTableExist)
                {
                    _db.CodeFirst.InitTables(typeof(JiKou_JieGuoZhongJianBiao));
                }

                isTableExist = allTableNames.Any(t =>
                t.Equals("接口_配置信息表", StringComparison.OrdinalIgnoreCase));

                if (!isTableExist)
                {
                    _db.CodeFirst.InitTables(typeof(JiKou_PeiZhiXinXiBiao));
                }

                isTableExist = allTableNames.Any(t =>
                    t.Equals("AppVersionLog", StringComparison.OrdinalIgnoreCase));

                if (!isTableExist)
                {
                    _db.CodeFirst.InitTables(typeof(AppVersionLog));
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"判断/创建表失败：{ex.Message}");
                return false;
            }
        }
        public SqlSugarClient WJInitSqlSugarClient(string constr)
        {
            if (string.IsNullOrWhiteSpace(constr))
            {
                throw new ArgumentNullException(nameof(constr), "数据库连接字符串不能为空");
            }

            return new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = constr,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                MoreSettings = new ConnMoreSettings
                {
                    PgSqlIsAutoToLower = false,
                    PgSqlIsAutoToLowerCodeFirst = false,
                }
            });
        }
        public SqlSugarClient InitSqlSugarClient()
        {
            if (string.IsNullOrWhiteSpace(m_settings.MainDbConnStr))
            {
                throw new ArgumentNullException(nameof(m_settings.MainDbConnStr), "数据库连接字符串不能为空");
            }

            return new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = m_settings.MainDbConnStr,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                MoreSettings = new ConnMoreSettings
                {
                    PgSqlIsAutoToLower = false,
                    PgSqlIsAutoToLowerCodeFirst = false,
                }
            });
        }
    }
}
