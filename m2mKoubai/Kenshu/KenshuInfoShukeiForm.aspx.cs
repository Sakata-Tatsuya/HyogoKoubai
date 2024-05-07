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

namespace m2mKoubai.Kenshu
{
    public partial class KenshuInfoShukeiForm : System.Web.UI.Page
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

        private const int G_CELL_SHIIRE = 0;
        private const int G_CELL_GOUKEI = 1;
        private const int G_CELL_ZEIGAKU = 2;
        private const int G_CELL_SOUGOUKEI = 3;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Owner)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                tab.Menu = CtlTabMain.MainMenu.Kenshu_Jyouhou;
                tab.KenshuMenu = CtlTabMain.Kenshu_Jyouhou.Shukei;

                // �������X�g�쐬
                this.SetList();
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

            // �s���ύX
            this.DdlRow.Attributes["onchange"] = "RowChange(); return false;";

            // ----- �J�����_�[ -----
            CtlDate.SharedCalendar = this.SC;

        }


        private void SetList()
        {
            // �d����
            ListSet.SetDdlShiiresaki_K(DdlShiire);

        }
        // �ۑ��p�d����R�[�h
        string strShiireCode = "";
        // �ۑ��p���v���z
        decimal nGoukei = 0;
        decimal nGoukei_Tax = 0;
        // �ۑ��p�����v���z
        decimal nSouGoukei = 0;
        private void Create()
        {
            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");

            KenshuClass.KensakuParam k = this.GetKensakuParam();
            if (k == null)
            {
                this.ShowMsg("", true);
                return;
            }
            // �S�Ă̌������f�[�^�擾
            KenshuDataSet.V_Kenshu_ShukeiDataTable dt =
                KenshuClass.getV_Kenshu_ShukeiDataTable(k, Global.GetConnection());

            // G.DataBind()
            KenshuDataSet.Kenshu_ShukeiBindDataTable dtBind =
                new KenshuDataSet.Kenshu_ShukeiBindDataTable();


            if (dt.Rows.Count == 0)
            {
                this.ShowMsg(dt.Rows.Count + "��", false);
                pagerTop.DdlClear();
                pagerBottom.DdlClear();
                this.ShowTblMain(false);
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    decimal dKingaku = dt[i].Suuryou * dt[i].Tanka;
                    decimal dKingaku_Round = Math.Round(dKingaku, 0, MidpointRounding.AwayFromZero);
                    if (strShiireCode != dt[i].ShiiresakiCode)
                    {

                        nGoukei = 0;

                        // ���őΉ� �؂�̂Ăł͂Ȃ��l�̌ܓ��ɕύX
                        //nGoukei =(int)Math.Floor(dt[i].Suuryou * dt[i].Tanka);
                        nGoukei = dKingaku_Round;
                        //nGoukei_Tax = Math.Round(dKingaku_Round * dt[i].Zeiritu / 100, 0, MidpointRounding.AwayFromZero);
                        nGoukei_Tax = dKingaku_Round * dt[i].Zeiritu / 100;

                        strShiireCode = dt[i].ShiiresakiCode;
                    }
                    else
                    {
                        // ���őΉ� �؂�̂Ăł͂Ȃ��l�̌ܓ��ɕύX
                        //nGoukei += (int)Math.Floor(dt[i].Suuryou * dt[i].Tanka);

                        nGoukei += dKingaku_Round;
                        //nGoukei_Tax += Math.Round(dKingaku_Round * dt[i].Zeiritu / 100, 0, MidpointRounding.AwayFromZero);
                        nGoukei_Tax += dKingaku_Round * dt[i].Zeiritu / 100;
                    }

                    if ((i + 1 == dt.Rows.Count) || (i + 1 < dt.Rows.Count && strShiireCode != dt[i + 1].ShiiresakiCode))
                    {

                        KenshuDataSet.Kenshu_ShukeiBindRow drBind =
                            dtBind.NewKenshu_ShukeiBindRow();


                        drBind.ShiiresakiCode = dt[i].ShiiresakiCode;
                        drBind.ShiiresakiMei = dt[i].ShiiresakiMei;
                        drBind.Goukei = nGoukei.ToString("#,##0");

                        // ����ŗ�
                        //decimal dZeiRitsu = (decimal.Parse(Global.ShouhiZei) / 100);
                        // �ŗ�
                        //int nShohizei = (int)Math.Floor(nGoukei * dZeiRitsu);
                        //drBind.Zeigaku = nShohizei.ToString("#,##0");

                        /* 2014/04/07 �Ή�
                         * ����ō��v�̌v�Z�𖾍ז��̏���Ŋz��
                         * ���Z������Ɏl�̌ܓ�����悤�ύX
                         */
                        nGoukei_Tax = Math.Round(nGoukei_Tax, 0, MidpointRounding.AwayFromZero);

                        drBind.Zeigaku = nGoukei_Tax.ToString("#,##0");
                        // �����v
                        //drBind.SouGoukei = (nGoukei + nShohizei).ToString("#,##0");
                        drBind.SouGoukei = (nGoukei + nGoukei_Tax).ToString("#,##0");

                        //nSouGoukei += nGoukei + nShohizei; 
                        nSouGoukei += nGoukei + nGoukei_Tax;
                        dtBind.AddKenshu_ShukeiBindRow(drBind);
                    }

                }

                this.ShowMsg(dtBind.Rows.Count + "��", false);
                this.ShowTblMain(true);
            }
            // ���v���z
            LblKingaku.Text = "\\" + nSouGoukei.ToString("#,##0");

            //�y�[�W���O            
            int nPageSize = AloowPaging();
            int nPageCount = 0;
            if (nPageSize > 0)
            {
                G.PageSize = nPageSize;
                G.AllowPaging = true;
                nPageCount = dtBind.Rows.Count / nPageSize;
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
                G.PageSize = dtBind.Rows.Count;
                G.AllowPaging = false;
                VsCurrentPageIndex = 0;
            }
            G.PageIndex = VsCurrentPageIndex;
            pagerTop.Create(nPageCount);
            pagerBottom.Create(nPageCount);
            pagerTop.CurrentPageIndex = pagerBottom.CurrentPageIndex = G.PageIndex;

            G.DataSource = dtBind;
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
        private KenshuClass.KensakuParam GetKensakuParam()
        {
            KenshuClass.KensakuParam k = new KenshuClass.KensakuParam();

            // �N��
            Common.CtlNengappiFromTo ctlDate = FindControl("CtlDate") as Common.CtlNengappiFromTo;
            if (ctlDate.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._UkeireBi = ctlDate.GetNengappiKikan();
            }
            // �d����
            if (DdlShiire.SelectedIndex > 0)
            {
                k._SCode = DdlShiire.SelectedValue;
            }
            // �C���K�v
            // ����ȖڃR�[�h 09/07/22�ǉ�
            if (TbxKanjyouKamokuCode.Text != "")
            {
                k._KanjyouKamokuCode = int.Parse(TbxKanjyouKamokuCode.Text);
            }

            // ��p�ȖڃR�[�h 09/07/22�ǉ�
            if (TbxHiyouKamokuCode.Text != "")
            {
                k._HiyouKamokuCode = int.Parse(TbxHiyouKamokuCode.Text);
            }

            // �⏕�Ȗ�No 09/07/22�ǉ�
            if (TbxHojyoKamokuNo.Text != "")
            {
                k._HojyoKamokuNo = int.Parse(TbxHojyoKamokuNo.Text);
            }
            // ���Ə��敪�i�ǉ�09-07-29 ���j
            k._JigyoushoKubun = SessionManager.JigyoushoKubun;
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
            TblGoukei.Visible = b;

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
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                    break;

                case "kensaku":
                    // ����
                    this.VsCurrentPageIndex = 0;
                    this.Create();
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                    break;

                case "row":
                case "reload":

                    // �s���ύX
                    this.Create();
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.LblMsg);
                    break;
            }

        }

        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                KenshuDataSet.Kenshu_ShukeiBindRow dr =
                    ((DataRowView)e.Row.DataItem).Row as KenshuDataSet.Kenshu_ShukeiBindRow;

                //
                e.Row.Cells[G_CELL_SHIIRE].Text = dr.ShiiresakiCode + ':' + dr.ShiiresakiMei;

                e.Row.Cells[G_CELL_GOUKEI].Text = "\\" + dr.Goukei;

                e.Row.Cells[G_CELL_ZEIGAKU].Text = "\\" + dr.Zeigaku;

                e.Row.Cells[G_CELL_SOUGOUKEI].Text = "\\" + dr.SouGoukei;
            }
        }
    }
}
