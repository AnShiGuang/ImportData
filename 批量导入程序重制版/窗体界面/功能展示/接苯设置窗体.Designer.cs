namespace 批量导入程序重制版
{
    partial class 接苯设置窗体
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
            this.cmd存盘 = new System.Windows.Forms.Button();
            this.cmb配置类型 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl标题1 = new System.Windows.Forms.Label();
            this.txt结果1上限 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt结果1下限 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt结果2下限 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt结果2上限 = new System.Windows.Forms.TextBox();
            this.lbl标题2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt结果3下限 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt结果3上限 = new System.Windows.Forms.TextBox();
            this.lbl标题3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txt结果4下限 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt结果4上限 = new System.Windows.Forms.TextBox();
            this.lbl标题4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmd退出 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb性别 = new System.Windows.Forms.ComboBox();
            this.rxt需要排除的危害因素 = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn删除配置 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            //
            // label1 / cmb配置类型
            //
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Size = new System.Drawing.Size(44, 21);
            this.label1.Text = "类型：";
            this.cmb配置类型.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb配置类型.Items.AddRange(new object[] { "接苯", "放射" });
            this.cmb配置类型.Location = new System.Drawing.Point(54, 11);
            this.cmb配置类型.Size = new System.Drawing.Size(110, 26);
            this.cmb配置类型.SelectedIndexChanged += new System.EventHandler(this.cmb配置类型_SelectedIndexChanged);
            //
            // label2 / cmb性别
            //
            this.label2.Location = new System.Drawing.Point(180, 14);
            this.label2.Size = new System.Drawing.Size(44, 21);
            this.label2.Text = "性别：";
            this.cmb性别.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb性别.Items.AddRange(new object[] { "男", "女" });
            this.cmb性别.Location = new System.Drawing.Point(222, 11);
            this.cmb性别.Size = new System.Drawing.Size(90, 26);
            this.cmb性别.SelectedIndexChanged += new System.EventHandler(this.cmb性别_SelectedIndexChanged);
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.lbl标题1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt结果1上限);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt结果1下限);
            this.groupBox1.Controls.Add(this.lbl标题2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt结果2上限);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt结果2下限);
            this.groupBox1.Controls.Add(this.lbl标题3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt结果3上限);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt结果3下限);
            this.groupBox1.Controls.Add(this.lbl标题4);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txt结果4上限);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txt结果4下限);
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
            this.groupBox1.Size = new System.Drawing.Size(480, 112);
            this.groupBox1.Text = "参考值";
            //
            // 行1
            //
            this.lbl标题1.Location = new System.Drawing.Point(12, 24);
            this.lbl标题1.Size = new System.Drawing.Size(88, 21);
            this.lbl标题1.Text = "血小板";
            this.label3.Location = new System.Drawing.Point(112, 26);
            this.label3.Size = new System.Drawing.Size(44, 21);
            this.label3.Text = "上限 :";
            this.txt结果1上限.Location = new System.Drawing.Point(156, 22);
            this.txt结果1上限.Size = new System.Drawing.Size(76, 26);
            this.label4.Location = new System.Drawing.Point(244, 26);
            this.label4.Size = new System.Drawing.Size(44, 21);
            this.label4.Text = "下限 :";
            this.txt结果1下限.Location = new System.Drawing.Point(288, 22);
            this.txt结果1下限.Size = new System.Drawing.Size(76, 26);
            //
            // 行2
            //
            this.lbl标题2.Location = new System.Drawing.Point(12, 50);
            this.lbl标题2.Size = new System.Drawing.Size(88, 21);
            this.lbl标题2.Text = "白细胞";
            this.label6.Location = new System.Drawing.Point(112, 52);
            this.label6.Size = new System.Drawing.Size(44, 21);
            this.label6.Text = "上限 :";
            this.txt结果2上限.Location = new System.Drawing.Point(156, 48);
            this.txt结果2上限.Size = new System.Drawing.Size(76, 26);
            this.label5.Location = new System.Drawing.Point(244, 52);
            this.label5.Size = new System.Drawing.Size(44, 21);
            this.label5.Text = "下限 :";
            this.txt结果2下限.Location = new System.Drawing.Point(288, 48);
            this.txt结果2下限.Size = new System.Drawing.Size(76, 26);
            //
            // 行3
            //
            this.lbl标题3.Location = new System.Drawing.Point(12, 76);
            this.lbl标题3.Size = new System.Drawing.Size(88, 21);
            this.lbl标题3.Text = "血红蛋白";
            this.label9.Location = new System.Drawing.Point(112, 78);
            this.label9.Size = new System.Drawing.Size(44, 21);
            this.label9.Text = "上限 :";
            this.txt结果3上限.Location = new System.Drawing.Point(156, 74);
            this.txt结果3上限.Size = new System.Drawing.Size(76, 26);
            this.label8.Location = new System.Drawing.Point(244, 78);
            this.label8.Size = new System.Drawing.Size(44, 21);
            this.label8.Text = "下限 :";
            this.txt结果3下限.Location = new System.Drawing.Point(288, 74);
            this.txt结果3下限.Size = new System.Drawing.Size(76, 26);
            //
            // 行4
            //
            this.lbl标题4.Location = new System.Drawing.Point(12, 102);
            this.lbl标题4.Size = new System.Drawing.Size(88, 21);
            this.lbl标题4.Text = "红细胞数";
            this.label15.Location = new System.Drawing.Point(112, 104);
            this.label15.Size = new System.Drawing.Size(44, 21);
            this.label15.Text = "上限 :";
            this.txt结果4上限.Location = new System.Drawing.Point(156, 100);
            this.txt结果4上限.Size = new System.Drawing.Size(76, 26);
            this.label14.Location = new System.Drawing.Point(244, 104);
            this.label14.Size = new System.Drawing.Size(44, 21);
            this.label14.Text = "下限 :";
            this.txt结果4下限.Location = new System.Drawing.Point(288, 100);
            this.txt结果4下限.Size = new System.Drawing.Size(76, 26);
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.rxt需要排除的危害因素);
            this.groupBox2.Location = new System.Drawing.Point(12, 158);
            this.groupBox2.Size = new System.Drawing.Size(480, 44);
            this.groupBox2.Text = "需要排除的危害因素，以分号分割，例如[苯酚;苯某;]：";
            this.rxt需要排除的危害因素.Location = new System.Drawing.Point(6, 18);
            this.rxt需要排除的危害因素.Size = new System.Drawing.Size(468, 22);
            this.rxt需要排除的危害因素.Text = "";
            //
            // 按钮
            //
            this.cmd存盘.Location = new System.Drawing.Point(12, 210);
            this.cmd存盘.Size = new System.Drawing.Size(90, 28);
            this.cmd存盘.Text = "存盘(&S)";
            this.cmd存盘.Click += new System.EventHandler(this.cmd存盘_Click);
            this.btn删除配置.Location = new System.Drawing.Point(110, 210);
            this.btn删除配置.Size = new System.Drawing.Size(100, 28);
            this.btn删除配置.Text = "删除配置(&D)";
            this.btn删除配置.Click += new System.EventHandler(this.btn删除配置_Click);
            this.cmd退出.Location = new System.Drawing.Point(392, 210);
            this.cmd退出.Size = new System.Drawing.Size(100, 28);
            this.cmd退出.Text = "退出(&E)";
            this.cmd退出.Click += new System.EventHandler(this.cmd退出_Click);
            //
            // 接苯设置窗体
            //
            this.ClientSize = new System.Drawing.Size(504, 246);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb配置类型);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb性别);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmd存盘);
            this.Controls.Add(this.btn删除配置);
            this.Controls.Add(this.cmd退出);
            this.Name = "接苯设置窗体";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "接苯放射设置";
            this.Load += new System.EventHandler(this.Form接苯放射设置_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button cmd存盘;
        private System.Windows.Forms.ComboBox cmb配置类型;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl标题1;
        private System.Windows.Forms.TextBox txt结果1上限;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt结果1下限;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt结果2下限;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt结果2上限;
        private System.Windows.Forms.Label lbl标题2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt结果3下限;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt结果3上限;
        private System.Windows.Forms.Label lbl标题3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt结果4下限;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt结果4上限;
        private System.Windows.Forms.Label lbl标题4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmd退出;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb性别;
        private System.Windows.Forms.RichTextBox rxt需要排除的危害因素;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn删除配置;
    }
}
