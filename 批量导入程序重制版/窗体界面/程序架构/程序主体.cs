using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    public sealed partial class 程序主体 : 页面基类
    {
        private bool _statsLoading;

        public 程序主体()
        {
            InitializeComponent();
            VisibleChanged += frmHome_VisibleChanged;
        }

        public 程序主体(string displayName) : this()
        {
            SetWelcomeUser(displayName, false);
        }

        public 程序主体(string displayName, bool isAdmin) : this()
        {
            SetWelcomeUser(displayName, isAdmin);
        }

        private void SetWelcomeUser(string displayName, bool isAdmin)
        {
            welcomeLabel.Text = displayName ?? string.Empty;
            welcomeLabel.ForeColor = isAdmin
                ? Color.FromArgb(230, 126, 34)
                : Color.FromArgb(35, 145, 112);
        }

        private async void frmHome_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            await LoadDailyStatsAsync();
        }

        private async Task LoadDailyStatsAsync()
        {
            if (_statsLoading) return;
            if (!AppConfig.HasDatabaseConnection)
            {
                SetDailyStats("—", "—", "—");
                return;
            }

            _statsLoading = true;
            var today = DateTime.Today;
            const string sql = @"
SELECT
    (SELECT COUNT(DISTINCT [TiJianHao]) FROM [dbo].[ry_TiJianDengJi]
     WHERE [DengJiShiJian] >= @today AND [DengJiShiJian] < @tomorrow) AS [RegisteredCount],
    (SELECT COUNT(DISTINCT [TiJianHao]) FROM [dbo].[zj_JieGuo]
     WHERE [ZongJianRiQi] >= @today AND [ZongJianRiQi] < @tomorrow) AS [ExaminedCount],
    (SELECT COUNT(DISTINCT e.[TiJianHao])
     FROM [dbo].[zj_JieGuo] e
     INNER JOIN [dbo].[ry_TiJianDengJi] r ON r.[TiJianHao] = e.[TiJianHao]
     WHERE e.[ZongJianRiQi] >= @today AND e.[ZongJianRiQi] < @tomorrow
       AND r.[PingTaiShangChuan] = N'是') AS [UploadSuccessCount]";

            try
            {
                using (var connection = new SqlConnection(AppConfig.ConnectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandTimeout = 15;
                    command.Parameters.Add("@today", SqlDbType.DateTime).Value = today;
                    command.Parameters.Add("@tomorrow", SqlDbType.DateTime).Value = today.AddDays(1);
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                            SetDailyStats(reader.GetInt32(0).ToString(), reader.GetInt32(1).ToString(), reader.GetInt32(2).ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                SetDailyStats("—", "—", "—");
                if (!IsDisposed && Visible)
                    MessageBox.Show(this, "读取主页当日统计失败：" + ex.Message, "主页统计", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _statsLoading = false;
            }
        }

        private void SetDailyStats(string registered, string examined, string uploaded)
        {
            registeredCountLabel.Text = registered + " 人";
            examinedCountLabel.Text = examined + " 人";
            uploadedCountLabel.Text = uploaded + " 人";
        }
    }
}
