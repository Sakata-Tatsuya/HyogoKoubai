using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using m2mKoubaiDAL;

namespace m2mKoubai.Shiiresaki
{
    public partial class KenshuInfoForm : Core.Web.ServerViewStatePage
    {
        private int VsCurrentPageIndex
        {
            get
            {
                object obj = this.ViewState["page_index"];
                if (null == obj) return 0;
                return Convert.ToInt32(obj);
            }
            set
            {
                this.ViewState["page_index"] = value;
            }
        }


      //  private const int G_CELL_I = 0;
        private const int G_CELL_NO = 0;
        private const int G_CELL_BUHIN_KUBUN = 1;
        private const int G_CELL_BUHIN_CODE = 2;
        private const int G_CELL_BUHIN_NAME = 3;
        private const int G_CELL_CHUMON_SUURYOU = 4;
        private const int G_CELL_TANI = 5;
        private const int G_CELL_TANKA = 6;
        private const int G_CELL_CHUMON_KINGAKU = 7;
        private const int G_CELL_NOUNYUU_BASHO = 8;
        private const int G_CELL_UKEIREBI = 9;
        private const int G_CELL_NYUKA_SUURYOU = 10;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Shiiresaki)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }

                CtlTabShiire tab = FindControl("Tab") as CtlTabShiire;
                tab.Menu = CtlTabShiire.MainMenu.Kensyu_Jyouhou;

                       
                
                DateTime dtNow = DateTime.Now;                
                int nYear = dtNow.Year;
                // 今年
                DdlYear.Items.Add(nYear.ToString());
                // 来年
                nYear--;
                DdlYear.Items.Add(nYear.ToString());
                // 再来年
                nYear--;
                DdlYear.Items.Add(nYear.ToString());               
                // 今年を選択する
                DdlYear.SelectedValue = dtNow.Year.ToString();
                // 月
                DdlMonth.SelectedIndex = dtNow.Month;
                //
                ListSet.SetDdlJigyoushoKubun(SessionManager.UserKubun, DdlJigyoshoKubun);
                
                this.Create();
            }
            Common.CtlMyPager pagerTop = (Common.CtlMyPager)this.FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)this.FindControl("Pb");
            pagerTop.OnPageIndexChanged += new Common.CtlMyPager.CtlMyPagerEventHandler(this.OnPageIndexChanged);
            pagerBottom.OnPageIndexChanged += new Common.CtlMyPager.CtlMyPagerEventHandler(this.OnPageIndexChanged);
            pagerTop.ClientEvent = pagerBottom.ClientEvent = "PageChange";

        }
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.PreRender += new System.EventHandler(this.Form_PreRender);
        }

        private void Form_PreRender(object sender, EventArgs e)
        {
            //Img
            this.Img1.Style.Add("display", "none");

            // 検索ボタン
            BtnK.Attributes["onclick"] = "Kensaku();";

            // 検収明細            
            this.BtnKI.Attributes["onclick"] = string.Format("Kenshu('{0}');", HidKeyKen.Value);
            // 請求書
            this.BtnSI.Attributes["onclick"] = string.Format("Seikyu('{0}');", HidKeyKen.Value);
          
            // 行数変更
            this.DdlRow.Attributes["onchange"] = "RowChange(); return false;";

            // ----- カレンダー -----
            //CtlUkeireBi.SharedCalendar = this.SC;

        }

        /*
        private void SetList()
        {
            // 仕入先
            ListSet.SetDdlShiiresaki(DdlShiire);
            // 部品区分
            ListSet.SetDdlBuhinKubun_C(DdlBuhinKubun);
            // 部品
            this.DdlBuhinKubun.Attributes["onchange"] = "OnBuhin(); return false";

        }
        */

        private void Create()
        {
            // hidden
            this.HidChkID.Value = "";
            this.HidKey.Value = "";
            

            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");

            // 仕入先会社情報を取得
            m2mKoubaiDataSet.M_ShiiresakiRow drShiire =
               ShiiresakiClass.getM_ShiiresakiRow(SessionManager.KaishaCode, Global.GetConnection());
            if (drShiire == null)
            {
                this.ShowMsg("", true);
                this.ShowTblMain(false);
                return;
            }
            int nYear = int.Parse(this.DdlYear.SelectedValue);
            int nMonth  = int.Parse(this.DdlMonth.SelectedValue);
            int nFromDate = 0;
            int nToDate = 0;

            AppCommon.CreateKikan(nYear,nMonth, drShiire.ShiharaiShimebi, ref nFromDate, ref nToDate);
            KenshuClass.KensakuParam k = this.GetKensakuParam(nFromDate, nToDate);
            if (k == null)
            {
                this.ShowMsg("", true);
                this.ShowTblMain(false);
                return;
            }
            HidKeyKen.Value = k._NouhinYearMonth;
            //
            KenshuDataSet.V_KenshuDataTable dt =
                KenshuClass.getV_KenshuDataTable(k, Global.GetConnection());

            this.ShowMsg(dt.Rows.Count + "件", false);
            if (dt.Rows.Count == 0)
            {
                pagerTop.DdlClear();
                pagerBottom.DdlClear();
                this.ShowTblMain(false);
                return;
            }
            else
            {
                this.ShowTblMain(true);
            }

            //ページング            
            int nPageSize = AloowPaging();
            int nPageCount = 0;
            if (nPageSize > 0)
            {
                G.PageSize = nPageSize;
                G.AllowPaging = true;
                nPageCount = dt.Rows.Count / nPageSize;
                if (0 < dt.Rows.Count % nPageSize) nPageCount++;
                if (nPageCount <= VsCurrentPageIndex)
                    VsCurrentPageIndex = 0;

                // 現在の表示行(何行目～何行目)
                int nStartCount = nPageSize * VsCurrentPageIndex + 1;
                int nEndCount = nStartCount + nPageSize - 1;
                if (nEndCount > dt.Rows.Count)
                    nEndCount = dt.Rows.Count;
                pagerTop.SetItemCounter(nStartCount, nEndCount);
                pagerBottom.SetItemCounter(nStartCount, nEndCount);
            }
            else
            {
                G.PageSize = dt.Rows.Count;
                G.AllowPaging = false;
                VsCurrentPageIndex = 0;
            }
            G.PageIndex = VsCurrentPageIndex;
            pagerTop.Create(nPageCount);
            pagerBottom.Create(nPageCount);
            pagerTop.CurrentPageIndex = pagerBottom.CurrentPageIndex = G.PageIndex;

            G.DataSource = dt;
            G.DataBind();
            G.EnableViewState = false;

            G.Attributes.Add("bordercolor", "#e1e1c8");
        }



        private KenshuClass.KensakuParam GetKensakuParam(int nfrom, int nto)
        {


            KenshuClass.KensakuParam k = new KenshuClass.KensakuParam();
            /*
            // 発注No
            if (TbxHacchuNo.Text != "")
            {
                k._HacchuNo = TbxHacchuNo.Text;
            }
            // 仕入先
            if (DdlShiire.SelectedIndex > 0)
            {
                k._SCode = DdlShiire.SelectedValue;
            }

            // 部品区分
            if (DdlBuhinKubun.SelectedIndex > 0)
            {
                k._Kubun = DdlBuhinKubun.SelectedValue;
            }
            // 部品
            if (DdlBuhin.SelectedIndex > 0)
            {
                k._BuhinCode = DdlBuhin.SelectedValue;
            }

            // 受入日
            Common.CtlNengappiFromTo ctlUkeirebi = FindControl("CtlUkeireBi") as Common.CtlNengappiFromTo;
            if (ctlUkeirebi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._UkeireBi = ctlUkeirebi.GetNengappiKikan();
            }
            */
            k._FromDate = nfrom.ToString();
            k._ToDate = nto.ToString();
            // 納品年月
            k._NouhinYearMonth = this.DdlYear.SelectedValue + (int.Parse(this.DdlMonth.SelectedValue)).ToString("00");
            //
            if (DdlJigyoshoKubun.SelectedIndex > 0)
            {
                k._JigyoushoKubun = int.Parse(DdlJigyoshoKubun.SelectedValue);
            }
            // 仕入先
            k._SCode = SessionManager.KaishaCode;

            return k;
        }
        private int AloowPaging()
        {
            try
            {
                return int.Parse(this.DdlRow.SelectedValue);
            }
            catch
            {
                return -1;
            }
        }

        // メッセージ表示
        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }

        // GridView表示
        private void ShowTblMain(bool b)
        {
            G.Visible = b;
            TblRow.Visible = b;
            this.BtnKI.Visible = b;
            this.BtnSI.Visible = b;
           
        }

        // ページチェンジ
        private void OnPageIndexChanged(int nNewPageIndex)
        {
            VsCurrentPageIndex = nNewPageIndex;
            this.Create();
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];

            switch (strCmd)
            {
                case "page":

                    // ページ切り替え
                    this.VsCurrentPageIndex = int.Parse(strArgs[1]);
                    this.Create();
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    break;

                case "kensaku":
                    // 検索
                    this.VsCurrentPageIndex = 0;
                    this.Create();
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    break;

                case "row":
                case "reload":

                    // 行数変更
                    this.Create();
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    break;
                /*
                case "ddlBuhin":
                    ListSet.SetDdlBuhin_C(this.DdlBuhinKubun.SelectedValue, this.DdlBuhin);
                    this.Ram.AjaxSettings.AddAjaxSetting(Ram, this.DdlBuhin);
                    break;
                */

            }

        }

        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {        
                KenshuDataSet.V_KenshuRow dr =
                  ((DataRowView)e.Row.DataItem).Row as KenshuDataSet.V_KenshuRow;
             
                ChumonClass.ChumonKey key = new ChumonClass.ChumonKey(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);               

                // 発注NO              
                (e.Row.FindControl("LitHacchuuNo") as Literal).Text =
                    Utility.LinkToHacchuuNo(key.ToString(), dr.HacchuuNo);
                // 発注日
                (e.Row.FindControl("LitHacchuuBi") as Literal).Text = dr.HacchuuBi.ToString("yy/MM/dd");

                // 仕入先コード
                //e.Row.Cells[G_CELL_SHIIRE_CODE].Text = dr.ShiiresakiCode;
                // 仕入先名
                //e.Row.Cells[G_CELL_SHIIRE_NAME].Text = dr.ShiiresakiMei;
                // 品目グループ
                e.Row.Cells[G_CELL_BUHIN_KUBUN].Text = dr.BuhinKubun;
                // 部品コード
                e.Row.Cells[G_CELL_BUHIN_CODE].Text = dr.BuhinCode;
                // 部品目名
                e.Row.Cells[G_CELL_BUHIN_NAME].Text = dr.BuhinMei;
                // 注文数量
                e.Row.Cells[G_CELL_CHUMON_SUURYOU].Text = dr.ChumonSuuryou.ToString("#,##0");
                // 単位
                e.Row.Cells[G_CELL_TANI].Text = dr.Tani;
                // 単価
                e.Row.Cells[G_CELL_TANKA].Text = "\\" + dr.Tanka.ToString("#,##0.#0");
                // 注文金額
                //decimal Kingaku = Math.Floor(dr.Suuryou * dr.Tanka);
                e.Row.Cells[G_CELL_CHUMON_KINGAKU].Text = "\\" + dr.Kingaku.ToString("#,##");
                // 納入場所
                e.Row.Cells[G_CELL_NOUNYUU_BASHO].Text = dr.BashoMei;
                // 受付日  
                e.Row.Cells[G_CELL_UKEIREBI].Text += dr.NouhinBi.ToString("yy/MM/dd");
                       
                // 入荷数量 
                e.Row.Cells[G_CELL_NYUKA_SUURYOU].Text += dr.NouhinSuuryou.ToString("#,##0");
            }
        }


       
    }
}