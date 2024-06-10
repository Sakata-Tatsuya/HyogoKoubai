using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using m2mKoubaiDAL;

namespace m2mKoubai
{
    public partial class ChoboListForm : System.Web.UI.Page
    {
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
        private string VsKaishaCode
        {
            get
            {
                return Convert.ToString(this.ViewState["VsKaishaCode"]);
            }
            set
            {
                this.ViewState["VsKaishaCode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M.MenuName = "�������";
                VsKaishaCode = string.Empty;
                if (SessionManager.UserKubun != (byte)UserKubun.Owner)
                {
                    VsKaishaCode = SessionManager.KaishaCode;
                }
                // �������X�g�쐬
                this.SetList();
                if (VsKaishaCode != string.Empty)
                {
                    DdlKaisha.SelectedValue = VsKaishaCode;
                    DdlKaisha.Enabled = false;
                }
            }
            this.Create();
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
            //Img
            this.Img1.Style.Add("display", "none");

            // �����{�^��
            //BtnK.Attributes["onclick"] = "Kensaku();";
            // �s���ύX
            this.DdlRow.Attributes["onchange"] = "RowChange(); return false;";
        }


        // �y�[�W�`�F���W
        private void OnPageIndexChanged(int nNewPageIndex)
        {
            VsCurrentPageIndex = nNewPageIndex;
            this.Create();
        }
        // �N���G�[�g
        private void Create()
        {
            HidKey.Value = "";
            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");

            FilesClass.KensakuParam k = this.GetKensakuParam();
            if (k == null)
            {
                this.ShowMsg("", true);
                return;

            }
            ShareDataSet.V_DocumentDataTable dt = FilesClass.getV_DocumentDataTable(k, Global.GetConnection());

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

            //�y�[�W���O            
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
        }

        private void SetList()
        {
            // ���[���
            LoadDdl("DataType", VsKaishaCode, DdlDataType);
            // �����
            LoadDdl("KaishaCode,KaishaMei", VsKaishaCode, DdlKaisha);
        }
        private FilesClass.KensakuParam GetKensakuParam()
        {
            FilesClass.KensakuParam k = new FilesClass.KensakuParam();
            // ���[�U�[�敪
            k._userKubun = (byte)SessionManager.UserKubun;
            // �����
            k._KaishaCode = "";
            // ���[���
            k._DataType = "";
            // ���s��
            Common.CtlNengappiFromTo CtlTourokuBi = FindControl("CtlTourokuBi") as Common.CtlNengappiFromTo;
            if (CtlTourokuBi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._TourokuBi = CtlTourokuBi.GetNengappiKikan();
            }
            return k;
        }
        protected void BtnKensaku_Click(object sender, EventArgs e)
        {
            Create();
        }


        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ShareDataSet.V_DocumentRow dr = ((DataRowView)e.Row.DataItem).Row as ShareDataSet.V_DocumentRow;
                Label LblDataType = e.Row.FindControl("LblDataType") as Label;
                Label LblSlipID = e.Row.FindControl("LblSlipID") as Label;
                Label LblKaisha = e.Row.FindControl("LblKaisha") as Label;
                Label LblTourokuBi = e.Row.FindControl("LblTourokuBi") as Label;
                ImageButton BtnDisp = e.Row.FindControl("BtnDisp") as ImageButton;

                //���[���
                LblDataType.Text = dr.DataType;
                //���[�ԍ�
                LblSlipID.Text = dr.SlipID;
                BtnDisp.CommandArgument = dr.FileID.ToString();
                //�����
                LblKaisha.Text = dr.KaishaMei;
                //���s��
                LblTourokuBi.Text = dr.TourokuBi.ToString("yyyy/MM/dd");
            }
        }
        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];
            LibError err = null;
 
            switch (strCmd)
            {
                case "page":

                    // �y�[�W�؂�ւ�
                    this.VsCurrentPageIndex = int.Parse(strArgs[1]);
                    this.Create();
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    break;

                case "kensaku":
                    // ����
                    this.VsCurrentPageIndex = 0;
                    this.Create();
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    break;

                case "row":
                case "reload":
                    // �s���ύX
                    this.Create();
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    break;
            }
        }
        protected void G_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Command = e.CommandName;

            switch (Command)
            {
                //�������ԍ������ŏڍׂ��J���Ƃ�
                case "detail":
                    //VsInvoiceID = (string)e.CommandArgument;

                    ////�������ڍׂ�����
                    //CreateDetail(VsInvoiceID);
                    //Showpdf();
                    break;
                //PDF�A�C�R�����N���b�N������
                case "disp":
                    string strFileID = (string)e.CommandArgument;
                    HidFileID.Value = strFileID;
                    break;
            }
        }

        public static void LoadDdl(string strKoumoku,string strKaishaCode, DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("", ""));

            DataTable dt = FilesClass.getV_DocunemtColumnLoadDataTable(strKoumoku, strKaishaCode, Global.GetConnection());

            string[] strKoumokuArray = strKoumoku.Split(',');

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (strKoumokuArray.Length > 1)
                    ddl.Items.Add(new ListItem(dt.Rows[i][strKoumokuArray[0]].ToString() + ":" + dt.Rows[i][strKoumokuArray[1]].ToString(), dt.Rows[i][strKoumokuArray[0]].ToString()));
                else
                    ddl.Items.Add(new ListItem(dt.Rows[i][strKoumokuArray[0]].ToString(), dt.Rows[i][strKoumokuArray[0]].ToString()));
            }
        }






































    }
}