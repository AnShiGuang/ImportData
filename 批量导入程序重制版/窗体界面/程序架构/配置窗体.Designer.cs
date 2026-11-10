namespace 批量导入程序重制版
{
    partial class 配置窗体
    {
        private AntdUI.Label configTitleLabel;
        private AntdUI.Label configDescriptionLabel;
        private AntdUI.Panel uploadSectionPanel;
        private AntdUI.Label uploadSectionTitleLabel;
        private AntdUI.Label uploadBaseUrlLabel;
        private AntdUI.Input uploadBaseUrlInput;
        private AntdUI.Label appIdLabel;
        private AntdUI.Input appIdInput;
        private AntdUI.Label userCodeLabel;
        private AntdUI.Input userCodeInput;
        private AntdUI.Label publicKeyLabel;
        private AntdUI.Input publicKeyInput;
        private AntdUI.Label privateKeyLabel;
        private AntdUI.Input privateKeyInput;
        private AntdUI.Button saveUploadSettingsButton;
        private AntdUI.Label uploadStatusLabel;
        private AntdUI.Panel campusSectionPanel;
        private AntdUI.Label campusSectionTitleLabel;
        private AntdUI.Label campusLabel;
        private AntdUI.Select campusSelect;
        private AntdUI.Button saveCampusButton;
        private AntdUI.Label campusStatusLabel;

        private void InitializeComponent()
        {
            this.configTitleLabel = new AntdUI.Label();
            this.configDescriptionLabel = new AntdUI.Label();
            this.uploadSectionPanel = new AntdUI.Panel();
            this.uploadSectionTitleLabel = new AntdUI.Label();
            this.uploadBaseUrlLabel = new AntdUI.Label();
            this.uploadBaseUrlInput = new AntdUI.Input();
            this.appIdLabel = new AntdUI.Label();
            this.appIdInput = new AntdUI.Input();
            this.userCodeLabel = new AntdUI.Label();
            this.userCodeInput = new AntdUI.Input();
            this.publicKeyLabel = new AntdUI.Label();
            this.publicKeyInput = new AntdUI.Input();
            this.privateKeyLabel = new AntdUI.Label();
            this.privateKeyInput = new AntdUI.Input();
            this.saveUploadSettingsButton = new AntdUI.Button();
            this.uploadStatusLabel = new AntdUI.Label();
            this.campusSectionPanel = new AntdUI.Panel();
            this.campusSectionTitleLabel = new AntdUI.Label();
            this.campusLabel = new AntdUI.Label();
            this.campusSelect = new AntdUI.Select();
            this.saveCampusButton = new AntdUI.Button();
            this.campusStatusLabel = new AntdUI.Label();
            this.campusSectionPanel.SuspendLayout();
            this.uploadSectionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // 配置窗体
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(918, 602);
            this.Padding = new System.Windows.Forms.Padding(28);
            this.Text = "配置参数";
            // 
            // configTitleLabel
            // 
            this.configTitleLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold);
            this.configTitleLabel.ForeColor = System.Drawing.Color.FromArgb(37, 55, 65);
            this.configTitleLabel.Location = new System.Drawing.Point(28, 20);
            this.configTitleLabel.Name = "configTitleLabel";
            this.configTitleLabel.Size = new System.Drawing.Size(280, 32);
            this.configTitleLabel.Text = "配置参数";
            // 
            // configDescriptionLabel
            // 
            this.configDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(108, 123, 134);
            this.configDescriptionLabel.Location = new System.Drawing.Point(30, 55);
            this.configDescriptionLabel.Name = "configDescriptionLabel";
            this.configDescriptionLabel.Size = new System.Drawing.Size(820, 22);
            this.configDescriptionLabel.Text = "配置批量导入上报服务参数；接口密钥会加密保存在当前 Windows 用户配置中。";
            // 
            // uploadSectionPanel
            // 
            this.uploadSectionPanel.BackColor = System.Drawing.Color.White;
            this.uploadSectionPanel.BorderColor = System.Drawing.Color.FromArgb(226, 233, 238);
            this.uploadSectionPanel.BorderWidth = 1F;
            this.uploadSectionPanel.Location = new System.Drawing.Point(28, 188);
            this.uploadSectionPanel.Name = "uploadSectionPanel";
            this.uploadSectionPanel.Radius = 8;
            this.uploadSectionPanel.Size = new System.Drawing.Size(862, 386);
            this.uploadSectionPanel.TabIndex = 0;
            this.uploadSectionPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.uploadSectionPanel.Controls.Add(this.uploadSectionTitleLabel);
            this.uploadSectionPanel.Controls.Add(this.uploadBaseUrlLabel);
            this.uploadSectionPanel.Controls.Add(this.uploadBaseUrlInput);
            this.uploadSectionPanel.Controls.Add(this.appIdLabel);
            this.uploadSectionPanel.Controls.Add(this.appIdInput);
            this.uploadSectionPanel.Controls.Add(this.userCodeLabel);
            this.uploadSectionPanel.Controls.Add(this.userCodeInput);
            this.uploadSectionPanel.Controls.Add(this.publicKeyLabel);
            this.uploadSectionPanel.Controls.Add(this.publicKeyInput);
            this.uploadSectionPanel.Controls.Add(this.privateKeyLabel);
            this.uploadSectionPanel.Controls.Add(this.privateKeyInput);
            this.uploadSectionPanel.Controls.Add(this.saveUploadSettingsButton);
            this.uploadSectionPanel.Controls.Add(this.uploadStatusLabel);
            //
            // campusSectionPanel
            //
            this.campusSectionPanel.BackColor = System.Drawing.Color.White;
            this.campusSectionPanel.BorderColor = System.Drawing.Color.FromArgb(226, 233, 238);
            this.campusSectionPanel.BorderWidth = 1F;
            this.campusSectionPanel.Location = new System.Drawing.Point(28, 94);
            this.campusSectionPanel.Name = "campusSectionPanel";
            this.campusSectionPanel.Radius = 8;
            this.campusSectionPanel.Size = new System.Drawing.Size(862, 82);
            this.campusSectionPanel.TabIndex = 0;
            this.campusSectionPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.campusSectionPanel.Controls.Add(this.campusSectionTitleLabel);
            this.campusSectionPanel.Controls.Add(this.campusLabel);
            this.campusSectionPanel.Controls.Add(this.campusSelect);
            this.campusSectionPanel.Controls.Add(this.saveCampusButton);
            this.campusSectionPanel.Controls.Add(this.campusStatusLabel);
            //
            // campusSectionTitleLabel
            //
            this.campusSectionTitleLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.campusSectionTitleLabel.ForeColor = System.Drawing.Color.FromArgb(37, 55, 65);
            this.campusSectionTitleLabel.Location = new System.Drawing.Point(18, 10);
            this.campusSectionTitleLabel.Name = "campusSectionTitleLabel";
            this.campusSectionTitleLabel.Size = new System.Drawing.Size(400, 24);
            this.campusSectionTitleLabel.Text = "本地院区";
            //
            // campusLabel
            //
            this.campusLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.campusLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.campusLabel.Location = new System.Drawing.Point(18, 46);
            this.campusLabel.Name = "campusLabel";
            this.campusLabel.Size = new System.Drawing.Size(100, 20);
            this.campusLabel.Text = "院区";
            //
            // campusSelect
            //
            this.campusSelect.BorderActive = System.Drawing.Color.FromArgb(35, 145, 112);
            this.campusSelect.BorderHover = System.Drawing.Color.FromArgb(35, 145, 112);
            this.campusSelect.Location = new System.Drawing.Point(120, 43);
            this.campusSelect.Name = "campusSelect";
            this.campusSelect.Radius = 7;
            this.campusSelect.Size = new System.Drawing.Size(320, 34);
            this.campusSelect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // saveCampusButton
            //
            this.saveCampusButton.BackColor = System.Drawing.Color.FromArgb(35, 145, 112);
            this.saveCampusButton.ForeColor = System.Drawing.Color.White;
            this.saveCampusButton.Location = new System.Drawing.Point(460, 43);
            this.saveCampusButton.Name = "saveCampusButton";
            this.saveCampusButton.Radius = 7;
            this.saveCampusButton.Size = new System.Drawing.Size(140, 34);
            this.saveCampusButton.Text = "保存院区";
            this.saveCampusButton.Type = AntdUI.TTypeMini.Primary;
            this.saveCampusButton.Click += new System.EventHandler(this.saveCampus_Click);
            //
            // campusStatusLabel
            //
            this.campusStatusLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular);
            this.campusStatusLabel.ForeColor = System.Drawing.Color.FromArgb(108, 123, 134);
            this.campusStatusLabel.Location = new System.Drawing.Point(620, 49);
            this.campusStatusLabel.Name = "campusStatusLabel";
            this.campusStatusLabel.Size = new System.Drawing.Size(220, 20);
            this.campusStatusLabel.Text = "";
            // 
            // uploadSectionTitleLabel
            // 
            this.uploadSectionTitleLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.uploadSectionTitleLabel.ForeColor = System.Drawing.Color.FromArgb(37, 55, 65);
            this.uploadSectionTitleLabel.Location = new System.Drawing.Point(18, 13);
            this.uploadSectionTitleLabel.Name = "uploadSectionTitleLabel";
            this.uploadSectionTitleLabel.Size = new System.Drawing.Size(400, 24);
            this.uploadSectionTitleLabel.Text = "批量导入上报接口";
            // 
            // uploadBaseUrlLabel
            // 
            this.uploadBaseUrlLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.uploadBaseUrlLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.uploadBaseUrlLabel.Location = new System.Drawing.Point(18, 45);
            this.uploadBaseUrlLabel.Name = "uploadBaseUrlLabel";
            this.uploadBaseUrlLabel.Size = new System.Drawing.Size(220, 20);
            this.uploadBaseUrlLabel.Text = "服务根地址（HTTPS）";
            // 
            // uploadBaseUrlInput
            // 
            this.uploadBaseUrlInput.BackColor = System.Drawing.Color.White;
            this.uploadBaseUrlInput.BorderActive = System.Drawing.Color.FromArgb(35, 145, 112);
            this.uploadBaseUrlInput.BorderColor = System.Drawing.Color.FromArgb(215, 225, 230);
            this.uploadBaseUrlInput.BorderHover = System.Drawing.Color.FromArgb(112, 184, 177);
            this.uploadBaseUrlInput.ForeColor = System.Drawing.Color.FromArgb(37, 55, 65);
            this.uploadBaseUrlInput.Location = new System.Drawing.Point(18, 68);
            this.uploadBaseUrlInput.Name = "uploadBaseUrlInput";
            this.uploadBaseUrlInput.PlaceholderText = "https://服务器地址";
            this.uploadBaseUrlInput.Radius = 7;
            this.uploadBaseUrlInput.Size = new System.Drawing.Size(824, 38);
            this.uploadBaseUrlInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            // 
            // appIdLabel
            // 
            this.appIdLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.appIdLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.appIdLabel.Location = new System.Drawing.Point(18, 119);
            this.appIdLabel.Name = "appIdLabel";
            this.appIdLabel.Size = new System.Drawing.Size(380, 20);
            this.appIdLabel.Text = "接口 appId";
            this.appIdInput.BackColor = System.Drawing.Color.White;
            this.appIdInput.BorderActive = System.Drawing.Color.FromArgb(35, 145, 112);
            this.appIdInput.BorderColor = System.Drawing.Color.FromArgb(215, 225, 230);
            this.appIdInput.BorderHover = System.Drawing.Color.FromArgb(112, 184, 177);
            this.appIdInput.ForeColor = System.Drawing.Color.FromArgb(37, 55, 65);
            this.appIdInput.Location = new System.Drawing.Point(18, 142);
            this.appIdInput.Name = "appIdInput";
            this.appIdInput.PlaceholderText = "接口应用标识";
            this.appIdInput.Radius = 7;
            this.appIdInput.Size = new System.Drawing.Size(390, 38);
            this.appIdInput.UseSystemPasswordChar = true;
            this.userCodeLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.userCodeLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.userCodeLabel.Location = new System.Drawing.Point(430, 119);
            this.userCodeLabel.Name = "userCodeLabel";
            this.userCodeLabel.Size = new System.Drawing.Size(390, 20);
            this.userCodeLabel.Text = "批量导入账号代码 userCode";
            this.userCodeInput.BackColor = System.Drawing.Color.White;
            this.userCodeInput.BorderActive = System.Drawing.Color.FromArgb(35, 145, 112);
            this.userCodeInput.BorderColor = System.Drawing.Color.FromArgb(215, 225, 230);
            this.userCodeInput.BorderHover = System.Drawing.Color.FromArgb(112, 184, 177);
            this.userCodeInput.ForeColor = System.Drawing.Color.FromArgb(37, 55, 65);
            this.userCodeInput.Location = new System.Drawing.Point(430, 142);
            this.userCodeInput.Name = "userCodeInput";
            this.userCodeInput.PlaceholderText = "请输入批量导入账号代码";
            this.userCodeInput.Radius = 7;
            this.userCodeInput.Size = new System.Drawing.Size(410, 38);
            this.userCodeInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            // 
            // key fields
            // 
            this.publicKeyLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.publicKeyLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.publicKeyLabel.Location = new System.Drawing.Point(18, 193);
            this.publicKeyLabel.Name = "publicKeyLabel";
            this.publicKeyLabel.Size = new System.Drawing.Size(390, 20);
            this.publicKeyLabel.Text = "平台 SM2 公钥";
            this.publicKeyInput.BackColor = System.Drawing.Color.White;
            this.publicKeyInput.BorderActive = System.Drawing.Color.FromArgb(35, 145, 112);
            this.publicKeyInput.BorderColor = System.Drawing.Color.FromArgb(215, 225, 230);
            this.publicKeyInput.BorderHover = System.Drawing.Color.FromArgb(112, 184, 177);
            this.publicKeyInput.ForeColor = System.Drawing.Color.FromArgb(37, 55, 65);
            this.publicKeyInput.Location = new System.Drawing.Point(18, 216);
            this.publicKeyInput.Multiline = true;
            this.publicKeyInput.Name = "publicKeyInput";
            this.publicKeyInput.PlaceholderText = "平台公钥";
            this.publicKeyInput.Radius = 7;
            this.publicKeyInput.Size = new System.Drawing.Size(390, 70);
            this.publicKeyInput.UseSystemPasswordChar = true;
            this.privateKeyLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.privateKeyLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.privateKeyLabel.Location = new System.Drawing.Point(430, 193);
            this.privateKeyLabel.Name = "privateKeyLabel";
            this.privateKeyLabel.Size = new System.Drawing.Size(390, 20);
            this.privateKeyLabel.Text = "本方 SM2 私钥（加密保存）";
            this.privateKeyInput.BackColor = System.Drawing.Color.White;
            this.privateKeyInput.BorderActive = System.Drawing.Color.FromArgb(35, 145, 112);
            this.privateKeyInput.BorderColor = System.Drawing.Color.FromArgb(215, 225, 230);
            this.privateKeyInput.BorderHover = System.Drawing.Color.FromArgb(112, 184, 177);
            this.privateKeyInput.ForeColor = System.Drawing.Color.FromArgb(37, 55, 65);
            this.privateKeyInput.Location = new System.Drawing.Point(430, 216);
            this.privateKeyInput.Multiline = true;
            this.privateKeyInput.Name = "privateKeyInput";
            this.privateKeyInput.PlaceholderText = "本方私钥";
            this.privateKeyInput.Radius = 7;
            this.privateKeyInput.Size = new System.Drawing.Size(410, 70);
            this.privateKeyInput.UseSystemPasswordChar = true;
            // 
            // action buttons and status
            // 
            this.saveUploadSettingsButton.BackColor = System.Drawing.Color.FromArgb(35, 145, 112);
            this.saveUploadSettingsButton.ForeColor = System.Drawing.Color.White;
            this.saveUploadSettingsButton.Location = new System.Drawing.Point(18, 303);
            this.saveUploadSettingsButton.Name = "saveUploadSettingsButton";
            this.saveUploadSettingsButton.Radius = 7;
            this.saveUploadSettingsButton.Size = new System.Drawing.Size(146, 38);
            this.saveUploadSettingsButton.TabIndex = 0;
            this.saveUploadSettingsButton.Text = "保存接口配置";
            this.saveUploadSettingsButton.Type = AntdUI.TTypeMini.Primary;
            this.saveUploadSettingsButton.Click += new System.EventHandler(this.saveUploadSettings_Click);
            this.uploadStatusLabel.ForeColor = System.Drawing.Color.FromArgb(108, 123, 134);
            this.uploadStatusLabel.Location = new System.Drawing.Point(18, 348);
            this.uploadStatusLabel.Name = "uploadStatusLabel";
            this.uploadStatusLabel.Size = new System.Drawing.Size(800, 22);
            // 
            // root controls
            // 
            this.Controls.Add(this.uploadSectionPanel);
            this.Controls.Add(this.campusSectionPanel);
            this.Controls.Add(this.configDescriptionLabel);
            this.Controls.Add(this.configTitleLabel);
            this.campusSectionPanel.ResumeLayout(false);
            this.uploadSectionPanel.ResumeLayout(false);
            this.uploadSectionPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
