using SqlSugar;

namespace 批量导入程序重制版.TiJian4
{
    /// <summary>
    /// 申请单主表（只映射外接数据查询所需字段）
    /// </summary>
    [SugarTable("ry_ShenQingDanZhu")]
    public partial class ry_ShenQingDanZhu
    {
        [SugarColumn(IsIdentity = true)]
        public int tid { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string TiJianHao { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string TiaoMaHao { get; set; }

        public string ZuHeXiangMuMingChengJiHe { get; set; }
        public string KeShiID { get; set; }
        public System.DateTime ShenQingDanShiJian { get; set; }
    }
}
