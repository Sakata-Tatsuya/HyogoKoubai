using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web;
using m2mKoubaiDAL;

namespace m2mKoubai
{
    [Serializable]
    public class HonyakuUserControl : System.Web.UI.UserControl
    {
        public System.IO.MemoryStream ms;
        public byte[] theData;

        public bool HonyakuZumi
        {
            get;
            set;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!SessionManager.User.TwoLetterISOLanguageName.Equals("ja"))
            {
                if (!HonyakuZumi)
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                        AppCommon.HonyakuChildControls(this.Controls[i]);
                    this.HonyakuZumi = true;
                }
            }
        }
    }

    [Serializable]
    public class HonyakuPage : Core.Web.ServerViewStatePage
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!SessionManager.User.TwoLetterISOLanguageName.Equals("ja"))
            {
                for (int i = 0; i < this.Controls.Count; i++)
                    AppCommon.HonyakuChildControls(this.Controls[i]);

            }
        }
    }

    [Serializable]
    public class Language
    {
        // ISO 639の2桁コードで定義する。
        public const string Japanese = "ja";
        public const string English = "en";
        public const string Chinese = "zh";
    }

    [Serializable]
    public class RM
    {
        public static string GetString(string str)
        {
            return SessionManager.User.Honyaku(str);
        }

        public static void Honyaku(Core.Web.FilterTextBox tbx)
        {
            for (int i = 0; i < tbx.FilterItems.Count; i++)
            {
                tbx.FilterItems[i].Text = SessionManager.User.Honyaku(tbx.FilterItems[i].Text);
            }
        }

        public static void Honyaku(DropDownList ddl)
        {
            if (ddl.Attributes["Honyaku"] == "true") return;
            for (int i = 0; i < ddl.Items.Count; i++)
                ddl.Items[i].Text = SessionManager.User.Honyaku(ddl.Items[i].Text);

            ddl.Attributes["Honyaku"] = "true";
        }
    }

    [Serializable]
    public class AppCommon
    {

        // 自社の会社コード
        public const string Jisha_KAISHACODE = "0000000";

        public const string NO_DATA = "該当するデータはありません";


        /// <summary>
        /// 有効/無効の表示
        /// </summary>
        /// <param name="bSakujo"></param>
        /// <returns></returns> 
        public static string YukouMukouText(bool bSakujo)
        {
            if (bSakujo)
                return "無効";
            else
                return "有効";
        }
       
        // 数字以外の入力不可
        public static void NumOnly(TextBox tbx)
        {
            tbx.Attributes["onkeydown"] = "return KeyCodeCheck();";
        }

        /// <summary>
        /// 品目の勘定科目名
        /// </summary>
        /// <param name="nKamokuCode"></param>
        /// <returns></returns>
        public static string KamokuMei(int nKamokuCode)
        {
            if (nKamokuCode == 174)
                return "原材料";
            else if (nKamokuCode == 176)
                return "貯蔵品";
            else
                return "";
        }

        /// <summary>
        /// 品目の費用勘定科目名
        /// </summary>
        /// <param name="nHiyouCode"></param>
        /// <returns></returns>
        public static string HiyouMei(int nHiyouCode)
        {
            if (nHiyouCode == 723)
                return "原料費";
            else if (nHiyouCode == 752)
                return "消耗品費";
            else if (nHiyouCode == 722)
                return "補助材料費";
            else if (nHiyouCode == 740)
                return "原燃料費";
            else
                return "";
        }

        /// <summary>
        /// 品目の補助勘定科目名
        /// </summary>
        /// <param name="nHojyoNo"></param>
        /// <returns></returns>
        public static string HojyoMei(int nHojyoNo)
        {
            if (nHojyoNo == 1)
                return "主原料費";
            else if (nHojyoNo == 2)
                return "副原料費";
            else if (nHojyoNo == 3)
                return "原燃料費";
            else if (nHojyoNo == 4)
                return "梱包材料費";
            else if (nHojyoNo == 5)
                return "SP付属品";
            else if (nHojyoNo == 6)
                return "直接消耗品費";
            else
                return "";
        }
        //支払締日
        public static string ShiharaiShimebi(int Shimebi)
        {
            switch (Shimebi)
            {

                case 0:
                    return "末日";
                case 1:
                    return "5日";
                case 2:
                    return "10日";
                case 3:
                    return "15日";
                case 4:
                    return "20日";
                case 5:
                    return "25日";
                default:
                    return "未決定";
            }
        }

        //支払予定日
        public static string ShiharaiYoteibi(int Yoteibi)
        {
            switch (Yoteibi)
            {
                case 0:
                    return "翌月末日";
                case 1:
                    return "翌月5日";
                case 2:
                    return "翌月10日";
                case 3:
                    return "翌月15日";
                case 4:
                    return "翌月20日";
                case 5:
                    return "翌月25日";
                case 6:
                    return "翌翌月末日";
                case 7:
                    return "翌翌月5日";
                case 8:
                    return "翌翌月10日";
                case 9:
                    return "翌翌月15日";
                case 10:
                    return "翌翌月20日";
                case 11:
                    return "翌翌月25日";
                default:
                    return "未決定";
            }
        }

        public static int ShiharaiShimebiUp(string Shimebi)
        {
            if (Shimebi == "末日")
                return 0;
            if (Shimebi == "5日")
                return 1;
            if (Shimebi == "10日")
                return 2;
            if (Shimebi == "15日")
                return 3;
            if (Shimebi == "20日")
                return 4;
            if (Shimebi == "25日")
                return 5;
            else
                return 6;
         
        }

        public static int ShiharaiYoteibiUp(string Yoteibi)
        {
            if (Yoteibi == "10")
                return 0;
            if (Yoteibi == "11")
                return 1;
            if (Yoteibi == "12")
                return 2;
            if (Yoteibi == "13")
                return 3;
            if (Yoteibi == "14")
                return 4;
            if (Yoteibi == "15")
                return 5;
            if (Yoteibi == "20")
                return 6;
            if (Yoteibi == "21")
                return 7;
            if (Yoteibi == "22")
                return 8;
            if (Yoteibi == "23")
                return 9;
            if (Yoteibi == "24")
                return 10;
            if (Yoteibi == "25")
                return 11;
            else
                return 12;
        }

        public static string YoteibiDdl1Select(int Yoteibi)
        {
             switch (Yoteibi)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    return "1";
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    return "2";
                default:
                    return "0";
            }
        }

        public static string YoteibiDdl2Select(int Yoteibi)
        {           
            switch (Yoteibi)
            {
                case 0:
                case 6:
                    return "0";
                case 1:
                case 7:
                    return "1";
                case 2:
                case 8:
                    return "2";
                case 3:
                case 9:
                    return "3";
                case 4:
                case 10:
                    return "4";
                case 5:
                case 11:
                    return "5";
                default:
                    return "6";
            }
        }

        /// <summary>
        /// リードタイムの単位の表示形式
        /// </summary>
        /// <param name="Shimebi"></param>
        /// <returns></returns>
        public static string LT_Tani(byte bLT_Tani)
        {
            switch (bLT_Tani)
            {
                case 1:
                    return LeadTime_Tani_Txt.Day;
                case 2:
                    return LeadTime_Tani_Txt.Week;
                case 3:
                    return LeadTime_Tani_Txt.Month;
                case 4:
                    return LeadTime_Tani_Txt.Year;
                default:
                    return "---";
            }
        }

        /// <summary>
        /// リードタイムのかける日数
        /// </summary>
        /// <param name="Shimebi"></param>
        /// <returns></returns>
        public static int LT_Suuji(byte bLT_Tani)
        {
            if (bLT_Tani == (byte)LeadTimeKubun.Day)
            {
                return (int)LeadTime_Nissu.Day;
            }
            else if (bLT_Tani == (byte)LeadTimeKubun.Week)
            {
                return (int)LeadTime_Nissu.Week;
            }
            else if (bLT_Tani == (byte)LeadTimeKubun.Month)
            {
                return (int)LeadTime_Nissu.Month;
            }
            else if (bLT_Tani == (byte)LeadTimeKubun.Year)
            {
                return (int)LeadTime_Nissu.Year;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// リードタイムから納期を割り出す
        /// </summary>
        /// <param name="nLT_Suuji"></param>
        /// <param name="bLT_Tani"></param>
        /// <returns></returns>
        public static DateTime GetNouki(int nLT_Suuji, byte bLT_Tani)
        {
            // 日数を計算する
            int nAddDays = nLT_Suuji * LT_Suuji(bLT_Tani);
            // 現在の日付け
            DateTime dateForm = DateTime.Now;
            // リードタイムの日付け
            DateTime dateTo = DateTime.Now.AddDays(nAddDays);
            // 日付けを曜日に変換
            DayOfWeek weekForm = dateForm.DayOfWeek;
            // 日付けを曜日に変換
            DayOfWeek weekTo = dateTo.DayOfWeek;
            // 週間数を取得
            int n = nAddDays / 7;
            if (n == 0) // 一週間未満の場合
            {
                // 月～土、火～日、日～金の場合は、日数 + 1
                if ((weekForm == DayOfWeek.Monday && weekTo == DayOfWeek.Saturday) ||
                    (weekForm == DayOfWeek.Sunday && weekTo == DayOfWeek.Friday)　|| 
                    (weekTo == DayOfWeek.Saturday))
                {
                    nAddDays++;
                }
                // 水～月、木～火、土～木の場合は、日数 + 2
                else if ((weekForm == DayOfWeek.Wednesday && weekTo == DayOfWeek.Monday) ||
                    (weekForm == DayOfWeek.Thursday && weekTo == DayOfWeek.Tuesday) ||
                      (weekForm == DayOfWeek.Tuesday && weekTo == DayOfWeek.Sunday) ||
                    (weekForm == DayOfWeek.Saturday && weekTo == DayOfWeek.Thursday) ||
                    (weekTo == DayOfWeek.Sunday))
                {
                    nAddDays = nAddDays + 2;
                }
            　  
            }
            else// 一週間以上の場合
            {
                int nNew = nAddDays % 7;
                if (nNew == 0)
                {

                    // 納期が土の場合
                    if (weekForm == DayOfWeek.Saturday)
                        nAddDays = nAddDays + ((n + 1) * 2 - 1);
                    // 納期が日の場合
                    else if (weekForm == DayOfWeek.Sunday)
                        nAddDays = nAddDays + ((n + 1) * 2);
                    else
                        nAddDays = nAddDays + n * 2;
                }
                else
                {
                    // 納期が土の場合
                    if (weekTo == DayOfWeek.Saturday)
                        nAddDays = nAddDays + (n * 2 + 1);
                    // 納期が日の場合
                    else if (weekTo == DayOfWeek.Sunday)
                        nAddDays = nAddDays + ((n + 1) * 2);
                    else
                        nAddDays = nAddDays + n * 2;
                }

            }
            // 最終納期が土曜日の場合、最終納期＋2
            if(DateTime.Now.AddDays(nAddDays).DayOfWeek == DayOfWeek.Saturday)
                return DateTime.Now.AddDays(nAddDays + 2);
            // 最終納期が日曜日の場合、最終納期＋1
            else if (DateTime.Now.AddDays(nAddDays).DayOfWeek == DayOfWeek.Sunday)
                return DateTime.Now.AddDays(nAddDays + 1);
            else
                return DateTime.Now.AddDays(nAddDays);

        }

        // 得意先の〆日による期間作成
        public static void CreateKikan(int nYear, int nMonth, int nShimeBi, ref int nSNen, ref int nENen)
        {
            // 〆日によって変化 
            int[] nDayAry = new int[2];
            int[] nDayAry1 = { 1, 31 };
            int[] nDayAry2 = { 6, 5 };
            int[] nDayAry3 = { 11, 10 };
            int[] nDayAry4 = { 16, 15 };
            int[] nDayAry5 = { 21, 20 };
            int[] nDayAry6 = { 26, 25 };

            if (nShimeBi == (int)ShiiresakiClass.ShimeBi.MATUJITU)
                nDayAry = nDayAry1;
            else if (nShimeBi == (int)ShiiresakiClass.ShimeBi.GO)
                nDayAry = nDayAry2;
            else if (nShimeBi == (int)ShiiresakiClass.ShimeBi.JYU)
                nDayAry = nDayAry3;
            else if (nShimeBi == (int)ShiiresakiClass.ShimeBi.JYUGO)
                nDayAry = nDayAry4;
            else if (nShimeBi == (int)ShiiresakiClass.ShimeBi.NIJYU)
                nDayAry = nDayAry5;
            else
                nDayAry = nDayAry6;

            // 15日、20日、25日が〆日の場合
            if (nShimeBi == (int)ShiiresakiClass.ShimeBi.JYUGO || nShimeBi == (int)ShiiresakiClass.ShimeBi.NIJYU ||
                nShimeBi == (int)ShiiresakiClass.ShimeBi.NIJYUGO)
            {
                // 年月を調整
                SetYearMonth(ref nYear, ref nMonth);
            }

            nSNen = CreateNengappi(nYear, nMonth, nDayAry[0]);
            if (nDayAry[1] < nDayAry[0])
            {
                // 年月を調整
                SetYearMonthPlus(ref nYear, ref nMonth);
            }

            // 実際使用する検索日
            int nDay = CreateDay(nYear, nMonth, nDayAry[1]);
            nENen = CreateNengappi(nYear, nMonth, nDay);
        }

        private static int CreateNengappi(int nYear, int nMonth, int nDay)
        {
            return int.Parse(string.Format("{0:0000}{1:00}{2:00}", nYear, nMonth, nDay));
        }

        // 年月調整
        public static void SetYearMonth(ref int nYear, ref int nMonth)
        {
            nMonth--;
            if (nMonth == 0)
            {
                nMonth = 12;
                nYear -= 1;
            }
        }

        // 年月調整
        public static void SetYearMonthPlus(ref int nYear, ref int nMonth)
        {
            nMonth++;
            if (nMonth == 13)
            {
                nMonth = 1;
                nYear += 1;
            }
        }

        // 月末日取得
        public static DateTime GetLastOfMonth(int nYear, int nMonth)
        {
            DateTime startOfMonth = new DateTime(nYear, nMonth, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            return endOfMonth;
        }

        // 実際に使用する日を返す
        private static int CreateDay(int nYear, int nMonth, int nDay)
        {
            int nNewDay = 0;
            if (nDay == 31)
            {
                nNewDay = DateTime.DaysInMonth(nYear, nMonth);
            }
            else
            {
                nNewDay = nDay;
            }

            return nNewDay;
        }

        public static void HonyakuChildControls(System.Web.UI.Control c)
        {
            if (c is Literal)
            {
                (c as Literal).Text = SessionManager.User.Honyaku((c as Literal).Text);
            }
            else if (c is Label)
            {
                (c as Label).Text = SessionManager.User.Honyaku((c as Label).Text);
            }
            else if (c is Button)
            {
                (c as Button).Text = SessionManager.User.Honyaku((c as Button).Text);
            }
            else if (c is HtmlInputButton)
            {
                (c as HtmlInputButton).Value = SessionManager.User.Honyaku((c as HtmlInputButton).Value);
            }
            else if (c is DropDownList)
            {
                DropDownList ddl = c as DropDownList;
                for (int i = 0; i < ddl.Items.Count; i++)
                    ddl.Items[i].Text = SessionManager.User.Honyaku(ddl.Items[i].Text);
            }
            else if (c is RadioButtonList)
            {
                RadioButtonList rbl = c as RadioButtonList;
                for (int i = 0; i < rbl.Items.Count; i++)
                    rbl.Items[i].Text = SessionManager.User.Honyaku(rbl.Items[i].Text);
            }
            else if (c is CheckBoxList)
            {
                CheckBoxList cbl = c as CheckBoxList;
                for (int i = 0; i < cbl.Items.Count; i++)
                    cbl.Items[i].Text = SessionManager.User.Honyaku(cbl.Items[i].Text);
            }
            else if (c is Core.Web.FilterTextBox)
            {
                Core.Web.FilterTextBox tbx = c as Core.Web.FilterTextBox;
                for (int i = 0; i < tbx.FilterItems.Count; i++)
                    tbx.FilterItems[i].Text = SessionManager.User.Honyaku(tbx.FilterItems[i].Text);
            }
            else if (c is CheckBox)
            {
                (c as CheckBox).Text = SessionManager.User.Honyaku((c as CheckBox).Text);
            }
            else if (c is HyperLink)
            {
                HyperLink l = c as HyperLink;
                l.Text = SessionManager.User.Honyaku(l.Text);
            }
            else if (c is Telerik.Web.UI.RadGrid)
            {
                Telerik.Web.UI.RadGrid dgd = c as Telerik.Web.UI.RadGrid;

                for (int i = 0; i < dgd.Columns.Count; i++)
                    dgd.Columns[i].HeaderText = SessionManager.User.Honyaku(dgd.Columns[i].HeaderText);

                dgd.MasterTableView.PagerStyle.PagerTextFormat =
                    SessionManager.User.Honyaku("ページ移動: {4} &nbsp;ページ : <strong>{0:N0}</strong> / <strong>{1:N0}</strong> | 件数: <strong>{2:N0}</strong> - <strong>{3:N0}件</strong> / <strong>{5:N0}</strong>件中");
                dgd.MasterTableView.PagerStyle.FirstPageToolTip = SessionManager.User.Honyaku("最初のページに移動");
                dgd.MasterTableView.PagerStyle.LastPageToolTip = SessionManager.User.Honyaku("最後のページに移動");
                dgd.MasterTableView.PagerStyle.NextPageToolTip = SessionManager.User.Honyaku("次のページに移動");
                dgd.MasterTableView.PagerStyle.PrevPageToolTip = SessionManager.User.Honyaku("前のページに移動");
                dgd.MasterTableView.PagerStyle.PageSizeLabelText = SessionManager.User.Honyaku("ページサイズ");

                dgd.MasterTableView.PagerStyle.PrevPagesToolTip = SessionManager.User.Honyaku("前のページに移動");
                dgd.MasterTableView.PagerStyle.NextPagesToolTip = SessionManager.User.Honyaku("次のページに移動");

            }
            else if (c is Telerik.Web.UI.RadDatePicker)
            {
                Telerik.Web.UI.RadDatePicker r = c as Telerik.Web.UI.RadDatePicker;
                r.DatePopupButton.ToolTip = SessionManager.User.Honyaku("カレンダーを開きます。");
            }
            else if (c is DataGrid)
            {
                DataGrid d = c as DataGrid;
                for (int i = 0; i < d.Columns.Count; i++)
                {
                    d.Columns[i].HeaderText = SessionManager.User.Honyaku(d.Columns[i].HeaderText);
                }
            }
            else if (c is GridView)
            {
                GridView d = c as GridView;
                for (int i = 0; i < d.Columns.Count; i++)
                    d.Columns[i].HeaderText = SessionManager.User.Honyaku(d.Columns[i].HeaderText);
            }
            else if (c is Telerik.Web.UI.RadWindow)
            {
                Telerik.Web.UI.RadWindow w = c as Telerik.Web.UI.RadWindow;
                w.Title = SessionManager.User.Honyaku(w.Title);

                w.Localization.Cancel = SessionManager.User.Honyaku("キャンセル");
                w.Localization.Close = SessionManager.User.Honyaku("閉じる");
                w.Localization.Maximize = SessionManager.User.Honyaku("最大化");
                w.Localization.Minimize = SessionManager.User.Honyaku("最小化");
                w.Localization.No = SessionManager.User.Honyaku("いいえ");
                w.Localization.Reload = SessionManager.User.Honyaku("再表示");
                w.Localization.Restore = SessionManager.User.Honyaku("保存");
                w.Localization.Yes = SessionManager.User.Honyaku("はい");

                // RadWindowにはテンプレートのコントロールがあるので注意
                for (int i = 0; i < w.Controls.Count; i++)
                {
                    HonyakuChildControls(w.Controls[i]);
                }
            }
            else if (c is Telerik.Web.UI.RadWindowManager)
            {
                Telerik.Web.UI.RadWindowManager w = c as Telerik.Web.UI.RadWindowManager;
                for (int i = 0; i < w.Windows.Count; i++)
                    w.Windows[i].Title = SessionManager.User.Honyaku(w.Windows[i].Title);
            }
            else if (c is Telerik.Web.UI.RadTabStrip)
            {
                Telerik.Web.UI.RadTabStrip t = c as Telerik.Web.UI.RadTabStrip;
                for (int i = 0; i < t.Tabs.Count; i++)
                    t.Tabs[i].Text = SessionManager.User.Honyaku(t.Tabs[i].Text);
            }
            else if (c is Core.Web.DataBindControls.DataBindTextBox)
            {
                Core.Web.DataBindControls.DataBindTextBox t = c as Core.Web.DataBindControls.DataBindTextBox;
                if (string.IsNullOrEmpty(t.DataName))
                    t.DataName = SessionManager.User.Honyaku(t.DataName);
            }
            else if (c is Core.Web.DataBindControls.DataBindRadioButtonList)
            {
                Core.Web.DataBindControls.DataBindRadioButtonList t = c as Core.Web.DataBindControls.DataBindRadioButtonList;
                if (string.IsNullOrEmpty(t.DataName))
                    t.DataName = SessionManager.User.Honyaku(t.DataName);
            }
            else
            {
                if (c is HonyakuUserControl)
                {
                    HonyakuUserControl ascx = c as HonyakuUserControl;
                    if (ascx.HonyakuZumi) return;   // HonyakuUserControl内で既に翻訳済みの場合は処理する必要がない(翻訳後の内容を翻訳してしてしまう問題がある)
                    ascx.HonyakuZumi = true;
                }
                // DataGridなどは、翻訳した後DataGridの子コントロールに対して以下を絶対に実行してはいけない
                for (int i = 0; i < c.Controls.Count; i++)
                    HonyakuChildControls(c.Controls[i]);


            }

        }


        public static void Honyaku(Core.Web.FilterTextBox tbx)
        {
            if (tbx.Attributes["Honyaku"] == "true") return;
            for (int i = 0; i < tbx.FilterItems.Count; i++)
            {
                tbx.FilterItems[i].Text = SessionManager.User.Honyaku(tbx.FilterItems[i].Text);
            }
            tbx.Attributes["Honyaku"] = "true";
        }

    }
}
