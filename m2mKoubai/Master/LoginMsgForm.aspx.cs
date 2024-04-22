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
    public partial class LoginMsgForm : Core.Web.ServerViewStatePage
    {
        private const int G_CELL_SAKUJO = 0;
        private const int G_CELL_KOUSHIN = 1;
        private const int G_CELL_FLG = 2;
        private const int G_CELL_MSG = 3;
        private const int G_CELL_TOUROKUBI = 4;
        private const int G_CELL_KOUSHINBI = 5;

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
                CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                tab.Menu = CtlTabMain.MainMenu.Master;
                tab.MasterMenu = CtlTabMain.Master.Message;

                // 最初は非表示
                this.ShowTblMain(false);
                this.Create();
                ListSet.SetDdlSakujoFlag(DdlFlag);
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
            this.PreRender += new System.EventHandler(this.LoginMsgForm_PreRender);
        }

        private void LoginMsgForm_PreRender(object sender, EventArgs e)
        {            
            // 行数変更
            this.DdlRow.Attributes["onchange"] = "Row(); return false;";
            // 新規ボタン
            this.BtnNew.Attributes["onclick"] = "Shinki(); return false;";
            // 削除(input)
            this.BtnS.Attributes["onclick"] = "Delete(); return false;";
            // 検索
            this.BtnKen.Attributes["onclick"] = "Kensaku(); return false; ";
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

            // ログイ画面メッセージデータを取得する
            m2mKoubaiDataSet.M_LoginMsgDataTable dt =
                LoginMsgClass.getM_LoginMsgDataTable(this.GetKensakuParam(),  Global.GetConnection());

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
                m2mKoubaiDataSet.M_LoginMsgRow dr =
                    ((DataRowView)e.Row.DataItem).Row as m2mKoubaiDataSet.M_LoginMsgRow;
                // 削除
                HtmlInputCheckBox chk = e.Row.FindControl("ChkI") as HtmlInputCheckBox;
                // chkID
                if (HidChkID.Value != "") this.HidChkID.Value += ",";
                this.HidChkID.Value += chk.ClientID;
                // 主キー                
                if (HidThisID.Value != "") this.HidThisID.Value += ",";
                this.HidThisID.Value += dr.MsgID;
                // 更新ボタン
                HtmlInputButton btn = e.Row.FindControl("BK") as HtmlInputButton;
                btn.Attributes["onclick"] =
                    string.Format("Update('{0}'); return false; ", dr.MsgID);
                // msg
                e.Row.Cells[G_CELL_MSG].Text = dr.Msg.Replace("\r\n", "<br>");
                // 有効/無効
                e.Row.Cells[G_CELL_FLG].Text = AppCommon.YukouMukouText(dr.DelFlg);
                if (dr.DelFlg) // 無効
                    e.Row.Cells[G_CELL_FLG].ForeColor = System.Drawing.Color.Red;
                else // 有効
                    e.Row.Cells[G_CELL_FLG].ForeColor = System.Drawing.Color.Blue;
                // 登録日
                e.Row.Cells[G_CELL_TOUROKUBI].Text = dr.TourokuBi.ToString("yy/MM/dd<br>HH:mm");
                // 更新日
                if (!dr.IsKoushinBiNull())
                    e.Row.Cells[G_CELL_KOUSHINBI].Text = dr.KoushinBi.ToString("yy/MM/dd<br>HH:mm");

            }
        }
        // 検索条件
        private LoginMsgClass.KensakuParam GetKensakuParam()
        {
            LoginMsgClass.KensakuParam k = new LoginMsgClass.KensakuParam();

            // 表示
            if (this.DdlFlag.SelectedIndex > 0)
            {
                k._Flag = int.Parse(this.DdlFlag.SelectedValue);
            }
            return k;
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
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
               
            }
            else if (strCmd == "delete")
            {
                // 削除
                string[] strDelKeyAry = strArgs[1].Split(',');
                for (int i = 0; i < strDelKeyAry.Length; i++)
                {
                    int nMsgID = int.Parse(strDelKeyAry[i]);
                    LibError err = LoginMsgClass.M_LoginMsg_Delete(nMsgID, Global.GetConnection());
                    if (err != null)
                    {
                        this.ShowMsg(err.Message, false);
                        return;
                    }
                }
                this.Create(); // 再表示               
                this.ShowMsg("削除しました", false);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
               
            }
        }

      

       


    }
}
