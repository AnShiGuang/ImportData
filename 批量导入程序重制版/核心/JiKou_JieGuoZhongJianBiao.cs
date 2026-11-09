using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace 批量导入程序重制版
{
    /// </summary>
    [SugarTable("接口_结果中间表")] // 指定数据库表名
    public class JiKou_JieGuoZhongJianBiao
    {
     
        /// <summary>
        /// 体检编号
        /// </summary>
        [SugarColumn(ColumnName = "体检编号", Length = 50, IsNullable = true)]
        public string TiJianBianHao { get; set; }

        /// <summary>
        /// 条码号
        /// </summary>
        [SugarColumn(ColumnName = "条码号", Length = 50, IsNullable = true)]
        public string TiaoMaHao { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(ColumnName = "姓名", Length = 50, IsNullable = true)]
        public string XingMing { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [SugarColumn(ColumnName = "性别", Length = 10, IsNullable = true)]
        public string XingBie { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [SugarColumn(ColumnName = "年龄", IsNullable = true)]
        public string NianLing { get; set; }
         
        /// <summary>
        /// 组合项目名称
        /// </summary>
        [SugarColumn(ColumnName = "组合项目名称", Length = 200, IsNullable = true)]
        public string ZuHeXiangMuMingCheng { get; set; }

        /// <summary>
        /// 组合项目编号
        /// </summary>
        [SugarColumn(ColumnName = "组合项目编号", Length = 50, IsNullable = true)]
        public string ZuHeXiangMuBianHao { get; set; }

        /// <summary>
        /// 明细项目名称
        /// </summary>
        [SugarColumn(ColumnName = "明细项目名称", Length = 200, IsNullable = true)]
        public string MingXiXiangMuMingCheng { get; set; }

        /// <summary>
        /// 明细项目编号
        /// </summary>
        [SugarColumn(ColumnName = "明细项目编号", Length = 50, IsNullable = true)]
        public string MingXiXiangMuBianHao { get; set; }
        /// <summary>
        /// 检查结果（支持数值/文本）
        /// </summary>
        [SugarColumn(ColumnName = "影像表现", Length = 200, IsNullable = true)]
        public string YingXiangBiaoXian { get; set; }
        /// <summary>
        /// 检查结果（支持数值/文本）
        /// </summary>
        [SugarColumn(ColumnName = "检查结果", Length = 200, IsNullable = true)]
        public string JianChaJieGuo { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [SugarColumn(ColumnName = "单位", Length = 50, IsNullable = true)]
        public string DanWei { get; set; }

        /// <summary>
        /// 参考范围（文本描述，如"0-100"）
        /// </summary>
        [SugarColumn(ColumnName = "范围", Length = 100, IsNullable = true)]
        public string FanWei { get; set; }

        /// <summary>
        /// 参考上限（数值）
        /// </summary>
        [SugarColumn(ColumnName = "上限", IsNullable = true)]
        public string ShangXian { get; set; }

        /// <summary>
        /// 参考下限（数值）
        /// </summary>
        [SugarColumn(ColumnName = "下限", IsNullable = true)]
        public string XiaXian { get; set; }

        /// <summary>
        /// 危急值（文本/数值）
        /// </summary>
        [SugarColumn(ColumnName = "危急值", Length = 100, IsNullable = true)]
        public string WeiJiZhi { get; set; }

        /// <summary>
        /// 异常提示（如"↑""↓"）
        /// </summary>
        [SugarColumn(ColumnName = "异常提示", Length = 20, IsNullable = true)]
        public string YiChangTiShi { get; set; }

        /// <summary>
        /// 是否异常（true=异常，false=正常）
        /// </summary>
        [SugarColumn(ColumnName = "是否异常", IsNullable = true)]
        public string ShiFouYiChang { get; set; }

        /// <summary>
        /// 检查日期
        /// </summary>
        [SugarColumn(ColumnName = "检查日期", IsNullable = true)]
        public string JianChaRiQi { get; set; }

        /// <summary>
        /// 检查医师
        /// </summary>
        [SugarColumn(ColumnName = "检查医师", Length = 50, IsNullable = true)]
        public string JianChaYiShi { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        [SugarColumn(ColumnName = "审核日期", IsNullable = true)]
        public string ShenHeRiQi { get; set; }

        /// <summary>
        /// 审核医师
        /// </summary>
        [SugarColumn(ColumnName = "审核医师", Length = 50, IsNullable = true)]
        public string ShenHeYiShi { get; set; }

        /// <summary>
        /// 文件路径（报告附件路径）
        /// </summary>
        [SugarColumn(ColumnName = "文件路径", Length = 500, IsNullable = true)]
        public string WenJianLuJing { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [SugarColumn(ColumnName = "顺序", IsNullable = true)]
        public string ShunXu { get; set; }
    }
}