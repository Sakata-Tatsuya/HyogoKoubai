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
    public partial class TantoushaAccountUpForm : Core.Web.ServerViewStatePage
    {
        // LoginID
        private string VsLoginID
        {
            get { return Convert.ToString(this.ViewState["VsLoginID"]); }
            set { this.ViewState["VsLoginID"] = value; }
        }

        protected int loadFlg = 0;  // �\����window_reload�p


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Yodoko) // Yodoko���̂ݕ\����
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }

                if (Request.Url.Query == "")
                {
                    // 
                    ListSet.SetDdlJigyoushoKubun(SessionManager.UserKubun, DdlJigyoushoKubun);
                    // �V�K
                    this.Shinki();

                }
                else
                {
                    try
                    {
                        // ���b�Z�[�WID
                        this.VsLoginID = Request.QueryString["key"]; // LoginID
                    }
                    catch
                    {
                        this.ShowTblMain(false);
                        this.ShowMsg(AppCommon.NO_DATA, true);
                        return;
                    }
                    // 
                    ListSet.SetDdlJigyoushoKubun(SessionManager.UserKubun, DdlJigyoushoKubun);
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


            // �o�^�{�^��(submit)
            this.BtnTS.Style.Add("display", "none");
            // �X�V�{�^��(submit)
            this.BtnKS.Style.Add("display", "none");

            // �����̂ݓ��͉�
            AppCommon.NumOnly(TbxTCode);

            this.HankakuChk();

        }

        private void HankakuChk()
        {
            this.TbxID.Attributes["onfocusout"] =
               string.Format("HankakuChk('{0}','{1}');", TbxPass.ClientID, "���O�C��ID");
            this.TbxPass.Attributes["onfocusout"] =
               string.Format("HankakuChk('{0}','{1}');", TbxPass.ClientID, "�p�X���[�h");
            this.TbxMail.Attributes["onfocusout"] =
               string.Format("HankakuChk('{0}','{1}');", TbxPass.ClientID, "E-Mail");

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
            m2mKoubaiDataSet.M_LoginRow dr =
                LoginClass.getM_LoginRow(VsLoginID, Global.GetConnection());
            if (dr == null)
            {
                this.ShowTblMain(false);
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            // ���O�C��ID
            LitLoginID.Text = dr.LoginID;
            
            //  ����
            TbxBusho.Text = dr.Busho;
            //�@��E
            TbxYakushoku.Text = dr.Yakushoku;
            //�@�p�X���[�h
            TbxPass.Text = dr.Password;
            
            //�@�S���҃R�[�h
            TbxTCode.Text = dr.TantoushaCode;
            //�@�S���Җ�
            TbxTName.Text = dr.Name;
            // ���[���A�h���X
            TbxMail.Text = dr.Mail;
            // �Ǘ��Ҍ���
            if (dr.KanrishaFlg)
                this.RbtnKanrisha.Checked = true;
            else
                this.RbtnKanrishaNashi.Checked = true;

            // ���Ə��敪
            DdlJigyoushoKubun.SelectedValue = dr.JigyoushoKubun.ToString();



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
            // �V�K�o�^  
            if (!b)
            {
                TbxID.Visible = b;
            }
            // �o�^            
            BtnT.Visible = b;
            // �X�V            
            BtnK.Visible = !b;
        }

        // Row�쐬
        private m2mKoubaiDataSet.M_LoginRow CreateRow(bool bSinki)
        {
            m2mKoubaiDataSet.M_LoginRow dr = LoginClass.newM_LoginRow();

            if (bSinki)
            {
                // �V�K��                
                // ���O�C��ID
                dr.LoginID = TbxID.Text;
            }
            else
            {
                // �X�V��              
                // ���O�C��ID
                dr.LoginID = LitLoginID.Text;
            }
            //�@���[�U�[�敪
            dr.UserKubun = (byte)UserKubun.Yodoko;
            //�@�d����R�[�h
            dr.KaishaCode = "0";
            //  ����
            dr.Busho = TbxBusho.Text;
            //�@��E
            dr.Yakushoku = TbxYakushoku.Text;
            //�@�p�X���[�h
            dr.Password = TbxPass.Text;           
            //�@�Ǘ��Ҍ���
            //dr.KanrishaFlg = false;
            if (this.RbtnKanrisha.Checked)
            {
                dr.KanrishaFlg = true;
            }
            else
            {
                dr.KanrishaFlg = false;
            }                       
           
            //�@�S���҃R�[�h
            dr.TantoushaCode = TbxTCode.Text;
            //�@�S���Җ�
            dr.Name = TbxTName.Text;
            // ���[���A�h���X
            dr.Mail = TbxMail.Text;
            // ���Ə��敪
            dr.JigyoushoKubun = int.Parse(DdlJigyoushoKubun.SelectedValue);

            return dr;
        }
        // �V�K�o�^�d���`�F�b�N
        private bool TourokuCheck(m2mKoubaiDataSet.M_LoginRow dr)
        {
            // �f�[�^�d���`�F�b�N
            m2mKoubaiDataSet.M_LoginRow drChk =
                LoginClass.getM_LoginRow(dr.LoginID, Global.GetConnection());
            if (drChk != null)
            {
                this.ShowMsg("���O�C��ID���d�����Ă��܂�", true);
                return false;
            }

            return true;
        }
        // �o�^�{�^���N���b�N
        protected void BtnTS_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.M_LoginRow dr = this.CreateRow(true);
            if (dr == null)
                return;
            // �V�K�o�^�d���`�F�b�N
            if (!TourokuCheck(dr))
                return;
            // �o�^
            LibError err = LoginClass.M_Login_Insert(dr, Global.GetConnection());
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
            m2mKoubaiDataSet.M_LoginRow dr1 = this.CreateRow(false);
            if (dr1 == null)
                return;
            // �X�V
            LibError err1 = LoginClass.M_Login_Update(VsLoginID, dr1, Global.GetConnection());
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