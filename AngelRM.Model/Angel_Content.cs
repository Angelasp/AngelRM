using System;
namespace AngelRM.Model
{
	/// <summary>
	/// Angel_Content:实体类(信息内容)
	/// </summary>
	[Serializable]
	public partial class Angel_Content
	{
		public Angel_Content()
		{}
		#region Model
		private int _id;
		private int? _cid;
		private int? _sid;
		private string _title;
		private string _style;
		private string _author;
		private string _source;
		private string _jumpurl;
		private int? _commend;
		private int? _views;
		private int? _diggs;
		private int? _comments;
		private int? _iscomment;
		private string _seo_keywords;
		private string _seo_description;
		private int? _sequence;
		private string _filepath;
		private string _viewpath;
		private string _diyname;
		private DateTime? _create_time;
		private DateTime? _modify_time;
		private int _display;
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
		public int? Cid
		{
			set{ _cid=value;}
			get{return _cid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sid
		{
			set{ _sid=value;}
			get{return _sid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Style
		{
			set{ _style=value;}
			get{return _style;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Author
		{
			set{ _author=value;}
			get{return _author;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Source
		{
			set{ _source=value;}
			get{return _source;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Jumpurl
		{
			set{ _jumpurl=value;}
			get{return _jumpurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Commend
		{
			set{ _commend=value;}
			get{return _commend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Views
		{
			set{ _views=value;}
			get{return _views;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Diggs
		{
			set{ _diggs=value;}
			get{return _diggs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Comments
		{
			set{ _comments=value;}
			get{return _comments;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsComment
		{
			set{ _iscomment=value;}
			get{return _iscomment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Seo_Keywords
		{
			set{ _seo_keywords=value;}
			get{return _seo_keywords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Seo_Description
		{
			set{ _seo_description=value;}
			get{return _seo_description;}
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
		public string FilePath
		{
			set{ _filepath=value;}
			get{return _filepath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ViewPath
		{
			set{ _viewpath=value;}
			get{return _viewpath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Diyname
		{
			set{ _diyname=value;}
			get{return _diyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Create_Time
		{
			set{ _create_time=value;}
			get{return _create_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Modify_Time
		{
			set{ _modify_time=value;}
			get{return _modify_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Display
		{
			set{ _display=value;}
			get{return _display;}
		}
		#endregion Model

	}
}

