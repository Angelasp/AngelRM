using System;
using System.Collections.Generic;
using System.Text;

namespace AngelRM.Model
{
    /// <summary>
    /// 站点配置实体类
    /// </summary>
    [Serializable]
    public class Angel_Siteconfig
    {
        public Angel_Siteconfig() { }
        //系统基本信息
        private string _Websitename = "";
        private string _Websiteurl = "";
        private string _Websitelogo = "";
        private string _Websitecompany = "";
        private string _Websiteaddress = "";
        private string _Websitetel = "";
        private string _Websitefax = "";
        private string _Websitemail = "";
        private string _Websitecrod = "";
        private string _Websitetitle = "";
        private string _Websitekeyword = "";
        private string _Websitedescription = "";
        private string _Websitecopyright = "";
        //系统设置
        private string _Websitepath = "";
        private string _Websiteadminpath = "";
        private int    _Islogstatus = 0;
        private int    _Websitestatus = 1;
        private string _Websiteclosereason = "";
        private string _Websitecountcode = "";
        //系统初始化设置
        private string _sysdatabaseprefix = "Angel_";
        private string _sysencryptstring = "AngelRM";

        #region 系统基本信息==================================
        /// <summary>
        /// 系统名称
        /// </summary>
        public string Websitename
        {
            get { return _Websitename; }
            set { _Websitename = value; }
        }
        /// <summary>
        /// 系统域名
        /// </summary>
        public string Websiteurl
        {
            get { return _Websiteurl; }
            set { _Websiteurl = value; }
        }
        /// <summary>
        /// 系统LOGO
        /// </summary>
        public string Websitelogo
        {
            get { return _Websitelogo; }
            set { _Websitelogo = value; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Websitecompany
        {
            get { return _Websitecompany; }
            set { _Websitecompany = value; }
        }
        /// <summary>
        /// 通讯地址
        /// </summary>
        public string Websiteaddress
        {
            get { return _Websiteaddress; }
            set { _Websiteaddress = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Websitetel
        {
            get { return _Websitetel; }
            set { _Websitetel = value; }
        }
        /// <summary>
        /// 传真号码
        /// </summary>
        public string Websitefax
        {
            get { return _Websitefax; }
            set { _Websitefax = value; }
        }
        /// <summary>
        /// 管理员邮箱
        /// </summary>
        public string Websitemail
        {
            get { return _Websitemail; }
            set { _Websitemail = value; }
        }

        /// <summary>
        /// 系统首页标题
        /// </summary>
        public string Websitetitle
        {
            get { return _Websitetitle; }
            set { _Websitetitle = value; }
        }

        /// <summary>
        /// 系统描述
        /// </summary>
        public string Websitedescription
        {
            get { return _Websitedescription; }
            set { _Websitedescription = value; }
        }
        /// <summary>
        /// 系统版权信息
        /// </summary>
        public string Websitecopyright
        {
            get { return _Websitecopyright; }
            set { _Websitecopyright = value; }
        }
        #endregion

        #region 功能权限设置==================================
        /// <summary>
        /// 系统安装目录
        /// </summary>
        public string Websitepath
        {
            get { return _Websitepath; }
            set { _Websitepath = value; }
        }
        /// <summary>
        /// 系统管理目录
        /// </summary>
        public string Websiteadminpath
        {
            get { return _Websiteadminpath; }
            set { _Websiteadminpath = value; }
        }
        /// <summary>
        /// 系统管理日志
        /// </summary>
        public int Islogstatus
        {
            get { return _Islogstatus; }
            set { _Islogstatus = value; }
        }
        /// <summary>
        /// 是否关闭系统
        /// </summary>
        public int Websitestatus
        {
            get { return _Websitestatus; }
            set { _Websitestatus = value; }
        }
        /// <summary>
        /// 关闭原因描述
        /// </summary>
        public string Websiteclosereason
        {
            get { return _Websiteclosereason; }
            set { _Websiteclosereason = value; }
        }

        #endregion

        #region  系统初始化设置================================
        /// <summary>
        /// 数据库表前缀
        /// </summary>
        public string sysdatabaseprefix
        {
            get { return _sysdatabaseprefix; }
            set { _sysdatabaseprefix = value; }
        }
        /// <summary>
        /// 加密字符串
        /// </summary>
        public string sysencryptstring
        {
            get { return _sysencryptstring; }
            set { _sysencryptstring = value; }
        }
        #endregion


    }


}
