using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    internal sealed partial class 数据配置窗体 : Form
    {
        private bool builderMode;
        private bool operationInProgress;
        private bool suppressPreviewUpdate;

        internal 数据配置窗体()
        {
            InitializeComponent();
            // 设计时只按设计器序列化的布局展示，不读取本机配置、不调整窗体尺寸
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            connectionInput.Text = AppConfig.ConnectionString;
            ApplyMode();
        }

        private void showConnectionCheckBox_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            connectionInput.UseSystemPasswordChar = !showConnectionCheckBox.Checked;
        }

        private void BuilderField_TextChanged(object sender, EventArgs e)
        {
            if (suppressPreviewUpdate || previewInput == null) return;
            previewInput.Text = BuildConnectionString();
        }

        private void modeToggleButton_Click(object sender, EventArgs e)
        {
            if (builderMode)
            {
                // 手动填写 -> 连接字符串
                connectionInput.Text = previewInput.Text;
            }
            else
            {
                // 连接字符串 -> 手动填写
                ParseConnectionString(connectionInput.Text);
                previewInput.Text = BuildConnectionString();
            }
            builderMode = !builderMode;
            ApplyMode();
        }

        private void ApplyMode()
        {
            // 两种模式共用同一块区域，运行时只显示当前模式
            stringPanel.Location = new Point(24, 56);
            builderPanel.Location = new Point(24, 56);
            stringPanel.Visible = !builderMode;
            builderPanel.Visible = builderMode;

            statusLabel.Location = new Point(26, 322);
            testButton.Location = new Point(316, 352);
            saveButton.Location = new Point(464, 352);
            ClientSize = new Size(620, 400);

            modeToggleButton.Text = builderMode ? "连接字符串" : "手动填写";
        }

        private string BuildConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                ConnectTimeout = 8,
                PersistSecurityInfo = false
            };

            var server = serverInput.Text.Trim();
            var port = portInput.Text.Trim();
            if (!string.IsNullOrEmpty(server))
                builder.DataSource = string.IsNullOrEmpty(port) ? server : server + "," + port;

            var database = databaseInput.Text.Trim();
            if (!string.IsNullOrEmpty(database))
                builder.InitialCatalog = database;

            var user = userInput.Text.Trim();
            if (!string.IsNullOrEmpty(user))
                builder.UserID = user;

            var password = passwordInput.Text;
            if (!string.IsNullOrEmpty(password))
                builder.Password = password;

            return builder.ConnectionString;
        }

        private void ParseConnectionString(string connectionString)
        {
            suppressPreviewUpdate = true;
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString ?? string.Empty);
                var dataSource = builder.DataSource ?? string.Empty;
                var comma = dataSource.LastIndexOf(',');
                if (comma >= 0)
                {
                    serverInput.Text = dataSource.Substring(0, comma).Trim();
                    portInput.Text = dataSource.Substring(comma + 1).Trim();
                }
                else
                {
                    serverInput.Text = dataSource.Trim();
                    portInput.Text = string.Empty;
                }
                databaseInput.Text = builder.InitialCatalog ?? string.Empty;
                userInput.Text = builder.UserID ?? string.Empty;
                passwordInput.Text = builder.Password ?? string.Empty;
            }
            catch
            {
                serverInput.Text = string.Empty;
                portInput.Text = string.Empty;
                databaseInput.Text = string.Empty;
                userInput.Text = string.Empty;
                passwordInput.Text = string.Empty;
            }
            finally
            {
                suppressPreviewUpdate = false;
            }
        }

        private string EffectiveConnectionString
        {
            get { return builderMode ? previewInput.Text : connectionInput.Text; }
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
            var connectionString = EffectiveConnectionString.Trim();
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
