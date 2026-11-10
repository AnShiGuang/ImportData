using System;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 是否对应错误项目：对应旧程序 错误项目窗体。
    /// </summary>
    public partial class 错误项目窗体 : Form
    {
        public string str组合项目名称;
        public string str组合项目序号;
        public string str明细项目名称;
        public string str明细项目序号;

        public 错误项目窗体()
        {
            InitializeComponent();
        }

        private void Form是否对应错误项目_Load(object sender, EventArgs e)
        {
            lbl提示.Text = "检验项目[" + str明细项目名称 + "]是组合项目[" + str组合项目名称 + "]里面的项目吗？";
            if (str组合项目名称 == "")
            {
                timer1.Enabled = true;
            }
        }

        private void cmd是_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmd否_Click(object sender, EventArgs e)
        {
            try
            {
                var db = new Database(AppConfig.ConnectionString);
                db.ExecuteSql("delete jcsj_MingXiXiangMu where zuhexiangmuid ='" + (str组合项目序号 ?? string.Empty).Replace("'", "''")
                    + "' and mingxixiangmumingcheng = '" + (str明细项目名称 ?? string.Empty).Replace("'", "''")
                    + "' and lisxiangmubianma ='" + (str明细项目序号 ?? string.Empty).Replace("'", "''") + "'");
            }
            catch { }
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (str组合项目名称 == "")
            {
                for (int i = 3; i > 0; i--)
                {
                    lbl提示.Text = "检验项目[" + str明细项目名称 + "]是组合项目[" + str组合项目名称 + "]里面的项目吗？ " + i + "秒";
                    System.Threading.Thread.Sleep(800);
                    Application.DoEvents();
                }
                cmd是.Visible = false;
                cmd否_Click(sender, e);
            }
        }
    }
}
