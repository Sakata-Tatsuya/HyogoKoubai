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
    public partial class KaishaInfoUpForm : System.Web.UI.Page
    {
        // Code
        private string VsCode
        {
            get { return Convert.ToString(this.ViewState["VsCode"]); }
            set { this.ViewState["VsCode"] = value; }
        }

        protected int loadFlg = 0;  // �\����window_reload�p

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Owner) // ������
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                if (Request.Url.Query == "")
                {
                    // �V�K
                    this.Shinki();
                }
                else
                {
                    try
                    {
                        // Code
                        this.VsCode = Request.QueryString["key"];
                    }
                    catch
                    {
                        this.ShowTblMain(false);
                        this.ShowMsg(AppCommon.NO_DATA, true);
                        return;
                    }
                    // �X�V
                    this.Koushin();

                }
                loadFlg = 0;

            }
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
            // �o�^�{�^��
            this.BtnT.Attributes["onclick"] = "Touroku(); return false;";
            // �X�V�{�^��
            this.BtnK.Attributes["onclick"] = "Koushin(); return false;";
            // ����{�^��
            this.BtnC.Attributes["onclick"] = "Close(); return false;";

            // �o�^�{�^��(submit)
            this.BtnTS.Style.Add("display", "none");
            // �X�V�{�^��(submit)
            this.BtnKS.Style.Add("display", "none");



          
            AppCommon.NumOnly(TbxKaishaID);

            
        }
        
        // �V�K
        private void Shinki()
        {
            this.ShinkiTouroku(true);
            this.ShowTblMain(true);
           
        }

        // �X�V
        private void Koushin()
        {
            m2mKoubaiDataSet.T_KaishaInfoRow dr = KaishaInfoClass.getT_KaishaInfoRow(int.Parse(VsCode), Global.GetConnection());
            if (dr == null)
            {
                this.ShowTblMain(false);
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            // ���Ə��R�[�h
            LitCode.Text = dr.KaishaID.ToString();
            
            // ��Ж�
            //TbxKaishaMei.Text = dr.KaishaMei;
            // ���Ə���
            TbxEigyousho.Text = dr.EigyouSho;
            // �Z��
            TbxJyusho.Text = dr.Address;
            // �X�֔ԍ�
            TbxYubin.Text = dr.Yuubin;
            // TEL
            TbxTel.Text = dr.Tel;
            // FAX
            TbxFax.Text = dr.Fax;
            // E-Mail
            TbxMail.Text = dr.Mail;
            // �K�i���������s���Ǝ�
            if (dr.InvoiceRegFlg)
            { this.RbtSumi.Checked = true; }
            else
            { this.RbtMi.Checked = true; }
            // �K�i���������s���ƎҔԍ�
            this.TbxInvoiceNo.Text = dr.InvoiceRegNo;

            this.ShinkiTouroku(false);
            this.ShowTblMain(true);
        }
        // ���b�Z�[�W�\��
        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }
        // ���C���e�[�u���\���A��\��
        private void ShowTblMain(bool b)
        {
            this.TblMain.Visible = b;
            this.TblBtn.Visible = b;
        }
        // �o�^�E�X�V�\��
        private void ShinkiTouroku(bool b)
        {
            // �o�^
            BtnT.Visible = b;
            // �X�V
            BtnK.Visible = !b;

            if (!b)
            {
                TbxKaishaID.Visible = b;
            }
        }

        // Row�쐬
        private m2mKoubaiDataSet.T_KaishaInfoRow CreateRow(bool bShinki)
        {
            m2mKoubaiDataSet.T_KaishaInfoRow dr = KaishaInfoClass.newT_KaishaInfoRow();

            // ���Ə��R�[�h
            if (bShinki)
            {
                // �V�K                
                dr.KaishaID = Convert.ToInt32(this.TbxKaishaID.Text);
            }
            else
            {
                // �X�V
                dr.KaishaID = Convert.ToInt32(LitCode.Text);
            }
            // ��Ж�
            dr.KaishaMei = "";
           
            // ���Ə���
            dr.EigyouSho = this.TbxEigyousho.Text;
            // �Z��
            dr.Address = this.TbxJyusho.Text;
            // �X�֔ԍ�
            dr.Yuubin =this.TbxYubin.Text;
            // TEL
            dr.Tel = this.TbxTel.Text;
            // FAX
            dr.Fax = this.TbxFax.Text;
            // E-Mail
            dr.Mail = this.TbxMail.Text;
            // �K�i���������s���Ǝ�
            if (this.RbtSumi.Checked)
            { dr.InvoiceRegFlg = true; }
            else
            { dr.InvoiceRegFlg = false; }
            // �K�i���������s���ƎҔԍ�
            dr.InvoiceRegNo = this.TbxInvoiceNo.Text;

            return dr;
        }
        // �V�K�o�^�d���`�F�b�N
        private bool TourokuCheck(m2mKoubaiDataSet.T_KaishaInfoRow dr)
        {
            // �f�[�^�d���`�F�b�N
            m2mKoubaiDataSet.T_KaishaInfoRow drChk = KaishaInfoClass.getT_KaishaInfoRow(dr.KaishaID, Global.GetConnection());
            if (drChk != null)
            {
                this.ShowMsg("�f�[�^���d�����Ă��܂�", true);
                return false;
            }
            return true;
        }

        // �o�^�{�^���N���b�N
        protected void BtnTS_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.T_KaishaInfoRow dr = this.CreateRow(true);
            if (dr == null)
                return;
            if (!TourokuCheck(dr))
                return;
            // �o�^
            LibError err = KaishaInfoClass.T_KaishaInfo_Insert(dr, Global.GetConnection());
            if (err != null)
            {
                this.ShowMsg(err.Message, true);
                return;
            }
            this.ShowMsg("�o�^���܂���", false);

            loadFlg = 1;
        }
        // �X�V�{�^���N���b�N
        protected void BtnKS_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.T_KaishaInfoRow dr1 = this.CreateRow(false);
            if (dr1 == null)
                return;

            LibError err1 = KaishaInfoClass.T_KaishaInfo_Update(int.Parse(VsCode), dr1, Global.GetConnection());
            if (err1 != null)
            {
                this.ShowMsg(err1.Message, true);
                return;
            }
            this.ShowMsg("�X�V���܂���", false);

            loadFlg = 1;

        }
    }
}
