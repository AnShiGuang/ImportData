using System;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 自动导入数据：按设定的时间间隔触发导入页的查询与上传。
    /// </summary>
    public partial class 自动导入窗体 : AntdUI.Window
    {
        /// <summary>
        /// 到达导入时间时触发，由主界面调用导入页执行自动导入。
        /// </summary>
        public event EventHandler AutoImportTick;

        private readonly string _userName;
        private System.Windows.Forms.Timer _timer;
        private bool _running;
        private DateTime _nextRun;

        public 自动导入窗体(string userName)
        {
            InitializeComponent();
            _userName = userName;
            UpdateStatus();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_timer != null) _timer.Stop();
            this.Hide();
            base.OnFormClosing(e);
        }

        private async void startButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(intervalInput.Text.Trim(), out var minutes) || minutes <= 0)
            {
                MessageBox.Show(this, "请输入有效的时间间隔（分钟）。", "自动导入数据", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_timer == null)
            {
                _timer = new System.Windows.Forms.Timer();
                _timer.Tick += Timer_Tick;
            }
            _timer.Stop();
            _timer.Interval = minutes * 60 * 1000;
            _timer.Start();
            _running = true;
            _nextRun = DateTime.Now.AddMinutes(minutes);
            UpdateStatus();
            try
            {
                await 操作日志.WriteAsync(操作日志.Program, "常规", _userName, 登录限制.GetClientIp(),
                    "启动自动导入", "自动导入已启动", "间隔=" + minutes + " 分钟");
            }
            catch { }
        }

        private async void stopButton_Click(object sender, EventArgs e)
        {
            if (_timer != null) _timer.Stop();
            _running = false;
            _nextRun = DateTime.MinValue;
            UpdateStatus();
            try
            {
                await 操作日志.WriteAsync(操作日志.Program, "常规", _userName, 登录限制.GetClientIp(),
                    "停止自动导入", "自动导入已停止", null);
            }
            catch { }
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (_timer != null) _timer.Stop();
            _nextRun = DateTime.Now.AddMilliseconds(_timer.Interval);
            try
            {
                await 操作日志.WriteAsync(操作日志.Program, "常规", _userName, 登录限制.GetClientIp(),
                    "自动导入执行", "开始执行自动导入", null);
            }
            catch { }
            AutoImportTick?.Invoke(this, EventArgs.Empty);
            UpdateStatus();
            if (_running && _timer != null) _timer.Start();
        }

        private void UpdateStatus()
        {
            statusLabel.Text = _running
                ? "自动导入运行中，下次执行：" + (_nextRun == DateTime.MinValue ? "—" : _nextRun.ToString("HH:mm:ss"))
                : "自动导入未运行。";
        }
    }
}
