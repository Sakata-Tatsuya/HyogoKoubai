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

namespace m2mKoubai.Master
{
    public partial class KaishaInfoForm : Core.Web.ServerViewStatePage
    {
        private const int G_CELL_DELETE = 0;
        private const int G_CELL_SHUUSEI = 1;
        private const int G_CELL_KAISHAID = 2;
       // private const int G_CELL_KAISHAMEI = 3; //更新: 09-08-19 呉
        private const int G_CELL_JIGYOUSHOMEI = 3;
        private const int G_CELL_JUSHO = 4;
        private const int G_CELL_YUUBIN = 5;
        private const int G_CELL_TEL = 6;
        private const int G_CELL_FAX = 7;
        private const int G_CELL_EMAIL = 8;

        private int VsKaishaID
        {
            get { return Convert.ToInt32(this.ViewState["VsKaishaID"]); }
            set { this.ViewState["VsKaishaID"] = value; }
        }

        private byte bKubun = (byte)UserKubun.Owner;


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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Owner)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }

                CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                tab.Menu = CtlTabMain.MainMenu.Master;
                tab.MasterMenu = CtlTabMain.Master.KaishaJyouhou;

                // Create
                this.ShowTblMain(false);
                this.Create();
                this.SetList();
            }

            // 改ページ
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
            this.PreRender += new EventHandler(Form_PreRender);
        }

        private void Form_PreRender(object sender, EventArgs e)
        {
            // 行数変更
            this.DdlRow.Attributes["onchange"] = "Row(); return false;";
            // 更新ボタン            
            //this.BtnK.Attributes["onclick"] = "Koushin(); return false;";
            // 新規登録ボタン
            this.BtnNew.Attributes["onclick"] = "Shinki(); return false;";
            // 検索ボタン
            this.BtnKen.Attributes["onclick"] = "Kensaku(); return false;";
            // 削除ボタン(input)
            this.BtnS.Attributes["onclick"] = "Delete(); return false;";
            // Img
            this.Img1.Style.Add("display", "none");


            if (G.Rows.Count > 0 && this.HidChkID.Value == "" && this.HidThisID.Value == "")
            {
                HtmlInputCheckBox chk =
                    G.HeaderRow.Cells[G_CELL_DELETE].FindControl("ChkH") as HtmlInputCheckBox;

                chk.Visible = false;
                this.BtnS.Visible = false;
            }


        }

        // Ddlセット
        private void SetList()
        {
            // Ddl事業所をセット
            ListSet.SetDdlJigyoushoKubun(bKubun, DdlJCode);

        }
        // ページチェンジ
        private void OnPageIndexChanged(int nNewPageIndex)
        {
            VsCurrentPageIndex = nNewPageIndex;
            this.Create();
        }
        // GridView表示
        private void ShowTblMain(bool b)
        {
            G.Visible = b;
            TblRow.Visible = b;
            this.BtnS.Visible = b;
        }
        // メッセージ表示
        private void ShowMsg(string strMsg, bool bErrorMsg)
        {
            this.LblMsg.ForeColor = (bErrorMsg) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
            this.LblMsg.Text = strMsg;
        }
        // クリエート
        private void Create()
        {
            // HidClear
            this.HidChkID.Value = "";
            this.HidThisID.Value = "";

            // Pt,Pb
            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");

            // データ取得
            //m2mKoubaiDataSet.T_KaishaInfoDataTable dtKaisha =
            //    KaishaInfoClass.getT_KaishaInfoDataTable(this.GetKensakuParam(), Global.GetConnection());
            // 変更（09-08-03　呉）
            LoginDataSet.V_Jigyousho_CountDataTable dt =
                KaishaInfoClass.getV_Jigyousho_CountDataTable(this.GetKensakuParam(), Global.GetConnection());

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

            // ページング
            int nPageSize = AllowPaging();
            int nPageCount = 0;

            if (nPageSize > 0)
            {
                G.PageSize = nPageSize;
                G.AllowPaging = true;
                nPageCount = dt.Rows.Count / nPageSize;
                if (0 < dt.Rows.Count % nPageSize) nPageCount++;
                if (nPageCount <= VsCurrentPageIndex)
                    VsCurrentPageIndex = 0;

                // 現在の表示行（何行目～何行目）
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
        }

        private int AllowPaging()
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
        // 検索条件
        private KaishaInfoClass.KensakuParam GetKensakuParam()
        {
            KaishaInfoClass.KensakuParam k = new KaishaInfoClass.KensakuParam();

            // 事業所コード
            if (DdlJCode.SelectedIndex > 0)
            {
                k._Code = this.DdlJCode.SelectedValue;
            }
            return k;
        }
     
        // Rowを表示
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // 削除チェック
                HtmlInputCheckBox chkH = e.Row.FindControl("ChkH") as HtmlInputCheckBox;
                chkH.Attributes["onclick"] = "DelChk(this.checked)";


            }
            // データ行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //m2mKoubaiDataSet.T_KaishaInfoRow dr =
                  //  ((DataRowView)e.Row.DataItem).Row as m2mKoubaiDataSet.T_KaishaInfoRow;

                // 担当者の件数
                LoginDataSet.V_Jigyousho_CountRow dr =
                   ((DataRowView)e.Row.DataItem).Row as LoginDataSet.V_Jigyousho_CountRow;

                // 削除
                HtmlInputCheckBox chk = e.Row.FindControl("ChkI") as HtmlInputCheckBox;               
                // 事業所に1人でも担当者がいる場合チェックボックスを外しておきたい    
                /*
                if (dtT.Rows.Count == 0)
                {

                    chk.Visible = true;
                }
                else
                {
                    chk.Visible = false;
                    e.Row.Cells[G_CELL_DELETE].CssClass = "hei30";
                }
                */
                // 変更（09-08-03　呉）
                if (dr.Count > 1)
                {
                    // 削除用チェックボックスを非表示
                    chk.Visible = false;
                }
                else
                {
                    // 削除用チェックボックスを表示
                    chk.Visible = true;                    
                   
                    // chkID
                    if (HidChkID.Value != "") this.HidChkID.Value += ",";
                    this.HidChkID.Value += chk.ClientID;
                    // 主キー
                    chk.Value = dr.KaishaID.ToString();

                    if (HidThisID.Value != "") this.HidThisID.Value += ",";
                    this.HidThisID.Value += chk.Value;
                }
             
                
                // 更新
                HtmlInputButton btn = e.Row.FindControl("BtnK") as HtmlInputButton;
                // 変更（09-08-19　呉）
                btn.Attributes["onclick"] =
                    string.Format("Update('{0}'); return false; ", dr.KaishaID);
                /*
                // 他の事業所の更新はできないようにボタンを隠しておく
                if (dr.KaishaID == SessionManager.JigyoushoKubun)
                {
                    btn.Visible = true;
                    btn.Attributes["onclick"] =
                        string.Format("Update('{0}'); return false; ", dr.KaishaID);
                }
                else
                {
                    btn.Visible = false;                       
                }
                */ 
                e.Row.Cells[G_CELL_SHUUSEI].CssClass = "hei30 tc";
                // 事業所コード
                e.Row.Cells[G_CELL_KAISHAID].Text = dr.KaishaID.ToString();
                /* //更新: 09-08-19 呉
                // 会社名
                e.Row.Cells[G_CELL_KAISHAMEI].Text = dr.KaishaMei;
                */ 
                // 事業所名
                e.Row.Cells[G_CELL_JIGYOUSHOMEI].Text = dr.EigyouSho;
                // 住所
                e.Row.Cells[G_CELL_JUSHO].Text = dr.Address;
                // 郵便番号
                e.Row.Cells[G_CELL_YUUBIN].Text = dr.Yuubin;
                // TEL
                e.Row.Cells[G_CELL_TEL].Text = dr.Tel;
                // FAX
                e.Row.Cells[G_CELL_FAX].Text = dr.Fax;
                // E-Mail
                e.Row.Cells[G_CELL_EMAIL].Text = dr.Mail;
            }
        }


        protected void Ram_AjaxRequest(object sender, Telerik.WebControls.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];

            if (strCmd == "page")
            {
                // ページ切り替え
                this.VsCurrentPageIndex = int.Parse(strArgs[1]);
                this.Create();
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
            }
            else if (strCmd == "kensaku")
            {
                // 検索
                this.VsCurrentPageIndex = 0;
                this.Create();
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
            }
            else if (strCmd == "row")
            {
                // 行数変更
                this.Create();

                this.SetList();
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DdlJCode);
            }
            else if (strCmd == "delete")
            {

                // 削除
                string[] strDelKeyAry = strArgs[1].Split(',');
                for (int i = 0; i < strDelKeyAry.Length; i++)
                {
                    string strCode = strDelKeyAry[i];
                    LibError err = KaishaInfoClass.T_KaishaInfo_Delete(strCode, Global.GetConnection());
                    if (err != null)
                    {
                        this.ShowMsg(err.Message, false);
                        return;
                    }
                }
                this.Create(); // 再表示
                this.SetList();
                this.ShowMsg("削除しました", false);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblKen);
            }       
        }
    }
}