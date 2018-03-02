using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Data;
using AngelRM.Core;
using AngelRM.DBHelper;
using Newtonsoft.Json;

namespace AngelRM.Web.Admin.Admin.ashx
{
    /// <summary>
    /// RoleService 的摘要说明
    /// </summary>
    public class RoleService : IHttpHandler, IRequiresSessionState
    {
        AngelRM.Business.Angel_Admin_Role ObjectBLL = new Business.Angel_Admin_Role();               //角色分类
        AngelRM.Business.Angel_Admin_Roleoperate OperateBll = new Business.Angel_Admin_Roleoperate();//操作权限
        public void ProcessRequest(HttpContext context)
        {
            string ActionName = context.Request.Params["action"];
            //根据相应的表进行操作
            switch (ActionName)
            {
                case "SaveDB":
                    Model.Angel_Admin_Role modeobj = new Model.Angel_Admin_Role();
                    modeobj.RoleName = context.Request.Params["RoleName"];
                    modeobj.RoleType = Convert.ToInt32(context.Request.Params["RoleType"]);
                    modeobj.IsSystem = Convert.ToInt32(context.Request.Params["IsSystem"]);
                    modeobj.IsWorking = Convert.ToInt32(context.Request.Params["IsWorking"]);
                    if (modeobj.RoleName == "" || modeobj.RoleName == null || modeobj.RoleType == null || modeobj.IsSystem == null || modeobj.IsWorking == null)
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

                case "UpOpereate":

                    int RoleId = Convert.ToInt32(context.Request.Params["id"]);
                    string RoleValue = context.Request.Params["RoleValue"];
                    string[] RoleListValue;

                    if (RoleId <= 0)
                    {
                        context.Response.Write("{\"success\":false}");
                        return;
                    }

                    //删除以前的权限
                    if (RoleId > 0)
                    {
                        string sqlrole = " RoleId=" + RoleId;
                        DataTable dt = OperateBll.GetAllDataTable(sqlrole);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                Model.Angel_Admin_Roleoperate UpModel = OperateBll.GetModel(row["id"].ToString());
                                UpModel.IsView = 0;
                                OperateBll.Update(UpModel);
                                // OperateBll.Delete(row["id"].ToString());
                            }
                        }
                    }
                    //重新添加权限
                    if (RoleValue != "")
                    {
                        RoleListValue = RoleValue.Split(',');
                        foreach (string rvalue in RoleListValue)
                        {

                            if (OperateBll.IsRoleoperateDataExist(rvalue, RoleId))
                            {
                                Model.Angel_Admin_Roleoperate modelYes = OperateBll.GetModelWhere(rvalue, RoleId);
                                modelYes.IsView = 1;
                                OperateBll.Update(modelYes);
                            }
                            else
                            {
                                Model.Angel_Admin_Roleoperate operatemodel = new Model.Angel_Admin_Roleoperate();
                                operatemodel.RoleId = RoleId;
                                operatemodel.NavidName = rvalue;
                                operatemodel.Operation_Value = "Show,View,Edit,Add,Delete"; //暂定
                                operatemodel.IsView = 1;
                                OperateBll.Add(operatemodel);
                            }
                        }
                    }
                    context.Response.Write("{\"success\":true}");
                    break;
                case "RoleTree":
                    string Rid = context.Request.Params["Rid"];
                    int Roleid = Convert.ToInt32(Rid);
                    List<tree> treelist = getChildren("Angel_Admin_Navigation", "0", Roleid);
                    Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                    timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
                    string ResJsonStr = JsonConvert.SerializeObject(treelist, Formatting.Indented, timeConverter);
                    context.Response.ContentType = "text/plain";
                    context.Response.Clear();
                    context.Response.Write(ResJsonStr);
                    break;
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

                    //查询角色列表
                    DataSet datasetlist = ObjectBLL.GetAllList("1=1");
                    if (datasetlist != null)
                    {
                        context.Response.ContentType = "text/plain";
                        string text = string.Empty; text += "[";
                        text += "{\"id\":0,\"text\":\"" + "请选择角色" + "\"," + "\"selected\":" + "true" + "},";
                        for (int i = 0; i < datasetlist.Tables[0].Rows.Count; i++)
                        {
                            text += "{\"id\":\"" + datasetlist.Tables[0].Rows[i][0] + "\",\"text\":\"" + datasetlist.Tables[0].Rows[i][1] + "\"},";
                        }
                        text = text.Substring(0, text.Length - 1);
                        text += "]";
                        context.Response.Write(text);

                    }
                    else
                    {
                        context.Response.ContentType = "text/plain";
                        context.Response.Clear();
                        context.Response.Write("{ID:1,RoleName:'暂无角色'}");  //写空数据
                    }

                    //========end   查询节点分类列表               

                    break;


                default:
                    context.Response.Write("{\"success\":false}");
                    break;
            }
        }


        //Tree递归调用
        public List<tree> getChildren(string tableName, string fid, int RoleId)
        {
            DBHelperSql Dbhelper = new DBHelperSql();
            List<tree> list = new List<tree>();
            DataTable dt = Dbhelper.GetDataTable(tableName, " ParentId='" + fid + "' ");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tree tree = new tree();
                    tree.id = dt.Rows[i]["NavName"].ToString();
                    tree.text = dt.Rows[i]["TitleName"].ToString();
                    if (OperateBll.IsOkRoleoperateDataExist(dt.Rows[i]["NavName"].ToString(), RoleId))
                    {
                        tree.@checked = true;
                    }
                    tree.children = getChildren(tableName, dt.Rows[i]["id"].ToString(), RoleId);
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
            public bool @checked { get; set; }
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