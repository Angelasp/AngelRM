using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using AngelRM;
using AngelRM.Core;

namespace AngelRM.Web.Admin.ADataTable
{
    /// <summary>
    /// ADataTable_Data 的摘要说明
    /// </summary>
    public class ADataTable_Data : IHttpHandler, IRequiresSessionState
    {

        /// <summary>
        /// 处理客户端请求
        /// </summary>
        /// <param name="context">请求参数</param>
        public void ProcessRequest(HttpContext context)
        {
            string tableName = context.Request.Params["tableName"];               //表名
            string modelClassName = context.Request.Params["modelClassName"];     //类名
            if ((tableName == null || tableName == "") ||
                (modelClassName == null || modelClassName == ""))
            {
                //写dataGrid数据
                context.Response.ContentType = "text/plain";
                context.Response.Clear();
                context.Response.Write("{total:0,rows:[]}");  //写空数据
                return;
            }
            //context.Response.Write("<script>alert('"+tableName+"');</script>");

            AngelData TestData = new AngelData(tableName, modelClassName);
            //分页控件上传的参数
            int pageNo = int.Parse(context.Request.Params["pageNo"]);
            int pageSize = int.Parse(context.Request.Params["pageSize"]);

            //查询条件
            string str_querryConditon = context.Request.Params["querryCondition"];

            if ((str_querryConditon == "") || (str_querryConditon == null))
                TestData.SetConditionAll();   //this.ConditionStr = "1=1";
            else
            {
                //对PC栏目菜单按照排序号排序
                if (tableName == "t_entpcpermission" || tableName == "t_enttermpermission")
                {
                    TestData.SetConditionString(str_querryConditon + " order by PermissionNo desc");
                }
                else
                {
                    TestData.SetConditionString(str_querryConditon);
                }
            }
            // 在表中增加企业条件
            TestData.GetObjIdList(modelClassName);  // 获得实体的id列表
            if (TestData.lst_colId.Contains("Corp_ID") && tableName != "t_corp" && tableName != "t_nodeurl")   // id中含有“Corp_ID”
            //if (tableName != "t_corp" && tableName != "t_actiontype" && tableName != "v_actiontype" && tableName != "t_entpcpermission" && tableName != "t_enttermpermission" && tableName != "t_superpcpermission" && tableName != "v_nodetype" && tableName != "t_flowstatus")
            {
                string str_Corp_ID = context.Session["Corp_ID"] == null ? "" : context.Session["Corp_ID"].ToString();
                TestData.AddCondition("Corp_ID", str_Corp_ID);
            }
            // 排序条件
            string str_orderBy = context.Request.Params["orderBy"];


            //if (tableName == "t_depart")
            //{
            //    string str_Corp_ID = Session["Corp_ID"] == null ? "" : Session["Corp_ID"].ToString();
            //    string id = context.Request.Params["nodeid"];
            //    List<string> list = new List<string>();
            //    getChildrenid(list, tableName, id, str_Corp_ID);
            //    if (list.Count > 0)
            //    {
            //        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
            //        string ResJsonStr = JsonConvert.SerializeObject(list, Formatting.Indented, timeConverter);
            //        TestData.AddConditionString("id in (" + ResJsonStr.Replace("[", "").Replace("]", "") + ")");
            //    }
            //}
            // TestData
            //获得分页数据
            int total = TestData.GetDataSetPage(pageNo, pageSize);
            //数据集==>json串
            TestData.GetDsJson(total);
            //写dataGrid数据
            context.Response.ContentType = "text/plain";
            context.Response.Clear();
            context.Response.Write(TestData.strDsJson);

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