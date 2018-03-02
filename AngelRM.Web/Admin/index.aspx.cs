using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AngelRM;
using AngelRM.Model;
using AngelRM.Core;
using AngelRM.PageBase;


namespace AngelRM.Web.Admin
{
    public partial class index : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Model.Angel_Admin admin_info = GetAdminInfo(); //管理员信息
                //登录信息                                     
                if (admin_info != null)
                {
                    Business.Angel_Admin_log bll = new Business.Angel_Admin_log();
                    //Model.Angel_Admin_log model1 = bll.GetModel(admin_info.UserName, 1, AngelActionName.ActionName.Login.ToString());
                    //if (model1 != null)
                    //{
                    //    //本次登录
                    //    litIP.Text = model1.user_ip;
                    //}
                    //Age_manager_log model2 = bll.GetModel(admin_info.user_name, 2, AgeEnums.ActionEnum.Login.ToString());
                    //if (model2 != null)
                    //{
                    //    //上一次登录
                    //    litBackIP.Text = model2.user_ip;
                    //    litBackTime.Text = model2.add_time.ToString();
                    //}
                }


                //LitUpgrade.Text = Utils.GetDomainStr(AgeKeys.CACHE_OFFICIAL_UPGRADE, DESEncrypt.Decrypt(AgeKeys.FILE_URL_UPGRADE_CODE, "Age"));
                //NewsNotice.Text = Utils.GetDomainStr(AgeKeys.CACHE_OFFICIAL_NOTICE, DESEncrypt.Decrypt(AgeKeys.FILE_URL_NOTICE_CODE, "Age"));
                //Utils.GetDomainStr("Age_cache_domain_info", "http://www.angelasp.com/upangelcms.asp?u=" + Request.Url.DnsSafeHost + "&i=" + Request.ServerVariables["LOCAL_ADDR"]);
            }
        




        }
    }
}