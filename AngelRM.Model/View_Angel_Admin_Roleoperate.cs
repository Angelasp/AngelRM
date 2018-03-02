using System;
using System.Collections.Generic;
using System.Text;

namespace AngelRM.Model
{
    /// <summary>
    /// View_Angel_Admin_Roleoperate:实体类(角色操作权限视图表)
    /// </summary>
    [Serializable]
    public partial class View_Angel_Admin_Roleoperate
    {
        public View_Angel_Admin_Roleoperate()
        { }
        #region Model
        private int _id;
        private string _navname;
        private string _navidname;
        private string _titlename;
        private string _navurl;
        private int? _roleid;
        private string _rolename;
        private int? _roletype;
        private string _operation_value;
        private int? _isview;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NavName
        {
            set { _navname = value; }
            get { return _navname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NavIdName
        {
            set { _navidname = value; }
            get { return _navidname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TitleName
        {
            set { _titlename = value; }
            get { return _titlename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NavUrl
        {
            set { _navurl = value; }
            get { return _navurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RoleId
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RoleType
        {
            set { _roletype = value; }
            get { return _roletype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Operation_Value
        {
            set { _operation_value = value; }
            get { return _operation_value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsView
        {
            set { _isview = value; }
            get { return _isview; }
        }
        #endregion Model

    }
}
