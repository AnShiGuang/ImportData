namespace 批量导入程序重制版
{
    partial class 用户登录
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolTip databaseIconToolTip;
        private System.Windows.Forms.Panel brandPanel;
        private AntdUI.Panel loginPanel;
        private System.Windows.Forms.Label brandEyebrowLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Label userLabel;
        private AntdUI.Input userInput;
        private System.Windows.Forms.Label passwordLabel;
        private AntdUI.Input passwordInput;
        private AntdUI.Checkbox rememberPasswordCheckBox;
        private AntdUI.Checkbox autoLoginCheckBox;
        private AntdUI.Button loginButton;
        private System.Windows.Forms.Label securityLabel;
        private AntdUI.PageHeader windowBar;
        private AntdUI.Button closeButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.databaseIconToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.windowBar = new AntdUI.PageHeader();
            this.closeButton = new AntdUI.Button();
            this.loginPanel = new AntdUI.Panel();
            this.securityIcon = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.securityLabel = new System.Windows.Forms.Label();
            this.loginButton = new AntdUI.Button();
            this.autoLoginCheckBox = new AntdUI.Checkbox();
            this.rememberPasswordCheckBox = new AntdUI.Checkbox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.passwordInput = new AntdUI.Input();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userInput = new AntdUI.Input();
            this.userLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.brandPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.brandEyebrowLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.windowBar.SuspendLayout();
            this.loginPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.securityIcon)).BeginInit();
            this.brandPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // windowBar
            // 
            this.windowBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.windowBar.Controls.Add(this.closeButton);
            this.windowBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(192)))), ((int)(((byte)(198)))));
            this.windowBar.Location = new System.Drawing.Point(0, 0);
            this.windowBar.MaximizeBox = false;
            this.windowBar.MinimizeBox = false;
            this.windowBar.Name = "windowBar";
            this.windowBar.Size = new System.Drawing.Size(900, 32);
            this.windowBar.TabIndex = 11;
            this.windowBar.Text = " ";
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(230)))), ((int)(((byte)(233)))));
            this.closeButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.closeButton.DefaultBack = System.Drawing.Color.Transparent;
            this.closeButton.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(212)))), ((int)(((byte)(217)))));
            this.closeButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 13F, System.Drawing.FontStyle.Bold);
            this.closeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(146)))), ((int)(((byte)(153)))));
            this.closeButton.Location = new System.Drawing.Point(864, 4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Radius = 16;
            this.closeButton.Shape = AntdUI.TShape.Circle;
            this.closeButton.Size = new System.Drawing.Size(32, 32);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "×";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // loginPanel
            // 
            this.loginPanel.Back = System.Drawing.Color.White;
            this.loginPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.loginPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(233)))), ((int)(((byte)(238)))));
            this.loginPanel.BorderWidth = 1F;
            this.loginPanel.Controls.Add(this.securityIcon);
            this.loginPanel.Controls.Add(this.label5);
            this.loginPanel.Controls.Add(this.securityLabel);
            this.loginPanel.Controls.Add(this.loginButton);
            this.loginPanel.Controls.Add(this.autoLoginCheckBox);
            this.loginPanel.Controls.Add(this.rememberPasswordCheckBox);
            this.loginPanel.Controls.Add(this.messageLabel);
            this.loginPanel.Controls.Add(this.passwordInput);
            this.loginPanel.Controls.Add(this.passwordLabel);
            this.loginPanel.Controls.Add(this.userInput);
            this.loginPanel.Controls.Add(this.userLabel);
            this.loginPanel.Controls.Add(this.subtitleLabel);
            this.loginPanel.Location = new System.Drawing.Point(436, 53);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Radius = 10;
            this.loginPanel.Size = new System.Drawing.Size(436, 475);
            this.loginPanel.TabIndex = 10;
            this.loginPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // securityIcon
            // 
            this.securityIcon.BackColor = System.Drawing.Color.White;
            this.securityIcon.BackgroundImage = global::批量导入程序重制版.Properties.Resources.安全;
            this.securityIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.securityIcon.Location = new System.Drawing.Point(54, 417);
            this.securityIcon.Name = "securityIcon";
            this.securityIcon.Size = new System.Drawing.Size(19, 19);
            this.securityIcon.TabIndex = 418;
            this.securityIcon.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.SlateGray;
            this.label5.Location = new System.Drawing.Point(48, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 31);
            this.label5.TabIndex = 416;
            this.label5.Text = "欢迎登录";
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // securityLabel
            // 
            this.securityLabel.AutoSize = true;
            this.securityLabel.BackColor = System.Drawing.Color.White;
            this.securityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(156)))), ((int)(((byte)(171)))));
            this.securityLabel.Location = new System.Drawing.Point(74, 419);
            this.securityLabel.Name = "securityLabel";
            this.securityLabel.Size = new System.Drawing.Size(152, 17);
            this.securityLabel.TabIndex = 9;
            this.securityLabel.Text = "仅体检系统授权用户可登录";
            this.securityLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // loginButton
            // 
            this.loginButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(108)))), ((int)(((byte)(83)))));
            this.loginButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.loginButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.loginButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginButton.ForeColor = System.Drawing.Color.White;
            this.loginButton.Icon = global::批量导入程序重制版.Properties.Resources.BtnLogin;
            this.loginButton.IconRatio = 1F;
            this.loginButton.Location = new System.Drawing.Point(50, 340);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(334, 48);
            this.loginButton.TabIndex = 4;
            this.loginButton.Text = "登 录";
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // autoLoginCheckBox
            // 
            this.autoLoginCheckBox.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this.autoLoginCheckBox.BackColor = System.Drawing.Color.White;
            this.autoLoginCheckBox.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.autoLoginCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(103)))), ((int)(((byte)(119)))));
            this.autoLoginCheckBox.Location = new System.Drawing.Point(135, 293);
            this.autoLoginCheckBox.Name = "autoLoginCheckBox";
            this.autoLoginCheckBox.Size = new System.Drawing.Size(91, 37);
            this.autoLoginCheckBox.TabIndex = 3;
            this.autoLoginCheckBox.Text = "自动登录";
            this.autoLoginCheckBox.CheckedChanged += new AntdUI.BoolEventHandler(this.autoLoginCheckBox_CheckedChanged);
            // 
            // rememberPasswordCheckBox
            // 
            this.rememberPasswordCheckBox.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this.rememberPasswordCheckBox.BackColor = System.Drawing.Color.White;
            this.rememberPasswordCheckBox.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.rememberPasswordCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(103)))), ((int)(((byte)(119)))));
            this.rememberPasswordCheckBox.Location = new System.Drawing.Point(54, 293);
            this.rememberPasswordCheckBox.Name = "rememberPasswordCheckBox";
            this.rememberPasswordCheckBox.Size = new System.Drawing.Size(91, 37);
            this.rememberPasswordCheckBox.TabIndex = 2;
            this.rememberPasswordCheckBox.Text = "记住密码";
            this.rememberPasswordCheckBox.CheckedChanged += new AntdUI.BoolEventHandler(this.rememberPasswordCheckBox_CheckedChanged);
            // 
            // messageLabel
            // 
            this.messageLabel.BackColor = System.Drawing.Color.White;
            this.messageLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.messageLabel.Location = new System.Drawing.Point(54, 392);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(347, 22);
            this.messageLabel.TabIndex = 7;
            this.messageLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // passwordInput
            // 
            this.passwordInput.BackColor = System.Drawing.Color.White;
            this.passwordInput.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.passwordInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.passwordInput.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(184)))), ((int)(((byte)(177)))));
            this.passwordInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.passwordInput.Location = new System.Drawing.Point(50, 241);
            this.passwordInput.Name = "passwordInput";
            this.passwordInput.PlaceholderText = "请输入密码...";
            this.passwordInput.Radius = 7;
            this.passwordInput.Size = new System.Drawing.Size(334, 46);
            this.passwordInput.TabIndex = 1;
            this.passwordInput.UseSystemPasswordChar = true;
            this.passwordInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passwordInput_KeyDown);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.BackColor = System.Drawing.Color.White;
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.passwordLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.passwordLabel.Location = new System.Drawing.Point(51, 217);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(32, 17);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "密码";
            this.passwordLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // userInput
            // 
            this.userInput.BackColor = System.Drawing.Color.White;
            this.userInput.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.userInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.userInput.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(184)))), ((int)(((byte)(177)))));
            this.userInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.userInput.Location = new System.Drawing.Point(50, 162);
            this.userInput.Name = "userInput";
            this.userInput.PlaceholderText = "请输入账号...";
            this.userInput.Radius = 7;
            this.userInput.Size = new System.Drawing.Size(334, 46);
            this.userInput.SuffixFore = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(107)))), ((int)(((byte)(117)))));
            this.userInput.SuffixText = "";
            this.userInput.TabIndex = 0;
            this.userInput.TextChanged += new System.EventHandler(this.userInput_TextChanged);
            this.userInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userInput_KeyDown);
            this.userInput.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.userInput_PreviewKeyDown);
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.BackColor = System.Drawing.Color.White;
            this.userLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.userLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.userLabel.Location = new System.Drawing.Point(51, 138);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(32, 17);
            this.userLabel.TabIndex = 3;
            this.userLabel.Text = "账号";
            this.userLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.BackColor = System.Drawing.Color.White;
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(128)))), ((int)(((byte)(139)))));
            this.subtitleLabel.Location = new System.Drawing.Point(51, 79);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(163, 17);
            this.subtitleLabel.TabIndex = 2;
            this.subtitleLabel.Text = "批量导入程序重制版 · 请登录";
            this.subtitleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // brandPanel
            // 
            this.brandPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.brandPanel.Controls.Add(this.label2);
            this.brandPanel.Controls.Add(this.label1);
            this.brandPanel.Controls.Add(this.brandEyebrowLabel);
            this.brandPanel.Controls.Add(this.versionLabel);
            this.brandPanel.Controls.Add(this.pictureBox4);
            this.brandPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.brandPanel.Location = new System.Drawing.Point(0, 32);
            this.brandPanel.Name = "brandPanel";
            this.brandPanel.Size = new System.Drawing.Size(414, 540);
            this.brandPanel.TabIndex = 0;
            this.brandPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(54)))), ((int)(((byte)(66)))));
            this.label2.Location = new System.Drawing.Point(58, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 416;
            this.label2.Text = "批量导入程序重制版";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(54)))), ((int)(((byte)(66)))));
            this.label1.Location = new System.Drawing.Point(54, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 31);
            this.label1.TabIndex = 415;
            this.label1.Text = "批量导入程序重制版";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // brandEyebrowLabel
            // 
            this.brandEyebrowLabel.AutoSize = true;
            this.brandEyebrowLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.brandEyebrowLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(133)))), ((int)(((byte)(128)))));
            this.brandEyebrowLabel.Location = new System.Drawing.Point(57, 440);
            this.brandEyebrowLabel.Name = "brandEyebrowLabel";
            this.brandEyebrowLabel.Size = new System.Drawing.Size(97, 17);
            this.brandEyebrowLabel.TabIndex = 0;
            this.brandEyebrowLabel.Text = "MUYANG●沐阳";
            this.brandEyebrowLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.versionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(135)))), ((int)(((byte)(147)))));
            this.versionLabel.Location = new System.Drawing.Point(293, 440);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(63, 17);
            this.versionLabel.TabIndex = 4;
            this.versionLabel.Text = "版本 1.0.0";
            this.versionLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(91, 159);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(250, 250);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 417;
            this.pictureBox4.TabStop = false;
            // 
            // 用户登录
            // 
            this.AcceptButton = this.loginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(900, 572);
            this.Controls.Add(this.loginPanel);
            this.Controls.Add(this.brandPanel);
            this.Controls.Add(this.windowBar);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "用户登录";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量导入程序重制版";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFormDrag_MouseDown);
            this.windowBar.ResumeLayout(false);
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.securityIcon)).EndInit();
            this.brandPanel.ResumeLayout(false);
            this.brandPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.PictureBox securityIcon;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}
