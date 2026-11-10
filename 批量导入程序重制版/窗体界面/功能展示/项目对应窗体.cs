using System;
using System.Data;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 检验项目对应：对应旧程序 项目对应窗体。
    /// 把检验明细项目对应到体检组合项目（jcsj_MingXiXiangMu）。
    /// </summary>
    public partial class 项目对应窗体 : Form
    {
        public string str体检编号 = string.Empty;
        public string str项目名称 = string.Empty;
        public string str项目编码 = string.Empty;
        public string str检验条码 = string.Empty;
        public string str项目单位 = string.Empty;
        public string str参考范围 = string.Empty;
        public string str参考上限 = string.Empty;
        public string str参考下限 = string.Empty;
        public bool bl是否对应 = false;
        public string gstr组合项目代码 = "0";
        public string gstr组合项目名称 = "";
        public string m_str体检编号前缀 = string.Empty;

        public 项目对应窗体()
        {
            InitializeComponent();
        }

        private void Form项目对应_Load(object sender, EventArgs e)
        {
            try
            {
                lv组合项目.Columns.Clear();
                lv组合项目.Columns.Add("序号", 80, HorizontalAlignment.Center);
                lv组合项目.Columns.Add("组合项目序号", 140, HorizontalAlignment.Center);
                lv组合项目.Columns.Add("组合项目名称", 270, HorizontalAlignment.Center);

                var dt = 项目对应Helper.获取组合项目(AppConfig.ConnectionString, str检验条码);
                int i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    var item = new ListViewItem(i.ToString());
                    item.SubItems.Add(row["组合项目序号"].ToString());
                    item.SubItems.Add(row["组合项目名称"].ToString());
                    lv组合项目.Items.Add(item);
                    i++;
                }

                lbl条码名称.Text = 项目对应Helper.获取条码名称(AppConfig.ConnectionString, str检验条码);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            lbl项目名称.Text = str项目名称;
            lbl项目编码.Text = str项目编码;
            lbl检验条码.Text = str检验条码;
        }

        private void btn跳过对照_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("点击跳过可能导致总检出现问题，是否继续跳过？", "提示信息", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btn确定_Click(object sender, EventArgs e)
        {
            try
            {
                if (lv组合项目.SelectedItems.Count == 0) return;

                string str组合项目代码 = lv组合项目.SelectedItems[0].SubItems[1].Text;
                string str组合项目名称 = lv组合项目.SelectedItems[0].SubItems[2].Text;

                gstr组合项目名称 = str组合项目名称;
                gstr组合项目代码 = str组合项目代码;

                if (!项目对应Helper.是否存在对应(AppConfig.ConnectionString, str组合项目代码, lbl项目编码.Text))
                {
                    项目对应Helper.新增明细对应(AppConfig.ConnectionString, lbl项目名称.Text, str组合项目代码,
                        str项目单位, str参考范围, str参考下限, str参考上限, str项目编码);
                }

                var dt = 项目对应Helper.获取对应组合项目(AppConfig.ConnectionString, str体检编号, lbl项目编码.Text);
                foreach (DataRow row in dt.Rows)
                {
                    string str对应组合项目名称 = row["zuhexiangmumingcheng"].ToString();
                    string str对应组合项目序号 = row["zuhexiangmuid"].ToString();
                    if (str对应组合项目名称 != str组合项目名称)
                    {
                        using (var form = new 错误项目窗体())
                        {
                            form.str组合项目名称 = str对应组合项目名称;
                            form.str组合项目序号 = str对应组合项目序号;
                            form.str明细项目名称 = lbl项目名称.Text;
                            form.str明细项目序号 = lbl项目编码.Text;
                            form.ShowDialog(this);
                        }
                    }
                }

                bl是否对应 = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }

        private void lv组合项目_DoubleClick(object sender, EventArgs e)
        {
            btn确定_Click(sender, e);
        }
    }
}
