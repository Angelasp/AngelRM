using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AngelRM.DBHelper;
using AngelRM.Core;
using AngelRM.PageBase;

namespace AngelRM.Web.Admin.Fileinfo
{
    public partial class ExportCsv : AdminPage
    {
        int dataCount;
        string listTitle;
        string tableName;
        string strCondition;
        DBHelperSql Agedb = new DBHelperSql();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //读取参数
                listTitle = Request["listTitle"] == null ? "" : Request["listTitle"].ToString();
                tableName = Request["tableName"] == null ? "" : Request["tableName"].ToString();
                strCondition = Request.QueryString["querryString"] == null ? "" : Request.QueryString["querryString"].ToString();

                hidexportfield.Value = Request.Params["exportfield"] == null ? "" : Request.Params["exportfield"];
                hidexporttitle.Value = Request.Params["exporttitle"] == null ? "" : Request.Params["exporttitle"];

                if (strCondition == "" || strCondition == null)
                {
                    strCondition = "1=1";
                }
                else
                    tb_strCondition.Text = strCondition;


                tb_listTitle.Text = listTitle;
                //读取记录数
                if (tableName != "")
                {
                    tb_tableName.Text = tableName;
                    dataCount = Agedb.selectRecordCountByCondition(tableName, strCondition);
                    tb_Count.Text = dataCount.ToString();
                }

            }

        }

        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (int.Parse(tb_Count.Text) > 0)
                {
                    //下载文件
                    tableName = tb_tableName.Text;
                    strCondition = tb_strCondition.Text;
                    string myFielName = this.tb_listTitle.Text;
                    DownloadFile(Response, tableName, myFielName + ".csv", strCondition);
                    Response.End();
                }
                else
                {
                    AngelShowMessage.JscriptMessage("友情提示", "没有" + tb_listTitle.Text + "数据，请先填写数据再导出", this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //
        /// <summary>
        /// 弹出下载框
        /// </summary>
        /// <param name="argResp">弹出页面</param>
        /// <param name="argFileStream">文件流</param>
        /// <param name="strFileName">文件名</param>
        public void DownloadFile(HttpResponse argResp, string str_tableName, string strFileName, string mywhere)
        {
            if (str_tableName == "") return;
            try
            {
                argResp.Clear();
                argResp.Buffer = true;
                argResp.Charset = "GB2312";
                string strResHeader = "attachment; filename=" + Guid.NewGuid().ToString() + ".csv";
                if (!string.IsNullOrEmpty(strFileName))
                {
                    strResHeader = "inline; filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8).ToString(); //文件名
                }
                argResp.AppendHeader("Content-Disposition", strResHeader);//attachment说明以附件下载，inline说明在线打开
                argResp.ContentType = "application/ms-excel";
                argResp.ContentEncoding = Encoding.GetEncoding("GB2312"); // Encoding.UTF8;//
                string data = GetCSV(str_tableName, strCondition);
                argResp.Write(data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 生成表数据的CSV字符串
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="strCondition">条件</param>
        /// <returns>CSV字符串</returns>
        private string GetCSV(string tableName, string strCondition)
        {
            StringBuilder csvStr = new StringBuilder();
            if (tableName == "") return "";                  //得到列名   
            csvStr.Append(hidexporttitle.Value);
            csvStr.Append("\t\n");
            List<List<string>> ll_data = Agedb.GetDataByCondition(tableName, hidexportfield.Value, strCondition);

            for (int i = 0; i < ll_data.Count; i++)
            {
                for (int j = 0; j < ll_data[i].Count; j++)
                {
                    csvStr.Append(@"" + ll_data[i][j].ToString() + ", ");
                }
                csvStr.Remove(csvStr.Length - 1, 1);
                csvStr.Append("\t\n");
            }
            return csvStr.ToString();
        }

    }
}