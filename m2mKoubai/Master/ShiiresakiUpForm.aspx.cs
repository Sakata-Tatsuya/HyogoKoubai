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
    public partial class ShiiresakiUpForm : System.Web.UI.Page
    {

        private string VsShiiresakiCode
        {
            get
            {
                return Convert.ToString(this.ViewState["VsShiiresakiCode"]);
            }
            set
            {
                this.ViewState["VsShiiresakiCode"] = value;
            }
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
                    this.Create_Shinki();

                }
                else
                {
                    try
                    {
                        // Code
                        this.VsShiiresakiCode = Request.QueryString["key"];
                    }
                    catch
                    {
                        this.ShowTblMain(false);
                        this.ShowMsg(AppCommon.NO_DATA, true);
                        return;
                    }

                    // �X�V
                    this.CreateKoushin();
                }

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
            this.BtnT.Attributes["onclick"] = "Touroku(); return false; ";
            // �X�V�{�^��
            this.BtnK.Attributes["onclick"] = "Koushin(); return false; ";
            // ����{�^��
            this.BtnC.Attributes["onclick"] = "Close(); return false; ";
            
            // �o�^�{�^��(submit)
            this.BtnTouroku.Style.Add("display", "none");
            // �X�V�{�^��(submit)
            this.BtnKoushin.Style.Add("display", "none");
        }

        private void ShowMsg(string strMsg, bool bErrorMsg)
        {
            this.LblMsg.ForeColor = (bErrorMsg) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
            this.LblMsg.Text = strMsg;
        }

        // �V�K�o�^
        private void Create_Shinki()
        {
            this.ShinkiTouroku(true);
            this.ShowTblMain(true);
        }

        private void CreateKoushin()
        {
            this.ShinkiTouroku(false);
            this.ShowTblMain(true);

            m2mKoubaiDataSet.M_ShiiresakiRow drShiiresaki =
                ShiiresakiClass.GetV_SHiiresakiRow(VsShiiresakiCode, Global.GetConnection());

            if (drShiiresaki == null)
            {
                this.ShowMsg("�d���悪������܂���ł���", true);
                this.ShowTblMain(false);
                return;
            }

            // �d����R�[�h
           //this.TbxShiireCode.Text = drShiiresaki.ShiiresakiCode;
            this.LitCode.Text =  drShiiresaki.ShiiresakiCode;
            // �d���於
            this.TbxShiireName.Text = drShiiresaki.ShiiresakiMei;
            // �X�֔ԍ�            
            this.TbxYuubin.Text = drShiiresaki.YubinBangou;
            // �Z��
            this.TbxJyusho.Text = drShiiresaki.Address;
            // TEL
            this.TbxTel.Text = drShiiresaki.Tel;
            // FAX
            this.TbxFax.Text = drShiiresaki.Fax;
            // �U����
           // this.TbxFurikomisaki.Text = drShiiresaki.FurikomiSaki;
            // �������`
            this.TbxKouzameigi.Text = drShiiresaki.KouzaMeigi;
            // ���Z�@�֖�
            this.TbxKinyuukikanMei.Text = drShiiresaki.KinyuuKikanMei;
            // �����ԍ�
            this.TbxKouzaBangou.Text = drShiiresaki.KouzaBangou;
            // �x������
            this.DdlShimebi.SelectedValue = drShiiresaki.ShiharaiShimebi.ToString();
            // �x���\���1
            this.DdlYotebi1.SelectedValue = AppCommon.YoteibiDdl1Select(drShiiresaki.ShiharaiYoteibi);
            // �x���\���2
            this.DdlYotebi2.SelectedValue = AppCommon.YoteibiDdl2Select(drShiiresaki.ShiharaiYoteibi);
            // ���������J
            if (drShiiresaki.KensyukoukaiFlg == true)
                this.RbtnKoukai.Checked = true;
            else
                this.RbtnKoukaiNashi.Checked = true;
            // �[���񓚍Ñ����[��
            if (drShiiresaki.SaisokuMailFlg == true)
                this.RbtnSoushin.Checked = true;
            else
                this.RbtnSoushinNashi.Checked = true;
            // �d������X�V����
            if (drShiiresaki.KousinKyokaFlg)
                this.RbtnKyoka.Checked = true;
            else
                this.RbtnKyokaNashi.Checked = true;
        }

        // �o�^�A�X�V�{�^���\��
        private void ShinkiTouroku(bool b)
        {
            // �o�^            
            BtnT.Visible = b;
            // �X�V            
            BtnK.Visible = !b;
            if (!b)
            {
                TbxShiireCode.Visible = b;
            }
        }

        private void ShowTblMain(bool b)
        {
            this.TblMain.Visible = b;
            for (int i = 0; i < TblMain.Controls.Count; i++)
                TblMain.Controls[i].EnableViewState = b;
            this.TblBtn.Visible = b;
        }


        private m2mKoubaiDataSet.M_ShiiresakiRow CreateRow(bool b)
        {
            m2mKoubaiDataSet.M_ShiiresakiRow dr = ShiiresakiClass.newM_ShiiresakiRow();
            
            //�d����R�[�h
            dr.ShiiresakiCode = this.TbxShiireCode.Text;
            // �d���於
            dr.ShiiresakiMei = this.TbxShiireName.Text;
            // �X�֔ԍ�
            dr.YubinBangou = this.TbxYuubin.Text;
            // �Z��
            dr.Address = this.TbxJyusho.Text;
            // TEL
            dr.Tel = this.TbxTel.Text;
            // FAX
            dr.Fax = this.TbxFax.Text;
            // �U����
           // dr.FurikomiSaki = this.TbxFurikomisaki.Text;
            // �������`
            dr.KouzaMeigi = this.TbxKouzameigi.Text;
            // ���Z�@�֖�
            dr.KinyuuKikanMei = this.TbxKinyuukikanMei.Text;
            // �����ԍ�
            dr.KouzaBangou = this.TbxKouzaBangou.Text;
            // �x������
            dr.ShiharaiShimebi = int.Parse(DdlShimebi.SelectedValue);
                //AppCommon.ShiharaiShimebiUp(DdlShimebi.SelectedValue);
            // �x���\���
            dr.ShiharaiYoteibi = AppCommon.ShiharaiYoteibiUp(DdlYotebi1.SelectedValue + DdlYotebi2.SelectedValue);
            //�������ڍ�
            if (this.RbtnKoukai.Checked)
            {
                dr.KensyukoukaiFlg = true;
            }
            else
            {
                dr.KensyukoukaiFlg = false;
            }
            //�[���񓚍Ñ����[��
            if (this.RbtnSoushin.Checked)
            {
                dr.SaisokuMailFlg = true;
            }
            else
            {
                dr.SaisokuMailFlg = false;
            }
            //�d������X�V����
            if (this.RbtnKyoka.Checked)
            {
                dr.KousinKyokaFlg = true;
            }
            else
            {
                dr.KousinKyokaFlg = false;
            }

            return dr;
        }

        // �V�K�o�^�d���`�F�b�N
        private bool TourokuCheck(m2mKoubaiDataSet.M_ShiiresakiRow dr)
        {
            // �f�[�^�d���`�F�b�N
            m2mKoubaiDataSet.M_ShiiresakiRow drChk =
                ShiiresakiClass.getM_ShiiresakiRow(dr.ShiiresakiCode, Global.GetConnection());
            if (drChk != null)
            {
                this.ShowMsg("�f�[�^���d�����Ă��܂�", true);
                return false;
            }

            return true;
        }
        // �o�^�{�^���N���b�N
        protected void BtnTouroku_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.M_ShiiresakiRow dr = this.CreateRow(true);
            if (dr == null)
                return;
            if (!TourokuCheck(dr))
                return;
            LibError err = ShiiresakiClass.M_Shiiresaki_Insert(dr, Global.GetConnection());
            if (err != null)
            {
                this.ShowMsg(err.Message, true);
                return;
            }

            this.ShowMsg("�o�^���܂���", false);

            loadFlg = 1;
        }

        // �X�V�{�^���N���b�N
        protected void BtnKoushin_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.M_ShiiresakiRow dr1 = this.CreateRow(false);
            if (dr1 == null)
                return;

            LibError err1 = ShiiresakiClass.M_Shiiresaki_Update(VsShiiresakiCode, dr1, Global.GetConnection());
            if (err1 != null)
            {
                this.ShowMsg(err1.Message, true);
                return;
            }
            //CreateKoushin();
            this.ShowMsg("�X�V���܂���", false);

            loadFlg = 1;
        }

       
    }
}
