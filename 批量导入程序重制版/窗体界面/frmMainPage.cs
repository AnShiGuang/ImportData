using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    public sealed partial class frmMainPage : AntdUI.Window
    {
        private readonly string _userName;
        private readonly string _logUserName;
        private readonly bool _isAdmin;
        private AntdUI.Button[] _navigationButtons;
        private Rectangle[] _navigationButtonBounds;
        private string[] _navigationButtonTexts;
        private AntdUI.Button[] _allMenuButtons;
        private Image[] _originalIcons;
        private Size[] _originalIconSizes;
        private float[] _originalIconGaps;
        private AntdUI.TAlignMini[] _originalIconPositions;
        private bool[] _originalTextCenterHasIcons;
        private ContentAlignment[] _originalTextAlignments;
        private Padding[] _originalPaddings;
        private Image[] _legacyMenuIcons;
        private Image _collapsedToggleIcon;
        private Rectangle _toggleButtonBounds;
        private string _toggleButtonText;
        private string _toggleButtonIconSvg;
        private Image _toggleButtonIcon;
        private Size _toggleButtonIconSize;
        private float _toggleButtonIconGap;
        private AntdUI.TAlignMini _toggleButtonIconPosition;
        private bool _toggleButtonTextCenterHasIcon;
        private ContentAlignment _toggleButtonTextAlign;
        private Padding _toggleButtonPadding;
        private Timer _sidebarAnimationTimer;
        private int _sidebarAnimationTargetWidth;
        private int _sidebarExpandedWidth;
        private bool _expandedSidebarVisualsPrepared;
        private Rectangle _navigationGroupBounds;
        private int _navigationGroupBottomMargin;
        private int _toggleButtonBottomMargin;
        private Rectangle _menuTitleButtonBounds;
        private string _menuTitleButtonText;
        private Image _menuTitleButtonIcon;
        private Size _menuTitleButtonIconSize;
        private float _menuTitleButtonIconGap;
        private AntdUI.TAlignMini _menuTitleButtonIconPosition;
        private bool _menuTitleButtonTextCenterHasIcon;
        private ContentAlignment _menuTitleButtonTextAlign;
        private Padding _menuTitleButtonPadding;
        private AntdUI.Button _activeNavigationButton;
        private Rectangle _logoutButtonBounds;
        private string _logoutButtonText;
        private Image _logoutButtonIcon;
        private Size _logoutButtonIconSize;
        private float _logoutButtonIconGap;
        private AntdUI.TAlignMini _logoutButtonIconPosition;
        private bool _logoutButtonTextCenterHasIcon;
        private ContentAlignment _logoutButtonTextAlign;
        private Padding _logoutButtonPadding;
        private int _sidebarCollapseDelayTicks;
        private frmHome _homeForm;
        private ImportDataForm _importDataForm;
        private ProgramLogForm _programLogForm;
        private FunctionPanel _functionPanelForm;
        private OperationManualForm _operationManualForm;
        private frmConfig _configForm;
        private Panel _configPermissionPromptPanel;
        private FlowLayoutPanel _configPermissionPromptContent;
        private PictureBox _configPermissionPicture;
        private Label _configPermissionPrompt;
        public bool LoggedOut { get; private set; }

        private AntdUI.Button homeNavButton => homeMenuButton;
        private AntdUI.Button importNavButton => importDataMenuButton;
        private AntdUI.Button logNavButton => uploadLogMenuButton;
        private AntdUI.Button featureNavButton => functionPanelMenuButton;
        private AntdUI.Button configNavButton => configMenuButton;
        private AntdUI.Button toggleSidebarButton => sidebarToggleMenuButton;

        public frmMainPage(string userName) : this(userName, userName) { }

        public frmMainPage(string userName, string displayName)
        {
            _userName = userName;
            _logUserName = 操作日志.FormatUserName(userName, displayName);
            _isAdmin = IsAdministrator(userName);
            InitializeComponent();
            CreateNavigationButtons();
            CreateChildForms(displayName, _isAdmin);
            LayoutSidebar(false);
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                ShowChild(_homeForm);
                return;
            }
            ShowChild(_homeForm);
        }

        private void CreateChildForms(string displayName, bool isAdmin)
        {
            _homeForm = new frmHome(displayName, isAdmin);
            _importDataForm = new ImportDataForm(_logUserName);
            _importDataForm.ViewProgramLogRequested += importDataForm_ViewProgramLogRequested;
            _programLogForm = new ProgramLogForm(_logUserName);
            _programLogForm.FunctionPanelRequested += programLogForm_FunctionPanelRequested;
            _functionPanelForm = new FunctionPanel(isAdmin);
            _functionPanelForm.LogoutRequested += functionPanel_LogoutRequested;
            _operationManualForm = new OperationManualForm();
            _configForm = new frmConfig(_logUserName);
            foreach (Form child in new Form[] { _homeForm, _importDataForm, _programLogForm, _functionPanelForm, _operationManualForm, _configForm })
            {
                child.TopLevel = false;
                child.FormBorderStyle = FormBorderStyle.None;
                child.Dock = DockStyle.Fill;
                child.ShowInTaskbar = false;
                pageFramePanel.Controls.Add(child);
                child.Show();
            }

            _configPermissionPromptPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Visible = false
            };
            _configPermissionPromptContent = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };
            _configPermissionPicture = new PictureBox
            {
                Image = LoadMenuIcon("config-admin-required.png"),
                Size = new Size(220, 180),
                SizeMode = PictureBoxSizeMode.Zoom,
                Margin = Padding.Empty
            };
            _configPermissionPrompt = new Label
            {
                AutoSize = false,
                Size = new Size(220, 32),
                Text = "配置程序参数，请登录管理员账户操作！",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(108, 123, 134),
                Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular),
                Margin = new Padding(0, 14, 0, 0)
            };
            _configPermissionPromptContent.Controls.Add(_configPermissionPicture);
            _configPermissionPromptContent.Controls.Add(_configPermissionPrompt);
            _configPermissionPromptPanel.Controls.Add(_configPermissionPromptContent);
            _configPermissionPromptPanel.Resize += mainPage_ConfigPermissionPromptPanel_Resize;
            pageFramePanel.Controls.Add(_configPermissionPromptPanel);
            LayoutConfigPermissionPrompt();
        }

        private void mainPage_ConfigPermissionPromptPanel_Resize(object sender, EventArgs e)
        {
            LayoutConfigPermissionPrompt();
        }

        private void LayoutConfigPermissionPrompt()
        {
            if (_configPermissionPromptPanel == null || _configPermissionPromptContent == null) return;
            _configPermissionPromptContent.Location = new Point(
                Math.Max(0, (_configPermissionPromptPanel.ClientSize.Width - _configPermissionPromptContent.Width) / 2),
                Math.Max(0, (_configPermissionPromptPanel.ClientSize.Height - _configPermissionPromptContent.Height) / 2));
        }

        private static bool IsAdministrator(string userName)
        {
            return string.Equals(userName, AppConfig.Get("SuperUser", "muyang"), StringComparison.OrdinalIgnoreCase)
                || string.Equals(userName, AppConfig.Get("SecondSuperUser", "hcwy"), StringComparison.OrdinalIgnoreCase);
        }

        private void ShowChild(Form child)
        {
            foreach (Control control in pageFramePanel.Controls) control.Visible = false;
            child.Visible = true;
            child.BringToFront();
            _activeNavigationButton = child == _homeForm ? homeNavButton
                : child == _importDataForm ? importNavButton
                : child == _programLogForm ? logNavButton
                : child == _functionPanelForm ? featureNavButton
                : child == _configForm ? configNavButton : null;
            ApplyNavigationSelection();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                RecordPageNavigation(child.Text);
        }

        private void ApplyNavigationSelection()
        {
            if (_navigationButtons == null) return;
            foreach (var button in _navigationButtons)
            {
                var selected = button == _activeNavigationButton;
                button.Ghost = false;
                button.Radius = 8;
                button.BorderWidth = 0F;
                if (selected)
                {
                    var selectedBack = Color.FromArgb(28, 126, 97);
                    button.DefaultBack = selectedBack;
                    button.DefaultBorderColor = selectedBack;
                }
                else
                {
                    var groupBack = Color.FromArgb(35, 145, 112);
                    button.DefaultBack = groupBack;
                    button.DefaultBorderColor = groupBack;
                }
            }
        }

        private async void RecordPageNavigation(string pageName)
        {
            try
            {
                await 操作日志.WriteAsync(操作日志.Program, "常规", _logUserName, 登录限制.GetClientIp(),
                    "打开页面", "打开" + pageName, "页面标题=" + pageName);
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    MessageBox.Show(this, "页面操作未能写入日志：" + ex.Message, "日志记录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void mainPage_WindowBar_MouseDown(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Left) DraggableMouseDown(); }
        private void mainPage_ImportDataMenuButton_Click(object sender, EventArgs e) { ShowChild(_importDataForm); }
        private void mainPage_UploadLogMenuButton_Click(object sender, EventArgs e) { ShowChild(_programLogForm); }
        private void mainPage_FunctionPanelMenuButton_Click(object sender, EventArgs e) { ShowChild(_functionPanelForm); }
        private void mainPage_ConfigMenuButton_Click(object sender, EventArgs e)
        {
            if (!_isAdmin)
            {
                foreach (Control control in pageFramePanel.Controls) control.Visible = false;
                _configPermissionPromptPanel.Visible = true;
                _configPermissionPromptPanel.BringToFront();
                _activeNavigationButton = configNavButton;
                ApplyNavigationSelection();
                return;
            }

            ShowChild(_configForm);
        }
        private void mainPage_HomeMenuButton_Click(object sender, EventArgs e) { ShowChild(_homeForm); }

        private void importDataForm_ViewProgramLogRequested(object sender, EventArgs e)
        {
            _programLogForm.SelectUploadLogs();
            ShowChild(_programLogForm);
        }

        private void programLogForm_FunctionPanelRequested(object sender, EventArgs e)
        {
            ShowChild(_operationManualForm);
        }

        private void functionPanel_LogoutRequested(object sender, EventArgs e)
        {
            mainPage_LogoutButton_Click(sender, e);
        }

        private async void mainPage_LogoutButton_Click(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show(this,
                "确定要注销登录吗？注销后将清除已保存的账号和密码。",
                "注销登录", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmation != DialogResult.Yes) return;

            try
            {
                await 操作日志.WriteAsync(操作日志.Program, "常规", _logUserName, 登录限制.GetClientIp(),
                    "注销登录", "用户已注销登录", "当前用户确认注销并清除本机保存的登录凭据");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "注销日志写入失败：" + ex.Message, "操作日志", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                登录凭据.ClearSaved();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "清除已保存账号密码失败：" + ex.Message, "注销登录", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoggedOut = true;
            Close();
        }

        private void CreateNavigationButtons()
        {
            _navigationButtons = new[]
            {
                homeNavButton,
                importNavButton,
                logNavButton,
                featureNavButton,
                configNavButton
            };
            _navigationButtonBounds = Array.ConvertAll(_navigationButtons, button => button.Bounds);
            _navigationButtonTexts = Array.ConvertAll(_navigationButtons, button => button.Text);
            _allMenuButtons = _navigationButtons;
            _originalIcons = Array.ConvertAll(_allMenuButtons, button => button.Icon);
            _originalIconSizes = Array.ConvertAll(_allMenuButtons, button => button.IconSize);
            _originalIconGaps = Array.ConvertAll(_allMenuButtons, button => button.IconGap);
            _originalIconPositions = Array.ConvertAll(_allMenuButtons, button => button.IconPosition);
            _originalTextCenterHasIcons = Array.ConvertAll(_allMenuButtons, button => button.TextCenterHasIcon);
            _originalTextAlignments = Array.ConvertAll(_allMenuButtons, button => button.TextAlign);
            _originalPaddings = Array.ConvertAll(_allMenuButtons, button => button.Padding);
            _legacyMenuIcons = new[]
            {
                LoadMenuIcon("nav-home.png"),
                LoadMenuIcon("nav-import-collapsed.png"),
                LoadMenuIcon("nav-log.png"),
                LoadMenuIcon("nav-feature.png"),
                LoadMenuIcon("nav-settings.png")
            };
            _collapsedToggleIcon = LoadMenuIcon("nav-toggle-collapsed.png");
            _toggleButtonBounds = toggleSidebarButton.Bounds;
            _toggleButtonText = toggleSidebarButton.Text;
            _toggleButtonIconSvg = toggleSidebarButton.IconSvg;
            _toggleButtonIcon = toggleSidebarButton.Icon;
            _toggleButtonIconSize = toggleSidebarButton.IconSize;
            _toggleButtonIconGap = toggleSidebarButton.IconGap;
            _toggleButtonIconPosition = toggleSidebarButton.IconPosition;
            _toggleButtonTextCenterHasIcon = toggleSidebarButton.TextCenterHasIcon;
            _toggleButtonTextAlign = toggleSidebarButton.TextAlign;
            _toggleButtonPadding = toggleSidebarButton.Padding;
            _logoutButtonBounds = logoutButton.Bounds;
            _logoutButtonText = logoutButton.Text;
            _logoutButtonIcon = logoutButton.Icon;
            _logoutButtonIconSize = logoutButton.IconSize;
            _logoutButtonIconGap = logoutButton.IconGap;
            _logoutButtonIconPosition = logoutButton.IconPosition;
            _logoutButtonTextCenterHasIcon = logoutButton.TextCenterHasIcon;
            _logoutButtonTextAlign = logoutButton.TextAlign;
            _logoutButtonPadding = logoutButton.Padding;
            _sidebarExpandedWidth = sidebarPanel.Width;
            _navigationGroupBounds = navigationGroupPanel.Bounds;
            _navigationGroupBottomMargin = sidebarPanel.ClientSize.Height - _navigationGroupBounds.Bottom;
            _toggleButtonBottomMargin = _navigationGroupBounds.Height - _toggleButtonBounds.Bottom;
            _menuTitleButtonBounds = button1.Bounds;
            _menuTitleButtonText = button1.Text;
            _menuTitleButtonIcon = button1.Icon;
            _menuTitleButtonIconSize = button1.IconSize;
            _menuTitleButtonIconGap = button1.IconGap;
            _menuTitleButtonIconPosition = button1.IconPosition;
            _menuTitleButtonTextCenterHasIcon = button1.TextCenterHasIcon;
            _menuTitleButtonTextAlign = button1.TextAlign;
            _menuTitleButtonPadding = button1.Padding;
            _sidebarAnimationTargetWidth = _sidebarExpandedWidth;
            _sidebarAnimationTimer = new Timer(components) { Interval = 15 };
            _sidebarAnimationTimer.Tick += mainPage_SidebarAnimationTimer_Tick;
        }

        private static Image LoadMenuIcon(string iconName)
        {
            var resourceName = "批量导入程序重制版.Resources." + iconName;
            using (var stream = typeof(frmMainPage).Assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;
                using (var source = Image.FromStream(stream)) return new Bitmap(source);
            }
        }

        private void mainPage_SidebarToggleButton_Click(object sender, EventArgs e)
        {
            pageFramePanel.Visible = false;
            _sidebarAnimationTargetWidth = _sidebarAnimationTargetWidth > 100 ? 72 : _sidebarExpandedWidth;
            if (_sidebarAnimationTargetWidth < 100)
            {
                _expandedSidebarVisualsPrepared = false;
                button1.Text = string.Empty;
                foreach (var button in _navigationButtons) button.Text = string.Empty;
                logoutButton.Text = string.Empty;
                toggleSidebarButton.Text = string.Empty;
                _sidebarCollapseDelayTicks = 5;
            }
            else
            {
                _sidebarCollapseDelayTicks = 0;
            }
            _sidebarAnimationTimer.Start();
        }

        private void mainPage_SidebarAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (_sidebarCollapseDelayTicks > 0)
            {
                _sidebarCollapseDelayTicks--;
                return;
            }

            var delta = _sidebarAnimationTargetWidth - sidebarPanel.Width;
            if (Math.Abs(delta) <= 3)
            {
                sidebarPanel.Width = _sidebarAnimationTargetWidth;
                _sidebarAnimationTimer.Stop();
                LayoutSidebar(_sidebarAnimationTargetWidth < 100);
                pageFramePanel.Visible = true;
                pageFramePanel.Invalidate(true);
                return;
            }

            var nextWidth = sidebarPanel.Width + (int)Math.Round(delta * 0.56);
            if (nextWidth == sidebarPanel.Width) nextWidth += Math.Sign(delta);
            sidebarPanel.Width = nextWidth;
            var progress = (sidebarPanel.Width - 72d) / (_sidebarExpandedWidth - 72d);
            for (var i = 0; i < _navigationButtons.Length; i++)
            {
                _navigationButtons[i].Width = (int)Math.Round(48 + (_navigationButtonBounds[i].Width - 48) * progress);
                if (_sidebarAnimationTargetWidth < 100)
                    _navigationButtons[i].IconSize = GetCollapsingIconSize(_originalIconSizes[i], progress);
            }
            if (_sidebarAnimationTargetWidth < 100)
            {
                button1.IconSize = GetCollapsingIconSize(_menuTitleButtonIconSize, progress);
                logoutButton.IconSize = GetCollapsingIconSize(_logoutButtonIconSize, progress);
                toggleSidebarButton.IconSize = GetCollapsingIconSize(_toggleButtonIconSize, progress);
            }
            navigationGroupPanel.Width = (int)Math.Round(48 + (_navigationGroupBounds.Width - 48) * progress);
            button1.Width = (int)Math.Round(48 + (_menuTitleButtonBounds.Width - 48) * progress);
            logoutButton.Width = navigationGroupPanel.Width;
            toggleSidebarButton.Width = (int)Math.Round(48 + (_toggleButtonBounds.Width - 48) * progress);
            toggleSidebarButton.Top = GetToggleButtonTop();
            if (_sidebarAnimationTargetWidth >= 100 && progress >= 0.82d && !_expandedSidebarVisualsPrepared)
                ApplySidebarVisuals(false);
        }

        private static Size GetCollapsingIconSize(Size expandedSize, double expandedProgress)
        {
            var collapsedProgress = 1d - Math.Max(0d, Math.Min(1d, expandedProgress));
            return new Size(
                Math.Min(22, (int)Math.Round(expandedSize.Width + (22 - expandedSize.Width) * collapsedProgress)),
                Math.Min(22, (int)Math.Round(expandedSize.Height + (22 - expandedSize.Height) * collapsedProgress)));
        }

        private void mainPage_SidebarPanel_Resize(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || _navigationButtons == null) return;
            if (_sidebarAnimationTimer != null && _sidebarAnimationTimer.Enabled) return;
            LayoutSidebar(sidebarPanel.ClientSize.Width < 100);
        }

        private void LayoutSidebar(bool collapsed)
        {
            if (_navigationButtons == null) return;
            brandSubLabel.Visible = true;
            logoutButton.Visible = true;
            var navigationGroupHeight = Math.Max(0, sidebarPanel.ClientSize.Height
                - _navigationGroupBounds.Y - _navigationGroupBottomMargin);
            navigationGroupPanel.Bounds = collapsed
                ? new Rectangle(_navigationGroupBounds.X, _navigationGroupBounds.Y, 48, navigationGroupHeight)
                : new Rectangle(_navigationGroupBounds.X, _navigationGroupBounds.Y, _navigationGroupBounds.Width, navigationGroupHeight);
            button1.Bounds = collapsed
                ? new Rectangle(_menuTitleButtonBounds.X, _menuTitleButtonBounds.Y, 48, _menuTitleButtonBounds.Height)
                : _menuTitleButtonBounds;
            logoutButton.Bounds = collapsed
                ? new Rectangle(_logoutButtonBounds.X, _logoutButtonBounds.Y, 48, _logoutButtonBounds.Height)
                : _logoutButtonBounds;
            toggleSidebarButton.Bounds = collapsed
                ? new Rectangle(_toggleButtonBounds.X, GetToggleButtonTop(), 48, _toggleButtonBounds.Height)
                : new Rectangle(_toggleButtonBounds.X, GetToggleButtonTop(), _toggleButtonBounds.Width, _toggleButtonBounds.Height);
            for (var i = 0; i < _navigationButtons.Length; i++)
            {
                var button = _navigationButtons[i];
                var originalBounds = _navigationButtonBounds[i];
                button.Bounds = collapsed
                    ? new Rectangle(originalBounds.X, originalBounds.Y, 48, originalBounds.Height)
                    : originalBounds;
            }
            if (collapsed || !_expandedSidebarVisualsPrepared)
                ApplySidebarVisuals(collapsed);
        }

        private void ApplySidebarVisuals(bool collapsed)
        {
            button1.Radius = 8;
            button1.BorderWidth = 0F;
            button1.Text = collapsed ? string.Empty : _menuTitleButtonText;
            button1.Icon = _menuTitleButtonIcon;
            button1.IconSize = collapsed ? new Size(22, 22) : _menuTitleButtonIconSize;
            button1.IconGap = collapsed ? 10F : _menuTitleButtonIconGap;
            button1.IconPosition = collapsed ? AntdUI.TAlignMini.Left : _menuTitleButtonIconPosition;
            button1.TextCenterHasIcon = collapsed || _menuTitleButtonTextCenterHasIcon;
            button1.TextAlign = collapsed ? ContentAlignment.MiddleCenter : _menuTitleButtonTextAlign;
            button1.Padding = collapsed ? Padding.Empty : _menuTitleButtonPadding;
            toggleSidebarButton.Ghost = false;
            toggleSidebarButton.BackColor = Color.FromArgb(35, 145, 112);
            toggleSidebarButton.DefaultBack = Color.FromArgb(35, 145, 112);
            toggleSidebarButton.DefaultBorderColor = Color.FromArgb(35, 145, 112);
            toggleSidebarButton.BackHover = Color.FromArgb(28, 126, 97);
            toggleSidebarButton.BackActive = Color.FromArgb(22, 108, 83);
            toggleSidebarButton.ForeColor = Color.White;
            toggleSidebarButton.Radius = 8;
            toggleSidebarButton.BorderWidth = 0F;
            logoutButton.Text = collapsed ? string.Empty : _logoutButtonText;
            logoutButton.Icon = _logoutButtonIcon;
            logoutButton.IconSize = collapsed ? new Size(22, 22) : _logoutButtonIconSize;
            logoutButton.Radius = 8;
            logoutButton.BorderWidth = 0F;
            logoutButton.IconGap = collapsed ? 10F : _logoutButtonIconGap;
            logoutButton.IconPosition = collapsed ? AntdUI.TAlignMini.Left : _logoutButtonIconPosition;
            logoutButton.TextCenterHasIcon = collapsed || _logoutButtonTextCenterHasIcon;
            logoutButton.TextAlign = collapsed ? ContentAlignment.MiddleCenter : _logoutButtonTextAlign;
            logoutButton.Padding = collapsed ? Padding.Empty : _logoutButtonPadding;
            navigationToolTip.SetToolTip(logoutButton, collapsed ? _logoutButtonText : string.Empty);
            toggleSidebarButton.Text = collapsed ? string.Empty : _toggleButtonText;
            toggleSidebarButton.IconSvg = collapsed ? null : _toggleButtonIconSvg;
            toggleSidebarButton.Icon = collapsed ? _collapsedToggleIcon : _toggleButtonIcon;
            toggleSidebarButton.IconSize = collapsed ? new Size(22, 22) : _toggleButtonIconSize;
            toggleSidebarButton.IconGap = _toggleButtonIconGap;
            toggleSidebarButton.IconPosition = _toggleButtonIconPosition;
            toggleSidebarButton.TextCenterHasIcon = _toggleButtonTextCenterHasIcon;
            toggleSidebarButton.TextAlign = _toggleButtonTextAlign;
            toggleSidebarButton.Padding = _toggleButtonPadding;
            navigationToolTip.SetToolTip(toggleSidebarButton, collapsed ? "展开菜单" : "收起菜单");
            for (var i = 0; i < _allMenuButtons.Length; i++)
            {
                var button = _allMenuButtons[i];
                button.Icon = collapsed && i != 1 ? _legacyMenuIcons[i] : _originalIcons[i];
                if (collapsed)
                {
                    button.IconSize = new Size(22, 22);
                    button.IconGap = 10F;
                    button.IconPosition = AntdUI.TAlignMini.Left;
                    button.TextCenterHasIcon = true;
                    button.TextAlign = ContentAlignment.MiddleCenter;
                    button.Padding = Padding.Empty;
                }
                else
                {
                    button.IconSize = _originalIconSizes[i];
                    button.IconGap = _originalIconGaps[i];
                    button.IconPosition = _originalIconPositions[i];
                    button.TextCenterHasIcon = _originalTextCenterHasIcons[i];
                    button.TextAlign = _originalTextAlignments[i];
                    button.Padding = _originalPaddings[i];
                }
            }
            for (var i = 0; i < _navigationButtons.Length; i++)
            {
                var button = _navigationButtons[i];
                button.Text = collapsed ? string.Empty : _navigationButtonTexts[i];
                navigationToolTip.SetToolTip(button, collapsed ? _navigationButtonTexts[i] : string.Empty);
            }
            ApplyNavigationSelection();
            _expandedSidebarVisualsPrepared = !collapsed;
        }

        private int GetToggleButtonTop()
        {
            return Math.Max(0, navigationGroupPanel.Height - _toggleButtonBottomMargin - _toggleButtonBounds.Height);
        }
    }
}
