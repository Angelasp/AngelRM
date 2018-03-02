using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace AngelRM.Web.Admin.Log.ashx
{
    /// <summary>
    /// NavigationService 的摘要说明
    /// </summary>
    public class LogService : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
           // string tableName = context.Request.Params["tableName"];               //表名
            AngelRM.Business.Angel_Admin_log AdminNavBLL = new Business.Angel_Admin_log();
            string ActionName = context.Request.Params["action"];
            
          //  string str_Corp_ID = Session["Corp_ID"] == null ? "" : Session["Corp_ID"].ToString();
            //根据相应的表进行操作
            switch (ActionName)
            {
                case "DelDB":

                        string id = context.Request.Params["id"];
                        if (AdminNavBLL.Delete(id))
                        {
                            context.Response.Write("{\"success\":true}");
                        }
                        else
                        {
                            context.Response.Write("{\"success\":false}");
                        }
                        break;

                case "List":

                    ////查询节点分类列表

                    //    C_xData tcu = new C_xData("t_nodetype", "NodeType");
                    //    int pageNo = int.Parse(context.Request.Params["page"]);
                    //    int pageSize = int.Parse(context.Request.Params["rows"]);
                    //    string wheresql = string.Empty;
                    //    if (context.Request.Params["q"] != "" && context.Request.Params["q"] != null)
                    //    {
                    //        wheresql = string.Format(" nodeName like'{0}%' ", context.Request.Params["q"]);
                    //    }
                    //    else
                    //    {
                    //        wheresql = " 1=1 ";
                    //    }
                    //    tcu.SetConditionString(wheresql);
                    //    //获得分页数据
                    //    int total = tcu.GetDataSetPage(pageNo, pageSize);
                    //    if (total > 0)
                    //    {
                    //        //数据集==>json串
                    //        tcu.GetDsJson(total);
                    //        //写dataGrid数据
                    //        context.Response.ContentType = "text/plain";
                    //        context.Response.Clear();
                    //        context.Response.Write(tcu.strDsJson);

                    //    }
                    //    else
                    //    {
                    //        context.Response.ContentType = "text/plain";
                    //        context.Response.Clear();
                    //        context.Response.Write("{total:0,rows:[]}");  //写空数据
                    //    }
                    
                    ////========end   查询节点分类列表               

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