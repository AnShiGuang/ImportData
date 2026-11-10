using SqlSugar;

namespace 批量导入程序重制版.TiJian4
{
    /// <summary>
    /// 体检登记（只映射外接数据查询所需字段）
    /// </summary>
    [SugarTable("ry_TiJianDengJi")]
    public partial class ry_TiJianDengJi
    {
        [SugarColumn(IsIdentity = true)]
        public int tid { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string TiJianHao { get; set; }

        public string XingMing { get; set; }
        public string XingBie { get; set; }
        public int NianLing { get; set; }
        public System.DateTime? TiaoMaZuiZaoDaYinShiJian { get; set; }
    }
}
