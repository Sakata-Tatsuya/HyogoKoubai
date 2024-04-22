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

namespace m2mKoubai
{
    public partial class NouhinForm : System.Web.UI.Page
    {
        //�O�����
        DateTime minDate = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);


        // ���ݕ\������Ă��锭��No
        private string VsInfo
        {
            get { return Convert.ToString(this.ViewState["VsInfo"]); }
            set { this.ViewState["VsInfo"] = value; }
        }

        // ���[�{�^�����������̂��[�i�m��{�^�����������̂�����
        bool judgeFlg = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Owner)
                {
                    HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                // �^�u
                CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                tab.Menu = CtlTabMain.MainMenu.Nouhin;

                DateTime dtNow = DateTime.Now;
                int nYear = dtNow.Year;
                // ���N
                DdlYear.Items.Add(nYear.ToString());
                // ���N
                nYear--;
                DdlYear.Items.Add(nYear.ToString());
                // �ė��N
                nYear--;
                DdlYear.Items.Add(nYear.ToString());
                // ���N��I������
                DdlYear.SelectedValue = dtNow.Year.ToString();



                //ListSet.SetYear(DdlYear);
                // �ŏ��͔�\��
                this.ShowTblMain(false);


                ///2013/05 �[���蓮�C���@�\�ǉ�
                ///
                this.RdpDay.SelectedDate = DateTime.Today;

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
            // �m�F
            this.BtnCK.Attributes["onclick"] = "Check(); return false;";
            // �N���A
            this.BtnC.Attributes["onclick"] = string.Format("Clear('{0}'); return false;", TblList.ClientID);
                //"Clear(); return false;";
            // �[�i�m��
            this.BtnNK.Attributes["onclick"] = "Nouhin(); return false;";
            // ���[  �ǉ� 09/07/23
            this.BtnKN.Attributes["onclick"] = "Kannou(); return false;";
            // Img
            this.Img1.Style.Add("display", "none");
            // 
            this.TbxNouhinsuu.Attributes["onfocusout"] =
                string.Format("SuuryouChk('{0}','{1}','{2}'); return false;",
                LblSuuryou.Text.Replace(",", ""), LblNouhinSumiSuu.Text, TbxNouhinsuu.ClientID);

            this.TbxHacchuNo.Attributes["onkeyup"] = "KenChk();";
            // �����̂ݓ��͉�
            this.SetNumOnly();


            ///2013/05 �[���̎蓮�ύX�@�\�ǉ�
            ///
            /*
             �E�J�����_�[��1�J���ȏ�O�͑I��s�B
             �i����͂��ēo�^���悤�Ƃ���ƁA�G���[���b�Z�[�W��\������B�j
            �E�J�����_�[�͖������ł̑I���͕s�B
             �i����͂��ēo�^���悤�Ƃ���ƁA�G���[���b�Z�[�W��\������B�j
            */
            this.RdpDay.MinDate = minDate;
            this.RdpDay.MaxDate = DateTime.Today;

        }
        // �����̂ݓ��͉�
        private void SetNumOnly()
        {
            AppCommon.NumOnly(TbxNouhinsuu);
            // AppCommon.NumOnly(TbxHacchuNo);
        }
        private void Create()
        {
            // ����No��7�������œ��͂��ꂽ��0�𖄂߂�
            string strHacchuNoZeroume = "";

            try
            {
                strHacchuNoZeroume = string.Format("{0:0000000}", Convert.ToInt32(TbxHacchuNo.Value));
            }
            catch
            {
                ShowMsg(AppCommon.NO_DATA, true);
                this.ShowTblMain(false);
                return;
            }

            // string strYear = 
            // ����No�ɂ���āA�����f�[�^���擾
            NouhinDataSet_N.V_NouhinRow dr =
                NouhinClass_Y.getV_NouhinRow(DdlYear.SelectedValue.Substring(2, 2), strHacchuNoZeroume, SessionManager.JigyoushoKubun, Global.GetConnection());


            // �ύX 09/07/28

            if (dr == null)
            {
                ShowMsg(AppCommon.NO_DATA, true);
                this.ShowTblMain(false);
                return;

            }
            else if (dr.KaritankaFlg)
            {
                ShowMsg("���̒����͉��P���Ŕ�������Ă��܂��@�P�����m�肵�Ă�������", true);
                this.ShowTblMain(false);
                return;
            }
            else
            {
                // �[�iNo
                int nNouhinNo = 0;
                if (!dr.IsNouhinNoNull())
                    nNouhinNo = dr.NouhinNo;

                VsInfo = dr.Year + "_" + dr.HacchuuNo + "_" + dr.JigyoushoKubun.ToString() + "_" + nNouhinNo.ToString();


                this.ShowMsg("", false);
                this.ShowTblMain(true);
                this.GetHacchuuInfo(dr);
            }
        }

        private void GetHacchuuInfo(NouhinDataSet_N.V_NouhinRow dr)
        {
            // ����No
            this.LblHacchuuNo.Text = dr.HacchuuNo;

            // �d����R�[�h               
            this.LblShiireCode.Text = dr.ShiiresakiCode;
            // �d���於
            this.LblShiireMei.Text = dr.ShiiresakiMei;
            // �i���O���[�v
            this.LblBuhinGroup.Text = dr.BuhinKubun;
            // �R�[�h
            this.LblBuhinCode.Text = dr.BuhinCode;
            // �i��  �C�� 09/07/23
            if (!dr.IsBuhinMeiNull())
            {
                this.LblBuhinMei.Text = dr.BuhinMei;
            }
            else
            {
                this.LblBuhinMei.Text = "";
            }
            // ����
            LblSuuryou.Text = dr.Suuryou.ToString("#,##0");
            // �P��            
            LblTanka.Text = "\\" + dr.Tanka.ToString("#,##0.#0");
            // �������z
            decimal dKingaku_Round = Math.Round(dr.Kingaku, 0, MidpointRounding.AwayFromZero);
            LblChumonKingaku.Text = Convert.ToString(string.Format("{0:C}", dKingaku_Round));
            // ���őΉ�
            if (!this.bTourokuKanryou)
            {
                this.DdlTax.SelectedValue = dr.Zeiritu.ToString();
                this.Create_Zeigaku(dKingaku_Round);
            }


            // �P��  �C�� 09/07/23
            if (!dr.IsTaniNull())
            {
                LblTani.Text = dr.Tani;
            }
            else
            {
                this.LblTani.Text = "";
            }
            // �[���ꏊ
            LblBasho.Text = dr.BashoMei;
            // �[��
            string strNouki = "";
            if (!dr.IsHenkouNoNull())
            {
                m2mKoubaiDataSet.T_NoukiHenkouDataTable dtNouki =
                    NoukiHenkouClass.getT_NoukiHenkouDataTable(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, dr.HenkouNo, Global.GetConnection());
                for (int i = 0; i < dtNouki.Rows.Count; i++)
                {
                    strNouki += Utility.FormatFromyyyyMMdd(dtNouki[i].Nouki.ToString()) + "<br>";
                }
            }
            LblNouki.Text = Utility.FormatFromyyyyMMdd(dr.Nouki);

            // �񓚔[��
            string strKaitou = "";
            if (!dr.IsKaitouNoNull())
            {
                m2mKoubaiDataSet.T_NoukiKaitouDataTable dt =
                    NoukiKaitouClass.getT_NoukiKaitouDataTable(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, dr.KaitouNo, Global.GetConnection());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strKaitou += Utility.FormatFromyyyyMMdd(dt[i].Nouki.ToString()) + "<br>";
                }
            }
            LblKaitouNouki.Text = strKaitou;

            // �[�i�ϐ���
            int nNouhinSuuryou = 0;
            if (!dr.IsNouhinSuuryouNull())
                nNouhinSuuryou = dr.NouhinSuuryou;

            LblNouhinSumiSuu.Text = nNouhinSuuryou.ToString("#,##0");

            // �[���c��
            int nZanSuu = dr.Suuryou - nNouhinSuuryou;
            TbxNouhinsuu.Text = nZanSuu.ToString();
            // �C�� 09/07/23
            if (nZanSuu == 0 && dr.KannouFlg == false)
            {
                dr.KannouFlg = true;
                string[] strkey = VsInfo.Split('_');
                m2mKoubaiDataSet.T_ChumonRow drChumon = this.UpdateCreateRow();
                if (dr == null)
                    return;
                string strYear = strkey[0];
                string strHacchuuNo = strkey[1];
                int nKubun = int.Parse(strkey[2]);
                int nNo = int.Parse(strkey[3]);

                LibError err = ChumonClass.T_Chumon_Update(strYear, strHacchuuNo, nKubun, drChumon, Global.GetConnection());
                if (err != null)
                {
                    this.ShowMsg(err.Message, true);
                }                
            }
            // �C�� 09/07/23
            if (nZanSuu != 0 && dr.KannouFlg == false)
            {
                this.TbxNouhinsuu.Visible = true;
                this.LitMsg.Text = "";
            }
            // �ǉ� 09/07/23
            if (dr.KannouFlg == true)
            {
                this.TbxNouhinsuu.Visible = false;
                this.LitMsg.Text = "���[��";
                this.BtnNK.Visible = false;
                this.BtnKN.Visible = false;
            }  
        }

        // GridView�\��
        private void ShowTblMain(bool b)
        {
            TblMain.Visible = b;
            BtnNK.Visible = b;
            BtnKN.Visible = b;
        }
        // ���b�Z�[�W�\��
        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }

        bool bTourokuKanryou = false;

        protected void Ram_AjaxRequest(object sender, Telerik.WebControls.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];

            if (strCmd == "Check")
            {
                this.Create();

                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TbxHacchuNo);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
            }
            else if (strCmd == "Clear")
            {
                this.TbxHacchuNo.Value = "";
                this.ShowTblMain(false);
                this.ShowMsg("", false);
                this.BtnNK.Visible = false;
                this.BtnKN.Visible = false;

                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TbxHacchuNo);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
            }
            // �C�� 09/07/24
            else if (strCmd == "Nouhin")
            {

                ///2013/05 �[���̎蓮�ύX�@�\�ǉ�
                ///
                /*
                 �E�J�����_�[��1�J���ȏ�O�͑I��s�B
�@                �i����͂��ēo�^���悤�Ƃ���ƁA�G���[���b�Z�[�W��\������B�j
                �E�J�����_�[�͖������ł̑I���͕s�B
�@                �i����͂��ēo�^���悤�Ƃ���ƁA�G���[���b�Z�[�W��\������B�j
                */
                if (this.RdpDay.SelectedDate == null)
                {
                    this.ShowMsg("�[�i�����w�肳��Ă��܂���", true);
                    return;
                }
                
                if (this.RdpDay.SelectedDate.Value < minDate)
                {
                    this.ShowMsg("�O������ȑO�̓��t�͎w��o���܂���", true);
                    return;
                }

                if (DateTime.Today < this.RdpDay.SelectedDate.Value)
                {
                    this.ShowMsg("�{���ȍ~�̓��t�͎w��o���܂���", true);
                    return;
                }


                string[] strkey = VsInfo.Split('_');

                m2mKoubaiDataSet.T_NouhinRow drN = this.CreateRow();
                if (drN == null)
                    return;

                m2mKoubaiDataSet.T_ChumonRow drC = this.UpdateCreateRow();
                if (drC == null)
                    return;
                
                // �o�^                
                LibError err = NouhinClass_Y.T_Nouhin_Insert_T_Chumon_Update
                    (drN, drC, strkey[0], strkey[1], int.Parse(strkey[2]), int.Parse(strkey[3]),judgeFlg, SessionManager.LoginID, Global.GetConnection());
                if (err != null)
                {
                    this.ShowMsg("�o�^�Ɏ��s���܂���" + "<br/>" + err.Message, true);
                }
                else
                {
                    bTourokuKanryou = true;
                    this.Create();
                    this.ShowMsg("�o�^���܂���", false);
                }

                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
            }
            // �ǉ� 09/07/23
            else if (strCmd == "Kannou")
            {
                ///2013/05 �[���̎蓮�ύX�@�\�ǉ�
                ///
                if (this.RdpDay.SelectedDate == null)
                {
                    this.ShowMsg("�[�i�����w�肳��Ă��܂���", true);
                    return;
                }

                if (this.RdpDay.SelectedDate.Value < minDate)
                {
                    this.ShowMsg("�O������ȑO�̓��t�͎w��o���܂���", true);
                    return;
                }

                if (DateTime.Today < this.RdpDay.SelectedDate.Value)
                {
                    this.ShowMsg("�{���ȍ~�̓��t�͎w��o���܂���", true);
                    return;
                }


                this.judgeFlg = true;
                string[] strkey = VsInfo.Split('_');

                m2mKoubaiDataSet.T_NouhinRow drN = this.CreateRow();
                if (drN == null)
                    return;

                m2mKoubaiDataSet.T_ChumonRow drC = this.UpdateCreateRow();
                if (drC == null)
                    return;

                LibError err = NouhinClass_Y.T_Nouhin_Insert_T_Chumon_Update
                    (drN, drC, strkey[0], strkey[1],int.Parse(strkey[2]),int.Parse(strkey[3]), judgeFlg, SessionManager.LoginID, Global.GetConnection());
                if (err != null)
                {
                    this.ShowMsg("�o�^�Ɏ��s���܂���" + "<br/>" + err.Message, true);
                    this.TblMain.Style.Add("display", "none");
                }
                else
                {
                    bTourokuKanryou = true;
                    this.Create();
                    this.ShowMsg("�o�^���܂���", false);
                    this.TbxNouhinsuu.Visible = false;
                    this.LitMsg.Text = "���[��";
                    this.BtnNK.Visible = false;
                    this.BtnKN.Visible = false;
                }

                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
            }

        }
        // �o�^�pRow
        private m2mKoubaiDataSet.T_NouhinRow CreateRow()
        {
            string[] strkey = VsInfo.Split('_');
            // NewRow
            m2mKoubaiDataSet.T_NouhinRow dr =
               NouhinClass_Y.newT_NouhinRow();

            dr.Year = strkey[0];
            dr.HacchuuNo = strkey[1];
            dr.JigyoushoKubun = int.Parse(strkey[2]);
            dr.NouhinNo = int.Parse(strkey[3]) + 1;
            //dr.NouhinBi = DateTime.Now;
           // dr.Suuryou = int.Parse(TbxNouhinsuu.Text);
            dr.Suuryou = int.Parse(TbxNouhinsuu.Text );


            ///2013/05 �[���̎蓮�ύX�@�\�ǉ�
            ///
            dr.NouhinBi = this.RdpDay.SelectedDate.Value;

            dr.Zeiritu = Convert.ToInt32(this.DdlTax.SelectedValue);


            return dr;

        }

        // �ǉ� 09/07/23
        private m2mKoubaiDataSet.T_ChumonRow UpdateCreateRow()
        {
            string[] strkey = VsInfo.Split('_');
            // NewRow
            m2mKoubaiDataSet.T_ChumonRow dr =
                ChumonClass.getT_ChumonRow(strkey[0], strkey[1], int.Parse(strkey[2]), Global.GetConnection());

            dr.KannouFlg = true;

            return dr;

        }

        protected void DdlTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strChumonKingaku = this.LblChumonKingaku.Text.Trim().Replace("\\", "").Replace(",", "");

            int nChumonKingaku = 0;
            if (int.TryParse(strChumonKingaku, out nChumonKingaku))
            {
                this.Create_Zeigaku(nChumonKingaku);
            }
            else
            {
                this.LblZeigaku.Text = "";
            }
        }


        private void Create_Zeigaku(decimal dKingaku_Round)
        {
            int Zeiritu = Convert.ToInt32(this.DdlTax.SelectedValue);

            decimal dZeigaku = Math.Round(dKingaku_Round * Zeiritu / 100, 0, MidpointRounding.AwayFromZero);

            LblZeigaku.Text = Convert.ToString(string.Format("{0:C}", dZeigaku));
        }
    }
}