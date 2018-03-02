using System;
namespace AngelRM.Model
{
	/// <summary>
	/// Angel_Admin_Role:实体类(后台管理员权限)
	/// </summary>
	[Serializable]
	public partial class Angel_Admin_Role
	{
		public Angel_Admin_Role()
		{}
		#region Model
		private int _id;
		private string _rolename;
		private int? _roletype;
        private int? _issystem;
		private int? _isworking;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoleName
		{
			set{ _rolename=value;}
			get{return _rolename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoleType
		{
			set{ _roletype=value;}
			get{return _roletype;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? IsSystem
        {
            set { _issystem = value; }
            get { return _issystem; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int? IsWorking
		{
			set{ _isworking=value;}
			get{return _isworking;}
		}
		#endregion Model

	}
}

