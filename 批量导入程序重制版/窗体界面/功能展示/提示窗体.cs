using AntdUI;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    internal class DoubleBufferedPanel : System.Windows.Forms.Panel
    {
        private const int WS_EX_COMPOSITED = 0x02000000;

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_COMPOSITED;
                return cp;
            }
        }

        public DoubleBufferedPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
    }

    public partial class 提示窗体 : AntdUI.Window
    {
        private AppSettings _settings;
        private System.Windows.Forms.Panel _infoCard;
        private int _rowY;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
            base.OnFormClosing(e);
        }

        public 提示窗体(string strLable, string strText, bool blShowExit, bool blShowPicInfo, string btnOkName, string btnExitName)
        {
            InitializeComponent();
            lblLable.Text = strLable;
            rxtText.Text = strText;
            btnExit.Visible = blShowExit;
            btnOK.Text = btnOkName;
            btnExit.Text = btnExitName;
            if (blShowExit)
                splitContainer1.SplitterDistance = 260;
            if (blShowPicInfo)
            {
                if (strLable.Contains("连接参考") || strLable.Contains("配置帮助"))
                    SetupHelpCardLayout();
                else
                    SetupUserInfoLayout();
            }
        }

        private void SetupUserInfoLayout()
        {
            bool isUserInfo = !lblLable.Text.Contains("版本");
            this.ClientSize = isUserInfo ? new Size(520, 560) : new Size(900, 600);
            rxtText.Visible = false;
            lblLable.Visible = false;

            // 把按钮从 splitContainer 移到窗体
            splitContainer1.Panel2.Controls.Remove(btnOK);
            splitContainer1.Panel2.Controls.Remove(btnExit);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnExit);
            splitContainer1.Visible = false;

            if (isUserInfo)
            {
                // 头像：居中显示，保持 Designer 原始样式
                avaPic.Visible = true;
                avaPic.Location = new Point((this.ClientSize.Width - 70) / 2, 20);

                // 用户名
                lblUserName.Visible = true;
                lblUserName.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
                lblUserName.ForeColor = Color.FromArgb(40, 50, 60);
                lblUserName.TextAlign = ContentAlignment.MiddleCenter;
                lblUserName.Size = new Size(this.ClientSize.Width, 36);
                lblUserName.Location = new Point(0, 120);
            }
            else
            {
                // 版本信息：显示标题
                lblLable.Visible = true;
                lblLable.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
                lblLable.ForeColor = Color.FromArgb(40, 50, 60);
                lblLable.TextAlign = ContentAlignment.MiddleCenter;
                lblLable.Size = new Size(this.ClientSize.Width, 36);
                lblLable.Location = new Point(0, 20);
            }

            // 信息卡片
            _rowY = 8;
            int cardY = isUserInfo ? 170 : 70;
            int cardH = isUserInfo ? 310 : 470;
            _infoCard = new DoubleBufferedPanel
            {
                BackColor = Color.White,
                Location = new Point(20, cardY),
                Size = new Size(this.ClientSize.Width - 40, cardH),
                AutoScroll = !isUserInfo,
                AutoScrollMargin = new Size(0, 10),
                HorizontalScroll = { Enabled = false, Visible = false },
                AutoSize = false
            };
            _infoCard.Paint += (s, e) =>
            {
                var rect = new Rectangle(0, 0, _infoCard.Width - 1, _infoCard.Height - 1);
                using (var path = RoundedRect(rect, 12))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.SetClip(path);
                    e.Graphics.Clear(Color.White);
                    e.Graphics.ResetClip();
                    e.Graphics.FillPath(Brushes.White, path);
                    using (var pen = new Pen(Color.FromArgb(230, 235, 240), 1))
                        e.Graphics.DrawPath(pen, path);
                }
            };
            _infoCard.Resize += (s, e) =>
            {
                var rect = new Rectangle(0, 0, _infoCard.Width - 1, _infoCard.Height - 1);
                using (var path = RoundedRect(rect, 12))
                    _infoCard.Region = new Region(path);
            };
            using (var initPath = RoundedRect(new Rectangle(0, 0, _infoCard.Width - 1, _infoCard.Height - 1), 12))
                _infoCard.Region = new Region(initPath);
            this.Controls.Add(_infoCard);

            // 关闭按钮
            int btnW = this.ClientSize.Width - 40;
            int btnY = isUserInfo ? 500 : 555;
            btnOK.Visible = false;
            btnExit.Visible = true;
            btnExit.Text = "关闭";
            btnExit.Size = new Size(btnW, 44);
            btnExit.Location = new Point(20, btnY);
            btnExit.DefaultBack = Color.FromArgb(45, 170, 160);
            btnExit.DefaultBorderColor = Color.FromArgb(35, 145, 138);
            btnExit.ForeColor = Color.White;
            btnExit.Radius = 10;
            btnExit.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
            btnExit.BringToFront();
        }

        private void SetupHelpCardLayout()
        {
            bool isConnHelp = lblLable.Text.Contains("连接参考");
            this.ClientSize = new Size(800, isConnHelp ? 500 : 700);

            rxtText.Visible = false;
            lblLable.Visible = false;

            // 把按钮从 splitContainer 移到窗体
            splitContainer1.Panel2.Controls.Remove(btnOK);
            splitContainer1.Panel2.Controls.Remove(btnExit);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnExit);
            splitContainer1.Visible = false;

            // 标题
            var lblTitle = new System.Windows.Forms.Label
            {
                Text = isConnHelp ? "连接字符串参考" : "配置帮助",
                Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 50, 60),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(this.ClientSize.Width, 40),
                Location = new Point(0, 20)
            };
            this.Controls.Add(lblTitle);

            // 副标题
            var lblSub = new System.Windows.Forms.Label
            {
                Text = isConnHelp ? "修改 [关键字] 为实际参数值（内容可复制）" : "SQL 语句配置说明",
                Font = new Font("Microsoft YaHei UI", 9.5F),
                ForeColor = Color.FromArgb(130, 140, 150),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(this.ClientSize.Width, 24),
                Location = new Point(0, 60)
            };
            this.Controls.Add(lblSub);

            // 信息卡片
            int cardY = 95;
            int cardH = this.ClientSize.Height - 160;
            _infoCard = new DoubleBufferedPanel
            {
                BackColor = Color.White,
                Location = new Point(20, cardY),
                Size = new Size(this.ClientSize.Width - 40, cardH),
                AutoScroll = true,
                AutoScrollMargin = new Size(0, 10),
                HorizontalScroll = { Enabled = false, Visible = false }
            };
            _infoCard.Paint += (s, e) =>
            {
                var rect = new Rectangle(0, 0, _infoCard.Width - 1, _infoCard.Height - 1);
                using (var path = RoundedRect(rect, 12))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.SetClip(path);
                    e.Graphics.Clear(Color.White);
                    e.Graphics.ResetClip();
                    e.Graphics.FillPath(Brushes.White, path);
                    using (var pen = new Pen(Color.FromArgb(230, 235, 240), 1))
                        e.Graphics.DrawPath(pen, path);
                }
            };
            _infoCard.Resize += (s, e) =>
            {
                var rect = new Rectangle(0, 0, _infoCard.Width - 1, _infoCard.Height - 1);
                using (var path = RoundedRect(rect, 12))
                    _infoCard.Region = new Region(path);
            };
            using (var initPath = RoundedRect(new Rectangle(0, 0, _infoCard.Width - 1, _infoCard.Height - 1), 12))
                _infoCard.Region = new Region(initPath);
            this.Controls.Add(_infoCard);

            int rowY = 12;
            int cw = _infoCard.ClientSize.Width;

            if (isConnHelp)
            {
                // SqlServer
                rowY = AddHelpSection(_infoCard, rowY, cw, "SqlServer", "data source=[地址],[端口默认1433];database=[数据库名];uid=[账号];pwd=[密码];", Color.FromArgb(0, 122, 204));
                // Oracle
                rowY = AddHelpSection(_infoCard, rowY, cw, "Oracle", "User ID=[账号];Password=[密码];Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =[地址])(PORT =[端口默认1521]))) (CONNECT_DATA = (SERVICE_NAME = [数据库名])))", Color.FromArgb(204, 85, 0));
                // MySql
                rowY = AddHelpSection(_infoCard, rowY, cw, "MySql", "Server=[地址];Port=[端口默认3306];Database=[数据库名];User Id=[账号默认root];Password=[密码];Charset=utf8;", Color.FromArgb(0, 153, 68));
            }
            else
            {
                // top 1
                rowY = AddHelpSection(_infoCard, rowY, cw, "查询优化", "top 1 条件可以识别加速查询且不会添加到原始语句中。\r目前支持SQLServer 暂不支持Oracle[row num] MySQL[limit]", Color.FromArgb(0, 122, 204));
                // where 1=1
                rowY = AddHelpSection(_infoCard, rowY, cw, "必要条件", "where 1=1 条件是必须添加的！", Color.FromArgb(220, 53, 69));
                // 体检编号
                rowY = AddHelpSection(_infoCard, rowY, cw, "体检编号", "and [外接体检号字段名] = '@体检编号@'", Color.FromArgb(0, 122, 204));
                // 申请单号
                rowY = AddHelpSection(_infoCard, rowY, cw, "申请单号", "and [外接体检号字段名] = '@申请单号@'", Color.FromArgb(0, 122, 204));
                // 日期条件
                rowY = AddHelpSection(_infoCard, rowY, cw, "日期条件", "and [外接日期字段名] between '@开始日期@' and '@结束日期@'", Color.FromArgb(102, 51, 153));
            }

            // 关闭按钮
            int btnW = this.ClientSize.Width - 40;
            int btnYPos = this.ClientSize.Height - 55;
            btnOK.Visible = false;
            btnExit.Visible = true;
            btnExit.Text = "关闭";
            btnExit.Size = new Size(btnW, 44);
            btnExit.Location = new Point(20, btnYPos);
            btnExit.DefaultBack = Color.FromArgb(45, 170, 160);
            btnExit.DefaultBorderColor = Color.FromArgb(35, 145, 138);
            btnExit.ForeColor = Color.White;
            btnExit.Radius = 10;
            btnExit.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
            btnExit.BringToFront();
        }

        private int AddHelpSection(System.Windows.Forms.Panel card, int y, int cardWidth, string title, string content, Color accentColor)
        {
            int padding = 16;
            int innerWidth = cardWidth - padding * 2;

            // 彩色竖条 + 标题
            var colorBar = new System.Windows.Forms.Label
            {
                BackColor = accentColor,
                Location = new Point(padding, y + 2),
                Size = new Size(4, 20)
            };
            card.Controls.Add(colorBar);

            var lblTitle = new System.Windows.Forms.Label
            {
                Text = title,
                Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold),
                ForeColor = accentColor,
                Location = new Point(padding + 12, y),
                AutoSize = true
            };
            card.Controls.Add(lblTitle);
            y += 28;

            // 内容（可复制）
            var rtf = new System.Windows.Forms.RichTextBox
            {
                Text = content,
                Font = new Font("Microsoft YaHei UI", 10F),
                ForeColor = Color.FromArgb(60, 70, 80),
                BackColor = Color.FromArgb(248, 249, 252),
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                Location = new Point(padding + 4, y),
                Size = new Size(innerWidth - 8, 40),
                WordWrap = true,
                ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None,
                DetectUrls = false
            };
            // 自适应高度
            using (var g = rtf.CreateGraphics())
            {
                var sz = g.MeasureString(content, rtf.Font, innerWidth - 8);
                int h = Math.Max(40, (int)sz.Height + 16);
                rtf.Size = new Size(innerWidth - 8, h);
            }
            card.Controls.Add(rtf);
            y += rtf.Size.Height + 8;

            // 分隔线
            card.Controls.Add(new System.Windows.Forms.Label
            {
                BackColor = Color.FromArgb(240, 243, 246),
                Location = new Point(padding, y),
                Size = new Size(innerWidth, 1)
            });
            y += 12;

            return y;
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(new Rectangle(bounds.Location, new Size(d, d)), 180, 90);
            path.AddArc(new Rectangle(bounds.Right - d, bounds.Y, d, d), 270, 90);
            path.AddArc(new Rectangle(bounds.Right - d, bounds.Bottom - d, d, d), 0, 90);
            path.AddArc(new Rectangle(bounds.X, bounds.Bottom - d, d, d), 90, 90);
            path.CloseFigure();
            return path;
        }

        private void AddInfoRow(string label, string value)
        {
            int cardWidth = _infoCard.ClientSize.Width;
            int rowHeight = 36;

            var lbl = new System.Windows.Forms.Label
            {
                Text = label,
                Font = new Font("Microsoft YaHei UI", 10F),
                ForeColor = Color.FromArgb(130, 140, 150),
                Location = new Point(16, _rowY + 6),
                AutoSize = true
            };

            var val = new System.Windows.Forms.Label
            {
                Text = value,
                Font = new Font("Microsoft YaHei UI", 10F),
                ForeColor = Color.FromArgb(40, 50, 60),
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(cardWidth / 2, _rowY + 6),
                Size = new Size(cardWidth / 2 - 16, 24)
            };

            _infoCard.Controls.Add(lbl);
            _infoCard.Controls.Add(val);
            _rowY += rowHeight;

            if (_rowY < _infoCard.ClientSize.Height - 10)
            {
                _infoCard.Controls.Add(new System.Windows.Forms.Label
                {
                    BackColor = Color.FromArgb(240, 243, 246),
                    Location = new Point(16, _rowY - 2),
                    Size = new Size(cardWidth - 32, 1)
                });
            }
        }

        private async void loadPcInFo()
        {
            if (_infoCard == null) return;
            try
            {
                string avatarPath = await Task.Run(() => SettingHelper.GetUserTilePath(Environment.UserName));
                if (!string.IsNullOrEmpty(avatarPath))
                    avaPic.Image = Image.FromFile(avatarPath);

                string strIP = _settings.MainDbConnStr;
                string[] arr = strIP.Split(';');
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].Contains("source"))
                        strIP = arr[i].Replace(" ", "").Replace("datasource=", "");
                }

                string ip = "";
                int port = 1433;
                if (strIP.Contains(","))
                {
                    ip = strIP.Split(',')[0];
                    port = int.Parse(string.IsNullOrEmpty(strIP.Split(',')[1]) ? "1433" : strIP.Split(',')[1]);
                }

                var netTask = Task.Run(() => SettingHelper.CheckInternetByTcp("www.baidu.com", 80, 3000));
                var innetTask = Task.Run(() => ip != "" && SettingHelper.CheckInternetByTcp(ip, port, 3000));
                var localIpTask = Task.Run(() => SettingHelper.GetLocalIPv4Address());
                await Task.WhenAll(netTask, innetTask, localIpTask);

                string netStatus = netTask.Result ? "已连接" : "未连接";
                string innetStatus = innetTask.Result ? "已连接" : "未连接";
                string localIp = string.IsNullOrEmpty(localIpTask.Result) ? "无地址" : localIpTask.Result;

                lblUserName.Text = Environment.UserName;
                AddInfoRow("计算机名称", Environment.MachineName);
                AddInfoRow("本机地址", localIp);
                AddInfoRow("服务器地址", strIP);
                AddInfoRow("局域网状态", innetStatus);
                AddInfoRow("互联网状态", netStatus);
                AddInfoRow("分辨率要求", "1440*900及以上");
                AddInfoRow("系统要求", "Windows7及以上");
                AddInfoRow("运行环境", ".NET Framework 4.6及以上");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("用户信息加载异常，详细信息：\r" + ex.ToString(), "加载失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void loadVersionInfo()
        {
            if (_infoCard == null) return;
            try
            {
            _rowY = 8;
            int cardWidth = _infoCard.ClientSize.Width - 40;
            var baseFont = new Font("Microsoft YaHei UI", 10F);
            var boldFont = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);

            var lines = rxtText.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(lines);
            foreach (var line in lines)
            {
                string trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed)) continue;

                // 解析格式: V X.X.X [日期] [标签] 描述内容
                string ver = "", date = "", tag = "", desc = "";
                string remaining = trimmed;

                // 提取版本号 V X.X.X
                int firstBracket = remaining.IndexOf('[');
                if (firstBracket > 0)
                {
                    ver = remaining.Substring(0, firstBracket).Trim();
                    remaining = remaining.Substring(firstBracket);
                }

                // 提取日期 [YYYY-MM-DD HH:mm]
                if (remaining.StartsWith("["))
                {
                    int closeBracket = remaining.IndexOf(']');
                    if (closeBracket > 0)
                    {
                        date = remaining.Substring(1, closeBracket - 1);
                        remaining = remaining.Substring(closeBracket + 1).Trim();
                    }
                }

                // 提取标签 [xxx]
                if (remaining.StartsWith("["))
                {
                    int closeBracket = remaining.IndexOf(']');
                    if (closeBracket > 0)
                    {
                        tag = remaining.Substring(1, closeBracket - 1);
                        remaining = remaining.Substring(closeBracket + 1).Trim();
                    }
                }

                desc = remaining;

                // 第一行: 版本号 + 日期 + 标签
                int x = 20;
                int lineH = 22;

                // 版本号
                _infoCard.Controls.Add(new System.Windows.Forms.Label
                {
                    Text = ver,
                    Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(45, 170, 160),
                    Location = new Point(x, _rowY + 10),
                    AutoSize = true
                });
                x += TextRenderer.MeasureText(ver, new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold), new Size(int.MaxValue, lineH), TextFormatFlags.NoPadding).Width + 8;

                // 日期
                _infoCard.Controls.Add(new System.Windows.Forms.Label
                {
                    Text = date,
                    Font = new Font("Microsoft YaHei UI", 9.5F),
                    ForeColor = Color.FromArgb(130, 140, 150),
                    Location = new Point(x, _rowY + 11),
                    AutoSize = true
                });
                x += TextRenderer.MeasureText(date, new Font("Microsoft YaHei UI", 9.5F), new Size(int.MaxValue, lineH), TextFormatFlags.NoPadding).Width + 10;

                // 标签
                if (!string.IsNullOrEmpty(tag))
                {
                    Color tagColor;
                    if (tag.Contains("重要") || tag.Contains("回退"))
                        tagColor = Color.FromArgb(220, 50, 50);
                    else if (tag.Contains("内测"))
                        tagColor = Color.FromArgb(40, 100, 200);
                    else if (tag.Contains("漏洞"))
                        tagColor = Color.FromArgb(230, 150, 30);
                    else if (tag.Contains("优化"))
                        tagColor = Color.FromArgb(40, 100, 200);
                    else if (tag.Contains("新增") || tag.Contains("开放") || tag.Contains("升级"))
                        tagColor = Color.FromArgb(40, 160, 80);
                    else
                        tagColor = Color.FromArgb(100, 110, 120);

                    _infoCard.Controls.Add(new System.Windows.Forms.Label
                    {
                        Text = tag,
                        Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold),
                        ForeColor = tagColor,
                        BackColor = Color.FromArgb(245, 247, 250),
                        Location = new Point(x, _rowY + 12),
                        AutoSize = true,
                        Padding = new Padding(4, 1, 4, 1)
                    });
                }

                // 第二行: 描述内容
                int y = _rowY + 36;
                x = 20;

                if (!string.IsNullOrEmpty(desc))
                {
                    var segments = ParseDescSegments(desc);
                    foreach (var seg in segments)
                    {
                        bool isTag = seg.IsTag;
                        Color color;
                        Font font;

                        if (isTag && seg.Text == "重要")
                        {
                            color = Color.FromArgb(220, 50, 50);
                            font = boldFont;
                        }
                        else if (isTag && seg.Text == "内测")
                        {
                            color = Color.FromArgb(40, 100, 200);
                            font = boldFont;
                        }
                        else if (isTag)
                        {
                            color = Color.FromArgb(60, 70, 80);
                            font = boldFont;
                        }
                        else
                        {
                            color = Color.FromArgb(60, 70, 80);
                            font = baseFont;
                        }

                        string displayText = isTag ? $"({seg.Text})" : seg.Text;
                        int textW = TextRenderer.MeasureText(displayText, font, new Size(int.MaxValue, lineH), TextFormatFlags.NoPadding).Width;

                        // 换行判断
                        if (x + textW > 20 + cardWidth - 8 && x > 20)
                        {
                            x = 20;
                            y += lineH + 4;
                        }

                        _infoCard.Controls.Add(new System.Windows.Forms.Label
                        {
                            Text = displayText,
                            Font = font,
                            ForeColor = color,
                            Location = new Point(x, y),
                            AutoSize = true
                        });

                        x += textW;
                    }
                }

                int descH = Math.Max(y - _rowY - 36 + lineH, 0);
                _rowY += 36 + descH + 16;

                // 分隔线
                _infoCard.Controls.Add(new System.Windows.Forms.Label
                {
                    BackColor = Color.FromArgb(230, 235, 240),
                    Location = new Point(20, _rowY - 6),
                    Size = new Size(cardWidth - 8, 1)
                });
            }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("版本信息解析异常，详细信息：\r" + ex.ToString(), "加载失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private static System.Collections.Generic.List<DescSegment> ParseDescSegments(string text)
        {
            var result = new System.Collections.Generic.List<DescSegment>();
            int i = 0;
            while (i < text.Length)
            {
                int open = text.IndexOf('(', i);
                if (open < 0)
                {
                    result.Add(new DescSegment(text.Substring(i), false));
                    break;
                }
                int close = text.IndexOf(')', open + 1);
                if (close < 0)
                {
                    result.Add(new DescSegment(text.Substring(i), false));
                    break;
                }
                if (open > i)
                    result.Add(new DescSegment(text.Substring(i, open - i), false));
                result.Add(new DescSegment(text.Substring(open + 1, close - open - 1), true));
                i = close + 1;
            }
            return result;
        }

        private struct DescSegment
        {
            public string Text;
            public bool IsTag;
            public DescSegment(string text, bool isTag) { Text = text; IsTag = isTag; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (_settings != null)
                {
                    _settings.TrueOrFalse = false;
                    SettingHelper.SaveConfig(_settings);
                }
                if (avaPic.Image != null) avaPic.Image.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("操作异常：" + ex.Message, "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnExit.Text.Contains("确认"))
                {
                    if (_settings != null)
                    {
                        _settings.TrueOrFalse = true;
                        SettingHelper.SaveConfig(_settings);
                    }
                    this.Close();
                }
                else if (_infoCard != null)
                {
                    if (avaPic.Image != null) avaPic.Image.Dispose();
                    this.Close();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("操作异常：" + ex.Message, "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private async void FormTips_Load(object sender, EventArgs e)
        {
            try
            {
                if (btnExit.Text != null && btnExit.Text.Contains("确认"))
                    btnExit.Icon = Properties.Resources.确认白色;

                _settings = await Task.Run(() => SettingHelper.LoadConfig());

                if (_infoCard != null)
                {
                    if (avaPic.Visible)
                        loadPcInFo();
                    else
                        loadVersionInfo();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("页面初始化异常，详细信息：\r" + ex.ToString(), "加载失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
