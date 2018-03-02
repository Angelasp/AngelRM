using System;
using System.Collections.Generic;
using System.Text;
using AngelRM.Model;
using AngelRM.Core;
using System.Web.SessionState;
using System.Web.UI;
using AngelRM.Business;


namespace AngelRM.PageBase
{
    public class AdminPage : System.Web.UI.Page
    {


        protected internal Model.Angel_Siteconfig AngelSiteconfig;

        public AdminPage()
        {
            this.Load += new EventHandler(AdminPage_Load);
            AngelSiteconfig = new Business.Angel_Siteconfig().loadConfig();
        }


        private void AdminPage_Load(object sender, EventArgs e)
        {
            //判断管理员是否登录
            if (!IsAdminLogin())
            {
                Response.Write("<script>parent.location.href='" + AngelSiteconfig.Websitepath + AngelSiteconfig.Websiteadminpath+ "/login.aspx'</script>");
                Response.End();
            }
        }
        #region 管理员============================================
        /// <summary>
        /// 判断管理员是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsAdminLogin()
        {
            //如果Session为Null
            if (Session[AngelConst.ANGEL_SESSION_ADMIN] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string adminname = AngelUtils.GetCookie("AdminName", "AngelRM");
                string adminpwd = AngelUtils.GetCookie("AdminPwd", "AngelRM");
                if (adminname != "" && adminpwd != "")
                {
                   Business.Angel_Admin bll = new Business.Angel_Admin();
                   Model.Angel_Admin model = bll.GetModel(adminname, adminpwd);
                    if (model != null)
                    {
                       Session[AngelConst.ANGEL_SESSION_ADMIN] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得管理员信息
        /// </summary>
        public Model.Angel_Admin GetAdminInfo()
        {
            if (IsAdminLogin())
            {
                Model.Angel_Admin model = Session[AngelConst.ANGEL_SESSION_ADMIN] as Model.Angel_Admin;
                if (model != null)
                {
                    return model;
                }
            }
            return null;
        }

        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="nav_name">菜单名称</param>
        /// <param name="action_type">操作类型</param>
        public void ChkAdminLevel(string nav_name, string action_type)
        {
            Model.Angel_Admin model = GetAdminInfo();
            Business.Angel_Admin_Role bll = new Business.Angel_Admin_Role();
            //bool result = bll.Exists(model.role_id, nav_name, action_type);

            //if (!result)
            //{
            //    string msgbox = "parent.jsdialog(\"错误提示\", \"您没有管理该页面的权限，请勿非法进入！\", \"back\", \"Error\")";
            //    Response.Write("<script type=\"text/javascript\">" + msgbox + "</script>");
            //    Response.End();
            //}
        }

        /// <summary>
        /// 写入管理日志
        /// </summary>
        /// <param name="action_type"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool AddAdminLog(string action_type, string remark)
        {
            if (AngelSiteconfig.Islogstatus > 0)
            {
                Model.Angel_Admin model = GetAdminInfo();
                Model.Angel_Admin_log adminlog = new Model.Angel_Admin_log();
                adminlog.AdminId = model.ID;
                adminlog.AdminName = model.LoginName;
                adminlog.OperateIP = AngelRequest.GetIP();
                adminlog.Operate_Value = action_type;
                adminlog.Explain = remark;
                bool issuccess= new Business.Angel_Admin_log().Add(adminlog);
               if (issuccess)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

    }
}
