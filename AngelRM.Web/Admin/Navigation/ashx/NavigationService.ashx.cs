using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Data;
using AngelRM.Core;
using AngelRM.DBHelper;
using Newtonsoft.Json;

namespace AngelRM.Web.Admin.Navigation.ashx
{
    /// <summary>
    /// NavigationService 的摘要说明
    /// </summary>
    public class NavigationService : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            // string tableName = context.Request.Params["tableName"];               //表名
            AngelRM.Business.Angel_Admin_Navigation AdminNavBLL = new Business.Angel_Admin_Navigation();
            string ActionName = context.Request.Params["action"];

            //根据相应的表进行操作
            switch (ActionName)
            {
                case "SaveDB":

                    Model.Angel_Admin_Navigation modeNav = new Model.Angel_Admin_Navigation();
                    modeNav.NavName = context.Request.Params["NavName"];
                    modeNav.TitleName = context.Request.Params["TitleName"];
                    modeNav.NavUrl = context.Request.Params["NavUrl"];
                    modeNav.ParentID = Convert.ToInt32(context.Request.Params["ParentID"]);
                    modeNav.Sequence = Convert.ToInt32(context.Request.Params["Sequence"]);
                    modeNav.Operation_Value = context.Request.Params["Operation_Value"];
                    modeNav.ViewFlag = context.Request.Params["ViewFlag"];
                    modeNav.Remark = context.Request.Params["Remark"];


                    if (modeNav.NavName == "" || modeNav.NavName == null || modeNav.TitleName == "" || modeNav.TitleName == null || modeNav.NavUrl == "" || modeNav.NavUrl == null || modeNav.Sequence == null || modeNav.ParentID == null || modeNav.ViewFlag == "" || modeNav.ViewFlag == null)
                    {
                        context.Response.Write("{\"success\":false}");
                    }


                    if (context.Request.Params["method"] == "add")
                    {
                        modeNav.AddTime = DateTime.Now;
                        bool isNavName = AdminNavBLL.IsNavNameDataExist(modeNav.NavName);
                        if (!isNavName)
                        {
                            bool iscount = AdminNavBLL.Add(modeNav);
                            if (iscount)
                                context.Response.Write("{\"success\":true}");
                            else
                                context.Response.Write("{\"success\":false}");

                            return;
                        }
                        else
                        {
                            context.Response.Write("{\"success\":false}");
                        }

                    }

                    if (context.Request.Params["method"] == "modify")
                    {
                        modeNav.ID = Convert.ToInt32(context.Request.Params["id"]);
                        modeNav.AddTime = DateTime.Now;
                        if (AdminNavBLL.Update(modeNav))
                        {
                            context.Response.Write("{\"success\":true}");
                        }
                        else
                        {
                            context.Response.Write("{\"success\":false}");
                        }

                    }
                    break;

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

                case "NavList":
                    List<tree> treelist = getChildren("Angel_Admin_Navigation", "0");
                    Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                    timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
                    string ResJsonStr = JsonConvert.SerializeObject(treelist, Formatting.Indented, timeConverter);
                    context.Response.ContentType = "text/plain";
                    context.Response.Clear();
                    context.Response.Write(ResJsonStr);
                    break;
                case "OperationData":
                    //获取系统自带所有操作权限
                    Business.Angel_System_Parameter bsp = new Business.Angel_System_Parameter();
                    string Data = bsp.GetDataString("RoleoperateValue");
                    string[] arr = Data.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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


        //Tree递归调用
        public List<tree> getChildren(string tableName, string fid)
        {
            DBHelperSql Dbhelper = new DBHelperSql();
            List<tree> list = new List<tree>();
            if (fid == "0")
            {

                tree tree1 = new tree();
                tree1.id = "0";
                tree1.text = "一级主栏目";
                list.Add(tree1);
            }
            DataTable dt = Dbhelper.GetDataTable(tableName, " ParentId='" + fid + "' and ViewFlag='1' ");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tree tree = new tree();
                    tree.id = dt.Rows[i]["id"].ToString();
                    tree.text = dt.Rows[i]["TitleName"].ToString();
                    tree.children = getChildren(tableName, dt.Rows[i]["id"].ToString());
                    list.Add(tree);
                }
            }
            else
                list = null;

            return list;
        }
        //tree属性
        public class tree
        {
            public string id { get; set; }
            public string text { get; set; }
            public List<tree> children { get; set; }
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