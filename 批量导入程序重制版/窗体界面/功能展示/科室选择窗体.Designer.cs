namespace 批量导入程序重制版
{
    partial class 科室选择窗体
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSystemName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tree1 = new AntdUI.Tree();
            this.tree2 = new AntdUI.Tree();
            this.tree3 = new AntdUI.Tree();
            this.btnSave = new AntdUI.Button();
            this.btnClose = new AntdUI.Button();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.SuspendLayout();
            //
            // lblSystemName
            //
            this.lblSystemName.AutoSize = true;
            this.lblSystemName.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.lblSystemName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblSystemName.Location = new System.Drawing.Point(56, 44);
            this.lblSystemName.Name = "lblSystemName";
            this.lblSystemName.Size = new System.Drawing.Size(107, 20);
            this.lblSystemName.TabIndex = 13;
            this.lblSystemName.Text = "当前系统标识：";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label1.Location = new System.Drawing.Point(390, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "当前已选：";
            //
            // tree1
            //
            this.tree1.Checkable = true;
            this.tree1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.tree1.Location = new System.Drawing.Point(60, 67);
            this.tree1.Name = "tree1";
            this.tree1.Size = new System.Drawing.Size(328, 348);
            this.tree1.TabIndex = 15;
            this.tree1.Text = "tree1";
            this.tree1.SelectChanged += new AntdUI.TreeSelectEventHandler(this.tree1_SelectChanged);
            //
            // tree2
            //
            this.tree2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.tree2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.tree2.Location = new System.Drawing.Point(394, 67);
            this.tree2.Name = "tree2";
            this.tree2.Size = new System.Drawing.Size(169, 299);
            this.tree2.TabIndex = 16;
            this.tree2.Text = "tree2";
            //
            // tree3
            //
            this.tree3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.tree3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.tree3.Location = new System.Drawing.Point(569, 67);
            this.tree3.Name = "tree3";
            this.tree3.Size = new System.Drawing.Size(169, 299);
            this.tree3.TabIndex = 17;
            this.tree3.Text = "tree3";
            //
            // btnSave
            //
            this.btnSave.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnSave.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnSave.BorderWidth = 1F;
            this.btnSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.IconRatio = 0.9F;
            this.btnSave.IsLink = true;
            this.btnSave.Location = new System.Drawing.Point(569, 372);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(169, 43);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnClose
            //
            this.btnClose.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnClose.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnClose.BorderWidth = 1F;
            this.btnClose.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.IconRatio = 0.9F;
            this.btnClose.IsLink = true;
            this.btnClose.Location = new System.Drawing.Point(394, 372);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(169, 43);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "暂不对应";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // pageHeader1
            //
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.MaximizeBox = false;
            this.pageHeader1.MinimizeBox = false;
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.Size = new System.Drawing.Size(800, 40);
            this.pageHeader1.TabIndex = 23;
            this.pageHeader1.Text = "科室信息对应";
            //
            // 科室选择窗体
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tree3);
            this.Controls.Add(this.tree2);
            this.Controls.Add(this.tree1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSystemName);
            this.Name = "科室选择窗体";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择科室";
            this.Load += new System.EventHandler(this.FormSelectDept_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSystemName;
        private System.Windows.Forms.Label label1;
        private AntdUI.Tree tree1;
        private AntdUI.Tree tree2;
        private AntdUI.Tree tree3;
        private AntdUI.Button btnSave;
        private AntdUI.Button btnClose;
        private AntdUI.PageHeader pageHeader1;
    }
}
