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
    public partial class BuhinForm : Core.Web.ServerViewStatePage
    {
        private const int G_CELL_SAKUJO = 0;
        private const int G_CELL_KOUSHIN = 1;
        private const int G_CELL_BUHIN_KUBUN = 2;
        private const int G_CELL_BUHIN_CODE = 3;
        private const int G_CELL_BUHIN_MEI = 4;
        private const int G_CELL_TANKA = 5;
        private const int G_CELL_TANI = 6;
        private const int G_CELL_LOT = 7;
        private const int G_CELL_LT = 8;
        private const int G_CELL_SHIIRE = 9;

        private const int G_CELL_Kamoku = 10;
        private const int G_CELL_Hiyou = 11;
        private const int G_CELL_Hojyo = 12;
        

        

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
            if (!this.IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Yodoko) // Yodoko�̂ݕ\����
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                tab.Menu = CtlTabMain.MainMenu.Master;
                tab.MasterMenu = CtlTabMain.Master.Buhin;

                // �ŏ��͔�\��
                this.ShowTblMain(false);
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
            this.PreRender += new System.EventHandler(this.BuhinForm_PreRender);
        }

        private void BuhinForm_PreRender(object sender, EventArgs e)
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

        }
        private void SetList()
        {
           // �i�ڃ��X�g
           ListSet.SetDdlBuhin(DdlHinmoku);
            //ListSet.SetDdlBuhin(DdlHinmei);
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
            // Hid_Clear
            this.HidChkID.Value = "";
            this.HidThisID.Value = "";

            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");


            // ���i�f�[�^���擾����
            BuhinDataSet.V_Buhin_MasterDataTable dt =
                BuhinClass.getV_Buhin_MasterDataTable(this.GetKensakuParam(), Global.GetConnection());
            
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
        private BuhinClass.KensakuParam GetKensakuParam()
        {
            BuhinClass.KensakuParam k = new BuhinClass.KensakuParam();
           
            // �R�[�h
            if (DdlHinmoku.SelectedIndex > 0)
            {
                //string[] strkey = DdlCode.SelectedValue.Split('_');
                //k._Kubun = strkey[0];
                //k._Code = strkey[1];
                k._Code = DdlHinmoku.SelectedValue;
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
         
        }

        int nRowNo = 0;
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // �폜�`�F�b�N
                HtmlInputCheckBox chkH = e.Row.Cells[G_CELL_SAKUJO].FindControl("ChkH") as HtmlInputCheckBox;
                chkH.Attributes["onclick"] = "DelChk(this.checked)";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BuhinDataSet.V_Buhin_MasterRow dr =
                    ((DataRowView)e.Row.DataItem).Row as BuhinDataSet.V_Buhin_MasterRow;
                // �폜
                HtmlInputCheckBox chk = e.Row.FindControl("ChkI") as HtmlInputCheckBox;
                //BuhinClass.BuhinKey key =
                //    new BuhinClass.BuhinKey(dr.BuhinCode);
                chk.Value = dr.BuhinCode;
                // chkID
                if (HidChkID.Value != "") this.HidChkID.Value += ",";
                this.HidChkID.Value += chk.ClientID;
                // ��L�[                
                //if (HidThisID.Value != "") this.HidThisID.Value += ",";
                if (nRowNo > 0) this.HidThisID.Value += ",";
                this.HidThisID.Value += chk.Value;
                
                // �X�V�{�^��
                HtmlInputButton btn = e.Row.FindControl("BK") as HtmlInputButton;
                btn.Attributes["onclick"] =
                    string.Format("Update('{0}'); return false; ", chk.Value);
                // ���i�敪
                e.Row.Cells[G_CELL_BUHIN_KUBUN].Text = dr.BuhinKubun;
                // �i�ڃR�[�h
                e.Row.Cells[G_CELL_BUHIN_CODE].Text = dr.BuhinCode;                
                // �i�ږ�
                e.Row.Cells[G_CELL_BUHIN_MEI].Text = dr.BuhinMei;
                // �P��
                if (dr.Tanka != 0)
                    e.Row.Cells[G_CELL_TANKA].Text = "\\" + dr.Tanka.ToString("#,##0.00");
                // �P��                
                e.Row.Cells[G_CELL_TANI].Text = dr.Tani;
                // ���b�g
                if (dr.Lot != 0)
                    e.Row.Cells[G_CELL_LOT].Text = dr.Lot.ToString();
                // ���[�h�^�C��
                if (dr.LT_Suuji != 0 &&  dr.LT_Tani != 0)
                    e.Row.Cells[G_CELL_LT].Text = dr.LT_Suuji.ToString() + AppCommon.LT_Tani(dr.LT_Tani); 
                // �d����1
                if (!dr.IsShiiresakiMei1Null())
                    ((Literal)e.Row.Cells[G_CELL_SHIIRE].FindControl("LitShiire1")).Text = dr.ShiiresakiMei1;                    
                // �d����1
                if (!dr.IsShiiresakiMei2Null() && dr.ShiiresakiCode2 != "")
                    ((Literal)e.Row.Cells[G_CELL_SHIIRE].FindControl("LitShiire2")).Text = dr.ShiiresakiMei2;

                // ����Ȗ�
                if (dr.KanjyouKamokuCode != 0)
                {
                    ((Literal)e.Row.Cells[G_CELL_Kamoku].FindControl("LitKamokuCode")).Text = dr.KanjyouKamokuCode.ToString();
                    ((Literal)e.Row.Cells[G_CELL_Kamoku].FindControl("LitKamokuMei")).Text = AppCommon.KamokuMei(dr.KanjyouKamokuCode);
                }
                // ��p����Ȗ�
                if (dr.HiyouKamokuCode != 0)
                {
                    ((Literal)e.Row.Cells[G_CELL_Kamoku].FindControl("LitHiyouCode")).Text = dr.HiyouKamokuCode.ToString();
                    ((Literal)e.Row.Cells[G_CELL_Kamoku].FindControl("LitHiyouMei")).Text = AppCommon.HiyouMei(dr.HiyouKamokuCode);
                }
                // �⏕����Ȗ�
                if (dr.HojyoKamokuNo != 0)
                {
                    ((Literal)e.Row.Cells[G_CELL_Kamoku].FindControl("LitHojyoCode")).Text = dr.HojyoKamokuNo.ToString();
                    ((Literal)e.Row.Cells[G_CELL_Kamoku].FindControl("LitHojyoMei")).Text = AppCommon.HojyoMei(dr.HojyoKamokuNo);
                }


                nRowNo++;
            }
        }


        



        protected void Ram_AjaxRequest(object sender, Telerik.WebControls.AjaxRequestEventArgs e)
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
                //this.SetList();
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                //this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DdlHinmoku);
            }
            else if (strCmd == "Reload")
            {
                // �s���ύX
                this.SetList();
                this.Create();                
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DdlHinmoku);
            }
            else if (strCmd == "delete")
            {
                // �폜
                string[] strDelKeyAry = strArgs[1].Split(',');
                for (int i = 0; i < strDelKeyAry.Length; i++)
                {
                    //string[] strKey = strDelKeyAry[i].Split('_');
                    LibError err = BuhinClass.M_Buhin_Delete(strDelKeyAry[i], Global.GetConnection());
                    if (err != null)
                    {
                        this.ShowMsg(err.Message, false);
                        return;
                    }
                }
                this.Create(); // �ĕ\��    
                this.SetList();
                this.ShowMsg("�폜���܂���", false);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DdlHinmoku);
               
            }
        }






    }
}
