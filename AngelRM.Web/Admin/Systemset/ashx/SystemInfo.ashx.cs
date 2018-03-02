using System;
using System.Collections.Generic;
using System.Web;
using AngelRM.Core;

namespace AngelRM.Web.Admin.Systemset.ashx
{
    /// <summary>
    /// SystemInfo 的摘要说明
    /// </summary>
    public class SystemInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Business.Angel_System_Parameter PBLL = new Business.Angel_System_Parameter();
            string ActionName = context.Request.Params["action"];
            //根据相应操作进行处理
            switch (ActionName)
            {
                case "fromJson":
                    AngelRM.Model.Angel_Siteconfig WebSiteInfo = new Business.Angel_Siteconfig().loadConfig();
                    string fjson = Angel_DataJson.Object2Json<AngelRM.Model.Angel_Siteconfig>(WebSiteInfo);
                    context.Response.Write(fjson);
                    break;
                case "Savewebsiteinfo":
                    AngelRM.Model.Angel_Siteconfig SiteInfo = new Model.Angel_Siteconfig();
                    SiteInfo.Websitename = context.Request.Params["Websitename"];
                    SiteInfo.Websiteurl = context.Request.Params["Websiteurl"];
                    SiteInfo.Websitelogo = context.Request.Params["Websitelogo"];
                    SiteInfo.Websitecompany = context.Request.Params["Websitecompany"];
                    SiteInfo.Websiteaddress = context.Request.Params["Websiteaddress"];
                    SiteInfo.Websitetel = context.Request.Params["Websitetel"];
                    SiteInfo.Websitefax = context.Request.Params["Websitefax"];
                    SiteInfo.Websitemail = context.Request.Params["Websitemail"];
                    SiteInfo.Websitetitle = context.Request.Params["Websitetitle"];
                    SiteInfo.Websitedescription = context.Request.Params["Websitedescription"];
                    SiteInfo.Websitecopyright = context.Request.Params["Websitecopyright"];
                    SiteInfo.Websitepath = context.Request.Params["Websitepath"];
                    SiteInfo.Websiteadminpath = context.Request.Params["Websiteadminpath"];
                    SiteInfo.Islogstatus = Convert.ToInt32(context.Request.Params["Islogstatus"]);
                    SiteInfo.Websitestatus = Convert.ToInt32(context.Request.Params["Websitestatus"]);
                    SiteInfo.Websiteclosereason = context.Request.Params["Websiteclosereason"];
                    AngelRM.Business.Angel_Siteconfig bll = new Business.Angel_Siteconfig();
                    bll.saveConifg(SiteInfo);
                    context.Response.Write("{\"success\":true}");
                    break;

                case "AddSysparameter":
                    AngelRM.Model.Angel_System_Parameter parameter = new Model.Angel_System_Parameter();
                    parameter.ParaID = context.Request.Params["ParaID"];
                    parameter.ParaName = context.Request.Params["ParaName"];
                    parameter.Data = context.Request.Params["hidData"];
                    if (parameter.ParaID == "" || parameter.ParaID == null || parameter.ParaName == "" || parameter.ParaName == null || parameter.Data == "" || parameter.Data == null)
                    {
                        context.Response.Write("{\"success\":false}");
                    }
                    bool isParaID = PBLL.IsParaIDDataExist(parameter.ParaID);
                    if (!isParaID)
                    {
                        bool iscount = PBLL.Add(parameter);
                        if (iscount)
                            context.Response.Write("{\"success\":true}");
                        else
                            context.Response.Write("{\"success\":false}");
                    }
                    else
                    {
                        context.Response.Write("{\"success\":false}");
                    }
                    break;

                case "UpSysparameter":
                    AngelRM.Model.Angel_System_Parameter parameteru = new Model.Angel_System_Parameter();
                    parameteru.id = Convert.ToInt32(context.Request.Params["id"]);
                    parameteru.ParaID = context.Request.Params["ParaID"];
                    parameteru.ParaName = context.Request.Params["ParaName"];
                    parameteru.Data = context.Request.Params["hidData"];
                    parameteru.IsView = "1";
                    if (parameteru.id < 1 || parameteru.ParaID == null || parameteru.ParaName == "" || parameteru.ParaName == null || parameteru.Data == "" || parameteru.Data == null)
                    {
                        context.Response.Write("{\"success\":false}");
                    }
                    bool isupdate = PBLL.Update(parameteru);
                    if (isupdate)
                    {
                        context.Response.Write("{\"success\":true}");
                    }
                    else
                    {
                        context.Response.Write("{\"success\":false}");
                    }
                    break;

                case "DelDB":
                    string idd = context.Request.Params["id"];
                    if (idd != "")
                    {
                        bool isdelete = PBLL.Delete(idd);
                        if (isdelete)
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