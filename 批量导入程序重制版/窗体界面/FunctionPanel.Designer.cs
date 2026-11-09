namespace 批量导入程序重制版
{
    partial class FunctionPanel
    {
        private System.Windows.Forms.WebBrowser manualBrowser;
        private void InitializeComponent()
        {
            this.manualBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            this.manualBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manualBrowser.Location = new System.Drawing.Point(28, 28);
            this.manualBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.manualBrowser.Name = "manualBrowser";
            this.manualBrowser.ScriptErrorsSuppressed = true;
            this.manualBrowser.Size = new System.Drawing.Size(862, 624);
            this.manualBrowser.TabIndex = 0;
            this.Text = "操作文档";
            this.ClientSize = new System.Drawing.Size(918, 680);
            this.BackColor = System.Drawing.Color.White;
            this.Padding = new System.Windows.Forms.Padding(28);
            this.Controls.Add(this.manualBrowser);
            this.Name = "FunctionPanel";
            this.ResumeLayout(false);
        }
    }
}
