using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    internal sealed class frmDatabaseConfig : Form
    {
        private readonly AntdUI.Input connectionInput;
        private readonly AntdUI.Checkbox showConnectionCheckBox;
        private readonly AntdUI.Label statusLabel;
        private readonly AntdUI.Button testButton;
        private readonly AntdUI.Button saveButton;
        private bool operationInProgress;

        internal frmDatabaseConfig()
        {
            Text = "数据库连接配置";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(620, 244);
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            BackColor = Color.White;
            Font = new Font("Microsoft YaHei UI", 9F);

            var title = new AntdUI.Label
            {
                Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 55, 65),
                Location = new Point(24, 18),
                Size = new Size(560, 28),
                Text = "数据库连接"
            };
            var description = new AntdUI.Label
            {
                ForeColor = Color.FromArgb(108, 123, 134),
                Location = new Point(26, 50),
                Size = new Size(566, 24),
                Text = "登录需要连接 tjdatabase4。修改后会先测试连接，再加密保存。"
            };
            var connectionLabel = new AntdUI.Label
            {
                Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(26, 84),
                Size = new Size(400, 20),
                Text = "SQL Server 连接字符串"
            };
            connectionInput = new AntdUI.Input
            {
                BackColor = Color.White,
                BorderActive = Color.FromArgb(35, 145, 112),
                BorderColor = Color.FromArgb(215, 225, 230),
                BorderHover = Color.FromArgb(112, 184, 177),
                ForeColor = Color.FromArgb(37, 55, 65),
                Location = new Point(26, 108),
                Name = "connectionInput",
                PlaceholderText = "请输入 tjdatabase4 数据库连接字符串",
                Radius = 7,
                Size = new Size(566, 38),
                UseSystemPasswordChar = true,
                Text = AppConfig.ConnectionString
            };
            showConnectionCheckBox = new AntdUI.Checkbox
            {
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(26, 154),
                Name = "showConnectionCheckBox",
                Size = new Size(150, 24),
                Text = "显示连接字符串"
            };
            showConnectionCheckBox.CheckedChanged += showConnectionCheckBox_CheckedChanged;
            statusLabel = new AntdUI.Label
            {
                ForeColor = Color.FromArgb(108, 123, 134),
                Location = new Point(180, 154),
                Name = "statusLabel",
                Size = new Size(410, 24),
                Text = "已加载当前配置。"
            };
            testButton = new AntdUI.Button
            {
                Location = new Point(316, 190),
                Name = "testButton",
                Radius = 7,
                Size = new Size(128, 38),
                Text = "测试连接"
            };
            testButton.Click += testButton_Click;
            saveButton = new AntdUI.Button
            {
                BackColor = Color.FromArgb(35, 145, 112),
                ForeColor = Color.White,
                Location = new Point(464, 190),
                Name = "saveButton",
                Radius = 7,
                Size = new Size(128, 38),
                Text = "测试并保存",
                Type = AntdUI.TTypeMini.Primary
            };
            saveButton.Click += saveButton_Click;

            Controls.Add(title);
            Controls.Add(description);
            Controls.Add(connectionLabel);
            Controls.Add(connectionInput);
            Controls.Add(showConnectionCheckBox);
            Controls.Add(statusLabel);
            Controls.Add(testButton);
            Controls.Add(saveButton);
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
