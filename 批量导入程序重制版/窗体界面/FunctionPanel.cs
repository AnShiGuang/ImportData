using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    public sealed partial class FunctionPanel : PageFormBase
    {
        public FunctionPanel()
        {
            InitializeComponent();
            Load += FunctionPanel_Load;
        }

        private void FunctionPanel_Load(object sender, System.EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            var documentPath = Path.Combine(Application.StartupPath, "操作文档", "操作手册.html");
            if (!File.Exists(documentPath))
            {
                MessageBox.Show(this, "未找到操作手册文件：" + documentPath, "操作文档", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            manualBrowser.Navigate(documentPath);
        }
    }
}
