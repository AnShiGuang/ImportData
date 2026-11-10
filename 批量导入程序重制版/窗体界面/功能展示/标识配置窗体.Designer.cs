namespace 批量导入程序重制版
{
    partial class 标识配置窗体
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader1 = new AntdUI.PageHeader();
            this.selConfigName = new AntdUI.Select();
            this.lblTips = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblWjConStr = new System.Windows.Forms.Label();
            this.lblSqlHelp = new System.Windows.Forms.Label();
            this.btnJieXi = new AntdUI.Button();
            this.btnYanZheng = new AntdUI.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rxtSQL = new System.Windows.Forms.RichTextBox();
            this.input1 = new AntdUI.Input();
            this.label1 = new System.Windows.Forms.Label();
            this.rxtConnectionString = new System.Windows.Forms.RichTextBox();
            this.txtReqNo = new AntdUI.Input();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnSave = new AntdUI.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabOtherColumns = new AntdUI.Table();
            this.label3 = new System.Windows.Forms.Label();
            this.tabLocalColumns = new AntdUI.Table();
            this.btnReport = new AntdUI.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnSaveConfig = new AntdUI.Button();
            this.selFangWenMiMa = new AntdUI.Select();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.selFangWenZhangHao = new AntdUI.Select();
            this.selXuanZhuanJiaoDu = new AntdUI.Select();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.selYaSuoChengDu = new AntdUI.Select();
            this.selFenGeFuHao = new AntdUI.Select();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.selWenJianGeShi = new AntdUI.Select();
            this.selTiGongFangShi = new AntdUI.Select();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.selLuJingLeiXing = new AntdUI.Select();
            this.label6 = new System.Windows.Forms.Label();
            this.btnJieXiLuJing = new AntdUI.Button();
            this.txtWenJianLuJing = new AntdUI.Input();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPageUp = new AntdUI.Button();
            this.btnPageDown = new AntdUI.Button();
            this.btnSelDept = new AntdUI.Button();
            this.btnOperation = new AntdUI.Button();
            this.selDeptType = new AntdUI.Select();
            this.selCampus = new AntdUI.Select();
            this.lblCampus = new System.Windows.Forms.Label();
            this.lblTipsDY = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.MaximizeBox = false;
            this.pageHeader1.MinimizeBox = false;
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(1100, 40);
            this.pageHeader1.TabIndex = 2;
            this.pageHeader1.Text = " ";
            // 
            // selConfigName
            // 
            this.selConfigName.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selConfigName.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selConfigName.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.selConfigName.Location = new System.Drawing.Point(160, 65);
            this.selConfigName.MaxCount = 20;
            this.selConfigName.Name = "selConfigName";
            this.selConfigName.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selConfigName.PlaceholderText = "请输入配置标识名称...";
            this.selConfigName.Size = new System.Drawing.Size(215, 43);
            this.selConfigName.TabIndex = 11;
            this.selConfigName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.selConfigName.TextChanged += new System.EventHandler(this.selConfigName_TextChanged);
            this.selConfigName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.selConfigName_KeyDown);
            // 
            // lblTips
            // 
            this.lblTips.AutoSize = true;
            this.lblTips.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.lblTips.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblTips.Location = new System.Drawing.Point(61, 42);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(79, 20);
            this.lblTips.TabIndex = 12;
            this.lblTips.Text = "标识名称：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(40, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1026, 527);
            this.panel1.TabIndex = 17;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-4, -22);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1039, 558);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.tabPage1.Controls.Add(this.lblWjConStr);
            this.tabPage1.Controls.Add(this.lblSqlHelp);
            this.tabPage1.Controls.Add(this.btnJieXi);
            this.tabPage1.Controls.Add(this.btnYanZheng);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.rxtSQL);
            this.tabPage1.Controls.Add(this.input1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.rxtConnectionString);
            this.tabPage1.Controls.Add(this.txtReqNo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1031, 532);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // lblWjConStr
            // 
            this.lblWjConStr.AutoSize = true;
            this.lblWjConStr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblWjConStr.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWjConStr.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblWjConStr.Location = new System.Drawing.Point(162, 19);
            this.lblWjConStr.Name = "lblWjConStr";
            this.lblWjConStr.Size = new System.Drawing.Size(56, 17);
            this.lblWjConStr.TabIndex = 26;
            this.lblWjConStr.Text = "连接参考";
            this.lblWjConStr.Click += new System.EventHandler(this.lblWjConStr_Click);
            // 
            // lblSqlHelp
            // 
            this.lblSqlHelp.AutoSize = true;
            this.lblSqlHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSqlHelp.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSqlHelp.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblSqlHelp.Location = new System.Drawing.Point(162, 226);
            this.lblSqlHelp.Name = "lblSqlHelp";
            this.lblSqlHelp.Size = new System.Drawing.Size(56, 17);
            this.lblSqlHelp.TabIndex = 25;
            this.lblSqlHelp.Text = "配置帮助";
            this.lblSqlHelp.Click += new System.EventHandler(this.lblSqlHelp_Click);
            // 
            // btnJieXi
            // 
            this.btnJieXi.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnJieXi.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnJieXi.BorderWidth = 1F;
            this.btnJieXi.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnJieXi.Icon = global::批量导入程序重制版.Properties.Resources.key;
            this.btnJieXi.IconRatio = 0.9F;
            this.btnJieXi.IsLink = true;
            this.btnJieXi.Location = new System.Drawing.Point(857, 474);
            this.btnJieXi.Name = "btnJieXi";
            this.btnJieXi.Size = new System.Drawing.Size(147, 43);
            this.btnJieXi.TabIndex = 24;
            this.btnJieXi.Text = "解析语句";
            this.btnJieXi.Click += new System.EventHandler(this.btnJieXi_Click);
            // 
            // btnYanZheng
            // 
            this.btnYanZheng.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnYanZheng.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnYanZheng.BorderWidth = 1F;
            this.btnYanZheng.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnYanZheng.Icon = global::批量导入程序重制版.Properties.Resources.连接;
            this.btnYanZheng.IconRatio = 0.9F;
            this.btnYanZheng.IsLink = true;
            this.btnYanZheng.Location = new System.Drawing.Point(857, 197);
            this.btnYanZheng.Name = "btnYanZheng";
            this.btnYanZheng.Size = new System.Drawing.Size(147, 43);
            this.btnYanZheng.TabIndex = 22;
            this.btnYanZheng.Text = "验证链接";
            this.btnYanZheng.Click += new System.EventHandler(this.btnYanZheng_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label2.Location = new System.Drawing.Point(21, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "外接系统查询语句：";
            // 
            // rxtSQL
            // 
            this.rxtSQL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rxtSQL.Location = new System.Drawing.Point(23, 254);
            this.rxtSQL.Name = "rxtSQL";
            this.rxtSQL.Size = new System.Drawing.Size(973, 205);
            this.rxtSQL.TabIndex = 20;
            this.rxtSQL.Text = "";
            this.rxtSQL.TextChanged += new System.EventHandler(this.rxtSQL_TextChanged);
            // 
            // input1
            // 
            this.input1.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(219)))), ((int)(((byte)(214)))));
            this.input1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(232)))), ((int)(((byte)(228)))));
            this.input1.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(219)))), ((int)(((byte)(214)))));
            this.input1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.input1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.input1.IconRatio = 0.8F;
            this.input1.Location = new System.Drawing.Point(15, 246);
            this.input1.Name = "input1";
            this.input1.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.input1.PlaceholderColorExtend = "";
            this.input1.PlaceholderText = "";
            this.input1.Size = new System.Drawing.Size(989, 222);
            this.input1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label1.Location = new System.Drawing.Point(21, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "数据库连接字符串：";
            // 
            // rxtConnectionString
            // 
            this.rxtConnectionString.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rxtConnectionString.Location = new System.Drawing.Point(23, 47);
            this.rxtConnectionString.Name = "rxtConnectionString";
            this.rxtConnectionString.Size = new System.Drawing.Size(973, 135);
            this.rxtConnectionString.TabIndex = 3;
            this.rxtConnectionString.Text = "";
            this.rxtConnectionString.TextChanged += new System.EventHandler(this.rxtConnectionString_TextChanged);
            // 
            // txtReqNo
            // 
            this.txtReqNo.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(219)))), ((int)(((byte)(214)))));
            this.txtReqNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(232)))), ((int)(((byte)(228)))));
            this.txtReqNo.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(219)))), ((int)(((byte)(214)))));
            this.txtReqNo.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.txtReqNo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtReqNo.IconRatio = 0.8F;
            this.txtReqNo.Location = new System.Drawing.Point(15, 39);
            this.txtReqNo.Name = "txtReqNo";
            this.txtReqNo.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtReqNo.PlaceholderColorExtend = "";
            this.txtReqNo.PlaceholderText = "";
            this.txtReqNo.Size = new System.Drawing.Size(989, 152);
            this.txtReqNo.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.tabPage2.Controls.Add(this.lblTipsDY);
            this.tabPage2.Controls.Add(this.btnSave);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.tabOtherColumns);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.tabLocalColumns);
            this.tabPage2.Controls.Add(this.btnReport);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1031, 532);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // btnSave
            // 
            this.btnSave.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnSave.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnSave.BorderWidth = 1F;
            this.btnSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.Icon = global::批量导入程序重制版.Properties.Resources.完成;
            this.btnSave.IconRatio = 0.9F;
            this.btnSave.IsLink = true;
            this.btnSave.Location = new System.Drawing.Point(704, 471);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(147, 43);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "无需报告";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label4.Location = new System.Drawing.Point(547, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "外接字段名：";
            // 
            // tabOtherColumns
            // 
            this.tabOtherColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabOtherColumns.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabOtherColumns.Gap = 12;
            this.tabOtherColumns.Location = new System.Drawing.Point(551, 39);
            this.tabOtherColumns.Name = "tabOtherColumns";
            this.tabOtherColumns.Size = new System.Drawing.Size(462, 426);
            this.tabOtherColumns.TabIndex = 21;
            this.tabOtherColumns.Text = "table1";
            this.tabOtherColumns.CellDoubleClick += new AntdUI.Table.ClickEventHandler(this.tabOtherColumns_CellDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label3.Location = new System.Drawing.Point(21, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "本地字段名：";
            // 
            // tabLocalColumns
            // 
            this.tabLocalColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabLocalColumns.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabLocalColumns.Gap = 12;
            this.tabLocalColumns.Location = new System.Drawing.Point(15, 39);
            this.tabLocalColumns.Name = "tabLocalColumns";
            this.tabLocalColumns.Size = new System.Drawing.Size(530, 426);
            this.tabLocalColumns.TabIndex = 13;
            this.tabLocalColumns.Text = "table1";
            this.tabLocalColumns.CellDoubleClick += new AntdUI.Table.ClickEventHandler(this.tabLocalColumns_CellDoubleClick);
            // 
            // btnReport
            // 
            this.btnReport.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnReport.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnReport.BorderWidth = 1F;
            this.btnReport.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReport.Icon = global::批量导入程序重制版.Properties.Resources.报告详情;
            this.btnReport.IconRatio = 0.9F;
            this.btnReport.IsLink = true;
            this.btnReport.Location = new System.Drawing.Point(857, 471);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(147, 43);
            this.btnReport.TabIndex = 22;
            this.btnReport.Text = "配置报告";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.tabPage3.Controls.Add(this.btnSaveConfig);
            this.tabPage3.Controls.Add(this.selFangWenMiMa);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.selFangWenZhangHao);
            this.tabPage3.Controls.Add(this.selXuanZhuanJiaoDu);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.selYaSuoChengDu);
            this.tabPage3.Controls.Add(this.selFenGeFuHao);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.selWenJianGeShi);
            this.tabPage3.Controls.Add(this.selTiGongFangShi);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.selLuJingLeiXing);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.btnJieXiLuJing);
            this.tabPage3.Controls.Add(this.txtWenJianLuJing);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1031, 532);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnSaveConfig.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnSaveConfig.BorderWidth = 1F;
            this.btnSaveConfig.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveConfig.Icon = global::批量导入程序重制版.Properties.Resources.完成;
            this.btnSaveConfig.IconRatio = 0.9F;
            this.btnSaveConfig.IsLink = true;
            this.btnSaveConfig.Location = new System.Drawing.Point(857, 471);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(147, 43);
            this.btnSaveConfig.TabIndex = 48;
            this.btnSaveConfig.Text = "完成配置";
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // selFangWenMiMa
            // 
            this.selFangWenMiMa.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selFangWenMiMa.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selFangWenMiMa.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.selFangWenMiMa.Items.AddRange(new object[] {
            "未设置(可能导致无法下载)"});
            this.selFangWenMiMa.Location = new System.Drawing.Point(647, 389);
            this.selFangWenMiMa.MaxCount = 20;
            this.selFangWenMiMa.Name = "selFangWenMiMa";
            this.selFangWenMiMa.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selFangWenMiMa.PlaceholderText = "请选择符合要求的选项...";
            this.selFangWenMiMa.Size = new System.Drawing.Size(357, 43);
            this.selFangWenMiMa.TabIndex = 47;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label13.Location = new System.Drawing.Point(534, 402);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 20);
            this.label13.TabIndex = 46;
            this.label13.Text = "报告访问密码：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label14.Location = new System.Drawing.Point(21, 402);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 20);
            this.label14.TabIndex = 45;
            this.label14.Text = "报告访问账号：";
            // 
            // selFangWenZhangHao
            // 
            this.selFangWenZhangHao.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selFangWenZhangHao.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selFangWenZhangHao.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.selFangWenZhangHao.Items.AddRange(new object[] {
            "未设置(可能导致无法下载)"});
            this.selFangWenZhangHao.Location = new System.Drawing.Point(134, 389);
            this.selFangWenZhangHao.MaxCount = 20;
            this.selFangWenZhangHao.Name = "selFangWenZhangHao";
            this.selFangWenZhangHao.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selFangWenZhangHao.PlaceholderText = "请选择符合要求的选项...";
            this.selFangWenZhangHao.Size = new System.Drawing.Size(357, 43);
            this.selFangWenZhangHao.TabIndex = 44;
            // 
            // selXuanZhuanJiaoDu
            // 
            this.selXuanZhuanJiaoDu.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selXuanZhuanJiaoDu.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selXuanZhuanJiaoDu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.selXuanZhuanJiaoDu.Items.AddRange(new object[] {
            "0°",
            "90°",
            "180°",
            "270°"});
            this.selXuanZhuanJiaoDu.Location = new System.Drawing.Point(647, 302);
            this.selXuanZhuanJiaoDu.MaxCount = 20;
            this.selXuanZhuanJiaoDu.Name = "selXuanZhuanJiaoDu";
            this.selXuanZhuanJiaoDu.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selXuanZhuanJiaoDu.PlaceholderText = "请选择符合要求的选项...";
            this.selXuanZhuanJiaoDu.Size = new System.Drawing.Size(357, 43);
            this.selXuanZhuanJiaoDu.TabIndex = 39;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label11.Location = new System.Drawing.Point(534, 315);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 20);
            this.label11.TabIndex = 38;
            this.label11.Text = "图片旋转角度：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label12.Location = new System.Drawing.Point(21, 315);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 20);
            this.label12.TabIndex = 37;
            this.label12.Text = "图片压缩程度：";
            // 
            // selYaSuoChengDu
            // 
            this.selYaSuoChengDu.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selYaSuoChengDu.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selYaSuoChengDu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.selYaSuoChengDu.Items.AddRange(new object[] {
            "10%(最低质量)",
            "20%",
            "30%(推荐选择)",
            "40%",
            "50%",
            "60%(不推荐)",
            "70%(不推荐)",
            "80%(不推荐)",
            "90%(不推荐)",
            "原图(不推荐)"});
            this.selYaSuoChengDu.Location = new System.Drawing.Point(134, 302);
            this.selYaSuoChengDu.MaxCount = 20;
            this.selYaSuoChengDu.Name = "selYaSuoChengDu";
            this.selYaSuoChengDu.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selYaSuoChengDu.PlaceholderText = "请选择符合要求的选项...";
            this.selYaSuoChengDu.Size = new System.Drawing.Size(357, 43);
            this.selYaSuoChengDu.TabIndex = 36;
            // 
            // selFenGeFuHao
            // 
            this.selFenGeFuHao.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selFenGeFuHao.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selFenGeFuHao.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.selFenGeFuHao.Items.AddRange(new object[] {
            ";",
            "；",
            ",",
            "，"});
            this.selFenGeFuHao.Location = new System.Drawing.Point(647, 215);
            this.selFenGeFuHao.MaxCount = 20;
            this.selFenGeFuHao.Name = "selFenGeFuHao";
            this.selFenGeFuHao.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selFenGeFuHao.PlaceholderText = "请选择符合要求的选项...";
            this.selFenGeFuHao.Size = new System.Drawing.Size(357, 43);
            this.selFenGeFuHao.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label9.Location = new System.Drawing.Point(534, 228);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 20);
            this.label9.TabIndex = 30;
            this.label9.Text = "路径分隔符号：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label10.Location = new System.Drawing.Point(21, 228);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 20);
            this.label10.TabIndex = 29;
            this.label10.Text = "报告文件格式：";
            // 
            // selWenJianGeShi
            // 
            this.selWenJianGeShi.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selWenJianGeShi.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selWenJianGeShi.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.selWenJianGeShi.Items.AddRange(new object[] {
            "jpg",
            "jepg",
            "png",
            "bmp",
            "pdf",
            "dicom（暂不支持）",
            "word（暂不支持）"});
            this.selWenJianGeShi.Location = new System.Drawing.Point(134, 215);
            this.selWenJianGeShi.MaxCount = 20;
            this.selWenJianGeShi.Name = "selWenJianGeShi";
            this.selWenJianGeShi.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selWenJianGeShi.PlaceholderText = "请选择符合要求的选项...";
            this.selWenJianGeShi.Size = new System.Drawing.Size(357, 43);
            this.selWenJianGeShi.TabIndex = 28;
            // 
            // selTiGongFangShi
            // 
            this.selTiGongFangShi.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selTiGongFangShi.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selTiGongFangShi.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.selTiGongFangShi.Items.AddRange(new object[] {
            "共享文件夹",
            "本地文件",
            "http",
            "https",
            "ftp",
            "WebService（暂不支持）",
            "二进制（暂不支持）",
            "BASE64（暂不支持）"});
            this.selTiGongFangShi.Location = new System.Drawing.Point(647, 128);
            this.selTiGongFangShi.MaxCount = 20;
            this.selTiGongFangShi.Name = "selTiGongFangShi";
            this.selTiGongFangShi.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selTiGongFangShi.PlaceholderText = "请选择符合要求的选项...";
            this.selTiGongFangShi.Size = new System.Drawing.Size(357, 43);
            this.selTiGongFangShi.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label8.Location = new System.Drawing.Point(534, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 20);
            this.label8.TabIndex = 26;
            this.label8.Text = "文件提供方式：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label7.Location = new System.Drawing.Point(21, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 20);
            this.label7.TabIndex = 24;
            this.label7.Text = "报告路径类型：";
            // 
            // selLuJingLeiXing
            // 
            this.selLuJingLeiXing.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selLuJingLeiXing.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selLuJingLeiXing.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.selLuJingLeiXing.Items.AddRange(new object[] {
            "完整路径",
            "文件目录"});
            this.selLuJingLeiXing.Location = new System.Drawing.Point(134, 128);
            this.selLuJingLeiXing.MaxCount = 20;
            this.selLuJingLeiXing.Name = "selLuJingLeiXing";
            this.selLuJingLeiXing.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selLuJingLeiXing.PlaceholderText = "请选择符合要求的选项...";
            this.selLuJingLeiXing.Size = new System.Drawing.Size(357, 43);
            this.selLuJingLeiXing.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.label6.Location = new System.Drawing.Point(21, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "输入报告地址：";
            // 
            // btnJieXiLuJing
            // 
            this.btnJieXiLuJing.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnJieXiLuJing.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnJieXiLuJing.BorderWidth = 1F;
            this.btnJieXiLuJing.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnJieXiLuJing.Icon = global::批量导入程序重制版.Properties.Resources.标识解析;
            this.btnJieXiLuJing.IconRatio = 0.9F;
            this.btnJieXiLuJing.IsLink = true;
            this.btnJieXiLuJing.Location = new System.Drawing.Point(857, 39);
            this.btnJieXiLuJing.Name = "btnJieXiLuJing";
            this.btnJieXiLuJing.Size = new System.Drawing.Size(147, 43);
            this.btnJieXiLuJing.TabIndex = 22;
            this.btnJieXiLuJing.Text = "解析路径";
            this.btnJieXiLuJing.Click += new System.EventHandler(this.btnJieXiLuJing_Click);
            // 
            // txtWenJianLuJing
            // 
            this.txtWenJianLuJing.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(219)))), ((int)(((byte)(214)))));
            this.txtWenJianLuJing.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(232)))), ((int)(((byte)(228)))));
            this.txtWenJianLuJing.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(219)))), ((int)(((byte)(214)))));
            this.txtWenJianLuJing.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.txtWenJianLuJing.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtWenJianLuJing.IconRatio = 0.8F;
            this.txtWenJianLuJing.Location = new System.Drawing.Point(15, 39);
            this.txtWenJianLuJing.Name = "txtWenJianLuJing";
            this.txtWenJianLuJing.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtWenJianLuJing.PlaceholderColorExtend = "";
            this.txtWenJianLuJing.PlaceholderText = "请输入对方提供的报告路径...";
            this.txtWenJianLuJing.Size = new System.Drawing.Size(836, 43);
            this.txtWenJianLuJing.Suffix = global::批量导入程序重制版.Properties.Resources.条码;
            this.txtWenJianLuJing.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPageUp);
            this.panel2.Controls.Add(this.btnPageDown);
            this.panel2.Controls.Add(this.btnSelDept);
            this.panel2.Location = new System.Drawing.Point(569, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(497, 53);
            this.panel2.TabIndex = 18;
            // 
            // btnPageUp
            // 
            this.btnPageUp.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnPageUp.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnPageUp.BorderWidth = 1F;
            this.btnPageUp.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPageUp.Icon = global::批量导入程序重制版.Properties.Resources.上一页;
            this.btnPageUp.IconRatio = 1F;
            this.btnPageUp.IsLink = true;
            this.btnPageUp.Location = new System.Drawing.Point(167, 5);
            this.btnPageUp.Name = "btnPageUp";
            this.btnPageUp.Size = new System.Drawing.Size(147, 43);
            this.btnPageUp.TabIndex = 21;
            this.btnPageUp.Text = "上一页";
            this.btnPageUp.Click += new System.EventHandler(this.btnPageUp_Click);
            // 
            // btnPageDown
            // 
            this.btnPageDown.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnPageDown.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnPageDown.BorderWidth = 1F;
            this.btnPageDown.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPageDown.Icon = global::批量导入程序重制版.Properties.Resources.下一页;
            this.btnPageDown.IconRatio = 1F;
            this.btnPageDown.IsLink = true;
            this.btnPageDown.Location = new System.Drawing.Point(320, 5);
            this.btnPageDown.Name = "btnPageDown";
            this.btnPageDown.Size = new System.Drawing.Size(147, 43);
            this.btnPageDown.TabIndex = 20;
            this.btnPageDown.Text = "下一页";
            this.btnPageDown.Click += new System.EventHandler(this.btnPageDown_Click);
            // 
            // btnSelDept
            // 
            this.btnSelDept.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnSelDept.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnSelDept.BorderWidth = 1F;
            this.btnSelDept.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSelDept.Icon = global::批量导入程序重制版.Properties.Resources.科室;
            this.btnSelDept.IconRatio = 0.9F;
            this.btnSelDept.IsLink = true;
            this.btnSelDept.Location = new System.Drawing.Point(22, 5);
            this.btnSelDept.Name = "btnSelDept";
            this.btnSelDept.Size = new System.Drawing.Size(147, 43);
            this.btnSelDept.TabIndex = 19;
            this.btnSelDept.Text = "选择科室";
            this.btnSelDept.Visible = true;
            this.btnSelDept.Click += new System.EventHandler(this.btnSelDept_Click);
            // 
            // btnOperation
            // 
            this.btnOperation.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnOperation.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnOperation.BorderWidth = 1F;
            this.btnOperation.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnOperation.Icon = global::批量导入程序重制版.Properties.Resources.新建;
            this.btnOperation.IconRatio = 0.9F;
            this.btnOperation.IsLink = true;
            this.btnOperation.Location = new System.Drawing.Point(381, 65);
            this.btnOperation.Name = "btnOperation";
            this.btnOperation.Size = new System.Drawing.Size(147, 43);
            this.btnOperation.TabIndex = 13;
            this.btnOperation.Text = "新增标识";
            this.btnOperation.Click += new System.EventHandler(this.btnOperation_Click);
            // 
            // selDeptType
            // 
            this.selDeptType.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selDeptType.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selDeptType.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.selDeptType.Items.AddRange(new object[] {
            "检验科室",
            "其他科室"});
            this.selDeptType.Location = new System.Drawing.Point(55, 65);
            this.selDeptType.MaxCount = 20;
            this.selDeptType.Name = "selDeptType";
            this.selDeptType.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selDeptType.PlaceholderText = "科室类型";
            this.selDeptType.Size = new System.Drawing.Size(108, 43);
            this.selDeptType.TabIndex = 19;
            this.selDeptType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.selDeptType.SelectedIndexChanged += new AntdUI.IntEventHandler(this.selDeptType_SelectedIndexChanged);
            //
            // lblCampus
            //
            this.lblCampus.AutoSize = true;
            this.lblCampus.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.lblCampus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblCampus.Location = new System.Drawing.Point(390, 42);
            this.lblCampus.Name = "lblCampus";
            this.lblCampus.Size = new System.Drawing.Size(56, 20);
            this.lblCampus.Text = "院区：";
            //
            // selCampus
            //
            this.selCampus.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selCampus.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.selCampus.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.selCampus.Location = new System.Drawing.Point(390, 65);
            this.selCampus.MaxCount = 20;
            this.selCampus.Name = "selCampus";
            this.selCampus.PlaceholderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selCampus.PlaceholderText = "院区";
            this.selCampus.Size = new System.Drawing.Size(120, 43);
            this.selCampus.TabIndex = 28;
            this.selCampus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTipsDY
            // 
            this.lblTipsDY.AutoSize = true;
            this.lblTipsDY.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTipsDY.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTipsDY.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblTipsDY.Location = new System.Drawing.Point(117, 19);
            this.lblTipsDY.Name = "lblTipsDY";
            this.lblTipsDY.Size = new System.Drawing.Size(56, 17);
            this.lblTipsDY.TabIndex = 27;
            this.lblTipsDY.Text = "对应要求";
            this.lblTipsDY.Click += new System.EventHandler(this.lblTipsDY_Click);
            // 
            // 标识配置窗体
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.selDeptType);
            this.Controls.Add(this.selCampus);
            this.Controls.Add(this.lblCampus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnOperation);
            this.Controls.Add(this.lblTips);
            this.Controls.Add(this.selConfigName);
            this.Controls.Add(this.pageHeader1);
            this.Name = "标识配置窗体";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标识配置窗体";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Select selConfigName;
        private System.Windows.Forms.Label lblTips;
        private AntdUI.Button btnOperation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private AntdUI.Button btnPageUp;
        private AntdUI.Button btnPageDown;
        private AntdUI.Button btnSelDept;
        private AntdUI.Button btnJieXi;
        private AntdUI.Button btnYanZheng;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rxtSQL;
        private AntdUI.Input input1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rxtConnectionString;
        private AntdUI.Input txtReqNo;
        private System.Windows.Forms.Label label3;
        private AntdUI.Table tabLocalColumns;
        private System.Windows.Forms.Label label4;
        private AntdUI.Table tabOtherColumns;
        private AntdUI.Button btnSave;
        private AntdUI.Button btnReport;
        private System.Windows.Forms.TabPage tabPage3;
        private AntdUI.Button btnSaveConfig;
        private AntdUI.Select selFangWenMiMa;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private AntdUI.Select selFangWenZhangHao;
        private AntdUI.Select selXuanZhuanJiaoDu;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private AntdUI.Select selYaSuoChengDu;
        private AntdUI.Select selFenGeFuHao;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private AntdUI.Select selWenJianGeShi;
        private AntdUI.Select selTiGongFangShi;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private AntdUI.Select selLuJingLeiXing;
        private AntdUI.Button btnJieXiLuJing;
        private AntdUI.Input txtWenJianLuJing;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSqlHelp;
        private System.Windows.Forms.Label lblWjConStr;
        private AntdUI.Select selDeptType;
        private AntdUI.Select selCampus;
        private System.Windows.Forms.Label lblCampus;
        private System.Windows.Forms.Label lblTipsDY;
    }
}