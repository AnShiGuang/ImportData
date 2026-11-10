using SqlSugar;

namespace 批量导入程序重制版.TiJian4
{
    /// <summary>
    /// 申请单明细（只映射外接数据查询所需字段）
    /// </summary>
    [SugarTable("ry_ShenQingDanMingXi")]
    public partial class ry_ShenQingDanMingXi
    {
        [SugarColumn(IsIdentity = true)]
        public int tid { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string TiJianHao { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string TiaoMaHao { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string ZuHeXiangMuID { get; set; }

        public string ZuHeXiangMuMingCheng { get; set; }
        public string KeShiID { get; set; }
    }
}
