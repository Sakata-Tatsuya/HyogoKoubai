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
    public partial class ShiiresakiForm : System.Web.UI.Page
    {
        private const int GV_CELL_CHK = 0;
        private const int GV_CELL_KOUSIN = 1;
        private const int GV_CELL_SHIIRESAKI_CODE = 2;
        private const int GV_CELL_SHIIRESAKI_MEI = 3;
        private const int GV_CELL_YUBIN_BANGOU = 4;
        private const int GV_CELL_ADDRESS = 5;
        private const int GV_CELL_TEL = 6;
        private const int GV_CELL_FAX = 7;
        private const int GV_CELL_KOUZAMEIGI = 8;
        private const int GV_CELL_KINYUUKIKAN_MEI = 9;
        private const int GV_CELL_KOUZABANGOU = 10;
        private const int GV_CELL_SHIHARAI_SHIMEBI = 11;
        private const int GV_CELL_SHIHARAI_YOTEIBI = 12;
        private const int GV_CELL_KENSYUU_JYOUHOU_KOUKAI = 13;
        private const int GV_CELL_NOUKIKAITOU_SAISOKU_MAIL = 14;
        private const int GV_CELL_SHIIRESAKI_KOUSHINKYOKA = 15;
        private const int GV_CELL_INVOICEFLAG = 16;
        private const int GV_CELL_INVOICENO = 17;

        Core.Collection.StringCollections m_objStringCols = new Core.Collection.StringCollections();

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
            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");
            pagerTop.OnPageIndexChanged += new Common.CtlMyPager.CtlMyPagerEventHandler(this.OnPageIndexChanged);
            pagerBottom.OnPageIndexChanged += new Common.CtlMyPager.CtlMyPagerEventHandler(this.OnPageIndexChanged);
            pagerTop.ClientEvent = pagerBottom.ClientEvent = "PageChange";

            if (!this.IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Owner)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                M.MenuName = "マスタ管理 > 仕入先";
                //CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                //tab.Menu = CtlTabMain.MainMenu.Master;
                //tab.MasterMenu = CtlTabMain.Master.Shiiresaki;

                this.DdlLoad();
                this.Create();

            }
        }
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.PreRender += new System.EventHandler(this.ShiiresakiForm_PreRender);
        }

        private void ShiiresakiForm_PreRender(object sender, EventArgs e)
        {
            // 行数変更
            this.DdlRow.Attributes["onchange"] = "Row(); return false;";
            // 新規ボタン
            this.BtnNew.Attributes["onclick"] = "Shinki(); return false;";
            //　検索
            this.BtnKensaku.Attributes["onclick"] = "Kensaku(); return false;";
            // 削除(input)
            this.BtnS.Attributes["onclick"] = "Delete(); return false;";
            //Img
            this.Img1.Style.Add("display", "none");
        }

        private void OnPageIndexChanged(int nNewPageIndex)
        {
            VsCurrentPageIndex = nNewPageIndex;
            this.Create();
        }

        // 検索用Ddlをロード
        private void DdlLoad()
        {
            // 仕入先
            KoubaiDataSet.M_ShiiresakiDataTable dt = ShiiresakiClass.getM_ShiiresakiDataTable(Global.GetConnection());
            DdlCode.Items.Clear();
            DdlCode.Items.Add(new ListItem("-----", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DdlCode.Items.Add(new ListItem(String.Format(dt[i].ShiiresakiCode.ToString() + " : " + dt[i].ShiiresakiMei.ToString()), dt[i].ShiiresakiCode.ToString()));
            }
        }


        private void Create()
        {
            // Hid_Clear
            this.HidChkID.Value = "";
            this.HidThisID.Value = "";

            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");

            KoubaiDataSet.M_ShiiresakiDataTable dtShiiresaki = ShiiresakiClass.getM_ShiiresakiDataTable(this.GetKensakuParam(), Global.GetConnection());

            this.ShowMsg(dtShiiresaki.Rows.Count + "件", false);
            if (dtShiiresaki.Rows.Count == 0)
            {
                pagerTop.DdlClear();
                pagerBottom.DdlClear();
                this.ShowT_Gv(false);
                return;
            }

            DataView dv = dtShiiresaki.DefaultView;

            //ページング
            int nPageSize = AloowPaging();
            int nPageCount = 0;

            if (nPageSize > 0)
            {
                G.PageSize = nPageSize;
                G.AllowPaging = true;
                nPageCount = dv.Count / nPageSize;
                if (0 < dv.Count % nPageSize) nPageCount++;
                if (nPageCount <= VsCurrentPageIndex)
                    VsCurrentPageIndex = 0;

                // 現在の表示行(何行目～何行目)
                int nStartCount = nPageSize * VsCurrentPageIndex + 1;
                int nEndCount = nStartCount + nPageSize - 1;
                if (nEndCount > dv.Count)
                    nEndCount = dv.Count;
                pagerTop.SetItemCounter(nStartCount, nEndCount);
                pagerBottom.SetItemCounter(nStartCount, nEndCount);
            }
            else
            {
                G.PageSize = dv.Count;
                G.AllowPaging = false;
                VsCurrentPageIndex = 0;
            }


            G.PageIndex = VsCurrentPageIndex;
            pagerTop.Create(nPageCount);
            pagerBottom.Create(nPageCount);
            pagerTop.CurrentPageIndex = pagerBottom.CurrentPageIndex = G.PageIndex;

            G.DataSource = dv;
            G.DataBind();
            this.ShowT_Gv(true);
            G.EnableViewState = false;
        }

        //GridView表示、非表示
        private void ShowT_Gv(bool b)
        {
            T_Gv.Visible = b;
        }

        private ShiiresakiClass.KensakuParam GetKensakuParam()
        {
            ShiiresakiClass.KensakuParam k = new ShiiresakiClass.KensakuParam();

            // コード
            if (DdlCode.SelectedIndex > 0)
            {
                k._Code = this.DdlCode.SelectedValue;
            }
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

        // 検索ボタンクリック
        protected void BtnKensaku_Click(object sender, EventArgs e)
        {
            this.VsCurrentPageIndex = 0;
            this.Create();
        }

        // メッセージ表示
        private void ShowMsg(string strMsg, bool bErr)
        {
            this.LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bErr) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }

        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // 削除チェック
                HtmlInputCheckBox chkH = e.Row.Cells[GV_CELL_CHK].FindControl("ChkH") as HtmlInputCheckBox;
                chkH.Attributes["onclick"] = "DelChk(this.checked)";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                KoubaiDataSet.M_ShiiresakiRow dr = ((DataRowView)e.Row.DataItem).Row as KoubaiDataSet.M_ShiiresakiRow;
              
                // 削除
                HtmlInputCheckBox chk = e.Row.FindControl("ChkI") as HtmlInputCheckBox;

                 chk.Value = dr.ShiiresakiCode;
                // chkID
                if (HidChkID.Value != "") this.HidChkID.Value += ",";
                this.HidChkID.Value += chk.ClientID;
                // 主キー                
                if (HidThisID.Value != "") this.HidThisID.Value += ",";
                this.HidThisID.Value += chk.Value;
                // 更新ボタン
                HtmlInputButton btn = e.Row.FindControl("BtnK") as HtmlInputButton;
                btn.Attributes["onclick"] = string.Format("Update('{0}'); return false; ", chk.Value);

                // 仕入先コード
                e.Row.Cells[GV_CELL_SHIIRESAKI_CODE].Text = dr.ShiiresakiCode;
                // 仕入先名
                e.Row.Cells[GV_CELL_SHIIRESAKI_MEI].Text = dr.ShiiresakiMei.ToString();
                // 郵便番号
                e.Row.Cells[GV_CELL_YUBIN_BANGOU].Text = dr.YubinBangou;
                // 住所
                e.Row.Cells[GV_CELL_ADDRESS].Text = dr.Address.ToString();
                // 電話番号
                e.Row.Cells[GV_CELL_TEL].Text = dr.Tel;
                // FAX
                e.Row.Cells[GV_CELL_FAX].Text = dr.Fax;
                // 口座名義
                e.Row.Cells[GV_CELL_KOUZAMEIGI].Text = dr.KouzaMeigi;
                // 金融機関名
                e.Row.Cells[GV_CELL_KINYUUKIKAN_MEI].Text = dr.KinyuuKikanMei;
                // 口座番号
                e.Row.Cells[GV_CELL_KOUZABANGOU].Text = dr.KouzaBangou;
                // 支払締日
                e.Row.Cells[GV_CELL_SHIHARAI_SHIMEBI].Text = AppCommon.ShiharaiShimebi(dr.ShiharaiShimebi);
                // 支払予定日
                e.Row.Cells[GV_CELL_SHIHARAI_YOTEIBI].Text = AppCommon.ShiharaiYoteibi(dr.ShiharaiYoteibi);
                // 検収情報公開
                if (dr.KensyukoukaiFlg)
                {
                    e.Row.Cells[GV_CELL_KENSYUU_JYOUHOU_KOUKAI].Text = "○";
                }
                else
                {
                    e.Row.Cells[GV_CELL_KENSYUU_JYOUHOU_KOUKAI].Text = "";
                }
                // 納期回答催促メール
                if (dr.SaisokuMailFlg)
                {
                    e.Row.Cells[GV_CELL_NOUKIKAITOU_SAISOKU_MAIL].Text = "○";
                }
                else
                {
                    e.Row.Cells[GV_CELL_NOUKIKAITOU_SAISOKU_MAIL].Text = "";
                }
                // 仕入先情報更新許可
                if (dr.KousinKyokaFlg)
                {
                    e.Row.Cells[GV_CELL_SHIIRESAKI_KOUSHINKYOKA].Text = "○";
                }
                else
                {
                    e.Row.Cells[GV_CELL_SHIIRESAKI_KOUSHINKYOKA].Text = "";
                }
                // 適格請求書発行事業者
                if (dr.InvoiceRegFlg)
                {
                    e.Row.Cells[GV_CELL_INVOICEFLAG].Text = "○";
                }
                else
                {
                    e.Row.Cells[GV_CELL_INVOICEFLAG].Text = "";
                }
                e.Row.Cells[GV_CELL_INVOICENO].Text = dr.InvoiceRegNo;

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
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.T_Main);
               
            }
            else if (strCmd == "kensaku")
            {
                // 検索
                this.VsCurrentPageIndex = 0;
                this.Create();
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.T_Main);

               
            }
            else if (strCmd == "row")
            {
                // 行数変更
                this.Create();
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.T_Main);

               
            }
            else if (strCmd == "Reload")
            {
                
                this.Create();
                // Ddl担当者をセット
                this.DdlLoad();
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.T_Main);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DdlCode);

               
            }
            else if (strCmd == "delete")
            {
                // 削除
                string[] strDelKeyAry = strArgs[1].Split(',');
                for (int i = 0; i < strDelKeyAry.Length; i++)
                {
                    string[] strKey = strDelKeyAry[i].Split('_');
                    LibError err = ShiiresakiClass.M_Shiiresaki_Delete(strKey[0], Global.GetConnection());
                    if (err != null)
                    {
                        this.ShowMsg(err.Message, false);
                        return;
                    }
                }
                // Ddl担当者をセット
                this.DdlLoad();
                this.Create(); // 再表示               
                this.ShowMsg("削除しました", false);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.T_Main);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DdlCode);

               
            }
        }
    }
}


