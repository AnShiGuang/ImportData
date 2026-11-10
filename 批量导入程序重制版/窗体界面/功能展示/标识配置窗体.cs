using AntdUI;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 批量导入程序重制版.Properties;
using 批量导入程序重制版.TiJian4;

namespace 批量导入程序重制版
{
    public partial class 标识配置参数 : Window
    {
        private SqlSugarClient _db;
        private AppSettings m_settings;
        string m_CaoZuo = string.Empty;
        List<接口_配置信息表> _configInfos;
        BindingList<LocalSetting> listLocalSetting = new BindingList<LocalSetting>();
        string m_strPeiZhiXuHao;
        public 标识配置参数()
        {
            InitializeComponent();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
            base.OnFormClosing(e);
        }
        public 标识配置参数(string strCaoZuo)
        {
            InitializeComponent();
            m_CaoZuo = strCaoZuo;
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            btnOperation.Enabled = false;
            btnYanZheng.Enabled = false;
            btnJieXi.Enabled = false;
            btnSave.Enabled = false;
            btnSaveConfig.Enabled = false;

            this.Shown += async (s, ev) =>
            {
                try
                {
                    await Task.Run(() =>
                    {
                        m_settings = SettingHelper.LoadConfig();
                        _db = new DBTableAndView().InitSqlSugarClient();
                    });

                    selCampus.Items.Clear();
                    selCampus.Items.AddRange(院区Helper.获取院区列表().Cast<object>().ToArray());
                    if (string.IsNullOrWhiteSpace(selCampus.Text) && selCampus.Items.Count > 0)
                        selCampus.Text = selCampus.Items[0].ToString();

                    if (!string.IsNullOrEmpty(m_CaoZuo))
                    {
                        await loadSetting(false);
                    }

                    if (m_CaoZuo.Contains("Delete") || m_CaoZuo.Contains("Enabled"))
                    {
                        selConfigName.Text = m_settings.CurrentConfig;
                        btnOperation.Text = m_CaoZuo == "Delete" ? "删除标识" : "隐藏标识";
                        btnOperation.Icon = m_CaoZuo == "Delete" ? Properties.Resources.删除 : Properties.Resources.隐藏;
                    }
                    if (m_CaoZuo.Contains("Update"))
                    {
                        selConfigName.Text = m_settings.CurrentConfig;
                        btnOperation.Text = "修改标识";
                        btnOperation.Icon = Properties.Resources.修改编辑;
                        await CheckData();
                        btnOperation.Enabled = false;
                    }

                    if (m_CaoZuo.Contains("Delete") || m_CaoZuo.Contains("Enabled"))
                    {
                        panel1.Visible = false;
                        panel2.Visible = false;
                    }

                    btnOperation.Enabled = true;
                    btnYanZheng.Enabled = true;
                    btnJieXi.Enabled = true;
                    btnSave.Enabled = true;
                    btnSaveConfig.Enabled = true;
                }
                catch (Exception ex)
                {
                    btnOperation.Enabled = true;
                    btnYanZheng.Enabled = true;
                    btnJieXi.Enabled = true;
                    btnSave.Enabled = true;
                    btnSaveConfig.Enabled = true;
                    new 提示信息窗体("加载失败：", "配置页面初始化异常，详细信息：\r" + ex.ToString(), false, false, "好的", "").ShowDialog();
                }
            };
        }
        public async Task CheckData()
        {
            try
            {
                 
                if (_configInfos == null || _configInfos.Count == 0)
                {
                    _configInfos = await _db.Queryable<接口_配置信息表>()
                   .Where(u => !string.IsNullOrEmpty(u.配置名称))
                   .ToListAsync()
                   .ConfigureAwait(true);
                }
                // 异步查询配置数据（逻辑不变，移除Dispose）
                var list配置 = _configInfos
                    .Where(t => t.配置名称 == selConfigName.Text && t.参数标识 == "字段对应" && t.是否启用 == "是")
                    .OrderBy(t => t.字段顺序)
                    .Select((t) => new
                    {
                        BenDiZiDuan = t.本地字段名,
                        DuiYingZiDuan = t.外接字段名
                    })
                    .ToList();

                // 关键修改4：SetWidth("285") → SetWidth(285)，修正参数类型
                tabLocalColumns.Columns = new AntdUI.ColumnCollection
                {
                    new AntdUI.Column("BenDiZiDuan", "本地字段名") { Width = "260" },
                    new AntdUI.Column("DuiYingZiDuan", "对应字段名") { Width = "260" }
                };


                listLocalSetting = new BindingList<LocalSetting>(
                list配置.Select((t) => new LocalSetting
                {
                    BenDiZiDuan = t.BenDiZiDuan,
                    DuiYingZiDuan = t.DuiYingZiDuan

                }).ToList()
                );

                tabLocalColumns.Binding(listLocalSetting);
            }
            catch (Exception ex)
            {
                提示信息窗体 from = new 提示信息窗体("解析错误：", ex.ToString(), false, false, "好的", "");
                from.ShowDialog();
                return;
            }
        }

        public class LocalSetting : NotifyProperty
        {
            private string _benDiZiDuan = string.Empty;
            private string _duiYingZiDuan = string.Empty;
            public string BenDiZiDuan
            {
                get => _benDiZiDuan;
                set { _benDiZiDuan = value; OnPropertyChanged(nameof(BenDiZiDuan)); }
            }
            public string DuiYingZiDuan
            {
                get => _duiYingZiDuan;
                set { _duiYingZiDuan = value; OnPropertyChanged(nameof(DuiYingZiDuan)); }
            }
            public LocalSetting(string benDiZiDuan, string duiYingZiDuan)
            {
                BenDiZiDuan = benDiZiDuan ?? string.Empty;
                DuiYingZiDuan = duiYingZiDuan ?? string.Empty;
            }

            public LocalSetting() { }
        }
        private async Task loadSetting(bool ChongZai)
        {
            selConfigName.Items.Clear();

            _configInfos = await _db.Queryable<接口_配置信息表>()
              .Where(u => !string.IsNullOrEmpty(u.配置名称))
              .ToListAsync();

            if (_configInfos.Any())
            {
                var distinct字段值 = _configInfos
                    .Select(t => t.配置名称)
                    .Distinct()
                    .ToList();

                selConfigName.Items.AddRange(distinct字段值.ToArray());
            }
            if (m_CaoZuo.Contains("Update"))
            {
                if (!ChongZai)
                    selConfigName.Text = m_settings.CurrentConfig;

                var controlMap = new Dictionary<string, Action<string>>
                {
                    { "外接数据源SQL语句", v => rxtSQL.Text = v },
                    { "科室名称",          v => selDeptType.Text = v },
                    { "所属院区",          v => selCampus.Text = v },
                    { "图片访问密码",      v => selFangWenMiMa.Text = v },
                    { "图片访问账号",      v => selFangWenZhangHao.Text = v },
                    { "图片分隔符号",      v => selFenGeFuHao.Text = v },
                    { "图片路径类型",      v => selLuJingLeiXing.Text = v },
                    { "图片提供方式",      v => selTiGongFangShi.Text = v },
                    { "图片文件格式",      v => selWenJianGeShi.Text = v },
                    { "图片旋转角度",      v => selXuanZhuanJiaoDu.Text = v },
                    { "图片压缩程度",      v => selYaSuoChengDu.Text = v },
                };

                foreach (var item in _configInfos.Where(u => u.配置名称 == selConfigName.Text))
                {
                    if (item.参数标识 == "外接系统连接字符串")
                    {
                        lblTips.Text = "配置标识：" + item.配置名称;
                        m_strPeiZhiXuHao = item.配置序号;
                        rxtConnectionString.Text = item.参数值;
                        btnOperation.Enabled = false;
                    }
                    else if (controlMap.TryGetValue(item.参数标识, out var setter))
                    {
                        setter(item.参数值);
                    }
                }
            }
        }
        SqlSugarClient wjdb;
        private async void btnYanZheng_Click(object sender, EventArgs e)
        {
            string oldText = "验证链接";
            try
            {
                btnYanZheng.Loading = true;
                if (btnOperation.Enabled)
                {
                    btnOperation_Click(sender, e);
                    if (btnOperation.Enabled)
                        return;
                }
                btnYanZheng.Enabled = false;
                wjdb = new DBTableAndView().WJInitSqlSugarClient(rxtConnectionString.Text);
                wjdb.Ado.ExecuteCommand("SELECT 1"); // 对SQL Server，执行SELECT 1即可
                await new SqlSugarRepository<TiJian4.接口_配置信息表>(_db)
                                 .UpdateWhereAsync(t => new TiJian4.接口_配置信息表 { 参数值 = rxtConnectionString.Text },
                                 t => t.配置名称 == selConfigName.Text && t.参数标识 == "外接系统连接字符串");
                btnYanZheng.Loading = false;
                btnYanZheng.Text = "验证成功！";
                Timer timer = new Timer { Interval = 2000 };
                timer.Tick += (s, ea) =>
                {
                    btnYanZheng.Text = oldText;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                btnYanZheng.Text = oldText; // 异常时也恢复原文字
                if (ex.ToString().Contains("SSL出错，因为升级了驱动,字符串增加")
                    && !rxtConnectionString.Text.Contains("TrustServerCertificate=True"))
                {
                    rxtConnectionString.Text = "Encrypt=True;TrustServerCertificate=True;" + rxtConnectionString.Text;
                    btnYanZheng_Click(sender, e);
                }
                else
                {
                    提示信息窗体 errorTip = new 提示信息窗体("验证失败：", ex.ToString(), false, false, "好的", "");
                    errorTip.ShowDialog();
                }
                btnYanZheng.Loading = false;
                btnYanZheng.Enabled = true;
            }
        }

        Database m_databaseWJ;
        private async void btnJieXi_Click(object sender, EventArgs e)
        {
            if (!rxtSQL.Text.ToLower().Contains("1=1"))
            {
                提示信息窗体 errorTip = new 提示信息窗体("验证失败：", "★ where 1=1 条件是必须添加的！", false, false, "好的", "");
                errorTip.ShowDialog();
                return;
            }
            btnJieXi.Loading = true;
            btnJieXi.Enabled = false;
            if (btnYanZheng.Enabled)
            {
                btnYanZheng_Click(sender, e);
                if (btnYanZheng.Enabled)
                    return;
            }

            // 1. 禁用按钮，防止重复点击
            try
            {
                tabLocalColumns.Columns.Clear();
                tabOtherColumns.Columns.Clear();

                if (string.IsNullOrEmpty(rxtSQL.Text))
                {
                    MessageBox.Show($"解析失败：", "详细信息：语句不能为空！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnJieXi.Enabled = true;
                }
                // 关键修改1：await异步方法，确保CheckData执行完成后再走后续逻辑
                await CheckData();

                // 2. 配置外接表格列
                tabOtherColumns.Columns = new AntdUI.ColumnCollection
                {
                    new AntdUI.Column("WaiJieZiDuan", "外接字段名") { Width = "260" }
                };

                // 3. 异步查询外接数据库（改为异步调用）
                m_databaseWJ = new Database(rxtConnectionString.Text);
                string strSQLWJ = rxtSQL.Text;
                strSQLWJ = strSQLWJ.Replace("#日期条件#", "-- #日期条件#");
                strSQLWJ = strSQLWJ.Replace("#体检编号#", "-- #体检编号#");
                strSQLWJ = strSQLWJ.Replace("#申请单号#", "-- #申请单号#");

                // 关键修改2：同步Query → 异步QueryAsync + await，避免DataReader问题
                DataTable data = await Task.Run(() => m_databaseWJ.Query(strSQLWJ));

                // 4. 填充外接表格数据源
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("WaiJieZiDuan", typeof(string));
                foreach (DataColumn col in data.Columns) // 简化循环，更易读
                {
                    dt2.Rows.Add(col.ColumnName);
                }
                tabOtherColumns.DataSource = dt2;
                SqlSugarClient newDB = new DBTableAndView().InitSqlSugarClient();
                // 核心修复：执行UpdateWhereAsync（此时_db未被释放，且CheckData已完成）
                await new SqlSugarRepository<TiJian4.接口_配置信息表>(newDB)
                    .UpdateWhereAsync(
                        t => new TiJian4.接口_配置信息表 { 参数值 = rxtSQL.Text.ToLower().Replace("top 1", "") }, // 要更新的字段
                        t => t.配置名称 == selConfigName.Text && t.参数标识 == "外接数据源SQL语句" // 更新条件
                    );

                // 执行分页按钮逻辑
                tabControl1.SelectedIndex = 1;
                btnPageUp.Enabled = true;
                btnJieXi.Loading = false;

            }
            catch (Exception ex)
            {
                提示信息窗体 errorTip = new 提示信息窗体("解析失败：", ex.ToString(), false, false, "好的", "");
                errorTip.ShowDialog();
                btnJieXi.Loading = false;
                btnJieXi.Enabled = true;
            }
        }

        private async void tabLocalColumns_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            string str本地字段值 = tabLocalColumns[e.RowIndex - 1]["BenDiZiDuan"].ToString();

            listLocalSetting[e.RowIndex - 1].DuiYingZiDuan = "";
            //listLocalSetting[tabLocalColumns.SelectedIndex - 1].DuiYingZiDuan = str外接字段值;
            await new SqlSugarRepository<TiJian4.接口_配置信息表>(_db)
                               .UpdateWhereAsync(t => new TiJian4.接口_配置信息表 { 外接字段名 = "" },
                               t => t.配置名称 == selConfigName.Text && t.本地字段名 == str本地字段值);

        }

        private async void tabOtherColumns_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            try
            {
                string str外接字段值 = tabOtherColumns[e.RowIndex - 1]["WaiJieZiDuan"].ToString();
                string str本地字段值 = tabLocalColumns[tabLocalColumns.SelectedIndex - 1]["BenDiZiDuan"].ToString();

                listLocalSetting[tabLocalColumns.SelectedIndex - 1].DuiYingZiDuan = str外接字段值;
                //listLocalSetting[tabLocalColumns.SelectedIndex - 1].DuiYingZiDuan = str外接字段值;
                await new SqlSugarRepository<TiJian4.接口_配置信息表>(_db)
                                   .UpdateWhereAsync(t => new TiJian4.接口_配置信息表 { 外接字段名 = str外接字段值 },
                                   t => t.配置名称 == selConfigName.Text && t.本地字段名 == str本地字段值);
            }
            catch (Exception ex)
            {
                string info = ex.ToString();
                if (info.Contains("NullReferenceException"))
                    info = "请先选择左侧要对应的字段。";
                提示信息窗体 errorTip = new 提示信息窗体("对应错误：", info, false, false, "好的", "");
                errorTip.ShowDialog();
            }
        }

        int page = 0;
        private void btnPageUp_Click(object sender, EventArgs e)
        {
            page = tabControl1.SelectedIndex;
            if (page > 0)
            {
                page--;
                tabControl1.SelectTab(page);
                btnPageDown.Enabled = true;
                if (page == 0)
                    btnPageUp.Enabled = false;
                else
                    btnPageUp.Enabled = true;
            }
            else
            {
                btnPageUp.Enabled = false;
                btnPageDown.Enabled = true;
            }
        }

        private void btnPageDown_Click(object sender, EventArgs e)
        {
            page = tabControl1.SelectedIndex;
            if (page < 2)
            {
                page++;
                tabControl1.SelectTab(page);
                btnPageUp.Enabled = true;
                if (page == 2)
                    btnPageDown.Enabled = false;
                else
                    btnPageDown.Enabled = true;
            }
            else
            {
                btnPageDown.Enabled = false;
                btnPageUp.Enabled = true;
            }
        }
        private async Task<bool> funYanZhengShuJu()
        {
            object sender = null;
            EventArgs e = null;

            if (btnOperation.Enabled)
            {
                btnOperation_Click(sender, e);
                if (btnOperation.Enabled)
                    return false;
            }
            string deptName = await _db.Queryable<接口_配置信息表>()
                     .Where(t => t.配置名称 == selConfigName.Text && t.参数标识 == "科室名称").Take(1).Select(t => t.参数值).FirstAsync();
            if (btnYanZheng.Enabled)
            {
                btnYanZheng_Click(sender, e);
                if (btnYanZheng.Enabled)
                    return false;
            }
            if (btnJieXi.Enabled)
            {
                btnJieXi_Click(sender, e);
                if (btnJieXi.Enabled)
                    return false;
            }
            return true;
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Loading = true;
            if (await funYanZhengShuJu())
            {
                SqlSugarClient newDB = new DBTableAndView().InitSqlSugarClient();
                await new SqlSugarRepository<TiJian4.接口_配置信息表>(newDB)
                             .UpdateWhereAsync(t => new TiJian4.接口_配置信息表 { 配置启用 = "是" },
                             t => t.配置名称 == selConfigName.Text);
                this.Close();
            }
            btnSave.Loading = false;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            btnPageDown_Click(sender, e);
        }

        private void btnJieXiLuJing_Click(object sender, EventArgs e)
        {
            string filePath = txtWenJianLuJing.Text.Replace("\\", "/");

            selLuJingLeiXing.Text = get图片路径类型(filePath);
            selTiGongFangShi.Text = get图片提供方式(filePath);
            selWenJianGeShi.Text = get图片文件格式(filePath);
            selFenGeFuHao.Text = get图片分割符号(filePath);

            if (string.IsNullOrEmpty(selYaSuoChengDu.Text))
                selYaSuoChengDu.Text = "30%(推荐选择)";
            if (string.IsNullOrEmpty(selXuanZhuanJiaoDu.Text))
                selXuanZhuanJiaoDu.Text = "0°";
            if (string.IsNullOrEmpty(selFangWenZhangHao.Text))
                selFangWenZhangHao.Text = "未设置(可能导致无法下载)";
            if (string.IsNullOrEmpty(selFangWenMiMa.Text))
                selFangWenMiMa.Text = "未设置";
        }
        public string get图片分割符号(string strPath)
        {
            string strText = strPath;

            if (strText.Contains(";"))
            {
                strText = ";";
            }
            else if (strText.Contains("；"))
            {
                strText = "；";
            }
            else if (strText.Contains("："))
            {
                strText = "：";
            }
            else if (strText.Contains(","))
            {
                strText = ",";
            }
            else if (strText.Contains("，"))
            {
                strText = "，";
            }
            else if (strText.Contains("|"))
            {
                strText = "|";
            }
            else
            {
                strText = "";
            }

            if (string.IsNullOrEmpty(strText))
            {
                strText = "无";
            }
            return strText;
        }

        public string get图片文件格式(string strPath)
        {
            string extension = Path.GetExtension(strPath);
            if (string.IsNullOrEmpty(extension))
            {
                extension = "无";
            }
            else if (!extension.ToLower().Contains("jpg") &&
                !extension.ToLower().Contains("jepg") &&
                !extension.ToLower().Contains("png") &&
                !extension.ToLower().Contains("bmp") &&
                !extension.ToLower().Contains("pdf") &&
                !extension.ToLower().Contains("word") &&
                !extension.ToLower().Contains("jpg"))
            {
                extension = "未能识别";
            }

            return extension;
        }
        public string get图片提供方式(string strPath)
        {
            Uri uri = null;
            if (Uri.TryCreate(strPath, UriKind.Absolute, out uri))
            {
                if (uri.Scheme == Uri.UriSchemeHttp)
                {
                    return "HTTP";
                }
                else if (uri.Scheme == Uri.UriSchemeHttps)
                {
                    return "HTTPS";
                }
                else if (uri.Scheme == Uri.UriSchemeFtp)
                {
                    return "FTP";
                }
                else if (uri.Scheme == Uri.UriSchemeFile && uri.IsUnc)
                {
                    return "共享文件夹";
                }
                else if (Path.IsPathRooted(strPath))
                {

                    return "本地文件";
                }
                else
                {
                    return "识别失败";
                }
            }
            else
            {
                return "路径无效";
            }
        }

        public string get图片路径类型(string strPath)
        {
            string extension = Path.GetExtension(strPath);
            if (string.IsNullOrEmpty(extension))
            {
                return "文件目录";
            }
            else
            {
                return "完整路径";
            }
        }
        private async void btnSaveConfig_Click(object sender, EventArgs e)
        {

            // 1. 定义【参数标识 ↔ 控件文本】的映射关系（完全保留你的对应关系，无任何修改）
            var configMappings = new Dictionary<string, string>
            {
                { "图片路径类型", selLuJingLeiXing.Text },
                { "图片文件格式", selWenJianGeShi.Text },
                { "图片压缩程度", selYaSuoChengDu.Text },
                { "图片访问账号", selFangWenZhangHao.Text },
                { "图片提供方式", selTiGongFangShi.Text },
                { "图片分隔符号", selFenGeFuHao.Text },
                { "图片旋转角度", selXuanZhuanJiaoDu.Text },
                { "图片访问密码", selFangWenMiMa.Text }
            };



            // 批量更新：一次查询 + 内存修改 + 一次保存
            var records = await _db.Queryable<TiJian4.接口_配置信息表>()
                .Where(t => t.配置名称 == selConfigName.Text && t.参数标识 == "图片路径类型"
                    || t.配置名称 == selConfigName.Text && t.参数标识 == "图片文件格式"
                    || t.配置名称 == selConfigName.Text && t.参数标识 == "图片压缩程度"
                    || t.配置名称 == selConfigName.Text && t.参数标识 == "图片访问账号"
                    || t.配置名称 == selConfigName.Text && t.参数标识 == "图片提供方式"
                    || t.配置名称 == selConfigName.Text && t.参数标识 == "图片分隔符号"
                    || t.配置名称 == selConfigName.Text && t.参数标识 == "图片旋转角度"
                    || t.配置名称 == selConfigName.Text && t.参数标识 == "图片访问密码")
                .ToListAsync();
            foreach (var record in records)
            {
                if (configMappings.TryGetValue(record.参数标识, out var val))
                    record.参数值 = val;
            }
            if (records.Any())
                await _db.Updateable(records).UpdateColumns(t => new { t.参数值 }).ExecuteCommandAsync();
            if (await funYanZhengShuJu())
            {
                SqlSugarClient newDB = new DBTableAndView().InitSqlSugarClient();
                await new SqlSugarRepository<TiJian4.接口_配置信息表>(newDB)
                             .UpdateWhereAsync(t => new TiJian4.接口_配置信息表 { 配置启用 = "是" },
                             t => t.配置名称 == selConfigName.Text);
                this.Close();
            }
        }

        private async void btnOperation_Click(object sender, EventArgs e)
        {

            if (m_CaoZuo.Contains("Delete") || m_CaoZuo.Contains("Enabled"))
            {
                var tips = "确认删除：";
                var text = "★标识删除后不可恢复！\r如后续还需使用，不要直接删除标识，请前往功能面板选择隐藏标识。";
                var btnName = "确认删除";
                if (m_CaoZuo.Contains("Enabled"))
                {
                    btnName = btnOperation.Text == "显示标识" ? "确认显示" : "确认隐藏"; ;
                    tips = btnName + "：";
                    text = $"是否{btnName}标识，后续可再次调整。";
                }
                提示信息窗体 from = new 提示信息窗体(tips, text, true, false, "取消操作", btnName);
                from.ShowDialog();
                m_settings = await Task.Run(() => SettingHelper.LoadConfig());
                if (m_settings.TrueOrFalse)
                {
                    if (m_CaoZuo.Contains("Enabled"))
                    {

                        await new SqlSugarRepository<TiJian4.接口_配置信息表>(_db)
                                 .UpdateWhereAsync(t => new TiJian4.接口_配置信息表 { 配置启用 = (btnOperation.Text == "显示标识" ? "是" : "否") },
                                 t => t.配置名称 == selConfigName.Text);
                        await loadSetting(false);
                        btnOperation.Text = btnOperation.Text == "显示标识" ? "隐藏标识" : "显示标识";
                        if (btnOperation.Text.Contains("显示标识"))
                            btnOperation.Icon = Resources.显示;
                        else
                            btnOperation.Icon = Resources.隐藏;
                    }
                    else
                    {
                        await new SqlSugarRepository<TiJian4.接口_配置信息表>(_db)
                                                      .DeleteWhereAsync(t => t.配置名称 == selConfigName.Text);
                        await loadSetting(false);
                        selConfigName.Text = string.Empty;
                    }
                }
                return;
            }
            if (string.IsNullOrEmpty(selDeptType.Text))
            {
                提示信息窗体 from = new 提示信息窗体("提示信息：", "请先选择所属科室类型！", false, false, "了解", "");
                from.ShowDialog();
                return;
            }

            if (string.IsNullOrWhiteSpace(selCampus.Text))
            {
                提示信息窗体 from = new 提示信息窗体("提示信息：", "请先选择所属院区！", false, false, "了解", "");
                from.ShowDialog();
                return;
            }

            string sysName = await _db.Queryable<接口_配置信息表>()
            .Where(t => t.配置名称 == selConfigName.Text).Take(1).Select(t => t.配置名称).FirstAsync();
            if (string.IsNullOrEmpty(selConfigName.Text))
            {
                提示信息窗体 from = new 提示信息窗体("程序提示：", "配置标识名称不可为空！", false, false, "好的", "");
                from.ShowDialog();
                return;
            }
            if (!m_CaoZuo.Contains("Update") && !string.IsNullOrEmpty(sysName))
            {
                提示信息窗体 from = new 提示信息窗体("程序提示：", "\r配置标识已存在，如需修改请在功能面板选择修改标识进行处理。\r如需继续新增，请修改为其他名称。", false, false, "好的", "");
                from.ShowDialog();
                return;
            }
            selConfigName.Enabled = false;
            selDeptType.Enabled = false;
            if (m_CaoZuo.Contains("Update"))
            {
                提示信息窗体 from = new 提示信息窗体("标识修改：", $"{lblTips.Text} 将修改为 {selConfigName.Text} ，是否确认修改？", true, false, "取消操作", "确认修改");
                from.ShowDialog();
                m_settings = await Task.Run(() => SettingHelper.LoadConfig());
                if (m_settings.TrueOrFalse)
                {
                    await new SqlSugarRepository<TiJian4.接口_配置信息表>(_db)
                                  .UpdateWhereAsync(t => new TiJian4.接口_配置信息表 { 配置名称 = selConfigName.Text },
                                  t => t.配置序号 == m_strPeiZhiXuHao);
                    selConfigName.Enabled = true;
                    selDeptType.Enabled = true;
                    await loadSetting(true);
                    new 提示信息窗体("操作成功：", "标识修改成功！", false, false, "好的", "").ShowDialog();
                }
                else
                {
                    selConfigName.Enabled = true;
                    selDeptType.Enabled = true;
                }
                btnOperation.Enabled = false;
            }
            else
            {
                await BatchInsertConfig(selConfigName.Text); 
                updateEnable(selDeptType.Text);
                btnOperation.Enabled = false;
            }
            if (!await SaveDeptInfo())
                return;
        }

        private void btnSelDept_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selConfigName.Text))
            {
                new 提示信息窗体("提示信息：", "请先选择或填写配置标识名称！", false, false, "好的", "").ShowDialog();
                return;
            }

            using (var form = new 科室选择窗体(selConfigName.Text))
            {
                form.ShowDialog(this);
            }
            _ = ReloadDeptTypeAsync();
        }

        /// <summary>
        /// 选择科室保存后，重新读取“科室名称”参数
        /// </summary>
        private async System.Threading.Tasks.Task ReloadDeptTypeAsync()
        {
            try
            {
                var dept = await _db.Queryable<TiJian4.接口_配置信息表>()
                    .Where(t => t.配置名称 == selConfigName.Text && t.参数标识 == "科室名称")
                    .Take(1).Select(t => t.参数值).FirstAsync();
                if (!string.IsNullOrEmpty(dept))
                    selDeptType.Text = dept;
            }
            catch { }
        }

        private async Task<bool> SaveDeptInfo()
        {
            try
            {
                string targetType = selDeptType.Text.Contains("检验科室") ? "lis" : "pacs";
                // 批量查询所有相关记录
                var records = await _db.Queryable<TiJian4.接口_配置信息表>()
                    .Where(t => t.配置名称 == selConfigName.Text &&
                           (t.参数标识 == "科室名称" || t.参数标识 == "字段对应" || t.参数标识 == 院区Helper.院区参数标识))
                    .ToListAsync();
                foreach (var r in records)
                {
                    if (r.参数标识 == "科室名称")
                        r.参数值 = selDeptType.Text;
                    else if (r.参数标识 == 院区Helper.院区参数标识)
                        r.参数值 = 院区Helper.规范化院区(selCampus.Text);
                    else if (r.参数标识 == "字段对应")
                        r.是否启用 = (r.字段类别 == "all" || r.字段类别 == targetType) ? "是" : "否";
                }
                if (records.Any())
                    await _db.Updateable(records).UpdateColumns(t => new { t.参数值, t.是否启用 }).ExecuteCommandAsync();
                return true;
            }
            catch (Exception ex)
            {
                提示信息窗体 errorTip = new 提示信息窗体("更新失败：此配置页将自动关闭！", "\r" + ex.ToString(), false, false, "好的", "");
                errorTip.ShowDialog();
                this.Close();
                return false;
            }
        }

        public async Task<bool> BatchInsertConfig(string strSystemName)
        {
            try
            {
                // 构建插入数据列表
                var configList = new List<JiKou_PeiZhiXinXiBiao>();

                string id = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                // ****************** 第一步：添加纯配置项（字段顺序NULL、字段类别空）******************
                var pureConfigItems = new List<JiKou_PeiZhiXinXiBiao>
        {
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "数据库类型", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "科室名称", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "所属院区", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "图片访问密码", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "图片访问账号", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "图片分隔符号", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "图片路径类型", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "图片提供方式", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "图片文件格式", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "图片旋转角度", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "图片压缩程度", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "外接数据源SQL语句", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"},
            new JiKou_PeiZhiXinXiBiao { PeiZhiXuHao = id, PeiZhiMingCheng = strSystemName, CanShuBiaoZhi = "外接系统连接字符串", BenDiZiDuanMing = "", ZiDuanLeiBie = "", ZiDuanShunXu = null, ShiFouQiYong = "",PeiZhiQiYong = "否"}
        };
                configList.AddRange(pureConfigItems);

                var fieldValueMap = new List<Tuple<string, string>>
        {
            Tuple.Create("体检编号", "all"),
            Tuple.Create("条码号", "all"),
            Tuple.Create("姓名", "all"),
            Tuple.Create("性别", "all"),
            Tuple.Create("年龄", "all"),
            Tuple.Create("组合项目名称", "all"),
            Tuple.Create("组合项目编号", "all"),
            Tuple.Create("明细项目名称", "lis"),
            Tuple.Create("明细项目编号", "lis"),
            Tuple.Create("检查结果", "all"),
            Tuple.Create("单位", "lis"),
            Tuple.Create("范围", "lis"),
            Tuple.Create("上限", "lis"),
            Tuple.Create("下限", "lis"),
            Tuple.Create("危急值", "lis"),
            Tuple.Create("异常提示", "lis"),
            Tuple.Create("是否异常", "all"),
            Tuple.Create("检查日期", "all"),
            Tuple.Create("检查医师", "all"),
            Tuple.Create("审核日期", "all"),
            Tuple.Create("审核医师", "all"),
            Tuple.Create("文件路径", "all"),
            Tuple.Create("顺序", "lis") // 最后一个强制赋值为lis
        };

                // 2. 遍历键值对生成字段对应项（去掉本地表名、直接赋值参数值）
                var fieldMapItems = new List<JiKou_PeiZhiXinXiBiao>();
                for (int i = 0; i < fieldValueMap.Count; i++)
                {
                    var item = fieldValueMap[i];
                    fieldMapItems.Add(new JiKou_PeiZhiXinXiBiao
                    {
                        PeiZhiXuHao = id,
                        PeiZhiMingCheng = strSystemName,
                        CanShuBiaoZhi = "字段对应",       // 参数标识固定为“字段对应”
                        BenDiZiDuanMing = item.Item1, // 本地字段名（Tuple的第一个元素）
                        ZiDuanLeiBie = item.Item2,    // 字段类别（Tuple的第二个元素）
                        ZiDuanShunXu = i + 1,         // 字段顺序从1开始递增（直接索引+1）
                        ShiFouQiYong = "否",          // 字段启用=是
                        CanShuZhi = "",               // 直接赋值参数值（无任何判断）
                        WaiJieZiDuanMing = "",        // 外接字段名空
                        PeiZhiQiYong = "否"           // 配置启用状态
                                                     // 完全去掉本地表名赋值（字段保留但不赋值，数据库存NULL）
                    });
                }

                // 3. 添加到总配置列表
                configList.AddRange(fieldMapItems);

                // ****************** 第三步：批量插入（LINQ风格操作）******************
                // 执行批量插入（SqlSugar自动生成SQL Server批量INSERT语句）
                int insertCount = await _db.Insertable(configList).ExecuteCommandAsync();
                // 验证插入行数是否匹配
                return insertCount == configList.Count;
            }
            catch (Exception ex)
            {
                提示信息窗体 errorTip = new 提示信息窗体("操作失败：", ex.Message, false, false, "好的", "");
                errorTip.ShowDialog();
                return false;
            }
        }

        public async void selConfigName_TextChanged(object sender, EventArgs e)
        {

            //如果是隐藏 判断当前标志状态 按钮文字为 隐藏标识/启用标识
            //
            //
            ///
            ///


            if (!string.IsNullOrEmpty(m_CaoZuo))
            {
                if (selConfigName.Items.Cast<string>().Any(item => item == selConfigName.Text))
                {
                    selConfigName.Enabled = false;
                    await loadSetting(true);
                    await CheckData();
                    selConfigName.Enabled = true;
                }
                else
                {
                    btnOperation.Enabled = true;
                }
                if (!m_CaoZuo.Contains("Update"))
                {
                    string deptName = _configInfos
                    .Where(u => u.参数标识 == "科室名称" && u.配置名称 == selConfigName.Text)  // 筛选条件：年龄=25
                    .Select(u => u.参数值)      // 投影：只提取Name字段
                    .FirstOrDefault();
                    selDeptType.Text = deptName;
                    selDeptType.Enabled = false;

                }
                if (m_CaoZuo.Contains("Enable"))
                {
                    string Enabled = _configInfos
                    .Where(u => u.配置名称 == selConfigName.Text)  // 筛选条件：年龄=25
                    .Select(u => u.配置启用)      // 投影：只提取Name字段
                    .FirstOrDefault();
                    btnOperation.Text = Enabled.Contains("否") ? "显示标识" : "隐藏标识";
                    if (btnOperation.Text.Contains("显示标识"))
                        btnOperation.Icon = Resources.显示;
                    else
                        btnOperation.Icon = Resources.隐藏;

                }
            }
        }

        private void selConfigName_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(m_CaoZuo) && !selConfigName.Items.Cast<string>().Any(item => item == selConfigName.Text))
            {
                btnOperation.Enabled = true;
            }
        }

        private void rxtConnectionString_TextChanged(object sender, EventArgs e)
        {
            btnYanZheng.Text = "验证链接";
            btnYanZheng.Enabled = true;
            HighlightConnectionString();
        }

        private void rxtSQL_TextChanged(object sender, EventArgs e)
        {
            btnJieXi.Enabled = true;
            HighlightSqlKeywords();
        }

        private void HighlightConnectionString()
        {
            int selStart = rxtConnectionString.SelectionStart;
            int selLen = rxtConnectionString.SelectionLength;
            string text = rxtConnectionString.Text;
            if (string.IsNullOrEmpty(text)) return;

            // 重置全部样式
            rxtConnectionString.SelectAll();
            rxtConnectionString.SelectionColor = Color.FromArgb(60, 60, 60);
            rxtConnectionString.SelectionFont = new Font("Microsoft YaHei UI", 10.5F);

            var boldFont = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold);
            var kwColor = Color.FromArgb(30, 80, 160);

            string[] keywords = new[]
            {
                "data source", "Data Source", "database", "initial catalog", "Initial Catalog",
                "user id", "User ID", "User Id", "uid", "pwd", "password", "Password",
                "server", "Server", "port", "Port", "charset", "Charset",
                "service_name", "SERVICE_NAME", "Service Name",
                "Encrypt", "TrustServerCertificate", "Integrated Security"
            };

            foreach (var kw in keywords)
            {
                int idx = 0;
                while (true)
                {
                    idx = text.IndexOf(kw, idx, StringComparison.OrdinalIgnoreCase);
                    if (idx < 0) break;
                    rxtConnectionString.Select(idx, kw.Length);
                    rxtConnectionString.SelectionColor = kwColor;
                    rxtConnectionString.SelectionFont = boldFont;
                    idx += kw.Length;
                }
            }

            rxtConnectionString.Select(selStart, selLen);
            boldFont.Dispose();
        }

        private void HighlightSqlKeywords()
        {
            int selStart = rxtSQL.SelectionStart;
            int selLen = rxtSQL.SelectionLength;
            string text = rxtSQL.Text;
            if (string.IsNullOrEmpty(text)) return;

            // 重置全部样式
            rxtSQL.SelectAll();
            rxtSQL.SelectionColor = Color.FromArgb(60, 60, 60);
            rxtSQL.SelectionFont = new Font("Microsoft YaHei UI", 10.5F);

            var boldFont = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold);
            var kwColor = Color.FromArgb(30, 80, 160);
            var placeholderColor = Color.FromArgb(200, 100, 0);
            var stringColor = Color.FromArgb(0, 128, 0);

            // SQL 关键字（按长度降序避免子串误匹配）
            string[] sqlKeywords = new[]
            {
                "SELECT", "DISTINCT", "INSERT", "UPDATE", "DELETE",
                "FROM", "WHERE", "ORDER BY", "GROUP BY", "BETWEEN",
                "EXISTS", "COUNT", "HAVING", "LIMIT", "OFFSET",
                "JOIN", "LEFT", "RIGHT", "INNER", "OUTER", "FULL",
                "LIKE", "IN", "NOT", "AND", "OR", "AS", "ON",
                "TOP", "ASC", "DESC", "NULL", "INTO", "VALUES",
                "SET", "CREATE", "TABLE", "ALTER", "DROP",
                "MAX", "MIN", "SUM", "AVG", "CASE", "WHEN", "THEN", "ELSE", "END"
            };

            // 高亮 SQL 关键字（整词匹配）
            foreach (var kw in sqlKeywords)
            {
                int idx = 0;
                while (true)
                {
                    idx = text.IndexOf(kw, idx, StringComparison.OrdinalIgnoreCase);
                    if (idx < 0) break;
                    // 前后必须是非字母字符（整词匹配）
                    bool beforeOk = idx == 0 || !char.IsLetter(text[idx - 1]);
                    bool afterOk = idx + kw.Length >= text.Length || !char.IsLetter(text[idx + kw.Length]);
                    if (beforeOk && afterOk)
                    {
                        rxtSQL.Select(idx, kw.Length);
                        rxtSQL.SelectionColor = kwColor;
                        rxtSQL.SelectionFont = boldFont;
                    }
                    idx += kw.Length;
                }
            }

            // 高亮占位符 @xxx@
            int pos = 0;
            while (pos < text.Length)
            {
                int start = text.IndexOf('@', pos);
                if (start < 0) break;
                int end = text.IndexOf('@', start + 1);
                if (end < 0) break;
                rxtSQL.Select(start, end - start + 1);
                rxtSQL.SelectionColor = placeholderColor;
                rxtSQL.SelectionFont = boldFont;
                pos = end + 1;
            }

            // 高亮字符串值 'xxx'
            pos = 0;
            while (pos < text.Length)
            {
                int start = text.IndexOf('\'', pos);
                if (start < 0) break;
                int end = text.IndexOf('\'', start + 1);
                if (end < 0) break;
                rxtSQL.Select(start, end - start + 1);
                rxtSQL.SelectionColor = stringColor;
                pos = end + 1;
            }

            rxtSQL.Select(selStart, selLen);
            boldFont.Dispose();
        }

        private void lblWjConStr_Click(object sender, EventArgs e)
        {
            new 提示信息窗体("连接参考", "", false, true, "", "关闭").ShowDialog();
        }

        private void lblSqlHelp_Click(object sender, EventArgs e)
        {
            new 提示信息窗体("配置帮助", "", false, true, "", "关闭").ShowDialog();
        }

        private async void selDeptType_SelectedIndexChanged(object sender, IntEventArgs e)
        {
            selDeptType.Enabled = false;
            if (string.IsNullOrEmpty(selConfigName.Text))
                return;
            if (!await SaveDeptInfo())
                return;
            提示信息窗体 fromtips = new 提示信息窗体("提示信息：", "科室类型修改成功！", false, false, "好的", "");
            fromtips.ShowDialog();

            updateEnable(selDeptType.Text);
            selDeptType.Enabled = true;
        }

        public async void updateEnable(string strDeptType)
        {
            string targetType = strDeptType.Contains("检验科室") ? "lis" : "pacs";
            // 先查询所有字段对应记录，内存修改后批量保存
            var records = await _db.Queryable<TiJian4.接口_配置信息表>()
                .Where(t => t.配置名称 == selConfigName.Text && t.参数标识 == "字段对应")
                .ToListAsync();
            foreach (var r in records)
                r.是否启用 = (r.字段类别 == "all" || r.字段类别 == targetType) ? "是" : "否";
            if (records.Any())
                await _db.Updateable(records).UpdateColumns(t => new { t.是否启用 }).ExecuteCommandAsync();

            _configInfos = await _db.Queryable<接口_配置信息表>()
            .Where(u => !string.IsNullOrEmpty(u.配置名称))
            .ToListAsync()
            .ConfigureAwait(true);
        }


        private void lblTipsDY_Click(object sender, EventArgs e)
        {
            提示信息窗体 fromtips = new 提示信息窗体("对应要求：", "\r检验：\r体检编号 条码号 组合项目编码 明细项目编码 检查结果 异常标志 检查日期，为必须对应项！" +
                "\r检查：\r体检编号 条码号 组合项目编码 明细项目编码 检查结果 检查日期，为必须对应项！\r★如不对应可能会导致程序无法正常运行或无法达到预计效果。", false, false, "好的", "");
            fromtips.ShowDialog();
        }
    }
}
