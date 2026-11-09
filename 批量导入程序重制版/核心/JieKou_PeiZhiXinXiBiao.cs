using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace 批量导入程序重制版
{ 

    /// <summary>
    /// 接口_配置信息表 实体类
    /// </summary>
    [SugarTable("接口_配置信息表")] // 指定数据库表名
    public class JiKou_PeiZhiXinXiBiao
    {
        /// <summary>
        /// 自增主键（唯一标识配置项）
        /// </summary>
        [SugarColumn(
            IsPrimaryKey = true,
            IsIdentity = true,
            ColumnName = "ID" )]
        public int ID { get; set; }

        /// <summary>
        /// 配置序号
        /// </summary>
        [SugarColumn(
            ColumnName = "配置序号",
            IsNullable = true)]
        public string PeiZhiXuHao { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        [SugarColumn(
            ColumnName = "配置名称",
            Length = 100,
            IsNullable = true)]
        public string PeiZhiMingCheng { get; set; }

 
        /// <summary>
        /// 参数标识
        /// </summary>
        [SugarColumn(
            ColumnName = "参数标识",
            Length = 50,
            IsNullable = true)]
        public string CanShuBiaoZhi { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        [SugarColumn(
            ColumnName = "参数值",
            Length = 5000,
            IsNullable = true)]
        public string CanShuZhi { get; set; }

        /// <summary>
        /// 外接字段名
        /// </summary>
        [SugarColumn(
            ColumnName = "外接字段名",
            Length = 100,
            IsNullable = true)]
        public string WaiJieZiDuanMing { get; set; }

        /// <summary>
        /// 本地字段名
        /// </summary>
        [SugarColumn(
            ColumnName = "本地字段名",
            Length = 100,
            IsNullable = true)]
        public string BenDiZiDuanMing { get; set; }

        /// <summary>
        /// 字段类别
        /// </summary>
        [SugarColumn(
            ColumnName = "字段类别",
            Length = 50,
            IsNullable = true)]
        public string ZiDuanLeiBie { get; set; }

        /// <summary>
        /// 字段顺序
        /// </summary>
        [SugarColumn(
            ColumnName = "字段顺序",
            IsNullable = true)]
        public int? ZiDuanShunXu { get; set; }

        /// <summary>
        /// 字段启用
        /// </summary>
        [SugarColumn(
            ColumnName = "是否启用",
            IsNullable = true)]
        public string ShiFouQiYong { get; set; }


        [SugarColumn(
          ColumnName = "配置启用",
          IsNullable = true)]
        public string PeiZhiQiYong { get; set; }
    }
}
