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
    public partial class OrderInfoForm : System.Web.UI.Page
    {
        private const int G_CELL_HACCHUUSYO = 0;
        private const int G_CELL_NOUHINPYOU = 1;
        private const int G_CELL_GENHINPYOU = 2;
        private const int G_CELL_CANCEL = 3;
        private const int G_CELL_NO = 4;
        private const int G_CELL_BUHIN = 5;
        private const int G_CELL_SUURYOU = 6;
        private const int G_CELL_CHUMON_KINGAKU = 7;
        private const int G_CELL_TANI = 8;
        private const int G_CELL_NOUNYUU_BASHO = 9;
        private const int G_CELL_HACCHUU_TANTOUSHA = 10;
        private const int G_CELL_NOUKI_HENKOU = 11;
        private const int G_CELL_NOUKI_KAITOU = 12;
        private const int G_CELL_NOUHINBI = 13;
        private const int G_CELL_MSG = 14;

        protected int cell_index = G_CELL_NOUKI_KAITOU;
        Core.Collection.StringCollections m_objStringCols = new Core.Collection.StringCollections();

        DataView m_dvNoukiHenkou = null;
        m2mKoubaiDataSet.T_NoukiHenkouDataTable m_dtNoukiHenkou = null;

        DataView m_dvKaitouNouki = null;
        m2mKoubaiDataSet.T_NoukiKaitouDataTable m_dtKaitouNouki = null;

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


        bool b = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Shiiresaki)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                M.MenuName = "�󒍏��";
                //CtlTabShiire tab = FindControl("Tab") as CtlTabShiire;
                //tab.Menu = CtlTabShiire.MainMenu.Jyuchuu_Jyouhou;

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
            
            this.BtnAH.Attributes["onclick"] = "NKC_OPEN(); return false;";
            this.BtnAC.Attributes["onclick"] = "NKC_CLOSE(); return false;";
            this.BtnAT.Attributes["onclick"] = "NKC_REG(null); return false;";

            BtnHacchuusho.Visible = b;
            BtnNouhinsho.Visible = b;
            BtnGenhinpyou.Visible = b;

            // ���
            this.BtnHacchuusho.Attributes["onclick"] = string.Format("Print('{0}')", "������");
            this.BtnNouhinsho.Attributes["onclick"] = string.Format("Print('{0}')", "�[�i��");
            this.BtnGenhinpyou.Attributes["onclick"] = string.Format("Print('{0}')", "���i�[");

            //Img
            this.Img1.Style.Add("display", "none");

            // �����{�^��
            BtnK.Attributes["onclick"] = "Kensaku();";

          
            // ���P���œo�^����Ă���f�[�^�́A�[�i������ł��Ȃ��i�ǉ��@09-07-31 ���j
            if (G.Rows.Count > 0 && this.HidChkID_N.Value == "")
            {
                HtmlInputCheckBox chk =
                    G.HeaderRow.Cells[G_CELL_NOUHINPYOU].FindControl("ChkH_N") as HtmlInputCheckBox;

                chk.Visible = !b;
            }
            // �s���ύX
            this.DdlRow.Attributes["onchange"] = "RowChange(); return false;";
            // ----- �J�����_�[ -----
            CtlHacchuubi.SharedCalendar = CtlNouki.SharedCalendar = CtlKNouki.SharedCalendar = CtlNouhinbi.SharedCalendar = this.SC;
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
            this.m_dtNoukiHenkou = NoukiHenkouClass.getT_NoukiHenkouDataTable(Global.GetConnection());
            this.m_dvNoukiHenkou = new DataView(this.m_dtNoukiHenkou);
            this.m_dvNoukiHenkou.Sort = "HenkouNo DESC ";

            this.m_dtKaitouNouki = NoukiKaitouClass.getT_NoukiKaitouDataTable(Global.GetConnection());
            this.m_dvKaitouNouki = new DataView(this.m_dtKaitouNouki);
            this.m_dvKaitouNouki.Sort = "KaitouNo DESC ";

            // Hid           
            this.HidChkID_N.Value = "";
            this.HidChkID_H.Value = "";
            this.HidChkID_G.Value = "";

            Common.CtlMyPager pagerTop = (Common.CtlMyPager)FindControl("Pt");
            Common.CtlMyPager pagerBottom = (Common.CtlMyPager)FindControl("Pb");

            ChumonClass.KensakuParam k = this.GetKensakuParam();
            if (k == null)
            {
                this.ShowMsg("", true);
                return;

            }
            ChumonDataSet.V_Chumon_JyouhouDataTable dt =
                ChumonClass.getV_Chumon_JyouhouDataTable(k, Global.GetConnection());

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

            G.Attributes.Add("bordercolor", "#e1e1c8"); //


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
            this.TblI.Visible = b;

        }

        private void SetList()
        {

            //�@�[���ꏊ
            ListSet.SetDdlNounyuuBasho_C(DdlNBasho);
            // �[���񓚏�
            ListSet.SetDdlNoukiKaitouJyoukyou(DdlNKJyoukyou, SessionManager.UserKubun);
            // �[�i��
            ListSet.SetDdlNouhinJyoukyou(DdlNJyoukyou);
            ListSet.SetDdlNoukiJyoukyou(DdlNoukiHenkou);
            // ���i�敪
            ListSet.SetDdlBuhinKubun_C(DdlBuhinKubun, SessionManager.KaishaCode);

            // ���i
            this.DdlBuhinKubun.Attributes["onchange"] = "OnBuhin(); return false";
            // �����S����
            ListSet.SetDdlHacchuTantousha_C(DdlHacchuTantousha);
            // ���b�Z�[�W
            ListSet.SetDdlMsg(DdlMsg);
            // �L�����Z����
            ListSet.SetDdlCancel(DdlCancel);
        �@�@//
            ListSet.SetDdlJigyoushoKubun(SessionManager.UserKubun, DdlJigyoshoKubun);
        }
        private ChumonClass.KensakuParam GetKensakuParam()
        {
            ChumonClass.KensakuParam k = new ChumonClass.KensakuParam();
            k._Cancelbi = 0;
            // ���[�U�[�敪
            k._userKubun = (byte)SessionManager.UserKubun;
            // ����No
            if (TbxHacchuNo.Text != "")
            {
                k._HacchuNo = TbxHacchuNo.Text;
            }
            // �d����
            k._SCode = SessionManager.KaishaCode;

            // �[���ꏊ
            if (DdlNBasho.SelectedIndex > 0)
            {
                k._NBasho = DdlNBasho.SelectedValue;
            }
            // �[���񓚏�
            if (DdlNKJyoukyou.SelectedIndex > 0)
            {
                k._NkJyoukyou = int.Parse(DdlNKJyoukyou.SelectedValue);
            }
            // �[�i��
            if (DdlNJyoukyou.SelectedIndex > 0)
            {
                k._NHJyoukyou = int.Parse(DdlNJyoukyou.SelectedValue);
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
            // �����S����
            if (DdlHacchuTantousha.SelectedIndex > 0)
            {
                k._TantoushaCode = DdlHacchuTantousha.SelectedValue;
            }
            if (DdlNoukiHenkou.SelectedIndex > 0)
            {
                k._NhJyoukyou = int.Parse(DdlNoukiHenkou.SelectedValue);
            }
            // �[��
            Common.CtlNengappiFromTo ctlNouki = FindControl("CtlNouki") as Common.CtlNengappiFromTo;
            if (ctlNouki.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._Nouki = ctlNouki.GetNengappiKikan();
            }

            // ������
            Common.CtlNengappiFromTo ctlHacchuubi = FindControl("CtlHacchuubi") as Common.CtlNengappiFromTo;
            if (ctlHacchuubi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._Hacchuubi = ctlHacchuubi.GetNengappiKikan();
            }
            // �[�i��
            Common.CtlNengappiFromTo ctlNouhinbi = FindControl("CtlNouhinbi") as Common.CtlNengappiFromTo;
            if (ctlNouhinbi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._NouhinBi = ctlNouhinbi.GetNengappiKikan();
            }
            // �񓚔[��
            Common.CtlNengappiFromTo ctlKaitouNouki = FindControl("CtlKNouki") as Common.CtlNengappiFromTo;
            if (ctlKaitouNouki.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._KaitouNouki = ctlKaitouNouki.GetNengappiKikan();
            }

            // ���b�Z�[�W
            k._Msg = int.Parse(DdlMsg.SelectedValue);
            // �L�����Z��
            k._Cancelbi = int.Parse(DdlCancel.SelectedValue);
            if (k._Cancelbi == 0)
                b = true;

            if (DdlJigyoshoKubun.SelectedIndex > 0)
            {
                k._JigyoushoKubun = int.Parse(DdlJigyoshoKubun.SelectedValue);
            }
            return k;
        }


        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                HtmlInputCheckBox chkH_H = e.Row.FindControl("ChkH_H") as HtmlInputCheckBox;
                HtmlInputCheckBox chkH_N = e.Row.FindControl("ChkH_N") as HtmlInputCheckBox;
                HtmlInputCheckBox chkH_G = e.Row.FindControl("ChkH_G") as HtmlInputCheckBox;

                chkH_H.Visible = b;
                chkH_N.Visible = b;
                chkH_G.Visible = b;
                if (b)
                {
                    chkH_H.Attributes["onclick"] = "ChkAll_H(this.checked)";
                    chkH_N.Attributes["onclick"] = "ChkAll_N(this.checked)";
                    chkH_G.Attributes["onclick"] = "ChkAll_G(this.checked)";
                }
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ChumonDataSet.V_Chumon_JyouhouRow dr = ((DataRowView)e.Row.DataItem).Row as ChumonDataSet.V_Chumon_JyouhouRow;
                m_objStringCols["C"].Add(dr.Year + '_' + dr.HacchuuNo + '_' + dr.JigyoushoKubun);
                ChumonClass.ChumonKey key = new ChumonClass.ChumonKey(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);               

                // ���������
                HtmlInputCheckBox chkH = e.Row.FindControl("ChkH") as HtmlInputCheckBox;
                // �[�i�����
                HtmlInputCheckBox chkN = e.Row.FindControl("ChkN") as HtmlInputCheckBox;
                //�@���i�[���
                HtmlInputCheckBox chkG = e.Row.FindControl("ChkG") as HtmlInputCheckBox;

                if (HidChkID_H.Value != "") HidChkID_H.Value += ",";
                HidChkID_H.Value += chkH.ClientID;

                // ���P���̏ꍇ�A�[�i���̈���ł��Ȃ�
                if (dr.KaritankaFlg)
                {
                    chkN.Visible = false;
                }
                else
                {
                    if (HidChkID_N.Value != "") HidChkID_N.Value += ",";
                    HidChkID_N.Value += chkN.ClientID;
                }

                if (HidChkID_G.Value != "") HidChkID_G.Value += ",";
                HidChkID_G.Value += chkG.ClientID;

                chkH.Value = key.ToString();
                chkN.Value = key.ToString();
                chkG.Value = key.ToString();
                // �L�����Z��
                if (!dr.IsCancelBiNull())
                {
                    //
                    chkH.Visible = false;
                    chkN.Visible = false;
                    chkG.Visible = false;
                    e.Row.Cells[G_CELL_CANCEL].Text = "�L����<br>�Z��";
                    e.Row.Cells[G_CELL_CANCEL].ForeColor = System.Drawing.Color.Red;
                }

                Literal LitHacchuuNo = e.Row.FindControl("LitHacchuuNo") as Literal;
                Literal LitHacchuuBi = e.Row.FindControl("LitHacchuuBi") as Literal;
                Literal LitHacchuuTantoushaCode = e.Row.FindControl("LitHacchuuTantoushaCode") as Literal;
                Literal LitHacchuuTantoushaMei = e.Row.FindControl("LitHacchuuTantoushaMei") as Literal;
                //Literal LitShiireCode = e.Row.FindControl("LitShiireCode") as Literal;
                //Literal LitShiireName = e.Row.FindControl("LitShiireName") as Literal;
                Literal LitCode = e.Row.FindControl("LitCode") as Literal;
                Literal LitName = e.Row.FindControl("LitName") as Literal;
                Literal LitSuuryou = e.Row.FindControl("LitSuuryou") as Literal;
                Literal LitTanka = e.Row.FindControl("LitTanka") as Literal;
                Literal LitChumonKingaku = e.Row.FindControl("LitChumonKingaku") as Literal;
                Literal LitZeigaku = e.Row.FindControl("LitZeigaku") as Literal;
                Label LblNouki = e.Row.FindControl("LblNouki") as Label;
                HtmlInputButton btnM = e.Row.FindControl("BM") as HtmlInputButton;

                // ����NO
                LitHacchuuNo.Text = Utility.LinkToHacchuuNo(key.ToString(), dr.HacchuuNo);
                // ������
                LitHacchuuBi.Text = dr.HacchuuBi.ToString("yy/MM/dd");
                // �����S���҃R�[�h                
                LitHacchuuTantoushaCode.Text = dr.TantoushaCode;
                // �����S���Җ�
                LitHacchuuTantoushaMei.Text = dr.Name;
                // ���i�R�[�h
                LitCode.Text = dr.BuhinKubun + dr.BuhinCode;
                // ���i�ږ�
                LitName.Text = dr.BuhinMei;
                // ����
                LitSuuryou.Text = dr.Suuryou.ToString("#,##0");
                // �P��
                if (!dr.KaritankaFlg)
                {
                    LitTanka.Text = "\\" + dr.Tanka.ToString("#,##0.#0");
                }
                else
                {
                    LitTanka.Text = string.Format("<font color =\"red\">{0}</font>", "(��) \\" + dr.Tanka.ToString("#,##0.#0"));
                }
                // �������z
                if (!dr.KaritankaFlg)
                {
                    LitChumonKingaku.Text = "\\" + dr.Kingaku.ToString("#,##0");
                }
                else
                {
                    LitChumonKingaku.Text = string.Format("<font color =\"red\">{0}</font>", "\\" + dr.Kingaku.ToString("#,##0"));
                }
                decimal Zeigaku = Math.Round((decimal)dr.Kingaku * dr.Zeiritu / 100, 0, MidpointRounding.AwayFromZero);
                LitZeigaku.Text = "\\" + Zeigaku.ToString("#,##0");
                // �P��
                e.Row.Cells[G_CELL_TANI].Text = dr.Tani;
                // �[���ꏊ
                e.Row.Cells[G_CELL_NOUNYUU_BASHO].Text = dr.BashoMei;
                // �[��
                DataView dvNouki = this.m_dvNoukiHenkou;
                dvNouki.RowFilter =
                    string.Format("Year = '{0}' AND HacchuuNo = '{1}' AND JigyoushoKubun = '{2}' ", dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);

                NoukiHenkouInfo infoNoki = null;
                // �ۑ��p�[��
                string strNoukiHenkou = "";
                if (dr.IsHenkouNoNull())
                {
                    this.GetNoukiHenkouNullInfo(dr, out infoNoki, out strNoukiHenkou);
                }
                else if (!dr.IsHenkouShouninFlgNull() && dr.HenkouShouninFlg)
                {
                    // �ύX�[�������F�ς̏ꍇ
                    this.GetNoukiHenkouInfo(dr, m_dvNoukiHenkou, out infoNoki, true, out strNoukiHenkou);
                }
                else
                {
                    // �ύX�[���������F�̏ꍇ
                    this.GetMiNoukiHenkouInfo(dr, m_dvNoukiHenkou, out infoNoki, true, out strNoukiHenkou);
                }

                e.Row.Cells[G_CELL_NOUKI_HENKOU].Text = infoNoki.strNoukiHenkouHtml;

                // �񓚔[��
                // �[���񓚂̂��ׂăf�[�^�擾
                DataView dvKaitouNouki = this.m_dvKaitouNouki;

                dvKaitouNouki.RowFilter =
                    string.Format("Year = '{0}' AND HacchuuNo = '{1}' AND JigyoushoKubun = '{2}' ",
                                dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);

                NoukiKaitouInfo infoKaitou = null;             

                // �ŐV�̉񓚔[�������F�̏ꍇ
                if (!dr.IsKaitouShouninFlgNull() && dr.KaitouShouninFlg)
                    this.GetKaitouNoukiInfo(dr, m_dvKaitouNouki, out infoKaitou, true);
                else
                    // �ŐV�̉񓚔[���������F�̏ꍇ
                    this.GetMiKaitouNoukiInfo(dr, m_dvKaitouNouki, out infoKaitou, true);

                CtlNoukiKaitou k = this.LoadControl("CtlNoukiKaitou.ascx") as CtlNoukiKaitou;
                k.ID = "NKM_" + dr.Year + "_" + dr.HacchuuNo + "_" + dr.JigyoushoKubun;

                this.DivNoukiKaitou.Controls.Add(k);

                // �w��[���{�^���N���b�N
                HtmlInputButton btnSN = (HtmlInputButton)e.Row.FindControl("BtnSN");
              
                btnSN.Attributes["onclick"] = string.Format("ShiteiNouki('{0}_{1}_{2}'); return false;",
                    dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);

               // �[���񓚐��ʂ��������ʂ�菭�Ȃ����A�񓚔[�����[�����x�ꂽ�ꍇ�A�w�i�F�ύX
                if (!dr.IsKaitouNoNull())
                {
                    int nSuuryou = 0;
                    bool b = false;
                    m2mKoubaiDataSet.T_NoukiKaitouDataTable dtKaitou = NoukiKaitouClass.getT_NoukiKaitouDataTable(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, dr.KaitouNo, Global.GetConnection());

                    for (int i = 0; i < dtKaitou.Rows.Count; i++)
                    {
                        nSuuryou += dtKaitou[i].Suuryou;

                        string[] strNoukiAry = strNoukiHenkou.Split(',');
                        for (int j = 0; j < strNoukiAry.Length; j++)
                        {
                            int ChkNouki = 0;
                            int.TryParse(strNoukiAry[j].ToString(), out ChkNouki);

                            if (dtKaitou[i].Nouki > ChkNouki)
                            {
                                b = true;
                            }
                            else
                            {
                                b = false;
                            }
                        }
                       
                    }
                    if (nSuuryou < dr.Suuryou || b)
                    {
                        e.Row.Cells[G_CELL_NOUKI_KAITOU].BackColor = System.Drawing.Color.FromName("#FFFFA0");

                    }
                }

                // �����w��No�A�[���R���g���[��ID�A���ʃR���g���[��ID
                this.m_objStringCols["KaitouNoukiData"].Add(
                    string.Format("{0}_{1}_{2}\t{3}\t{4}\t{5}",
                    dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, k.HidNouki.ClientID, k.HidSuuryou.ClientID, k.HidKaitouNo.ClientID));

                this.DivNoukiKaitou.Controls.Clear();

                // �ŐV�̉񓚔[��
                Label lblNyuryoku = (Label)e.Row.Cells[G_CELL_NOUKI_KAITOU].FindControl("LblNyuryoku");
                lblNyuryoku.Text = infoKaitou.strNoukiKaitouHtml;

                // �[�i��
               
                if (!dr.IsNouhinNoNull())
                {
                    m2mKoubaiDataSet.T_NouhinDataTable dt =
                        NouhinClass_N.getT_NouhinDataTable(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, Global.GetConnection());
                     string strNouhinbi = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                       strNouhinbi +=
                            string.Format("{0} {1}:{2}<br>", dt[i].NouhinBi.ToString("yy/MM/dd"),
                                     "����", dt[i].Suuryou.ToString("#,##0"));
                    }
                    e.Row.Cells[G_CELL_NOUHINBI].Text = strNouhinbi;
                }

                // ���b�Z�[�W
                btnM.Attributes["onclick"] = string.Format("Msg('{0}');", key.ToString());

                if (!dr.IsOpenedFlgNull())
                {
                    if (dr.OpenedFlg) { 
                        btnM.Value = "����";
                    }
                    else if(dr.UserKubun != 0)
                    { 
                        btnM.Value = BtnMsgText(dr.UserKubun);
                    }
                    else
                    {
                        btnM.Value = "-----";
                    }
                }
                else
                {
                    btnM.Value = "-----";
                }
            }
            else
            {
                this.HidDataKey.Value = string.Join(",", this.m_objStringCols.Keys);
                this.HidData.Value = this.m_objStringCols.ToArrayString(":", ",");
            }
        }

        // �[���ύX���k���̏ꍇ
        private void GetNoukiHenkouNullInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, out NoukiHenkouInfo info, out string strNouki)
        {
            info = new NoukiHenkouInfo();

            info.strNoukiHenkouHtml = string.Format("{0}", Utility.FormatToyyMMdd(dr.Nouki.ToString()));

            strNouki = dr.Nouki;
        }
        // �[���ύX�������F�̏ꍇ
        private void GetMiNoukiHenkouInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, DataView dvHN, out NoukiHenkouInfo info, bool bCreateHenkou, out string strNouki)
        {
            info = new NoukiHenkouInfo();
            string[] strNoukiAry = null;
            string str = "";

            if (dvHN.Count > 0)
            {
                strNoukiAry = new string[dvHN.Count];

                // �ۑ��p�ύX�[��No
                int nHenkouNo = 0;

                // �ۑ��p�ύX�[��
                string strNouki1 = "";
                string strNouki2 = "";

                // ���ׂĂ̕ύX�[�����f�[�^�擾
                for (int i = 0; i < dvHN.Count; i++)
                {
                    if (i == 0 || nHenkouNo == int.Parse(dvHN[i]["HenkouNo"].ToString()))
                    {
                        // ����o�^�̏ꍇ�����ŕ\��
                        if (int.Parse(dvHN[i]["HenkouNo"].ToString()) == 1)
                            strNouki1 +=
                                    string.Format("{0} {1}:{2}<br> ", Utility.FormatToyyMMdd(dvHN[i]["Nouki"].ToString()),
                                    "����", dvHN[i]["Suuryou"].ToString());

                        else
                            // �ŐV�o�^�̏ꍇ�Ԏ��ŕ\��
                            strNouki1 +=
                                 string.Format("<font color = \"red\">{0} {1}:{2}<br>", Utility.FormatToyyMMdd(dvHN[i]["Nouki"].ToString()),
                                    "����", dvHN[i]["Suuryou"].ToString());

                        nHenkouNo = int.Parse(dvHN[i]["HenkouNo"].ToString());


                        if (dvHN.Count - 1 == i)
                        {
                            strNouki1 +=
                                         string.Format("({0} {1})<br> ",
                                         Utility.FormatToyyMMddHHmm(dvHN[i]["Tourokubi"].ToString()), "�o�^");
                        }
                        else if (i + 1 < dvHN.Count)
                        {
                            if (nHenkouNo != int.Parse(dvHN[i + 1]["HenkouNo"].ToString()))
                            {
                                strNouki1 +=
                                    string.Format("<font color = \"red\">({0} {1})</font><br> ",
                                    Utility.FormatToyyMMddHHmm(dvHN[i]["Tourokubi"].ToString()), "�o�^");
                            }
                        }
                        if (str != "") str += ",";
                        str += dvHN[i]["Nouki"].ToString();

                    }
                    else if (nHenkouNo - 1 == int.Parse(dvHN[i]["HenkouNo"].ToString()))
                    {
                        strNouki2 +=
                               string.Format("{0} {1}:{2}<br>", Utility.FormatToyyMMdd(dvHN[i]["Nouki"].ToString()),
                                                "����", dvHN[i]["Suuryou"].ToString());

                        if ((i > 0 && i == dvHN.Count - 1) || ((nHenkouNo - 1) != int.Parse(dvHN[i + 1]["HenkouNo"].ToString())))
                        {
                            strNouki2 +=
                                string.Format("({0} {1})<br> ",
                                Utility.FormatToyyMMddHHmm(dvHN[i]["Tourokubi"].ToString()), "�o�^");
                        }
                    }

                }

                if (strNouki1 != "" && strNouki2 != "")
                {
                    strNoukiAry[0] = strNouki2 + strNouki1;

                }
                else
                {
                    strNoukiAry[0] = strNouki1;

                }
                // �ύX�[��
                info.strNoukiHenkouHtml = Utility.ToInnerRowsHTML_NoLine(strNoukiAry);
            }
            else 
            {
                str = dr.Nouki;
            }
            strNouki = str;

            if (bCreateHenkou)
            {
                if (info.strNoukiHenkouHtml == "")
                    info.strNoukiHenkouHtml += string.Format("{0}<br>({1} {2})<br>", Utility.FormatToyyMMdd(dr.Nouki.ToString()),
                                     Utility.FormatToyyMMddHHmm(dr.HacchuuBi.ToString()), "�o�^");

                if (!dr.IsHenkouShouninFlgNull() && dr.HenkouNo >= 1)
                {
                    info.strNoukiHenkouHtml +=
                        string.Format("<input type=button class='f8' onclick=Shounin('{0}_{1}_{2}_{3}'); return false;  value={4} />",
                        dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, dr.HenkouNo, "���F");
                }
            }
        }
        // �[���ύX�����F�ς̏ꍇ
        private void GetNoukiHenkouInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, DataView dvHN,
                                        out NoukiHenkouInfo info, bool bCreateHenkou, out string strNouki)
        {
            info = new NoukiHenkouInfo();
            string[] strNoukiAry = null;
            strNoukiAry = new string[dvHN.Count];
             string str = "";
            if (0 < dvHN.Count)
            {

                // �ۑ��p��No
                int nHenkouNo = 0;
                // �ۑ��p�[����
                string strNoukiHenkou = "";

                for (int i = 0; i < dvHN.Count; i++)
                {
                    if (i == 0 || nHenkouNo == int.Parse(dvHN[i]["HenkouNo"].ToString()))
                    {
                        // �ŐV�o�^�̏ꍇ�Ԏ��ŕ\��
                        strNoukiHenkou +=
                              string.Format("{0} {1}:{2}<br>",
                                Utility.FormatToyyMMdd(dvHN[i]["Nouki"].ToString()), "����", dvHN[i]["Suuryou"].ToString());

                        // �ύXNo
                        nHenkouNo = int.Parse(dvHN[i]["HenkouNo"].ToString());

                        if (dvHN.Count - 1 == i || nHenkouNo != int.Parse(dvHN[i + 1]["HenkouNo"].ToString()))
                        {
                            strNoukiHenkou +=
                                         string.Format("({0} {1})<br> ",
                                         Utility.FormatToyyMMddHHmm(dvHN[i]["Tourokubi"].ToString()), "�o�^");
                        }
                       
                        if (str != "") str += ",";
                        str += dvHN[i]["Nouki"].ToString();

                    }
                    else
                        break;
                }

                strNoukiAry[0] = strNoukiHenkou;

            }
            info.strNoukiHenkouHtml = Utility.ToInnerRowsHTML_NoLine(strNoukiAry);
            strNouki = str;
        }

        // �񓚔[��
        private class NoukiKaitouInfo
        {
            public string strNoukiKaitouHtml = "";
            public string strShiteiNouki = "";
        }
        // �ύX�[��
        private class NoukiHenkouInfo
        {
            public string strNoukiHenkouHtml = "";
        }
        // �[�i��
        public class NouhinBiInfo
        {
            public string strNouhinbiHtml = "";
        }
        // ���b�Z�[�W�{�^���̃e�L�X�g�\��
        private string BtnMsgText(Byte mFlag)
        {
            string strText = "";
            if (mFlag == (byte)UserKubun.Shiiresaki)
                strText = "���M";
            else
                strText = "��M";
            return strText;
        }


        // �񓚔[������HTML���쐬�i�ꗗ�\�����j
        private void    GetMiKaitouNoukiInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, DataView dvNK, 
                                    out NoukiKaitouInfo info, bool bCreateKaitou)
        {
            info = new NoukiKaitouInfo();
            string[] strNKaitouAry = new string[dvNK.Count];

            if (0 < dvNK.Count)
            {
                // �ۑ��p��No
                int nKaitouNo = 0;
                // �ۑ��p�[����
                string strNoukiKaitou = "";
                string strNoukiKaitou2 = "";

                for (int i = 0; i < dvNK.Count; i++)
                {
                    if (i == 0 || nKaitouNo == int.Parse(dvNK[i]["KaitouNo"].ToString()))
                    {

                        // ����o�^�̏ꍇ�����ŕ\��
                        if (int.Parse(dvNK[i]["KaitouNo"].ToString()) == 1)
                            strNoukiKaitou += string.Format("{0} {1}:{2:N0}</font><br>",
                                                            Utility.FormatToyyMMdd(dvNK[i]["Nouki"].ToString()),
                                                            "����", dvNK[i]["Suuryou"]);
                        else
                        {
                            // �ŐV�o�^�̏ꍇ�Ԏ��ŕ\��
                            strNoukiKaitou += string.Format("<font color = \"red\">{0} {1}:{2:N0}</font><br>",
                                                            Utility.FormatToyyMMdd(dvNK[i]["Nouki"].ToString()),
                                                            "����", dvNK[i]["Suuryou"]);
                        }
                        // �ۑ��p��No
                        nKaitouNo = int.Parse(dvNK[i]["KaitouNo"].ToString());

                        if (dvNK.Count - 1 == i)
                        {
                            strNoukiKaitou +=
                                         string.Format("({0} {1})<br> ",
                                         Utility.FormatToyyMMddHHmm(dvNK[i]["Tourokubi"].ToString()), "�o�^");
                        }
                        else if (i + 1 < dvNK.Count)
                        {
                            if (nKaitouNo != int.Parse(dvNK[i + 1]["KaitouNo"].ToString()))
                            {
                                strNoukiKaitou +=
                                    string.Format("<font color = \"red\">({0} {1})</font><br> ",
                                    Utility.FormatToyyMMddHHmm(dvNK[i]["Tourokubi"].ToString()), "�o�^");
                            }
                        }

                    }
                    else if (nKaitouNo - 1 == int.Parse(dvNK[i]["KaitouNo"].ToString()))
                    {
                        strNoukiKaitou2 +=
                            string.Format("{0} {1}:{2:N0}</font><br>",
                                                            Utility.FormatToyyMMdd(dvNK[i]["Nouki"].ToString()),
                                                            "����", dvNK[i]["Suuryou"]);

                        if ((i > 0 && i == dvNK.Count - 1) || ((nKaitouNo - 1) != int.Parse(dvNK[i + 1]["KaitouNo"].ToString())))
                        {
                            strNoukiKaitou2 +=
                                string.Format("({0} {1})<br> ",
                                Utility.FormatToyyMMddHHmm(dvNK[i]["Tourokubi"].ToString()), "�o�^");
                        }


                    }
                    else
                        break;
                }

                if (strNoukiKaitou != "" && strNoukiKaitou2 != "")
                {
                    strNKaitouAry[0] = strNoukiKaitou2 + strNoukiKaitou;

                }
                else
                {
                    strNKaitouAry[0] = strNoukiKaitou;

                }

                info.strNoukiKaitouHtml = Utility.ToInnerRowsHTML_NoLine(strNKaitouAry);

            }


            if (bCreateKaitou)
            {
                if (info.strNoukiKaitouHtml == "")
                    info.strNoukiKaitouHtml += string.Format("<font color = \"red\" font-size = \"8pt\">{0}<br></font>", "����");

                info.strNoukiKaitouHtml += string.Format(
                     "<a href=\"javascript:void(0);\" onclick=\"YN('{0}_{1}_{2}');\"><font color =\"blue\">{3}</font></a>",
                    dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, "���͗��\��");

                info.strShiteiNouki += string.Format(
                     "<input type=button  onclick=ShiteiNouki('{0}_{1}_{2}'); return false; value={3} />",
                      dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, "�w��[��");
            }
        }

        // �񓚔[������HTML���쐬�i�ꗗ�\�����j
        private void GetKaitouNoukiInfo(ChumonDataSet.V_Chumon_JyouhouRow drChumon, DataView dvKN, out NoukiKaitouInfo info, bool bCreateKaitou)
        {
            info = new NoukiKaitouInfo();
            string[] strNKaitouAry = new string[dvKN.Count];

            if (0 < dvKN.Count)
            {
                // �ۑ��p��No
                int nKaitouNo = 0;
                // �ۑ��p�[���� 
                string strNoukiKaitou = "";

                for (int i = 0; i < dvKN.Count; i++)
                {
                    // �ŐV�o�^�̏ꍇ�Ԏ��ŕ\��
                    if (i == 0 || nKaitouNo == int.Parse(dvKN[i]["KaitouNo"].ToString()))
                    {
                        strNoukiKaitou += string.Format("{0} {1}:{2:N0}<br>",
                           Utility.FormatToyyMMdd(dvKN[i]["Nouki"].ToString()),
                                                          "����", dvKN[i]["Suuryou"]);
                        // �ۑ��p��No
                        nKaitouNo = int.Parse(dvKN[i]["KaitouNo"].ToString());

                        if (dvKN.Count - 1 == i)
                        {
                            strNoukiKaitou +=
                                         string.Format("({0} {1})<br> ",
                                         Utility.FormatToyyMMddHHmm(dvKN[i]["Tourokubi"].ToString()), "�o�^");
                        }
                        else if (i + 1 < dvKN.Count)
                        {
                            if (nKaitouNo != int.Parse(dvKN[i + 1]["KaitouNo"].ToString()))
                            {
                                strNoukiKaitou +=
                                    string.Format("({0} {1})<br> ",
                                    Utility.FormatToyyMMddHHmm(dvKN[i]["Tourokubi"].ToString()), "�o�^");
                            }
                        }


                    }
                    else
                        break;
                }
                strNKaitouAry[0] = strNoukiKaitou;
            }
            info.strNoukiKaitouHtml = Utility.ToInnerRowsHTML_NoLine(strNKaitouAry);


            if (bCreateKaitou)
            {
                info.strNoukiKaitouHtml += string.Format(
                      "<a href=\"javascript:void(0);\" onclick=\"YN('{0}_{1}_{2}');\"><font color =\"blue\">{3}</font></a>",
                    drChumon.Year, drChumon.HacchuuNo, drChumon.JigyoushoKubun, "���͗��\��");
                info.strShiteiNouki += string.Format(
                      "<input type=button  onclick=ShiteiNouki('{0}_{1}_{2}'); return false; value={3} />",
                      drChumon.Year, drChumon.HacchuuNo, drChumon.JigyoushoKubun, "�w��[��");
            }

        }


        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];
            LibError err = null;

            // ��L�[�i�N�A����No�A���Ə��敪�j
            string[] strChumonKey = null;
            string strYear = "";
            string strHacchuuNo = "";
            int nKubun = 0;

            // �[��
            string strNouki = "";
            string Suuryou = "";

            // �[����No
            int nKaitouNo = 0;
            
            // �[���ύXNo
            int nHenkouNo = 0;
            //
            //int nSuuryou = 0;

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
                case "ddlBuhin":

                    ListSet.SetDdlBuhin_C(SessionManager.KaishaCode,DdlBuhinKubun.SelectedValue, DdlBuhin);
                    this.Ram.AjaxSettings.AddAjaxSetting(Ram, this.DdlBuhin);
                    break;
                case "shounin":

                    strChumonKey= strArgs[1].Split('_');
                    err = NoukiHenkouClass.T_NoukiHenkou_Update(
                                                   strChumonKey[0], strChumonKey[1], int.Parse(strChumonKey[2]),
                                                   int.Parse(strChumonKey[3]), SessionManager.LoginID, Global.GetConnection());
                    if (err != null)
                    {
                        this.ShowMsg(e.ToString(), true);
                        return;
                    }
                    this.Create();
                    this.ShowMsg("���F���܂���", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    break;

                case "nyuryoku_open":
                    // �[�������R���g���[���̕\��
                    this.DivNoukiKaitou.Controls.Clear();

                    string[] strChumonKeyArray = strArgs[1].Split('\t');
                    for (int i = 0; i < strChumonKeyArray.Length; i++)
                    {
                        strChumonKey = strChumonKeyArray[i].Split('_');
                        strYear = strChumonKey[0];
                        strHacchuuNo = strChumonKey[1];
                        nKubun = int.Parse(strChumonKey[2]);
                        strNouki = "";
                        Suuryou = "";


                        CtlNoukiKaitou c = this.LoadControl("CtlNoukiKaitou.ascx") as CtlNoukiKaitou;
                        c.ID = "NKM_" + strChumonKeyArray[i];
                        // �ŐV�̉�No�擾
                        ChumonDataSet.V_Chumon_JyouhouRow dr =
                            ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo,nKubun, Global.GetConnection());

                        // ��No
                        if (!dr.IsKaitouNoNull())
                            nKaitouNo = dr.KaitouNo;
                        else
                            nKaitouNo = 0;

                        // �[���@
                        if (!dr.IsHenkouNoNull())
                        {
                            nHenkouNo = dr.HenkouNo;
                            //nKaitouNo = drChuumon1.KaitouNo;

                            m2mKoubaiDataSet.T_NoukiHenkouDataTable dtNouki =
                                NoukiHenkouClass.getT_NoukiHenkouDataTable
                                (strYear, strHacchuuNo, nKubun, dr.HenkouNo, Global.GetConnection());
                            for (int n = 0; n < dtNouki.Rows.Count; n++)
                            {
                                if (strNouki != "") strNouki += '_';
                                strNouki += dtNouki[n].Nouki.ToString();
                                if (Suuryou != "") Suuryou += '_';
                                Suuryou += dtNouki[n].Suuryou.ToString();
                            }
                        }
                        else
                        {
                            strNouki = dr.Nouki;   // �[�� 
                            // ����                          
                            Suuryou = Convert.ToString(dr.Suuryou);

                        }               

                        // �ŐV�̉�No��
                        m2mKoubaiDataSet.T_NoukiKaitouDataTable dt = NoukiKaitouClass.getT_NoukiKaitouDataTable(strYear, strHacchuuNo, nKubun, nKaitouNo, Global.GetConnection());

                        NoukiKaitouDataSet.KaitouNoukiDataTable dtKaitouNouki = new NoukiKaitouDataSet.KaitouNoukiDataTable();

                        if (0 < dt.Rows.Count)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                NoukiKaitouDataSet.KaitouNoukiRow drKaitouNouki = (NoukiKaitouDataSet.KaitouNoukiRow)dtKaitouNouki.NewKaitouNoukiRow();

                                drKaitouNouki.Year = strYear;
                                drKaitouNouki.HacchuuNo = strHacchuuNo;
                                drKaitouNouki.JigyoushoKubun = nKubun;
                                drKaitouNouki.Suuryou = string.Format("{0:F0}", dt[j].Suuryou);
                                drKaitouNouki.Tourokubi = dt[j].Tourokubi.ToString();
                                drKaitouNouki.Nouki = Convert.ToString(dt[j].Nouki);
                                drKaitouNouki.KaitouNo = dt[j].KaitouNo.ToString();

                                drKaitouNouki.RowNo = dt[j].RowNo.ToString();
                                dtKaitouNouki.AddKaitouNoukiRow(drKaitouNouki);
                            }
                            c.Create(dtKaitouNouki, true);
                        }
                        else
                        {
                            string[] strNoukiAry = strNouki.Split('_');
                            string[] strSuuryouAry = Suuryou.Split('_');
                            for (int n = 0; n < strNoukiAry.Length; n++)
                            {
                                NoukiKaitouDataSet.KaitouNoukiRow drKaitouNouki = (NoukiKaitouDataSet.KaitouNoukiRow)dtKaitouNouki.NewKaitouNoukiRow();

                                drKaitouNouki.Year = strYear;
                                drKaitouNouki.HacchuuNo = strHacchuuNo;
                                drKaitouNouki.JigyoushoKubun = nKubun;
                                drKaitouNouki.Suuryou = strSuuryouAry[n];
                                drKaitouNouki.Tourokubi = "";
                                drKaitouNouki.Nouki = strNoukiAry[n];
                                drKaitouNouki.KaitouNo = "";

                                dtKaitouNouki.AddKaitouNoukiRow(drKaitouNouki);
                            }
                            c.Create(dtKaitouNouki, true);
                        }
                        this.DivNoukiKaitou.Controls.Add(c);
                    }
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DivNoukiKaitou);
                    break;

                case "nyuryoku_close":
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TbxKaitouNouki);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TbxShiteiNouki);
                    // ���e�L�X�g�{�b�N�X�i�[���񓚂̔[���Ɛ��ʂ�ۑ����邽�߁j
                    TbxKaitouNouki.Text = "";
                    // ���e�L�X�g�{�b�N�X�i�[���񓚂̎w��[���{�^����ۑ����邽�߁j
                    TbxShiteiNouki.Text = "";


                    // �s���̎擾
                    strChumonKeyArray = strArgs[1].Split('\t');

                    for (int i = 0; i < strChumonKeyArray.Length; i++)
                    {
                        // ��L�[�̎擾
                        strChumonKey = strChumonKeyArray[i].Split('_');
                        strYear = strChumonKey[0];
                        strHacchuuNo = strChumonKey[1];
                        nKubun = int.Parse(strChumonKey[2]);
                        // �ŐV�̉�No�擾
                        ChumonDataSet.V_Chumon_JyouhouRow drChumon = ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo, nKubun, Global.GetConnection());

                        // �񓚔[��
                        this.m_dvKaitouNouki = new DataView(this.m_dtKaitouNouki);
                        this.m_dtKaitouNouki = NoukiKaitouClass.getT_NoukiKaitouDataTable(Global.GetConnection());
                        this.m_dvKaitouNouki = new DataView(this.m_dtKaitouNouki);
                        this.m_dvKaitouNouki.Sort = "KaitouNo DESC ";

                        DataView dvKaitouNouki = this.m_dvKaitouNouki;
                        dvKaitouNouki.RowFilter = string.Format("Year = '{0}' AND HacchuuNo = '{1}' AND JigyoushoKubun = '{2}' ",
                                                                 drChumon.Year, drChumon.HacchuuNo, drChumon.JigyoushoKubun);

                        NoukiKaitouInfo info = null;
                        this.GetMiKaitouNoukiInfo(drChumon, dvKaitouNouki, out info, true);

                        if (0 < i)
                        {
                            this.HidKaitouNouki.Value += "\t";
                            this.TbxKaitouNouki.Text += "\t";
                            this.TbxShiteiNouki.Text += "\t";
                        }

                        TbxKaitouNouki.Text += info.strNoukiKaitouHtml;
                        TbxShiteiNouki.Text += info.strShiteiNouki;
                    }

                    break;
                case "nyuryoku_add_row":
                case "nyuryoku_del_row":

                    strChumonKeyArray = strArgs[1].Split('\t');

                    // ��L�[�̎擾
                    strChumonKey = strChumonKeyArray[0].Split('_');
                    strYear = strChumonKey[0];
                    strHacchuuNo = strChumonKey[1];
                    nKubun = int.Parse(strChumonKey[2]);

                    // �[��1\t����2\t����NO\t�[��2\t����2\t�[������NO2�E�E�E
                    string[] strArray = this.HidKaitouNoukiArg.Value.Split('\t');

                    ArrayList aryNouki = new ArrayList();
                    ArrayList arySuuryou = new ArrayList();
                    ArrayList aryKaitouNo = new ArrayList();

                    for (int i = 0; i < strArray.Length / 3; i++)
                    {
                        aryNouki.Add(strArray[i * 3]);
                        arySuuryou.Add(strArray[i * 3 + 1]);
                        aryKaitouNo.Add(strArray[i * 3 + 2]);
                    }
                    if ("nyuryoku_add_row" == strCmd)
                    {
                        // �s�̒ǉ��̏ꍇ�́A��ԍŌ�ɒǉ�
                        aryNouki.Add("");
                        arySuuryou.Add("");
                        aryKaitouNo.Add("");
                    }
                    string[] strAryNouki = new string[aryNouki.Count];
                    aryNouki.CopyTo(strAryNouki);
                    string[] strSuuryou = new string[arySuuryou.Count];
                    arySuuryou.CopyTo(strSuuryou);
                    string[] strKaitouNo = new string[aryKaitouNo.Count];
                    aryKaitouNo.CopyTo(strKaitouNo);

                    CtlNoukiKaitou kn = this.LoadControl("CtlNoukiKaitou.ascx") as CtlNoukiKaitou;
                    kn.ID = "NKM_" + strYear + "_" + strHacchuuNo + "_" + nKubun;
                    kn.Create(strYear, strHacchuuNo,nKubun, strAryNouki, strSuuryou, strKaitouNo);

                    this.DivNoukiKaitou.Controls.Clear();
                    this.DivNoukiKaitou.Controls.Add(kn);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DivNoukiKaitou);

                    break;

                case "shiteinouki_reg":
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);

                    strChumonKey = strArgs[1].Split('_');
                    // ��L�[�̎擾
                    strYear = strChumonKey[0];
                    strHacchuuNo = strChumonKey[1];
                    nKubun = int.Parse(strChumonKey[2]);

                    // �ŐV�̉�No�擾
                    ChumonDataSet.V_Chumon_JyouhouRow drChuumon1 = ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo,nKubun, Global.GetConnection());

                    // ��No  
                    if (!drChuumon1.IsKaitouNoNull())
                        nKaitouNo = drChuumon1.KaitouNo;
                    else
                        nKaitouNo = 0;

                    if (!drChuumon1.IsHenkouNoNull())
                    {
                        nHenkouNo = drChuumon1.HenkouNo;

                        m2mKoubaiDataSet.T_NoukiHenkouDataTable dtNouki =
                            NoukiHenkouClass.getT_NoukiHenkouDataTable
                            (strYear, strHacchuuNo, nKubun, drChuumon1.HenkouNo, Global.GetConnection());
                        for (int i = 0; i < dtNouki.Rows.Count; i++)
                        {
                            if (strNouki != "") strNouki += '_';
                            strNouki += dtNouki[i].Nouki.ToString();
                            if (Suuryou != "") Suuryou += '_';
                            Suuryou += dtNouki[i].Suuryou.ToString();
                        }
                    }
                    else
                    {
                        nHenkouNo = 0;
                        strNouki = drChuumon1.Nouki;
                        // �w�萔��                     
                        Suuryou = drChuumon1.Suuryou.ToString();
                    }


                    // �����ԍ��A���敪�����Ƃɔ[���A���ʂ��X�V����
                    err =
                         NoukiKaitouClass.T_NoukiKaitou_Insert
                         (strYear, strHacchuuNo,nKubun, nKaitouNo, Suuryou, strNouki, Global.GetConnection());
                    // �X�V�G���[�`�F�b�N
                    if (err != null)
                    {
                        this.ShowMsg(err.Message, true);
                        return;
                    }
                    else
                    {
                        // �[���񓚓o�^��A���[�����M���擾
                        ChumonDataSet.V_MailInfoDataTable dtMail =
                           ChumonClass.getV_MailInfoDataTable(strYear, strHacchuuNo, nKubun, SessionManager.LoginID, Global.GetConnection());
                       
                        for (int i = 0; i < dtMail.Rows.Count; i++)
                        {
                            // CC�̔z����쐬
                            ChumonDataSet.V_MailToCCDataTable dtCC =
                               ChumonClass.getV_MailToCCDataTable(dtMail[i].JigyoushoKubun, dtMail[i].LoginID_Y, Global.GetConnection());
                           
                            string[] aryTocc = new string[dtCC.Rows.Count];
                            for (int j = 0; j < dtCC.Rows.Count; j++)
                            {
                                aryTocc[j] = dtCC[j].Mail;

                            }
                          
                            MailClass.MailParam p = this.GetMailParam(dtMail[i], nKaitouNo + 1);

                            MailClass.SendMail(p, aryTocc);
                        }                        
                    }

                    this.Create();
                    this.ShowMsg("�[���񓚂�o�^���܂���", false);

                    break;

                case "nouki_kaitou_reg":
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);                    
                    strChumonKeyArray = strArgs[1].Split('\t');

                    string[] strNoukiKaitouData = this.HidKaitouNoukiArg.Value.Split('|');
                    // ���̓`�F�b�N�p������
                    string strNouKiChkMsg = "";
                    string strSuuryouChkMsg = "";
                    string strErrMsg = "";                  
                   
                    for (int i = 0; i < strChumonKeyArray.Length; i++)
                    {
                        strChumonKey = strChumonKeyArray[i].Split('_');
                        // �����ԍ��@���敪                      
                        strYear = strChumonKey[0];
                        strHacchuuNo = strChumonKey[1];
                        nKubun = int.Parse(strChumonKey[2]);

                        Hashtable KaitouNoukiTbl =
                            this.splitKaitouNoukiData(strNoukiKaitouData[i]);
                        // �[���`�F�b�N
                        bool chkN = this.NoukiCheck(KaitouNoukiTbl);
                        if (!chkN)
                        {
                            if (strNouKiChkMsg != "")
                                strNouKiChkMsg += ",";
                            strNouKiChkMsg += strHacchuuNo;
                        }
                        // ���ʃ`�F�b�N
                        bool chkS = this.SuuryouCheck(strYear, strHacchuuNo, nKubun, KaitouNoukiTbl);
                        if (!chkS)
                        {
                            if (strSuuryouChkMsg != "")
                                strSuuryouChkMsg += ",";
                            strSuuryouChkMsg += strHacchuuNo;
                        }
                        if (chkN == false || chkS == false)
                            continue;

                        // �ŐV�̉�No�ƍs���擾
                        // �ŐV�̉�No�擾
                        ChumonDataSet.V_Chumon_JyouhouRow drChuumon =
                            ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo, nKubun, Global.GetConnection());

                        if (!drChuumon.IsKaitouNoNull())
                            nKaitouNo = drChuumon.KaitouNo;
                        else
                            nKaitouNo = 0;

                        // �����ԍ��A���敪�����Ƃɔ[���A���ʂ��X�V����
                        err =
                            NoukiKaitouClass.T_NoukiKaitou_Update(strYear, strHacchuuNo, nKubun, KaitouNoukiTbl, nKaitouNo, Global.GetConnection());
                        // �X�V�G���[�`�F�b�N
                        if (err != null)
                        {
                            if (strErrMsg != "")
                                strErrMsg += ",";
                            strErrMsg += strHacchuuNo;
                            continue;
                        }
                    }
                    
                    int nCnt = 1;
                    // �[���񓚕ύX���������ׂăf�[�^���擾
                    ChumonDataSet.V_Chumon_NoukiKaitouDataTable dtKN =
                        ChumonClass.getV_Chumon_NoukiKaitouDataTable(strArgs[1], Global.GetConnection());
                    // �ۑ��p�V�K�o�^������ID�z��
                    ArrayList aryID1 = new ArrayList();
                    // �ۑ��p�X�V������ID�z��
                    ArrayList aryID2 = new ArrayList();
                    for (int i = 0; i < dtKN.Rows.Count; i++)
                    {
                        if (dtKN[i].KaitouNo == 1)
                        {
                            if (!aryID1.Contains(dtKN[i].HacchushaID))
                                aryID1.Add(dtKN[i].HacchushaID);
                        }
                        else
                        {
                            if (!aryID2.Contains(dtKN[i].HacchushaID))
                                aryID2.Add(dtKN[i].HacchushaID);
                        }
                    }

                    // �[���񓚓o�^��A���[�����M���擾
                    ChumonDataSet.V_MailInfoDataTable dtMailN =
                        ChumonClass.getV_MailInfo_KNDataTable(strArgs[1], SessionManager.LoginID, Global.GetConnection());
                    // �����S���Ґl�����Ń��[���𑗐M
                    for (int n = 0; n < dtMailN.Rows.Count; n++)
                    {
                        // CC�̔z����쐬
                        ChumonDataSet.V_MailToCCDataTable dtCC =
                           ChumonClass.getV_MailToCCDataTable(dtMailN[n].JigyoushoKubun, dtMailN[n].LoginID_Y, Global.GetConnection());

                        string[] aryTocc = new string[dtCC.Rows.Count];
                        for (int j = 0; j < dtCC.Rows.Count; j++)
                        {
                            aryTocc[j] = dtCC[j].Mail;

                        }
                        // �V�K�o�^�f�[�^
                        if (aryID1.Count != 0)
                        {
                            for (int i = 0; i < aryID1.Count; i++)
                            {
                                if (dtMailN[n].LoginID_Y == aryID1[i].ToString())
                                {
                                    MailClass.MailParam p = this.GetMailParam(dtMailN[n], nCnt);
                                    MailClass.SendMail(p, aryTocc);
                                }
                            }
                        }
                        // �X�V�f�[�^
                        if (aryID2.Count != 0)
                        {
                            for (int i = 0; i < aryID2.Count; i++)
                            {
                                if (dtMailN[n].LoginID_Y == aryID2[i].ToString())
                                {
                                    MailClass.MailParam p = this.GetMailParam(dtMailN[n], nCnt + 1);
                                    MailClass.SendMail(p, aryTocc);
                                }
                            }
                        }
                    }

                    this.Create();

                    if (strNouKiChkMsg != "" || strSuuryouChkMsg != "" || strErrMsg != "")
                    {
                        string msg = "";
                        if (strNouKiChkMsg != "")
                        {
                            msg +=
                                "[�����ԍ�]" + strNouKiChkMsg + "�̔[�����s���ł�";
                        }
                        if (strSuuryouChkMsg != "")
                        {
                            if (strNouKiChkMsg != "") msg += "<br>";
                            msg +=
                                "[�����ԍ�]" + strSuuryouChkMsg + "�̐��ʂ��s���ł�";
                        }
                        if (strErrMsg != "")
                        {
                            msg +=
                                "[�����ԍ�]" + strErrMsg + "�̍X�V�Ɏ��s���܂���";
                        }
                        this.ShowMsg(msg, true);
                        return;
                    }

                    this.ShowMsg("�X�V���܂���", false);

                    break;
            }
        }
       
        private MailClass.MailParam GetMailParam(ChumonDataSet.V_MailInfoRow dr, int n)
        {
            // �d��������擾
            m2mKoubaiDataSet.M_ShiiresakiRow drShiire =
                ShiiresakiClass.getM_ShiiresakiRow(SessionManager.KaishaCode, Global.GetConnection());

            MailClass.MailParam p = new MailClass.MailParam();
            // ���M�����[���A�h���X
            p._MailFrom = dr.Mail_S;
            // ���M�惁�[���A�h���X
            p._MailTo = dr.Mail_Y;
            if (n == 1)
            {
                p._Subject = "�[���񓚂��o�^����܂���";
                p._Body = MailClass.GetBody_NoukiKaitou_Shinki(drShiire);// �{��
            }
            else
            {
                p._Subject = "�[���񓚂��ύX����܂���";
                p._Body = MailClass.GetBody_NoukiKaitou_Koushin(drShiire);// �{��
            }
            
            // SMTP
            p._SMTP_Server = Global.SMTP_Server;

            return p;
            
        }
      
        private Hashtable splitKaitouNoukiData(string strNoukiKaitouData)
        {
            string[] splitKNData = strNoukiKaitouData.Split('\t');

            ArrayList arrayNouki = new ArrayList();
            ArrayList arraySuuryou = new ArrayList();
            ArrayList arrayKaitouNo = new ArrayList();

            for (int i = 0; i < splitKNData.Length; i++)
            {
                switch (i % 3)
                {
                    case 0:
                        if (splitKNData[i].Length != 8)
                            splitKNData[i] = Utility.FormatToyyyyMMdd(splitKNData[i]);
                        arrayNouki.Add(splitKNData[i]);
                        break;
                    case 1:
                        arraySuuryou.Add(splitKNData[i]);
                        break;
                    case 2:
                        arrayKaitouNo.Add(splitKNData[i]);
                        break;
                }
            }

            Hashtable kaitouNoukiTbl = new Hashtable();
            kaitouNoukiTbl.Add("Nouki", arrayNouki);
            kaitouNoukiTbl.Add("Suuryou", arraySuuryou);
            kaitouNoukiTbl.Add("KaitouNo", arrayKaitouNo);

            return kaitouNoukiTbl;
        }


        // ���ʂ��������l���ǂ����`�F�b�N
        private bool SuuryouCheck(string strYear, string strHacchuuNo, int nKubun, Hashtable KaitouNoukiTbl)
        {
            // �����������擾
            // �ŐV�̉�No�擾
            ChumonDataSet.V_Chumon_JyouhouRow drChuumon =
                ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo, nKubun,  Global.GetConnection());

            // �[���񓚎��̒������͐��ʂ��擾
            ArrayList arySuuryou = (ArrayList)KaitouNoukiTbl["Suuryou"];
            int souInputSuu = 0;
            for (int i = 0; i < arySuuryou.Count; i++)
            {
                try
                {
                    souInputSuu += Convert.ToInt32(arySuuryou[i]);
                }
                catch
                {
                    // ���l�����łȂ��ꍇ�s��
                    return false;
                }
            }

            // ���������ƒ������͐��ʂ��قȂ��Ă�����s��
            if (souInputSuu <= drChuumon.Suuryou)
                return true;
            else
                return false;
           
        }
        // �[�����������l���ǂ����`�F�b�N
        private bool NoukiCheck(Hashtable KaitouNoukiTbl)
        {
            // �[���񓚎��̒������͔[�����擾
            ArrayList arySuuryou = (ArrayList)KaitouNoukiTbl["Nouki"];
            for (int i = 0; i < arySuuryou.Count; i++)
            {
                if (!this.CheckDayFormat(arySuuryou[i].ToString()))
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckDayFormat(string yyMMdd)
        {
            // �������`�F�b�N
            if (yyMMdd.Length != 8)
                return false;
            else
                return true;
        }



    }
}
