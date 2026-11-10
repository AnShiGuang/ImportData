using System;
using System.Collections.Generic;
using System.Linq;
using 批量导入程序重制版.TiJian4;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 院区辅助类：提供可选院区列表，并统一院区在配置中的参数标识。
    /// </summary>
    public static class 院区Helper
    {
        /// <summary>
        /// 接口_配置信息表中保存标识所属院区的参数标识
        /// </summary>
        public const string 院区参数标识 = "所属院区";

        /// <summary>
        /// 兜底院区列表（数据库不可用或没有院区表时使用）
        /// </summary>
        private static readonly string[] 默认院区 = { "主院区", "南院区", "西院区", "东院区" };

        /// <summary>
        /// 读取可选院区列表：优先从 jcsj_YuanQu 表读取，失败时返回兜底列表。
        /// </summary>
        public static List<string> 获取院区列表()
        {
            try
            {
                var db = new DBTableAndView().InitSqlSugarClient();
                var list = db.Queryable<jcsj_YuanQu>()
                    .Where(t => !string.IsNullOrEmpty(t.YuanQuMingCheng))
                    .OrderBy(t => t.YuanQuID)
                    .Select(t => t.YuanQuMingCheng)
                    .ToList();
                if (list != null && list.Count > 0)
                    return list.Distinct().ToList();
            }
            catch
            {
                // 数据库不可用或没有院区表时使用兜底列表
            }
            return new List<string>(默认院区);
        }

        /// <summary>
        /// 校验院区是否为空，为空时返回兜底列表第一项。
        /// </summary>
        public static string 规范化院区(string 院区)
        {
            if (string.IsNullOrWhiteSpace(院区))
                return 默认院区[0];
            return 院区.Trim();
        }
    }
}
