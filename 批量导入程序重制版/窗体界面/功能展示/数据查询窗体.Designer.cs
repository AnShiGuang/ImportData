namespace 批量导入程序重制版
{
    partial class 数据查询窗体
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pageHeader1 = new AntdUI.PageHeader();
            this.sqlInput = new System.Windows.Forms.RichTextBox();
            this.runButton = new AntdUI.Button();
            this.resultGrid = new System.Windows.Forms.DataGridView();
            this.statusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).BeginInit();
            this.SuspendLayout();
            //
            // pageHeader1
            //
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.MaximizeBox = false;
            this.pageHeader1.MinimizeBox = false;
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(900, 40);
            this.pageHeader1.Text = "数据库查询器";
            //
            // sqlInput
            //
            this.sqlInput.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqlInput.Location = new System.Drawing.Point(12, 46);
            this.sqlInput.Name = "sqlInput";
            this.sqlInput.Size = new System.Drawing.Size(876, 120);
            this.sqlInput.Text = "";
            //
            // runButton
            //
            this.runButton.BackColor = System.Drawing.Color.FromArgb(35, 145, 112);
            this.runButton.ForeColor = System.Drawing.Color.White;
            this.runButton.Location = new System.Drawing.Point(12, 172);
            this.runButton.Name = "runButton";
            this.runButton.Radius = 7;
            this.runButton.Size = new System.Drawing.Size(120, 36);
            this.runButton.Text = "执行查询";
            this.runButton.Type = AntdUI.TTypeMini.Primary;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            //
            // resultGrid
            //
            this.resultGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.resultGrid.BackgroundColor = System.Drawing.Color.White;
            this.resultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultGrid.Location = new System.Drawing.Point(12, 216);
            this.resultGrid.Name = "resultGrid";
            this.resultGrid.Size = new System.Drawing.Size(876, 264);
            this.resultGrid.ReadOnly = true;
            this.resultGrid.AllowUserToAddRows = false;
            this.resultGrid.AllowUserToDeleteRows = false;
            this.resultGrid.RowHeadersVisible = false;
            //
            // statusLabel
            //
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(108, 123, 134);
            this.statusLabel.Location = new System.Drawing.Point(140, 182);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(100, 17);
            this.statusLabel.Text = "请输入查询语句。";
            //
            // 数据查询窗体
            //
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.sqlInput);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.resultGrid);
            this.Controls.Add(this.statusLabel);
            this.Name = "数据查询窗体";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据库查询器";
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private AntdUI.PageHeader pageHeader1;
        private System.Windows.Forms.RichTextBox sqlInput;
        private AntdUI.Button runButton;
        private System.Windows.Forms.DataGridView resultGrid;
        private System.Windows.Forms.Label statusLabel;
    }
}
