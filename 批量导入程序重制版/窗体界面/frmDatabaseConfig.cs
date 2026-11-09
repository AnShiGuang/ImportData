using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    internal sealed partial class frmDatabaseConfig : Form
    {
        private bool operationInProgress;

        internal frmDatabaseConfig()
        {
            InitializeComponent();
            // 设计时不读取本机配置，避免设计器加载配置文件
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                connectionInput.Text = AppConfig.ConnectionString;
        }

        private void showConnectionCheckBox_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            connectionInput.UseSystemPasswordChar = !showConnectionCheckBox.Checked;
        }

        private async void testButton_Click(object sender, EventArgs e)
        {
            await TestAndSaveAsync(false);
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            await TestAndSaveAsync(true);
        }

        private async System.Threading.Tasks.Task TestAndSaveAsync(bool save)
        {
            if (operationInProgress) return;
            var connectionString = connectionInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show(this, "请输入数据库连接字符串。", "数据库连接配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            operationInProgress = true;
            testButton.Enabled = false;
            saveButton.Enabled = false;
            statusLabel.ForeColor = Color.FromArgb(108, 123, 134);
            statusLabel.Text = "正在测试数据库连接…";
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                if (!string.Equals(builder.InitialCatalog, "tjdatabase4", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("数据库必须设置为 tjdatabase4。");
                builder.ConnectTimeout = 8;
                builder.PersistSecurityInfo = false;
                using (var connection = new SqlConnection(builder.ConnectionString))
                    await connection.OpenAsync();

                if (save)
                {
                    AppConfig.SetDatabaseConnectionString(builder.ConnectionString);
                    statusLabel.Text = "连接成功，配置已加密保存。";
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    statusLabel.ForeColor = Color.FromArgb(35, 145, 112);
                    statusLabel.Text = "连接成功，可保存此配置。";
                }
            }
            catch (Exception ex)
            {
                statusLabel.ForeColor = Color.Firebrick;
                statusLabel.Text = "连接失败，请检查地址、账号或密码。";
                MessageBox.Show(this, "数据库连接失败：\r\n" + ex.Message, "数据库连接配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                operationInProgress = false;
                if (!IsDisposed)
                {
                    testButton.Enabled = true;
                    saveButton.Enabled = true;
                }
            }
        }
    }
}
