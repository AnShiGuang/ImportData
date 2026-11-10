using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 批量导入程序重制版
{
    /// <summary>
    /// 数据库查询器 / 原始数据查询：执行只读 SELECT 语句并展示结果。
    /// </summary>
    public partial class 数据查询窗体 : AntdUI.Window
    {
        /// <summary>
        /// 原始数据查询模式（默认查询接口_结果中间表）
        /// </summary>
        public bool RawDataMode { get; set; }

        public 数据查询窗体()
        {
            InitializeComponent();
            Text = RawDataMode ? "原始数据查询" : "数据库查询器";
            pageHeader1.Text = Text;
            sqlInput.Text = RawDataMode
                ? "SELECT TOP 100 * FROM 接口_结果中间表"
                : "SELECT TOP 100 * FROM 接口_配置信息表";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
            base.OnFormClosing(e);
        }

        private async void runButton_Click(object sender, EventArgs e)
        {
            var sql = sqlInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(sql))
            {
                MessageBox.Show(this, "请输入 SQL 语句。", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(this, "只允许执行 SELECT 查询。", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            runButton.Enabled = false;
            statusLabel.Text = "正在执行…";
            try
            {
                var table = await Task.Run(() => ExecuteQuery(sql));
                if (IsDisposed) return;
                resultGrid.DataSource = table;
                statusLabel.Text = "查询完成，共 " + table.Rows.Count + " 行。";
            }
            catch (Exception ex)
            {
                if (IsDisposed) return;
                statusLabel.Text = "查询失败。";
                MessageBox.Show(this, "查询失败：" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed) runButton.Enabled = true;
            }
        }

        private static DataTable ExecuteQuery(string sql)
        {
            using (var connection = new SqlConnection(AppConfig.ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 60;
                var adapter = new SqlDataAdapter(command);
                var table = new DataTable();
                connection.Open();
                adapter.Fill(table);
                return table;
            }
        }
    }
}
