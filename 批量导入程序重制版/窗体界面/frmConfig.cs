using System;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    public sealed partial class frmConfig : PageFormBase
    {
        private readonly string userName;
        private string savedUploadBaseUrl;
        public frmConfig() : this(string.Empty) { }

        public frmConfig(string userName)
        {
            this.userName = userName;
            InitializeComponent();
            savedUploadBaseUrl = AppConfig.UploadBaseUrl;
            uploadBaseUrlInput.Text = savedUploadBaseUrl;
            appIdInput.Text = AppConfig.BatchImportAppId;
            userCodeInput.Text = AppConfig.BatchImportUserCode;
            publicKeyInput.Text = AppConfig.BatchImportPublicKey;
            privateKeyInput.Text = AppConfig.BatchImportPrivateKey;
            uploadStatusLabel.Text = string.IsNullOrWhiteSpace(AppConfig.BatchImportPrivateKey)
                ? "尚未配置新接口密钥。"
                : "新接口密钥已加密保存在当前 Windows 用户配置中。";
        }

        private async System.Threading.Tasks.Task TryWriteLogAsync(string level, string action, string message, string details)
        {
            try
            {
                await 操作日志.WriteAsync(操作日志.Program, level, userName, 登录限制.GetClientIp(), action, message, details);
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    MessageBox.Show(this, "配置操作未能写入日志：" + ex.Message, "日志记录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void saveUploadSettings_Click(object sender, EventArgs e)
        {
            Uri parsed;
            if (!TryGetUploadBaseUrl(out parsed))
            {
                MessageBox.Show(this, "新接口服务根地址必须是有效的 HTTPS 地址。", "批量导入接口配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(userCodeInput.Text))
            {
                MessageBox.Show(this, "请输入批量导入账号代码 userCode。", "批量导入接口配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            saveUploadSettingsButton.Enabled = false;
            try
            {
                var normalizedBaseUrl = parsed.GetLeftPart(UriPartial.Authority).TrimEnd('/');
                var endpointChanged = !string.Equals(savedUploadBaseUrl.TrimEnd('/'), normalizedBaseUrl, StringComparison.OrdinalIgnoreCase);
                var credentialsMissing = string.IsNullOrWhiteSpace(appIdInput.Text)
                    || string.IsNullOrWhiteSpace(publicKeyInput.Text)
                    || string.IsNullOrWhiteSpace(privateKeyInput.Text);
                if (endpointChanged || credentialsMissing)
                {
                    uploadStatusLabel.Text = "正在自动申请接口密钥…";
                    var bundle = await BatchImportUploadService.GenerateKeysAsync(parsed);
                    appIdInput.Text = bundle.AppId;
                    publicKeyInput.Text = bundle.PublicKey;
                    privateKeyInput.Text = bundle.PrivateKey;
                }

                AppConfig.Set("UploadBaseUrl", normalizedBaseUrl);
                AppConfig.SetBatchImportCredentials(appIdInput.Text, publicKeyInput.Text, privateKeyInput.Text);
                AppConfig.SetBatchImportUserCode(userCodeInput.Text);
                uploadBaseUrlInput.Text = AppConfig.UploadBaseUrl;
                savedUploadBaseUrl = AppConfig.UploadBaseUrl;
                appIdInput.Text = AppConfig.BatchImportAppId;
                publicKeyInput.Text = AppConfig.BatchImportPublicKey;
                privateKeyInput.Text = AppConfig.BatchImportPrivateKey;
                userCodeInput.Text = AppConfig.BatchImportUserCode;
                uploadStatusLabel.Text = "上传配置已加密保存；接口密钥已自动缓存。";
                await TryWriteLogAsync("常规", "保存批量导入接口配置", "批量导入接口参数已加密保存", "服务器=" + parsed.Authority + "；批量导入账号代码已配置");
                MessageBox.Show(this, "上传配置已加密保存。", "批量导入接口配置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                uploadStatusLabel.Text = "配置保存失败。";
                MessageBox.Show(this, "保存上传配置失败：" + ex.Message, "批量导入接口配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed) saveUploadSettingsButton.Enabled = true;
            }
        }

        private bool TryGetUploadBaseUrl(out Uri parsed)
        {
            return Uri.TryCreate(uploadBaseUrlInput.Text.Trim(), UriKind.Absolute, out parsed)
                && parsed.Scheme == Uri.UriSchemeHttps;
        }
    }
}
