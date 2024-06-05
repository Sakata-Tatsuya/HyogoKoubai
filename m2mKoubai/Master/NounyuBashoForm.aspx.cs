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
    public partial class NounyuBashoForm : System.Web.UI.Page
    {
        private const int G_CELL_SAKUJO = 0;
        private const int G_CELL_KOUSHIN = 1;
        private const int G_CELL_NOUNYUU_BASHO_CODE = 2;
        private const int G_CELL_NOUNYUU_BASHO_MEI = 3;

        private int VsCurrentPageIndex
        {
            get
            {
                object obj = this.ViewState["page_index"];
                if (null == obj) return 0;
                return Convert.ToInt32(obj);
            }
            set
            {
                this.ViewState["page_index"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Owner)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }

                //CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                //tab.Menu = CtlTabMain.MainMenu.Master;
                //tab.MasterMenu = CtlTabMain.Master.NounyuBasho;

                // �ŏ��͔�\��
                this.ShowTblMain(false);
                this.Create();
              
            }
            Common.CtlMyPager pagerTop = (Common.CtlMyPager)this.FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)this.FindControl("Pb");
            pagerTop.OnPageIndexChanged += new Common.CtlMyPager.CtlMyPagerEventHandler(this.OnPageIndexChanged);
            pagerBottom.OnPageIndexChanged += new Common.CtlMyPager.CtlMyPagerEventHandler(this.OnPageIndexChanged);
            pagerTop.ClientEvent = pagerBottom.ClientEvent = "PageChange";
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
            // �s���ύX
            this.DdlRow.Attributes["onchange"] = "Row(); return false;";
            // �V�K�{�^��
            this.BtnNew.Attributes["onclick"] = "Shinki(); return false;";
            //�@����
            this.BtnK.Attributes["onclick"] = "Kensaku(); return false;";
            // �폜(input)
            this.BtnS.Attributes["onclick"] = "Delete(); return false;";
            //Img
            this.Img1.Style.Add("display", "none");
            // �����̂ݓ��͉�
            AppCommon.NumOnly(this.TbxNCode);

        }
      
        private void OnPageIndexChanged(int nNewPageIndex)
        {
            VsCurrentPageIndex = nNewPageIndex;
            this.Create();
        }


        // �N���G�[�g
        private void Create()
        {
            // Hid_Clear
            this.HidChkID.Value = "";
            this.HidThisID.Value = "";

            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");


            // ���i�f�[�^���擾����
            m2mKoubaiDataSet.M_NounyuuBashoDataTable dt =
                NounyuuBashoClass.getM_NounyuuBashoDataTable(this.GetKensakuParam(), Global.GetConnection());

            this.ShowMsg(dt.Rows.Count + "��", false);
            if (dt.Rows.Count == 0)
            {
                pagerTop.DdlClear();
                pagerBottom.DdlClear();
                this.ShowTblMain(false);
                return;
            }
            else
            {
                this.ShowTblMain(true);
            }

            // �y�[�W���O            
            int nPageSize = AloowPaging();
            int nPageCount = 0;

            if (nPageSize > 0)
            {
                G.PageSize = nPageSize;
                G.AllowPaging = true;
                nPageCount = dt.Rows.Count / nPageSize;
                if (0 < dt.Rows.Count % nPageSize) nPageCount++;
                if (nPageCount <= VsCurrentPageIndex)
                    VsCurrentPageIndex = 0;

                // ���݂̕\���s(���s�ځ`���s��)
                int nStartCount = nPageSize * VsCurrentPageIndex + 1;
                int nEndCount = nStartCount + nPageSize - 1;
                if (nEndCount > dt.Rows.Count)
                    nEndCount = dt.Rows.Count;
                pagerTop.SetItemCounter(nStartCount, nEndCount);
                pagerBottom.SetItemCounter(nStartCount, nEndCount);
            }
            else
            {
                G.PageSize = dt.Rows.Count;
                G.AllowPaging = false;
                VsCurrentPageIndex = 0;
            }
            G.PageIndex = VsCurrentPageIndex;
            pagerTop.Create(nPageCount);
            pagerBottom.Create(nPageCount);
            pagerTop.CurrentPageIndex = pagerBottom.CurrentPageIndex = G.PageIndex;

            G.DataSource = dt;
            G.DataBind();
            G.EnableViewState = false;
        }

        private int AloowPaging()
        {
            try
            {
                return int.Parse(this.DdlRow.SelectedValue);
            }
            catch
            {
                return -1;
            }
        }
        // ��������
        private NounyuuBashoClass.KensakuParam GetKensakuParam()
        {
            NounyuuBashoClass.KensakuParam k = new NounyuuBashoClass.KensakuParam();

            // �[���ꏊ�R�[�h
            if (TbxNCode.Text != "")
            {
                k._BashoCode = TbxNCode.Text;
            }
            // �[���ꏊ��
            if (TbxNMei.Text != "")
            {
                k._BashoName = TbxNMei.Text;
            }
           
            return k;
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
            TblRow.Visible = b;
            this.BtnS.Visible = b;
        }

        // 
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // �폜�`�F�b�N
                HtmlInputCheckBox chkH = e.Row.FindControl("ChkH") as HtmlInputCheckBox;
                chkH.Attributes["onclick"] = "DelChk(this.checked)";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                m2mKoubaiDataSet.M_NounyuuBashoRow dr =
                    ((DataRowView)e.Row.DataItem).Row as m2mKoubaiDataSet.M_NounyuuBashoRow;
                // �폜
                HtmlInputCheckBox chk = e.Row.FindControl("ChkI") as HtmlInputCheckBox;
               
                // chkID
                if (HidChkID.Value != "") this.HidChkID.Value += ",";
                this.HidChkID.Value += chk.ClientID;
                // ��L�[                
                if (HidThisID.Value != "") this.HidThisID.Value += ",";
                this.HidThisID.Value += dr.BashoCode;
                // �X�V�{�^��
                HtmlInputButton btn = e.Row.FindControl("BK") as HtmlInputButton;
                btn.Attributes["onclick"] =
                    string.Format("Update('{0}'); return false; ", dr.BashoCode);
                // �[���ꏊ�R�[�h
                e.Row.Cells[G_CELL_NOUNYUU_BASHO_CODE].Text = dr.BashoCode.ToString();
                // �[���ꏊ��
                e.Row.Cells[G_CELL_NOUNYUU_BASHO_MEI].Text = dr.BashoMei;                
              
            }
        }



        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];

            if (strCmd == "page")
            {
                // �y�[�W�؂�ւ�
                this.VsCurrentPageIndex = int.Parse(strArgs[1]);
                this.Create();
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
               
            }
            else if (strCmd == "kensaku")
            {
                // ����
                this.VsCurrentPageIndex = 0;
                this.Create();
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
            }
            else if (strCmd == "row")
            {
                // �s���ύX
                this.Create();
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
               
            }
            else if (strCmd == "delete")
            {
                // �폜
                string[] strDelKeyAry = strArgs[1].Split(',');
                for (int i = 0; i < strDelKeyAry.Length; i++)
                {
                    string strCode = strDelKeyAry[i];
                    LibError err = NounyuuBashoClass.M_NounyuuBasho_Delete(strCode, Global.GetConnection());
                    if (err != null)
                    {
                        this.ShowMsg(err.Message, false);
                        return;
                    }
                }
                this.Create(); // �ĕ\��               
                this.ShowMsg("�폜���܂���", false);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
               
            }
        }






    }
}
