using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AngelRM.Core
{
  public  class Angel_RegularExpression
    {

      public Angel_RegularExpression()
        {
        }
        //将符合ptn的目标串，替换成 repStr,
        //不符合的不做替换
        //调用时，替换串中引用子匹配Group用$Group 
        // 如 ReplaceStr("aabb","(aa)(bb)","$2$1");
        public String ReplaceStr(String strObj, String strPtn, String strRep)
        {
            String result = strObj;
            Regex rx = new Regex(strPtn, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (rx.IsMatch(strObj))
                result=rx.Replace(strObj, strRep);
            return result; 

        }
        //是否符合某种格式
        public bool IsMatchStr(String strObj, String strPatrn)
        {
            Regex rx = new Regex(strPatrn, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return rx.IsMatch(strObj);
        }
        //删除不匹配串
        //把Str中的所有匹配串连接起来,放到一个字符串中
        public String Matches2Str(String strObj,String patrn)
        {
            String strResult = "";
            Regex rx = new Regex(patrn, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (rx.IsMatch(strObj))
            {
                MatchCollection ms = rx.Matches(strObj);
                for (int mi = 0; mi < ms.Count; mi++)
                {
                    Match m = ms[mi];
                    strResult += m.Groups[0].Value.ToString();
                }
            }
            return strResult;
        }//Matches2Str

        //删除不匹配串
        //把Str中的所有匹配串,分别放到列表中
        public List<String> Matches2List(String strObj, String patrn)
        {
            List<string> lss = new List<string>();
            Regex rx = new Regex(patrn, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (rx.IsMatch(strObj))
            {
                MatchCollection Matches = rx.Matches(strObj);
                foreach (Match m in Matches)
                {
                    string s = m.Value.ToString();
                    lss.Add(s);
                }
            }
            return lss;
        }

        //得到匹配的子字符串
        public String GetSubMatch(String strObj, String patrn, int group)
        {
            String strResult = "";
            Regex rx = new Regex(patrn, RegexOptions.IgnoreCase);
            if (rx.IsMatch(strObj))
            {

                MatchCollection ms = rx.Matches(strObj);
                Match m = ms[0];
                if (group < m.Groups.Count)
                    strResult = m.Groups[group].Value.ToString();
            }
            return strResult;
        }//GetSubMatch

        //得到匹配数
        public int GetMatchCount(String strObj, String patrn)
        {
            Regex rx = new Regex(patrn, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return rx.Matches(strObj).Count;
        }//GetSubMatch

        //得到子匹配数
        public int GetSubMatchCount(String strObj, String patrn)
        {
            Regex rx = new Regex(patrn, RegexOptions.IgnoreCase);
            if (rx.IsMatch(strObj))
            {

                MatchCollection ms = rx.Matches(strObj);
                return (ms[0].Groups.Count);
            }
            return 0;
        }//GetSubMatchCount
    }
}
