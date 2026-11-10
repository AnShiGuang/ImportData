using SqlSugar;

namespace 批量导入程序重制版.TiJian4
{
    /// <summary>
    /// 科室表（只映射选择科室所需字段）
    /// </summary>
    [SugarTable("jcsj_KeShi")]
    public partial class jcsj_KeShi
    {
        [SugarColumn(IsIdentity = true)]
        public int tid { get; set; }

        public string KeShiID { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string KeShiMingCheng { get; set; }

        public string KeShiLeiXing { get; set; }

        public string SuoShuYuanQu { get; set; }
    }
}
