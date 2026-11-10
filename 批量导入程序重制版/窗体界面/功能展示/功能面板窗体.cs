using System;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    public sealed partial class 功能面板窗体 : 页面基类
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

        public 功能面板窗体() : this(true) { }

        public 功能面板窗体(bool isAdmin)
        {
            InitializeComponent();
            SetAdminMode(isAdmin);
            LayoutButtons();
            Resize += (s, e) => LayoutButtons();
        }

        /// <summary>
        /// 功能面板按钮排版：三列网格，间距可通过下面的常量调整。
        /// </summary>
        private void LayoutButtons()
        {
            // ===== 可调整的距离参数 =====
            const int left = 40;          // 左起第一个按钮的 X
            const int top = 24;           // 第一行按钮的 Y
            const int gapX = 51;          // 列间距
            const int gapY = 40;          // 行间距
            const int buttonWidth = 240;  // 按钮宽度
            const int buttonHeight = 56;  // 按钮高度

            var grid = new FunctionButtonPlacement[]
            {
                new FunctionButtonPlacement(btnCreateConfig, 0, 0),
                new FunctionButtonPlacement(btnUpdateConfig, 0, 1),
                new FunctionButtonPlacement(btnDeleteConfig, 0, 2),
                new FunctionButtonPlacement(btnEnabledConfig, 0, 3),
                new FunctionButtonPlacement(btn接苯放射, 0, 4),

                new FunctionButtonPlacement(btnAutoImport, 1, 0),
                new FunctionButtonPlacement(btnRawQuery, 1, 1),
                new FunctionButtonPlacement(btnDbQuery, 1, 2),
                new FunctionButtonPlacement(btnResetConfig, 1, 3),
                new FunctionButtonPlacement(btn条件勾选, 1, 4),

                new FunctionButtonPlacement(btnGlobalConfig, 2, 0),
                new FunctionButtonPlacement(btnAutostart, 2, 1),
                new FunctionButtonPlacement(btnLogQuery, 2, 2),
                new FunctionButtonPlacement(btnUserOut, 2, 3),
                new FunctionButtonPlacement(button1, 2, 4),
            };

            foreach (var item in grid)
            {
                item.Button.Location = new System.Drawing.Point(
                    left + item.Col * (buttonWidth + gapX),
                    top + item.Row * (buttonHeight + gapY));
                item.Button.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            }
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
            new 标识配置窗体().ShowDialog(this);
        }

        private void btnUpdateConfig_Click(object sender, EventArgs e)
        {
            new 标识配置窗体("Update").ShowDialog(this);
        }

        private void btnDeleteConfig_Click(object sender, EventArgs e)
        {
            new 标识配置窗体("Delete").ShowDialog(this);
        }

        private void btnEnabledConfig_Click(object sender, EventArgs e)
        {
            new 标识配置窗体("Enabled").ShowDialog(this);
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

        private sealed class FunctionButtonPlacement
        {
            public AntdUI.Button Button;
            public int Col;
            public int Row;

            public FunctionButtonPlacement(AntdUI.Button button, int col, int row)
            {
                Button = button;
                Col = col;
                Row = row;
            }
        }
    }
}
