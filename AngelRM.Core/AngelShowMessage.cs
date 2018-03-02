using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;


namespace AngelRM.Core
{
    public class AngelShowMessage
    {
        #region JS提示============================================
        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        public static void JscriptMessage(string msgtitle, string url, string msgcss,Page pages)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\")";
            pages.ClientScript.RegisterClientScriptBlock(pages.GetType(), "JsPrint", msbox, true);
        }
        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        /// <param name="callback">JS回调函数</param>
        public static void JscriptMessage(string msgtitle, string url, string msgcss, string callback, Page pages)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")";
            pages.ClientScript.RegisterClientScriptBlock(pages.GetType(), "JsPrint", msbox, true);
        }
        /// <summary>
        /// 无跳转弹出消息框
        /// </summary>
        /// <param name="msgtitle">显示标题</param>
        /// <param name="msgcss">消息内容</param>
        /// <param name="pages">当前页面</param>
        public static void JscriptMessage(string msgtitle, string msgcss, Page pages)
        {
            string msbox = "parent.AlertMessage(\"" + msgtitle + "\", \"" + msgcss + "\")";
            pages.ClientScript.RegisterStartupScript(pages.GetType(), "JsPrint", msbox, true);
        }

        #endregion
    }
}
