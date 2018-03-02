using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AngelRM;
using AngelRM.Core;
//using Angelcms.Web;

namespace AngelRM.Web.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtUserName.Text = AngelUtils.GetCookie("AgeRememberName");
            } 
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string userPwd = txtPassword.Text.Trim();

            if (userName.Equals("") || userPwd.Equals(""))
            {
                msgtip.InnerHtml = "请输入用户名或密码";
                return;
            }
            if (Session["AdminLoginCount"] == null)
            {
                Session["AdminLoginCount"] = 1;
            }
            else
            {
                Session["AdminLoginCount"] = Convert.ToInt32(Session["AdminLoginCount"]) + 1;
            }
            //判断登录错误次数
            if (Session["AdminLoginCount"] != null && Convert.ToInt32(Session["AdminLoginCount"]) > 5)
            {
                msgtip.InnerHtml = "错误超过5次，关闭浏览器重新登录！";
                return;
            }


            Business.Angel_Admin bll = new Business.Angel_Admin();
            
            Model.Angel_Admin model= bll.GetModel(userName, userPwd);
            if (model == null)
            {
                msgtip.InnerHtml = "用户名或密码有误，请重试！";
                return;
            }
              Session[AngelConst.ANGEL_SESSION_ADMIN] = model;
              Session.Timeout = 45;
            //写入登录日志
              Model.Angel_Siteconfig SiteconfigInfo =new Business.Angel_Siteconfig().loadConfig();
              if (SiteconfigInfo.Islogstatus > 0)
              {
                  Model.Angel_Admin_log adminlog = new Model.Angel_Admin_log();
                  adminlog.AdminId = model.ID;
                  adminlog.AdminName = model.LoginName;
                  adminlog.OperateIP =AngelRequest.GetIP();
                  adminlog.Operate_Value =AngelActionName.ActionName.Login.ToString();
                  adminlog.Explain = "用户登录";
                  adminlog.AddTime = DateTime.Now;
                  Business.Angel_Admin_log blllog = new Business.Angel_Admin_log();
                  bool aaa =blllog.Add(adminlog);
                 // new Business.Angel_Admin_log().Add(adminlog);
              }
            //写入Cookies记住用户名
            if (this.Issavepwd.Checked == true)
            {
                AngelUtils.WriteCookie("AgeRememberName", model.UserName, 16000);
            }
            else
            {
                AngelUtils.WriteCookie("AgeRememberName", "", 16000);
            }
            AngelUtils.WriteCookie("AgeRememberName", model.UserName, 16000);
            AngelUtils.WriteCookie("AdminName", "AngelRM", model.UserName);
            Response.Redirect("MainFrame/Main.aspx");
            return;
        }
    }
}