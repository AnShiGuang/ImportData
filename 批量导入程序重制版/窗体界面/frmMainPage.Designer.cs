namespace 批量导入程序重制版
{
    partial class frmMainPage
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel sidebarPanel;
        private AntdUI.Panel navigationGroupPanel;
        private System.Windows.Forms.Panel contentHost;
        private AntdUI.Panel pageFramePanel;
        private System.Windows.Forms.Panel workspacePanel;
        private System.Windows.Forms.Label brandSubLabel;
        private System.Windows.Forms.ToolTip navigationToolTip;
        private AntdUI.PageHeader windowBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && _configPermissionPicture != null && _configPermissionPicture.Image != null)
                _configPermissionPicture.Image.Dispose();
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.navigationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.navigationGroupPanel = new AntdUI.Panel();
            this.sidebarToggleMenuButton = new AntdUI.Button();
            this.button1 = new AntdUI.Button();
            this.logoutButton = new AntdUI.Button();
            this.configMenuButton = new AntdUI.Button();
            this.functionPanelMenuButton = new AntdUI.Button();
            this.uploadLogMenuButton = new AntdUI.Button();
            this.importDataMenuButton = new AntdUI.Button();
            this.homeMenuButton = new AntdUI.Button();
            this.brandSubLabel = new System.Windows.Forms.Label();
            this.contentHost = new System.Windows.Forms.Panel();
            this.pageFramePanel = new AntdUI.Panel();
            this.workspacePanel = new System.Windows.Forms.Panel();
            this.windowBar = new AntdUI.PageHeader();
            this.sidebarPanel.SuspendLayout();
            this.navigationGroupPanel.SuspendLayout();
            this.contentHost.SuspendLayout();
            this.workspacePanel.SuspendLayout();
            this.windowBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.White;
            this.sidebarPanel.Controls.Add(this.navigationGroupPanel);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(208, 728);
            this.sidebarPanel.TabIndex = 1;
            this.sidebarPanel.Resize += new System.EventHandler(this.mainPage_SidebarPanel_Resize);
            // 
            // navigationGroupPanel
            // 
            this.navigationGroupPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.navigationGroupPanel.Back = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.navigationGroupPanel.BackColor = System.Drawing.Color.Transparent;
            this.navigationGroupPanel.Controls.Add(this.sidebarToggleMenuButton);
            this.navigationGroupPanel.Controls.Add(this.button1);
            this.navigationGroupPanel.Controls.Add(this.logoutButton);
            this.navigationGroupPanel.Controls.Add(this.configMenuButton);
            this.navigationGroupPanel.Controls.Add(this.functionPanelMenuButton);
            this.navigationGroupPanel.Controls.Add(this.uploadLogMenuButton);
            this.navigationGroupPanel.Controls.Add(this.importDataMenuButton);
            this.navigationGroupPanel.Controls.Add(this.homeMenuButton);
            this.navigationGroupPanel.Location = new System.Drawing.Point(18, 24);
            this.navigationGroupPanel.Name = "navigationGroupPanel";
            this.navigationGroupPanel.Radius = 10;
            this.navigationGroupPanel.Size = new System.Drawing.Size(176, 680);
            this.navigationGroupPanel.TabIndex = 15;
            // 
            // sidebarToggleMenuButton
            // 
            this.sidebarToggleMenuButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sidebarToggleMenuButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(108)))), ((int)(((byte)(83)))));
            this.sidebarToggleMenuButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.sidebarToggleMenuButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.sidebarToggleMenuButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.sidebarToggleMenuButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sidebarToggleMenuButton.ForeColor = System.Drawing.Color.White;
            this.sidebarToggleMenuButton.Icon = global::批量导入程序重制版.Properties.Resources.登录__1_;
            this.sidebarToggleMenuButton.IconRatio = 0.8F;
            this.sidebarToggleMenuButton.Location = new System.Drawing.Point(0, 624);
            this.sidebarToggleMenuButton.Name = "sidebarToggleMenuButton";
            this.sidebarToggleMenuButton.Size = new System.Drawing.Size(176, 44);
            this.sidebarToggleMenuButton.TabIndex = 14;
            this.sidebarToggleMenuButton.Text = "收起菜单";
            this.sidebarToggleMenuButton.Click += new System.EventHandler(this.mainPage_SidebarToggleButton_Click);
            // 
            // button1
            // 
            this.button1.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.button1.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.button1.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.button1.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Icon = global::批量导入程序重制版.Properties.Resources.功能菜单信息;
            this.button1.IconRatio = 0.9F;
            this.button1.Location = new System.Drawing.Point(0, 5);
            this.button1.Name = "button1";
            this.button1.Radius = 8;
            this.button1.Size = new System.Drawing.Size(176, 49);
            this.button1.TabIndex = 15;
            this.button1.Text = "功能菜单 ▤";
            // 
            // logoutButton
            // 
            this.logoutButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(108)))), ((int)(((byte)(83)))));
            this.logoutButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.logoutButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.logoutButton.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.logoutButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logoutButton.ForeColor = System.Drawing.Color.White;
            this.logoutButton.Icon = global::批量导入程序重制版.Properties.Resources.用户退出;
            this.logoutButton.IconRatio = 0.8F;
            this.logoutButton.Location = new System.Drawing.Point(0, 269);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Radius = 8;
            this.logoutButton.Size = new System.Drawing.Size(176, 44);
            this.logoutButton.TabIndex = 14;
            this.logoutButton.Text = "注销登录";
            this.logoutButton.Click += new System.EventHandler(this.mainPage_LogoutButton_Click);
            // 
            // configMenuButton
            // 
            this.configMenuButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(108)))), ((int)(((byte)(83)))));
            this.configMenuButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.configMenuButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.configMenuButton.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.configMenuButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.configMenuButton.ForeColor = System.Drawing.Color.White;
            this.configMenuButton.Icon = global::批量导入程序重制版.Properties.Resources.菜单_配置;
            this.configMenuButton.IconRatio = 0.8F;
            this.configMenuButton.Location = new System.Drawing.Point(0, 225);
            this.configMenuButton.Name = "configMenuButton";
            this.configMenuButton.Radius = 8;
            this.configMenuButton.Size = new System.Drawing.Size(176, 44);
            this.configMenuButton.TabIndex = 13;
            this.configMenuButton.Text = "配置参数";
            this.configMenuButton.Click += new System.EventHandler(this.mainPage_ConfigMenuButton_Click);
            // 
            // functionPanelMenuButton
            // 
            this.functionPanelMenuButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(108)))), ((int)(((byte)(83)))));
            this.functionPanelMenuButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.functionPanelMenuButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.functionPanelMenuButton.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.functionPanelMenuButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.functionPanelMenuButton.ForeColor = System.Drawing.Color.White;
            this.functionPanelMenuButton.Icon = global::批量导入程序重制版.Properties.Resources.菜单_功能;
            this.functionPanelMenuButton.IconRatio = 0.8F;
            this.functionPanelMenuButton.Location = new System.Drawing.Point(0, 181);
            this.functionPanelMenuButton.Name = "functionPanelMenuButton";
            this.functionPanelMenuButton.Radius = 8;
            this.functionPanelMenuButton.Size = new System.Drawing.Size(176, 44);
            this.functionPanelMenuButton.TabIndex = 12;
            this.functionPanelMenuButton.Text = "功能面板";
            this.functionPanelMenuButton.Click += new System.EventHandler(this.mainPage_FunctionPanelMenuButton_Click);
            // 
            // uploadLogMenuButton
            // 
            this.uploadLogMenuButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(108)))), ((int)(((byte)(83)))));
            this.uploadLogMenuButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.uploadLogMenuButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.uploadLogMenuButton.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.uploadLogMenuButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uploadLogMenuButton.ForeColor = System.Drawing.Color.White;
            this.uploadLogMenuButton.Icon = global::批量导入程序重制版.Properties.Resources.菜单_日志;
            this.uploadLogMenuButton.IconRatio = 0.8F;
            this.uploadLogMenuButton.Location = new System.Drawing.Point(0, 137);
            this.uploadLogMenuButton.Name = "uploadLogMenuButton";
            this.uploadLogMenuButton.Radius = 8;
            this.uploadLogMenuButton.Size = new System.Drawing.Size(176, 44);
            this.uploadLogMenuButton.TabIndex = 11;
            this.uploadLogMenuButton.Text = "程序日志";
            this.uploadLogMenuButton.Click += new System.EventHandler(this.mainPage_UploadLogMenuButton_Click);
            // 
            // importDataMenuButton
            // 
            this.importDataMenuButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(108)))), ((int)(((byte)(83)))));
            this.importDataMenuButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.importDataMenuButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.importDataMenuButton.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.importDataMenuButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.importDataMenuButton.ForeColor = System.Drawing.Color.White;
            this.importDataMenuButton.Icon = global::批量导入程序重制版.Properties.Resources.导入;
            this.importDataMenuButton.IconRatio = 0.8F;
            this.importDataMenuButton.Location = new System.Drawing.Point(0, 93);
            this.importDataMenuButton.Name = "importDataMenuButton";
            this.importDataMenuButton.Radius = 8;
            this.importDataMenuButton.Size = new System.Drawing.Size(176, 44);
            this.importDataMenuButton.TabIndex = 10;
            this.importDataMenuButton.Text = "导入数据";
            this.importDataMenuButton.Click += new System.EventHandler(this.mainPage_ImportDataMenuButton_Click);
            // 
            // homeMenuButton
            // 
            this.homeMenuButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(108)))), ((int)(((byte)(83)))));
            this.homeMenuButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.homeMenuButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.homeMenuButton.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.homeMenuButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.homeMenuButton.ForeColor = System.Drawing.Color.White;
            this.homeMenuButton.Icon = global::批量导入程序重制版.Properties.Resources.menu_home;
            this.homeMenuButton.IconRatio = 0.8F;
            this.homeMenuButton.Location = new System.Drawing.Point(0, 49);
            this.homeMenuButton.Name = "homeMenuButton";
            this.homeMenuButton.Radius = 8;
            this.homeMenuButton.Size = new System.Drawing.Size(176, 44);
            this.homeMenuButton.TabIndex = 9;
            this.homeMenuButton.Text = "程序主页";
            this.homeMenuButton.Click += new System.EventHandler(this.mainPage_HomeMenuButton_Click);
            // 
            // brandSubLabel
            // 
            this.brandSubLabel.AutoSize = true;
            this.brandSubLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.brandSubLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(135)))), ((int)(((byte)(147)))));
            this.brandSubLabel.Location = new System.Drawing.Point(14, 15);
            this.brandSubLabel.Name = "brandSubLabel";
            this.brandSubLabel.Size = new System.Drawing.Size(220, 19);
            this.brandSubLabel.TabIndex = 2;
            this.brandSubLabel.Text = "MUYANG ● 批量导入程序重制版";
            // 
            // contentHost
            // 
            this.contentHost.BackColor = System.Drawing.Color.White;
            this.contentHost.Controls.Add(this.pageFramePanel);
            this.contentHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentHost.Location = new System.Drawing.Point(208, 0);
            this.contentHost.Name = "contentHost";
            this.contentHost.Padding = new System.Windows.Forms.Padding(8, 24, 24, 24);
            this.contentHost.Size = new System.Drawing.Size(972, 728);
            this.contentHost.TabIndex = 0;
            // 
            // pageFramePanel
            // 
            this.pageFramePanel.Back = System.Drawing.Color.White;
            this.pageFramePanel.BackColor = System.Drawing.Color.White;
            this.pageFramePanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(233)))), ((int)(((byte)(238)))));
            this.pageFramePanel.BorderWidth = 1F;
            this.pageFramePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageFramePanel.Location = new System.Drawing.Point(8, 24);
            this.pageFramePanel.Name = "pageFramePanel";
            this.pageFramePanel.Padding = new System.Windows.Forms.Padding(1);
            this.pageFramePanel.Radius = 10;
            this.pageFramePanel.Size = new System.Drawing.Size(940, 680);
            this.pageFramePanel.TabIndex = 0;
            // 
            // workspacePanel
            // 
            this.workspacePanel.BackColor = System.Drawing.Color.White;
            this.workspacePanel.Controls.Add(this.contentHost);
            this.workspacePanel.Controls.Add(this.sidebarPanel);
            this.workspacePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspacePanel.Location = new System.Drawing.Point(0, 32);
            this.workspacePanel.Name = "workspacePanel";
            this.workspacePanel.Size = new System.Drawing.Size(1180, 728);
            this.workspacePanel.TabIndex = 0;
            // 
            // windowBar
            // 
            this.windowBar.BackColor = System.Drawing.Color.White;
            this.windowBar.Controls.Add(this.brandSubLabel);
            this.windowBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowBar.Location = new System.Drawing.Point(0, 0);
            this.windowBar.Name = "windowBar";
            this.windowBar.ShowButton = true;
            this.windowBar.Size = new System.Drawing.Size(1180, 32);
            this.windowBar.TabIndex = 1;
            // 
            // frmMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.workspacePanel);
            this.Controls.Add(this.windowBar);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Name = "frmMainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量导入程序重制版";
            this.sidebarPanel.ResumeLayout(false);
            this.navigationGroupPanel.ResumeLayout(false);
            this.contentHost.ResumeLayout(false);
            this.workspacePanel.ResumeLayout(false);
            this.windowBar.ResumeLayout(false);
            this.windowBar.PerformLayout();
            this.ResumeLayout(false);

        }

        private AntdUI.Button configMenuButton;
        private AntdUI.Button functionPanelMenuButton;
        private AntdUI.Button uploadLogMenuButton;
        private AntdUI.Button importDataMenuButton;
        private AntdUI.Button homeMenuButton;
        private AntdUI.Button sidebarToggleMenuButton;
        private AntdUI.Button logoutButton;
        private AntdUI.Button button1;
    }
}
