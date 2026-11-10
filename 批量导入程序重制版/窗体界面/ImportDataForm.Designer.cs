namespace 批量导入程序重制版
{
    partial class ImportDataForm
    {
        private System.Windows.Forms.Label examLabel;
        private System.Windows.Forms.Label applicationLabel;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.Label endDateLabel;
        private AntdUI.Input examInput;
        private AntdUI.Input applicationInput;
        private AntdUI.DatePicker startDatePicker;
        private AntdUI.DatePicker endDatePicker;
        private AntdUI.Button queryDataButton;
        private System.Windows.Forms.Label resultCountLabel;
        private AntdUI.Table peopleGrid;
        private AntdUI.Checkbox selectAllCheckBox;
        private AntdUI.Button uploadButton;
        private System.Windows.Forms.Label statusLabel;

        private void InitializeComponent()
        {
            this.statusLabel = new System.Windows.Forms.Label();
            this.peopleGrid = new AntdUI.Table();
            this.selectAllCheckBox = new AntdUI.Checkbox();
            this.resultCountLabel = new System.Windows.Forms.Label();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.startDatePicker = new AntdUI.DatePicker();
            this.endDatePicker = new AntdUI.DatePicker();
            this.applicationLabel = new System.Windows.Forms.Label();
            this.examInput = new AntdUI.Input();
            this.applicationInput = new AntdUI.Input();
            this.examLabel = new System.Windows.Forms.Label();
            this.queryDataButton = new AntdUI.Button();
            this.uploadButton = new AntdUI.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.selSystemId = new AntdUI.Select();
            this.label2 = new System.Windows.Forms.Label();
            this.peopleGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(116)))), ((int)(((byte)(128)))));
            this.statusLabel.Location = new System.Drawing.Point(28, 648);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(161, 12);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "请设置查询条件或选择文件。";
            // 
            // peopleGrid
            // 
            this.peopleGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.peopleGrid.AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
            this.peopleGrid.BackColor = System.Drawing.Color.White;
            this.peopleGrid.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(233)))), ((int)(((byte)(238)))));
            this.peopleGrid.Bordered = true;
            this.peopleGrid.ColumnFont = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.peopleGrid.ColumnFore = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.peopleGrid.Controls.Add(this.selectAllCheckBox);
            this.peopleGrid.EmptyText = "暂无人员数据";
            this.peopleGrid.Location = new System.Drawing.Point(12, 120);
            this.peopleGrid.Name = "peopleGrid";
            this.peopleGrid.Radius = 8;
            this.peopleGrid.RowHeightHeader = 36;
            this.peopleGrid.Size = new System.Drawing.Size(923, 525);
            this.peopleGrid.TabIndex = 5;
            // 
            // selectAllCheckBox
            // 
            this.selectAllCheckBox.BackColor = System.Drawing.Color.White;
            this.selectAllCheckBox.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.selectAllCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.selectAllCheckBox.Location = new System.Drawing.Point(28, 9);
            this.selectAllCheckBox.Name = "selectAllCheckBox";
            this.selectAllCheckBox.Size = new System.Drawing.Size(17, 17);
            this.selectAllCheckBox.TabIndex = 12;
            this.selectAllCheckBox.Visible = false;
            // 
            // resultCountLabel
            // 
            this.resultCountLabel.AutoSize = true;
            this.resultCountLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.resultCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.resultCountLabel.Location = new System.Drawing.Point(72, 97);
            this.resultCountLabel.Name = "resultCountLabel";
            this.resultCountLabel.Size = new System.Drawing.Size(71, 17);
            this.resultCountLabel.TabIndex = 6;
            this.resultCountLabel.Text = "查询到 0 人";
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.startDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.startDateLabel.Location = new System.Drawing.Point(624, 11);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(56, 17);
            this.startDateLabel.TabIndex = 3;
            this.startDateLabel.Text = "开始日期";
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.endDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.endDateLabel.Location = new System.Drawing.Point(783, 11);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(56, 17);
            this.endDateLabel.TabIndex = 4;
            this.endDateLabel.Text = "结束日期";
            // 
            // startDatePicker
            // 
            this.startDatePicker.BackColor = System.Drawing.Color.White;
            this.startDatePicker.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.startDatePicker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.startDatePicker.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.startDatePicker.Location = new System.Drawing.Point(623, 31);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Radius = 7;
            this.startDatePicker.Size = new System.Drawing.Size(153, 34);
            this.startDatePicker.TabIndex = 8;
            // 
            // endDatePicker
            // 
            this.endDatePicker.BackColor = System.Drawing.Color.White;
            this.endDatePicker.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.endDatePicker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.endDatePicker.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.endDatePicker.Location = new System.Drawing.Point(782, 31);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Radius = 7;
            this.endDatePicker.Size = new System.Drawing.Size(153, 34);
            this.endDatePicker.TabIndex = 9;
            // 
            // applicationLabel
            // 
            this.applicationLabel.AutoSize = true;
            this.applicationLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.applicationLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.applicationLabel.Location = new System.Drawing.Point(307, 11);
            this.applicationLabel.Name = "applicationLabel";
            this.applicationLabel.Size = new System.Drawing.Size(56, 17);
            this.applicationLabel.TabIndex = 2;
            this.applicationLabel.Text = "申请单号";
            // 
            // examInput
            // 
            this.examInput.BackColor = System.Drawing.Color.White;
            this.examInput.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.examInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.examInput.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(184)))), ((int)(((byte)(177)))));
            this.examInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.examInput.Location = new System.Drawing.Point(465, 31);
            this.examInput.Name = "examInput";
            this.examInput.PlaceholderText = "请输入体检编号";
            this.examInput.Radius = 7;
            this.examInput.Size = new System.Drawing.Size(153, 34);
            this.examInput.TabIndex = 6;
            // 
            // 
            // applicationInput
            // 
            this.applicationInput.BackColor = System.Drawing.Color.White;
            this.applicationInput.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.applicationInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.applicationInput.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(184)))), ((int)(((byte)(177)))));
            this.applicationInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.applicationInput.Location = new System.Drawing.Point(306, 31);
            this.applicationInput.Name = "applicationInput";
            this.applicationInput.PlaceholderText = "请输入申请单号";
            this.applicationInput.Radius = 7;
            this.applicationInput.Size = new System.Drawing.Size(153, 34);
            this.applicationInput.TabIndex = 7;
            // 
            // examLabel
            // 
            this.examLabel.AutoSize = true;
            this.examLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.examLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.examLabel.Location = new System.Drawing.Point(466, 11);
            this.examLabel.Name = "examLabel";
            this.examLabel.Size = new System.Drawing.Size(56, 17);
            this.examLabel.TabIndex = 1;
            this.examLabel.Text = "体检编号";
            // 
            // 
            // queryDataButton
            // 
            this.queryDataButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.queryDataButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(157)))), ((int)(((byte)(124)))));
            this.queryDataButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.queryDataButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.queryDataButton.ForeColor = System.Drawing.Color.White;
            this.queryDataButton.Icon = global::批量导入程序重制版.Properties.Resources.BtnSearch;
            this.queryDataButton.IconRatio = 0.9F;
            this.queryDataButton.Location = new System.Drawing.Point(623, 71);
            this.queryDataButton.Name = "queryDataButton";
            this.queryDataButton.Size = new System.Drawing.Size(153, 43);
            this.queryDataButton.TabIndex = 10;
            this.queryDataButton.Text = "查询数据";
            this.queryDataButton.Click += new System.EventHandler(this.queryDataButton_Click);
            // 
            // uploadButton
            // 
            this.uploadButton.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(126)))), ((int)(((byte)(97)))));
            this.uploadButton.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(157)))), ((int)(((byte)(124)))));
            this.uploadButton.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(145)))), ((int)(((byte)(112)))));
            this.uploadButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uploadButton.ForeColor = System.Drawing.Color.White;
            this.uploadButton.Icon = global::批量导入程序重制版.Properties.Resources.BtnUpload;
            this.uploadButton.IconRatio = 0.9F;
            this.uploadButton.Location = new System.Drawing.Point(782, 71);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(153, 43);
            this.uploadButton.TabIndex = 11;
            this.uploadButton.Text = "上传数据";
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.label1.Location = new System.Drawing.Point(10, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "人员列表";
            // 
            // selSystemId
            // 
            this.selSystemId.BackColor = System.Drawing.Color.White;
            this.selSystemId.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.selSystemId.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.selSystemId.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(157)))), ((int)(((byte)(143)))));
            this.selSystemId.Location = new System.Drawing.Point(161, 31);
            this.selSystemId.Name = "selSystemId";
            this.selSystemId.PlaceholderText = "请选择系统标识";
            this.selSystemId.Radius = 7;
            this.selSystemId.Size = new System.Drawing.Size(139, 34);
            this.selSystemId.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.label2.Location = new System.Drawing.Point(158, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "系统标识";
            // 
            // ImportDataForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(948, 678);
            this.Controls.Add(this.selSystemId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startDateLabel);
            this.Controls.Add(this.resultCountLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.endDateLabel);
            this.Controls.Add(this.peopleGrid);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.queryDataButton);
            this.Controls.Add(this.examLabel);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.applicationLabel);
            this.Controls.Add(this.applicationInput);
            this.Controls.Add(this.examInput);
            this.Name = "ImportDataForm";
            this.Padding = new System.Windows.Forms.Padding(28);
            this.Text = "上传数据";
            this.peopleGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label label1;
        private AntdUI.Select selSystemId;
        private System.Windows.Forms.Label label2;
    }
}
