using System;
namespace AngelRM.Model
{
	/// <summary>
	/// Angel_Channel:实体类(信息分类)
	/// </summary>
	[Serializable]
	public partial class Angel_Channel
	{
		public Angel_Channel()
		{}
		#region Model
		private int _id;
		private int? _cid;
		private int? _fatherid;
		private int? _childid;
		private int? _childids;
		private int? _deeppath;
		private string _name;
		private string _seo_title;
		private string _seo_keywords;
		private string _seo_description;
		private string _sequence;
		private string _tablename;
		private string _domain;
		private int? _outsidelink;
		private string _templatechannel;
		private string _templateclass;
		private string _templateview;
		private string _rulechannel;
		private string _ruleview;
		private string _picture;
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
		public int? FatherID
		{
			set{ _fatherid=value;}
			get{return _fatherid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ChildID
		{
			set{ _childid=value;}
			get{return _childid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ChildIDs
		{
			set{ _childids=value;}
			get{return _childids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DeepPath
		{
			set{ _deeppath=value;}
			get{return _deeppath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Seo_Title
		{
			set{ _seo_title=value;}
			get{return _seo_title;}
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
		public string Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Domain
		{
			set{ _domain=value;}
			get{return _domain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OutSideLink
		{
			set{ _outsidelink=value;}
			get{return _outsidelink;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Templatechannel
		{
			set{ _templatechannel=value;}
			get{return _templatechannel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Templateclass
		{
			set{ _templateclass=value;}
			get{return _templateclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Templateview
		{
			set{ _templateview=value;}
			get{return _templateview;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rulechannel
		{
			set{ _rulechannel=value;}
			get{return _rulechannel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ruleview
		{
			set{ _ruleview=value;}
			get{return _ruleview;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Picture
		{
			set{ _picture=value;}
			get{return _picture;}
		}
		#endregion Model

	}
}

