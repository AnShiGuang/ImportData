using System;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new 登录窗体());
        }
    }
}
