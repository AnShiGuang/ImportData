namespace 批量导入程序重制版
{
    partial class FormTips
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
            this.lblLable = new AntdUI.Label();
            this.rxtText = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.avaPic = new AntdUI.Avatar();
            this.lblUserName = new AntdUI.Label();
            this.btnOK = new AntdUI.Button();
            this.btnExit = new AntdUI.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLable
            // 
            this.lblLable.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblLable.Location = new System.Drawing.Point(61, 33);
            this.lblLable.Name = "lblLable";
            this.lblLable.Size = new System.Drawing.Size(675, 43);
            this.lblLable.TabIndex = 0;
            this.lblLable.Text = "label1";
            // 
            // rxtText
            // 
            this.rxtText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.rxtText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rxtText.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.rxtText.Location = new System.Drawing.Point(61, 82);
            this.rxtText.Name = "rxtText";
            this.rxtText.Size = new System.Drawing.Size(675, 285);
            this.rxtText.TabIndex = 1;
            this.rxtText.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 373);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Panel2.Controls.Add(this.btnExit);
            this.splitContainer1.Size = new System.Drawing.Size(800, 77);
            this.splitContainer1.SplitterDistance = 325;
            this.splitContainer1.TabIndex = 18;
            // 
            // avaPic
            // 
            this.avaPic.BorderColor = System.Drawing.Color.DarkGray;
            this.avaPic.BorderWidth = 2F;
            this.avaPic.Location = new System.Drawing.Point(668, 83);
            this.avaPic.Name = "avaPic";
            this.avaPic.Radius = 35;
            this.avaPic.Size = new System.Drawing.Size(70, 70);
            this.avaPic.TabIndex = 19;
            this.avaPic.Text = "";
            this.avaPic.Visible = false;
            // 
            // lblUserName
            // 
            this.lblUserName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblUserName.Location = new System.Drawing.Point(627, 159);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(149, 34);
            this.lblUserName.TabIndex = 20;
            this.lblUserName.Text = "Administrator";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserName.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnOK.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.btnOK.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Icon = global::批量导入程序重制版.Properties.Resources.圆桌;
            this.btnOK.Location = new System.Drawing.Point(3, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Radius = 8;
            this.btnOK.Size = new System.Drawing.Size(139, 44);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "好的";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnExit
            // 
            this.btnExit.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnExit.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.btnExit.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Icon = global::批量导入程序重制版.Properties.Resources.关闭结束;
            this.btnExit.Location = new System.Drawing.Point(148, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Radius = 8;
            this.btnExit.Size = new System.Drawing.Size(139, 44);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "关闭";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FormTips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.avaPic);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.rxtText);
            this.Controls.Add(this.lblLable);
            this.Name = "FormTips";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormTips";
            this.Load += new System.EventHandler(this.FormTips_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Label lblLable;
        private System.Windows.Forms.RichTextBox rxtText;
        private AntdUI.Button btnExit;
        private AntdUI.Button btnOK;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private AntdUI.Avatar avaPic;
        private AntdUI.Label lblUserName;
    }
}