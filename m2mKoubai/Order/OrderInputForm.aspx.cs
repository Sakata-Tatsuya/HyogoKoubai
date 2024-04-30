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

namespace m2mKoubai.Order
{
    public partial class OrderInputForm : Core.Web.ServerViewStatePage
    {
        private const int G_CELL_SAKUJO = 0;
        private const int G_CELL_HACCHU_NO_SHIIRE = 1;
        //private const int G_CELL_SHIIRE = 2;
        private const int G_CELL_BUHIN_KUBUN_MEI = 2;
        //private const int G_CELL_BUHIN_Mei = 4;
        private const int G_CELL_LOT_TANKA = 3;
        private const int G_CELL_SUURYOU = 4;
        private const int G_CELL_TANI = 5;
        private const int G_CELL_LT = 6;
        private const int G_CELL_NOUKI_BASHO = 7; 
        private const int G_CELL_BIKOU = 8;

        //private const int MAX_HACCHUU_ROW = 5;

        // 選択が変更された行No
        private int _RowNo = -1;

        // 入力してした注文情報
        private ChumonDataSet_S.V_OrderInputDataTable _dtOrder = null;
        // 仕入先
        private ShiiresakiDataSet_S.V_ShiiresakiDataTable _dtShiire = null;        
        // 部品区分
        private BuhinDataSet_S.V_BuhinKubunDataTable _dtKubun = null;
        // 部品目名
        //BuhinDataSet_S.V_BuhinCodeMeiDataTable _dtBuhinMei = null;
        // 納入場所
        private m2mKoubaiDataSet.M_NounyuuBashoDataTable _dtNounyuBasho = null;
        // 注文データ
        //private m2mKoubaiDataSet.T_ChumonDataTable _dtChumon = null;


        private int VsRowCnt
        {
            get
            {
                string str = Convert.ToString(this.ViewState["VsRowCnt"]);
                if (str == null || str == "")
                {
                    // 最初は五行
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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 増税対応(2019/10/1 10%)
                this.DdlTax.SelectedValue = "10";

                if (SessionManager.UserKubun != (byte)UserKubun.Owner)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                tab.Menu = CtlTabMain.MainMenu.Hacchu_Nyuuryoku;
                tab.Hacchu_NyuuryokuMenu = CtlTabMain.Hacchu_Nyuuryoku.Single;

                this.ShowTblMain(false);
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
            this.PreRender += new System.EventHandler(this.Form_PreRender);
        }

        private void Form_PreRender(object sender, EventArgs e)
        {
            // 行追加
            this.BtnAdd.Attributes["onclick"] = "AddRow(); return false;";
            // 登録
            this.BtnT.Attributes["onclick"] = "Touroku(); return false;";
            // 削除(input)
            this.BtnS.Attributes["onclick"] = "RowClear(); return false;";
            // クリア
            this.BtnClear.Attributes["onclick"] = "AllClear(); return false;";            
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
            BtnS.Visible = b;
        }

        private void Create()
        {
            // Hid_Clear
            this.HidChkID.Value = "";
            this.HidNoukiID.Value = "";

            // 最新の売上伝票番号取得
            int MaxHacchuuNo = ChumonClass_S.GetMaxHacchuuNo(Global.GetConnection());
            this.VsHacchuuNo = MaxHacchuuNo + 1;

            // 新しい受注番号を取得 G.Row作成時に使いたい
            //this.TbxJuchuNo2.Text = AppCommon.CreateNewChumonNo().ToString();

            this.ShowTblMain(true);
            

            // 仕入先データ(仕入先コード、仕入先名)を取得
            //this.dt = ShiiresakiClass.getV_ShiiresakiCodeDataTable(Global.GetConnection());

            //行追加ボタンを押したとき用に分岐を作る

            //if (_dtOrder == null)
            //    VsRowCnt = 5;

            G.DataSource = new int[VsRowCnt];


            G.DataBind();

            //VsRowCnt = G.Rows.Count;

            G.EnableViewState = false;
        }

        // 
        bool bSetShiireFlg = false;
        int nRowNo = 0;
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //BuhinDataSet.T_ChumonRow dr =
                //    ((DataRowView)e.Row.DataItem).Row as BuhinDataSet.T_ChumonRow;

                ChumonDataSet_S.V_OrderInputRow dr = null;
                if (_dtOrder != null && _dtOrder.Rows.Count > 0 && _dtOrder.Rows.Count > nRowNo)
                {
                    dr = _dtOrder.Rows[nRowNo] as ChumonDataSet_S.V_OrderInputRow;
                }

                // 削除
                HtmlInputCheckBox chk = e.Row.FindControl("ChkI") as HtmlInputCheckBox;
                //chk.Value = dr.Year + "_" + dr.HacchuuNo;
                if (HidChkID.Value != "") this.HidChkID.Value += ",";
                this.HidChkID.Value += chk.ClientID;                

                // 発注No
                //e.Row.Cells[G_CELL_HACCHUU_NO].Text = VsHacchuuNo.ToString();
                //VsHacchuuNo++;
                Label lblOrderNo = e.Row.Cells[G_CELL_HACCHU_NO_SHIIRE].FindControl("LblOrderNo") as Label;
                lblOrderNo.Text = VsHacchuuNo.ToString("0000000");
                VsHacchuuNo++;

                // 納期
                Telerik.WebControls.RadDatePicker rdp = 
                    e.Row.Cells[G_CELL_NOUKI_BASHO].FindControl("RdpNouki") as Telerik.WebControls.RadDatePicker;
                rdp.SharedCalendar = this.SC;
                //rdp.SelectedDate = "";
                if (HidNoukiID.Value != "") this.HidNoukiID.Value += ",";
                HidNoukiID.Value += rdp.ClientID + "_dateInput_text";

                // 納入場所
                if (_dtNounyuBasho == null)
                {
                    _dtNounyuBasho = NounyuuBashoClass.getM_NounyuuBashoDataTable(Global.GetConnection());
                }
                DropDownList ddlBasho = e.Row.Cells[G_CELL_NOUKI_BASHO].FindControl("DdlBasho") as DropDownList;
                ddlBasho.Items.Add("---");
                //bool bSelect = false;
                for (int i = 0; i < _dtNounyuBasho.Rows.Count; i++)
                {
                    ddlBasho.Items.Add(new ListItem(_dtNounyuBasho[i].BashoCode + ":" +_dtNounyuBasho[i].BashoMei, _dtNounyuBasho[i].BashoCode));
                    if (dr != null && dr.NounyuuBashoCode == _dtNounyuBasho[i].BashoCode)
                    {
                        ddlBasho.SelectedIndex = i + 1;
                    }
                }

                // 備考
                TextBox tbxBikou = e.Row.Cells[G_CELL_BIKOU].FindControl("TbxBikou") as TextBox;

                if (dr != null)
                {
                    tbxBikou.Text = dr.Bikou;

                    //tbxBikou.Attributes["overflow"] = "hidden";
                }

                //tbxBikou.Attributes.Add("style", "OVERFLOW: hidden;");

                // 仕入先
                DropDownList ddlShiire = e.Row.Cells[G_CELL_HACCHU_NO_SHIIRE].FindControl("DdlShiire") as DropDownList;
                if (!bSetShiireFlg)
                {                
                    if (_dtShiire == null)
                    {
                        // 仕入先を取得
                        _dtShiire = ShiiresakiClass.getV_ShiiresakiDataTable(Global.GetConnection());
                    }

                    ddlShiire.Attributes["onchange"] = "ShiireChange()";
                    LoadDdlShiire(ddlShiire);

                    //ddlShiire.Items.Add(new ListItem("---", ""));
                    bool bSelect = false;
                    for (int i = 0; i < _dtShiire.Rows.Count; i++)
                    {
                        //ddlShiire.Items.Add(new ListItem(_dtShiire[i].ShiiresakiCode + ":" + _dtShiire[i].ShiiresakiMei, _dtShiire[i].ShiiresakiCode));
                        if (dr != null && dr.ShiiresakiCode == _dtShiire[i].ShiiresakiCode)
                        {
                            ddlShiire.SelectedIndex = i + 1;
                            bSelect = true;
                        }
                    }
                    if (!bSelect)
                    {
                        // 選択なし(つまり、次の行はDdlShiireを作成する必要がない)
                        bSetShiireFlg = true;
                    }
                }
                else
                {
                    // 以下の項目は作成する必要が無いのでreturnする
                    nRowNo++;
                    return;
                }

                // 部品区分
                DropDownList ddlKubun = null;
                if (ddlShiire.SelectedIndex > 0)
                {
                    // 仕入先選択時
                                                    
                    // ListSet.SetDdlBuhinKubun(ddlKubun);
                    
                    /*
                    if (_dtKubun == null)
                    {
                        _dtKubun = BuhinClass_S.getV_BuhinKubunDataTable(Global.GetConnection());
                    }
                    */
                    // 区分取得
                    _dtKubun = BuhinClass_S.getV_BuhinKubunDataTable(ddlShiire.SelectedValue, Global.GetConnection());

                    ddlKubun = e.Row.Cells[G_CELL_BUHIN_KUBUN_MEI].FindControl("DdlKubun") as DropDownList;
                    //ddlKubun.Attributes["onchange"] = "KubunChange();";
                    ddlKubun.Attributes["OnSelectedIndexChanged"] = "KubunChange();";
                    ddlKubun.Items.Add("---");
                    //bool bSelect = false;
                    for (int i = 0; i < _dtKubun.Rows.Count; i++)
                    {
                        ddlKubun.Items.Add(_dtKubun[i].BuhinKubun);
                        if (dr != null && dr.BuhinKubun == _dtKubun[i].BuhinKubun)
                        {
                            ddlKubun.SelectedIndex = i + 1;
                        }
                    }
                }
                else
                {
                    // 仕入先の選択がない = ddl区分が""
                    nRowNo++;
                    return;
                }

                // 品目名
                if (ddlKubun.SelectedIndex > 0)
                {
                    // 区分が選択されている場合のみ
                    DropDownList ddlBuhinMei = e.Row.Cells[G_CELL_BUHIN_KUBUN_MEI].FindControl("DdlBuhin") as DropDownList;
                    ddlBuhinMei.Attributes["onchange"] = string.Format("BuhinChange('{0}');", nRowNo);
                    // 部品プルダウンの作成
                    ListSet.SetddlBuhin_KubunBetsu(ddlBuhinMei, ddlShiire.SelectedValue, ddlKubun.SelectedValue);
                    if (dr != null)
                    {
                        ddlBuhinMei.SelectedValue = dr.BuhinCode;

                        if (nRowNo == _RowNo && dr.BuhinCode != "")
                        {
                            // 選択変更した行の場合
                            // 品目データを取得、表示する
                            BuhinDataSet_S.V_BuhinInfoRow drBuhinInfo =
                                BuhinClass_S.getV_BuhinInfoRow(dr.BuhinCode, Global.GetConnection());
                            // ロット数
                            Label lblLot = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("LblLot") as Label;
                            if (drBuhinInfo.Lot != 0)
                                lblLot.Text = drBuhinInfo.Lot.ToString();
                            // 仮単価   追加 09/07/28
                            HtmlInputCheckBox chkKariTanka = e.Row.FindControl("ChkKariTanka") as HtmlInputCheckBox;
                            if (dr.KariTankaFlg == "0")
                            {
                                chkKariTanka.Checked = true;
                            }
                            else
                            {
                                chkKariTanka.Checked = false;
                            }

                            // 単価
                            TextBox tbxTanka = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("TbxTanka") as TextBox;
                            tbxTanka.Text = drBuhinInfo.Tanka.ToString("#,##0.00"); 

                            // 数量
                            TextBox tbxSuu = e.Row.Cells[G_CELL_SUURYOU].FindControl("TbxSuu") as TextBox;
                            tbxSuu.Text = dr.Suuryou;
                            // 単位
                            Label lblTani = e.Row.Cells[G_CELL_TANI].FindControl("LblTani") as Label;
                            lblTani.Text = drBuhinInfo.Tani;

                            if (drBuhinInfo.LT_Suuji != 0 && drBuhinInfo.LT_Tani != 0)
                            {
                                // リードタイム                                
                                Label lblLT = e.Row.Cells[G_CELL_TANI].FindControl("LblLT") as Label;
                                lblLT.Text = drBuhinInfo.LT_Suuji + AppCommon.LT_Tani(drBuhinInfo.LT_Tani);

                                // 納期
                                //int nAddDays = AppCommon.GetAddDays(drBuhinInfo.LT_Suuji, drBuhinInfo.LT_Tani);
                                rdp.SelectedDate = AppCommon.GetNouki(drBuhinInfo.LT_Suuji, drBuhinInfo.LT_Tani);
                            }
                        }
                        else if (dr.BuhinCode != "")
                        {
                            // ロット数
                            Label lblLot = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("LblLot") as Label;
                            lblLot.Text = dr.Lot;
                            // 仮単価
                            HtmlInputCheckBox chkKariTanka = e.Row.FindControl("ChkKariTanka") as HtmlInputCheckBox;
                            if (dr.KariTankaFlg == "0")
                            {
                                chkKariTanka.Checked = true;
                            }
                            else
                            {
                                chkKariTanka.Checked = false;
                            }
                            
                            // 単価
                            // Label lblTanka = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("LblTanka") as Label;
                            //TextBox tbxTanka = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("TextBox") as Label;
                            //tbxTanka.Text = dr.Tanka;

                            TextBox tbxTanka = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("TbxTanka") as TextBox;
                            tbxTanka.Text = dr.Tanka;
                           
                            // 数量
                            TextBox tbxSuu = e.Row.Cells[G_CELL_SUURYOU].FindControl("TbxSuu") as TextBox;
                            tbxSuu.Text = dr.Suuryou;                     
                            // 単位
                            Label lblTani = e.Row.Cells[G_CELL_TANI].FindControl("LblTani") as Label;
                            lblTani.Text = dr.Tani;

                            // リードタイム                                
                            Label lblLT = e.Row.Cells[G_CELL_TANI].FindControl("LblLT") as Label;
                            lblLT.Text = dr.LT;

                            // 納期
                            if (dr.Nouki.Length == 10)
                            {
                                string[] strAry = dr.Nouki.Split('/');
                                if (strAry.Length == 3)
                                {
                                    try
                                    {
                                        rdp.SelectedDate = new DateTime(int.Parse(strAry[0]), int.Parse(strAry[1]), int.Parse(strAry[2]));
                                    }
                                    catch { }
                                }
                            }
                            
                            /*
                            if (!drBuhinInfo.IsLT_SuujiNull() && !drBuhinInfo.IsLT_TaniNull())
                            {
                                // リードタイム                                
                                Label lblLT = e.Row.Cells[G_CELL_TANI].FindControl("LblLT") as Label;
                                lblLT.Text = drBuhinInfo.LT_Suuji + AppCommon.LT_Tani(drBuhinInfo.LT_Tani);

                                // 納期
                                //int nAddDays = AppCommon.GetAddDays(drBuhinInfo.LT_Suuji, drBuhinInfo.LT_Tani);
                                rdp.SelectedDate = AppCommon.GetNouki(drBuhinInfo.LT_Suuji, drBuhinInfo.LT_Tani);
                            }
                            */
                        }
                    }
                }
                    /*
                else
                {
                    // 区分の選択がない = ddl品目が""
                    nRowNo++;
                    return;
                }
                */

                /*
                // ロット数
                Label lblLot = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("LblLot") as Label;
                if (dr != null)
                {
                    lblLot.Text = dr.Lot;
                }
                // 単価
                Label lblTanka = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("LblTanka") as Label;
                if (dr != null)
                {
                    lblTanka.Text = dr.Tanka;
                }
                */
                


               
                /*
                // 単位
                Label lblTani = e.Row.Cells[G_CELL_TANI].FindControl("LblTani") as Label;
                if (dr != null)
                {
                    lblTani.Text = dr.Tanka;
                }
                */
                
                nRowNo++;
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                // 削除チェック
                HtmlInputCheckBox chkH = e.Row.Cells[G_CELL_SAKUJO].FindControl("ChkH") as HtmlInputCheckBox;
                chkH.Attributes["onclick"] = "DelChk(this.checked)";
            }
        }        

        protected void Ram_AjaxRequest(object sender, Telerik.WebControls.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];
            string strRowItemAry = string.Empty;

            if (strCmd != "Touroku")
            {
                // 発注ボタン
                if (this.BtnT.Disabled)
                {
                    this.BtnT.Disabled = false;
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.BtnT);
                }
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
                    this.Create();
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

                        this.BtnT.Disabled = true;
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
                    this.Create();
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
                    this.Create();
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
                    this.Create();
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
                    this.Create();
                    this.ShowMsg("", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblMain);
                    break;
                case "AllClear":
                    // 初期状態に戻す
                    _RowNo = 5;
                    this.Create();
                    this.ShowMsg("", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblMain);
                    break;
            }

        }

       
        //private MailClass.MailParam GetMailParam(ChumonDataSet.V_Chumon_MailRow dr)
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
        protected void DdlShiire_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            int nIndex = int.Parse(this.Request.Params["__EVENTARGUMENT"]);
            // 仕入先選択変更
            if (ddl.SelectedIndex > 0) 
            {
            }


        }
        private void LoadDdlShiire(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            // 仕入先を取得
            _dtShiire = ShiiresakiClass.getV_ShiiresakiDataTable(Global.GetConnection());

            ddl.Items.Add(new ListItem("---", ""));
            for (int i = 0; i<_dtShiire.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(_dtShiire[i].ShiiresakiCode + ":" + _dtShiire[i].ShiiresakiMei, _dtShiire[i].ShiiresakiCode));
            }
        }

    }
}
