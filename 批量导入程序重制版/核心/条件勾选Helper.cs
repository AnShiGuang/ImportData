using System;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 条件勾选：对应旧程序 条件勾选窗体。
    /// 保存“要勾选的项目（最多 6 个）”与“要勾选的单位名”，供导入界面筛选使用。
    /// </summary>
    public static class 条件勾选Helper
    {
        /// <summary>
        /// 保存勾选条件
        /// </summary>
        public static void 保存(string[] 项目, string 单位名称)
        {
            var settings = SettingHelper.LoadConfig();
            settings.ConditionProjects = 项目 == null ? string.Empty : string.Join("|", 项目);
            settings.ConditionUnit = 单位名称 ?? string.Empty;
            SettingHelper.SaveConfig(settings);
        }

        /// <summary>
        /// 读取勾选的项目（“|”分隔）
        /// </summary>
        public static string[] 读取项目()
        {
            var value = SettingHelper.LoadConfig().ConditionProjects;
            if (string.IsNullOrWhiteSpace(value))
                return new string[0];
            return value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 读取勾选的单位名
        /// </summary>
        public static string 读取单位()
        {
            return SettingHelper.LoadConfig().ConditionUnit ?? string.Empty;
        }
    }
}
