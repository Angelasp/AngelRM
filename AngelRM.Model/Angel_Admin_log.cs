using System;
namespace AngelRM.Model
{
	/// <summary>
	/// Angel_Admin_log:实体类(管理员所有操作日志)
	/// </summary>
	[Serializable]
	public partial class Angel_Admin_log
	{
		public Angel_Admin_log()
		{}
		#region Model
		private int _id;
        private int _adminid;
		private string _adminname;
		private string _operateip;
		private string _operate_value;
		private string _explain;
		private DateTime? _addtime;
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
        public int AdminId
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string AdminName
		{
			set{ _adminname=value;}
			get{return _adminname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperateIP
		{
			set{ _operateip=value;}
			get{return _operateip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Operate_Value
		{
			set{ _operate_value=value;}
			get{return _operate_value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Explain
		{
			set{ _explain=value;}
			get{return _explain;}
		}
		/// <summary>
		/// 
		/// </summary>
        public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		#endregion Model

	}
}

