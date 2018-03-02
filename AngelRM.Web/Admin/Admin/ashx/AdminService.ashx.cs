using System;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web;
using AngelRM.Core;
using AngelRM.PageBase;

namespace AngelRM.Web.Admin.Admin.ashx
{
    /// <summary>
    /// AdminService 的系统管理员操作方法
    /// </summary>
    public class AdminService : AdminPage, IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            AngelRM.Business.Angel_Admin ObjectBLL = new Business.Angel_Admin();
            string ActionName = context.Request.Params["action"];
            //根据相应的表进行操作
            switch (ActionName)
            {
                case "SaveDB":
                    Model.Angel_Admin modeobj = new Model.Angel_Admin();
                    modeobj.RoleId = Convert.ToInt32(context.Request.Params["RoleId"]);
                    modeobj.LoginName = context.Request.Params["LoginName"];
                    modeobj.Password = AngelDESEncrypt.Encrypt(context.Request.Params["Password"]);
                    modeobj.UserName = context.Request.Params["UserName"];
                    modeobj.UserEmail = context.Request.Params["UserEmail"];
                    modeobj.AddTime = DateTime.Now;
                    modeobj.IsWorking = Convert.ToInt32(context.Request.Params["IsWorking"]);

                    if (modeobj.LoginName == "" || modeobj.LoginName == null || modeobj.RoleId == null || modeobj.AddTime == null || modeobj.IsWorking == null)
                    {
                        context.Response.Write("{\"success\":false}");
                    }

                    if (context.Request.Params["method"] == "add")
                    {

                        bool iscount = ObjectBLL.Add(modeobj);
                        if (iscount)
                        {
                            context.Response.Write("{\"success\":true}");
                            return;
                        }
                        else
                        {
                            context.Response.Write("{\"success\":false}");
                        }
                    }

                    if (context.Request.Params["method"] == "modify")
                    {
                        modeobj.ID = Convert.ToInt32(context.Request.Params["id"]);

                        if (ObjectBLL.Update(modeobj))
                        {
                            context.Response.Write("{\"success\":true}");
                        }
                        else
                        {
                            context.Response.Write("{\"success\":false}");
                        }

                    }
                    break;
                //修改管理员密码
                case "UpPwd":
                    AdminPage admininfo = new AdminPage();
                    Model.Angel_Admin modelpwd = admininfo.GetAdminInfo();
                    string OldPassword = AngelDESEncrypt.Encrypt(context.Request.Params["OldPasswrod"]);
                    string NewPassword = AngelDESEncrypt.Encrypt(context.Request.Params["NewPassword"]);

                    if (context.Request.Params["OldPasswrod"] == "" || context.Request.Params["OldPasswrod"] == null || context.Request.Params["NewPassword"] == "" || context.Request.Params["NewPassword"] == null)
                    {
                        context.Response.Write("{\"success\":false}");
                        return;
                    }
                    if (OldPassword == modelpwd.Password)
                    {
                        modelpwd.Password = NewPassword;

                        if (ObjectBLL.Update(modelpwd))
                        {
                            context.Response.Write("{\"success\":true}");
                        }
                        else
                        {
                            context.Response.Write("{\"success\":false}");
                        }

                    }
                    else
                    {
                        context.Response.Write("{\"success\":false}");
                    }
                    break;
                //删除信息
                case "DelDB":

                    string id = context.Request.Params["id"];
                    if (ObjectBLL.Delete(id))
                    {
                        context.Response.Write("{\"success\":true}");
                    }
                    else
                    {
                        context.Response.Write("{\"success\":false}");
                    }
                    break;

                case "List":

                    break;


                default:
                    context.Response.Write("{\"success\":false}");
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}