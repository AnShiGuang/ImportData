namespace 批量导入程序重制版
{
    partial class 自动导入窗体
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
            this.intervalInput = new AntdUI.Input();
            this.startButton = new AntdUI.Button();
            this.stopButton = new AntdUI.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.intervalLabel = new System.Windows.Forms.Label();
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
            this.pageHeader1.Size = new System.Drawing.Size(520, 40);
            this.pageHeader1.Text = "自动导入数据";
            //
            // intervalLabel
            //
            this.intervalLabel.AutoSize = true;
            this.intervalLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.intervalLabel.ForeColor = System.Drawing.Color.FromArgb(61, 90, 110);
            this.intervalLabel.Location = new System.Drawing.Point(24, 70);
            this.intervalLabel.Name = "intervalLabel";
            this.intervalLabel.Size = new System.Drawing.Size(140, 20);
            this.intervalLabel.Text = "自动导入间隔（分钟）：";
            //
            // intervalInput
            //
            this.intervalInput.BorderActive = System.Drawing.Color.FromArgb(35, 145, 112);
            this.intervalInput.BorderHover = System.Drawing.Color.FromArgb(35, 145, 112);
            this.intervalInput.Location = new System.Drawing.Point(180, 67);
            this.intervalInput.Name = "intervalInput";
            this.intervalInput.Radius = 7;
            this.intervalInput.Size = new System.Drawing.Size(120, 34);
            this.intervalInput.Text = "30";
            this.intervalInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // startButton
            //
            this.startButton.BackColor = System.Drawing.Color.FromArgb(35, 145, 112);
            this.startButton.ForeColor = System.Drawing.Color.White;
            this.startButton.Location = new System.Drawing.Point(180, 120);
            this.startButton.Name = "startButton";
            this.startButton.Radius = 7;
            this.startButton.Size = new System.Drawing.Size(120, 38);
            this.startButton.Text = "开始自动导入";
            this.startButton.Type = AntdUI.TTypeMini.Primary;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            //
            // stopButton
            //
            this.stopButton.BorderWidth = 1F;
            this.stopButton.Location = new System.Drawing.Point(316, 120);
            this.stopButton.Name = "stopButton";
            this.stopButton.Radius = 7;
            this.stopButton.Size = new System.Drawing.Size(120, 38);
            this.stopButton.Text = "停止自动导入";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            //
            // statusLabel
            //
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(108, 123, 134);
            this.statusLabel.Location = new System.Drawing.Point(24, 180);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(132, 17);
            this.statusLabel.Text = "自动导入未运行。";
            //
            // 自动导入窗体
            //
            this.ClientSize = new System.Drawing.Size(520, 240);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.intervalLabel);
            this.Controls.Add(this.intervalInput);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.statusLabel);
            this.Name = "自动导入窗体";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "自动导入数据";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private AntdUI.PageHeader pageHeader1;
        private System.Windows.Forms.Label intervalLabel;
        private AntdUI.Input intervalInput;
        private AntdUI.Button startButton;
        private AntdUI.Button stopButton;
        private System.Windows.Forms.Label statusLabel;
    }
}
