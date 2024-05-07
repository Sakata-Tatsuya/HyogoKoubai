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
using System.Drawing;

namespace m2mKoubai.Order
{
    public partial class OrderShousaiForm : System.Web.UI.Page
    {
        /// <summary>
        /// ��L�[1
        /// </summary>
        private string VsYear
        {
            get { return Convert.ToString(this.ViewState["VsYear"]); }
            set { this.ViewState["VsYear"] = value; }
        }
        /// <summary>
        /// ��L�[2
        /// </summary>
        private string VsHacchuuNo
        {
            get { return Convert.ToString(this.ViewState["VsHacchuuNo"]); }
            set { this.ViewState["VsHacchuuNo"] = value; }
        }
        /// <summary>
        /// ��L�[3
        /// </summary>
        private int VsKubun
        {
            get { return Convert.ToInt32(this.ViewState["VsKubun"]); }
            set { this.ViewState["VsKubun"] = value; }
        }

        bool b = false;
        protected int loadFlg = 0;  // �\����window_reload�p
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
             {
                string[] strAry = HttpContext.Current.Request.Form["HidKey"].Split('\t');
                if (strAry == null)
                {
                    ShowMsg(AppCommon.NO_DATA, true);
                    this.ShowTblList(false);
                    return;
                }
                // �����L�[���
                ChumonClass.ChumonKey key = new ChumonClass.ChumonKey(strAry[0]);

                // ��L�[1
                VsYear = key.Year;
                // ��L�[2
                VsHacchuuNo = key.HacchuuNo;
                // ��L�[3
                VsKubun = key.JigyoushoKubun;

                if (strAry[1] != "")
                    this.Create(!b);�@// ��������ʂ���̏ꍇ
                else
                {
                    this.Create(b);�@// ��������ʈȊO����̏ꍇ
                    this.LblMsg.Visible = b;
                }
            }
            loadFlg = 0;
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
            // �L�����Z���{�^��
            this.BtnCancel.Attributes["onclick"] = "Cancel(); return false;";
            // 
            this.BtnProcess.Style.Add("display", "none");
            // �X�V�{�^��
            this.BtnK.Attributes["onclick"] =
                string.Format("Koushin('{0}','{1}'); return false;", LitSuuryou.Text, LblTanka.Text);
        }

        private void Create(bool b)
        {
            // Hidden
            HidProcess.Value = "";

            ChumonDataSet.V_Chumon_MeisaiRow dr =
                ChumonClass.getV_Chumon_MeisaiRow(VsYear, VsHacchuuNo, VsKubun, Global.GetConnection());
            if (dr != null)
            {
                // ����No
                LitHacchuNo.Text = dr.HacchuuNo;
                // ������
                LitHacchuubi.Text = dr.HacchuuBi.ToString("yy/MM/dd");
                // �d����R�[�h
                LitShiireCode.Text = dr.ShiiresakiCode;
                // �d���於
                LitShiireName.Text = dr.ShiiresakiMei;
                // �i�ڃO���[�v
                this.LitKubun.Text = dr.BuhinKubun;
                // �R�[�h
                LitBuhinCode.Text = dr.BuhinCode;
                // �i�ږ�
                LitBuhinName.Text = dr.BuhinMei;
                // ����
                LitSuuryou.Text = dr.Suuryou.ToString("#,##0");
                // �P��
                if (dr.KaritankaFlg)
                {
                    LblT.Text = "(��)" + "\\";
                    LblTanka.Text = dr.Tanka.ToString();
                    LblT.ForeColor = Color.Red;
                    LblTanka.ForeColor = Color.Red;
                    //dr.Tanka.ToString("#,##0.#0");
                }
                else
                {
                    LblT.Text = "\\";
                    LblTanka.Text = dr.Tanka.ToString();
                   // LblT.ForeColor = Color.Black;
                    //LblTanka.ForeColor = Color.Black;
                }
                // �������z
                // ���őΉ� �������͐؂�̂Ăł͂Ȃ��l�̌ܓ�
                //decimal Kingaku = Math.Floor(dr.Suuryou * dr.Tanka);
                decimal Kingaku = Math.Round(dr.Suuryou * dr.Tanka, 0, MidpointRounding.AwayFromZero);

                if (dr.KaritankaFlg)
                {

                    LblKingaku.Text = "\\" + Kingaku.ToString("#,##0");
                    LblKingaku.ForeColor = Color.Red;
                }
                else
                {
                    LblKingaku.Text = "\\" + Kingaku.ToString("#,##0");
                    LblKingaku.ForeColor = Color.Black;
                }
                // �P��
                LitTani.Text = dr.Tani;
                // �[���ꏊ�R�[�h
                //LitNBashoCode.Text = dr.NounyuuBashoCode;
                // �[���ꏊ��
                LitNBashoMei.Text = dr.BashoMei;
                // �����S���҃R�[�h
                LitTantoushaID.Text = dr.TantoushaCode;
                // �����S���Җ�
                LitTantoushaMei.Text = dr.Name;
                // ���l
                TbxBikou.Text = dr.Bikou;
                // 
                if (b)
                {
                    if (!dr.IsCancelBiNull())
                    {
                        // �L�����Z�����b�Z�[�W
                        LblMsg.Text = "�L�����Z�������F" + dr.CancelBi.ToString("yy/MM/dd HH:mm");
                        // �ύX�R���g���[���̔�\��
                        ShowHouji(!b);
                    }
                    else
                    {
                        // �ύX�R���g���[���̕\��
                        ShowHouji(b);
                    }
                }
                else
                {
                    ShowHouji(b);
                }

                this.ShowTblList(true);
            }
            else
            {
                ShowMsg(AppCommon.NO_DATA, true);
                this.ShowTblList(false);
                return;
            }
        }
        // �ύX�R���g���[���̕\��
        private void ShowHouji(bool b)
        {
            // �L�����Z�����b�Z�[�W�̕\��
            LblMsg.Visible = !b;
            // �L�����Z���{�^��
            BtnCancel.Visible = b;
            // �X�V�{�^��
            BtnK.Visible = b;
            // ���ʕύX�e�[�u��
            TblS.Visible = b;
            // �P���ύX�e�[�u��
            TblT.Visible = b;
        }
        // ���C���e�[�u����\��
        private void ShowTblList(bool b)
        {
            TblAll.Visible = b;
        }

        private void ShowMsg(string strMsg, bool bError)
        {
            LblErrMsg.Text = strMsg;
            LblErrMsg.ForeColor = (bError) ? Color.Red : Color.Black;
        }

        protected void BtnProcess_Click(object sender, EventArgs e)
        {
            // �����̓��e
            string strProcess = this.HidProcess.Value;
            if (strProcess == "cancel")
            {

                m2mKoubaiDataSet.T_ChumonRow dr = ChumonClass.newT_ChumonRow();
                // �L�����Z����
                dr.CancelBi = DateTime.Now;
                // �L�����Z�����̓o�^
                LibError err = ChumonClass.T_Chumon_Update_CancelBi(VsYear, VsHacchuuNo, VsKubun, dr, Global.GetConnection());
                if (err == null)
                {
                    // ��L�[�ɂ���āA���[�����M�ɕK�v�f�[�^�擾
                   ChumonDataSet.V_MailInfoDataTable dtMail =
                                              ChumonClass.getV_MailInfoDataTable(VsYear, VsHacchuuNo, VsKubun, Global.GetConnection());
                    
                    for (int i = 0; i < dtMail.Rows.Count; i++)
                    {
                        MailClass.MailParam p = this.GetMailParam(dtMail[i], strProcess);

                        MailClass.SendMail(p, null);
                    }
                }
                else
                {
                    this.ShowMsg(e.ToString(), true);
                    return;
                }
                this.Create(true);
                this.ShowMsg("�L�����Z�����܂���", false);

                loadFlg = 1;
            }

            else if (strProcess == "koushin")
            {
                // NewRow
                m2mKoubaiDataSet.T_ChumonRow dr = ChumonClass.newT_ChumonRow();


                if (TbxT.Text != "")
                    dr.Tanka = decimal.Parse(TbxT.Text);
                else
                    dr.Tanka = -1;


                if (TbxS.Text != "")
                    dr.Suuryou = int.Parse(TbxS.Text.Replace(",",""));
                else
                    dr.Suuryou = -1;
                // ���ʁA�P�����X�V����
                LibError err = ChumonClass.T_Chumon_Update(VsYear, VsHacchuuNo, VsKubun, dr, Global.GetConnection());
                if (err == null)
                {

                    // ���ʕύX�̂݃��[�����M
                    if (TbxS.Text != "")
                    {
                        // ��L�[�ɂ���āA���[�����M�ɕK�v�f�[�^�擾                     
                        ChumonDataSet.V_MailInfoDataTable dtMail =
                           ChumonClass.getV_MailInfoDataTable(VsYear, VsHacchuuNo, VsKubun, Global.GetConnection());
                        for (int i = 0; i < dtMail.Rows.Count; i++)
                        {
                            MailClass.MailParam p = this.GetMailParam(dtMail[i], strProcess);

                            MailClass.SendMail(p, null);
                        }
                    }
                }
                else
                {
                    this.ShowMsg(e.ToString(), true);
                    return;
                }
                this.Create(true);

                ShowMsg("�X�V���܂���", false);
                TbxT.Text = "";
                TbxS.Text = "";

                loadFlg = 1;
            }
        }
        private MailClass.MailParam GetMailParam(ChumonDataSet.V_MailInfoRow dr, string strMsg)
        {

            MailClass.MailParam p = new MailClass.MailParam();
            // ���M�����[���A�h���X
            p._MailFrom = dr.Mail_Y;
            // ���M�惁�[���A�h���X
            p._MailTo = dr.Mail_S;

            if (strMsg == "cancel")
            {
                // ����
                p._Subject = "����������̂��ē�";
                // �{��
                p._Body = MailClass.GetBody_Cancel(dr);
            }
            else
            {
                // ����
                p._Subject = "�������ʕύX�̂��ē�";
                // �{��
                p._Body = MailClass.GetBody_Suuryou(dr);
            }

            // SMTP
            p._SMTP_Server = Global.SMTP_Server;
            return p;
        }
    }
}