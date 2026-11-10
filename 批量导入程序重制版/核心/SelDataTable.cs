using AntdUI;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using 批量导入程序重制版.TiJian4;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 外接数据查询与导入：按“接口_配置信息表”的标识配置查询外接数据库，
    /// 并批量写入本程序“接口_结果中间表”，再关联出人员列表。
    /// </summary>
    public class SelDataTable
    {
        private AppSettings _settings = SettingHelper.LoadConfig();
        private SqlSugarClient _db;

        public void logOut()
        {
            try
            {
                _settings.AutoLogin = "false";
                _settings.RememberPwd = "false";
                _settings.SavedUserName = "";
                _settings.CurrentUser = "";
                _settings.SavedPwd = "";
                SettingHelper.SaveConfig(_settings);
            }
            catch (Exception ex)
            {
                new 提示窗体("注销异常：", "清除登录信息失败，详细信息：\r" + ex.ToString(), false, false, "好的", "").ShowDialog();
            }
        }

        public static void SqlBulkCopyInsert(string conStr, string tableName, DataTable dtData)
        {
            if (dtData == null || dtData.Rows.Count == 0) return;
            try
            {
                using (var bulkCopy = new SqlBulkCopy(conStr))
                {
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BulkCopyTimeout = 300;
                    bulkCopy.WriteToServer(dtData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"批量写入表 {tableName} 失败：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 从外接数据库查询数据并导入到本地中间表，返回展示列表
        /// </summary>
        public async Task<BindingList<ClassInfo.UserModel>> loadPersonInfo(
            List<接口_配置信息表> _configInfos, string strConfigName,
            string strReqNo, string strExamNo, DateTime? dtStart, DateTime? dtEnd)
        {
            try
            {
                _db = new DBTableAndView().InitSqlSugarClient();

                var queryResult = await BuildExternalQueryAsync(strConfigName, strReqNo, strExamNo, dtStart, dtEnd);
                if (string.IsNullOrEmpty(queryResult.Sql))
                    return new BindingList<ClassInfo.UserModel>();

                if (queryResult.ExtConnStrUpdated)
                {
                    _settings.ExtDbConnStr = queryResult.ExtConnStr;
                    _settings.DeptType = queryResult.DeptType;
                    SettingHelper.SaveConfig(_settings);
                }

                await _db.Deleteable<JiKou_JieGuoZhongJianBiao>().ExecuteCommandAsync();

                await Task.Run(() =>
                {
                    var extDb = new Database(_settings.ExtDbConnStr);
                    DataTable extData = extDb.Query(queryResult.Sql);
                    SqlBulkCopyInsert(_settings.MainDbConnStr, "接口_结果中间表", extData);
                });

                return await loadPersonInfos();
            }
            catch (Exception ex)
            {
                throw new Exception("数据加载失败：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 从中间表查询关联数据，返回展示列表
        /// </summary>
        public async Task<BindingList<ClassInfo.UserModel>> loadPersonInfos()
        {
            try
            {
                _settings = SettingHelper.LoadConfig();

                List<PersonRow> list;
                if ((_settings.DeptType ?? string.Empty).Contains("检验"))
                {
                    list = await _db.Queryable<JiKou_JieGuoZhongJianBiao>()
                        .LeftJoin<ry_TiJianDengJi>((a, b) => a.TiJianBianHao == b.TiJianHao)
                        .LeftJoin<ry_ShenQingDanMingXi>((a, b, c) => a.TiJianBianHao == c.TiJianHao && a.TiaoMaHao == c.TiaoMaHao && a.ZuHeXiangMuBianHao == c.ZuHeXiangMuID)
                        .LeftJoin<jcsj_KeShi>((a, b, c, d) => c.KeShiID == d.KeShiID)
                        .OrderBy((a, b, c) => a.TiJianBianHao)
                        .OrderBy((a, b, c) => a.TiaoMaHao)
                        .OrderBy((a, b, c) => a.ZuHeXiangMuBianHao)
                        .Select((a, b, c, d) => new PersonRow
                        {
                            TiJianHao = a.TiJianBianHao, KeShiMingCheng = d.KeShiMingCheng,
                            TiaoMaHao = a.TiaoMaHao, XingMing = b.XingMing,
                            XingBie = b.XingBie, NianLing = b.NianLing,
                            ShenQingDanShiJian = b.TiaoMaZuiZaoDaYinShiJian,
                            ItemName = c.ZuHeXiangMuMingCheng
                        })
                        .ToListAsync().ConfigureAwait(true);
                }
                else
                {
                    list = await _db.Queryable<JiKou_JieGuoZhongJianBiao>()
                        .LeftJoin<ry_TiJianDengJi>((a, b) => a.TiJianBianHao == b.TiJianHao)
                        .LeftJoin<ry_ShenQingDanZhu>((a, b, c) => a.TiJianBianHao == c.TiJianHao && a.TiaoMaHao == c.TiaoMaHao)
                        .LeftJoin<jcsj_KeShi>((a, b, c, d) => c.KeShiID == d.KeShiID)
                        .OrderBy((a, b, c) => a.TiJianBianHao)
                        .OrderBy((a, b, c) => a.TiaoMaHao)
                        .Select((a, b, c, d) => new PersonRow
                        {
                            TiJianHao = a.TiJianBianHao, KeShiMingCheng = d.KeShiMingCheng,
                            TiaoMaHao = a.TiaoMaHao, XingMing = b.XingMing,
                            XingBie = b.XingBie, NianLing = b.NianLing,
                            ShenQingDanShiJian = c.ShenQingDanShiJian,
                            ItemName = c.ZuHeXiangMuMingChengJiHe
                        })
                        .ToListAsync().ConfigureAwait(true);
                }

                return new BindingList<ClassInfo.UserModel>(list.Select((t, i) => new ClassInfo.UserModel
                {
                    Check = false,
                    Id = i + 1,
                    Name = t.XingMing ?? string.Empty,
                    Age = t.NianLing.ToString() ?? string.Empty,
                    Gender = t.XingBie ?? string.Empty,
                    TiJianHao = t.TiJianHao ?? string.Empty,
                    TiaoMaHao = t.TiaoMaHao ?? string.Empty,
                    ShenQingDanShiJian = t.ShenQingDanShiJian.ToString(),
                    DeptName = t.KeShiMingCheng ?? string.Empty,
                    ItemName = t.ItemName ?? string.Empty,
                    Status = "",
                    CellLinks = new CellLink[]
                    {
                        new CellButton(Guid.NewGuid().ToString(), "日志", TTypeMini.Primary)
                        {
                            Back = Color.DarkCyan,
                            BackActive = Color.DarkCyan,
                            BackHover = Color.DarkCyan
                        }
                    }
                }).ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("关联数据查询失败：" + ex.Message, ex);
            }
        }

        private class PersonRow
        {
            public string TiJianHao { get; set; }
            public string KeShiMingCheng { get; set; }
            public string TiaoMaHao { get; set; }
            public string XingMing { get; set; }
            public string XingBie { get; set; }
            public int NianLing { get; set; }
            public DateTime? ShenQingDanShiJian { get; set; }
            public string ItemName { get; set; }
        }

        /// <summary>
        /// 拼接外接查询语句的结果
        /// </summary>
        private class QueryBuildResult
        {
            public string Sql { get; set; }
            public string ExtConnStr { get; set; }
            public string DeptType { get; set; }
            public bool ExtConnStrUpdated { get; set; }
        }

        /// <summary>
        /// 根据配置拼接外接查询语句
        /// </summary>
        private async Task<QueryBuildResult> BuildExternalQueryAsync(
            string strConfigName, string strReqNo, string strExamNo, DateTime? dtStart, DateTime? dtEnd)
        {
            try
            {
                _db = new DBTableAndView().InitSqlSugarClient();

                var result = new QueryBuildResult();
                string strWJ_SQL = "";
                string strColumns = "";

                var listSetting = await _db.Queryable<接口_配置信息表>()
                    .Where(t => t.配置名称 == strConfigName)
                    .OrderBy(t => t.字段顺序)
                    .ToListAsync();

                if (listSetting.Count <= 10)
                    return result;

                string extConnStr = null;
                string deptType = null;

                foreach (var item in listSetting)
                {
                    switch (item.参数标识)
                    {
                        case "外接数据源SQL语句":
                            strWJ_SQL = item.参数值;
                            break;
                        case "字段对应":
                            if (string.IsNullOrEmpty(item.外接字段名))
                                strColumns += $" '' as [{item.本地字段名}],";
                            else
                                strColumns += $" [{item.外接字段名}] as [{item.本地字段名}],";
                            break;
                        case "科室名称":
                            deptType = item.参数值;
                            break;
                        case "外接系统连接字符串":
                            extConnStr = item.参数值;
                            break;
                    }
                }

                if (string.IsNullOrEmpty(strWJ_SQL) || string.IsNullOrEmpty(strColumns))
                    return result;

                string strWjSqlSel = "SELECT * FROM (SELECT " + strColumns.TrimEnd(',') + " FROM (" + strWJ_SQL + ") a) a WHERE 1=1";

                if (!string.IsNullOrEmpty(strReqNo?.Trim()))
                {
                    strWjSqlSel = strWjSqlSel.Replace("#日期条件#", "--");
                    strWjSqlSel = strWjSqlSel.Replace("#体检编号#", "--");
                    strWjSqlSel = strWjSqlSel.Replace("#申请单号#", "");
                    strWjSqlSel = strWjSqlSel.Replace("@申请单号@", strReqNo.Trim().Replace("'", "''"));
                }
                else if (!string.IsNullOrEmpty(strExamNo?.Trim()))
                {
                    strWjSqlSel = strWjSqlSel.Replace("#体检编号#", "");
                    strWjSqlSel = strWjSqlSel.Replace("#日期条件#", "--");
                    strWjSqlSel = strWjSqlSel.Replace("#申请单号#", "--");
                    strWjSqlSel = strWjSqlSel.Replace("@体检编号@", strExamNo.Trim().Replace("'", "''"));
                }
                else
                {
                    strWjSqlSel = strWjSqlSel.Replace("#日期条件#", "");
                    strWjSqlSel = strWjSqlSel.Replace("#体检编号#", "--");
                    strWjSqlSel = strWjSqlSel.Replace("#申请单号#", "--");
                    strWjSqlSel = strWjSqlSel.Replace("@开始日期@", (dtStart ?? DateTime.Now).ToString("yyyy-MM-dd 00:00:00"));
                    strWjSqlSel = strWjSqlSel.Replace("@结束日期@", (dtEnd ?? DateTime.Now).ToString("yyyy-MM-dd 23:59:59"));
                }

                result.Sql = strWjSqlSel;
                result.ExtConnStr = extConnStr;
                result.DeptType = deptType;

                if (extConnStr != null && (extConnStr != _settings.ExtDbConnStr || deptType != _settings.DeptType))
                    result.ExtConnStrUpdated = true;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("查询语句构建失败：" + ex.Message, ex);
            }
        }
    }
}
