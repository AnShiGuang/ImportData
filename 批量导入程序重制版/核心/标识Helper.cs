using System;
using System.Collections.Generic;
using System.Linq;
using 批量导入程序重制版.TiJian4;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 系统标识辅助类：按本地院区读取可用的系统标识列表。
    /// </summary>
    public static class 标识Helper
    {
        /// <summary>
        /// 读取指定院区的系统标识列表（仅返回已启用的标识）。
        /// 院区为空时返回全部已启用标识。
        /// </summary>
        public static List<string> 获取院区标识列表(string 院区)
        {
            var result = new List<string>();
            try
            {
                var db = new DBTableAndView().InitSqlSugarClient();

                var enabled = db.Queryable<接口_配置信息表>()
                    .Where(t => t.配置启用 == "是" && !string.IsNullOrEmpty(t.配置名称))
                    .Select(t => t.配置名称)
                    .Distinct()
                    .ToList();

                if (enabled == null || enabled.Count == 0)
                    return result;

                if (string.IsNullOrWhiteSpace(院区))
                    return enabled;

                var campusConfigs = db.Queryable<接口_配置信息表>()
                    .Where(t => t.参数标识 == 院区Helper.院区参数标识 && t.参数值 == 院区)
                    .Select(t => t.配置名称)
                    .ToList();

                result = enabled.Intersect(campusConfigs).ToList();
            }
            catch
            {
                // 数据库不可用时返回空列表
            }
            return result;
        }
    }
}
