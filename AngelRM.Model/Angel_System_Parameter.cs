using System;
namespace AngelRM.Model
{
    /// <summary>
    /// Angel_System_Parameter:实体类(系统参数表)
    /// </summary>
    [Serializable]
    public partial class Angel_System_Parameter
    {
        public Angel_System_Parameter()
        { }
        #region Model
        //对象属性
        /// <summary>
        /// ID编号
        /// </summary>
        private int _id;
        public int id { get { return _id; } set { _id = value; } }
        /// <summary>
        /// 参数代码
        /// </summary>
        private string _ParaID;
        public string ParaID { get { return _ParaID; } set { _ParaID = value; } }
        /// <summary>
        /// 参数名称
        /// </summary>
        private string _ParaName;
        public string ParaName { get { return _ParaName; } set { _ParaName = value; } }
        /// <summary>
        /// 参数内容
        /// </summary>
        private string _Data;
        public string Data { get { return _Data; } set { _Data = value; } }
        /// <summary>
        /// 用户设置值
        /// </summary>
        private string _IsView;
        public string IsView { get { return _IsView; } set { _IsView = value; } }

        #endregion Model

    }
}

