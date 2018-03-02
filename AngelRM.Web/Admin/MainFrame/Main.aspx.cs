using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using AngelRM;
using AngelRM.PageBase;
using AngelRM.Core;

namespace AngelRM.Web.Admin.MainFrame
{
    public partial class Main : AdminPage
    {
        AngelData OData = new AngelData("Angel_Admin_Roleoperate", "Angel_Admin_Roleoperate");
        AngelData roleData = new AngelData("Angel_Admin_Role", "Angel_Admin_Role");

        public string NavigationList = string.Empty;
        public Model.Angel_Admin model;

        protected void Page_Load(object sender, EventArgs e)
        {
            model = GetAdminInfo(); //取得当前管理员信息
            NavigationList = loadColumn3(model.RoleId.ToString());

        }

        /// <summary>
        /// 载入菜单
        /// </summary>
        /// <param name="RoleId">权限ID</param>
        /// <returns></returns>
        private string loadColumn3(string RoleId)
        {
            StringBuilder columnStr = new StringBuilder();
            string RolelistString = getRoleList(RoleId);
            List<Model.Angel_Admin_Navigation> l_menu = getRoleMenuList(RolelistString, 0);  // 菜单对象列表
            // 得到Navigation表中的顶级菜单列表{内容管理，系统管理}
            //   List<string> l_m1Title = OData.Item_db.GetWholeColValue("Angel_Admin_Navigation", "TitleName", "ParentID=0");
            // 循环添加顶级菜单及子项
            for (int i = 0; i < l_menu.Count; i++)
            {
                // 分类菜单下的功能数
                int functionCount = l_menu.Count;//Count(t => t.m1Title == l_m1Title[i]);
                if (functionCount == 0) continue;   // 下一分类
                // 添加分类
                columnStr.Append("<div title= '" + l_menu[i].TitleName + "' style= \"overflow:auto;padding:1px;color：#a40000;width:180\">");
                //columnStr.Append(l_m1Title[i] + "\r\n");

                List<Model.Angel_Admin_Navigation> cl_menus = getRoleMenuList(RolelistString, l_menu[i].ID);
                if (cl_menus.Count > 0)
                {
                    foreach (Model.Angel_Admin_Navigation menus2 in cl_menus)
                    {
                        columnStr.Append("<ul class='menus'><li><a href='#' ");
                        columnStr.Append(" onclick=\"addTab('"
                                + menus2.TitleName + "','"
                                + menus2.NavName + "','"
                                + menus2.NavUrl + "')\"");
                        columnStr.Append(">" + menus2.TitleName + " </a></li></ul>");
                    }
                    // columnStr.Append("<ul class='menuDirectory'><li><a href='#'>" + l_menu[i].TitleName + "</a><ul>");
                }
                //else 
                //{


                //}
                // 分类结束
                columnStr.Append("</div>");
            }
            return columnStr.ToString();
        }

        /// <summary>
        /// 根据权限代码查询当行栏目
        /// </summary>
        /// <param name="Roleidlist">权限总和</param>
        /// <param name="ParentID">父类代码</param>
        /// <returns></returns>
        private List<Model.Angel_Admin_Navigation> getRoleMenuList(string Roleidlist, int ParentID)
        {
            StringBuilder str_condition = new StringBuilder();

            str_condition.Append(" NavName in (");
            str_condition.Append(Roleidlist);
            str_condition.Append(") and ParentID=" + ParentID + " order by Sequence");
            AngelData menuData = new AngelData("Angel_Admin_Navigation", "Angel_Admin_Navigation");
            return menuData.getObjectListByCondition<Model.Angel_Admin_Navigation>(str_condition.ToString());

        }

        /// <summary>
        /// 根据权限ID查询操作表所有导航菜单
        /// </summary>
        /// <param name="RoleId">角色代码</param>
        /// <returns></returns>
        private string getRoleList(string RoleId)
        {
            // 1) 得到角色权限列表
            StringBuilder sb = new StringBuilder();
            string str_condition = "RoleID=" + RoleId + " and IsView=1 ";
            List<string> str_userFunction = OData.Item_db.GetDisrinctColValue("Angel_Admin_Roleoperate", "NavidName", str_condition);
            // string[] l_functionList = Regex.Split(str_userFunction, ",", RegexOptions.IgnoreCase);
            for (int i = 0; i < str_userFunction.Count; i++)
            {
                sb.Append("'");
                sb.Append(str_userFunction[i]);
                sb.Append("'");
                if (i != str_userFunction.Count - 1)
                    sb.Append(",");
            }
            return sb.ToString();
        }

    }
}