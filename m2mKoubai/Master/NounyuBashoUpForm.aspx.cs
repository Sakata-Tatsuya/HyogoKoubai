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
    public partial class NounyuBashoUpForm : System.Web.UI.Page
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
                if (SessionManager.UserKubun != (byte)UserKubun.Owner) // �������̂ݕ\����
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
            this.PreRender += new EventHandler(Form_PreRender);
        }

        private void Form_PreRender(object sender, EventArgs e)
        {
            // �o�^�{�^��
            this.BtnT.Attributes["onclick"] = "Touroku(); return false; ";
            // �X�V�{�^��
            this.BtnK.Attributes["onclick"] = "Koushin(); return false; ";
            // ����{�^��
            this.BtnC.Attributes["onclick"] = "Close(); return false; ";
            // �����̂ݓ��͉\
            AppCommon.NumOnly(this.TbxCode);
           
            // �o�^�{�^��(submit)
            this.BtnTS.Style.Add("display", "none");
            // �X�V�{�^��(submit)
            this.BtnKS.Style.Add("display", "none");
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
            m2mKoubaiDataSet.M_NounyuuBashoRow dr = NounyuuBashoClass.getM_NounyuuBashoRow(VsCode, Global.GetConnection());
            if (dr == null)
            {
                this.ShowTblMain(false);
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            

            // �[���ꏊ�R�[�h
           // TbxCode.Text = dr.NounyuuBashoCode;
            LitCode.Text = dr.BashoCode;
            // �[���ꏊ��
            TbxName.Text = dr.BashoMei;

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
        // �o�^�A�X�V�\��
        private void ShinkiTouroku(bool b)
        {
            // �o�^            
            BtnT.Visible = b;
            // �X�V            
            BtnK.Visible = !b;

            if (!b)
            {                
                TbxCode.Visible = b;
            }
        }




        // Row�쐬
        private m2mKoubaiDataSet.M_NounyuuBashoRow CreateRow()
        {
            m2mKoubaiDataSet.M_NounyuuBashoRow dr = NounyuuBashoClass.newM_NounyuuBashoRow();
           
            // �[���ꏊ�R�[�h
            dr.BashoCode = TbxCode.Text;
            // �[���ꏊ��
            dr.BashoMei = TbxName.Text;          

            return dr;
        }
        // �V�K�o�^�d���`�F�b�N
        private bool TourokuCheck(m2mKoubaiDataSet.M_NounyuuBashoRow dr)
        {
            // �f�[�^�d���`�F�b�N
            m2mKoubaiDataSet.M_NounyuuBashoRow drChk =
                NounyuuBashoClass.getM_NounyuuBashoRow(dr.BashoCode, Global.GetConnection());
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
            m2mKoubaiDataSet.M_NounyuuBashoRow dr = this.CreateRow();
            if (dr == null)
                return;
            if (!TourokuCheck(dr))
                return;
            LibError err = NounyuuBashoClass.M_NounyuuBasho_Insert(dr, Global.GetConnection());
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
            m2mKoubaiDataSet.M_NounyuuBashoRow dr1 = this.CreateRow();
            if (dr1 == null)
                return;

            LibError err1 = NounyuuBashoClass.M_NounyuuBasho_Update(VsCode, dr1, Global.GetConnection());
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
