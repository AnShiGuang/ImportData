namespace 批量导入程序重制版
{
    partial class 错误项目窗体
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
            this.components = new System.ComponentModel.Container();
            this.lbl提示 = new System.Windows.Forms.Label();
            this.cmd是 = new System.Windows.Forms.Button();
            this.cmd否 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            //
            // lbl提示
            //
            this.lbl提示.Location = new System.Drawing.Point(12, 20);
            this.lbl提示.Size = new System.Drawing.Size(490, 80);
            this.lbl提示.Text = "label1";
            //
            // cmd是
            //
            this.cmd是.Location = new System.Drawing.Point(150, 125);
            this.cmd是.Size = new System.Drawing.Size(90, 35);
            this.cmd是.Text = "是";
            this.cmd是.Click += new System.EventHandler(this.cmd是_Click);
            //
            // cmd否
            //
            this.cmd否.Location = new System.Drawing.Point(270, 125);
            this.cmd否.Size = new System.Drawing.Size(90, 35);
            this.cmd否.Text = "不是";
            this.cmd否.Click += new System.EventHandler(this.cmd否_Click);
            //
            // timer1
            //
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            //
            // 错误项目窗体
            //
            this.ClientSize = new System.Drawing.Size(516, 206);
            this.Controls.Add(this.lbl提示);
            this.Controls.Add(this.cmd是);
            this.Controls.Add(this.cmd否);
            this.Name = "错误项目窗体";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "是否对应错误项目";
            this.Load += new System.EventHandler(this.Form是否对应错误项目_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lbl提示;
        private System.Windows.Forms.Button cmd是;
        private System.Windows.Forms.Button cmd否;
        private System.Windows.Forms.Timer timer1;
    }
}
