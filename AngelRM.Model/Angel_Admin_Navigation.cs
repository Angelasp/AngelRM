using System;
namespace AngelRM.Model
{
	/// <summary>
	/// Angel_Admin_Navigation:实体类(后台管理导航栏目)
	/// </summary>
	[Serializable]
	public partial class Angel_Admin_Navigation
	{
		public Angel_Admin_Navigation()
		{}
		#region Model
		private int _id;
		private string _navname;
		private string _titlename;
		private string _navurl;
		private string _remark;
		private int? _parentid;
		private string _sortpath;
		private int? _sequence;
		private int? _channelid;
        private string _operation_value;
		private string _viewflag;
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
		public string NavName
		{
			set{ _navname=value;}
			get{return _navname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TitleName
		{
			set{ _titlename=value;}
			get{return _titlename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NavUrl
		{
			set{ _navurl=value;}
			get{return _navurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SortPath
		{
			set{ _sortpath=value;}
			get{return _sortpath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ChannelId
		{
			set{ _channelid=value;}
			get{return _channelid;}
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
		public string ViewFlag
		{
			set{ _viewflag=value;}
			get{return _viewflag;}
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

