namespace 批量导入程序重制版
{
    partial class frmHome
    {
        private AntdUI.Label homeDescriptionLabel;
        private AntdUI.Label welcomePrefixLabel;
        private AntdUI.Label welcomeLabel;
        private System.Windows.Forms.PictureBox homeIllustration;
        private System.Windows.Forms.TableLayoutPanel statsLayoutPanel;
        private AntdUI.Panel registeredCardPanel;
        private AntdUI.Panel examinedCardPanel;
        private AntdUI.Panel uploadedCardPanel;
        private AntdUI.Label registeredTitleLabel;
        private AntdUI.Label examinedTitleLabel;
        private AntdUI.Label uploadedTitleLabel;
        private AntdUI.Label registeredCountLabel;
        private AntdUI.Label examinedCountLabel;
        private AntdUI.Label uploadedCountLabel;

        private void InitializeComponent()
        {
            this.homeDescriptionLabel = new AntdUI.Label();
            this.welcomePrefixLabel = new AntdUI.Label();
            this.welcomeLabel = new AntdUI.Label();
            this.homeIllustration = new System.Windows.Forms.PictureBox();
            this.statsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.registeredCardPanel = new AntdUI.Panel();
            this.registeredTitleLabel = new AntdUI.Label();
            this.registeredCountLabel = new AntdUI.Label();
            this.examinedCardPanel = new AntdUI.Panel();
            this.examinedTitleLabel = new AntdUI.Label();
            this.examinedCountLabel = new AntdUI.Label();
            this.uploadedCardPanel = new AntdUI.Panel();
            this.uploadedTitleLabel = new AntdUI.Label();
            this.uploadedCountLabel = new AntdUI.Label();
            ((System.ComponentModel.ISupportInitialize)(this.homeIllustration)).BeginInit();
            this.statsLayoutPanel.SuspendLayout();
            this.registeredCardPanel.SuspendLayout();
            this.examinedCardPanel.SuspendLayout();
            this.uploadedCardPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // homeDescriptionLabel
            // 
            this.homeDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(123)))), ((int)(((byte)(134)))));
            this.homeDescriptionLabel.Location = new System.Drawing.Point(31, 30);
            this.homeDescriptionLabel.Name = "homeDescriptionLabel";
            this.homeDescriptionLabel.Size = new System.Drawing.Size(420, 22);
            this.homeDescriptionLabel.TabIndex = 1;
            this.homeDescriptionLabel.Text = "公共卫生数据上传工作台";
            // 
            // welcomePrefixLabel
            // 
            this.welcomePrefixLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.welcomePrefixLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(94)))), ((int)(((byte)(103)))));
            this.welcomePrefixLabel.Location = new System.Drawing.Point(733, 30);
            this.welcomePrefixLabel.Name = "welcomePrefixLabel";
            this.welcomePrefixLabel.Size = new System.Drawing.Size(88, 22);
            this.welcomePrefixLabel.TabIndex = 2;
            this.welcomePrefixLabel.Text = "欢迎登录：";
            this.welcomePrefixLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.welcomeLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.welcomeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.welcomeLabel.Location = new System.Drawing.Point(824, 30);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(92, 22);
            this.welcomeLabel.TabIndex = 3;
            this.welcomeLabel.Text = "预览用户";
            // 
            // homeIllustration
            // 
            this.homeIllustration.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.homeIllustration.BackColor = System.Drawing.Color.Transparent;
            this.homeIllustration.BackgroundImage = global::批量导入程序重制版.Properties.Resources.homepage_analytics;
            this.homeIllustration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.homeIllustration.Location = new System.Drawing.Point(198, 216);
            this.homeIllustration.Name = "homeIllustration";
            this.homeIllustration.Size = new System.Drawing.Size(512, 512);
            this.homeIllustration.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.homeIllustration.TabIndex = 1;
            this.homeIllustration.TabStop = false;
            // 
            // statsLayoutPanel
            // 
            this.statsLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statsLayoutPanel.ColumnCount = 3;
            this.statsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.statsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.statsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.334F));
            this.statsLayoutPanel.Controls.Add(this.registeredCardPanel, 0, 0);
            this.statsLayoutPanel.Controls.Add(this.examinedCardPanel, 1, 0);
            this.statsLayoutPanel.Controls.Add(this.uploadedCardPanel, 2, 0);
            this.statsLayoutPanel.Location = new System.Drawing.Point(31, 91);
            this.statsLayoutPanel.Name = "statsLayoutPanel";
            this.statsLayoutPanel.RowCount = 1;
            this.statsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statsLayoutPanel.Size = new System.Drawing.Size(856, 122);
            this.statsLayoutPanel.TabIndex = 0;
            // 
            // registeredCardPanel
            // 
            this.registeredCardPanel.Back = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(234)))), ((int)(((byte)(225)))));
            this.registeredCardPanel.BackColor = System.Drawing.Color.White;
            this.registeredCardPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(190)))), ((int)(((byte)(148)))));
            this.registeredCardPanel.BorderWidth = 1F;
            this.registeredCardPanel.Controls.Add(this.registeredTitleLabel);
            this.registeredCardPanel.Controls.Add(this.registeredCountLabel);
            this.registeredCardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registeredCardPanel.Location = new System.Drawing.Point(0, 0);
            this.registeredCardPanel.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.registeredCardPanel.Name = "registeredCardPanel";
            this.registeredCardPanel.Radius = 8;
            this.registeredCardPanel.Size = new System.Drawing.Size(277, 122);
            this.registeredCardPanel.TabIndex = 0;
            // 
            // registeredTitleLabel
            // 
            this.registeredTitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(234)))), ((int)(((byte)(225)))));
            this.registeredTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(94)))), ((int)(((byte)(103)))));
            this.registeredTitleLabel.Location = new System.Drawing.Point(17, 15);
            this.registeredTitleLabel.Name = "registeredTitleLabel";
            this.registeredTitleLabel.Size = new System.Drawing.Size(230, 22);
            this.registeredTitleLabel.TabIndex = 0;
            this.registeredTitleLabel.Text = "当日登记";
            // 
            // registeredCountLabel
            // 
            this.registeredCountLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(234)))), ((int)(((byte)(225)))));
            this.registeredCountLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 22F, System.Drawing.FontStyle.Bold);
            this.registeredCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.registeredCountLabel.Location = new System.Drawing.Point(17, 51);
            this.registeredCountLabel.Name = "registeredCountLabel";
            this.registeredCountLabel.Size = new System.Drawing.Size(240, 43);
            this.registeredCountLabel.TabIndex = 1;
            this.registeredCountLabel.Text = "— 人";
            // 
            // examinedCardPanel
            // 
            this.examinedCardPanel.Back = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(231)))), ((int)(((byte)(237)))));
            this.examinedCardPanel.BackColor = System.Drawing.Color.White;
            this.examinedCardPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(202)))), ((int)(((byte)(233)))));
            this.examinedCardPanel.BorderWidth = 1F;
            this.examinedCardPanel.Controls.Add(this.examinedTitleLabel);
            this.examinedCardPanel.Controls.Add(this.examinedCountLabel);
            this.examinedCardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.examinedCardPanel.Location = new System.Drawing.Point(289, 0);
            this.examinedCardPanel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.examinedCardPanel.Name = "examinedCardPanel";
            this.examinedCardPanel.Radius = 8;
            this.examinedCardPanel.Size = new System.Drawing.Size(277, 122);
            this.examinedCardPanel.TabIndex = 1;
            // 
            // examinedTitleLabel
            // 
            this.examinedTitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(231)))), ((int)(((byte)(237)))));
            this.examinedTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(94)))), ((int)(((byte)(103)))));
            this.examinedTitleLabel.Location = new System.Drawing.Point(17, 15);
            this.examinedTitleLabel.Name = "examinedTitleLabel";
            this.examinedTitleLabel.Size = new System.Drawing.Size(230, 22);
            this.examinedTitleLabel.TabIndex = 0;
            this.examinedTitleLabel.Text = "当日总检";
            // 
            // examinedCountLabel
            // 
            this.examinedCountLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(231)))), ((int)(((byte)(237)))));
            this.examinedCountLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 22F, System.Drawing.FontStyle.Bold);
            this.examinedCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(114)))), ((int)(((byte)(160)))));
            this.examinedCountLabel.Location = new System.Drawing.Point(17, 51);
            this.examinedCountLabel.Name = "examinedCountLabel";
            this.examinedCountLabel.Size = new System.Drawing.Size(240, 43);
            this.examinedCountLabel.TabIndex = 1;
            this.examinedCountLabel.Text = "— 人";
            // 
            // uploadedCardPanel
            // 
            this.uploadedCardPanel.Back = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(231)))), ((int)(((byte)(213)))));
            this.uploadedCardPanel.BackColor = System.Drawing.Color.White;
            this.uploadedCardPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(190)))), ((int)(((byte)(121)))));
            this.uploadedCardPanel.BorderWidth = 1F;
            this.uploadedCardPanel.Controls.Add(this.uploadedTitleLabel);
            this.uploadedCardPanel.Controls.Add(this.uploadedCountLabel);
            this.uploadedCardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadedCardPanel.Location = new System.Drawing.Point(578, 0);
            this.uploadedCardPanel.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.uploadedCardPanel.Name = "uploadedCardPanel";
            this.uploadedCardPanel.Radius = 8;
            this.uploadedCardPanel.Size = new System.Drawing.Size(278, 122);
            this.uploadedCardPanel.TabIndex = 2;
            // 
            // uploadedTitleLabel
            // 
            this.uploadedTitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(231)))), ((int)(((byte)(213)))));
            this.uploadedTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(62)))));
            this.uploadedTitleLabel.Location = new System.Drawing.Point(17, 15);
            this.uploadedTitleLabel.Name = "uploadedTitleLabel";
            this.uploadedTitleLabel.Size = new System.Drawing.Size(250, 22);
            this.uploadedTitleLabel.TabIndex = 0;
            this.uploadedTitleLabel.Text = "当日总检已上传成功";
            // 
            // uploadedCountLabel
            // 
            this.uploadedCountLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(231)))), ((int)(((byte)(213)))));
            this.uploadedCountLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 22F, System.Drawing.FontStyle.Bold);
            this.uploadedCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(135)))), ((int)(((byte)(42)))));
            this.uploadedCountLabel.Location = new System.Drawing.Point(17, 51);
            this.uploadedCountLabel.Name = "uploadedCountLabel";
            this.uploadedCountLabel.Size = new System.Drawing.Size(240, 43);
            this.uploadedCountLabel.TabIndex = 1;
            this.uploadedCountLabel.Text = "— 人";
            // 
            // frmHome
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(918, 680);
            this.Controls.Add(this.homeDescriptionLabel);
            this.Controls.Add(this.welcomePrefixLabel);
            this.Controls.Add(this.welcomeLabel);
            this.Controls.Add(this.statsLayoutPanel);
            this.Controls.Add(this.homeIllustration);
            this.Name = "frmHome";
            this.Padding = new System.Windows.Forms.Padding(28);
            this.Text = "程序主页";
            ((System.ComponentModel.ISupportInitialize)(this.homeIllustration)).EndInit();
            this.statsLayoutPanel.ResumeLayout(false);
            this.registeredCardPanel.ResumeLayout(false);
            this.examinedCardPanel.ResumeLayout(false);
            this.uploadedCardPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
