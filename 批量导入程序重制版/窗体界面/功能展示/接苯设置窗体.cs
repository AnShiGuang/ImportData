using System;
using System.Data;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 接苯放射设置：对应旧程序 接苯设置窗体 的后台逻辑，写入 接苯设置 表。
    /// </summary>
    public partial class 接苯设置窗体 : Form
    {
        public 接苯设置窗体()
        {
            InitializeComponent();
        }

        private void cmb配置类型_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb配置类型.Text.Contains("接苯"))
            {
                lbl标题3.Text = "中性粒细胞";
                lbl标题4.Visible = false;
                txt结果4上限.Visible = false;
                txt结果4下限.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
            }
            else
            {
                lbl标题3.Text = "血红蛋白";
                lbl标题4.Visible = true;
                txt结果4上限.Visible = true;
                txt结果4下限.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
            }
            Fun_初始化接笨数据();
        }

        private void cmb性别_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fun_初始化接笨数据();
        }

        private void Form接苯放射设置_Load(object sender, EventArgs e)
        {
            try { 接苯放射Helper.创建表(AppConfig.ConnectionString); }
            catch { }
            cmb配置类型.SelectedIndex = 0;
            cmb性别.SelectedIndex = 0;
        }

        public void Fun_初始化接笨数据()
        {
            string str类别 = cmb配置类型.Text;
            string str性别 = cmb性别.Text;
            string str血小板上限 = "", str血小板下限 = "";
            string str白细胞上限 = "", str白细胞下限 = "";
            string str血红蛋白上限 = "", str血红蛋白下限 = "";
            string str红细胞数上限 = "", str红细胞数下限 = "";
            string str中性粒细胞上限 = "", str中性粒细胞下限 = "";

            if (string.IsNullOrEmpty(str类别) || string.IsNullOrEmpty(str性别)) return;
            str性别 = str性别.Contains("女") ? "女" : "男";

            DataTable dt = 接苯放射Helper.读取配置(AppConfig.ConnectionString, str类别, str性别);
            foreach (DataRow row in dt.Rows)
            {
                string 明细 = row["检验明细项目名称"].ToString();
                if (明细.Contains("血小板数")) { str血小板上限 = row["参考上限"].ToString(); str血小板下限 = row["参考下限"].ToString(); }
                if (明细.Contains("白细胞")) { str白细胞上限 = row["参考上限"].ToString(); str白细胞下限 = row["参考下限"].ToString(); }
                if (明细.Contains("中性粒细胞")) { str中性粒细胞上限 = row["参考上限"].ToString(); str中性粒细胞下限 = row["参考下限"].ToString(); }
                if (明细.Contains("血红蛋白")) { str血红蛋白上限 = row["参考上限"].ToString(); str血红蛋白下限 = row["参考下限"].ToString(); }
                if (明细.Contains("红细胞数")) { str红细胞数上限 = row["参考上限"].ToString(); str红细胞数下限 = row["参考下限"].ToString(); }
                if (row["组合项目名称"].ToString().Contains("需要排除的危害因素"))
                    rxt需要排除的危害因素.Text = 明细;
            }

            txt结果1上限.Text = str血小板上限;
            txt结果1下限.Text = str血小板下限;
            txt结果2上限.Text = str白细胞上限;
            txt结果2下限.Text = str白细胞下限;

            if (!str类别.Contains("接苯"))
            {
                txt结果3上限.Text = str血红蛋白上限;
                txt结果3下限.Text = str血红蛋白下限;
                txt结果4上限.Text = str红细胞数上限;
                txt结果4下限.Text = str红细胞数下限;
            }
            else
            {
                txt结果3上限.Text = str中性粒细胞上限;
                txt结果3下限.Text = str中性粒细胞下限;
            }
        }

        private void cmd存盘_Click(object sender, EventArgs e)
        {
            if (!cmb配置类型.Text.Contains("放射") && !cmb配置类型.Text.Contains("接苯"))
            {
                MessageBox.Show("保存失败！请选择正确的配置类型。");
                return;
            }
            if (!cmb性别.Text.Contains("男") && !cmb性别.Text.Contains("女"))
            {
                MessageBox.Show("保存失败！请选择正确的性别。");
                return;
            }

            string str性别 = cmb性别.Text;
            string 连接 = AppConfig.ConnectionString;
            try
            {
                接苯放射Helper.创建表(连接);
                接苯放射Helper.删除配置(连接, cmb配置类型.Text, str性别);

                if (cmb配置类型.Text == "放射")
                {
                    接苯放射Helper.添加配置(连接, "放射", "血常规", "血小板数", txt结果1下限.Text, txt结果1上限.Text, str性别);
                    接苯放射Helper.添加配置(连接, "放射", "血常规", "白细胞", txt结果2下限.Text, txt结果2上限.Text, str性别);
                    接苯放射Helper.添加配置(连接, "放射", "血常规", "血红蛋白", txt结果3下限.Text, txt结果3上限.Text, str性别);
                    接苯放射Helper.添加配置(连接, "放射", "血常规", "红细胞数", txt结果4下限.Text, txt结果4上限.Text, str性别);
                }
                else
                {
                    接苯放射Helper.添加配置(连接, "接苯", "血常规", "血小板数", txt结果1下限.Text, txt结果1上限.Text, str性别);
                    接苯放射Helper.添加配置(连接, "接苯", "血常规", "白细胞", txt结果2下限.Text, txt结果2上限.Text, str性别);
                    接苯放射Helper.添加配置(连接, "接苯", "血常规", "中性粒细胞", txt结果3下限.Text, txt结果3上限.Text, str性别);
                }

                接苯放射Helper.删除需要排除的危害因素(连接);
                接苯放射Helper.添加配置(连接, "接苯", "需要排除的危害因素", rxt需要排除的危害因素.Text, "", "", "男");
                接苯放射Helper.添加配置(连接, "接苯", "需要排除的危害因素", rxt需要排除的危害因素.Text, "", "", "女");

                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message);
            }
        }

        private void cmd退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn删除配置_Click(object sender, EventArgs e)
        {
            string str类别 = cmb配置类型.Text;
            string str性别 = cmb性别.Text;
            try
            {
                接苯放射Helper.删除配置(AppConfig.ConnectionString, str类别, str性别);
                MessageBox.Show("已删除[" + str类别 + "][" + str性别 + "]配置数据。");
                txt结果1上限.Text = "";
                txt结果1下限.Text = "";
                txt结果2上限.Text = "";
                txt结果2下限.Text = "";
                txt结果3上限.Text = "";
                txt结果3下限.Text = "";
                txt结果4上限.Text = "";
                txt结果4下限.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message);
            }
        }
    }
}
