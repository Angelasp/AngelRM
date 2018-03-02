using System;
using System.Collections.Generic;
using System.Web;
using AngelRM.Core;

namespace AngelRM.Web.Admin.Admin.ashx
{
    /// <summary>
    /// DataTable_Action 的摘要说明
    /// </summary>
    public class DataTable_Action : IHttpHandler
    {
        /// <summary>
        /// ---------------操作响应-------Server Side-----
        /// 根据上传参数，执行相应操作，返回是否成功
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {

            bool b_result = false;
            //上传的参数
            //data: {tableName:str_tableName, modelClassName:str_modelClassName, actionType: str_actType, actionObj: str_actObj },
            string tableName = context.Request.Params["tableName"];               //表名
            string modelClassName = context.Request.Params["modelClassName"];     //类名
            string action = context.Request.Params["actionType"];       //操作：删除对象、删除列表、添加对象、修改对象
            string targetObject = context.Request.Params["actionObj"];  //对象json或对象数组
            //缺少参数，返回错误
            if ((tableName == null || tableName == "") ||
                (modelClassName == null || modelClassName == ""))
                context.Response.Write("{'success':" + "false" + "}");
            //视图改成表操作
            //V开头的视图改成T开头
            if (tableName.Substring(0, 1) == "v") tableName = "t" + tableName.Substring(1);
            //v开头的模型去掉v
            if (modelClassName.Substring(0, 1) == "v") modelClassName = modelClassName.Substring(1);

            AngelData OperatingData = new AngelData(tableName, modelClassName);     //"Test","Test"--操作数据库数据方法
            context.Response.ContentType = "text/plain";

            //对象是否存在(id)
            if (action == "isObjectExist")
            {
                b_result = OperatingData.ObjectExistById(targetObject);
                // 对于用户表，还要检查同一企业的用户名是否已存在
            }

            string AdminName = AngelUtils.GetCookie("AgeRememberName") == null ? "" : AngelUtils.GetCookie("AgeRememberName").ToString();
            if ((AdminName == "") || (AdminName == null))
            {
                context.Response.Write("{'success':" + "false" + "}");
                return;
            }

            //删除对象(id)
            switch (tableName)
            {
                case "t_product":
                    //Product pro1 = TestData.GetObject_ById<Product>(targetObject);
                    //if (action == "deleteObject")
                    //{
                    //    C_xData quolinedata = new C_xData("t_quoteline", "Quoteline");
                    //    quolinedata.SetConditionString(" Product_ID='" + pro1.Product_ID + "' and Corp_ID='" + str_Corp_ID + "'");
                    //    C_xData orderlinedata = new C_xData("t_orderline", "Orderline");
                    //    orderlinedata.SetConditionString(" Product_ID='" + pro1.Product_ID + "' and Corp_ID='" + str_Corp_ID + "'");
                    //    bool isProduct = quolinedata.Condition_DataExist();
                    //    bool isProduct1 = orderlinedata.Condition_DataExist();
                    //    if (isProduct || isProduct1)
                    //    {
                    //        b_result = false;
                    //    }
                    //    else
                    //    {
                    //        b_result = TestData.DeleteObjectById(targetObject);
                    //    }
                    //}
                    break;

                default:
                    if (action == "deleteObject")
                        b_result = OperatingData.DeleteObjectById(targetObject);
                    break;
            }

            //删除多个对象(id[])
            if (action == "deleteObjects")
                b_result = OperatingData.DeleteObjectsById(targetObject);

                //批量审批多个对象（id[])
                if (action == "reviewObjects")
                    b_result = OperatingData.setObjectsAttributById(targetObject, "Status", "审批通过");

                //审批一个对象
                if (action == "reviewObject")
                    b_result = OperatingData.setObjectAttributById(targetObject, "Status", "审批通过");


            //更新对象(json)
            if (action == "updateObject")
            {
                // 读取Json数据
                Angel_DataJson.getValuelistByJsonString(targetObject, OperatingData.lst_colId, out OperatingData.lst_Value);
                //if (tableName != "t_corp")
                //{
                //    // 写入企业代码
                //    for (int i = 0; i < TestData.lst_colId.Count; i++)
                //        if (TestData.lst_colId[i] == "Corp_ID") TestData.lst_Value[i] = str_Corp_ID;
                //}
                b_result = OperatingData.UpdateData();

            }
            //插入新对象(json)
            if (action == "insertObject")
            {
                //if (TestData.lst_colId[4] == "PassMd5")
                //    TestData.lst_Value[4] = 


                // 读取Json数据
                Angel_DataJson.getValuelistByJsonString(targetObject, OperatingData.lst_colId, out OperatingData.lst_Value);

                // 如果是用户表，检查User_ID是否重名
                bool isUserExist = false;
                string str_user_id = "";



                if (isUserExist)
                    b_result = false;
                else
                    b_result = OperatingData.InsertData();  // 插入数据    
            }
            //返回结果
            if (b_result)
            {
                context.Response.Write("{'success':" + "true" + "}");
            }
            else
            {
                context.Response.Write("{'success':" + "false" + "}");
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