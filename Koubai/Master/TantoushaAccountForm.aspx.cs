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
using KoubaiDAL;
namespace Koubai.Master
{
    public partial class TantoushaAccountForm : System.Web.UI.Page
    {
        private const int G_CELL_DELETE = 0;
        private const int G_CELL_SHUUSEI = 1;
        private const int G_CELL_JIGYOUSHO = 2;
        private const int G_CELL_TANTOUSHA_CODE = 3;
        private const int G_CELL_TANTOUSHA_MEI = 4;
        private const int G_CELL_SYOZOKU_BUSHO = 5;
        private const int G_CELL_YAKUSHOKU = 6;
        private const int G_CELL_MAIL = 7;
        private const int G_CELL_LOGIN_ID = 8;
        private const int G_CELL_PASSWORD = 9;
        private const int G_CELL_KANRISHA_KUBUN = 10;
       
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
            if (!this.IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Owner) // 発注側のみ表示可
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                M.MenuName = "マスタ管理 > 担当者(社内)";
                //CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                //tab.Menu = CtlTabMain.MainMenu.Master;
                //tab.MasterMenu = CtlTabMain.Master.Account;
                //tab.AccoutMenu = CtlTabMain.Account.Shanai;

                // 最初は非表示
                this.ShowTblMain(false);
                this.SetList();
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
            // 行数変更
            this.DdlRow.Attributes["onchange"] = "Row(); return false;";
            // 新規ボタン
            this.BtnNew.Attributes["onclick"] = "Shinki(); return false;";
            //　検索
            this.BtnKen.Attributes["onclick"] = "Kensaku(); return false;";
            // 削除(input)
            this.BtnS.Attributes["onclick"] = "Delete(); return false;";
            //Img
            this.Img1.Style.Add("display", "none");

        }
       
        // ページチェンジ
        private void OnPageIndexChanged(int nNewPageIndex)
        {
            VsCurrentPageIndex = nNewPageIndex;
            this.Create();
        }


        // クリエート
        private void Create()
        {
            // Hid_Clear
            this.HidChkID.Value = "";
            this.HidThisID.Value = "";

            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");

            LoginDataSet.V_TantoushaAccountDataTable dt = LoginClass.getV_TantoushaAccountDataTable(this.GetKensakuParam(), bKubun, Global.GetConnection());

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
        // 検索条件
        private LoginClass.KensakuParam GetKensakuParam()
        {
            LoginClass.KensakuParam k = new LoginClass.KensakuParam();

            // コード
            if (DdlTCode.SelectedIndex > 0)
            {
                k._Code = this.DdlTCode.SelectedValue;
            }
            if (DdlJigyousho.SelectedIndex > 0)
            {
                k._JigyoushoKubun = int.Parse(DdlJigyousho.SelectedValue);
            }

            return k;
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
           
        }
        private void SetList()
        {
            //
            ListSet.SetDdlTantousha(bKubun, DdlTCode);
            // 事業所
            ListSet.SetDdlJigyoushoKubun(bKubun, DdlJigyousho);
        }
        // 
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // 削除チェック
                HtmlInputCheckBox chkH = e.Row.FindControl("ChkH") as HtmlInputCheckBox;
                chkH.Attributes["onclick"] = "DelChk(this.checked)";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LoginDataSet.V_TantoushaAccountRow dr = ((DataRowView)e.Row.DataItem).Row as LoginDataSet.V_TantoushaAccountRow;

                // 削除
                HtmlInputCheckBox chk = e.Row.FindControl("ChkI") as HtmlInputCheckBox;
                
                 chk.Value = dr.LoginID;
                // chkID
                if (HidChkID.Value != "") this.HidChkID.Value += ",";
                this.HidChkID.Value += chk.ClientID;
                // 主キー                
                if (HidThisID.Value != "") this.HidThisID.Value += ",";
                this.HidThisID.Value += chk.Value;
                // 更新ボタン
                HtmlInputButton btn = e.Row.FindControl("BtnK") as HtmlInputButton;
                btn.Attributes["onclick"] =
                    string.Format("Update('{0}'); return false; ", chk.Value);
                // 事業所（09-07-31　呉）
                e.Row.Cells[G_CELL_JIGYOUSHO].Text = dr.EigyouSho;
                // 担当者コード
                e.Row.Cells[G_CELL_TANTOUSHA_CODE].Text = dr.TantoushaCode;
                // 担当者名
                e.Row.Cells[G_CELL_TANTOUSHA_MEI].Text = dr.Name;
                // 所属部署
                e.Row.Cells[G_CELL_SYOZOKU_BUSHO].Text = dr.Busho;
                // 役職
                e.Row.Cells[G_CELL_YAKUSHOKU].Text = dr.Yakushoku;
                // EMail
                e.Row.Cells[G_CELL_MAIL].Text = dr.Mail;
                // ログインID
                e.Row.Cells[G_CELL_LOGIN_ID].Text = dr.LoginID;
                // パスワード
                e.Row.Cells[G_CELL_PASSWORD].Text = "*****";
                // 管理者区分
                if (dr.KanrishaFlg)
                    e.Row.Cells[G_CELL_KANRISHA_KUBUN].Text = "○";
                else
                    e.Row.Cells[G_CELL_KANRISHA_KUBUN].Text = "";
            }
        }



        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
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
                // Ddl担当者をセット
                this.SetList();

                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DdlTCode);
            }
            else if (strCmd == "delete")
            {
                // 削除
                string[] strDelKeyAry = strArgs[1].Split(',');
                for (int i = 0; i < strDelKeyAry.Length; i++)
                {
                    string strCode = strDelKeyAry[i];
                    LibError err = LoginClass.M_Login_Delete(strCode, Global.GetConnection());
                    if (err != null)
                    {
                        this.ShowMsg(err.Message, false);
                        return;
                    }
                }
                this.Create(); // 再表示
                // Ddl担当者をセット
                this.SetList();
                this.ShowMsg("削除しました", false);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DdlTCode);
            }
        }

     
    }
}