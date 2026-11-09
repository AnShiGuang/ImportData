using SqlSugar;
using System;

namespace 批量导入程序重制版.TiJian4
{
    [SugarTable("AppVersionLog")]
    public class AppVersionLog
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 20)]
        public string VersionStr { get; set; }

        public int MajorVer { get; set; }

        public int MinorVer { get; set; }

        public int PatchVer { get; set; }

        [SugarColumn(Length = 30)]
        public string ReleaseDate { get; set; }

        [SugarColumn(Length = 50, IsNullable = true)]
        public string LogTag { get; set; }

        [SugarColumn(Length = 500)]
        public string LogDescription { get; set; }

        public int SortOrder { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime CreateTime { get; set; }
    }
}
