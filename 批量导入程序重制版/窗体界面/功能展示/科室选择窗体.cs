using AntdUI;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 批量导入程序重制版.TiJian4;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 选择科室界面：按系统标识勾选科室，并写入接口_配置信息表的“科室名称/字段对应”参数。
    /// </summary>
    public partial class 科室选择窗体 : AntdUI.Window
    {
        private readonly string m_strSystemName;
        private readonly SqlSugarClient _db;

        public 科室选择窗体(string strSystemName)
        {
            InitializeComponent();
            m_strSystemName = strSystemName;
            _db = new DBTableAndView().InitSqlSugarClient();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
            base.OnFormClosing(e);
        }

        private async void FormSelectDept_Load(object sender, EventArgs e)
        {
            try
            {
                tree1.Items.Clear();
                tree2.Items.Clear();
                tree3.Items.Clear();
                lblSystemName.Text = "系统标识：" + m_strSystemName;

                var deptNameTask = _db.Queryable<接口_配置信息表>()
                               .Where(t => t.配置名称 == m_strSystemName && t.参数标识 == "科室名称")
                               .Take(1).Select(t => t.参数值).FirstAsync();
                var deptListTask = _db.Queryable<jcsj_KeShi>().ToListAsync();

                await Task.WhenAll(deptNameTask, deptListTask);

                string deptName = deptNameTask.Result;
                foreach (var item in deptListTask.Result)
                {
                    TreeItem treeItem = new TreeItem();
                    treeItem.Text = item.KeShiMingCheng;
                    treeItem.ID = item.KeShiLeiXing;
                    if (deptName != null && deptName.Contains(item.KeShiMingCheng))
                    {
                        treeItem.Checked = true;
                    }
                    tree1.Items.Add(treeItem);
                }
                loadItem();
            }
            catch (Exception ex)
            {
                new 提示窗体("加载失败：", "科室列表加载异常，详细信息：\r" + ex.ToString(), false, false, "好的", "").ShowDialog();
            }
        }

        public void loadItem()
        {
            int i = 0;
            foreach (TreeItem item in tree1.Items)
            {
                if (item.Checked)
                {
                    i++;
                    if (i <= 8)
                    {
                        TreeItem item2 = new TreeItem();
                        item2.Text = item.Text;
                        tree2.Items.Add(item2);
                    }
                    else
                    {
                        TreeItem item3 = new TreeItem();
                        item3.Text = item.Text;
                        tree3.Items.Add(item3);
                    }
                }
            }
        }

        private void tree1_SelectChanged(object sender, TreeSelectEventArgs e)
        {
            tree2.Items.Clear();
            tree3.Items.Clear();
            e.Item.Checked = !e.Item.Checked;

            loadItem();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strDepts = "";
                string strDeptType = "";
                int x = 0;
                foreach (TreeItem item in tree1.Items)
                {
                    if (item.Checked)
                    {
                        x++;
                        strDepts += item.Text + ",";
                        strDeptType += item.ID + ";";
                    }
                }

                strDepts = strDepts.TrimEnd(',');
                if (!await new SqlSugarRepository<接口_配置信息表>(_db)
                                   .UpdateWhereAsync(t => new 接口_配置信息表 { 参数值 = strDepts }, t => t.配置名称 == m_strSystemName && t.参数标识 == "科室名称"))
                {
                    提示窗体 errorTip = new 提示窗体("更新失败：", "发生了位置的错误，未能更新成功科室信息！", false, false, "好的", "");
                    errorTip.ShowDialog();
                }

                await new SqlSugarRepository<接口_配置信息表>(_db).UpdateWhereAsync(t =>
                new 接口_配置信息表 { 是否启用 = "否" }, t => t.配置名称 == m_strSystemName && t.参数标识 == "字段对应");

                await new SqlSugarRepository<接口_配置信息表>(_db).UpdateWhereAsync(t =>
                new 接口_配置信息表 { 是否启用 = "是" }, t => t.配置名称 == m_strSystemName && t.参数标识 == "字段对应" && t.字段类别 == "all");

                if (strDeptType.Contains("检查科室") || strDeptType.Contains("肺功能") || strDeptType.Contains("彩超科室") || strDeptType.Contains("特检科室"))
                    await new SqlSugarRepository<接口_配置信息表>(_db).UpdateWhereAsync(t =>
                    new 接口_配置信息表 { 是否启用 = "是" }, t => t.配置名称 == m_strSystemName && t.参数标识 == "字段对应" && t.字段类别 == "pacs");
                if (strDeptType.Contains("检验科室"))
                    await new SqlSugarRepository<接口_配置信息表>(_db).UpdateWhereAsync(t =>
                    new 接口_配置信息表 { 是否启用 = "是" }, t => t.配置名称 == m_strSystemName && t.参数标识 == "字段对应" && t.字段类别 == "lis");

                this.Close();
            }
            catch (Exception ex)
            {
                提示窗体 errorTip = new 提示窗体("更新错误：", ex.Message, false, false, "好的", "");
                errorTip.ShowDialog();
            }
        }
    }
}
