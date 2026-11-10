using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace 批量导入程序重制版
{
    public class Runlog
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }

    public class AppSettings
    {
        public string MainDbConnStr { get; set; } = string.Empty;
        public string ExtDbConnStr { get; set; } = string.Empty;
        public string CurrentConfig { get; set; } = string.Empty;
        public string RememberPwd { get; set; } = "false";
        public string AutoLogin { get; set; } = "false";
        public string IsDefault { get; set; } = "false";
        public string DeptType { get; set; } = "false";
        public bool TrueOrFalse { get; set; } = false;
        public string CurrentUser { get; set; } = string.Empty;
        public string SavedUserName { get; set; } = string.Empty;
        public string SavedPwd { get; set; } = string.Empty;
        public string ExamVerText { get; set; } = string.Empty;
        public string WinVer { get; set; } = string.Empty;
        public DateTime LastRunTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 本程序的本地院区（在“配置参数”界面设置）
        /// </summary>
        public string LocalCampus { get; set; } = string.Empty;
        /// <summary>
        /// 条件勾选：要勾选的项目（“|”分隔）
        /// </summary>
        public string ConditionProjects { get; set; } = string.Empty;
        /// <summary>
        /// 条件勾选：要勾选的单位名
        /// </summary>
        public string ConditionUnit { get; set; } = string.Empty;
    }

    public static class SettingHelper
    {
        private static readonly string _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app_config.json");
        private static AppSettings _cachedConfig;

        [DllImport("shell32.dll", EntryPoint = "#261",
            CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void GetUserTilePath(
            string username, UInt32 whatever, StringBuilder picpath, int maxLength);

        public static string GetUserTilePath(string username)
        {
            var sb = new StringBuilder(1000);
            GetUserTilePath(username, 0x80000000, sb, sb.Capacity);
            return sb.ToString();
        }

        public static AppSettings LoadConfig()
        {
            if (_cachedConfig != null) return _cachedConfig;
            try
            {
                if (!File.Exists(_configPath))
                {
                    var defaultConfig = new AppSettings { MainDbConnStr = AppConfig.ConnectionString };
                    SaveConfig(defaultConfig);
                    return defaultConfig;
                }

                string jsonContent = File.ReadAllText(_configPath, Encoding.UTF8);
                var config = JsonConvert.DeserializeObject<AppSettings>(jsonContent) ?? new AppSettings();

                // 解密连接字符串
                config.MainDbConnStr = DecryptConnStr(config.MainDbConnStr);
                config.ExtDbConnStr = DecryptConnStr(config.ExtDbConnStr);
                if (string.IsNullOrWhiteSpace(config.MainDbConnStr))
                    config.MainDbConnStr = AppConfig.ConnectionString;

                _cachedConfig = config;
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载配置失败：{ex.Message}");
                return new AppSettings { MainDbConnStr = AppConfig.ConnectionString };
            }
        }

        public static void SaveConfig(AppSettings config)
        {
            if (config == null)
            {
                Console.WriteLine("配置实体不能为空");
                return;
            }

            try
            {
                // 创建副本，加密连接字符串后再写入文件
                var saveCopy = new AppSettings
                {
                    MainDbConnStr = EncryptConnStr(config.MainDbConnStr),
                    ExtDbConnStr = EncryptConnStr(config.ExtDbConnStr),
                    CurrentConfig = config.CurrentConfig,
                    RememberPwd = config.RememberPwd,
                    AutoLogin = config.AutoLogin,
                    IsDefault = config.IsDefault,
                    DeptType = config.DeptType,
                    TrueOrFalse = config.TrueOrFalse,
                    CurrentUser = config.CurrentUser,
                    SavedUserName = config.SavedUserName,
                    SavedPwd = config.SavedPwd,
                    ExamVerText = config.ExamVerText,
                    WinVer = config.WinVer,
                    LastRunTime = config.LastRunTime,
                    LocalCampus = config.LocalCampus,
                    ConditionProjects = config.ConditionProjects,
                    ConditionUnit = config.ConditionUnit
                };

                var jsonSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                };
                string jsonContent = JsonConvert.SerializeObject(saveCopy, jsonSettings);
                File.WriteAllText(_configPath, jsonContent, Encoding.UTF8);
                _cachedConfig = config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"保存配置失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 加密连接字符串（空值或已有 AES 前缀的不处理）
        /// </summary>
        private static string EncryptConnStr(string connStr)
        {
            if (string.IsNullOrWhiteSpace(connStr)) return string.Empty;
            if (connStr.StartsWith("AES:")) return connStr; // 已加密
            return CryptoHelper.EncryptForStore(connStr);
        }

        /// <summary>
        /// 解密连接字符串（自动识别 AES 新格式或旧 DES 格式）
        /// </summary>
        private static string DecryptConnStr(string connStr)
        {
            if (string.IsNullOrWhiteSpace(connStr)) return string.Empty;
            // 如果不是加密格式，直接返回明文
            if (!connStr.StartsWith("AES:")) return connStr;
            try
            {
                return CryptoHelper.DecryptStored(connStr);
            }
            catch
            {
                // 解密失败，返回原始值
                return connStr;
            }
        }

        public static string GetLocalIPv4Address()
        {
            try
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus != OperationalStatus.Up ||
                        ni.NetworkInterfaceType == NetworkInterfaceType.Loopback ||
                        ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                        continue;

                    IPInterfaceProperties ipProps = ni.GetIPProperties();
                    foreach (UnicastIPAddressInformation ipInfo in ipProps.UnicastAddresses)
                    {
                        if (ipInfo.Address.AddressFamily == AddressFamily.InterNetwork &&
                            !IPAddress.IsLoopback(ipInfo.Address) &&
                            IsPrivateIP(ipInfo.Address))
                            return ipInfo.Address.ToString();
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取IP地址失败：{ex.Message}");
                return string.Empty;
            }
        }

        public static bool IsPrivateIP(IPAddress ipAddress)
        {
            byte[] ipBytes = ipAddress.GetAddressBytes();
            switch (ipBytes[0])
            {
                case 10: return true;
                case 172: return ipBytes[1] >= 16 && ipBytes[1] <= 31;
                case 192: return ipBytes[1] == 168;
                default: return false;
            }
        }

        public static bool CheckInternetByTcp(string host, int port, int timeoutMs)
        {
            try
            {
                using (var tcpClient = new TcpClient())
                {
                    var connectTask = tcpClient.ConnectAsync(host, port);
                    var timeoutTask = Task.Delay(timeoutMs);
                    var completedTask = Task.WhenAny(connectTask, timeoutTask).Result;
                    return completedTask == connectTask && tcpClient.Connected;
                }
            }
            catch { return false; }
        }
    }
}
