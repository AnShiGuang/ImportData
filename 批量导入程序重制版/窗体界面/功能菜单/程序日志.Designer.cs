namespace 批量导入程序重制版
{
    partial class 程序日志
    {
        private AntdUI.Label titleLabel;
        private AntdUI.Label descriptionLabel;
        private AntdUI.Radio allLogsRadio;
        private AntdUI.Radio programLogsRadio;
        private AntdUI.Radio uploadLogsRadio;
        private AntdUI.Button refreshButton;
        private AntdUI.Button functionPanelButton;
        private AntdUI.Label statusLabel;
        private AntdUI.Table logTable;

        private void InitializeComponent()
        {
            this.logTable = new AntdUI.Table();
            this.statusLabel = new AntdUI.Label();
            this.refreshButton = new AntdUI.Button();
            this.functionPanelButton = new AntdUI.Button();
            this.uploadLogsRadio = new AntdUI.Radio();
            this.programLogsRadio = new AntdUI.Radio();
            this.allLogsRadio = new AntdUI.Radio();
            this.descriptionLabel = new AntdUI.Label();
            this.titleLabel = new AntdUI.Label();
            this.SuspendLayout();
            // 
            // logTable
            // 
            this.logTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logTable.AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
            this.logTable.BackColor = System.Drawing.Color.White;
            this.logTable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(233)))), ((int)(((byte)(238)))));
            this.logTable.Bordered = true;
            this.logTable.EmptyText = "暂无日志记录";
            this.logTable.Location = new System.Drawing.Point(32, 100);
            this.logTable.Name = "logTable";
            this.logTable.Radius = 8;
            this.logTable.RowHeightHeader = 38;
            this.logTable.Size = new System.Drawing.Size(856, 538);
            this.logTable.TabIndex = 7;
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(123)))), ((int)(((byte)(134)))));
            this.statusLabel.Location = new System.Drawing.Point(32, 648);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(700, 20);
            this.statusLabel.TabIndex = 6;
            this.statusLabel.Text = "准备加载日志。";
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.refreshButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(157)))), ((int)(((byte)(124)))));
            this.refreshButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.Location = new System.Drawing.Point(796, 60);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(92, 34);
            this.refreshButton.TabIndex = 5;
            this.refreshButton.Text = "刷新";
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // functionPanelButton
            // 
            this.functionPanelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.functionPanelButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.functionPanelButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(157)))), ((int)(((byte)(124)))));
            this.functionPanelButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.functionPanelButton.ForeColor = System.Drawing.Color.White;
            this.functionPanelButton.Location = new System.Drawing.Point(680, 60);
            this.functionPanelButton.Name = "functionPanelButton";
            this.functionPanelButton.Size = new System.Drawing.Size(104, 34);
            this.functionPanelButton.TabIndex = 8;
            this.functionPanelButton.Text = "操作文档";
            // 
            // uploadLogsRadio
            // 
            this.uploadLogsRadio.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.uploadLogsRadio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.uploadLogsRadio.Location = new System.Drawing.Point(216, 60);
            this.uploadLogsRadio.Name = "uploadLogsRadio";
            this.uploadLogsRadio.Size = new System.Drawing.Size(86, 34);
            this.uploadLogsRadio.TabIndex = 4;
            this.uploadLogsRadio.Text = "上传日志";
            // 
            // programLogsRadio
            // 
            this.programLogsRadio.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.programLogsRadio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.programLogsRadio.Location = new System.Drawing.Point(124, 60);
            this.programLogsRadio.Name = "programLogsRadio";
            this.programLogsRadio.Size = new System.Drawing.Size(86, 34);
            this.programLogsRadio.TabIndex = 3;
            this.programLogsRadio.Text = "程序日志";
            // 
            // allLogsRadio
            // 
            this.allLogsRadio.Checked = true;
            this.allLogsRadio.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.allLogsRadio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.allLogsRadio.Location = new System.Drawing.Point(32, 60);
            this.allLogsRadio.Name = "allLogsRadio";
            this.allLogsRadio.Size = new System.Drawing.Size(86, 34);
            this.allLogsRadio.TabIndex = 2;
            this.allLogsRadio.Text = "全部日志";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(123)))), ((int)(((byte)(134)))));
            this.descriptionLabel.Location = new System.Drawing.Point(136, 32);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(353, 22);
            this.descriptionLabel.TabIndex = 1;
            this.descriptionLabel.Text = "查看程序操作与数据上传记录。选择记录可查看完整详情。";
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.titleLabel.Location = new System.Drawing.Point(28, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(260, 34);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "操作日志";
            // 
            // 程序日志
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(918, 680);
            this.Controls.Add(this.logTable);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.functionPanelButton);
            this.Controls.Add(this.uploadLogsRadio);
            this.Controls.Add(this.programLogsRadio);
            this.Controls.Add(this.allLogsRadio);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "程序日志";
            this.Padding = new System.Windows.Forms.Padding(28);
            this.Text = "操作日志";
            this.ResumeLayout(false);

        }
    }
}
