using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace m2mKoubai
{
    public class Utility
    {
        /// <summary>
        /// 発注Noのリンクボタン
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static string LinkToHacchuuNo(string strKey, string strHacchuuNo)
        {
            return string.Format("<a href=Javascript:void(0); onclick=\"HacchuuNo('{0}')\"; style=color:Blue>{1}</a>",
                strKey, strHacchuuNo);
        }
        public static string ToInnerRowsHTML_NoLine(params string[] strArray)
        {
            string str = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                string s = ("" == strArray[i]) ? "&nbsp;" : strArray[i];
                if (0 == i)
                    str += string.Format("<div noWrap>{0}</div>", s);
                else
                    str += string.Format("<div noWrap>{0}</div>", s);
            }
            return str;
        }

        /// <summary>
        /// 一覧の複数行表示
        /// </summary>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static string ToInnerRowsHTML(params string[] strArray)
        {
            string str = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                string s = ("" == strArray[i]) ? "&nbsp;" : strArray[i];
                if (0 == i)
                    str += string.Format("<div noWrap class=i >{0}</div>", s);
                else
                    str += string.Format("<div noWrap class=\"i tb\">{0}</div>", s);
            }
            return str;
        }
        
        /*  
        public static string FormatFromyyyyMMdd(string yyyyMMdd)
        {
            if (yyyyMMdd.Length != 8)
                return yyyyMMdd;

            return string.Format("{0}/{1}/{2}", yyyyMMdd.Substring(0, 4), yyyyMMdd.Substring(4, 2), yyyyMMdd.Substring(6, 2));
        }
        */
        public static string FormatFromyyyyMMdd(string yyyyMMdd)
        {
            if (yyyyMMdd.Length != 8)
                return yyyyMMdd;

            return string.Format("{0}/{1}/{2}", yyyyMMdd.Substring(2, 2), yyyyMMdd.Substring(4, 2), yyyyMMdd.Substring(6, 2));
        }

        public static string FormatToyyMMdd(string yyyyMMdd)
        {
            if (yyyyMMdd.Length != 8)
                return yyyyMMdd;

            return string.Format("{0}/{1}/{2}", yyyyMMdd.Substring(2, 2), yyyyMMdd.Substring(4, 2), yyyyMMdd.Substring(6, 2));
        }

        public static string FormatToyyMMddHHmm(string yyyyMMdd)
        {
            return (DateTime.Parse(yyyyMMdd)).ToString("yy/MM/dd HH:mm");
        }

        public static string FormatToyyyyMMdd(string yyMMdd)
        {
            if (yyMMdd.Length != 6)
            {
                return yyMMdd;
            }
            else
            {
                try
                {
                    DateTime date = DateTime.ParseExact(yyMMdd, "yyMMdd", null);
                    return date.ToString("yyyyMMdd");
                }
                catch
                {
                    return "";
                }

            }
        }
        public static string FormatFromyyMMdd(string yyMMdd)
        {
            if (yyMMdd.Length != 6)
            {
                return yyMMdd;
            }
            else
            {
                DateTime date = DateTime.ParseExact(yyMMdd, "yyMMdd", null);
                return date.ToString("yyyyMMdd");
            }
        }
        
        public static string FormatFromyyyyMM(string yyyyMM)
        {
            if (yyyyMM.Length != 6)
            {
                return yyyyMM;
            }
            else
            {
                return yyyyMM.Substring(0, 4) + "年" + yyyyMM.Substring(4, 2) + "月";
            }
        }
        // 郵便番号変換
        public static string FormatYuubin(string strBanggou)
        {
            if (strBanggou.Length != 7)
                return strBanggou;
            else
                return strBanggou.Substring(0,3) + "-" + strBanggou.Substring(3,4);
        }
        // 
        public static string FormatBanggo(string strBanggou)
        {
            if (strBanggou.Length != 10)
                return strBanggou;
            else
                return strBanggou.Substring(0, 2) + "-" + strBanggou.Substring(2, 4) + "-" + strBanggou.Substring(6, 4); 
        }

    }
}
