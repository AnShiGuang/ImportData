using System;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 条件勾选：对应旧程序 条件勾选窗体。
    /// </summary>
    public partial class 条件勾选窗体 : Form
    {
        public 条件勾选窗体()
        {
            InitializeComponent();
        }

        private void frm条件勾选_Load(object sender, EventArgs e)
        {
            var 项目 = 条件勾选Helper.读取项目();
            if (项目.Length > 0) txt项目1.Text = 项目[0];
            if (项目.Length > 1) txt项目2.Text = 项目[1];
            if (项目.Length > 2) txt项目3.Text = 项目[2];
            if (项目.Length > 3) txt项目4.Text = 项目[3];
            if (项目.Length > 4) txt项目5.Text = 项目[4];
            if (项目.Length > 5) txt项目6.Text = 项目[5];
            txt单位名称.Text = 条件勾选Helper.读取单位();
        }

        private void btn确定_Click(object sender, EventArgs e)
        {
            条件勾选Helper.保存(
                new[] { txt项目1.Text, txt项目2.Text, txt项目3.Text, txt项目4.Text, txt项目5.Text, txt项目6.Text },
                txt单位名称.Text);
            this.Close();
        }
    }
}
