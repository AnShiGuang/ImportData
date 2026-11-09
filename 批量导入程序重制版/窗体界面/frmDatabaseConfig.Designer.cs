namespace 批量导入程序重制版
{
    partial class frmDatabaseConfig
    {
        private AntdUI.Label title;
        private AntdUI.Label description;
        private AntdUI.Label connectionLabel;
        private AntdUI.Input connectionInput;
        private AntdUI.Checkbox showConnectionCheckBox;
        private AntdUI.Label statusLabel;
        private AntdUI.Button testButton;
        private AntdUI.Button saveButton;

        private void InitializeComponent()
        {
            this.title = new AntdUI.Label();
            this.description = new AntdUI.Label();
            this.connectionLabel = new AntdUI.Label();
            this.connectionInput = new AntdUI.Input();
            this.showConnectionCheckBox = new AntdUI.Checkbox();
            this.statusLabel = new AntdUI.Label();
            this.testButton = new AntdUI.Button();
            this.saveButton = new AntdUI.Button();
            this.SuspendLayout();
            //
            // frmDatabaseConfig
            //
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(620, 244);
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
            this.title.Location = new System.Drawing.Point(24, 18);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(560, 28);
            this.title.Text = "数据库连接";
            //
            // description
            //
            this.description.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(123)))), ((int)(((byte)(134)))));
            this.description.Location = new System.Drawing.Point(26, 50);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(566, 24);
            this.description.Text = "登录需要连接 tjdatabase4。修改后会先测试连接，再加密保存。";
            //
            // connectionLabel
            //
            this.connectionLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.connectionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.connectionLabel.Location = new System.Drawing.Point(26, 84);
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
            this.connectionInput.Location = new System.Drawing.Point(26, 108);
            this.connectionInput.Name = "connectionInput";
            this.connectionInput.PlaceholderText = "请输入 tjdatabase4 数据库连接字符串";
            this.connectionInput.Radius = 7;
            this.connectionInput.Size = new System.Drawing.Size(566, 38);
            this.connectionInput.UseSystemPasswordChar = true;
            //
            // showConnectionCheckBox
            //
            this.showConnectionCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.showConnectionCheckBox.Location = new System.Drawing.Point(26, 154);
            this.showConnectionCheckBox.Name = "showConnectionCheckBox";
            this.showConnectionCheckBox.Size = new System.Drawing.Size(150, 24);
            this.showConnectionCheckBox.Text = "显示连接字符串";
            this.showConnectionCheckBox.CheckedChanged += new AntdUI.BoolEventHandler(this.showConnectionCheckBox_CheckedChanged);
            //
            // statusLabel
            //
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(123)))), ((int)(((byte)(134)))));
            this.statusLabel.Location = new System.Drawing.Point(180, 154);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(410, 24);
            this.statusLabel.Text = "已加载当前配置。";
            //
            // testButton
            //
            this.testButton.Location = new System.Drawing.Point(316, 190);
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
            this.saveButton.Location = new System.Drawing.Point(464, 190);
            this.saveButton.Name = "saveButton";
            this.saveButton.Radius = 7;
            this.saveButton.Size = new System.Drawing.Size(128, 38);
            this.saveButton.Text = "测试并保存";
            this.saveButton.Type = AntdUI.TTypeMini.Primary;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            //
            // frmDatabaseConfig
            //
            this.Controls.Add(this.title);
            this.Controls.Add(this.description);
            this.Controls.Add(this.connectionLabel);
            this.Controls.Add(this.connectionInput);
            this.Controls.Add(this.showConnectionCheckBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.saveButton);
            this.ResumeLayout(false);
        }
    }
}
