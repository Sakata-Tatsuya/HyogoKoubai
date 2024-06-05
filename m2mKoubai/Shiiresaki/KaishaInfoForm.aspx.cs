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

namespace m2mKoubai.Shiiresaki
{
    public partial class KaishaInfoForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Shiiresaki)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                //CtlTabShiire tab = FindControl("Tab") as CtlTabShiire;
                //tab.Menu = CtlTabShiire.MainMenu.Kaisha_Jyouhou;
                CreateKoushin();
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
            // �X�V�{�^��
            this.BtnK.Attributes["onclick"] = "Koushin(); return false;";
            // Img
            this.Img1.Style.Add("display", "none");
        }

        private void ShowMsg(string strmsg, bool bErrorMsg)
        {
            LblMsg.ForeColor = (bErrorMsg) ? Color.Red : Color.Black;
            this.LblMsg.Text = strmsg;
        }

        private void CreateKoushin()
        {
            m2mKoubaiDataSet.M_ShiiresakiRow drShiiresaki =
                 ShiiresakiClass.GetV_SHiiresakiRow(SessionManager.KaishaCode, Global.GetConnection());

            // �d����R�[�h
            this.LitCode.Text = drShiiresaki.ShiiresakiCode;
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
            // �������`
            this.TbxKouzameigi.Text = drShiiresaki.KouzaMeigi;
            // ���Z�@�֖�
            this.TbxKinyuukikanMei.Text = drShiiresaki.KinyuuKikanMei;
            // �����ԍ�
            this.TbxKouzaBangou.Text = drShiiresaki.KouzaBangou;

        }

        private m2mKoubaiDataSet.M_ShiiresakiRow CreateRow(bool b)
        {
            m2mKoubaiDataSet.M_ShiiresakiRow dr = ShiiresakiClass.newM_ShiiresakiRow();

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
            // �������`
            dr.KouzaMeigi = this.TbxKouzameigi.Text;
            // ���Z�@�֖�
            dr.KinyuuKikanMei = this.TbxKinyuukikanMei.Text;
            // �����ԍ�
            dr.KouzaBangou = this.TbxKouzaBangou.Text;

            return dr;
        }


        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];


            if (strCmd == "koushin")
            {
                m2mKoubaiDataSet.M_ShiiresakiRow dr = this.CreateRow(true);
                if (dr == null)
                    return;
                LibError err = ShiiresakiClass.M_ShiiresakiInfo_Update(SessionManager.KaishaCode, dr, Global.GetConnection());
                if (err != null)
                {
                    this.ShowMsg(err.Message, true);
                    return;
                }
                else
                {
                    // ��ЃR�[�h�Ɖ�Ж����擾
                    m2mKoubaiDataSet.M_ShiiresakiRow drShiire =
                        ShiiresakiClass.getM_ShiiresakiRow(SessionManager.KaishaCode, Global.GetConnection());

                    // ���O�C��ID�ɂ���āA���[���A�h���X���擾
                    ChumonDataSet.V_Chumon_MailDataTable dtMail =
                        ChumonClass.getV_Chumon_Mail_KaishaInfoDataTable(SessionManager.LoginID, SessionManager.KaishaCode, Global.GetConnection());

                    for (int i = 0; i < dtMail.Rows.Count; i++)
                    {
                        // CC
                        ChumonDataSet.V_MailToCCDataTable dtCC =
                            ChumonClass.getV_MailToCCDataTable(dtMail[i].JigyoushoKubun, dtMail[i].LoginID, Global.GetConnection());

                        string[] aryTocc = new string[dtCC.Rows.Count];
                        for (int j = 0; j < dtCC.Rows.Count; j++)
                        {
                            aryTocc[j] = dtCC[j].Mail;

                        }

                        MailClass.MailParam p = this.GetMailParam(dtMail[i], drShiire);

                        MailClass.SendMail(p, aryTocc);
                    }
                }
                this.ShowMsg("�X�V���܂���", false);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblAll);
            }

        }

        private MailClass.MailParam GetMailParam
            (ChumonDataSet.V_Chumon_MailRow drInfo, m2mKoubaiDataSet.M_ShiiresakiRow drShiire)
        {

            MailClass.MailParam p = new MailClass.MailParam();
            // ���M�����[���A�h���X
            p._MailFrom = drInfo.FromMail;
            // ���M�惁�[���A�h���X
            p._MailTo = drInfo.ToMail;
            // ����
            p._Subject = "��Џ�񂪍X�V����܂���";
            // �{��
            p._Body = MailClass.GetBody_KaishaInfo_Koushin(drInfo, drShiire);
            // SMTP
            p._SMTP_Server = Global.SMTP_Server;
            return p;
        }
    }
}
