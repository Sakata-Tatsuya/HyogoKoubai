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
    public partial class ProductsForcastForm : System.Web.UI.Page
    {
        private const int LIST_ID = 102;

        private string VsKijyunYearMonth
        {
            get
            {
                return (string)this.ViewState["VsKijyunYearMonth"];
            }
            set
            {
                this.ViewState["VsKijyunYearMonth"] = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            BtnDownload.Style["display"] = "none";
            this.LblMsg.Text = "";
            if (!this.IsPostBack)
            {
                ListSet.SetDdlShiiresakiMulti(DdlShiiresaki);
                SetKijyunYM(this.DdlKijyunYM);
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

            SeisanDataSet.T_SeisanKeikakuDataTable dtH = new SeisanDataSet.T_SeisanKeikakuDataTable();
            dtH = SeisanClass.GetT_SeisanKeikakuDataTable(p, Global.GetConnection());

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
            if (this.DdlKijyunYM.SelectedIndex > 0)
            {
                p._Nengetu = DdlKijyunYM.SelectedValue;
            }
            if (this.DdlShiiresaki.SelectedIndex > 0)
            {
                //w.Add(string.Format("KLDKoubaiGaichusaki='{0}'", this.DdlShiiresaki.SelectedValue));
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

            if (e.Row.RowType == DataControlRowType.Header)
            {
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SeisanDataSet.T_SeisanKeikakuRow dr = ((DataRowView)e.Row.DataItem).Row as SeisanDataSet.T_SeisanKeikakuRow;

                Literal LitNengetu = e.Row.FindControl("LitNengetu") as Literal;
                Literal LitSeason = e.Row.FindControl("LitSeason") as Literal;
                Literal LitSeihinCode = e.Row.FindControl("LitSeihinCode") as Literal;
                Literal LitSeisanSu = e.Row.FindControl("LitSeisanSu") as Literal;

                LitNengetu.Text = dr.Nengetu;
                LitSeason.Text = dr.Season;
                LitSeihinCode.Text = dr.SeihinCode;
                LitSeisanSu.Text = dr.SeisanSu.ToString("#,##0");
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
            da.SelectCommand.CommandText = "SELECT DISTINCT Nengetu FROM T_SeisanKeikaku ";
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

    }
}