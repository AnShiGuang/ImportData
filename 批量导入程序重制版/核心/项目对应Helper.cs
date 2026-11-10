using System;
using System.Data;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 项目对应：对应旧程序 项目对应窗体 / 错误项目窗体 的后台逻辑。
    /// 把检验明细项目对应到体检组合项目（jcsj_MingXiXiangMu）。
    /// </summary>
    public static class 项目对应Helper
    {
        private static string 转义(string value)
        {
            return (value ?? string.Empty).Replace("'", "''");
        }

        /// <summary>
        /// 按条码号读取检验组合项目列表
        /// </summary>
        public static DataTable 获取组合项目(string connectionString, string 条码)
        {
            var db = new Database(connectionString);
            return db.Query($"select zuhexiangmuid as 组合项目序号, zuhexiangmumingcheng as 组合项目名称 from ry_ShenQingDanMingXi where tiaomahao ='{转义(条码)}'");
        }

        /// <summary>
        /// 按条码号读取组合项目名称
        /// </summary>
        public static string 获取条码名称(string connectionString, string 条码)
        {
            var db = new Database(connectionString);
            var dt = db.Query($"select zuhexiangmumingchengjihe from ry_ShenQingDanzhu where tiaomahao ='{转义(条码)}'");
            if (dt.Rows.Count == 1)
                return dt.Rows[0][0].ToString();
            return "此条码号已被体检科删除！";
        }

        /// <summary>
        /// 该组合项目下是否已存在此明细项目的对应
        /// </summary>
        public static bool 是否存在对应(string connectionString, string 组合项目代码, string 明细编码)
        {
            var db = new Database(connectionString);
            var dt = db.Query($"select * from jcsj_MingXiXiangMu where zuhexiangmuid ='{转义(组合项目代码)}' and lisxiangmubianma ='{转义(明细编码)}'");
            return dt != null && dt.Rows.Count > 0;
        }

        /// <summary>
        /// 新增一条明细项目与组合项目的对应
        /// </summary>
        public static void 新增明细对应(
            string connectionString,
            string 明细名称,
            string 组合项目代码,
            string 单位,
            string 参考范围,
            string 参考下限,
            string 参考上限,
            string 明细编码)
        {
            var db = new Database(connectionString);
            var max = db.Query("select isnull(max(ShunXu),0)+1 as 顺序 from jcsj_MingXiXiangMu");
            int 顺序 = 1;
            if (max != null && max.Rows.Count > 0)
                int.TryParse(max.Rows[0][0].ToString(), out 顺序);

            db.Insert(
                "jcsj_MingXiXiangMu",
                new[] { "MingXiXiangMuMingCheng", "ZuHeXiangMuID", "ShiYongXingBie", "BaoGaoSuoShuFenLei", "JieGuoLeiXing", "DanWei", "CanKaoFanWei", "CanKaoXiaXian", "CanKaoShangXian", "LisXiangMuBianMa", "BeiZhu2", "ShiFouQiYong", "ShunXu" },
                new object[]
                {
                    明细名称 ?? string.Empty,
                    组合项目代码 ?? string.Empty,
                    "不限",
                    "公用",
                    "文本",
                    单位 ?? string.Empty,
                    参考范围 ?? string.Empty,
                    参考下限 ?? string.Empty,
                    参考上限 ?? string.Empty,
                    明细编码 ?? string.Empty,
                    "批量导入数据新增：组合项目[" + (组合项目代码 ?? string.Empty) + "]",
                    "是",
                    顺序
                });
        }

        /// <summary>
        /// 读取该体检编号下、与该明细项目对应的组合项目（用于冲突检查）
        /// </summary>
        public static DataTable 获取对应组合项目(string connectionString, string 体检编号, string 明细编码)
        {
            var db = new Database(connectionString);
            return db.Query($"select mxxm.*, (select top 1 zuhexiangmumingcheng from jcsj_ZuHeXiangMu where zuhexiangmuid = mxxm.zuhexiangmuid) as zuhexiangmumingcheng from jcsj_MingXiXiangMu mxxm where mxxm.LisXiangMuBianMa ='{转义(明细编码)}' and mxxm.zuhexiangmuid in (select zuhexiangmuid from ry_shenqingdanmingxi where tijianhao ='{转义(体检编号)}')");
        }
    }
}
