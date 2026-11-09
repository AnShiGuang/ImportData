using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    public sealed partial class ProgramLogForm : PageFormBase
    {
        public event EventHandler FunctionPanelRequested;
        private readonly string userName;
        private readonly Timer refreshTimer;
        private int loadRequestId;
        private int secondsUntilRefresh = 5;
        private bool isLoading;
        private bool reloadPending;
        private bool logReadErrorShown;

        public ProgramLogForm() : this(string.Empty) { }

        public ProgramLogForm(string userName)
        {
            this.userName = userName;
            InitializeComponent();
            refreshTimer = new Timer { Interval = 1000 };
            refreshTimer.Tick += refreshTimer_Tick;
            logTable.Columns = new AntdUI.ColumnCollection
            {
                new AntdUI.Column("OccurredAt", "时间") { Width = "128" },
                new AntdUI.Column("Type", "类型") { Width = "74" },
                new AntdUI.Column("Level", "等级") { Width = "58" },
                new AntdUI.Column("UserName", "用户") { Width = "82", Ellipsis = true },
                new AntdUI.Column("ClientIp", "IP 地址") { Width = "94" },
                new AntdUI.Column("Action", "操作") { Width = "115", Ellipsis = true },
                new AntdUI.Column("Message", "消息") { Width = "180", Ellipsis = true },
                new AntdUI.Column("View", "查看")
                {
                    Width = "64",
                    Align = AntdUI.ColumnAlign.Left,
                    ColAlign = AntdUI.ColumnAlign.Center,
                    Render = (value, record, index) => new AntdUI.CellButton("view-log-details", "查看", AntdUI.TTypeMini.Primary)
                }
            };
            logTable.SetRowStyle += logTable_SetRowStyle;
            logTable.CellButtonClick += logTable_CellButtonClick;
            allLogsRadio.CheckedChanged += logTypeRadio_CheckedChanged;
            programLogsRadio.CheckedChanged += logTypeRadio_CheckedChanged;
            uploadLogsRadio.CheckedChanged += logTypeRadio_CheckedChanged;
            refreshButton.Click += refreshButton_Click;
            functionPanelButton.Click += functionPanelButton_Click;
            VisibleChanged += ProgramLogForm_VisibleChanged;
        }

        private void functionPanelButton_Click(object sender, EventArgs e)
        {
            FunctionPanelRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ProgramLogForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                ResetRefreshCountdown();
                LoadLogs();
                refreshTimer.Start();
            }
            else
            {
                refreshTimer.Stop();
                refreshButton.Text = "刷新";
                loadRequestId++;
                reloadPending = false;
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (!Visible || isLoading) return;
            if (secondsUntilRefresh > 1)
            {
                secondsUntilRefresh--;
                UpdateRefreshButtonText();
                return;
            }

            secondsUntilRefresh = 0;
            refreshButton.Text = "刷新中...";
            LoadLogs();
        }

        private void logTypeRadio_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            if (!e.Value) return;
            if (sender == allLogsRadio)
            {
                programLogsRadio.Checked = false;
                uploadLogsRadio.Checked = false;
            }
            else if (sender == programLogsRadio)
            {
                allLogsRadio.Checked = false;
                uploadLogsRadio.Checked = false;
            }
            else
            {
                allLogsRadio.Checked = false;
                programLogsRadio.Checked = false;
            }
            LoadLogs();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            ResetRefreshCountdown();
            LoadLogs();
        }

        private void ResetRefreshCountdown()
        {
            secondsUntilRefresh = 5;
            UpdateRefreshButtonText();
        }

        private void UpdateRefreshButtonText()
        {
            refreshButton.Text = "刷新 (" + secondsUntilRefresh + ")";
        }

        public void SelectUploadLogs()
        {
            if (!uploadLogsRadio.Checked) uploadLogsRadio.Checked = true;
            else if (Visible) LoadLogs();
        }

        private async void LoadLogs()
        {
            if (isLoading)
            {
                reloadPending = true;
                return;
            }

            isLoading = true;
            var requestId = ++loadRequestId;
            var selectedType = allLogsRadio.Checked ? 操作日志.All
                : programLogsRadio.Checked ? 操作日志.Program : 操作日志.Upload;
            refreshButton.Enabled = false;
            refreshButton.Text = "刷新中...";
            statusLabel.Text = "正在读取日志...";
            try
            {
                List<OperationLogEntry> entries = await 操作日志.ReadAsync(selectedType);
                if (IsDisposed || requestId != loadRequestId) return;
                logTable.Binding(new AntdUI.AntList<OperationLogEntry>(entries));
                logTable.Empty = entries.Count == 0;
                statusLabel.Text = "共显示 " + entries.Count + " 条记录";
                logReadErrorShown = false;
            }
            catch (Exception ex)
            {
                if (IsDisposed || requestId != loadRequestId) return;
                statusLabel.Text = "日志读取失败。";
                if (!logReadErrorShown)
                {
                    logReadErrorShown = true;
                    MessageBox.Show(this, "读取日志失败：" + ex.Message, "操作日志", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                if (!IsDisposed)
                {
                    isLoading = false;
                    refreshButton.Enabled = true;
                    if (Visible) ResetRefreshCountdown();
                    if (reloadPending && Visible)
                    {
                        reloadPending = false;
                        LoadLogs();
                    }
                }
            }
        }

        private AntdUI.Table.CellStyleInfo logTable_SetRowStyle(object sender, AntdUI.TableSetRowStyleEventArgs e)
        {
            var entry = e.Record as OperationLogEntry;
            if (entry == null) return new AntdUI.Table.CellStyleInfo();

            Color color;
            if (string.Equals(entry.Level, "严重", StringComparison.Ordinal))
                color = Color.FromArgb(190, 48, 48);
            else if (string.Equals(entry.Level, "警告", StringComparison.Ordinal))
                color = Color.FromArgb(173, 112, 0);
            else if (ContainsSuccessMessage(entry.Action) || ContainsSuccessMessage(entry.Message))
                color = Color.FromArgb(35, 133, 93);
            else
                color = Color.FromArgb(75, 85, 99);
            return new AntdUI.Table.CellStyleInfo { ForeColor = color };
        }

        private static bool ContainsSuccessMessage(string value)
        {
            return !string.IsNullOrEmpty(value) && value.IndexOf("成功", StringComparison.Ordinal) >= 0;
        }

        private void logTable_CellButtonClick(object sender, AntdUI.TableButtonEventArgs e)
        {
            if (e.Btn == null || e.Btn.Id != "view-log-details") return;
            var entry = e.Record as OperationLogEntry;
            if (entry == null) return;

            var details = "时间：" + entry.OccurredAt
                + "\r\n类型：" + entry.Type
                + "\r\n等级：" + entry.Level
                + "\r\n用户：" + entry.UserName
                + "\r\nIP 地址：" + entry.ClientIp
                + "\r\n操作：" + entry.Action
                + "\r\n消息：" + entry.Message
                + "\r\n\r\n详情：\r\n" + (string.IsNullOrWhiteSpace(entry.Details) ? "无" : entry.Details);
            MessageBox.Show(this, details, "日志详情", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public async void AddLog(string time, string fileName, string result, string details)
        {
            var level = result != null && (result.Contains("失败") || result.Contains("错误")) ? "警告" : "常规";
            try
            {
                await 操作日志.WriteAsync(操作日志.Upload, level, userName, 登录限制.GetClientIp(),
                    "上传文件", result ?? "上传结果",
                    "文件名=" + (fileName ?? "未提供") + "；发生时间=" + (time ?? "未提供") + "；结果=" + (result ?? "未提供")
                    + (string.IsNullOrWhiteSpace(details) ? string.Empty : "；说明=" + details));
                if (Visible) LoadLogs();
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    MessageBox.Show(this, "写入上传日志失败：" + ex.Message, "操作日志", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
