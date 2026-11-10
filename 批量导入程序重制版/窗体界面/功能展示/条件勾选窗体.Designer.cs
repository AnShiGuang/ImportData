namespace 批量导入程序重制版
{
    partial class 条件勾选窗体
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt项目1 = new System.Windows.Forms.TextBox();
            this.txt项目2 = new System.Windows.Forms.TextBox();
            this.txt项目3 = new System.Windows.Forms.TextBox();
            this.txt项目4 = new System.Windows.Forms.TextBox();
            this.txt项目5 = new System.Windows.Forms.TextBox();
            this.txt项目6 = new System.Windows.Forms.TextBox();
            this.txt单位名称 = new System.Windows.Forms.TextBox();
            this.btn确定 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Size = new System.Drawing.Size(400, 21);
            this.label1.Text = "请选择要勾选数据的筛查条件：";
            //
            // label2 / txt项目1
            //
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Size = new System.Drawing.Size(240, 21);
            this.label2.Text = "要勾选的项目：1 （关键字也可以）";
            this.txt项目1.Location = new System.Drawing.Point(300, 55);
            this.txt项目1.Size = new System.Drawing.Size(500, 26);
            //
            // label3 / txt项目2
            //
            this.label3.Location = new System.Drawing.Point(12, 98);
            this.label3.Size = new System.Drawing.Size(120, 21);
            this.label3.Text = "要勾选的项目：2";
            this.txt项目2.Location = new System.Drawing.Point(300, 95);
            this.txt项目2.Size = new System.Drawing.Size(500, 26);
            //
            // label4 / txt项目3
            //
            this.label4.Location = new System.Drawing.Point(12, 138);
            this.label4.Size = new System.Drawing.Size(120, 21);
            this.label4.Text = "要勾选的项目：3";
            this.txt项目3.Location = new System.Drawing.Point(300, 135);
            this.txt项目3.Size = new System.Drawing.Size(500, 26);
            //
            // label5 / txt项目4
            //
            this.label5.Location = new System.Drawing.Point(12, 178);
            this.label5.Size = new System.Drawing.Size(120, 21);
            this.label5.Text = "要勾选的项目：4";
            this.txt项目4.Location = new System.Drawing.Point(300, 175);
            this.txt项目4.Size = new System.Drawing.Size(500, 26);
            //
            // label6 / txt项目5
            //
            this.label6.Location = new System.Drawing.Point(12, 218);
            this.label6.Size = new System.Drawing.Size(120, 21);
            this.label6.Text = "要勾选的项目：5";
            this.txt项目5.Location = new System.Drawing.Point(300, 215);
            this.txt项目5.Size = new System.Drawing.Size(500, 26);
            //
            // label7 / txt项目6
            //
            this.label7.Location = new System.Drawing.Point(12, 258);
            this.label7.Size = new System.Drawing.Size(120, 21);
            this.label7.Text = "要勾选的项目：6";
            this.txt项目6.Location = new System.Drawing.Point(300, 255);
            this.txt项目6.Size = new System.Drawing.Size(500, 26);
            //
            // label8 / txt单位名称
            //
            this.label8.Location = new System.Drawing.Point(12, 305);
            this.label8.Size = new System.Drawing.Size(790, 21);
            this.label8.Text = "要勾选的单位名： （如果选了上面的项目，只能查出该单位存在这些项目的人）";
            this.txt单位名称.Location = new System.Drawing.Point(300, 335);
            this.txt单位名称.Size = new System.Drawing.Size(500, 26);
            //
            // btn确定
            //
            this.btn确定.Location = new System.Drawing.Point(700, 400);
            this.btn确定.Size = new System.Drawing.Size(100, 35);
            this.btn确定.Text = "确定勾选";
            this.btn确定.Click += new System.EventHandler(this.btn确定_Click);
            //
            // 条件勾选窗体
            //
            this.ClientSize = new System.Drawing.Size(833, 454);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt项目1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt项目2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt项目3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt项目4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt项目5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt项目6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt单位名称);
            this.Controls.Add(this.btn确定);
            this.Name = "条件勾选窗体";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "条件勾选窗体";
            this.Load += new System.EventHandler(this.frm条件勾选_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt项目1;
        private System.Windows.Forms.TextBox txt项目2;
        private System.Windows.Forms.TextBox txt项目3;
        private System.Windows.Forms.TextBox txt项目4;
        private System.Windows.Forms.TextBox txt项目5;
        private System.Windows.Forms.TextBox txt项目6;
        private System.Windows.Forms.TextBox txt单位名称;
        private System.Windows.Forms.Button btn确定;
    }
}
