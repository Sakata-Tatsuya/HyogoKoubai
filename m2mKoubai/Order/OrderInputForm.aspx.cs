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

        // �I�����ύX���ꂽ�sNo
        private int _RowNo = -1;

        // ���͂��Ă����������
        private ChumonDataSet_S.V_OrderInputDataTable _dtOrder = null;
        // �d����
        private ShiiresakiDataSet_S.V_ShiiresakiDataTable _dtShiire = null;        
        // ���i�敪
        private BuhinDataSet_S.V_BuhinKubunDataTable _dtKubun = null;
        // ���i�ږ�
        //BuhinDataSet_S.V_BuhinCodeMeiDataTable _dtBuhinMei = null;
        // �[���ꏊ
        private m2mKoubaiDataSet.M_NounyuuBashoDataTable _dtNounyuBasho = null;
        // �����f�[�^
        //private m2mKoubaiDataSet.T_ChumonDataTable _dtChumon = null;


        private int VsRowCnt
        {
            get
            {
                string str = Convert.ToString(this.ViewState["VsRowCnt"]);
                if (str == null || str == "")
                {
                    // �ŏ��͌܍s
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
                // ���őΉ�(2019/10/1 10%)
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
            // �s�ǉ�
            this.BtnAdd.Attributes["onclick"] = "AddRow(); return false;";
            // �o�^
            this.BtnT.Attributes["onclick"] = "Touroku(); return false;";
            // �폜(input)
            this.BtnS.Attributes["onclick"] = "RowClear(); return false;";
            // �N���A
            this.BtnClear.Attributes["onclick"] = "AllClear(); return false;";            
            //Img
            this.Img1.Style.Add("display", "none");
        }

        // ���b�Z�[�W�\��
        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }

        // GridView�\��
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

            // �ŐV�̔���`�[�ԍ��擾
            int MaxHacchuuNo = ChumonClass_S.GetMaxHacchuuNo(Global.GetConnection());
            this.VsHacchuuNo = MaxHacchuuNo + 1;

            // �V�����󒍔ԍ����擾 G.Row�쐬���Ɏg������
            //this.TbxJuchuNo2.Text = AppCommon.CreateNewChumonNo().ToString();

            this.ShowTblMain(true);
            

            // �d����f�[�^(�d����R�[�h�A�d���於)���擾
            //this.dt = ShiiresakiClass.getV_ShiiresakiCodeDataTable(Global.GetConnection());

            //�s�ǉ��{�^�����������Ƃ��p�ɕ�������

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

                // �폜
                HtmlInputCheckBox chk = e.Row.FindControl("ChkI") as HtmlInputCheckBox;
                //chk.Value = dr.Year + "_" + dr.HacchuuNo;
                if (HidChkID.Value != "") this.HidChkID.Value += ",";
                this.HidChkID.Value += chk.ClientID;                

                // ����No
                //e.Row.Cells[G_CELL_HACCHUU_NO].Text = VsHacchuuNo.ToString();
                //VsHacchuuNo++;
                Label lblOrderNo = e.Row.Cells[G_CELL_HACCHU_NO_SHIIRE].FindControl("LblOrderNo") as Label;
                lblOrderNo.Text = VsHacchuuNo.ToString("0000000");
                VsHacchuuNo++;

                // �[��
                Telerik.WebControls.RadDatePicker rdp = 
                    e.Row.Cells[G_CELL_NOUKI_BASHO].FindControl("RdpNouki") as Telerik.WebControls.RadDatePicker;
                rdp.SharedCalendar = this.SC;
                //rdp.SelectedDate = "";
                if (HidNoukiID.Value != "") this.HidNoukiID.Value += ",";
                HidNoukiID.Value += rdp.ClientID + "_dateInput_text";

                // �[���ꏊ
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

                // ���l
                TextBox tbxBikou = e.Row.Cells[G_CELL_BIKOU].FindControl("TbxBikou") as TextBox;

                if (dr != null)
                {
                    tbxBikou.Text = dr.Bikou;

                    //tbxBikou.Attributes["overflow"] = "hidden";
                }

                //tbxBikou.Attributes.Add("style", "OVERFLOW: hidden;");

                // �d����
                DropDownList ddlShiire = e.Row.Cells[G_CELL_HACCHU_NO_SHIIRE].FindControl("DdlShiire") as DropDownList;
                if (!bSetShiireFlg)
                {                
                    if (_dtShiire == null)
                    {
                        // �d������擾
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
                        // �I���Ȃ�(�܂�A���̍s��DdlShiire���쐬����K�v���Ȃ�)
                        bSetShiireFlg = true;
                    }
                }
                else
                {
                    // �ȉ��̍��ڂ͍쐬����K�v�������̂�return����
                    nRowNo++;
                    return;
                }

                // ���i�敪
                DropDownList ddlKubun = null;
                if (ddlShiire.SelectedIndex > 0)
                {
                    // �d����I����
                                                    
                    // ListSet.SetDdlBuhinKubun(ddlKubun);
                    
                    /*
                    if (_dtKubun == null)
                    {
                        _dtKubun = BuhinClass_S.getV_BuhinKubunDataTable(Global.GetConnection());
                    }
                    */
                    // �敪�擾
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
                    // �d����̑I�����Ȃ� = ddl�敪��""
                    nRowNo++;
                    return;
                }

                // �i�ږ�
                if (ddlKubun.SelectedIndex > 0)
                {
                    // �敪���I������Ă���ꍇ�̂�
                    DropDownList ddlBuhinMei = e.Row.Cells[G_CELL_BUHIN_KUBUN_MEI].FindControl("DdlBuhin") as DropDownList;
                    ddlBuhinMei.Attributes["onchange"] = string.Format("BuhinChange('{0}');", nRowNo);
                    // ���i�v���_�E���̍쐬
                    ListSet.SetddlBuhin_KubunBetsu(ddlBuhinMei, ddlShiire.SelectedValue, ddlKubun.SelectedValue);
                    if (dr != null)
                    {
                        ddlBuhinMei.SelectedValue = dr.BuhinCode;

                        if (nRowNo == _RowNo && dr.BuhinCode != "")
                        {
                            // �I��ύX�����s�̏ꍇ
                            // �i�ڃf�[�^���擾�A�\������
                            BuhinDataSet_S.V_BuhinInfoRow drBuhinInfo =
                                BuhinClass_S.getV_BuhinInfoRow(dr.BuhinCode, Global.GetConnection());
                            // ���b�g��
                            Label lblLot = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("LblLot") as Label;
                            if (drBuhinInfo.Lot != 0)
                                lblLot.Text = drBuhinInfo.Lot.ToString();
                            // ���P��   �ǉ� 09/07/28
                            HtmlInputCheckBox chkKariTanka = e.Row.FindControl("ChkKariTanka") as HtmlInputCheckBox;
                            if (dr.KariTankaFlg == "0")
                            {
                                chkKariTanka.Checked = true;
                            }
                            else
                            {
                                chkKariTanka.Checked = false;
                            }

                            // �P��
                            TextBox tbxTanka = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("TbxTanka") as TextBox;
                            tbxTanka.Text = drBuhinInfo.Tanka.ToString("#,##0.00"); 

                            // ����
                            TextBox tbxSuu = e.Row.Cells[G_CELL_SUURYOU].FindControl("TbxSuu") as TextBox;
                            tbxSuu.Text = dr.Suuryou;
                            // �P��
                            Label lblTani = e.Row.Cells[G_CELL_TANI].FindControl("LblTani") as Label;
                            lblTani.Text = drBuhinInfo.Tani;

                            if (drBuhinInfo.LT_Suuji != 0 && drBuhinInfo.LT_Tani != 0)
                            {
                                // ���[�h�^�C��                                
                                Label lblLT = e.Row.Cells[G_CELL_TANI].FindControl("LblLT") as Label;
                                lblLT.Text = drBuhinInfo.LT_Suuji + AppCommon.LT_Tani(drBuhinInfo.LT_Tani);

                                // �[��
                                //int nAddDays = AppCommon.GetAddDays(drBuhinInfo.LT_Suuji, drBuhinInfo.LT_Tani);
                                rdp.SelectedDate = AppCommon.GetNouki(drBuhinInfo.LT_Suuji, drBuhinInfo.LT_Tani);
                            }
                        }
                        else if (dr.BuhinCode != "")
                        {
                            // ���b�g��
                            Label lblLot = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("LblLot") as Label;
                            lblLot.Text = dr.Lot;
                            // ���P��
                            HtmlInputCheckBox chkKariTanka = e.Row.FindControl("ChkKariTanka") as HtmlInputCheckBox;
                            if (dr.KariTankaFlg == "0")
                            {
                                chkKariTanka.Checked = true;
                            }
                            else
                            {
                                chkKariTanka.Checked = false;
                            }
                            
                            // �P��
                            // Label lblTanka = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("LblTanka") as Label;
                            //TextBox tbxTanka = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("TextBox") as Label;
                            //tbxTanka.Text = dr.Tanka;

                            TextBox tbxTanka = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("TbxTanka") as TextBox;
                            tbxTanka.Text = dr.Tanka;
                           
                            // ����
                            TextBox tbxSuu = e.Row.Cells[G_CELL_SUURYOU].FindControl("TbxSuu") as TextBox;
                            tbxSuu.Text = dr.Suuryou;                     
                            // �P��
                            Label lblTani = e.Row.Cells[G_CELL_TANI].FindControl("LblTani") as Label;
                            lblTani.Text = dr.Tani;

                            // ���[�h�^�C��                                
                            Label lblLT = e.Row.Cells[G_CELL_TANI].FindControl("LblLT") as Label;
                            lblLT.Text = dr.LT;

                            // �[��
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
                                // ���[�h�^�C��                                
                                Label lblLT = e.Row.Cells[G_CELL_TANI].FindControl("LblLT") as Label;
                                lblLT.Text = drBuhinInfo.LT_Suuji + AppCommon.LT_Tani(drBuhinInfo.LT_Tani);

                                // �[��
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
                    // �敪�̑I�����Ȃ� = ddl�i�ڂ�""
                    nRowNo++;
                    return;
                }
                */

                /*
                // ���b�g��
                Label lblLot = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("LblLot") as Label;
                if (dr != null)
                {
                    lblLot.Text = dr.Lot;
                }
                // �P��
                Label lblTanka = e.Row.Cells[G_CELL_LOT_TANKA].FindControl("LblTanka") as Label;
                if (dr != null)
                {
                    lblTanka.Text = dr.Tanka;
                }
                */
                


               
                /*
                // �P��
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
                // �폜�`�F�b�N
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
                // �����{�^��
                if (this.BtnT.Disabled)
                {
                    this.BtnT.Disabled = false;
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.BtnT);
                }
                // �����������x��
                this.LblOK.Text = "";
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblOK);
            }

            switch (strCmd)
            {
                case "RowClear":
                    // �I�����ꂽ�s�̑S�Ă̍��ڂ𖢑I����Ԃɂ���
                    string strErrMsg = "�f�[�^�̍폜�Ɏ��s���܂���";
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
                    // �폜����s��
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
                    // �����o�^
                    strRowItemAry = strArgs[1];
                    if (strArgs[1] != "")
                    {
                        if (!this.SetOrderData(strRowItemAry))
                        {
                            this.ShowMsg("�G���[", true);
                            this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                            return;
                        }
                    }
                    LibError err = ChumonClass_S.T_Chumon_Insert(_dtOrder, SessionManager.LoginID, SessionManager.JigyoushoKubun, Convert.ToInt32(DdlTax.SelectedValue), Global.GetConnection());
                    if (err != null)
                    {
                        this.ShowMsg("�����Ɏ��s���܂���<br/>" + err.Message, true);
                        //return;
                    }
                    else
                    {
                        // �d����z����쐬
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
                            // ��L�[�ɂ���āA���[�����M�ɕK�v�f�[�^�擾                       
                            ChumonDataSet.V_MailInfoDataTable dtMail =
                                ChumonClass.getV_MailInfoDataTable(SessionManager.LoginID, aryShiire[i].ToString(), Global.GetConnection());
                            // ���[�������
                            for (int j = 0; j < dtMail.Rows.Count; j++)
                            {
                                MailClass.MailParam p = this.GetMailParam(dtMail[j]);

                                MailClass.SendMail(p, null);
                            }
                        }
                        this.ShowMsg("�������������܂���", false);

                        // �����������x��
                        this.LblOK.Text = "��L�̓��e�Ŕ������������܂���";
                        this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblOK);

                        this.BtnT.Disabled = true;
                        this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.BtnT);
                    }
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                    break;
                case "KubunChange":
                    // �敪�I��ύX         
                    strRowItemAry = strArgs[1];
                    if (strArgs[1] != "")
                    {
                        if (!this.SetOrderData(strRowItemAry))
                        {
                            this.ShowMsg("�G���[", true);
                            this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                            return;
                        }
                    }
                    this.Create();
                    this.ShowMsg("", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblMain);
                    break;
                case "AddRow":
                    // �s�ǉ�
                    strRowItemAry = strArgs[1];
                    if (strArgs[1] != "")
                    {
                        if (!this.SetOrderData(strRowItemAry))
                        {
                            this.ShowMsg("�G���[", true);
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
                    // �d����I��ύX
                    strRowItemAry = strArgs[1];//RowNo
                    //if (strArgs[1] != "")
                    //{
                    //    if (!this.SetOrderData(strRowItemAry))
                    //    {
                    //        this.ShowMsg("�G���[", true);
                    //        this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                    //        return;
                    //    }
                    //}
                    this.Create();
                    this.ShowMsg("", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblMain);

                    break;
                case "BuhinChange":
                    // �d����I��ύX
                    strRowItemAry = strArgs[1];
                    if (strArgs[1] != "")
                    {
                        if (!this.SetOrderData(strRowItemAry))
                        {
                            this.ShowMsg("�G���[", true);
                            this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                            return;
                        }
                    }
                    // �I��ύX�����sNo
                    _RowNo = int.Parse(this.HidArgs.Value);
                    this.Create();
                    this.ShowMsg("", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblMain);
                    break;
                case "AllClear":
                    // ������Ԃɖ߂�
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
            // ���M�����[���A�h���X
            p._MailFrom = dr.Mail_Y;
            // ���M�惁�[���A�h���X
            p._MailTo = dr.Mail_S;
            // ����
            p._Subject = "�V�K�������̂��ē�";
            // �{��
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
                    // �񐔃`�F�b�N
                    return false;
                }
                ChumonDataSet_S.V_OrderInputRow dr = _dtOrder.NewV_OrderInputRow();
                // �����̃L�[
                //dr.Year = strItemAry[0].Split('_')[0];
                //dr.Year = DateTime.Now.ToString("yy");
                //dr.HacchuuNo = strItemAry[0].Split('_')[1];                
                // �d����R�[�h
                dr.ShiiresakiCode = strItemAry[0];
                // ���i�敪
                dr.BuhinKubun = strItemAry[1];
                // ���i�R�[�h
                dr.BuhinCode = strItemAry[2];
                // ���b�g
                dr.Lot = strItemAry[3];

                // �P��                
                dr.Tanka = strItemAry[4];
                // ����
                dr.Suuryou = strItemAry[5];
                // �P��
                dr.Tani = strItemAry[6];
                // ���[�h�^�C��
                dr.LT = strItemAry[7];
                // �[��
                dr.Nouki = strItemAry[8];
                // �[���ꏊ�R�[�h
                dr.NounyuuBashoCode = strItemAry[9];
                // ���l
                dr.Bikou = strItemAry[10];
                // ���P��   �ǉ� 09/07/28
                dr.KariTankaFlg = strItemAry[11];


                _dtOrder.AddV_OrderInputRow(dr);
                
            }
            
            return true;
        }
        protected void DdlShiire_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            int nIndex = int.Parse(this.Request.Params["__EVENTARGUMENT"]);
            // �d����I��ύX
            if (ddl.SelectedIndex > 0) 
            {
            }


        }
        private void LoadDdlShiire(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("---", "0"));
            // �d������擾
            _dtShiire = ShiiresakiClass.getV_ShiiresakiDataTable(Global.GetConnection());

            ddl.Items.Add(new ListItem("---", ""));
            for (int i = 0; i<_dtShiire.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(_dtShiire[i].ShiiresakiCode + ":" + _dtShiire[i].ShiiresakiMei, _dtShiire[i].ShiiresakiCode));
            }
        }

    }
}
