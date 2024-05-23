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
using Telerik.Web.UI;
using m2mKoubaiDAL;
using System.Collections.Generic;
using static Telerik.Web.UI.OrgChartStyles;
//using Telerik.Web.UI.Skins;

namespace m2mKoubai.Order
{
    public partial class OrderInputForm : System.Web.UI.Page
    {
        private const int G_CELL_SAKUJO = 0;
        private const int G_CELL_HACCHU_NO_SHIIRE = 1;
        private const int G_CELL_BUHIN_KUBUN_MEI = 2;
        private const int G_CELL_LOT_TANKA = 3;
        private const int G_CELL_SUURYOU = 4;
        private const int G_CELL_TANI = 5;
        private const int G_CELL_LT = 6;
        private const int G_CELL_NOUKI_BASHO = 7; 
        private const int G_CELL_BIKOU = 8;

        // 選択が変更された行No
        private int _RowNo = -1;

        // 入力してした注文情報
        private ChumonDataSet_S.V_OrderInputDataTable _dtOrder = null;
        // 仕入先
        private ShiiresakiDataSet_S.V_ShiiresakiDataTable _dtShiire = null;        
        // 部品区分
        //private BuhinDataSet_S.V_BuhinKubunDataTable _dtKubun = null;
        // 納入場所
        //private m2mKoubaiDataSet.M_NounyuuBashoDataTable _dtNounyuBasho = null;

        private int VsRowCnt
        {
            get
            {
                string str = Convert.ToString(this.ViewState["VsRowCnt"]);
                if (str == null || str == "")
                {
                    // 最初は5行
                    return 5;
                }
                else
                {
                    return int.Parse(str);
                }
            }
            set
            {
                this.ViewState["VsRowCnt"] = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            List<ChumonClass.ChumonMeisai> lst = new List<ChumonClass.ChumonMeisai>();
            if (!IsPostBack)
            {
                lst = InitHacchu_Meisai(VsRowCnt);
                VsYaer = DateTime.Now.ToString("yy");
                VsUserID = SessionManager.LoginID;
                VsJigyoushoKubun = SessionManager.JigyoushoKubun;
                VsHacchuuNo = 0;
                // 増税対応
                if (DateTime.Today >= new DateTime(2019, 10, 1))
                {
                    this.DdlTax.SelectedValue = "10";
                }
                else
                {
                    this.DdlTax.SelectedValue = "8";
                }
                lst = InitHacchu_Meisai(VsRowCnt);

                if (SessionManager.UserKubun != (byte)UserKubun.Owner)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                tab.Menu = CtlTabMain.MainMenu.Hacchu_Nyuuryoku;
                tab.Hacchu_NyuuryokuMenu = CtlTabMain.Hacchu_Nyuuryoku.Single;

            }
            else
            {
                lst = GetMeisaiItems();
            }
            this.ShowTblMain(false);
            this.Create(lst);

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
            // 登録
            //this.BtnT.Attributes["onclick"] = "Touroku(); return false;";
            //Img
            this.Img1.Style.Add("display", "none");
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
        }

        private void Create(List<ChumonClass.ChumonMeisai> lst)
        {
            // Hid_Clear
            this.HidChkID.Value = "";
            this.HidNoukiID.Value = "";

            this.ShowTblMain(true);

            G.DataSource = lst;
            G.DataBind();
        }

        //private void Create()
        //{
        //    this.HidChkID.Value = "";
        //    this.HidNoukiID.Value = "";
        //    // 最新の売上伝票番号取得
        //    int MaxHacchuuNo = ChumonClass_S.GetMaxHacchuuNo(Global.GetConnection());
        //    this.VsHacchuuNo = MaxHacchuuNo + 1;

        //    this.ShowTblMain(true);
        //    G.DataSource = new int[VsRowCnt];
        //    G.DataBind();
        //    G.EnableViewState = false;
        //}

        // 
        bool bSetShiireFlg = false;
        int nRowNo = 0;
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ChumonClass.ChumonMeisai mi = e.Row.DataItem as ChumonClass.ChumonMeisai;
                HtmlInputCheckBox ChkI = e.Row.FindControl("ChkI") as HtmlInputCheckBox;
                Label LblHacchuuNo = e.Row.FindControl("LblHacchuuNo") as Label;
                RadComboBox RcbShiiresaki = e.Row.FindControl("RcbShiiresaki") as RadComboBox;
                DropDownList DdlBuhinKubun = e.Row.FindControl("DdlBuhinKubun") as DropDownList;
                RadComboBox RcbBuhin = e.Row.FindControl("RcbBuhin") as RadComboBox;
                Label LblLot = e.Row.FindControl("LblLot") as Label;
                HtmlInputCheckBox ChkKariTanka = e.Row.FindControl("ChkKariTanka") as HtmlInputCheckBox;
                TextBox TbxTanka = e.Row.FindControl("TbxTanka") as TextBox;
                TextBox TbxSuryo = e.Row.FindControl("TbxSuryo") as TextBox;
                Label LblTani = e.Row.FindControl("LblTani") as Label;
                Label LblLT = e.Row.FindControl("LblLT") as Label;
                RadDatePicker RdpNouki = e.Row.FindControl("RdpNouki") as RadDatePicker;
                DropDownList DdlBasho = e.Row.FindControl("DdlBasho") as DropDownList;
                TextBox TbxBikou = e.Row.FindControl("TbxBikou") as TextBox;
                DateTime dtTemp = DateTime.Now;
                decimal decTanka = 0;
                decimal decSuryo = 0;
                decimal decKingaku = 0;
                decimal decZeigaku = 0;
                decimal decZeiritu = 0;

                ChkI.Checked = mi.ChkI;
                if (HidChkID.Value != "") this.HidChkID.Value += ",";
                this.HidChkID.Value += ChkI.ClientID;

                // 発注No
                LblHacchuuNo.Text = mi.strHacchuuNo;

                // 仕入先
                if (mi.strShiiresakiCode != string.Empty)
                {
                    RcbShiiresaki.SelectedValue = mi.strShiiresakiCode;
                    RcbShiiresaki.Text = mi.strShiiresakiItem;
                }
                if (mi.strBuhinKubun != string.Empty)
                {
                    DdlBuhinKubun.SelectedValue = mi.strBuhinKubun;
                    DdlBuhinKubun.Text = mi.strBuhinKubun;
                }
                LblTani.Text = mi.strTani;
                LblLT.Text = mi.strLT;
                if (mi.strBuhinCode != string.Empty)
                {
                    RcbBuhin.SelectedValue = mi.strBuhinCode;
                    RcbBuhin.Text = mi.strBuhinItem;
                    m2mKoubaiDataSet.M_BuhinRow drM = BuhinClass.getM_BuhinRow(mi.strBuhinCode, Global.GetConnection());
                    if (drM != null)
                    {
                        DdlBuhinKubun.SelectedValue = drM.BuhinKubun;
                        DdlBuhinKubun.Text = drM.BuhinKubun;
                        LblLot.Text = drM.Lot.ToString("#,###");
                        LblTani.Text = drM.Tani;
                        LblLT.Text = drM.LT_Suuji.ToString("#") + drM.LT_Tani;
                    }
                }
                LblLot.Text = mi.strLot;
                ChkKariTanka.Checked = mi.ChkKaritankaFlg;
                decimal.TryParse(mi.strTanka.Replace(",", ""), out decTanka);
                decimal.TryParse(mi.strSuryo.Replace(",", ""), out decSuryo);
                decimal.TryParse(mi.strZeiritu.Replace(",", ""), out decZeiritu);
                decKingaku = Math.Floor(decTanka * decSuryo);
                decZeigaku = Math.Floor(decKingaku * decZeiritu / 100);
                if (decTanka > 0 && decSuryo > 0)
                {
                    TbxTanka.Text = decTanka.ToString("#,##0.###");
                    TbxSuryo.Text = decSuryo.ToString("#,##0.###");
                }
                else
                {
                    TbxTanka.Text = decTanka.ToString("#,###.###");
                    TbxSuryo.Text = decSuryo.ToString("#,###.###");
                }

                // 納期
                if (mi.strNouki != string.Empty)
                {
                    DateTime.TryParse(mi.strNouki, out dtTemp);
                    if (dtTemp > RdpNouki.MinDate && dtTemp < RdpNouki.MaxDate)
                    {
                        RdpNouki.SelectedDate = dtTemp;
                    }
                }
                if (HidNoukiID.Value != "") this.HidNoukiID.Value += ",";
                HidNoukiID.Value += RdpNouki.ClientID + "_dateInput";
                LoadDdlBasho(mi.strNounyuuBashoCode, DdlBasho);
                TbxBikou.Text = mi.strBikou;
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                // 削除チェック
                HtmlInputCheckBox chkH = e.Row.FindControl("ChkH") as HtmlInputCheckBox;
                chkH.Attributes["onclick"] = "DelChk(this.checked)";
            }
        }

        private List<ChumonClass.ChumonMeisai> InitHacchu_Meisai(int RowNum)
        {
            List<ChumonClass.ChumonMeisai> lst = new List<ChumonClass.ChumonMeisai>();
            VsZeiritu = DdlTax.SelectedValue;

            // 最新の発注番号取得
            int MaxHacchuuNo = ChumonClass_S.GetMaxHacchuuNo(Global.GetConnection());
            VsHacchuuNo = MaxHacchuuNo + 1;
            for (int i = 0; i < RowNum; i++)
            {
                ChumonClass.ChumonMeisai m = new ChumonClass.ChumonMeisai();
                m.ChkI = false;
                m.strYear = VsYaer;
                m.strHacchuuNo = VsHacchuuNo.ToString("0000000");
                m.strBuhinKubun = String.Empty;
                m.strBuhinCode = String.Empty;
                m.strBuhinItem = String.Empty;
                m.strTanka = String.Empty;
                m.strSuryo = String.Empty;
                m.strKingaku = String.Empty;
                m.strZeiritu = "10";
                m.strNouki = String.Empty;
                m.strNounyuuBashoCode = String.Empty;
                m.strBikou = String.Empty;
                m.ChkKaritankaFlg = false;
                m.strShiiresakiCode = String.Empty;
                m.strShiiresakiItem = String.Empty;
                lst.Add(m);

                VsHacchuuNo++;
            }
            return lst;
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            List<ChumonClass.ChumonMeisai> lst = GetMeisaiItems();

            int MaxHacchuuNo = ChumonClass_S.GetMaxHacchuuNo(Global.GetConnection());
            VsHacchuuNo = MaxHacchuuNo + 1;

            for (int i = 0; i < lst.Count; i++)
            {
                lst[i].strHacchuuNo = VsHacchuuNo.ToString("0000000");
                VsHacchuuNo++;
            }

            ChumonClass.ChumonMeisai mi = new ChumonClass.ChumonMeisai();
            mi.ChkI = false;
            mi.strYear = VsYaer;
            mi.strHacchuuNo = VsHacchuuNo.ToString("0000000");
            mi.strBuhinKubun = String.Empty;
            mi.strBuhinCode = String.Empty;
            mi.strBuhinItem = String.Empty;
            mi.strTanka = String.Empty;
            mi.strSuryo = String.Empty;
            mi.strKingaku = String.Empty;
            mi.strZeiritu = "10";
            mi.strNouki = String.Empty;
            mi.strNounyuuBashoCode = String.Empty;
            mi.strBikou = String.Empty;
            mi.ChkKaritankaFlg = false;
            mi.strShiiresakiCode = String.Empty;
            mi.strShiiresakiItem = String.Empty;
            lst.Add(mi);

            Create(lst);
        }
        protected void BtnDel_Click(object sender, EventArgs e)
        {
            List<ChumonClass.ChumonMeisai> lst = GetMeisaiItems();
            int MaxHacchuuNo = ChumonClass_S.GetMaxHacchuuNo(Global.GetConnection());
            VsHacchuuNo = MaxHacchuuNo + 1;

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].ChkI == true)
                {
                    ChumonClass.ChumonMeisai mi = lst[i];
                    lst.Remove(mi);
                }
                else
                {
                    lst[i].strHacchuuNo = VsHacchuuNo.ToString("0000000");
                    VsHacchuuNo++;
                }
            }

            Create(lst);

        }
        protected void BtnClear_Click(object sender, EventArgs e)
        {
            List<ChumonClass.ChumonMeisai> lst = InitHacchu_Meisai(VsRowCnt);

            Create(lst);

        }
        private List<ChumonClass.ChumonMeisai> GetMeisaiItems()
        {
            List<ChumonClass.ChumonMeisai> lst = new List<ChumonClass.ChumonMeisai>();

            VsZeiritu = DdlTax.SelectedValue;

            int i = 0;
            try
            {
                for (i = 0; i < G.Rows.Count; i++)
                {
                    ChumonClass.ChumonMeisai m = this.GetMeisaiItem(i);

                    lst.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0}行目", i + 1) + ":" + ex.Message);
            }

            return lst;
        }

        private ChumonClass.ChumonMeisai GetMeisaiItem(int nItemIndex)
        {
            decimal decTanka = 0;
            decimal decSuryo = 0;
            decimal decKingaku = 0;
            DateTime dtTemp = DateTime.Now;
            GridViewRow item = G.Rows[nItemIndex];
            ChumonClass.ChumonMeisai m = new ChumonClass.ChumonMeisai();

            HtmlInputCheckBox ChkI = item.FindControl("ChkI") as HtmlInputCheckBox;
            Label LblHacchuuNo = item.FindControl("LblHacchuuNo") as Label;
            //DropDownList DdlShiire = item.FindControl("DdlShiire") as DropDownList;
            RadComboBox RcbShiiresaki = item.FindControl("RcbShiiresaki") as RadComboBox;
            DropDownList DdlBuhinKubun = item.FindControl("DdlBuhinKubun") as DropDownList;
            //DropDownList DdlBuhin = item.FindControl("DdlBuhin") as DropDownList;
            RadComboBox RcbBuhin = item.FindControl("RcbBuhin") as RadComboBox;
            Label LblLot = item.FindControl("LblLot") as Label;
            HtmlInputCheckBox ChkKariTanka = item.FindControl("ChkKariTanka") as HtmlInputCheckBox;
            TextBox TbxTanka = item.FindControl("TbxTanka") as TextBox;
            TextBox TbxSuryo = item.FindControl("TbxSuryo") as TextBox;
            Label LblTani = item.FindControl("LblTani") as Label;
            Label LblLT = item.FindControl("LblLT") as Label;
            RadDatePicker RdpNouki = item.FindControl("RdpNouki") as RadDatePicker;
            DropDownList DdlBasho = item.FindControl("DdlBasho") as DropDownList;
            TextBox TbxBikou = item.FindControl("TbxBikou") as TextBox;

            m.ChkI = ChkI.Checked;
            m.strYear = VsYaer;
            m.strHacchuuNo = LblHacchuuNo.Text;
            m.strShiiresakiCode = RcbShiiresaki.SelectedValue ?? string.Empty;
            m.strShiiresakiItem = RcbShiiresaki.Text ?? string.Empty;
            m.strBuhinKubun = DdlBuhinKubun.Text ?? string.Empty;
            m.strBuhinCode = RcbBuhin.SelectedValue ?? string.Empty;
            m.strBuhinItem = RcbBuhin.Text ?? string.Empty;
            m.strLot = LblLot.Text ?? string.Empty;
            m.ChkKaritankaFlg = ChkKariTanka.Checked;
            m.strTanka = TbxTanka.Text;
            m.strSuryo = TbxSuryo.Text;
            decimal.TryParse(TbxTanka.Text, out decTanka);
            decimal.TryParse(TbxSuryo.Text, out decSuryo);
            decKingaku = decTanka * decSuryo;
            m.strKingaku = decKingaku.ToString("0");
            m.strZeiritu = VsZeiritu;
            m.strTani=LblTani.Text;
            m.strLT=LblLT.Text;
            DateTime.TryParse(RdpNouki.SelectedDate.ToString(), out dtTemp);
            if (dtTemp > RdpNouki.MinDate && dtTemp < RdpNouki.MaxDate)
            { m.strNouki = dtTemp.ToString("yyyy/MM/dd"); }
            else
            { m.strNouki = string.Empty; }
            m.strNounyuuBashoCode = DdlBasho.SelectedValue;
            m.strBikou = TbxBikou.Text;

            return m;
        }
        private void LoadDdlBasho(string strKey, DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", ""));
            m2mKoubaiDataSet.M_NounyuuBashoDataTable dt = NounyuuBashoClass.getM_NounyuuBashoDataTable(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].BashoCode + ":" + dt[i].BashoMei, dt[i].BashoCode));
                if (strKey == dt[i].BashoCode)
                {
                    ddl.SelectedIndex = i + 1;
                }
            }
        }
        private void LoadDdlBuhinKubun(string strKey, DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", ""));
            BuhinDataSet_S.V_BuhinKubunDataTable dt = BuhinClass_S.getV_BuhinKubunDataTable(strKey,Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt[i].BuhinKubun, dt[i].BuhinKubun));
            }
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];
            string strRowItemAry = string.Empty;

            if (strCmd != "Touroku")
            {
                // 発注完了ラベル
                this.LblOK.Text = "";
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblOK);
            }

            switch (strCmd)
            {
                case "RowClear":
                    // 選択された行の全ての項目を未選択状態にする
                    string strErrMsg = "データの削除に失敗しました";
                    if (strArgs[1] != "")
                    {
                        strRowItemAry = strArgs[1];
                        if (!this.SetOrderData(strRowItemAry))
                        {
                            this.ShowMsg(strErrMsg, true);
                            this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                            return;
                        }
                    }
                    // 削除する行数
                    int nDelRowCnt = int.Parse(this.HidArgs.Value);
                    VsRowCnt -= nDelRowCnt;
                    if (VsRowCnt == 0)
                    {
                        VsRowCnt = 1;
                    }
                    //this.Create();
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblMain);
                    break;
                case "Touroku":
                    // 発注登録
                    strRowItemAry = strArgs[1];
                    if (strArgs[1] != "")
                    {
                        if (!this.SetOrderData(strRowItemAry))
                        {
                            this.ShowMsg("エラー", true);
                            this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                            return;
                        }
                    }
                    LibError err = ChumonClass_S.T_Chumon_Insert(_dtOrder, SessionManager.LoginID, SessionManager.JigyoushoKubun, Convert.ToInt32(DdlTax.SelectedValue), Global.GetConnection());
                    if (err != null)
                    {
                        this.ShowMsg("発注に失敗しました<br/>" + err.Message, true);
                        //return;
                    }
                    else
                    {
                        // 仕入先配列を作成
                        ArrayList aryShiire = new ArrayList();
                        for (int i = 0; i < _dtOrder.Rows.Count; i++)
                        {
                            if (!aryShiire.Contains(_dtOrder[i].ShiiresakiCode))
                            {
                                aryShiire.Add(_dtOrder[i].ShiiresakiCode);
                            }
                        }

                        for (int i = 0; i < aryShiire.Count; i++)
                        {
                            // 主キーによって、メール送信に必要データ取得                       
                            ChumonDataSet.V_MailInfoDataTable dtMail =
                                ChumonClass.getV_MailInfoDataTable(SessionManager.LoginID, aryShiire[i].ToString(), Global.GetConnection());
                            // メール送る回数
                            for (int j = 0; j < dtMail.Rows.Count; j++)
                            {
                                MailClass.MailParam p = this.GetMailParam(dtMail[j]);

                                MailClass.SendMail(p, null);
                            }
                        }
                        this.ShowMsg("発注が完了しました", false);

                        // 発注完了ラベル
                        this.LblOK.Text = "上記の内容で発注が完了しました";
                        this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblOK);

                        //this.BtnT.Disabled = true;
                        this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.BtnT);
                    }
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                    break;
                case "KubunChange":
                    // 区分選択変更         
                    strRowItemAry = strArgs[1];
                    if (strArgs[1] != "")
                    {
                        if (!this.SetOrderData(strRowItemAry))
                        {
                            this.ShowMsg("エラー", true);
                            this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                            return;
                        }
                    }
                    //this.Create();
                    this.ShowMsg("", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblMain);
                    break;
                case "AddRow":
                    // 行追加
                    strRowItemAry = strArgs[1];
                    if (strArgs[1] != "")
                    {
                        if (!this.SetOrderData(strRowItemAry))
                        {
                            this.ShowMsg("エラー", true);
                            this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                            return;
                        }
                    }
                    VsRowCnt++;
                    //this.Create();
                    this.ShowMsg("", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblMain);
                    break;
                case "ShiireChange":
                    // 仕入先選択変更
                    strRowItemAry = strArgs[1];//RowNo
                    //if (strArgs[1] != "")
                    //{
                    //    if (!this.SetOrderData(strRowItemAry))
                    //    {
                    //        this.ShowMsg("エラー", true);
                    //        this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                    //        return;
                    //    }
                    //}
                    //this.Create();
                    this.ShowMsg("", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblMain);

                    break;
                case "BuhinChange":
                    // 仕入先選択変更
                    strRowItemAry = strArgs[1];
                    if (strArgs[1] != "")
                    {
                        if (!this.SetOrderData(strRowItemAry))
                        {
                            this.ShowMsg("エラー", true);
                            this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                            return;
                        }
                    }
                    // 選択変更した行No
                    _RowNo = int.Parse(this.HidArgs.Value);
                    //this.Create();
                    this.ShowMsg("", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblMain);
                    break;
                case "AllClear":
                    // 初期状態に戻す
                    _RowNo = 5;
                    //this.Create();
                    this.ShowMsg("", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblMain);
                    break;
            }

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
        private bool SetOrderData(string strDataAry)
        {

            // chkKan.value + '|' + shiireCode + '|' + buhinKubun + '|' + buhinCode + '|' + suu + '|' + tani + '|' + nouki + '|' + basho + '|' + bikou;
            _dtOrder = new ChumonDataSet_S.V_OrderInputDataTable();
            string [] strRowAry = strDataAry.Split('\t');
            for (int i = 0; i < strRowAry.Length; i++)
            {
                string[] strItemAry = strRowAry[i].Split('|');
                if (strItemAry.Length != _dtOrder.Columns.Count)
                {      
                    // 列数チェック
                    return false;
                }
                ChumonDataSet_S.V_OrderInputRow dr = _dtOrder.NewV_OrderInputRow();
                // 注文のキー
                //dr.Year = strItemAry[0].Split('_')[0];
                //dr.Year = DateTime.Now.ToString("yy");
                //dr.HacchuuNo = strItemAry[0].Split('_')[1];                
                // 仕入先コード
                dr.ShiiresakiCode = strItemAry[0];
                // 部品区分
                dr.BuhinKubun = strItemAry[1];
                // 部品コード
                dr.BuhinCode = strItemAry[2];
                // ロット
                dr.Lot = strItemAry[3];

                // 単価                
                dr.Tanka = strItemAry[4];
                // 数量
                dr.Suuryou = strItemAry[5];
                // 単位
                dr.Tani = strItemAry[6];
                // リードタイム
                dr.LT = strItemAry[7];
                // 納期
                dr.Nouki = strItemAry[8];
                // 納入場所コード
                dr.NounyuuBashoCode = strItemAry[9];
                // 備考
                dr.Bikou = strItemAry[10];
                // 仮単価   追加 09/07/28
                dr.KariTankaFlg = strItemAry[11];


                _dtOrder.AddV_OrderInputRow(dr);
                
            }
            
            return true;
        }
        private string ChkMeisai(List<ChumonClass.ChumonMeisai> lst)
        {
            string strErr = string.Empty;

            decimal decTanka = 0;
            decimal decSuryo = 0;
            decimal decKingaku = 0;
            int intTemp = 0;
            DateTime dtNow = DateTime.Now;

            for (int i = 0; i < lst.Count; i++)
            {
                ChumonClass.ChumonMeisai mi = lst[i];

                if (mi.strBuhinCode.Length == 0 && mi.strShiiresakiCode.Length == 0)
                    continue;

                decTanka = decSuryo = decKingaku = 0;
                decimal.TryParse(mi.strTanka, out decTanka);
                decimal.TryParse(mi.strSuryo, out decSuryo);
                decKingaku = decTanka * decSuryo;
                intTemp = 0;
                int.TryParse(mi.strZeiritu, out intTemp);

                if (mi.strBuhinCode.Length < 1)
                {
                    strErr += string.Format("{0}行目 品目を選択して下さい", i + 1);
                }
                if (mi.strShiiresakiCode.Length < 1)
                {
                    strErr += string.Format("{0}行目 仕入先を選択して下さい", i + 1);
                }
                if (decTanka < 0)
                {
                    strErr += string.Format("{0}行目 単価を0以上で入力して下さい", i + 1);
                }
                if (decSuryo < 0)
                {
                    strErr += string.Format("{0}行目 数量を0以上で入力して下さい", i + 1);
                }
                if (mi.strNouki.Length < 8)
                {
                    strErr += string.Format("{0}行目 納期を入力して下さい", i + 1);
                }
                if (mi.strNounyuuBashoCode.Length < 1)
                {
                    strErr += string.Format("{0}行目 納品場所を選択して下さい", i + 1);
                }
                if (mi.strBikou.Length > 200)
                {
                    strErr += string.Format("{0}行目 備考は200文字以内で入力して下さい", i + 1);
                }

            }

            return strErr;
        }

        public List<ChumonClass.ChumonMeisai> Save()
        {
            List<ChumonClass.ChumonMeisai> lstS = GetMeisaiItems();
            List<ChumonClass.ChumonMeisai> lst = new List<ChumonClass.ChumonMeisai>();
            int MaxHacchuuNo = ChumonClass_S.GetMaxHacchuuNo(Global.GetConnection());
            VsHacchuuNo = MaxHacchuuNo + 1;

            for (int i = 0; i < lstS.Count; i++)
            {
                if (lstS[i].strBuhinCode.Length == 0 && lstS[i].strShiiresakiCode.Length == 0)
                {
                    ChumonClass.ChumonMeisai md = lstS[i];
                }
                else
                {
                    ChumonClass.ChumonMeisai m = lstS[i];
                    m.strHacchuuNo = VsHacchuuNo.ToString("0000000");
                    VsHacchuuNo++;
                    lst.Add(m);
                }
            }

            if (lst.Count < 1)
            {
                this.ShowMsg("発注データがありません", true);
                return lstS;
            }

            m2mKoubaiDataSet.T_ChumonDataTable dt = new m2mKoubaiDataSet.T_ChumonDataTable();

            decimal decTanka = 0;
            decimal decSuryo = 0;
            decimal decKingaku = 0;
            int intTemp = 0;
            DateTime dtNow = DateTime.Now;
            DateTime dtTemp = DateTime.Now;

            for (int i = 0; i < lst.Count; i++)
            {
                m2mKoubaiDataSet.T_ChumonRow dr = dt.NewT_ChumonRow();
                ChumonClass.ChumonMeisai mi = lst[i];

                dr.Year = mi.strYear;
                dr.HacchuuNo = mi.strHacchuuNo;
                dr.JigyoushoKubun = VsJigyoushoKubun;
                dr.ShiiresakiCode = mi.strShiiresakiCode;
                dr.BuhinKubun = mi.strBuhinKubun;
                dr.BuhinCode = mi.strBuhinCode;
                decTanka = decSuryo = decKingaku = 0;
                decimal.TryParse(mi.strTanka.Replace(",", ""), out decTanka);
                decimal.TryParse(mi.strSuryo.Replace(",", ""), out decSuryo);
                decKingaku = decTanka * decSuryo;
                dr.Tanka = decTanka;
                dr.Suuryou = (int)decSuryo;
                dr.Kingaku = decKingaku;
                intTemp = 0;
                int.TryParse(mi.strZeiritu, out intTemp);
                dr.Zeiritu = intTemp;
                dtTemp = DateTime.Now;
                DateTime.TryParse(mi.strNouki, out dtTemp);
                dr.Nouki = dtTemp.ToString("yyyyMMdd");
                dr.NounyuuBashoCode = mi.strNounyuuBashoCode;
                dr.Bikou = mi.strBikou;
                dr.HacchuuBi = dtNow;
                dr.HacchushaID = VsUserID;
                dr.KannouFlg = false;
                dr.KaritankaFlg = false;

                dt.AddT_ChumonRow(dr);
            }

            LibError err = ChumonClass_S.T_Chumon_Insert(dt, Global.GetConnection());
            if (err != null)
            {
                this.ShowMsg("発注に失敗しました<br/>" + err.Message, true);
            }

            return lst;
        }
        protected void BtnT_Click(object sender, EventArgs e)
        {
            ShowMsg("", false);
            this.LblOK.Text = "";
            this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblOK);

            List<ChumonClass.ChumonMeisai> lst = GetMeisaiItems();
            string strErr = ChkMeisai(lst);
            if (strErr.Length > 0)
            {
                ShowMsg(strErr, true);
                return;
            }
            else
            {
                lst = Save();
            }
            this.LblOK.Text = "上記の内容で発注が完了しました";
            this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblOK);

            Create(lst);

        }
        protected void RcbShiiresaki_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = o as RadComboBox;

            rcb.Items.Clear();
            string strText = e.Text.Trim();

            rcb.Height = Unit.Pixel(180);

            int itemOffset = e.NumberOfItems;
            int endOffset = itemOffset + 20;

            int nTotal = 0;
            ShiiresakiDataSet_S.V_ShiiresakiDataTable dt = ShiiresakiClass.getV_ShiiresakiDataTable(Global.GetConnection());
            for (int i = 0; i < dt.Count; i++)
            {
                rcb.Items.Add(new RadComboBoxItem(dt[i].ShiiresakiCode + ":" + dt[i].ShiiresakiMei, dt[i].ShiiresakiCode));
            }

            //ShiiresakiDataSet.M_ShiiresakiDataTable dt = new ShiiresakiDataSet.M_ShiiresakiDataTable();

            //try
            //{
            //    ShiiresakiClass.getM_ShiiresakiDataTable(strText, false, itemOffset, 20, Global.GetConnection(), out dt, ref nTotal);
            //}
            //catch (Exception ex)
            //{
            //    e.Message = ex.Message;
            //    return;
            //}
            //for (int i = 0; i < dt.Count; i++)
            //{
            //    rcb.Items.Add(new RadComboBoxItem(dt[i].ShiiresakiCode + ":" + dt[i].ShiiresakiMei, dt[i].ShiiresakiCode));
            //}

            //e.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>",
            //    endOffset.ToString(), nTotal.ToString());
        }
        protected void RcbBuhin_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = o as RadComboBox;
            GridViewRow thisGridViewRow = (GridViewRow)rcb.Parent.Parent;
            int nIndex = thisGridViewRow.RowIndex;
            RadComboBox RcbShiiresaki = G.Rows[nIndex].FindControl("RcbShiiresaki") as RadComboBox;
            string strShiiresaki = RcbShiiresaki.SelectedValue.ToString();
            DropDownList DdlBuhinKubun = G.Rows[nIndex].FindControl("DdlBuhinKubun") as DropDownList;
            string strBuhinKubun = DdlBuhinKubun.SelectedValue.ToString();

            rcb.Items.Clear();
            string strText = e.Text.Trim();

            rcb.Height = Unit.Pixel(180);

            int itemOffset = e.NumberOfItems;
            int endOffset = itemOffset + 20;

            int nTotal = 0;
            m2mKoubaiDataSet.M_BuhinDataTable dt = null;

            try
            {
                BuhinClass.getM_BuhinDataTable(strText, strShiiresaki, strBuhinKubun, false, itemOffset, 20, Global.GetConnection(), out dt, ref nTotal);
            }
            catch (Exception ex)
            {
                e.Message = ex.Message;
                return;
            }

            for (int i = 0; i < dt.Count; i++)
            {
                rcb.Items.Add(new RadComboBoxItem(dt[i].BuhinCode + ":" + dt[i].BuhinMei, dt[i].BuhinCode));
            }

            e.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>",
                endOffset.ToString(), nTotal.ToString());
        }

        protected void RcbBuhin_SelectionChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox rcb = sender as RadComboBox;
            GridViewRow thisGridViewRow = (GridViewRow)rcb.Parent.Parent;
            int nIndex = thisGridViewRow.RowIndex;
            Label LblLot = G.Rows[nIndex].FindControl("LblLot") as Label;
            Label LblTani = G.Rows[nIndex].FindControl("LblTani") as Label;
            Label LblLT = G.Rows[nIndex].FindControl("LblLT") as Label;

            string strText = rcb.SelectedValue;

            m2mKoubaiDataSet.M_BuhinRow dr = BuhinClass.getM_BuhinRow(strText, Global.GetConnection());
            if(dr != null) 
            {
                LblLot.Text = dr.Lot.ToString("#,###");
                LblTani.Text = dr.Tani;
                LblLT.Text = dr.LT_Suuji.ToString("#") + dr.LT_Tani;
            }
        }









    }
}
