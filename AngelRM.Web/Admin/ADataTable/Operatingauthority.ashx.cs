using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using AngelRM.PageBase;
using AngelRM.Business;

namespace AngelRM.Web.Admin.ADataTable
{
    /// <summary>
    /// Operatingauthority 的摘要说明
    /// </summary>
    public class Operatingauthority : AdminPage, IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            AdminPage managerinfo = new AdminPage();
            Business.Angel_Admin_Roleoperate bll = new Angel_Admin_Roleoperate();
            string action = context.Request.Params["action"];
            string RoleID = managerinfo.GetAdminInfo().RoleId.ToString();
            string NavlistName = context.Request.Params["NavlistName"];
            if (action == "GetOdata")
            {

                Model.Angel_Admin_Roleoperate objectmobel = null;
                if (RoleID != "")
                {
                    objectmobel = bll.GetModelWhere(NavlistName, Convert.ToInt32(RoleID));
                }
                string OperateList = objectmobel.Operation_Value;
                if (OperateList.Length > 0)
                {
                    string[] OperateName = OperateList.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string OperateString = string.Empty;
                    OperateString += "";
                    foreach (string item in OperateName)
                    {
                        switch (item)
                        {
                            case "Add":
                                OperateString += "{\"text\":\"新增\",\"iconCls\":\"icon-add\",\"handler\":function () { toolBarAddClick(); }},";
                                OperateString += "'-',";
                                break;
                            case "Query":
                                OperateString += "{\"text\":\"查询\",\"iconCls\":\"icon-search\",\"handler\":function () { toolBarSearchClick(); }},";
                                OperateString += "'-',";
                                break;
                            case "Refresh":
                                OperateString += "{\"text\":\"刷新\",\"iconCls\":\"icon-reload\",\"handler\":function () { toolBarReloadClick(); }},";
                                OperateString += "'-',";
                                break;
                            case "Export":
                                OperateString += "{\"text\":\"导出\",\"iconCls\":\"icon-redo\",\"handler\":function () { toolBarExportClick(); }},";
                                OperateString += "'-',";
                                break;
                        }
                    }
                    if (OperateString.Length != 0)
                        OperateString = OperateString.Substring(0, OperateString.Length - 1);
                    context.Response.Write("{\"success\":true,\"operatevalue\":[" + OperateString + "],\"operate\":\"" + OperateList + "\"}");
                }
                else 
                {
                    context.Response.Write("{\"success\":false,\"operatevalue\":\"\",\"operate\":\"\"}");
                }
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