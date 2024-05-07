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

namespace m2mKoubai.Shiiresaki
{
    public partial class KenshuInfoForm : Core.Web.ServerViewStatePage
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


      //  private const int G_CELL_I = 0;
        private const int G_CELL_NO = 0;
        private const int G_CELL_BUHIN_KUBUN = 1;
        private const int G_CELL_BUHIN_CODE = 2;
        private const int G_CELL_BUHIN_NAME = 3;
        private const int G_CELL_CHUMON_SUURYOU = 4;
        private const int G_CELL_TANI = 5;
        private const int G_CELL_TANKA = 6;
        private const int G_CELL_CHUMON_KINGAKU = 7;
        private const int G_CELL_NOUNYUU_BASHO = 8;
        private const int G_CELL_UKEIREBI = 9;
        private const int G_CELL_NYUKA_SUURYOU = 10;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Shiiresaki)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }

                CtlTabShiire tab = FindControl("Tab") as CtlTabShiire;
                tab.Menu = CtlTabShiire.MainMenu.Kensyu_Jyouhou;

                       
                
                DateTime dtNow = DateTime.Now;                
                int nYear = dtNow.Year;
                // ���N
                DdlYear.Items.Add(nYear.ToString());
                // ���N
                nYear--;
                DdlYear.Items.Add(nYear.ToString());
                // �ė��N
                nYear--;
                DdlYear.Items.Add(nYear.ToString());               
                // ���N��I������
                DdlYear.SelectedValue = dtNow.Year.ToString();
                // ��
                DdlMonth.SelectedIndex = dtNow.Month;
                //
                ListSet.SetDdlJigyoushoKubun(SessionManager.UserKubun, DdlJigyoshoKubun);
                
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
            //Img
            this.Img1.Style.Add("display", "none");

            // �����{�^��
            BtnK.Attributes["onclick"] = "Kensaku();";

            // ��������            
            this.BtnKI.Attributes["onclick"] = string.Format("Kenshu('{0}');", HidKeyKen.Value);
            // ������
            this.BtnSI.Attributes["onclick"] = string.Format("Seikyu('{0}');", HidKeyKen.Value);
          
            // �s���ύX
            this.DdlRow.Attributes["onchange"] = "RowChange(); return false;";

            // ----- �J�����_�[ -----
            //CtlUkeireBi.SharedCalendar = this.SC;

        }

        /*
        private void SetList()
        {
            // �d����
            ListSet.SetDdlShiiresaki(DdlShiire);
            // ���i�敪
            ListSet.SetDdlBuhinKubun_C(DdlBuhinKubun);
            // ���i
            this.DdlBuhinKubun.Attributes["onchange"] = "OnBuhin(); return false";

        }
        */

        private void Create()
        {
            // hidden
            this.HidChkID.Value = "";
            this.HidKey.Value = "";
            

            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");

            // �d�����Џ����擾
            m2mKoubaiDataSet.M_ShiiresakiRow drShiire =
               ShiiresakiClass.getM_ShiiresakiRow(SessionManager.KaishaCode, Global.GetConnection());
            if (drShiire == null)
            {
                this.ShowMsg("", true);
                this.ShowTblMain(false);
                return;
            }
            int nYear = int.Parse(this.DdlYear.SelectedValue);
            int nMonth  = int.Parse(this.DdlMonth.SelectedValue);
            int nFromDate = 0;
            int nToDate = 0;

            AppCommon.CreateKikan(nYear,nMonth, drShiire.ShiharaiShimebi, ref nFromDate, ref nToDate);
            KenshuClass.KensakuParam k = this.GetKensakuParam(nFromDate, nToDate);
            if (k == null)
            {
                this.ShowMsg("", true);
                this.ShowTblMain(false);
                return;
            }
            HidKeyKen.Value = k._NouhinYearMonth;
            //
            KenshuDataSet.V_KenshuDataTable dt =
                KenshuClass.getV_KenshuDataTable(k, Global.GetConnection());

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

            G.Attributes.Add("bordercolor", "#e1e1c8");
        }



        private KenshuClass.KensakuParam GetKensakuParam(int nfrom, int nto)
        {


            KenshuClass.KensakuParam k = new KenshuClass.KensakuParam();
            /*
            // ����No
            if (TbxHacchuNo.Text != "")
            {
                k._HacchuNo = TbxHacchuNo.Text;
            }
            // �d����
            if (DdlShiire.SelectedIndex > 0)
            {
                k._SCode = DdlShiire.SelectedValue;
            }

            // ���i�敪
            if (DdlBuhinKubun.SelectedIndex > 0)
            {
                k._Kubun = DdlBuhinKubun.SelectedValue;
            }
            // ���i
            if (DdlBuhin.SelectedIndex > 0)
            {
                k._BuhinCode = DdlBuhin.SelectedValue;
            }

            // �����
            Common.CtlNengappiFromTo ctlUkeirebi = FindControl("CtlUkeireBi") as Common.CtlNengappiFromTo;
            if (ctlUkeirebi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._UkeireBi = ctlUkeirebi.GetNengappiKikan();
            }
            */
            k._FromDate = nfrom.ToString();
            k._ToDate = nto.ToString();
            // �[�i�N��
            k._NouhinYearMonth = this.DdlYear.SelectedValue + (int.Parse(this.DdlMonth.SelectedValue)).ToString("00");
            //
            if (DdlJigyoshoKubun.SelectedIndex > 0)
            {
                k._JigyoushoKubun = int.Parse(DdlJigyoshoKubun.SelectedValue);
            }
            // �d����
            k._SCode = SessionManager.KaishaCode;

            return k;
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
            TblRow.Visible = b;
            this.BtnKI.Visible = b;
            this.BtnSI.Visible = b;
           
        }

        // �y�[�W�`�F���W
        private void OnPageIndexChanged(int nNewPageIndex)
        {
            VsCurrentPageIndex = nNewPageIndex;
            this.Create();
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];

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
                /*
                case "ddlBuhin":
                    ListSet.SetDdlBuhin_C(this.DdlBuhinKubun.SelectedValue, this.DdlBuhin);
                    this.Ram.AjaxSettings.AddAjaxSetting(Ram, this.DdlBuhin);
                    break;
                */

            }

        }

        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {        
                KenshuDataSet.V_KenshuRow dr =
                  ((DataRowView)e.Row.DataItem).Row as KenshuDataSet.V_KenshuRow;
             
                ChumonClass.ChumonKey key = new ChumonClass.ChumonKey(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);               

                // ����NO              
                (e.Row.FindControl("LitHacchuuNo") as Literal).Text =
                    Utility.LinkToHacchuuNo(key.ToString(), dr.HacchuuNo);
                // ������
                (e.Row.FindControl("LitHacchuuBi") as Literal).Text = dr.HacchuuBi.ToString("yy/MM/dd");

                // �d����R�[�h
                //e.Row.Cells[G_CELL_SHIIRE_CODE].Text = dr.ShiiresakiCode;
                // �d���於
                //e.Row.Cells[G_CELL_SHIIRE_NAME].Text = dr.ShiiresakiMei;
                // �i�ڃO���[�v
                e.Row.Cells[G_CELL_BUHIN_KUBUN].Text = dr.BuhinKubun;
                // ���i�R�[�h
                e.Row.Cells[G_CELL_BUHIN_CODE].Text = dr.BuhinCode;
                // ���i�ږ�
                e.Row.Cells[G_CELL_BUHIN_NAME].Text = dr.BuhinMei;
                // ��������
                e.Row.Cells[G_CELL_CHUMON_SUURYOU].Text = dr.ChumonSuuryou.ToString("#,##0");
                // �P��
                e.Row.Cells[G_CELL_TANI].Text = dr.Tani;
                // �P��
                e.Row.Cells[G_CELL_TANKA].Text = "\\" + dr.Tanka.ToString("#,##0.#0");
                // �������z
                //decimal Kingaku = Math.Floor(dr.Suuryou * dr.Tanka);
                e.Row.Cells[G_CELL_CHUMON_KINGAKU].Text = "\\" + dr.Kingaku.ToString("#,##");
                // �[���ꏊ
                e.Row.Cells[G_CELL_NOUNYUU_BASHO].Text = dr.BashoMei;
                // ��t��  
                e.Row.Cells[G_CELL_UKEIREBI].Text += dr.NouhinBi.ToString("yy/MM/dd");
                       
                // ���א��� 
                e.Row.Cells[G_CELL_NYUKA_SUURYOU].Text += dr.NouhinSuuryou.ToString("#,##0");
            }
        }


       
    }
}