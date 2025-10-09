using Core.Type;
using KoubaiDAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Koubai.Jyuchu
{
    public partial class BuhinForcastForm : System.Web.UI.Page
    {
        private string VsKijyunYM
        {
            get
            {
                return (string)this.ViewState["VsKijyunYM"];
            }
            set
            {
                this.ViewState["VsKijyunYM"] = value;
            }
        }

        private bool IsJa
        {
            get
            {
                bool isJa = (SessionManager.User.LanguageCode == "ja");
                return isJa;
            }
        }
        private int VsHacchuuNo
        {
            get
            {
                object obj = this.ViewState["VsHacchuuNo"];
                return Convert.ToInt32(obj);
            }
            set
            {
                this.ViewState["VsHacchuuNo"] = value;
            }
        }
        private string VsZeiritu
        {
            get
            {
                return Convert.ToString(this.ViewState["VsZeiritu"]);
            }
            set
            {
                this.ViewState["VsZeiritu"] = value;
            }
        }
        private bool VsKeigenZeirituFlg
        {
            get
            {
                return (bool)(ViewState["VsKeigenZeirituFlg"] ?? false);
            }
            set
            {
                this.ViewState["VsKeigenZeirituFlg"] = value;
            }
        }
        private string VsYaer
        {
            get
            {
                return Convert.ToString(this.ViewState["VsYaer"]);
            }
            set
            {
                this.ViewState["VsYaer"] = value;
            }
        }
        private string VsUserID
        {
            get
            {
                return Convert.ToString(this.ViewState["VsUserID"]);
            }
            set
            {
                this.ViewState["VsUserID"] = value;
            }
        }
        private int VsJigyoushoKubun
        {
            get
            {
                string str = Convert.ToString(this.ViewState["VsJigyoushoKubun"]);
                if (str == null || str == "")
                {
                    return 8;
                }
                else
                {
                    return int.Parse(str);
                }
            }
            set
            {
                this.ViewState["VsJigyoushoKubun"] = value;
            }
        }
        private string VsShiiresaki
        {
            get
            {
                return Convert.ToString(this.ViewState["VsShiiresaki"]);
            }
            set
            {
                this.ViewState["VsShiiresaki"] = value;
            }
        }

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

        //private Core.Sql.SqlDataFactory _SqlDataFactory = null;
        //private Core.Sql.SqlDataFactory SqlDataFactory
        //{
        //    get
        //    {
        //        if (null == _SqlDataFactory)
        //            _SqlDataFactory = new Core.Sql.SqlDataFactory(LIST_ID, Global.GetConnection());
        //        return _SqlDataFactory;
        //    }
        //}

        bool b = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LblMsg.Text = "";
            //this.BtnPostback.Style["display"] = "none";
            //this.BtnDelPostback.Style["display"] = "none";
            if (!this.IsPostBack)
            {
                VsYaer = DateTime.Now.ToString("yy");
                VsUserID = SessionManager.LoginID;
                VsJigyoushoKubun = SessionManager.JigyoushoKubun;
                VsHacchuuNo = 0;
                VsKeigenZeirituFlg = false;
                if (DateTime.Today >= new DateTime(2019, 10, 1))
                {
                    VsZeiritu = "10";
                    VsKeigenZeirituFlg = false;
                }
                else
                {
                    VsZeiritu = "8";
                    VsKeigenZeirituFlg = true;
                }
                SetKijyunYM(this.DdlKijyunYM);
                string TodayYM = DateTime.Today.ToString("yyyyMM");
                VsKijyunYM = TodayYM;
                DdlKijyunYM.SelectedValue = VsKijyunYM;
                ListSet.SetDdlShiiresakiMulti(DdlShiiresaki);
                this.Create();
            }
            Common.CtlMyPager pagerTop = (Common.CtlMyPager)this.FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)this.FindControl("Pb");
            pagerTop.OnPageIndexChanged += new Common.CtlMyPager.CtlMyPagerEventHandler(this.OnPageIndexChanged);
            pagerBottom.OnPageIndexChanged += new Common.CtlMyPager.CtlMyPagerEventHandler(this.OnPageIndexChanged);
            pagerTop.ClientEvent = pagerBottom.ClientEvent = "PageChange";
        }
        protected string H(string str)
        {
            return SessionManager.User.Honyaku(str);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //this.BtnUp.Attributes["onclick"] = string.Format("return confirm('{0}');", H("登録しますか？"));
            // 行数変更
            //this.DdlRow.Attributes["onchange"] = "RowChange(); return false;";
        }

        protected void BtnKensaku_Click(object sender, EventArgs e)
        {
            this.Create();
        }
        private void OnPageIndexChanged(int nNewPageIndex)
        {
            VsCurrentPageIndex = nNewPageIndex;
            this.Create();
        }

        private void Create()
        {
            HidChkID.Value = "";

            this.D.Visible = false;
            ShowList(false);

            if (DdlKijyunYM.SelectedIndex < 0)
            {
                this.ShowMsg("基準年月を指定してください。", true);
                return;
            }

            SeisanClass.KensakuParam p = new SeisanClass.KensakuParam();
            SetKensakuParam(p);

            SeisanDataSet.V_ShoyouSuDataTable dtH = new SeisanDataSet.V_ShoyouSuDataTable();
            dtH = SeisanClass.GetV_ShoyouSuDataTable(p, Global.GetConnection());

            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");

            this.ShowMsg(dtH.Rows.Count + "件", false);
            if (1 > dtH.Rows.Count)
            {
                this.D.PageIndex = 0;
                pagerTop.DdlClear();
                pagerBottom.DdlClear();
                this.ShowMsg(H("該当のデータはありません。"), false);
                return;
            }
            //ページング
            int nPageSize = AloowPaging();
            int nPageCount = 0;
            if (nPageSize > 0)
            {
                D.PageSize = nPageSize;
                D.AllowPaging = true;
                nPageCount = dtH.Rows.Count / nPageSize;
                if (0 < dtH.Rows.Count % nPageSize) nPageCount++;
                if (nPageCount <= VsCurrentPageIndex)
                    VsCurrentPageIndex = 0;

                // 現在の表示行(何行目～何行目)
                int nStartCount = nPageSize * VsCurrentPageIndex + 1;
                int nEndCount = nStartCount + nPageSize - 1;
                if (nEndCount > dtH.Rows.Count)
                    nEndCount = dtH.Rows.Count;
                pagerTop.SetItemCounter(nStartCount, nEndCount);
                pagerBottom.SetItemCounter(nStartCount, nEndCount);
            }
            else
            {
                D.PageSize = dtH.Rows.Count;
                D.AllowPaging = false;
                VsCurrentPageIndex = 0;
            }
            D.PageIndex = VsCurrentPageIndex;
            pagerTop.Create(nPageCount);
            pagerBottom.Create(nPageCount);
            pagerTop.CurrentPageIndex = pagerBottom.CurrentPageIndex = D.PageIndex;

            this.D.DataSource = dtH;

            this.D.DataBind();
            ShowList(true);

        }
        private void SetKensakuParam(SeisanClass.KensakuParam p)
        {
            p._KijyunYM = DdlKijyunYM.SelectedValue;
            if (this.DdlKijyunYM.SelectedIndex > 0)
            {
                p._KijyunYM = DdlKijyunYM.SelectedValue;
            }
            if (this.DdlShiiresaki.SelectedIndex > 0)
            {
                p._ShiiresakiCode = DdlShiiresaki.SelectedValue;
            }

        }

        protected void D_PreRender(object sender, EventArgs e)
        {
            //this.D.MasterTableView.Attributes["bordercolor"] = "#708090";
            //this.D.MasterTableView.Attributes["border"] = "1";
            //this.D.MasterTableView.HeaderStyle.CssClass = "radgrid_header_cor";
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
        private void ShowMsg(string strMsg, bool bErrorMsg)
        {
            this.LblMsg.ForeColor = (bErrorMsg) ? System.Drawing.Color.Red : System.Drawing.Color.Blue;
            this.LblMsg.Text = strMsg;
        }

        // 一覧表示
        private void ShowList(bool bShow)
        {
            this.L.Style["width"] = L.Style["height"] = (bShow) ? "" : "200px";
            D.Visible = bShow;
        }

        protected void D_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            D.PageIndex = e.NewPageIndex;
            this.Create();
        }

        bool _bD_PageSizeChanged = false;
        protected void D_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            if (!_bD_PageSizeChanged)
            {
                _bD_PageSizeChanged = true;
                D.PageSize = e.NewPageSize;
                this.Create();
            }
        }
        protected void D_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string sKijyunYM = VsKijyunYM.Substring(0, 4) + "/" + VsKijyunYM.Substring(4) + "/01";
            DateTime dtN0 = DateTime.Today;
            DateTime.TryParse(sKijyunYM, out dtN0);
            DateTime dtN_1 = dtN0.AddMonths(-1);
            DateTime dtN1 = dtN0.AddMonths(1);
            DateTime dtN2 = dtN0.AddMonths(2);
            DateTime dtN3 = dtN0.AddMonths(3);
            DateTime dtN4 = dtN0.AddMonths(4);
            DateTime dtN5 = dtN0.AddMonths(5);
            DateTime dtN6 = dtN0.AddMonths(6);

            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label LitH_PlanN0 = e.Row.FindControl("H_PlanN0") as Label;
                Label LitH_PlanN1 = e.Row.FindControl("H_PlanN1") as Label;
                Label LitH_PlanN2 = e.Row.FindControl("H_PlanN2") as Label;
                Label LitH_PlanN3 = e.Row.FindControl("H_PlanN3") as Label;
                Label LitH_PlanN4 = e.Row.FindControl("H_PlanN4") as Label;
                Label LitH_PlanN5 = e.Row.FindControl("H_PlanN5") as Label;
                LitH_PlanN0.Text = dtN0.ToString("yyyyMM");
                LitH_PlanN1.Text = dtN1.ToString("yyyyMM");
                LitH_PlanN2.Text = dtN2.ToString("yyyyMM");
                LitH_PlanN3.Text = dtN3.ToString("yyyyMM");
                LitH_PlanN4.Text = dtN4.ToString("yyyyMM");
                LitH_PlanN5.Text = dtN5.ToString("yyyyMM");
                LitH_PlanN0.ForeColor = Color.Red;
                HtmlInputCheckBox chkH = e.Row.FindControl("ChkH") as HtmlInputCheckBox;
                //chkH.Visible = b;
                //if (b)
                chkH.Attributes["onclick"] = "ChkAll(this.checked)";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SeisanDataSet.V_ShoyouSuRow dr = ((DataRowView)e.Row.DataItem).Row as SeisanDataSet.V_ShoyouSuRow;

                // chk.idを格納
                HtmlInputCheckBox chk = e.Row.FindControl("ChkI") as HtmlInputCheckBox;
                if (HidChkID.Value != "") HidChkID.Value += ",";
                HidChkID.Value += chk.ClientID;

                Literal LitNengetu = e.Row.FindControl("LitNengetu") as Literal;
                Literal LitBuhinCode = e.Row.FindControl("LitBuhinCode") as Literal;
                Literal LitShiiresakiCode = e.Row.FindControl("LitShiiresakiCode") as Literal;
                Literal LitShiiresakiMei = e.Row.FindControl("LitShiiresakiMei") as Literal;
                Literal LitLeadTime = e.Row.FindControl("LitLeadTime") as Literal;
                Literal LitLot = e.Row.FindControl("LitLot") as Literal;
                Label LblNyukaSuN_1 = e.Row.FindControl("LblNyukaSuN_1") as Label;
                Label LblNyukaSuN0 = e.Row.FindControl("LblNyukaSuN0") as Label;
                Label LblNyukaSuN1 = e.Row.FindControl("LblNyukaSuN1") as Label;
                Label LblNyukaSuN2 = e.Row.FindControl("LblNyukaSuN2") as Label;
                Label LblNyukaSuN3 = e.Row.FindControl("LblNyukaSuN3") as Label;
                Label LblNyukaSuN4 = e.Row.FindControl("LblNyukaSuN4") as Label;
                Label LblNyukaSuN5 = e.Row.FindControl("LblNyukaSuN5") as Label;
                Label LblShoyouSuN_1 = e.Row.FindControl("LblShoyouSuN_1") as Label;
                Label LblShoyouSuN0 = e.Row.FindControl("LblShoyouSuN0") as Label;
                Label LblShoyouSuN1 = e.Row.FindControl("LblShoyouSuN1") as Label;
                Label LblShoyouSuN2 = e.Row.FindControl("LblShoyouSuN2") as Label;
                Label LblShoyouSuN3 = e.Row.FindControl("LblShoyouSuN3") as Label;
                Label LblShoyouSuN4 = e.Row.FindControl("LblShoyouSuN4") as Label;
                Label LblShoyouSuN5 = e.Row.FindControl("LblShoyouSuN5") as Label;
                Label LblHacchuSuN_1 = e.Row.FindControl("LblHacchuSuN_1") as Label;
                Label LblHacchuSuN0 = e.Row.FindControl("LblHacchuSuN0") as Label;
                Label LblHacchuSuN1 = e.Row.FindControl("LblHacchuSuN1") as Label;
                Label LblHacchuSuN2 = e.Row.FindControl("LblHacchuSuN2") as Label;
                Label LblHacchuSuN3 = e.Row.FindControl("LblHacchuSuN3") as Label;
                Label LblHacchuSuN4 = e.Row.FindControl("LblHacchuSuN4") as Label;
                Label LblHacchuSuN5 = e.Row.FindControl("LblHacchuSuN5") as Label;
                Label LblZaikoSuN_1 = e.Row.FindControl("LblZaikoSuN_1") as Label;
                Label LblZaikoSuN0 = e.Row.FindControl("LblZaikoSuN0") as Label;
                Label LblZaikoSuN1 = e.Row.FindControl("LblZaikoSuN1") as Label;
                Label LblZaikoSuN2 = e.Row.FindControl("LblZaikoSuN2") as Label;
                Label LblZaikoSuN3 = e.Row.FindControl("LblZaikoSuN3") as Label;
                Label LblZaikoSuN4 = e.Row.FindControl("LblZaikoSuN4") as Label;
                Label LblZaikoSuN5 = e.Row.FindControl("LblZaikoSuN5") as Label;

                LitNengetu.Text = dr.Nengetu;
                LitBuhinCode.Text = dr.BuhinCode;
                LitShiiresakiCode.Text = dr.ShiiresakiCode1;
                LitShiiresakiMei.Text = ShiiresakiClass.GetShiiresakiMei(dr.ShiiresakiCode1, Global.GetConnection());
                LitLeadTime.Text = (dr.LT_Suuji * AppCommon.LT_Suuji(dr.LT_Tani)).ToString();
                LitLot.Text = dr.Lot.ToString() + dr.Tani;

                //発注計画計算
                decimal[] dNyu = new decimal[8];
                decimal[] dUse = new decimal[8];
                decimal[] dOdr = new decimal[8];
                decimal[] dZai = new decimal[8];
                //表示フラグ
                int[] fNyu = new int[8] { 0, 0, 1, 1, 1, 1, 1, 0 };
                int[] fOdr = new int[8] { 0, 1, 1, 1, 1, 1, 1, 0 };
                decimal dTemp = 0;
                //パラメータ取得
                decimal dLot = dr.Lot; //最小ロット

                dUse[0] = 0;
                dUse[1] = dr.N0;
                dUse[2] = dr.N1;
                dUse[3] = dr.N2;
                dUse[4] = dr.N3;
                dUse[5] = dr.N4;
                dUse[6] = dr.N5;
                dUse[7] = 0;

                dNyu[0] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN_1.ToString("yyyyMMdd"), dtN0.ToString("yyyyMMdd"), Global.GetConnection());
                dNyu[1] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN0.ToString("yyyyMMdd"), dtN1.ToString("yyyyMMdd"), Global.GetConnection());
                dNyu[2] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN1.ToString("yyyyMMdd"), dtN2.ToString("yyyyMMdd"), Global.GetConnection());
                dNyu[3] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN2.ToString("yyyyMMdd"), dtN3.ToString("yyyyMMdd"), Global.GetConnection());
                dNyu[4] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN3.ToString("yyyyMMdd"), dtN4.ToString("yyyyMMdd"), Global.GetConnection());
                dNyu[5] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN4.ToString("yyyyMMdd"), dtN5.ToString("yyyyMMdd"), Global.GetConnection());
                dNyu[6] = ChumonClass.GetSumNyukoYotei(dr.BuhinCode, dtN5.ToString("yyyyMMdd"), dtN6.ToString("yyyyMMdd"), Global.GetConnection());

                dOdr[0] = ChumonClass.GetSumHacyuuSu(dr.BuhinCode, dtN_1.ToString("yyyy-MM-dd"), dtN0.ToString("yyyy-MM-dd"), Global.GetConnection());
                SeisanDataSet.V_ShoyouSuRow drN_1 = SeisanClass.GetV_ShoyouSuRow(dtN_1.ToString("yyyyMM"), dr.BuhinCode, Global.GetConnection());
                if (drN_1 != null)
                {
                    dUse[0] = drN_1.N0;
                }
                dZai[0] = dNyu[0] - dUse[0];

                for (int i = 1; i < 7; i++)
                {
                    if (dNyu[i] < 1)
                    {
                        dNyu[i] = dOdr[i - 1];
                        fNyu[i] = fOdr[i - 1] = 1;
                    }
                    else
                    {
                        fNyu[i] = fOdr[i - 1] = 0;
                    }
                    dZai[i] = dZai[i - 1] + dNyu[i] - dUse[i];
                    if ((dZai[i - 1] + dNyu[i] - dUse[i]) <= dUse[i + 1])
                    {
                        dTemp = Math.Ceiling(Math.Abs(dZai[i] - dUse[i + 1]) / dLot);
                        dOdr[i] = dLot * dTemp;
                    }
                    else
                    {
                        dOdr[i] = 0;
                    }
                }

                LblNyukaSuN_1.Text = dNyu[0].ToString("#,##0");
                LblNyukaSuN0.Text = dNyu[1].ToString("#,##0");
                LblNyukaSuN1.Text = dNyu[2].ToString("#,##0");
                LblNyukaSuN2.Text = dNyu[3].ToString("#,##0");
                LblNyukaSuN3.Text = dNyu[4].ToString("#,##0");
                LblNyukaSuN4.Text = dNyu[5].ToString("#,##0");
                LblNyukaSuN5.Text = dNyu[6].ToString("#,##0");

                LblShoyouSuN_1.Text = dUse[0].ToString("#,##0");
                LblShoyouSuN0.Text = dUse[1].ToString("#,##0");
                LblShoyouSuN1.Text = dUse[2].ToString("#,##0");
                LblShoyouSuN2.Text = dUse[3].ToString("#,##0");
                LblShoyouSuN3.Text = dUse[4].ToString("#,##0");
                LblShoyouSuN4.Text = dUse[5].ToString("#,##0");
                LblShoyouSuN5.Text = dUse[6].ToString("#,##0");

                LblHacchuSuN_1.Text = dOdr[0].ToString("#,##0");
                LblHacchuSuN0.Text = dOdr[1].ToString("#,##0");
                LblHacchuSuN1.Text = dOdr[2].ToString("#,##0");
                LblHacchuSuN2.Text = dOdr[3].ToString("#,##0");
                LblHacchuSuN3.Text = dOdr[4].ToString("#,##0");
                LblHacchuSuN4.Text = dOdr[5].ToString("#,##0");
                LblHacchuSuN5.Text = dOdr[6].ToString("#,##0");

                LblZaikoSuN_1.Text = dZai[0].ToString("#,##0");
                LblZaikoSuN0.Text = dZai[1].ToString("#,##0");
                LblZaikoSuN1.Text = dZai[2].ToString("#,##0");
                LblZaikoSuN2.Text = dZai[3].ToString("#,##0");
                LblZaikoSuN3.Text = dZai[4].ToString("#,##0");
                LblZaikoSuN4.Text = dZai[5].ToString("#,##0");
                LblZaikoSuN5.Text = dZai[6].ToString("#,##0");

                if (fNyu[1] == 1) { LblNyukaSuN0.ForeColor = Color.Red; }
                if (fNyu[2] == 1) { LblNyukaSuN1.ForeColor = Color.Red; }
                if (fNyu[3] == 1) { LblNyukaSuN2.ForeColor = Color.Red; }
                if (fNyu[4] == 1) { LblNyukaSuN3.ForeColor = Color.Red; }
                if (fNyu[5] == 1) { LblNyukaSuN4.ForeColor = Color.Red; }
                if (fNyu[6] == 1) { LblNyukaSuN5.ForeColor = Color.Red; }
                if (fOdr[1] == 1) { LblHacchuSuN0.ForeColor = Color.Red; }
                if (fOdr[2] == 1) { LblHacchuSuN1.ForeColor = Color.Red; }
                if (fOdr[3] == 1) { LblHacchuSuN2.ForeColor = Color.Red; }
                if (fOdr[4] == 1) { LblHacchuSuN3.ForeColor = Color.Red; }
                if (fOdr[5] == 1) { LblHacchuSuN4.ForeColor = Color.Red; }
                if (fOdr[6] == 1) { LblHacchuSuN5.ForeColor = Color.Red; }

            }
        }

        private string GetYM(int nM, string strOLDYM)
        {
            string strYM = "";
            DateTime dtBirth = DateTime.Parse(Utility.FormatFromyyyyMMdd(strOLDYM + "01"));
            dtBirth = dtBirth.AddMonths(nM);
            strYM = dtBirth.ToString().Substring(0, 5) + int.Parse(dtBirth.ToString().Substring(5, 2));
            return strYM;
        }

        protected void D_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void BtnPostback_Click(object sender, System.EventArgs e)
        {
            //string strPartsCode = strArgs[0];
            //string strBasho = strArgs[1];
            //string strBi = strArgs[2];
        }

        protected void BtnDelPostback_Click(object sender, EventArgs e)
        {
        }
        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];
            LibError err = null;

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
                    //this.Create();
                    //this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    break;

            }
        }

        public static void SetKijyunYM(DropDownList ddl)
        {
            ddl.Items.Clear();
            SqlDataAdapter da = new SqlDataAdapter("", Global.GetConnection());
            da.SelectCommand.CommandTimeout = 10000;
            da.SelectCommand.CommandText = "SELECT DISTINCT Nengetu FROM V_ShoyouSu ";
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            DataView dv = new DataView(dt);
            dv.Sort = "Nengetu";

            for (int i = 0; i < dv.Count; i++)
            {
                string Code = Convert.ToString(dv[i][0]);
                ddl.Items.Add(new ListItem(Code, Code));
            }

        }

        protected void DdlKijyunYM_SelectedIndexChanged(object sender, EventArgs e)
        {
            VsKijyunYM = DdlKijyunYM.SelectedValue;
            this.Create();
        }

        protected void DdlRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Create();
        }
        protected void DdlShiiresaki_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Create();
        }

        protected void BtnOD_Click(object sender, EventArgs e)
        {
            KoubaiDataSet.T_ChumonDataTable dt = new KoubaiDataSet.T_ChumonDataTable();
            int MaxHacchuuNo = ChumonClass.GetMaxHacchuuNo(Global.GetConnection());
            VsHacchuuNo = MaxHacchuuNo;

            for (int i = 0; i < D.Rows.Count; i++)
            {
                HtmlInputCheckBox ChkI = D.Rows[i].FindControl("ChkI") as HtmlInputCheckBox;
                Literal LitNengetu = D.Rows[i].FindControl("LitNengetu") as Literal;
                Literal LitBuhinCode = D.Rows[i].FindControl("LitBuhinCode") as Literal;
                Literal LitShiiresakiCode = D.Rows[i].FindControl("LitShiiresakiCode") as Literal;
                Literal LitLeadTime = D.Rows[i].FindControl("LitLeadTime") as Literal;
                Literal LitLot = D.Rows[i].FindControl("LitLot") as Literal;
                Label LblHacchuSuN0 = D.Rows[i].FindControl("LblHacchuSuN0") as Label;
                DateTime dtNow = DateTime.Now;
                DateTime dtTemp = DateTime.Now;
                int intTemp = 0;
                decimal decTanka = 0;
                decimal decSuryo = 0;
                decimal decKingaku = 0;

                if (ChkI.Checked)
                {
                    ChkI.Checked = false;
                    decSuryo = 0;
                    decimal.TryParse(LblHacchuSuN0.Text.Replace(",", ""), out decSuryo);
                    if (decSuryo <= 0) { continue; }

                    KoubaiDataSet.T_ChumonRow dr = dt.NewT_ChumonRow();

                    dr.Year = VsYaer;
                    dr.HacchuuNo = (VsHacchuuNo++).ToString("0000000");
                    dr.JigyoushoKubun = VsJigyoushoKubun;
                    dr.ShiiresakiCode = LitShiiresakiCode.Text;
                    dr.BuhinCode = LitBuhinCode.Text;
                    KoubaiDataSet.M_BuhinRow drM = BuhinClass.getM_BuhinRow(dr.BuhinCode, Global.GetConnection());
                    if (drM != null)
                    {
                        dr.BuhinKubun = drM.BuhinKubun;
                        dr.Tanka = drM.Tanka;
                    }
                    else
                    {
                        dr.BuhinKubun = string.Empty;
                        dr.Tanka = 0;
                    }
                    decKingaku = 0;
                    decTanka = dr.Tanka;
                    decKingaku = decTanka * decSuryo;
                    dr.Suuryou = (int)decSuryo;
                    dr.Kingaku = (int)decKingaku;
                    intTemp = 0;
                    int.TryParse(VsZeiritu, out intTemp);
                    dr.Zeiritu = intTemp;
                    intTemp = 0;
                    int.TryParse(LitLeadTime.Text, out intTemp);
                    dtTemp = DateTime.Today.AddDays(intTemp);
                    dr.Nouki = dtTemp.ToString("yyyyMMdd");
                    dr.KeigenZeirituFlg = Utility.GetKeigenZeirituFlg(dtTemp, VsZeiritu);
                    dr.NounyuuBashoCode = "08";
                    dr.Bikou = string.Empty;
                    dr.HacchuuBi = dtNow;
                    // デモ用　発注日は基準年月1日-LT
                    string strDemo = VsKijyunYM.Substring(0, 4) + "/" + VsKijyunYM.Substring(4, 2) + "/01";
                    DateTime dtDemi = DateTime.Parse(strDemo);
                    dr.HacchuuBi = dtDemi.AddDays(-14);
                    // デモ用　発注日は基準年月1日-LT
                    dr.HacchushaID = VsUserID;
                    dr.KannouFlg = false;
                    dr.KaritankaFlg = false;
                    dr.KeigenZeirituFlg = false;

                    dt.AddT_ChumonRow(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                LibError err = ChumonClass.T_Chumon_Insert(dt, Global.GetConnection());
                if (err != null)
                {
                    this.ShowMsg("発注に失敗しました<br/>" + err.Message, true);
                }
                else
                {
                    //// 仕入先配列を作成
                    //ArrayList aryShiire = new ArrayList();
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    if (!aryShiire.Contains(dt[i].ShiiresakiCode))
                    //    {
                    //        aryShiire.Add(dt[i].ShiiresakiCode);
                    //    }
                    //}
                    //for (int i = 0; i < aryShiire.Count; i++)
                    //{
                    //    // 主キーによって、メール送信に必要データ取得
                    //    ChumonDataSet.V_MailInfoDataTable dtMail = ChumonClass.getV_MailInfoDataTable(VsUserID, aryShiire[i].ToString(), Global.GetConnection());
                    //    for (int j = 0; j < dtMail.Rows.Count; j++)
                    //    {
                    //        MailClass.MailParam p = this.GetMailParam(dtMail[j]);
                    //        MailClass.SendMail(p, null);
                    //    }
                    //}
                }
            }

            this.Create();

        }

        private MailClass.MailParam GetMailParam(ChumonDataSet.V_MailInfoRow dr)
        {

            MailClass.MailParam p = new MailClass.MailParam();
            // 送信元メールアドレス
            p._MailFrom = dr.Mail_Y;
            // 送信先メールアドレス
            p._MailTo = dr.Mail_S;
            // 件名
            p._Subject = "新規発注情報のご案内";
            // 本文
            p._Body = MailClass.GetBody_ShinkiChumon(dr);
            // SMTP
            p._SMTP_Server = Global.SMTP_Server;
            return p;
        }












    }
}