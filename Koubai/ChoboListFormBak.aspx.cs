using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using KoubaiDAL;
using System.Collections.Generic;

namespace Koubai
{
    public partial class ChoboListFormBak : System.Web.UI.Page
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
        private string VsKaishaCode
        {
            get
            {
                return Convert.ToString(this.ViewState["VsKaishaCode"]);
            }
            set
            {
                this.ViewState["VsKaishaCode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M.MenuName = "帳票管理";
                VsKaishaCode = string.Empty;
                if (SessionManager.UserKubun != (byte)UserKubun.Owner)
                {
                    VsKaishaCode = SessionManager.KaishaCode;
                }
                // 検索リスト作成
                this.SetList();
                if (VsKaishaCode != string.Empty)
                {
                    DdlKaisha.SelectedValue = VsKaishaCode;
                    DdlKaisha.Enabled = false;
                }
            }
            this.Create();
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
            //BtnK.Attributes["onclick"] = "Kensaku();";
            // 行数変更
            this.DdlRow.Attributes["onchange"] = "RowChange(); return false;";
        }
        // ページチェンジ
        private void OnPageIndexChanged(int nNewPageIndex)
        {
            VsCurrentPageIndex = nNewPageIndex;
            this.Create();
        }
        private void Create()
        {
            HidKey.Value = "";
            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");

            FilesClass.KensakuParam k = this.GetKensakuParam();
            if (k == null)
            {
                this.ShowMsg("", true);
                return;
            }
            ShareDataSet.V_DocumentDataTable dt = FilesClass.getV_DocumentDataTable(k, Global.GetConnection());

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
                D.PageSize = nPageSize;
                D.AllowPaging = true;
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
                D.PageSize = dt.Rows.Count;
                D.AllowPaging = false;
                VsCurrentPageIndex = 0;
            }
            D.CurrentPageIndex = VsCurrentPageIndex;
            pagerTop.Create(nPageCount);
            pagerBottom.Create(nPageCount);
            pagerTop.CurrentPageIndex = pagerBottom.CurrentPageIndex = D.CurrentPageIndex;

            D.DataSource = dt;
            D.DataBind();
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
            D.Visible = b;
        }

        private void SetList()
        {
            // 帳票種別
            LoadDdl("DataType", VsKaishaCode, DdlDataType);
            // 取引先
            LoadDdl("KaishaCode,KaishaMei", VsKaishaCode, DdlKaisha);
        }
        private FilesClass.KensakuParam GetKensakuParam()
        {
            FilesClass.KensakuParam k = new FilesClass.KensakuParam();
            // ユーザー区分
            k._userKubun = (byte)SessionManager.UserKubun;
            // 取引先
            k._KaishaCode = DdlKaisha.SelectedValue;
            // 帳票種別
            k._DataType = DdlDataType.SelectedValue;
            // 計上日
            Common.CtlNengappiFromTo CtlKeijoBi = FindControl("CtlKeijoBi") as Common.CtlNengappiFromTo;
            if (CtlKeijoBi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._KeijoBi = CtlKeijoBi.GetNengappiKikan();
            }
            // 発行日
            Common.CtlNengappiFromTo CtlTourokuBi = FindControl("CtlTourokuBi") as Common.CtlNengappiFromTo;
            if (CtlTourokuBi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._TourokuBi = CtlTourokuBi.GetNengappiKikan();
            }
            return k;
        }
        protected void BtnKensaku_Click(object sender, EventArgs e)
        {
            Create();
        }

        protected void D_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType != Telerik.Web.UI.GridItemType.Item && e.Item.ItemType != Telerik.Web.UI.GridItemType.AlternatingItem)
                return;

            ShareDataSet.V_DocumentRow dr = ((DataRowView)e.Item.DataItem).Row as ShareDataSet.V_DocumentRow;
            Label LblDataType = e.Item.FindControl("LblDataType") as Label;
            Label LblKeijoBi = e.Item.FindControl("LblKeijoBi") as Label;
            Label LblSlipID = e.Item.FindControl("LblSlipID") as Label;
            Label LblKaisha = e.Item.FindControl("LblKaisha") as Label;
            Label LblTourokuBi = e.Item.FindControl("LblTourokuBi") as Label;
            ImageButton BtnDisp = e.Item.FindControl("BtnDisp") as ImageButton;
            //帳票種別
            LblDataType.Text = dr.DataType;
            //計上日
            LblKeijoBi.Text = dr.KeijoBi.ToString("yyyy/MM/dd");
            //帳票番号
            LblSlipID.Text = dr.SlipID;
            BtnDisp.CommandArgument = dr.FileID.ToString();
            //取引先
            LblKaisha.Text = dr.KaishaMei;
            //発行日
            LblTourokuBi.Text = dr.TourokuBi.ToString("yyyy/MM/dd");
        }
        protected void D_PreRender(object sender, EventArgs e)
        {
            //this.D.MasterTableView.Attributes["bordercolor"] = "#708090";
            this.D.MasterTableView.Attributes["border"] = "1";
            //this.D.MasterTableView.HeaderStyle.CssClass = "radgrid_header_cor";
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
            }
        }
        protected void G_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Command = e.CommandName;

            switch (Command)
            {
                //請求書番号押下で詳細を開くとき
                case "detail":
                    //VsInvoiceID = (string)e.CommandArgument;

                    ////請求書詳細をつくる
                    //CreateDetail(VsInvoiceID);
                    //Showpdf();
                    break;
                //PDFアイコンをクリックした時
                case "disp":
                    string strFileID = (string)e.CommandArgument;
                    HidFileID.Value = strFileID;
                    break;
            }
        }

        public static void LoadDdl(string strKoumoku,string strKaishaCode, DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("", ""));

            DataTable dt = FilesClass.getV_DocunemtColumnLoadDataTable(strKoumoku, strKaishaCode, Global.GetConnection());

            string[] strKoumokuArray = strKoumoku.Split(',');

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (strKoumokuArray.Length > 1)
                    ddl.Items.Add(new ListItem(dt.Rows[i][strKoumokuArray[0]].ToString() + ":" + dt.Rows[i][strKoumokuArray[1]].ToString(), dt.Rows[i][strKoumokuArray[0]].ToString()));
                else
                    ddl.Items.Add(new ListItem(dt.Rows[i][strKoumokuArray[0]].ToString(), dt.Rows[i][strKoumokuArray[0]].ToString()));
            }
        }

        protected void D_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            D.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            Create();
        }

        bool _bD_PageSizeChanged = false;
        protected void D_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            if (!_bD_PageSizeChanged)
            {
                _bD_PageSizeChanged = true;
                D.MasterTableView.PageSize = e.NewPageSize;
                Create();
            }
        }

        protected void D_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //string cmdArg = e.CommandArgument.ToString();
            //int dummy;
            //if (int.TryParse(cmdArg, out dummy)) { return; }
            //if (cmdArg == "First" || cmdArg == "Prev" || cmdArg == "Next" || cmdArg == "Last") { return; }
        }
        protected void BtnDisp_Click(object sender, EventArgs e)
        {
            ImageButton BtnDisp = sender as ImageButton;
            //string strFileID = (string)e.ToString();
            HidFileID.Value = BtnDisp.CommandArgument.ToString();
        }



































    }
}