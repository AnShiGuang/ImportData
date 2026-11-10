namespace 批量导入程序重制版
{
    partial class 数据配置窗体
    {
        private AntdUI.Label title;
        private AntdUI.Button modeToggleButton;
        private System.Windows.Forms.Panel stringPanel;
        private System.Windows.Forms.Panel builderPanel;
        private AntdUI.Label description;
        private AntdUI.Label connectionLabel;
        private AntdUI.Input connectionInput;
        private AntdUI.Checkbox showConnectionCheckBox;
        private AntdUI.Label hintLabel;
        private AntdUI.Label serverLabel;
        private AntdUI.Input serverInput;
        private AntdUI.Label portLabel;
        private AntdUI.Input portInput;
        private AntdUI.Label databaseLabel;
        private AntdUI.Input databaseInput;
        private AntdUI.Label userLabel;
        private AntdUI.Input userInput;
        private AntdUI.Label passwordLabel;
        private AntdUI.Input passwordInput;
        private AntdUI.Label previewLabel;
        private AntdUI.Input previewInput;
        private AntdUI.Label statusLabel;
        private AntdUI.Button testButton;
        private AntdUI.Button saveButton;

        private void InitializeComponent()
        {
            this.title = new AntdUI.Label();
            this.modeToggleButton = new AntdUI.Button();
            this.stringPanel = new System.Windows.Forms.Panel();
            this.builderPanel = new System.Windows.Forms.Panel();
            this.description = new AntdUI.Label();
            this.connectionLabel = new AntdUI.Label();
            this.connectionInput = new AntdUI.Input();
            this.showConnectionCheckBox = new AntdUI.Checkbox();
            this.hintLabel = new AntdUI.Label();
            this.serverLabel = new AntdUI.Label();
            this.serverInput = new AntdUI.Input();
            this.portLabel = new AntdUI.Label();
            this.portInput = new AntdUI.Input();
            this.databaseLabel = new AntdUI.Label();
            this.databaseInput = new AntdUI.Input();
            this.userLabel = new AntdUI.Label();
            this.userInput = new AntdUI.Input();
            this.passwordLabel = new AntdUI.Label();
            this.passwordInput = new AntdUI.Input();
            this.previewLabel = new AntdUI.Label();
            this.previewInput = new AntdUI.Input();
            this.statusLabel = new AntdUI.Label();
            this.testButton = new AntdUI.Button();
            this.saveButton = new AntdUI.Button();
            this.stringPanel.SuspendLayout();
            this.builderPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // 数据配置窗体
            //
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(620, 680);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据库连接配置";
            //
            // title
            //
            this.title.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.title.Location = new System.Drawing.Point(24, 16);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(560, 28);
            this.title.Text = "数据库连接";
            //
            // modeToggleButton
            //
            this.modeToggleButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.modeToggleButton.Location = new System.Drawing.Point(452, 14);
            this.modeToggleButton.Name = "modeToggleButton";
            this.modeToggleButton.Radius = 7;
            this.modeToggleButton.Size = new System.Drawing.Size(144, 32);
            this.modeToggleButton.Text = "手动填写";
            this.modeToggleButton.Click += new System.EventHandler(this.modeToggleButton_Click);
            //
            // stringPanel
            //
            this.stringPanel.BackColor = System.Drawing.Color.White;
            this.stringPanel.Controls.Add(this.description);
            this.stringPanel.Controls.Add(this.connectionLabel);
            this.stringPanel.Controls.Add(this.connectionInput);
            this.stringPanel.Controls.Add(this.showConnectionCheckBox);
            this.stringPanel.Controls.Add(this.hintLabel);
            this.stringPanel.Location = new System.Drawing.Point(24, 56);
            this.stringPanel.Name = "stringPanel";
            this.stringPanel.Size = new System.Drawing.Size(572, 250);
            //
            // description
            //
            this.description.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(123)))), ((int)(((byte)(134)))));
            this.description.Location = new System.Drawing.Point(2, 0);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(566, 24);
            this.description.Text = "登录需要连接 tjdatabase4。修改后会先测试连接，再加密保存。";
            //
            // connectionLabel
            //
            this.connectionLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.connectionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.connectionLabel.Location = new System.Drawing.Point(2, 34);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(400, 20);
            this.connectionLabel.Text = "SQL Server 连接字符串";
            //
            // connectionInput
            //
            this.connectionInput.BackColor = System.Drawing.Color.White;
            this.connectionInput.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.connectionInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.connectionInput.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(184)))), ((int)(((byte)(177)))));
            this.connectionInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.connectionInput.Location = new System.Drawing.Point(2, 56);
            this.connectionInput.Name = "connectionInput";
            this.connectionInput.PlaceholderText = "请输入 tjdatabase4 数据库连接字符串";
            this.connectionInput.Radius = 7;
            this.connectionInput.Size = new System.Drawing.Size(566, 38);
            this.connectionInput.UseSystemPasswordChar = true;
            //
            // showConnectionCheckBox
            //
            this.showConnectionCheckBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.showConnectionCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.showConnectionCheckBox.Location = new System.Drawing.Point(2, 104);
            this.showConnectionCheckBox.Name = "showConnectionCheckBox";
            this.showConnectionCheckBox.Size = new System.Drawing.Size(150, 24);
            this.showConnectionCheckBox.Text = "显示连接字符串";
            this.showConnectionCheckBox.CheckedChanged += new AntdUI.BoolEventHandler(this.showConnectionCheckBox_CheckedChanged);
            //
            // hintLabel
            //
            this.hintLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hintLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(156)))), ((int)(((byte)(171)))));
            this.hintLabel.Location = new System.Drawing.Point(2, 140);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(566, 90);
            this.hintLabel.Text = "可直接粘贴完整连接字符串；也可以点右上角「手动填写」，按服务器地址、端口、数据库名、用户名和密码自动生成。";
            //
            // builderPanel
            //
            this.builderPanel.BackColor = System.Drawing.Color.White;
            this.builderPanel.Controls.Add(this.serverLabel);
            this.builderPanel.Controls.Add(this.serverInput);
            this.builderPanel.Controls.Add(this.portLabel);
            this.builderPanel.Controls.Add(this.portInput);
            this.builderPanel.Controls.Add(this.databaseLabel);
            this.builderPanel.Controls.Add(this.databaseInput);
            this.builderPanel.Controls.Add(this.userLabel);
            this.builderPanel.Controls.Add(this.userInput);
            this.builderPanel.Controls.Add(this.passwordLabel);
            this.builderPanel.Controls.Add(this.passwordInput);
            this.builderPanel.Controls.Add(this.previewLabel);
            this.builderPanel.Controls.Add(this.previewInput);
            this.builderPanel.Location = new System.Drawing.Point(24, 322);
            this.builderPanel.Name = "builderPanel";
            this.builderPanel.Size = new System.Drawing.Size(572, 250);
            //
            // serverLabel
            //
            this.serverLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.serverLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.serverLabel.Location = new System.Drawing.Point(2, 0);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(90, 20);
            this.serverLabel.Text = "服务器地址";
            //
            // serverInput
            //
            this.serverInput.BackColor = System.Drawing.Color.White;
            this.serverInput.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.serverInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.serverInput.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(184)))), ((int)(((byte)(177)))));
            this.serverInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.serverInput.Location = new System.Drawing.Point(2, 22);
            this.serverInput.Name = "serverInput";
            this.serverInput.PlaceholderText = "例如 120.55.65.152";
            this.serverInput.Radius = 7;
            this.serverInput.Size = new System.Drawing.Size(448, 32);
            this.serverInput.TextChanged += new System.EventHandler(this.BuilderField_TextChanged);
            //
            // portLabel
            //
            this.portLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.portLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.portLabel.Location = new System.Drawing.Point(462, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(60, 20);
            this.portLabel.Text = "端口";
            //
            // portInput
            //
            this.portInput.BackColor = System.Drawing.Color.White;
            this.portInput.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.portInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.portInput.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(184)))), ((int)(((byte)(177)))));
            this.portInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.portInput.Location = new System.Drawing.Point(462, 22);
            this.portInput.Name = "portInput";
            this.portInput.PlaceholderText = "6000";
            this.portInput.Radius = 7;
            this.portInput.Size = new System.Drawing.Size(106, 32);
            this.portInput.Text = "6000";
            this.portInput.TextChanged += new System.EventHandler(this.BuilderField_TextChanged);
            //
            // databaseLabel
            //
            this.databaseLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.databaseLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.databaseLabel.Location = new System.Drawing.Point(2, 60);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(90, 20);
            this.databaseLabel.Text = "数据库名";
            //
            // databaseInput
            //
            this.databaseInput.BackColor = System.Drawing.Color.White;
            this.databaseInput.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.databaseInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.databaseInput.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(184)))), ((int)(((byte)(177)))));
            this.databaseInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.databaseInput.Location = new System.Drawing.Point(2, 82);
            this.databaseInput.Name = "databaseInput";
            this.databaseInput.PlaceholderText = "tjdatabase4";
            this.databaseInput.Radius = 7;
            this.databaseInput.Size = new System.Drawing.Size(566, 32);
            this.databaseInput.Text = "tjdatabase4";
            this.databaseInput.TextChanged += new System.EventHandler(this.BuilderField_TextChanged);
            //
            // userLabel
            //
            this.userLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.userLabel.Location = new System.Drawing.Point(2, 120);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(90, 20);
            this.userLabel.Text = "用户名";
            //
            // userInput
            //
            this.userInput.BackColor = System.Drawing.Color.White;
            this.userInput.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.userInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.userInput.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(184)))), ((int)(((byte)(177)))));
            this.userInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.userInput.Location = new System.Drawing.Point(2, 142);
            this.userInput.Name = "userInput";
            this.userInput.PlaceholderText = "sa";
            this.userInput.Radius = 7;
            this.userInput.Size = new System.Drawing.Size(276, 32);
            this.userInput.TextChanged += new System.EventHandler(this.BuilderField_TextChanged);
            //
            // passwordLabel
            //
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.passwordLabel.Location = new System.Drawing.Point(290, 120);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(90, 20);
            this.passwordLabel.Text = "密码";
            //
            // passwordInput
            //
            this.passwordInput.BackColor = System.Drawing.Color.White;
            this.passwordInput.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.passwordInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.passwordInput.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(184)))), ((int)(((byte)(177)))));
            this.passwordInput.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordInput.Location = new System.Drawing.Point(290, 142);
            this.passwordInput.Name = "passwordInput";
            this.passwordInput.Radius = 7;
            this.passwordInput.Size = new System.Drawing.Size(276, 32);
            this.passwordInput.UseSystemPasswordChar = true;
            this.passwordInput.TextChanged += new System.EventHandler(this.BuilderField_TextChanged);
            //
            // previewLabel
            //
            this.previewLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.previewLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.previewLabel.Location = new System.Drawing.Point(2, 180);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(566, 20);
            this.previewLabel.Text = "连接字符串预览";
            //
            // previewInput
            //
            this.previewInput.BackColor = System.Drawing.Color.White;
            this.previewInput.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.previewInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.previewInput.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(184)))), ((int)(((byte)(177)))));
            this.previewInput.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.previewInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.previewInput.Location = new System.Drawing.Point(2, 202);
            this.previewInput.Name = "previewInput";
            this.previewInput.ReadOnly = true;
            this.previewInput.Radius = 7;
            this.previewInput.Size = new System.Drawing.Size(566, 32);
            //
            // statusLabel
            //
            this.statusLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(123)))), ((int)(((byte)(134)))));
            this.statusLabel.Location = new System.Drawing.Point(26, 590);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(566, 24);
            this.statusLabel.Text = "已加载当前配置。";
            //
            // testButton
            //
            this.testButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.testButton.Location = new System.Drawing.Point(316, 620);
            this.testButton.Name = "testButton";
            this.testButton.Radius = 7;
            this.testButton.Size = new System.Drawing.Size(128, 38);
            this.testButton.Text = "测试连接";
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            //
            // saveButton
            //
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.saveButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(464, 620);
            this.saveButton.Name = "saveButton";
            this.saveButton.Radius = 7;
            this.saveButton.Size = new System.Drawing.Size(128, 38);
            this.saveButton.Text = "测试并保存";
            this.saveButton.Type = AntdUI.TTypeMini.Primary;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            //
            // 数据配置窗体
            //
            this.Controls.Add(this.title);
            this.Controls.Add(this.modeToggleButton);
            this.Controls.Add(this.stringPanel);
            this.Controls.Add(this.builderPanel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.saveButton);
            this.stringPanel.ResumeLayout(false);
            this.builderPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
