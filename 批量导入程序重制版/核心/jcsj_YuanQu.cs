using SqlSugar;

namespace 批量导入程序重制版.TiJian4
{
    /// <summary>
    /// 院区表（只映射院区相关字段，用于读取可选院区列表）
    /// </summary>
    [SugarTable("jcsj_YuanQu")]
    public partial class jcsj_YuanQu
    {
        [SugarColumn(IsIdentity = true)]
        public int YuanQuID { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string YuanQuMingCheng { get; set; }
    }
}
