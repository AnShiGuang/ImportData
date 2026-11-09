using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CancellationToken = System.Threading.CancellationToken;
using CancellationTokenSource = System.Threading.CancellationTokenSource;
using System.Windows.Forms;
using System.Drawing;

namespace 批量导入程序重制版
{
    public sealed partial class frmLogin : AntdUI.Window
    {
        private readonly Timer userLookupTimer;
        private readonly Timer lockCountdownTimer;
        private readonly Timer autoLoginTimer;
        private readonly Dictionary<string, string> userDisplayNameCache = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private CancellationTokenSource userLookupCancellation;
        private bool loginInProgress;
        private bool autoLoginControlsLocked;
        private int autoLoginAnimationStep;
        private DateTime lockUntilUtc;

        internal static Image LoadLoginImage(string imageName)
        {
            var resourceName = "批量导入程序重制版.Resources." + imageName;
            using (var stream = typeof(frmLogin).Assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;
                using (var source = Image.FromStream(stream)) return new Bitmap(source);
            }
        }

        private void LoginFormDrag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) DraggableMouseDown();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void userInput_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) e.IsInputKey = true;
        }

        private void userInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Tab || e.Shift) return;

            e.Handled = true;
            e.SuppressKeyPress = true;
            passwordInput.Focus();
        }

        protected override bool ProcessTabKey(bool forward)
        {
            if (forward && userInput != null && userInput.ContainsFocus)
            {
                passwordInput.Focus();
                return true;
            }

            if (!forward && passwordInput != null && passwordInput.ContainsFocus)
            {
                userInput.Focus();
                return true;
            }

            return base.ProcessTabKey(forward);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Tab && userInput != null && userInput.ContainsFocus)
            {
                passwordInput.Focus();
                return true;
            }

            if (keyData == (Keys.Tab | Keys.Shift) && passwordInput != null && passwordInput.ContainsFocus)
            {
                userInput.Focus();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        public frmLogin()
        {
            InitializeComponent();
            pictureBox3.BackgroundImage = LoadLoginImage("ssms.png");
            pictureBox2.BackgroundImage = LoadLoginImage("oracle.png");
            pictureBox1.BackgroundImage = LoadLoginImage("MYSQL.png");
            pictureBox4.BackgroundImage = LoadLoginImage("背景_小.png");
            userLookupTimer = new Timer(components);
            lockCountdownTimer = new Timer(components);
            autoLoginTimer = new Timer(components);
            userLookupTimer.Interval = 350;
            userLookupTimer.Tick += userLookupTimer_Tick;
            lockCountdownTimer.Interval = 1000;
            lockCountdownTimer.Tick += lockCountdownTimer_Tick;
            autoLoginTimer.Interval = 320;
            autoLoginTimer.Tick += autoLoginTimer_Tick;
            versionLabel.Text = "版本 " + AppConfig.Get("AppVersion", "1.0.0");
            Shown += frmLogin_Shown;
            FormClosed += (sender, args) => CancelUserLookup();
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            LoadRememberedLogin();
        }

        private void lockCountdownTimer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.UtcNow;
            if (lockUntilUtc <= now) lockUntilUtc = DateTime.MinValue;

            if (lockUntilUtc > now)
            {
                loginButton.Enabled = false;
                var remaining = lockUntilUtc - now;
                messageLabel.ForeColor = Color.Firebrick;
                messageLabel.Text = string.Format("登录尝试已锁定，请等待 {0} 秒后重试。", (int)Math.Ceiling(remaining.TotalSeconds));
            }
            else
            {
                loginButton.Enabled = true;
                messageLabel.Text = "请输入账号和密码。";
            }

            if (lockUntilUtc == DateTime.MinValue)
                lockCountdownTimer.Stop();
        }

        private async void loginButton_Click(object sender, EventArgs e) { await LoginAsync(); }

        private async void autoLoginTimer_Tick(object sender, EventArgs e)
        {
            autoLoginAnimationStep++;
            messageLabel.Text = "正在自动登录" + new string('.', autoLoginAnimationStep % 4);
            if (autoLoginAnimationStep < 4) return;

            autoLoginTimer.Stop();
            await LoginAsync();
        }

        private void autoLoginCheckBox_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            if (autoLoginCheckBox.Checked) rememberPasswordCheckBox.Checked = true;
        }

        private void rememberPasswordCheckBox_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            if (!rememberPasswordCheckBox.Checked) autoLoginCheckBox.Checked = false;
        }
        private void userInput_TextChanged(object sender, EventArgs e)
        {
            if (lockUntilUtc > DateTime.UtcNow)
            {
                loginButton.Enabled = false;
                lockCountdownTimer.Start();
            }
            else if (!loginInProgress)
            {
                loginButton.Enabled = true;
            }
            userLookupTimer.Stop();
            CancelUserLookup();
            if (string.IsNullOrWhiteSpace(userInput.Text))
            {
                userInput.SuffixText = string.Empty;
                return;
            }
            userLookupTimer.Start();
            rememberPasswordCheckBox.Enabled = true;
            autoLoginCheckBox.Enabled = true;
        }

        private async void userLookupTimer_Tick(object sender, EventArgs e)
        {
            userLookupTimer.Stop();
            var lookupValue = userInput.Text.Trim();
            if (lookupValue.Length == 0) return;
            var fixedDisplayName = GetFixedAccountDisplayName(lookupValue);
            if (fixedDisplayName != null)
            {
                if (userDisplayNameCache.Count >= 64) userDisplayNameCache.Clear();
                userDisplayNameCache[lookupValue] = fixedDisplayName;
                if (!IsDisposed && string.Equals(userInput.Text.Trim(), lookupValue, StringComparison.Ordinal))
                    userInput.SuffixText = FormatDisplayNameForSuffix(fixedDisplayName);
                return;
            }

            string cachedDisplayName;
            if (userDisplayNameCache.TryGetValue(lookupValue, out cachedDisplayName))
            {
                if (!IsDisposed && string.Equals(userInput.Text.Trim(), lookupValue, StringComparison.Ordinal))
                    userInput.SuffixText = FormatDisplayNameForSuffix(cachedDisplayName);
                return;
            }

            var cancellation = new CancellationTokenSource();
            userLookupCancellation = cancellation;
            try
            {
                var displayName = await FindDatabaseDisplayNameAsync(lookupValue, cancellation.Token);
                if (!cancellation.IsCancellationRequested)
                {
                    if (userDisplayNameCache.Count >= 64) userDisplayNameCache.Clear();
                    userDisplayNameCache[lookupValue] = displayName;
                }
                if (!cancellation.IsCancellationRequested && !IsDisposed && string.Equals(userInput.Text.Trim(), lookupValue, StringComparison.Ordinal))
                    userInput.SuffixText = FormatDisplayNameForSuffix(displayName);
            }
            catch (Exception ex)
            {
                if (!cancellation.IsCancellationRequested && !IsDisposed && string.Equals(userInput.Text.Trim(), lookupValue, StringComparison.Ordinal))
                {
                    userInput.SuffixText = string.Empty;
                    MessageBox.Show(this, "读取用户姓名失败：\r\n" + ex.Message, "数据库查询错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                if (ReferenceEquals(userLookupCancellation, cancellation)) userLookupCancellation = null;
                cancellation.Dispose();
            }
        }

        private void CancelUserLookup()
        {
            var cancellation = userLookupCancellation;
            if (cancellation == null) return;
            userLookupCancellation = null;
            Task.Run(() =>
            {
                try { cancellation.Cancel(); }
                catch (ObjectDisposedException) { }
            });
        }

        private string FormatDisplayNameForSuffix(string displayName)
        {
            if (string.IsNullOrEmpty(displayName)) return string.Empty;

            // Keep enough room for the account being typed while allowing longer names to remain identifiable.
            const int maxSuffixWidth = 150;
            if (TextRenderer.MeasureText(displayName, userInput.Font).Width <= maxSuffixWidth)
                return displayName;

            const string ellipsis = "...";
            for (var length = displayName.Length - 1; length > 0; length--)
            {
                var shortened = displayName.Substring(0, length) + ellipsis;
                if (TextRenderer.MeasureText(shortened, userInput.Font).Width <= maxSuffixWidth)
                    return shortened;
            }

            return ellipsis;
        }

        private async void passwordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await LoginAsync(); }
        }

        private async Task LoginAsync()
        {
            var clientIp = 登录限制.GetClientIp();
            var user = userInput.Text.Trim();
            var password = passwordInput.Text;
            if (user.Length == 0) { messageLabel.Text = "请输入用户名。"; return; }

            if (string.IsNullOrWhiteSpace(AppConfig.ConnectionString))
            {
                messageLabel.ForeColor = System.Drawing.Color.Firebrick;
                messageLabel.Text = "尚未配置数据库连接。";
                using (var dialog = new frmDatabaseConfig())
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        messageLabel.Text = "数据库连接已保存，正在重新验证登录…";
                        await LoginAsync();
                    }
                }
                return;
            }
            if (loginInProgress) return;
            loginInProgress = true;
            userLookupTimer.Stop();
            CancelUserLookup();
            userInput.Enabled = false;
            passwordInput.Enabled = false;
            loginButton.Enabled = false; messageLabel.ForeColor = System.Drawing.Color.DimGray; messageLabel.Text = "正在验证…";
            try
            {
                var lockStatus = await 登录限制.GetRemainingLockAsync(user);
                if (lockStatus.剩余时间 > TimeSpan.Zero)
                {
                    autoLoginControlsLocked = false;
                    lockUntilUtc = DateTime.UtcNow.Add(lockStatus.剩余时间);
                    lockCountdownTimer.Start();
                    return;
                }

                string displayName = null;
                var userExists = true;
                bool authenticated;
                try
                {
                    authenticated = await Task.Run(() => Authenticate(user, password, out displayName, out userExists));
                }
                catch (SqlException ex)
                {
                    PromptDatabaseConfiguration(ex);
                    return;
                }
                catch (ArgumentException ex)
                {
                    PromptDatabaseConfiguration(ex);
                    return;
                }
                catch (InvalidOperationException ex)
                {
                    PromptDatabaseConfiguration(ex);
                    return;
                }
                if (!authenticated)
                {
                    autoLoginControlsLocked = false;
                    messageLabel.ForeColor = System.Drawing.Color.Firebrick;
                    messageLabel.Text = "账号或密码错误。";
                    var failureLockStatus = await 登录限制.RegisterFailureAsync(user);
                    if (failureLockStatus.剩余时间 > TimeSpan.Zero)
                    {
                        lockUntilUtc = DateTime.UtcNow.Add(failureLockStatus.剩余时间);
                                    messageLabel.Text = string.Format("登录尝试已锁定，请等待 {0} 秒后重试。", (int)Math.Ceiling(failureLockStatus.剩余时间.TotalSeconds));
                        lockCountdownTimer.Start();
                    }
                    QueueLoginLog("警告", "登录失败", "账号或密码验证失败",
                        "登录账号：" + user + "；账号存在：" + (userExists ? "是" : "否"), true);
                    return;
                }
                await 登录限制.ResetAsync(user);
                登录凭据.Save(user, password, rememberPasswordCheckBox.Checked, autoLoginCheckBox.Checked);
                await WriteLoginLogAsync("常规", "登录", "登录成功",
                    "登录账号：" + user
                    + (string.IsNullOrWhiteSpace(displayName) ? string.Empty : "；显示名称：" + displayName)
                    + "；记住密码：" + (rememberPasswordCheckBox.Checked ? "是" : "否")
                    + "；自动登录：" + (autoLoginCheckBox.Checked ? "是" : "否"), true,
                    操作日志.FormatUserName(user, displayName));
                userInput.SuffixText = string.Equals(user, AppConfig.Get("SuperUser", "muyang"), StringComparison.OrdinalIgnoreCase) ? "管理员" : string.Empty;
                Hide();
                using (var form = new frmMainPage(user, displayName))
                {
                    form.ShowDialog(this);
                    if (form.LoggedOut)
                    {
                        userInput.Clear();
                        passwordInput.Clear();
                        rememberPasswordCheckBox.Checked = false;
                        autoLoginCheckBox.Checked = false;
                        userInput.SuffixText = string.Empty;
                        messageLabel.Text = "已注销，请重新登录。";
                        Show();
                        return;
                    }
                }
                Close();
            }
            catch (Exception ex)
            {
                QueueLoginLog("严重", "登录异常", "登录验证过程中发生错误", ex.Message, false);
                messageLabel.ForeColor = System.Drawing.Color.Firebrick;
                messageLabel.Text = "登录失败，请检查配置和网络。";
                MessageBox.Show(this, ex.Message, "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loginInProgress = false;
                if (!IsDisposed && !autoLoginControlsLocked)
                {
                    userInput.Enabled = true;
                    passwordInput.Enabled = true;
                    rememberPasswordCheckBox.Enabled = true;
                    autoLoginCheckBox.Enabled = true;
                    loginButton.Enabled = lockUntilUtc <= DateTime.UtcNow;
                }
            }
        }

        private void PromptDatabaseConfiguration(Exception error)
        {
            messageLabel.ForeColor = System.Drawing.Color.Firebrick;
            messageLabel.Text = "数据库连接失败。";
            var answer = MessageBox.Show(this,
                "无法连接数据库：\r\n" + error.Message + "\r\n\r\n是否现在打开数据库配置？",
                "数据库连接失败", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (answer != DialogResult.Yes) return;

            using (var dialog = new frmDatabaseConfig())
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    messageLabel.Text = "数据库配置已更新，请重新登录。";
            }
        }

        private async Task WriteLoginLogAsync(string level, string action, string message, string details, bool showLogError, string logUserName = null)
        {
            try
            {
                var auditUserName = string.IsNullOrWhiteSpace(logUserName) ? userInput.Text.Trim() : logUserName;
                await 操作日志.WriteAsync(操作日志.Program, level, auditUserName, 登录限制.GetClientIp(), action, message, details);
            }
            catch (Exception ex)
            {
                if (showLogError && !IsDisposed)
                    MessageBox.Show(this, "登录操作未能写入数据库日志：" + ex.Message, "日志记录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void QueueLoginLog(string level, string action, string message, string details, bool showLogError)
        {
            await WriteLoginLogAsync(level, action, message, details, showLogError);
        }

        private void LoadRememberedLogin()
        {
            var saved = 登录凭据.Load();
            if (!saved.RememberPassword || string.IsNullOrWhiteSpace(saved.UserName)) return;
            userInput.Text = saved.UserName;
            passwordInput.Text = saved.Password;
            if (!rememberPasswordCheckBox.Enabled) return;
            rememberPasswordCheckBox.Checked = true;
            autoLoginCheckBox.Checked = saved.AutoLogin;
            if (saved.AutoLogin) BeginAutoLogin();
        }

        private void BeginAutoLogin()
        {
            userLookupTimer.Stop();
            autoLoginAnimationStep = 0;
            messageLabel.ForeColor = Color.FromArgb(35, 145, 112);
            messageLabel.Text = "正在自动登录";
            userInput.Enabled = false;
            passwordInput.Enabled = false;
            rememberPasswordCheckBox.Enabled = false;
            autoLoginCheckBox.Enabled = false;
            loginButton.Enabled = false;
            autoLoginControlsLocked = true;
            autoLoginTimer.Start();
        }

        private const string PasswordEntropy = "mysoft@2";

        private static bool Authenticate(string user, string password, out string displayName, out bool userExists)
        {
            userExists = true;
            var superUser = AppConfig.Get("SuperUser", "muyang");
            var superPassword = AppConfig.Get("SuperPassword", "MY@!");
            if (string.Equals(user, superUser, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(password, superPassword, StringComparison.Ordinal))
            {
                displayName = AppConfig.Get("SuperDisplayName", "管理员");
                return true;
            }

            const string sql = "SELECT TOP (1) [uPWD], [uName] FROM [dbo].[xt_usertable] " +
                "WHERE [UserID] = @username OR [uCode] = @username OR [uName] = @username OR [uPY] = @username " +
                "ORDER BY CASE WHEN [UserID] = @username THEN 0 WHEN [uCode] = @username THEN 1 WHEN [uName] = @username THEN 2 ELSE 3 END";
            using (var connection = new SqlConnection(AppConfig.ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 8;
                command.Parameters.Add("@username", SqlDbType.NVarChar, 256).Value = user;
                connection.Open();
                using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!reader.Read()) { displayName = null; userExists = false; return false; }
                    var accountDisplayName = reader.IsDBNull(1) ? user : Convert.ToString(reader.GetValue(1)).Trim();
                    if (reader.IsDBNull(0))
                    {
                        var nullPasswordValid = password.Length == 0;
                        displayName = nullPasswordValid ? accountDisplayName : null;
                        return nullPasswordValid;
                    }

                    var stored = Convert.ToString(reader.GetValue(0)).Trim();
                    var valid = string.Equals(stored, EncryptDes(password), StringComparison.OrdinalIgnoreCase);
                    displayName = valid ? accountDisplayName : null;
                    return valid;
                }
            }
        }

        private static string EncryptDes(string plainText)
        {
            if (plainText == null) plainText = string.Empty;
            using (var provider = new DESCryptoServiceProvider())
            {
                provider.Key = Encoding.ASCII.GetBytes(PasswordEntropy);
                provider.IV = Encoding.ASCII.GetBytes(PasswordEntropy);
                using (var stream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    var bytes = Encoding.UTF8.GetBytes(plainText);
                    cryptoStream.Write(bytes, 0, bytes.Length);
                    cryptoStream.FlushFinalBlock();
                    var builder = new StringBuilder();
                    foreach (var item in stream.ToArray()) builder.AppendFormat("{0:X2}", item);
                    return builder.ToString();
                }
            }
        }

        private static string GetFixedAccountDisplayName(string lookupValue)
        {
            if (string.IsNullOrEmpty(lookupValue)) return null;
            var superUser = AppConfig.Get("SuperUser", "muyang");
            if (string.IsNullOrEmpty(superUser)) return null;
            if (!string.Equals(lookupValue, superUser, StringComparison.OrdinalIgnoreCase)) return null;
            return AppConfig.Get("SuperDisplayName", "管理员");
        }

        private static async Task<string> FindDatabaseDisplayNameAsync(string user, CancellationToken cancellationToken)
        {
            const string sql = "SELECT TOP (1) [uName] FROM [dbo].[xt_usertable] " +
                "WHERE [UserID] = @username OR [uCode] = @username OR [uName] = @username OR [uPY] = @username " +
                "ORDER BY CASE WHEN [UserID] = @username THEN 0 WHEN [uCode] = @username THEN 1 WHEN [uName] = @username THEN 2 ELSE 3 END";
            using (var connection = new SqlConnection(AppConfig.ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 8;
                command.Parameters.Add("@username", SqlDbType.NVarChar, 256).Value = user;
                await connection.OpenAsync(cancellationToken);
                var value = await command.ExecuteScalarAsync(cancellationToken);
                return value == null || value == DBNull.Value ? null : Convert.ToString(value).Trim();
            }
        }

        private static string SafeIdentifier(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || !Regex.IsMatch(name, @"^[\p{L}_][\p{L}\p{N}_]*$"))
                throw new InvalidOperationException("表名或字段名配置无效：" + name);
            return name;
        }
    }
}
