namespace 批量导入程序重制版
{
    partial class 项目对应窗体
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn跳过对照 = new System.Windows.Forms.Button();
            this.lbl检验条码 = new System.Windows.Forms.Label();
            this.lbl项目编码 = new System.Windows.Forms.Label();
            this.lbl项目名称 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl条码名称 = new System.Windows.Forms.Label();
            this.btn确定 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lv组合项目 = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // label1 / lbl项目名称
            //
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Size = new System.Drawing.Size(80, 21);
            this.label1.Text = "项目名称：";
            this.lbl项目名称.Location = new System.Drawing.Point(92, 18);
            this.lbl项目名称.Size = new System.Drawing.Size(220, 21);
            this.lbl项目名称.Text = "项目名称";
            //
            // label2 / lbl项目编码
            //
            this.label2.Location = new System.Drawing.Point(320, 18);
            this.label2.Size = new System.Drawing.Size(80, 21);
            this.label2.Text = "项目编码：";
            this.lbl项目编码.Location = new System.Drawing.Point(400, 18);
            this.lbl项目编码.Size = new System.Drawing.Size(150, 21);
            this.lbl项目编码.Text = "项目编码";
            //
            // label3 / lbl检验条码
            //
            this.label3.Location = new System.Drawing.Point(480, 48);
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.Text = "检验条码：";
            this.lbl检验条码.Location = new System.Drawing.Point(560, 48);
            this.lbl检验条码.Size = new System.Drawing.Size(110, 21);
            this.lbl检验条码.Text = "检验条码";
            //
            // label4 / lbl条码名称
            //
            this.label4.Location = new System.Drawing.Point(12, 48);
            this.label4.Size = new System.Drawing.Size(80, 21);
            this.label4.Text = "条码名称：";
            this.lbl条码名称.Location = new System.Drawing.Point(92, 48);
            this.lbl条码名称.Size = new System.Drawing.Size(380, 21);
            this.lbl条码名称.Text = "条码名称";
            //
            // groupBox1 / lv组合项目
            //
            this.groupBox1.Controls.Add(this.lv组合项目);
            this.groupBox1.Location = new System.Drawing.Point(12, 78);
            this.groupBox1.Size = new System.Drawing.Size(656, 380);
            this.groupBox1.Text = "组合项目";
            this.lv组合项目.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv组合项目.FullRowSelect = true;
            this.lv组合项目.GridLines = true;
            this.lv组合项目.HideSelection = false;
            this.lv组合项目.View = System.Windows.Forms.View.Details;
            this.lv组合项目.DoubleClick += new System.EventHandler(this.lv组合项目_DoubleClick);
            //
            // btn确定 / btn跳过对照
            //
            this.btn确定.Location = new System.Drawing.Point(470, 468);
            this.btn确定.Size = new System.Drawing.Size(90, 35);
            this.btn确定.Text = "  确定";
            this.btn确定.Click += new System.EventHandler(this.btn确定_Click);
            this.btn跳过对照.Location = new System.Drawing.Point(565, 468);
            this.btn跳过对照.Size = new System.Drawing.Size(90, 35);
            this.btn跳过对照.Text = "  跳过";
            this.btn跳过对照.Click += new System.EventHandler(this.btn跳过对照_Click);
            //
            // 项目对应窗体
            //
            this.ClientSize = new System.Drawing.Size(680, 520);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl项目名称);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl项目编码);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl检验条码);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl条码名称);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn确定);
            this.Controls.Add(this.btn跳过对照);
            this.Name = "项目对应窗体";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "检验项目对应";
            this.Load += new System.EventHandler(this.Form项目对应_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn跳过对照;
        private System.Windows.Forms.Label lbl检验条码;
        private System.Windows.Forms.Label lbl项目编码;
        private System.Windows.Forms.Label lbl项目名称;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl条码名称;
        private System.Windows.Forms.Button btn确定;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lv组合项目;
    }
}
