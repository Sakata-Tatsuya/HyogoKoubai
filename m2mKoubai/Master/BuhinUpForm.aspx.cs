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
    public partial class BuhinUpForm : System.Web.UI.Page
    {
        /*
        // ���i�敪
        private string VsKubun
        {
            get { return Convert.ToString(this.ViewState["VsKubun"]); }
            set { this.ViewState["VsKubun"] = value; }
        }
        */
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
                    this.SetList();
                    
                }
                else
                {
                    try
                    {
                        //string[] strKey = Request.QueryString["key"].Split('_');
                        string strKey = Request.QueryString["key"];
                        //this.VsKubun = strKey[0];
                        // Code
                        this.VsCode = strKey;
                    }
                    catch
                    {
                        this.ShowTblMain(false);
                        this.ShowMsg(AppCommon.NO_DATA, true);
                        return;
                    }

                    this.SetList();
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
            //AppCommon.NumOnly(this.TbxCode);
            //AppCommon.NumOnly(this.TbxLeadTime);

            // �o�^�{�^��(submit)
            this.BtnTS.Style.Add("display", "none");
            // �X�V�{�^��(submit)
            this.BtnKS.Style.Add("display", "none");
        }

        public void SetList()
        {
            //ListSet.SetDdlTani(DdlTani);
            ListSet.SetDdlLeadTime(DdlLeadTime);

            // �d����1
            ListSet.SetDdlShiiresaki(DdlShiire1);
            // �d����2
            ListSet.SetDdlShiiresaki(DdlShiire2);

            // �Ȗږ�
            ListSet.SetKamokuMei(DdlKamoku);

            // ��p�Ȗږ�
            ListSet.SetHiyouMei(DdlHiyou);

            // �⏕�Ȗږ�
            ListSet.SetHojyoMei(DdlHojyo);
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
            m2mKoubaiDataSet.M_BuhinRow dr =
                BuhinClass.getM_BuhinRow(VsCode, Global.GetConnection());
            if (dr == null)
            {
                this.ShowTblMain(false);
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            // �i�ڃO���[�v
            TbxKubun.Text = dr.BuhinKubun;
            // �i�ڃR�[�h            
            LitCode.Text = dr.BuhinCode;
            // �i�ږ�
            TbxHinmei.Text = dr.BuhinMei;
            // �P��
            if (dr.Tanka != 0)
            {
                this.TbxTanka.Text = dr.Tanka.ToString();             
            }
            // �P��
            TbxTani.Text = dr.Tani;
            // ���b�g
            if (dr.Lot != 0)
                this.TbxLot.Text = dr.Lot.ToString();
            // ���[�h�^�C��
            if (dr.LT_Suuji != 0 && dr.LT_Tani != 0)
            {
                TbxLeadTime.Text = dr.LT_Suuji.ToString();
                DdlLeadTime.SelectedValue = dr.LT_Tani.ToString();
            }
            // �d����
            DdlShiire1.SelectedValue = dr.ShiiresakiCode1;
            if (dr.ShiiresakiCode2 != "")
                DdlShiire2.SelectedValue = dr.ShiiresakiCode2;

            // �Ȗ�
            DdlKamoku.SelectedValue = dr.KanjyouKamokuCode.ToString();            
            // ��p
            DdlHiyou.SelectedValue = dr.HiyouKamokuCode.ToString();
            // �⏕
            DdlHojyo.SelectedValue = dr.HojyoKamokuNo.ToString();

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
                //                
                TbxCode.Visible = b; 
            }
        }




        // Row�쐬
        private m2mKoubaiDataSet.M_BuhinRow CreateRow(bool bShinki)
        {
            m2mKoubaiDataSet.M_BuhinRow dr = BuhinClass.newM_BuhinRow();

            // �R�[�h
            if (bShinki)�@
            {      
                // �V�K�o�^                
                dr.BuhinCode = TbxCode.Text;
            }
            else 
            {
                // �X�V                
                dr.BuhinCode = LitCode.Text;
            }
            // �i�ڃO���[�v
            dr.BuhinKubun = TbxKubun.Text;            
            // �i�ږ�
            dr.BuhinMei = TbxHinmei.Text;
            // �P��              
            if (TbxTanka.Text != "")
                dr.Tanka = decimal.Parse(this.TbxTanka.Text);
            else
                dr.Tanka = 0;
            // �P��            
            dr.Tani = TbxTani.Text;
            // ���b�g
            if (this.TbxLot.Text != "")
                dr.Lot = int.Parse(this.TbxLot.Text);
            else
            {
                dr.Lot = 0;
            }
            // ���[�h�^�C��
            if (this.TbxLeadTime.Text != "" && this.DdlLeadTime.SelectedIndex > 0)
            {
                dr.LT_Suuji = int.Parse(TbxLeadTime.Text);
                dr.LT_Tani = byte.Parse(DdlLeadTime.SelectedValue);
            }
            else
            {
                dr.LT_Suuji = 0;
                dr.LT_Tani = 0;
            }

            // �d����1
            dr.ShiiresakiCode1 = this.DdlShiire1.SelectedValue;
            // �d����2
            if (this.DdlShiire2.SelectedIndex > 0)
            {
                dr.ShiiresakiCode2 = this.DdlShiire2.SelectedValue;
            }
            else
            {
                dr.ShiiresakiCode2 = "";
            }

            // �Ȗ�
            dr.KanjyouKamokuCode = int.Parse(this.DdlKamoku.SelectedValue);
            // ��p
            dr.HiyouKamokuCode = int.Parse(this.DdlHiyou.SelectedValue);
            // �⏕
            dr.HojyoKamokuNo = int.Parse(this.DdlHojyo.SelectedValue);

            return dr;
        }
        // �V�K�o�^�d���`�F�b�N
        private bool TourokuCheck(m2mKoubaiDataSet.M_BuhinRow dr)
        {
            // �f�[�^�d���`�F�b�N
            m2mKoubaiDataSet.M_BuhinRow drChk =
                BuhinClass.getM_BuhinRow(dr.BuhinCode, Global.GetConnection());
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
            m2mKoubaiDataSet.M_BuhinRow dr = this.CreateRow(true);
            if (dr == null)
                return;
            if (!TourokuCheck(dr))
                return;
            LibError err = BuhinClass.M_Buhin_Insert(dr, Global.GetConnection());
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
            m2mKoubaiDataSet.M_BuhinRow dr1 = this.CreateRow(false);
            if (dr1 == null)
                return;

            LibError err1 = BuhinClass.M_Buhin_Update(VsCode, dr1, Global.GetConnection());
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
