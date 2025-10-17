using Core.Type;
using KoubaiDAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.DynamicData;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI.ExportInfrastructure;
using static System.Net.Mime.MediaTypeNames;
using Label = System.Web.UI.WebControls.Label;
using TextBox = System.Web.UI.WebControls.TextBox;

namespace Koubai.Jyuchu
{
    public partial class BuhinSimForm : System.Web.UI.Page
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
        private SeisanDataSet.V_ShoyouSimDataTable VsShoyouSim
        {
            get
            {
                return (SeisanDataSet.V_ShoyouSimDataTable)this.ViewState["VsShoyouSim"];
            }
            set
            {
                this.ViewState["VsShoyouSim"] = value;
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
                LoadInit();
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

        private void LoadInit()
        {
            if (DdlKijyunYM.SelectedIndex < 0)
            {
                this.ShowMsg("基準年月を指定してください。", true);
                return;
            }
            string sKijyunYM = VsKijyunYM.Substring(0, 4) + "/" + VsKijyunYM.Substring(4) + "/01";
            DateTime dtN0 = DateTime.Today;
            DateTime.TryParse(sKijyunYM, out dtN0);

            SeisanClass.KensakuParam p = new SeisanClass.KensakuParam();
            SetKensakuParam(p);

            SeisanDataSet.V_ShoyouSimDataTable dtH = new SeisanDataSet.V_ShoyouSimDataTable();
            dtH = SeisanClass.GetV_ShoyouSimDataTable(p, Global.GetConnection());

            if (1 > dtH.Rows.Count)
            {
                return;
            }
            VsShoyouSim = SeisanClass.ShoyouSimCulc(dtH, dtN0, Global.GetConnection());
        }

        protected void BtnKensaku_Click(object sender, EventArgs e)
        {
            LoadInit();
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

            if (VsShoyouSim != null)
            {
                if (VsShoyouSim.Rows.Count < 1)
                {
                    this.ShowMsg("資材所要量データがありません", true);
                    return;
                }
            }
            else 
            {
                this.ShowMsg("資材所要量データがありません", true);
                return;
            }

            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");

            this.ShowMsg(VsShoyouSim.Rows.Count + "件", false);
            //ページング
            int nPageSize = AloowPaging();
            int nPageCount = 0;
            if (nPageSize > 0)
            {
                D.PageSize = nPageSize;
                D.AllowPaging = true;
                nPageCount = VsShoyouSim.Rows.Count / nPageSize;
                if (0 < VsShoyouSim.Rows.Count % nPageSize) nPageCount++;
                if (nPageCount <= VsCurrentPageIndex)
                    VsCurrentPageIndex = 0;

                // 現在の表示行(何行目～何行目)
                int nStartCount = nPageSize * VsCurrentPageIndex + 1;
                int nEndCount = nStartCount + nPageSize - 1;
                if (nEndCount > VsShoyouSim.Rows.Count)
                    nEndCount = VsShoyouSim.Rows.Count;
                pagerTop.SetItemCounter(nStartCount, nEndCount);
                pagerBottom.SetItemCounter(nStartCount, nEndCount);
            }
            else
            {
                D.PageSize = VsShoyouSim.Rows.Count;
                D.AllowPaging = false;
                VsCurrentPageIndex = 0;
            }
            D.PageIndex = VsCurrentPageIndex;
            pagerTop.Create(nPageCount);
            pagerBottom.Create(nPageCount);
            pagerTop.CurrentPageIndex = pagerBottom.CurrentPageIndex = D.PageIndex;

            this.D.DataSource = VsShoyouSim;
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
                chkH.Attributes["onclick"] = "ChkAll(this.checked)";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SeisanDataSet.V_ShoyouSimRow dr = ((DataRowView)e.Row.DataItem).Row as SeisanDataSet.V_ShoyouSimRow;

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
                Literal LitTani = e.Row.FindControl("LitTani") as Literal;
                Label LblNyukaSuN_1 = e.Row.FindControl("LblNyukaSuN_1") as Label;
                Label LblNyukaSuN0 = e.Row.FindControl("LblNyukaSuN0") as Label;
                Label LblNyukaSuN1 = e.Row.FindControl("LblNyukaSuN1") as Label;
                Label LblNyukaSuN2 = e.Row.FindControl("LblNyukaSuN2") as Label;
                Label LblNyukaSuN3 = e.Row.FindControl("LblNyukaSuN3") as Label;
                Label LblNyukaSuN4 = e.Row.FindControl("LblNyukaSuN4") as Label;
                Label LblNyukaSuN5 = e.Row.FindControl("LblNyukaSuN5") as Label;
                Label LblNyukaSuTT = e.Row.FindControl("LblNyukaSuTT") as Label;
                Label LblShoyouSuN_1 = e.Row.FindControl("LblShoyouSuN_1") as Label;
                Label LblShoyouSuN0 = e.Row.FindControl("LblShoyouSuN0") as Label;
                Label LblShoyouSuN1 = e.Row.FindControl("LblShoyouSuN1") as Label;
                Label LblShoyouSuN2 = e.Row.FindControl("LblShoyouSuN2") as Label;
                Label LblShoyouSuN3 = e.Row.FindControl("LblShoyouSuN3") as Label;
                Label LblShoyouSuN4 = e.Row.FindControl("LblShoyouSuN4") as Label;
                Label LblShoyouSuN5 = e.Row.FindControl("LblShoyouSuN5") as Label;
                Label LblShoyouSuTT = e.Row.FindControl("LblShoyouSuTT") as Label;
                Label LblHacchuSuN_1 = e.Row.FindControl("LblHacchuSuN_1") as Label;
                //Label LblHacchuSuN0 = e.Row.FindControl("LblHacchuSuN0") as Label;
                TextBox TbxHacchuSuN0 = e.Row.FindControl("TbxHacchuSuN0") as TextBox;
                Label LblHacchuSuN1 = e.Row.FindControl("LblHacchuSuN1") as Label;
                Label LblHacchuSuN2 = e.Row.FindControl("LblHacchuSuN2") as Label;
                Label LblHacchuSuN3 = e.Row.FindControl("LblHacchuSuN3") as Label;
                Label LblHacchuSuN4 = e.Row.FindControl("LblHacchuSuN4") as Label;
                Label LblHacchuSuN5 = e.Row.FindControl("LblHacchuSuN5") as Label;
                Label LblHacchuSuTT = e.Row.FindControl("LblHacchuSuTT") as Label;
                Label LblZaikoSuN_1 = e.Row.FindControl("LblZaikoSuN_1") as Label;
                Label LblZaikoSuN0 = e.Row.FindControl("LblZaikoSuN0") as Label;
                Label LblZaikoSuN1 = e.Row.FindControl("LblZaikoSuN1") as Label;
                Label LblZaikoSuN2 = e.Row.FindControl("LblZaikoSuN2") as Label;
                Label LblZaikoSuN3 = e.Row.FindControl("LblZaikoSuN3") as Label;
                Label LblZaikoSuN4 = e.Row.FindControl("LblZaikoSuN4") as Label;
                Label LblZaikoSuN5 = e.Row.FindControl("LblZaikoSuN5") as Label;
                Label LblZaikoSuTT = e.Row.FindControl("LblZaikoSuTT") as Label;

                TbxHacchuSuN0.Attributes["onFocus"] = string.Format("CntRow('{0}')", e.Row.RowIndex);

                LitNengetu.Text = dr.Nengetu;
                LitBuhinCode.Text = dr.BuhinCode;
                LitShiiresakiCode.Text = dr.ShiiresakiCode1;
                LitShiiresakiMei.Text = ShiiresakiClass.GetShiiresakiMei(dr.ShiiresakiCode1, Global.GetConnection());
                LitLeadTime.Text = (dr.LT_Suuji * AppCommon.LT_Suuji(dr.LT_Tani)).ToString();
                LitLot.Text = dr.Lot.ToString();
                LitTani.Text = dr.Tani;

                ////発注計画計算
                //decimal[] dNyu = new decimal[8];
                //decimal[] dUse = new decimal[8];
                //decimal[] dOdr = new decimal[8];
                //decimal[] dZai = new decimal[8];
                //表示フラグ
                //int[] fNyu = new int[8] { 0, 0, 1, 1, 1, 1, 1, 0 };
                //int[] fOdr = new int[8] { 0, 1, 1, 1, 1, 1, 1, 0 };
                decimal dTemp = 0;
                //パラメータ取得
                decimal dLot = dr.Lot; //最小ロット

                LblNyukaSuN_1.Text = dr.NyuN_1.ToString("#,##0");
                LblNyukaSuN0.Text = dr.NyuN0.ToString("#,##0");
                LblNyukaSuN1.Text = dr.NyuN1.ToString("#,##0");
                LblNyukaSuN2.Text = dr.NyuN2.ToString("#,##0");
                LblNyukaSuN3.Text = dr.NyuN3.ToString("#,##0");
                LblNyukaSuN4.Text = dr.NyuN4.ToString("#,##0");
                LblNyukaSuN5.Text = dr.NyuN5.ToString("#,##0");
                decimal dNyuTT = dr.NyuN0 + dr.NyuN1 + dr.NyuN2 + dr.NyuN3 + dr.NyuN4 + dr.NyuN5;
                LblNyukaSuTT.Text = dNyuTT.ToString("#,##0");

                LblShoyouSuN_1.Text = dr.UseN_1.ToString("#,##0");
                LblShoyouSuN0.Text = dr.UseN0.ToString("#,##0");
                LblShoyouSuN1.Text = dr.UseN1.ToString("#,##0");
                LblShoyouSuN2.Text = dr.UseN2.ToString("#,##0");
                LblShoyouSuN3.Text = dr.UseN3.ToString("#,##0");
                LblShoyouSuN4.Text = dr.UseN4.ToString("#,##0");
                LblShoyouSuN5.Text = dr.UseN5.ToString("#,##0");
                decimal dUseTT = dr.UseN0 + dr.UseN1 + dr.UseN2 + dr.UseN3 + dr.UseN4 + dr.UseN5;
                LblShoyouSuTT.Text = dUseTT.ToString("#,##0");

                LblHacchuSuN_1.Text = dr.OdrN_1.ToString("#,##0");
                //LblHacchuSuN0.Text = dr.OdrN0.ToString("#,##0");
                TbxHacchuSuN0.Text = dr.OdrN0.ToString("#,##0");
                LblHacchuSuN1.Text = dr.OdrN1.ToString("#,##0");
                LblHacchuSuN2.Text = dr.OdrN2.ToString("#,##0");
                LblHacchuSuN3.Text = dr.OdrN3.ToString("#,##0");
                LblHacchuSuN4.Text = dr.OdrN4.ToString("#,##0");
                LblHacchuSuN5.Text = dr.OdrN5.ToString("#,##0");
                decimal dOdrTT = dr.OdrN0 + dr.OdrN1 + dr.OdrN2 + dr.OdrN3 + dr.OdrN4 + dr.OdrN5;
                LblHacchuSuTT.Text = dOdrTT.ToString("#,##0");

                LblZaikoSuN_1.Text = dr.ZaiN_1.ToString("#,##0");
                LblZaikoSuN0.Text = dr.ZaiN0.ToString("#,##0");
                LblZaikoSuN1.Text = dr.ZaiN1.ToString("#,##0");
                LblZaikoSuN2.Text = dr.ZaiN2.ToString("#,##0");
                LblZaikoSuN3.Text = dr.ZaiN3.ToString("#,##0");
                LblZaikoSuN4.Text = dr.ZaiN4.ToString("#,##0");
                LblZaikoSuN5.Text = dr.ZaiN5.ToString("#,##0");
                decimal dZaiTT = dr.ZaiN_1 + dNyuTT - dUseTT;
                LblZaikoSuTT.Text = dZaiTT.ToString("#,##0");

                //if (fNyu[1] == 1) { LblNyukaSuN0.ForeColor = Color.Red; }
                //if (fNyu[2] == 1) { LblNyukaSuN1.ForeColor = Color.Red; }
                //if (fNyu[3] == 1) { LblNyukaSuN2.ForeColor = Color.Red; }
                //if (fNyu[4] == 1) { LblNyukaSuN3.ForeColor = Color.Red; }
                //if (fNyu[5] == 1) { LblNyukaSuN4.ForeColor = Color.Red; }
                //if (fNyu[6] == 1) { LblNyukaSuN5.ForeColor = Color.Red; }
                //if (fOdr[1] == 1) { TbxHacchuSuN0.ForeColor = Color.Red; }
                //if (fOdr[2] == 1) { LblHacchuSuN1.ForeColor = Color.Red; }
                //if (fOdr[3] == 1) { LblHacchuSuN2.ForeColor = Color.Red; }
                //if (fOdr[4] == 1) { LblHacchuSuN3.ForeColor = Color.Red; }
                //if (fOdr[5] == 1) { LblHacchuSuN4.ForeColor = Color.Red; }
                //if (fOdr[6] == 1) { LblHacchuSuN5.ForeColor = Color.Red; }

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
            LoadInit();
            this.Create();
        }

        protected void DdlRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Create();
        }
        protected void DdlShiiresaki_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInit();
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
                //Label LblHacchuSuN0 = D.Rows[i].FindControl("LblHacchuSuN0") as Label;
                TextBox TbxHacchuSuN0 = D.Rows[i].FindControl("TbxHacchuSuN0") as TextBox;
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
                    decimal.TryParse(TbxHacchuSuN0.Text.Replace(",", ""), out decSuryo);
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
                    // デモ用暫定
                    DateTime dtNouki = DateTime.Now;
                    DateTime dtOder = DateTime.Now;
                    dtTemp = DateTime.Today;
                    DateTime.TryParse(VsKijyunYM.Substring(0,4)+"/"+ VsKijyunYM.Substring(4, 2) + "/01", out dtTemp);
                    dtNouki = dtTemp.AddMonths(1);
                    dtOder = dtNouki.AddDays(intTemp * -1);
                    //dtTemp = DateTime.Today.AddDays(intTemp);
                    //dr.Nouki = dtTemp.ToString("yyyyMMdd");
                    //dr.KeigenZeirituFlg = Utility.GetKeigenZeirituFlg(dtTemp, VsZeiritu);
                    dr.Nouki = dtNouki.ToString("yyyyMMdd");
                    dr.KeigenZeirituFlg = Utility.GetKeigenZeirituFlg(dtNouki, VsZeiritu);
                    dr.NounyuuBashoCode = "08";
                    dr.Bikou = string.Empty;
                    //dr.HacchuuBi = dtNow;
                    dr.HacchuuBi = dtOder;
                    // デモ用暫定
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
            else
            {
                this.ShowMsg("発注可能な明細が選択されていません<br/>", true);
            }

            LoadInit();
            this.D.DataSource = VsShoyouSim;
            this.D.DataBind();
            //this.Create();

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

        protected void Tbx_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;
            int nIndex = 0;
            int.TryParse(count.Value,out nIndex);
            string sKijyunYM = VsKijyunYM.Substring(0, 4) + "/" + VsKijyunYM.Substring(4) + "/01";
            DateTime dtN0 = DateTime.Today;
            DateTime.TryParse(sKijyunYM, out dtN0);

            SeisanDataSet.V_ShoyouSimRow drSS = GetMeisai(nIndex);
            SeisanDataSet.V_ShoyouSimRow drSv = SeisanClass.ShoyouSimRowCulc(drSS, dtN0, Global.GetConnection());

            //SeisanDataSet.V_ShoyouSimRow drSv = SeisanClass.ShoyouSimRowCulc(GetMeisai(nIndex), dtN0, Global.GetConnection());

            var dataRows = VsShoyouSim.Select("Nengetu = '" + drSv.Nengetu + "' and BuhinCode = '" + drSv.BuhinCode + "' ");
            foreach (DataRow dataRow in VsShoyouSim.Rows)
            {
                dataRow.ItemArray = drSv.ItemArray;
            }
            //for (int i = 0; i < VsShoyouSim.Rows.Count; i++)
            //{
            //    if (VsShoyouSim[i].Nengetu == drSv.Nengetu && VsShoyouSim[i].BuhinCode == drSv.BuhinCode)
            //    {
            //        VsShoyouSim[i].ItemArray = drSv.ItemArray;
            //    }
            //}
            SetMeisai(nIndex, drSv);

        }

        protected SeisanDataSet.V_ShoyouSimRow GetMeisai(int nIndex)
        {
            DateTime now = DateTime.Now;
            int intWork = 0;
            decimal decWork = 0;
            decimal dTemp = 0;
            DateTime dtWork = SqlDateTime.MinValue.Value;

            SeisanDataSet.V_ShoyouSimDataTable dtSv = new SeisanDataSet.V_ShoyouSimDataTable();

            Literal LitNengetu = D.Rows[nIndex].FindControl("LitNengetu") as Literal;
            Literal LitBuhinCode = D.Rows[nIndex].FindControl("LitBuhinCode") as Literal;
            Literal LitShiiresakiCode = D.Rows[nIndex].FindControl("LitShiiresakiCode") as Literal;
            Literal LitShiiresakiMei = D.Rows[nIndex].FindControl("LitShiiresakiMei") as Literal;
            Literal LitLeadTime = D.Rows[nIndex].FindControl("LitLeadTime") as Literal;
            Literal LitLot = D.Rows[nIndex].FindControl("LitLot") as Literal;
            Literal LitTani = D.Rows[nIndex].FindControl("LitTani") as Literal;
            Label LblNyukaSuN_1 = D.Rows[nIndex].FindControl("LblNyukaSuN_1") as Label;
            Label LblNyukaSuN0 = D.Rows[nIndex].FindControl("LblNyukaSuN0") as Label;
            Label LblNyukaSuN1 = D.Rows[nIndex].FindControl("LblNyukaSuN1") as Label;
            Label LblNyukaSuN2 = D.Rows[nIndex].FindControl("LblNyukaSuN2") as Label;
            Label LblNyukaSuN3 = D.Rows[nIndex].FindControl("LblNyukaSuN3") as Label;
            Label LblNyukaSuN4 = D.Rows[nIndex].FindControl("LblNyukaSuN4") as Label;
            Label LblNyukaSuN5 = D.Rows[nIndex].FindControl("LblNyukaSuN5") as Label;
            Label LblNyukaSuTT = D.Rows[nIndex].FindControl("LblNyukaSuTT") as Label;
            Label LblShoyouSuN_1 = D.Rows[nIndex].FindControl("LblShoyouSuN_1") as Label;
            Label LblShoyouSuN0 = D.Rows[nIndex].FindControl("LblShoyouSuN0") as Label;
            Label LblShoyouSuN1 = D.Rows[nIndex].FindControl("LblShoyouSuN1") as Label;
            Label LblShoyouSuN2 = D.Rows[nIndex].FindControl("LblShoyouSuN2") as Label;
            Label LblShoyouSuN3 = D.Rows[nIndex].FindControl("LblShoyouSuN3") as Label;
            Label LblShoyouSuN4 = D.Rows[nIndex].FindControl("LblShoyouSuN4") as Label;
            Label LblShoyouSuN5 = D.Rows[nIndex].FindControl("LblShoyouSuN5") as Label;
            Label LblShoyouSuTT = D.Rows[nIndex].FindControl("LblShoyouSuTT") as Label;
            Label LblHacchuSuN_1 = D.Rows[nIndex].FindControl("LblHacchuSuN_1") as Label;
            TextBox TbxHacchuSuN0 = D.Rows[nIndex].FindControl("TbxHacchuSuN0") as TextBox;
            Label LblHacchuSuN1 = D.Rows[nIndex].FindControl("LblHacchuSuN1") as Label;
            Label LblHacchuSuN2 = D.Rows[nIndex].FindControl("LblHacchuSuN2") as Label;
            Label LblHacchuSuN3 = D.Rows[nIndex].FindControl("LblHacchuSuN3") as Label;
            Label LblHacchuSuN4 = D.Rows[nIndex].FindControl("LblHacchuSuN4") as Label;
            Label LblHacchuSuN5 = D.Rows[nIndex].FindControl("LblHacchuSuN5") as Label;
            Label LblHacchuSuTT = D.Rows[nIndex].FindControl("LblHacchuSuTT") as Label;
            Label LblZaikoSuN_1 = D.Rows[nIndex].FindControl("LblZaikoSuN_1") as Label;
            Label LblZaikoSuN0 = D.Rows[nIndex].FindControl("LblZaikoSuN0") as Label;
            Label LblZaikoSuN1 = D.Rows[nIndex].FindControl("LblZaikoSuN1") as Label;
            Label LblZaikoSuN2 = D.Rows[nIndex].FindControl("LblZaikoSuN2") as Label;
            Label LblZaikoSuN3 = D.Rows[nIndex].FindControl("LblZaikoSuN3") as Label;
            Label LblZaikoSuN4 = D.Rows[nIndex].FindControl("LblZaikoSuN4") as Label;
            Label LblZaikoSuN5 = D.Rows[nIndex].FindControl("LblZaikoSuN5") as Label;
            Label LblZaikoSuTT = D.Rows[nIndex].FindControl("LblZaikoSuTT") as Label;

            SeisanDataSet.V_ShoyouSimRow drSv = dtSv.NewV_ShoyouSimRow();

            drSv.Nengetu = LitNengetu.Text;
            drSv.BuhinCode = LitBuhinCode.Text;
            drSv.ShiiresakiCode1 = LitShiiresakiCode.Text;
            KoubaiDataSet.M_BuhinRow drM = BuhinClass.getM_BuhinRow(drSv.BuhinCode, Global.GetConnection());
            if (drM != null)
            {
                drSv.Lot = drM.Lot;
                drSv.Tani = drM.Tani;
                drSv.LT_Suuji = drM.LT_Suuji;
                drSv.LT_Tani = drM.LT_Tani;
                drSv.Tanka = drM.Tanka;
            }
            else
            {
                intWork = 0;
                int.TryParse(LitLot.Text.Trim().Replace(",", ""), out intWork);
                drSv.Lot = intWork;
                drSv.Tani = LitTani.Text;
                drSv.LT_Suuji = 2;
                drSv.LT_Tani = 2;
                drSv.Tanka = 0;
            }
            decWork = 0;
            decimal.TryParse(LblNyukaSuN_1.Text.Trim().Replace(",", ""), out decWork);
            drSv.NyuN_1 = decWork;
            decWork = 0;
            decimal.TryParse(LblNyukaSuN0.Text.Trim().Replace(",", ""), out decWork);
            drSv.NyuN0 = decWork;
            decWork = 0;
            decimal.TryParse(LblNyukaSuN1.Text.Trim().Replace(",", ""), out decWork);
            drSv.NyuN1 = decWork;
            decWork = 0;
            decimal.TryParse(LblNyukaSuN2.Text.Trim().Replace(",", ""), out decWork);
            drSv.NyuN2 = decWork;
            decWork = 0;
            decimal.TryParse(LblNyukaSuN3.Text.Trim().Replace(",", ""), out decWork);
            drSv.NyuN3 = decWork;
            decWork = 0;
            decimal.TryParse(LblNyukaSuN4.Text.Trim().Replace(",", ""), out decWork);
            drSv.NyuN4 = decWork;
            decWork = 0;
            decimal.TryParse(LblNyukaSuN5.Text.Trim().Replace(",", ""), out decWork);
            drSv.NyuN5 = decWork;
            drSv.NyuN6 = 0;
            decWork = 0;
            decimal.TryParse(LblShoyouSuN_1.Text.Trim().Replace(",", ""), out decWork);
            drSv.UseN_1 = decWork;
            decWork = 0;
            decimal.TryParse(LblShoyouSuN0.Text.Trim().Replace(",", ""), out decWork);
            drSv.UseN0 = decWork;
            decWork = 0;
            decimal.TryParse(LblShoyouSuN1.Text.Trim().Replace(",", ""), out decWork);
            drSv.UseN1 = decWork;
            decWork = 0;
            decimal.TryParse(LblShoyouSuN2.Text.Trim().Replace(",", ""), out decWork);
            drSv.UseN2 = decWork;
            decWork = 0;
            decimal.TryParse(LblShoyouSuN3.Text.Trim().Replace(",", ""), out decWork);
            drSv.UseN3 = decWork;
            decWork = 0;
            decimal.TryParse(LblShoyouSuN4.Text.Trim().Replace(",", ""), out decWork);
            drSv.UseN4 = decWork;
            decWork = 0;
            decimal.TryParse(LblShoyouSuN5.Text.Trim().Replace(",", ""), out decWork);
            drSv.UseN5 = decWork;
            drSv.UseN6 = 0;
            decWork = 0;
            decimal.TryParse(LblHacchuSuN_1.Text.Trim().Replace(",", ""), out decWork);
            drSv.OdrN_1 = decWork;
            decWork = 0;
            decimal.TryParse(TbxHacchuSuN0.Text.Trim().Replace(",", ""), out decWork);
            if (decWork > 0 && drSv.Lot > 0)
            {
                dTemp = Math.Ceiling(decWork / drSv.Lot);
                decWork = drSv.Lot * dTemp;
            }
            drSv.OdrN0 = decWork;
            decWork = 0;
            decimal.TryParse(LblHacchuSuN1.Text.Trim().Replace(",", ""), out decWork);
            drSv.OdrN1 = decWork;
            decWork = 0;
            decimal.TryParse(LblHacchuSuN2.Text.Trim().Replace(",", ""), out decWork);
            drSv.OdrN2 = decWork;
            decWork = 0;
            decimal.TryParse(LblHacchuSuN3.Text.Trim().Replace(",", ""), out decWork);
            drSv.OdrN3 = decWork;
            decWork = 0;
            decimal.TryParse(LblHacchuSuN4.Text.Trim().Replace(",", ""), out decWork);
            drSv.OdrN4 = decWork;
            decWork = 0;
            decimal.TryParse(LblHacchuSuN5.Text.Trim().Replace(",", ""), out decWork);
            drSv.OdrN5 = decWork;
            drSv.OdrN6 = 0;
            decWork = 0;
            decimal.TryParse(LblZaikoSuN_1.Text.Trim().Replace(",", ""), out decWork);
            drSv.ZaiN_1 = decWork;
            decWork = 0;
            decimal.TryParse(LblZaikoSuN0.Text.Trim().Replace(",", ""), out decWork);
            drSv.ZaiN0 = decWork;
            decWork = 0;
            decimal.TryParse(LblZaikoSuN1.Text.Trim().Replace(",", ""), out decWork);
            drSv.ZaiN1 = decWork;
            decWork = 0;
            decimal.TryParse(LblZaikoSuN2.Text.Trim().Replace(",", ""), out decWork);
            drSv.ZaiN2 = decWork;
            decWork = 0;
            decimal.TryParse(LblZaikoSuN3.Text.Trim().Replace(",", ""), out decWork);
            drSv.ZaiN3 = decWork;
            decWork = 0;
            decimal.TryParse(LblZaikoSuN4.Text.Trim().Replace(",", ""), out decWork);
            drSv.ZaiN4 = decWork;
            decWork = 0;
            decimal.TryParse(LblZaikoSuN5.Text.Trim().Replace(",", ""), out decWork);
            drSv.ZaiN5 = decWork;
            drSv.ZaiN6 = 0;

            return drSv;
        }

        protected void SetMeisai(int nIndex, SeisanDataSet.V_ShoyouSimRow dr)
        {
            Literal LitNengetu = D.Rows[nIndex].FindControl("LitNengetu") as Literal;
            Literal LitBuhinCode = D.Rows[nIndex].FindControl("LitBuhinCode") as Literal;
            Literal LitShiiresakiCode = D.Rows[nIndex].FindControl("LitShiiresakiCode") as Literal;
            Literal LitShiiresakiMei = D.Rows[nIndex].FindControl("LitShiiresakiMei") as Literal;
            Literal LitLeadTime = D.Rows[nIndex].FindControl("LitLeadTime") as Literal;
            Literal LitLot = D.Rows[nIndex].FindControl("LitLot") as Literal;
            Literal LitTani = D.Rows[nIndex].FindControl("LitTani") as Literal;
            Label LblNyukaSuN_1 = D.Rows[nIndex].FindControl("LblNyukaSuN_1") as Label;
            Label LblNyukaSuN0 = D.Rows[nIndex].FindControl("LblNyukaSuN0") as Label;
            Label LblNyukaSuN1 = D.Rows[nIndex].FindControl("LblNyukaSuN1") as Label;
            Label LblNyukaSuN2 = D.Rows[nIndex].FindControl("LblNyukaSuN2") as Label;
            Label LblNyukaSuN3 = D.Rows[nIndex].FindControl("LblNyukaSuN3") as Label;
            Label LblNyukaSuN4 = D.Rows[nIndex].FindControl("LblNyukaSuN4") as Label;
            Label LblNyukaSuN5 = D.Rows[nIndex].FindControl("LblNyukaSuN5") as Label;
            Label LblNyukaSuTT = D.Rows[nIndex].FindControl("LblNyukaSuTT") as Label;
            Label LblShoyouSuN_1 = D.Rows[nIndex].FindControl("LblShoyouSuN_1") as Label;
            Label LblShoyouSuN0 = D.Rows[nIndex].FindControl("LblShoyouSuN0") as Label;
            Label LblShoyouSuN1 = D.Rows[nIndex].FindControl("LblShoyouSuN1") as Label;
            Label LblShoyouSuN2 = D.Rows[nIndex].FindControl("LblShoyouSuN2") as Label;
            Label LblShoyouSuN3 = D.Rows[nIndex].FindControl("LblShoyouSuN3") as Label;
            Label LblShoyouSuN4 = D.Rows[nIndex].FindControl("LblShoyouSuN4") as Label;
            Label LblShoyouSuN5 = D.Rows[nIndex].FindControl("LblShoyouSuN5") as Label;
            Label LblShoyouSuTT = D.Rows[nIndex].FindControl("LblShoyouSuTT") as Label;
            Label LblHacchuSuN_1 = D.Rows[nIndex].FindControl("LblHacchuSuN_1") as Label;
            TextBox TbxHacchuSuN0 = D.Rows[nIndex].FindControl("TbxHacchuSuN0") as TextBox;
            Label LblHacchuSuN1 = D.Rows[nIndex].FindControl("LblHacchuSuN1") as Label;
            Label LblHacchuSuN2 = D.Rows[nIndex].FindControl("LblHacchuSuN2") as Label;
            Label LblHacchuSuN3 = D.Rows[nIndex].FindControl("LblHacchuSuN3") as Label;
            Label LblHacchuSuN4 = D.Rows[nIndex].FindControl("LblHacchuSuN4") as Label;
            Label LblHacchuSuN5 = D.Rows[nIndex].FindControl("LblHacchuSuN5") as Label;
            Label LblHacchuSuTT = D.Rows[nIndex].FindControl("LblHacchuSuTT") as Label;
            Label LblZaikoSuN_1 = D.Rows[nIndex].FindControl("LblZaikoSuN_1") as Label;
            Label LblZaikoSuN0 = D.Rows[nIndex].FindControl("LblZaikoSuN0") as Label;
            Label LblZaikoSuN1 = D.Rows[nIndex].FindControl("LblZaikoSuN1") as Label;
            Label LblZaikoSuN2 = D.Rows[nIndex].FindControl("LblZaikoSuN2") as Label;
            Label LblZaikoSuN3 = D.Rows[nIndex].FindControl("LblZaikoSuN3") as Label;
            Label LblZaikoSuN4 = D.Rows[nIndex].FindControl("LblZaikoSuN4") as Label;
            Label LblZaikoSuN5 = D.Rows[nIndex].FindControl("LblZaikoSuN5") as Label;
            Label LblZaikoSuTT = D.Rows[nIndex].FindControl("LblZaikoSuTT") as Label;

            LitNengetu.Text = dr.Nengetu;
            LitBuhinCode.Text = dr.BuhinCode;
            LitShiiresakiCode.Text = dr.ShiiresakiCode1;
            LitShiiresakiMei.Text = ShiiresakiClass.GetShiiresakiMei(dr.ShiiresakiCode1, Global.GetConnection());
            LitLeadTime.Text = (dr.LT_Suuji * AppCommon.LT_Suuji(dr.LT_Tani)).ToString();
            LitLot.Text = dr.Lot.ToString();
            LitTani.Text = dr.Tani;

            LblNyukaSuN_1.Text = dr.NyuN_1.ToString("#,##0");
            LblNyukaSuN0.Text = dr.NyuN0.ToString("#,##0");
            LblNyukaSuN1.Text = dr.NyuN1.ToString("#,##0");
            LblNyukaSuN2.Text = dr.NyuN2.ToString("#,##0");
            LblNyukaSuN3.Text = dr.NyuN3.ToString("#,##0");
            LblNyukaSuN4.Text = dr.NyuN4.ToString("#,##0");
            LblNyukaSuN5.Text = dr.NyuN5.ToString("#,##0");
            decimal dNyuTT = dr.NyuN0 + dr.NyuN1 + dr.NyuN2 + dr.NyuN3 + dr.NyuN4 + dr.NyuN5;
            LblNyukaSuTT.Text = dNyuTT.ToString("#,##0");

            LblShoyouSuN_1.Text = dr.UseN_1.ToString("#,##0");
            LblShoyouSuN0.Text = dr.UseN0.ToString("#,##0");
            LblShoyouSuN1.Text = dr.UseN1.ToString("#,##0");
            LblShoyouSuN2.Text = dr.UseN2.ToString("#,##0");
            LblShoyouSuN3.Text = dr.UseN3.ToString("#,##0");
            LblShoyouSuN4.Text = dr.UseN4.ToString("#,##0");
            LblShoyouSuN5.Text = dr.UseN5.ToString("#,##0");
            decimal dUseTT = dr.UseN0 + dr.UseN1 + dr.UseN2 + dr.UseN3 + dr.UseN4 + dr.UseN5;
            LblShoyouSuTT.Text = dUseTT.ToString("#,##0");

            LblHacchuSuN_1.Text = dr.OdrN_1.ToString("#,##0");
            //LblHacchuSuN0.Text = dr.OdrN0.ToString("#,##0");
            TbxHacchuSuN0.Text = dr.OdrN0.ToString("#,##0");
            LblHacchuSuN1.Text = dr.OdrN1.ToString("#,##0");
            LblHacchuSuN2.Text = dr.OdrN2.ToString("#,##0");
            LblHacchuSuN3.Text = dr.OdrN3.ToString("#,##0");
            LblHacchuSuN4.Text = dr.OdrN4.ToString("#,##0");
            LblHacchuSuN5.Text = dr.OdrN5.ToString("#,##0");
            decimal dOdrTT = dr.OdrN0 + dr.OdrN1 + dr.OdrN2 + dr.OdrN3 + dr.OdrN4 + dr.OdrN5;
            LblHacchuSuTT.Text = dOdrTT.ToString("#,##0");

            LblZaikoSuN_1.Text = dr.ZaiN_1.ToString("#,##0");
            LblZaikoSuN0.Text = dr.ZaiN0.ToString("#,##0");
            LblZaikoSuN1.Text = dr.ZaiN1.ToString("#,##0");
            LblZaikoSuN2.Text = dr.ZaiN2.ToString("#,##0");
            LblZaikoSuN3.Text = dr.ZaiN3.ToString("#,##0");
            LblZaikoSuN4.Text = dr.ZaiN4.ToString("#,##0");
            LblZaikoSuN5.Text = dr.ZaiN5.ToString("#,##0");
            decimal dZaiTT = dr.ZaiN_1 + dNyuTT - dUseTT;
            LblZaikoSuTT.Text = dZaiTT.ToString("#,##0");

        }











    }
}