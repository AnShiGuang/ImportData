using AntdUI;
using System;

namespace 批量导入程序重制版
{
    public class ClassInfo
    {
        public class UserModel : NotifyProperty
        {
            #region 字段（初始化空值，避免NullReferenceException）
            public CellLink[] _cellLinks = new CellLink[0];
            public CellBadge _cellBadge;
            public bool _check;
            public int _id;
            public string _tiJianHao = string.Empty;
            public string _tiaoMaHao = string.Empty;
            public string _name = string.Empty;
            public string _deptName = string.Empty;
            public string _gender = string.Empty;
            public string _age = string.Empty;
            public string _shenQingDanShiJian;
            public string _itemName = string.Empty;
            public string _status = string.Empty;
            #endregion

            #region 属性（优化NotifyPropertyChanged，确保正确触发）
            public bool Check
            {
                get => _check;
                set { _check = value; OnPropertyChanged(nameof(Check)); }
            }

            public int Id
            {
                get => _id;
                set { _id = value; OnPropertyChanged(nameof(Id)); }
            }

            public string TiJianHao
            {
                get => _tiJianHao;
                set { _tiJianHao = value; OnPropertyChanged(nameof(TiJianHao)); }
            }

            public string TiaoMaHao
            {
                get => _tiaoMaHao;
                set { _tiaoMaHao = value; OnPropertyChanged(nameof(TiaoMaHao)); }
            }

            public string Name
            {
                get => _name;
                set { _name = value; OnPropertyChanged(nameof(Name)); }
            }

            public string DeptName
            {
                get => _deptName;
                set { _deptName = value; OnPropertyChanged(nameof(DeptName)); }
            }

            public string Gender
            {
                get => _gender;
                set { _gender = value; OnPropertyChanged(nameof(Gender)); }
            }

            public string Age
            {
                get => _age;
                set { _age = value; OnPropertyChanged(nameof(Age)); }
            }

            public string ShenQingDanShiJian
            {
                get => _shenQingDanShiJian;
                set { _shenQingDanShiJian = value; OnPropertyChanged(nameof(ShenQingDanShiJian)); }
            }

            public string ItemName
            {
                get => _itemName;
                set { _itemName = value; OnPropertyChanged(nameof(ItemName)); }
            }

            public string Status
            {
                get => _status;
                set { _status = value; OnPropertyChanged(nameof(Status)); }
            }

            public CellLink[] CellLinks
            {
                get => _cellLinks;
                set
                {
                    if (_cellLinks == value) return;
                    _cellLinks = value;
                    OnPropertyChanged(nameof(CellLinks));
                }
            }

            public CellBadge CellBadge
            {
                get => _cellBadge;
                set
                {
                    if (_cellBadge == value) return;
                    _cellBadge = value;
                    OnPropertyChanged(nameof(CellBadge));
                }
            }
            #endregion

            #region 构造函数（保留原有逻辑，添加空值安全）
            /// <summary>
            /// 带参数构造函数
            /// </summary>
            /// <param name="check">是否选中</param>
            /// <param name="id">序号</param>
            /// <param name="tiJianHao">体检编号</param>
            /// <param name="tiaoMaHao">条码号</param>
            /// <param name="name">姓名</param>
            /// <param name="gender">性别</param>
            /// <param name="age">年龄</param>
            /// <param name="shenQingDanShiJian">申请单时间</param>
            /// <param name="deptName">科室名称</param>
            /// <param name="itemName">项目名称</param>
            /// <param name="button">按钮</param>
            /// <param name="status">状态</param>
            public UserModel(bool check, int id, string tiJianHao, string tiaoMaHao, string name, string gender, string age,
                string shenQingDanShiJian, string deptName, string itemName, string status)
            {
                Check = check;
                Id = id;
                TiJianHao = tiJianHao ?? string.Empty;
                TiaoMaHao = tiaoMaHao ?? string.Empty;
                Name = name ?? string.Empty;
                Gender = gender ?? string.Empty;
                Age = age ?? string.Empty;
                ShenQingDanShiJian = shenQingDanShiJian;
                DeptName = deptName ?? string.Empty;
                ItemName = itemName ?? string.Empty;
                Status = status ?? string.Empty;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public UserModel() { }
            #endregion
        }

    }
}
