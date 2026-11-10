using System;
using System.Linq;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    public sealed partial class 功能面板 : 页面基类
    {
        public event EventHandler LogoutRequested;
        public event EventHandler LogQueryRequested;
        public event EventHandler GlobalConfigRequested;
        public event EventHandler ResetConfigRequested;
        public event EventHandler AutostartRequested;
        public event EventHandler DbQueryRequested;
        public event EventHandler RawQueryRequested;
        public event EventHandler AutoImportRequested;
        public event EventHandler 接苯放射Requested;
        public event EventHandler 条件勾选Requested;
        public event EventHandler 项目对应Requested;

        public 功能面板() : this(true) { }

        public 功能面板(bool isAdmin)
        {
            InitializeComponent();
            SetAdminMode(isAdmin);
            应用按钮样式();
        }

        /// <summary>
        /// 统一功能面板按钮配色：白底 + 青绿描边 + 深青文字，避免与背景糊在一起、过亮。
        /// </summary>
        private void 应用按钮样式()
        {
            var 主色 = System.Drawing.Color.FromArgb(35, 145, 112);    // #239170 青绿
            var 文字 = System.Drawing.Color.FromArgb(28, 110, 88);     // 深青文字
            var 悬停 = System.Drawing.Color.FromArgb(233, 245, 240);   // 极浅青
            var 按下 = System.Drawing.Color.FromArgb(215, 238, 231);

            foreach (AntdUI.Button btn in panel1.Controls.OfType<AntdUI.Button>())
            {
                btn.IsLink = false;
                btn.Radius = 6;
                btn.BorderWidth = 1F;
                btn.WaveSize = 0;
                btn.DefaultBack = System.Drawing.Color.White;
                btn.DefaultBorderColor = 主色;
                btn.BackColor = System.Drawing.Color.White;
                btn.BackHover = 悬停;
                btn.BackActive = 按下;
                btn.ForeColor = 文字;
            }

            panel1.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
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
            new 标识配置参数().ShowDialog(this);
        }

        private void btnUpdateConfig_Click(object sender, EventArgs e)
        {
            new 标识配置参数("Update").ShowDialog(this);
        }

        private void btnDeleteConfig_Click(object sender, EventArgs e)
        {
            new 标识配置参数("Delete").ShowDialog(this);
        }

        private void btnEnabledConfig_Click(object sender, EventArgs e)
        {
            new 标识配置参数("Enabled").ShowDialog(this);
        }

        private void btnUserOut_Click(object sender, EventArgs e)
        {
            LogoutRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnAutoImport_Click(object sender, EventArgs e)
        {
            AutoImportRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnRawQuery_Click(object sender, EventArgs e)
        {
            RawQueryRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnDbQuery_Click(object sender, EventArgs e)
        {
            DbQueryRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnResetConfig_Click(object sender, EventArgs e)
        {
            ResetConfigRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnGlobalConfig_Click(object sender, EventArgs e)
        {
            GlobalConfigRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnAutostart_Click(object sender, EventArgs e)
        {
            AutostartRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnLogQuery_Click(object sender, EventArgs e)
        {
            LogQueryRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btn接苯放射_Click(object sender, EventArgs e)
        {
            接苯放射Requested?.Invoke(this, EventArgs.Empty);
        }

        private void btn条件勾选_Click(object sender, EventArgs e)
        {
            条件勾选Requested?.Invoke(this, EventArgs.Empty);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            项目对应Requested?.Invoke(this, EventArgs.Empty);
        }
    }
}
