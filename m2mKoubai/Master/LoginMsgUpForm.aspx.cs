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
    public partial class LoginMsgUpForm : System.Web.UI.Page
    {
        // MsgID
        private int VsMsgID
        {
            get { return Convert.ToByte(this.ViewState["VsMsgID"]); }
            set { this.ViewState["VsMsgID"] = value; }
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
                    // �V�K
                    this.Shinki();
                }
                else
                {
                    try
                    {
                        // ���b�Z�[�WID
                        this.VsMsgID = int.Parse(Request.QueryString["MsgID"]); // MsgID
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
            this.PreRender += new EventHandler(LoginMsgUpForm_PreRender);
        }

        private void LoginMsgUpForm_PreRender(object sender, EventArgs e)
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
            m2mKoubaiDataSet.M_LoginMsgRow dr =
                LoginMsgClass.getM_LoginMsgRow(VsMsgID, Global.GetConnection());
            if (dr == null)
            {
                this.ShowTblMain(false);
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            // �L��/����
            if (dr.DelFlg)
                this.RbtnMukou.Checked = true;
            // msg
            this.TbxMsg.Text = dr.Msg;
            
           
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
        }

        // Row�쐬
        private m2mKoubaiDataSet.M_LoginMsgRow CreateRow(bool bSinki)
        {
            m2mKoubaiDataSet.M_LoginMsgRow dr = LoginMsgClass.newM_LoginMsgRow();
            if (bSinki)
            {
                // �V�K��                
                // �o�^��
                dr.TourokuBi = DateTime.Now;
            }
            else
            {
                // �X�V��              
                // �X�V��
                dr.KoushinBi = DateTime.Now;
            }
           
            // ���b�Z�[�W
            dr.Msg = this.TbxMsg.Text;
            
            if (this.RbtnYukou.Checked)
            {
                dr.DelFlg = false;
            }
            else
            {
                dr.DelFlg = true;
            }
            return dr;
        }

        // �o�^�{�^���N���b�N
        protected void BtnTS_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.M_LoginMsgRow dr = this.CreateRow(true);
            if (dr == null)
                return;

            LibError err = LoginMsgClass.M_LoginMsg_Insert(dr, Global.GetConnection());
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
            m2mKoubaiDataSet.M_LoginMsgRow dr1 = this.CreateRow(false);
            if (dr1 == null)
                return;

            LibError err1 = LoginMsgClass.M_LoginMsg_Update(VsMsgID, dr1, Global.GetConnection());
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
