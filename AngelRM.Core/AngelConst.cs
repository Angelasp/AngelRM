using System;
using System.Collections.Generic;
using System.Text;

namespace AngelRM.Core
{
  public class AngelConst
    {
        //系统版本
        /// <summary>
        /// 版本号全称
        /// </summary>
        public const string ANGELSYS_VERSION = "1.2";
        /// <summary>
        /// 版本年号
        /// </summary>
        public const string ANGELSYS_YEAR = "2014";

        //Session=====================================================
        /// <summary>
        /// 网页验证码
        /// </summary>
        public const string ANGEL_SESSION_CODE  = "Angel_session_code";
        /// <summary>
        /// 后台管理员
        /// </summary>
        public const string ANGEL_SESSION_ADMIN = "Angel_session_admin";
        /// <summary>
        /// 会员用户
        /// </summary>
        public const string ANGEL_SESSION_USER  = "Angel_session_user";


        //文件路径======================================================
        /// <summary>
        /// 站点基本信息配置文件名
        /// </summary>
        public const string ANGELXML_FILE_SITECONFING = "AngelSiteconfig";
        /// <summary>
        /// URL配置文件名
        /// </summary>
        public const string FILE_URL_XML_CONFING = "Urlspath";
        /// <summary>
        /// 用户配置文件名
        /// </summary>
        public const string FILE_USER_XML_CONFING = "Userpath";
        /// <summary>
        /// 订单配置文件名
        /// </summary>
        public const string FILE_ORDER_XML_CONFING = "Orderpath";

        //文件路径结束==================================================




        //缓存参数配置 Cache======================================================
        /// <summary>
        /// 站点配置
        /// </summary>
        public const string ANGEL_CACHE_SITECONFIG = "Angel_cache_Siteconfig";
        /// <summary>
        /// 用户配置
        /// </summary>
        public const string CACHE_USER_CONFIG = "Age_cache_user_config";
        /// <summary>
        /// 订单配置
        /// </summary>
        public const string CACHE_ORDER_CONFIG = "Age_cache_order_config";
        /// HttpModule映射类
        /// </summary>
        public const string CACHE_SITE_HTTP_MODULE = "Age_cache_http_module";
        /// <summary>
        /// 绑定域名
        /// </summary>
        public const string CACHE_SITE_HTTP_DOMAIN = "Age_cache_http_domain";
        /// <summary>
        /// 站点一级目录名
        /// </summary>
        public const string CACHE_SITE_DIRECTORY = "Age_cache_site_directory";
        /// <summary>
        /// 站点ASPX目录名
        /// </summary>
        public const string CACHE_SITE_ASPX_DIRECTORY = "Age_cache_site_aspx_directory";
        /// <summary>
        /// URL重写映射表
        /// </summary>
        public const string CACHE_SITE_URLS = "Age_cache_site_urls";
        /// <summary>
        /// URL重写LIST列表
        /// </summary>
        public const string CACHE_SITE_URLS_LIST = "Age_cache_site_urls_list";
        /// <summary>
        /// 升级通知
        /// </summary>
        public const string CACHE_OFFICIAL_UPGRADE = "Age_official_upgrade";
        /// <summary>
        /// 官方消息
        /// </summary>
        public const string CACHE_OFFICIAL_NOTICE = "Age_official_notice";

        //Cookies=====================================================
        /// <summary>
        /// 防重复顶踩KEY
        /// </summary>
        public const string ANGEL_COOKIE_DIGG_KEY = "Angel_cookie_digg_key";
        /// <summary>
        /// 防重复评论KEY
        /// </summary>
        public const string ANGEL_COOKIE_COMMENT_KEY = "Angel_cookie_comment_key";
        /// <summary>
        /// 防止下载重复扣各分
        /// </summary>
        public const string ANGEL_COOKIE_DOWNLOAD_KEY = "Angel_download_attach_key";
        /// <summary>
        /// 记住会员用户名
        /// </summary>
        public const string ANGEL_COOKIE_USER_NAME_REMEMBER = "Angel_cookie_user_name_remember";
        /// <summary>
        /// 记住会员密码
        /// </summary>
        public const string ANGEL_COOKIE_USER_PWD_REMEMBER = "Angel_cookie_user_pwd_remember";
        /// <summary>
        /// 购物车
        /// </summary>
        public const string ANGEL_COOKIE_SHOPPING_CART = "Angel_cookie_shopping_cart";
        /// <summary>
        /// 返回上一页
        /// </summary>
        public const string ANGEL_COOKIE_URL_REFERRER = "Angel_cookie_url_referrer";




    }
}
