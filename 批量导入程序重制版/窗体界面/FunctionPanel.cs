using System;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    public sealed partial class FunctionPanel : PageFormBase
    {
        public event EventHandler LogoutRequested;

        public FunctionPanel() : this(true) { }

        public FunctionPanel(bool isAdmin)
        {
            InitializeComponent();
            SetAdminMode(isAdmin);
        }

        private void SetAdminMode(bool isAdmin)
        {
            btnCreateConfig.Enabled = isAdmin;
            btnUpdateConfig.Enabled = isAdmin;
            btnDeleteConfig.Enabled = isAdmin;
            btnEnabledConfig.Enabled = isAdmin;
        }

        private void btnCreateConfig_Click(object sender, EventArgs e)
        {
            new FormConfig().ShowDialog(this);
        }

        private void btnUpdateConfig_Click(object sender, EventArgs e)
        {
            new FormConfig("Update").ShowDialog(this);
        }

        private void btnDeleteConfig_Click(object sender, EventArgs e)
        {
            new FormConfig("Delete").ShowDialog(this);
        }

        private void btnEnabledConfig_Click(object sender, EventArgs e)
        {
            new FormConfig("Enabled").ShowDialog(this);
        }

        private void btnUserOut_Click(object sender, EventArgs e)
        {
            LogoutRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
