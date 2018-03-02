using System;
namespace AngelRM.Model
{
	/// <summary>
	/// Angel_Admin_Roleoperate:实体类(权限操作表)
	/// </summary>
	[Serializable]
	public partial class Angel_Admin_Roleoperate
	{
		public Angel_Admin_Roleoperate()
		{}
        #region Model
        private int _id;
        private int? _roleid;
        private string _navidname;
        private string _operation_value;
        private int? _isview;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
        public string NavidName
        {
            set { _navidname = value; }
            get { return _navidname; }
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

