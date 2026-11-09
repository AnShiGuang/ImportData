namespace 批量导入程序重制版
{
    partial class FunctionPanel
    {
        private System.Windows.Forms.TableLayoutPanel placeholderLayout;
        private System.Windows.Forms.Label placeholderTitle;
        private System.Windows.Forms.Label placeholderSubtitle;

        private void InitializeComponent()
        {
            this.placeholderLayout = new System.Windows.Forms.TableLayoutPanel();
            this.placeholderTitle = new System.Windows.Forms.Label();
            this.placeholderSubtitle = new System.Windows.Forms.Label();
            this.placeholderLayout.SuspendLayout();
            this.SuspendLayout();
            //
            // placeholderLayout
            //
            this.placeholderLayout.ColumnCount = 1;
            this.placeholderLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.placeholderLayout.Controls.Add(this.placeholderTitle, 0, 1);
            this.placeholderLayout.Controls.Add(this.placeholderSubtitle, 0, 2);
            this.placeholderLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.placeholderLayout.Location = new System.Drawing.Point(28, 28);
            this.placeholderLayout.Name = "placeholderLayout";
            this.placeholderLayout.RowCount = 4;
            this.placeholderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.placeholderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.placeholderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.placeholderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.placeholderLayout.Size = new System.Drawing.Size(862, 624);
            this.placeholderLayout.ResumeLayout(false);
            //
            // placeholderTitle
            //
            this.placeholderTitle.AutoSize = false;
            this.placeholderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.placeholderTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.placeholderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.placeholderTitle.Location = new System.Drawing.Point(3, 0);
            this.placeholderTitle.Name = "placeholderTitle";
            this.placeholderTitle.Size = new System.Drawing.Size(856, 44);
            this.placeholderTitle.Text = "暂未开放";
            this.placeholderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // placeholderSubtitle
            //
            this.placeholderSubtitle.AutoSize = false;
            this.placeholderSubtitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.placeholderSubtitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.placeholderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(156)))), ((int)(((byte)(171)))));
            this.placeholderSubtitle.Location = new System.Drawing.Point(3, 44);
            this.placeholderSubtitle.Name = "placeholderSubtitle";
            this.placeholderSubtitle.Size = new System.Drawing.Size(856, 28);
            this.placeholderSubtitle.Text = "该功能正在建设中，敬请期待";
            this.placeholderSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // FunctionPanel
            //
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(918, 680);
            this.Controls.Add(this.placeholderLayout);
            this.Name = "FunctionPanel";
            this.Padding = new System.Windows.Forms.Padding(28);
            this.Text = "功能面板";
            this.ResumeLayout(false);
        }
    }
}
