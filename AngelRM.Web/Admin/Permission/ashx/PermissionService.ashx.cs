using System;
using System.Collections.Generic;
using System.Web;

namespace AngelRM.Web.Admin.Permission.ashx
{
    /// <summary>
    /// PermissionService 的摘要说明
    /// </summary>
    public class PermissionService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            AngelRM.Business.Angel_Admin_Roleoperate BLL = new Business.Angel_Admin_Roleoperate();
            string ActionName = context.Request.Params["action"];

            //根据相应的表进行操作
            switch (ActionName)
            {

                case "Modify":
                    Model.Angel_Admin_Roleoperate modeOperate = new Model.Angel_Admin_Roleoperate();

                    modeOperate.ID = Convert.ToInt32(context.Request.Params["id"]);
                    string roleid = context.Request.Params["RoleId"].ToString();
                    modeOperate.RoleId = Convert.ToInt32(context.Request.Params["RoleId"]);
                    modeOperate.NavidName = context.Request.Params["NavName"];
                    modeOperate.Operation_Value = context.Request.Params["Operation_Value"];
                    //modeOperate.IsView = Convert.ToInt32(context.Request.Params["IsView"]);
                    modeOperate.IsView = 1;

                    if (modeOperate.ID <= 0 || modeOperate.Operation_Value == "" || modeOperate.Operation_Value == null || modeOperate.RoleId <= 0 || modeOperate.RoleId == null || modeOperate.IsView == null)
                    {
                        context.Response.Write("{\"success\":false}");
                        return;
                    }
                    if (BLL.Update(modeOperate))
                    {
                        context.Response.Write("{\"success\":true}");
                    }
                    else
                    {
                        context.Response.Write("{\"success\":false}");
                    }
                    break;

                case "DelDB":
                    string id = context.Request.Params["id"];
                    if (BLL.Delete(id))
                    {
                        context.Response.Write("{\"success\":true}");
                    }
                    else
                    {
                        context.Response.Write("{\"success\":false}");
                    }
                    break;

                case "OperationData":
                    //获取系统自带所有操作权限
                    string NavName = context.Request.Params["NavName"];
                    Business.Angel_Admin_Navigation bnav = new Business.Angel_Admin_Navigation();
                    string OperationValue = bnav.GetOperationValueString(NavName);
                    string[] arr = OperationValue.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    context.Response.ContentType = "text/plain";
                    string text = string.Empty;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        text += "{\"id\":\"" + arr[i] + "\",\"text\":\"" + arr[i] + "\"},";
                    }
                    if (text.Length > 0)
                        text = text.Substring(0, text.Length - 1);
                    context.Response.Write("[" + text + "]");
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