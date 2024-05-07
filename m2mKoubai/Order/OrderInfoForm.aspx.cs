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

namespace m2mKoubai.Order
{
    public partial class OrderInfoForm : System.Web.UI.Page
    {
        private const int G_CELL_I = 0;
        private const int G_CELL_CANCEL = 1;
        private const int G_CELL_NO = 2;

        private const int G_CELL_SHIIRE = 3;
        private const int G_CELL_BUHIN = 4;
        private const int G_CELL_SUURYOU = 5;
        private const int G_CELL_CHUMON_KINGAKU = 6;
        private const int G_CELL_TANI = 7;
        private const int G_CELL_NOUNYUU_BASHO = 8;
        private const int G_CELL_HACCHUU_TANTOUSHA = 9;
        private const int G_CELL_NOUKI_HENKOU = 10;
        private const int G_CELL_KAITOU_NOUKI = 11;
        private const int G_CELL_NOUHINBI = 12;
        private const int G_CELL_MSG = 13;

        protected int cell_index = G_CELL_NOUKI_HENKOU;
        Core.Collection.StringCollections m_objStringCols = new Core.Collection.StringCollections();

        DataView m_dvNoukiHenkou = null;
        m2mKoubaiDataSet.T_NoukiHenkouDataTable m_dtNoukiHenkou = null;

        DataView m_dvNoukiKaitou = null;
        m2mKoubaiDataSet.T_NoukiKaitouDataTable m_dtNoukiKaitou = null;

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
                if (SessionManager.UserKubun != (byte)UserKubun.Owner)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }

                CtlTabMain tab = FindControl("Tab") as CtlTabMain;
                tab.Menu = CtlTabMain.MainMenu.Hacchu_Jyouhou;

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

            //this.BtnI.Visible = b;
            this.BtnI.Attributes["onclick"] = "Print(); return false;";
            
                
            
            //Img
            this.Img1.Style.Add("display", "none");

            // �����{�^��
            BtnK.Attributes["onclick"] = "Kensaku();";


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
            // Hid
            HidKey.Value = "";
            HidChkID.Value = "";

            this.m_dtNoukiHenkou = NoukiHenkouClass.getT_NoukiHenkouDataTable(Global.GetConnection());
            this.m_dvNoukiHenkou = new DataView(this.m_dtNoukiHenkou);
            this.m_dvNoukiHenkou.Sort = "HenkouNo DESC ";


            this.m_dtNoukiKaitou = NoukiKaitouClass.getT_NoukiKaitouDataTable(Global.GetConnection());
            this.m_dvNoukiKaitou = new DataView(this.m_dtNoukiKaitou);
            this.m_dvNoukiKaitou.Sort = "KaitouNo DESC ";


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
            this.BtnI.Visible = b;
            BtnI.Visible = b;

        }

        private void SetList()
        {
            // �d����
            ListSet.SetDdlShiiresaki_C(DdlShiire);
            //�@�[���ꏊ
            ListSet.SetDdlNounyuuBasho_C(DdlNBasho);
            // �[���񓚏�
            ListSet.SetDdlNoukiKaitouJyoukyou(DdlNKJyoukyou, SessionManager.UserKubun);
            // �[�i��
            ListSet.SetDdlNouhinJyoukyou(DdlNJyoukyou);
            // ���i�敪
            ListSet.SetDdlBuhinKubun_C(DdlBuhinKubun,null);
            // ���i
            this.DdlBuhinKubun.Attributes["onchange"] = "OnBuhin(); return false";
            // �����S����
            ListSet.SetDdlHacchuTantousha_C(DdlHacchuTantousha);
            // �L�����Z����
            ListSet.SetDdlCancel(DdlCancel);
            // ���b�Z�[�W
            ListSet.SetDdlMsg(DdlMsg);

        }
        private ChumonClass.KensakuParam GetKensakuParam()
        {
            ChumonClass.KensakuParam k = new ChumonClass.KensakuParam();
            // ���[�U�[�敪
            k._userKubun = (byte)SessionManager.UserKubun;
            // ���Ə��敪�i�ǉ�09-07-29 ���j
            //k._JigyoushoKubun = SessionManager.JigyoushoKubun;
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
            // �L�����Z��
            k._Cancelbi = int.Parse(DdlCancel.SelectedValue);
            if (k._Cancelbi == 0)
                b = true;
          
            
            // ���b�Z�[�W
            k._Msg = int.Parse(DdlMsg.SelectedValue);
            return k;
        }

        
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.Header)
            {
                // DL_ChkAll               
                HtmlInputCheckBox chkH = e.Row.FindControl("ChkH") as HtmlInputCheckBox;
                chkH.Visible = b;
                if (b)
                    chkH.Attributes["onclick"] = "ChkAll(this.checked)";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ChumonDataSet.V_Chumon_JyouhouRow dr =
                     ((DataRowView)e.Row.DataItem).Row as ChumonDataSet.V_Chumon_JyouhouRow;

                m_objStringCols["C"].Add(dr.Year + '_' + dr.HacchuuNo + '_' + dr.JigyoushoKubun);

                // ���������
                HtmlInputCheckBox chk = e.Row.FindControl("ChkI") as HtmlInputCheckBox;
                ChumonClass.ChumonKey key = new ChumonClass.ChumonKey(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);

                // chk.id���i�[
                if (HidChkID.Value != "") HidChkID.Value += ",";
                HidChkID.Value += chk.ClientID;

                // �L�[���i�[
                chk.Value = key.ToString();
                // �L�����Z��
                if (!dr.IsCancelBiNull())
                {
                    chk.Visible = false;

                    e.Row.Cells[G_CELL_CANCEL].Text = "�L����<br>�Z��";
                    e.Row.Cells[G_CELL_CANCEL].ForeColor = System.Drawing.Color.Red;
                }

                // ����NO                
                (e.Row.FindControl("LitHacchuuNo") as Literal).Text = Utility.LinkToHacchuuNo(key.ToString(), dr.HacchuuNo);
                // ������
                (e.Row.FindControl("LitHacchuuBi") as Literal).Text = dr.HacchuuBi.ToString("yy/MM/dd");
                // �����S���҃R�[�h                
                (e.Row.FindControl("LitHacchuuTantoushaCode") as Literal).Text = dr.TantoushaCode;
                // �����S���Җ�
                (e.Row.FindControl("LitHacchuuTantoushaMei") as Literal).Text = dr.Name;
                // �d����R�[�h
                (e.Row.FindControl("LitShiireCode") as Literal).Text = dr.ShiiresakiCode;
                // �d���於
                (e.Row.FindControl("LitShiireName") as Literal).Text = dr.ShiiresakiMei;
                // ���i�R�[�h
                (e.Row.FindControl("LitCode") as Literal).Text = dr.BuhinKubun + dr.BuhinCode;
                // ���i�ږ�
                (e.Row.FindControl("LitName") as Literal).Text = dr.BuhinMei;
                // ����
                (e.Row.FindControl("LitSuuryou") as Literal).Text = dr.Suuryou.ToString("#,##0");
                // �P��
                //(e.Row.FindControl("LitTanka") as Literal).Text = "\\" + dr.Tanka.ToString("#,##0.#0");
                // �P��   �ύX 09/07/28
                if (!dr.KaritankaFlg)
                {
                    (e.Row.FindControl("LitTanka") as Literal).Text = "\\" + dr.Tanka.ToString("#,##0.#0");
                }
                else
                {
                    (e.Row.FindControl("LitTanka") as Literal).Text =
                        string.Format("<font color =\"red\">{0}</font>", "(��) \\" + dr.Tanka.ToString("#,##0.#0"));
                }
                // �������z
                // ���őΉ�
                decimal dKingaku_Round = Math.Round(dr.Kingaku, 0, MidpointRounding.AwayFromZero);

                // decimal Kingaku = Math.Floor(dr.Suuryou * dr.Tanka);
               // (e.Row.FindControl("LitChumonKingaku") as Literal).Text = "\\" + dr.Kingaku.ToString("#,##");
                // �������z   �ύX 09/07/28
                if (!dr.KaritankaFlg)
                {
                    (e.Row.FindControl("LitChumonKingaku") as Literal).Text = "\\" + dKingaku_Round.ToString("#,##0");
                }
                else
                {
                    (e.Row.FindControl("LitChumonKingaku") as Literal).Text =
                        string.Format("<font color =\"red\">{0}</font>", "\\" + dKingaku_Round.ToString("#,##0"));
                }

                // ���őΉ�
                decimal Zeigaku = Math.Round(dKingaku_Round * dr.Zeiritu / 100, 0, MidpointRounding.AwayFromZero);
                (e.Row.FindControl("LitZeigaku") as Literal).Text = "\\" + Zeigaku.ToString("#,##0");

                // �P��
                e.Row.Cells[G_CELL_TANI].Text = dr.Tani;
                // �[���ꏊ
                e.Row.Cells[G_CELL_NOUNYUU_BASHO].Text = dr.BashoMei;
                // �[���ύX
                DataView dvNouki = this.m_dvNoukiHenkou;
                dvNouki.RowFilter = string.Format("Year = '{0}' AND HacchuuNo = '{1}' AND JigyoushoKubun = '{2}'", dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);

                NoukiHenkouInfo infoNoki = null;
                // �ۑ��p�[��
                string strNoukiHenkou = "";
                if (dr.IsHenkouShouninFlgNull())
                {
                    this.GetNoukiHenkouNullInfo(dr, out infoNoki, out strNoukiHenkou);
                }
                else if (!dr.IsHenkouShouninFlgNull() && dr.HenkouShouninFlg)
                {
                    // �ύX�[�������F�ς̏ꍇ
                    this.GetNoukiInfo(dr, dvNouki, out infoNoki, true, out strNoukiHenkou);
                }
                else
                {
                    // �ύX�[���������F�̏ꍇ
                    this.GetMiNoukiInfo(dr, dvNouki, out infoNoki, true, out strNoukiHenkou);
                }


                CtlNouki k = this.LoadControl("../CtlNouki.ascx") as CtlNouki;
                k.ID = "NKM_" + dr.Year + "_" + dr.HacchuuNo + "_" + dr.JigyoushoKubun;

                this.DivNoukiHenkou.Controls.Add(k);

                this.m_objStringCols["HenkouNoukiData"].Add(
                    string.Format("{0}_{1}_{2}\t{3}\t{4}\t{5}",
                    dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, k.HidNouki.ClientID, k.HidSuuryou.ClientID, k.HidKaitouNo.ClientID));

                this.DivNoukiHenkou.Controls.Clear();


                // �ŐV�̕ύX�[��
                Label lblNyuryoku = (Label)e.Row.Cells[G_CELL_NOUKI_HENKOU].FindControl("LblNouki");
                lblNyuryoku.Text = infoNoki.strNoukiHenkouHtml;


                // �񓚔[��
                this.m_dvNoukiKaitou.RowFilter = string.Format("Year = '{0}' AND HacchuuNo = '{1}' AND JigyoushoKubun = '{2}' ", dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);

                NoukiKaitouInfo info = null;
                // �[���񓚂����F�ς̏ꍇ                
                if (!dr.IsKaitouShouninFlgNull() && dr.KaitouShouninFlg)
                {
                    this.GetNoukiKaitouInfo(dr, m_dvNoukiKaitou, out info);
                }
                else
                {
                    // �[���񓚂������F�̏ꍇ
                    this.GetMiNoukiKaitouInfo(dr, m_dvNoukiKaitou, out info);
                }
                e.Row.Cells[G_CELL_KAITOU_NOUKI].Text = info.strNoukiKaitouHtml;

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
                            if (dtKaitou[i].Nouki > int.Parse(strNoukiAry[j].ToString()))
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
                        e.Row.Cells[G_CELL_KAITOU_NOUKI].BackColor = System.Drawing.Color.FromName("#FFFFA0");

                    }
                }
                // �[�i��
                if (!dr.IsNouhinNoNull())
                {
                    m2mKoubaiDataSet.T_NouhinDataTable dt = NouhinClass_N.getT_NouhinDataTable(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, Global.GetConnection());
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
                HtmlInputButton btnM = e.Row.FindControl("BM") as HtmlInputButton;
                btnM.Attributes["onclick"] = string.Format("Msg('{0}');", key.ToString());

                if (!dr.IsOpenedFlgNull())
                {
                    if (dr.OpenedFlg)
                        btnM.Value = "����";
                    else
                        btnM.Value = BtnMsgText(dr.UserKubun);
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
        //�[���ύX��
        private void GetNoukiHenkouNullInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, out NoukiHenkouInfo info, out string strNouki)
        {
            info = new NoukiHenkouInfo();

            info.strNoukiHenkouHtml = string.Format("{0}<br>", Utility.FormatToyyMMdd(dr.Nouki.ToString()));

            info.strNoukiHenkouHtml += string.Format(
                   "<a href=\"javascript:void(0);\" onclick=\"YN('{0}_{1}_{2}');\"><font color =\"blue\">{3}</font></a>",
                    dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, "���͗��\��");
            
            strNouki = dr.Nouki;

        }
        // �[���ύX�������F�̏ꍇ      
        private void GetMiNoukiInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, DataView dvHN,
                                        out NoukiHenkouInfo info, bool bCreateHenkou, out string strNouki)
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
                        // �ŐV�o�^�̏ꍇ�Ԏ��ŕ\��                           
                        strNouki1 +=
                             string.Format("<font color = \"red\"> {0} {1}:{2}<br>", Utility.FormatToyyMMdd(dvHN[i]["Nouki"].ToString()),
                                "����", dvHN[i]["Suuryou"].ToString());

                        nHenkouNo = int.Parse(dvHN[i]["HenkouNo"].ToString());

                        if (dvHN.Count - 1 == i)
                        {
                            strNouki1 +=
                                         string.Format("({0} {1})<br> ", Utility.FormatToyyMMddHHmm(dvHN[i]["Tourokubi"].ToString()), "�o�^");
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
                if (nHenkouNo == 1)
                {
                    strNouki2 +=
                        string.Format("{0}<br>", Utility.FormatToyyMMdd(dr.Nouki));
                }

                if (strNouki1 != "" && strNouki2 != "")
                {
                    strNoukiAry[0] = strNouki2 + strNouki1;

                }
                else
                {
                    strNoukiAry[0] = strNouki1;
                }
            }
            else
            {
                strNoukiAry = new string[dvHN.Count + 1];
                strNoukiAry[0] = string.Format("{0}<br>", Utility.FormatToyyMMdd(dr.Nouki));

            }

            // �ύX�[��
            info.strNoukiHenkouHtml = Utility.ToInnerRowsHTML_NoLine(strNoukiAry);

            strNouki = str;
            if (bCreateHenkou)
            {

                info.strNoukiHenkouHtml += string.Format(
                    "<a href=\"javascript:void(0);\" onclick=\"YN('{0}_{1}_{2}');\"><font color =\"blue\">{3}</font></a>",
                     dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, "���͗��\��");
            }



        }
        // �ύX�[�������F�ς̏ꍇ
        private void GetNoukiInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, DataView dvHN,
                                    out NoukiHenkouInfo info, bool bCreateHenkou, out string strNoukiHenkou)
        {
            info = new NoukiHenkouInfo();
            string[] strNoukiAry = null;
            string str = "";
            strNoukiAry = new string[dvHN.Count];

            // �ύXNo�ۑ��p
            int nHenkouNo = 0;
            // �ύX�[���ۑ��p
            string strNouki = "";

            // ���ׂẲ񓚔[�����f�[�^�擾
            for (int i = 0; i < dvHN.Count; i++)
            {
                if ((dr.HenkouShouninFlg && i == 0) ||
                    nHenkouNo == int.Parse(dvHN[i]["HenkouNo"].ToString()))
                {
                    // �[���񓚂����F�ς̏ꍇ�����ŕ\��
                    strNouki +=
                         string.Format("{0} {1}:{2}<br>",
                                Utility.FormatToyyMMdd(dvHN[i]["Nouki"].ToString()), "����", dvHN[i]["Suuryou"].ToString());
                    // �ύXNo
                    nHenkouNo = int.Parse(dvHN[i]["HenkouNo"].ToString());

                    if (dvHN.Count - 1 == i || nHenkouNo != int.Parse(dvHN[i + 1]["HenkouNo"].ToString()))
                    {
                        strNouki +=
                                     string.Format("({0} {1})<br> ",
                                     Utility.FormatToyyMMddHHmm(dvHN[i]["Tourokubi"].ToString()), "�o�^");
                    }
                    if (str != "") str += ",";
                    str += dvHN[i]["Nouki"].ToString();

                }
                else
                    break;

                strNoukiAry[0] = strNouki;

            }
            // �[���񓚂̔[���Ɛ���
            info.strNoukiHenkouHtml = Utility.ToInnerRowsHTML_NoLine(strNoukiAry);
            strNoukiHenkou = str;

            if (bCreateHenkou)
            {
                info.strNoukiHenkouHtml += string.Format(
                        "<a href=\"javascript:void(0);\" onclick=\"YN('{0}_{1}_{2}');\"><font color =\"blue\">{3}</font></a>",
                     dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, "���͗��\��");
            }
        }
        // �񓚔[��
        private class NoukiKaitouInfo
        {
            public string strNoukiKaitouHtml = "";
        }
        // �ύX�[��
        private class NoukiHenkouInfo
        {
            public string strNoukiHenkouHtml = "";
        }
        // ���b�Z�[�W�{�^���̃e�L�X�g�\��
        private string BtnMsgText(Byte mFlag)
        {
            string strText = "";
            if (mFlag == (byte)UserKubun.Owner)
                strText = "���M";
            else
                strText = "��M";
            return strText;
        }
        // �񓚔[�������F
        private void GetMiNoukiKaitouInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, DataView dvNK, out NoukiKaitouInfo info)
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
                            //
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

            if (info.strNoukiKaitouHtml == "")
                info.strNoukiKaitouHtml += string.Format("<font color = \"red\">{0}<br></font>", "����");
            if (!dr.IsKaitouShouninFlgNull() && !dr.KaitouShouninFlg && dr.KaitouNo >= 1)
            {
                info.strNoukiKaitouHtml +=
                    string.Format("<input type=button class='f8' onclick=Shounin('{0}_{1}_{2}_{3}_{4}'); return false;  value={5} />",
                    dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, dr.ShiiresakiCode, dr.KaitouNo, "���F");


            }
        }
        // �񓚔[�����F��
        private void GetNoukiKaitouInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, DataView dvNK,
                                       out NoukiKaitouInfo info)
        {
            info = new NoukiKaitouInfo();
            string[] strNKaitouAry = new string[dvNK.Count];

            if (0 < dvNK.Count)
            {
                // �ۑ��p��No
                int nKaitouNo = 0;
                // �ۑ��p�[����
                string strNoukiKaitou = "";

                for (int i = 0; i < dvNK.Count; i++)
                {
                    // �ŐV�o�^�̏ꍇ�Ԏ��ŕ\��
                    if (i == 0 || nKaitouNo == int.Parse(dvNK[i]["KaitouNo"].ToString()))
                    {
                        strNoukiKaitou += string.Format("{0} {1}:{2:N0}<br>",
                           Utility.FormatToyyMMdd(dvNK[i]["Nouki"].ToString()),
                                                          "����", dvNK[i]["Suuryou"]);
                        // �ۑ��p��No
                        nKaitouNo = int.Parse(dvNK[i]["KaitouNo"].ToString());

                        if (dvNK.Count - 1 == i || nKaitouNo != int.Parse(dvNK[i + 1]["KaitouNo"].ToString()))
                        {
                            strNoukiKaitou +=
                                         string.Format("({0}{1})<br> ",
                                         Utility.FormatToyyMMddHHmm(dvNK[i]["Tourokubi"].ToString()), "�o�^");
                        }
                    }
                    else
                        break;
                }
                strNKaitouAry[0] = strNoukiKaitou;
            }
            info.strNoukiKaitouHtml = Utility.ToInnerRowsHTML_NoLine(strNKaitouAry);

            if (info.strNoukiKaitouHtml == "")
                info.strNoukiKaitouHtml += string.Format("<font color = \"red\">{0}<br></font>", "����");


        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];
            LibError err = null;


            string[] strChumonKey = null;
            string strYear = "";
            string strHacchuuNo = "";
            int nKubun = 0;

            // �[��
            string strNouki = "";
            string Suuryou = "";

            // �[����No
            // int nKaitouNo = 0;
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

                    ListSet.SetDdlBuhin_C(this.DdlBuhinKubun.SelectedValue, this.DdlBuhin);
                    this.Ram.AjaxSettings.AddAjaxSetting(Ram, this.DdlBuhin);
                    break;
                case "shounin":

                    strChumonKey = strArgs[1].Split('_');

                    strYear = strChumonKey[0];
                    strHacchuuNo = strChumonKey[1];
                    nKubun = int.Parse(strChumonKey[2]);
                    
                    
                    err = NoukiKaitouClass.T_NoukiKaitou_Update(strYear, strHacchuuNo,nKubun, strChumonKey[3], 
                                                int.Parse(strChumonKey[4]), SessionManager.LoginID, Global.GetConnection());
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
                    this.DivNoukiHenkou.Controls.Clear();

                    string[] strChumonKeyArray = strArgs[1].Split('\t');

                    for (int i = 0; i < strChumonKeyArray.Length; i++)
                    {
                        strChumonKey = strChumonKeyArray[i].Split('_');

                        strYear = strChumonKey[0];
                        strHacchuuNo = strChumonKey[1];
                        nKubun = int.Parse(strChumonKey[2]);

                        CtlNouki c = this.LoadControl("../CtlNouki.ascx") as CtlNouki;
                        c.ID = "NKM_" + strChumonKeyArray[i];

                        // �ŐV�̉�No�擾
                        ChumonDataSet.V_Chumon_JyouhouRow dr =
                            ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo, nKubun, Global.GetConnection());

                        // �@
                        if (!dr.IsHenkouNoNull())
                        {
                            nHenkouNo = dr.HenkouNo;
                        }
                        else
                        {
                            nHenkouNo = 0;
                            strNouki = dr.Nouki;
                           
                        }
                      
                        Suuryou = Convert.ToString(dr.Suuryou);

                      m2mKoubaiDataSet.T_NoukiHenkouDataTable dt =
                            NoukiHenkouClass.getT_NoukiHenkouDataTable(strYear, strHacchuuNo, nKubun, nHenkouNo, Global.GetConnection());

                        NoukiHenkouDataSet.HenkouNoukiDataTable dtNoukiHenkou =
                        new NoukiHenkouDataSet.HenkouNoukiDataTable();

                        if (0 < dt.Rows.Count)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                NoukiHenkouDataSet.HenkouNoukiRow drNoukiHenkou =
                                   (NoukiHenkouDataSet.HenkouNoukiRow)dtNoukiHenkou.NewHenkouNoukiRow();

                                drNoukiHenkou.Year = strYear;
                                drNoukiHenkou.HacchuuNo = strHacchuuNo;
                                drNoukiHenkou.JigyoushoKubun = nKubun;
                                drNoukiHenkou.Suuryou = string.Format("{0:F0}", dt[j].Suuryou);
                                drNoukiHenkou.Tourokubi = dt[j].Tourokubi.ToString();
                                drNoukiHenkou.Nouki = Convert.ToString(dt[j].Nouki);
                                drNoukiHenkou.HenkouNo = dt[j].HenkouNo.ToString();

                                drNoukiHenkou.RowNo = dt[j].RowNo.ToString();
                                dtNoukiHenkou.AddHenkouNoukiRow(drNoukiHenkou);
                            }
                            c.Create(dtNoukiHenkou, true);
                        }
                        else
                        {
                            string[] strNoukiAry = strNouki.Split('_');
                            string[] strSuuryouAry = Suuryou.Split('_');

                            NoukiHenkouDataSet.HenkouNoukiRow drNoukiHenkou =
                               (NoukiHenkouDataSet.HenkouNoukiRow)dtNoukiHenkou.NewHenkouNoukiRow();

                            drNoukiHenkou.Year = strYear;
                            drNoukiHenkou.HacchuuNo = strHacchuuNo;
                            drNoukiHenkou.JigyoushoKubun = nKubun;
                            drNoukiHenkou.Suuryou = Suuryou;
                            drNoukiHenkou.Tourokubi = "";
                            drNoukiHenkou.Nouki = strNouki;
                            drNoukiHenkou.HenkouNo = "";

                            dtNoukiHenkou.AddHenkouNoukiRow(drNoukiHenkou);

                            c.Create(dtNoukiHenkou, true);
                        }
                        this.DivNoukiHenkou.Controls.Add(c);
                    }
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DivNoukiHenkou);
                    break;

                case "nyuryoku_close":

                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TbxHenkouNouki);
                    //this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TbxShiteiNouki);
                    // ���e�L�X�g�{�b�N�X�i�[���񓚂̔[���Ɛ��ʂ�ۑ����邽�߁j
                    TbxHenkouNouki.Text = "";
                    // ���e�L�X�g�{�b�N�X�i�[���񓚂̎w��[���{�^����ۑ����邽�߁j
                    //TbxShiteiNouki.Text = "";

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
                        ChumonDataSet.V_Chumon_JyouhouRow drChumon =
                            ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo, nKubun, Global.GetConnection());

                        // �񓚔[��
                        this.m_dvNoukiHenkou = new DataView(this.m_dtNoukiHenkou);
                        this.m_dtNoukiHenkou = NoukiHenkouClass.getT_NoukiHenkouDataTable(Global.GetConnection());
                        this.m_dvNoukiHenkou = new DataView(this.m_dtNoukiHenkou);
                        this.m_dvNoukiHenkou.Sort = "HenkouNo DESC ";

                        DataView dvHenkouNouki = this.m_dvNoukiHenkou;
                        dvHenkouNouki.RowFilter =
                              string.Format("Year = '{0}' AND HacchuuNo = '{1}' ", drChumon.Year, drChumon.HacchuuNo);

                        NoukiHenkouInfo info = null;
                        string strNoukiHenkou = "";
                        this.GetMiNoukiInfo(drChumon, dvHenkouNouki, out info, true, out strNoukiHenkou);

                        if (0 < i)
                        {
                            this.HidHenkouNouki.Value += "\t";
                            this.TbxHenkouNouki.Text += "\t";
                        }
                        TbxHenkouNouki.Text += info.strNoukiHenkouHtml;
                    }

                    break;

                case "nyuryoku_add_row":
                case "nyuryoku_del_row":

                    strChumonKeyArray = strArgs[1].Split('\t');
                    strChumonKey = strChumonKeyArray[0].Split('_');

                    // ��L�[�̎擾
                    strYear = strChumonKey[0];
                    strHacchuuNo = strChumonKey[1];
                    nKubun = int.Parse(strChumonKey[2]);

                    // �[��1\t����2\t����NO\t�[��2\t����2\t�[������NO2�E�E�E
                    string[] strArray = this.HidHenkouNoukiArg.Value.Split('\t');

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

                    CtlNouki kn = this.LoadControl("../CtlNouki.ascx") as CtlNouki;
                    kn.ID = "NKM_" + strYear + '_' + strHacchuuNo + '_' + nKubun;
                    kn.Create(strYear, strHacchuuNo, nKubun, strAryNouki, strSuuryou, strKaitouNo);

                    this.DivNoukiHenkou.Controls.Clear();
                    this.DivNoukiHenkou.Controls.Add(kn);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.DivNoukiHenkou);

                    break;


                case "nouki_kaitou_reg":
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);

                    strChumonKeyArray = strArgs[1].Split('\t');

                    string[] strNoukiHenkouData = this.HidHenkouNoukiArg.Value.Split('|');
                    // ���̓`�F�b�N�p������
                    string strNouKiChkMsg = "";
                    string strSuuryouChkMsg = "";
                    string strErrMsg = "";

                    for (int i = 0; i < strChumonKeyArray.Length; i++)
                    {
                        // ��L�[�̎擾
                        strChumonKey = strChumonKeyArray[i].Split('_');
                        strYear = strChumonKey[0];
                        strHacchuuNo = strChumonKey[1];
                        nKubun = int.Parse(strChumonKey[2]);

                        Hashtable NoukiHenkouTbl =
                            this.splitNoukiHenkouData(strNoukiHenkouData[i]);
                        // �[���`�F�b�N
                        bool chkN = this.NoukiCheck(NoukiHenkouTbl);
                        if (!chkN)
                        {
                            if (strNouKiChkMsg != "")
                                strNouKiChkMsg += ",";
                            strNouKiChkMsg += strHacchuuNo;
                        }
                        // ���ʃ`�F�b�N
                        bool chkS = this.SuuryouCheck(strYear, strHacchuuNo, nKubun, NoukiHenkouTbl);
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

                        if (!drChuumon.IsHenkouNoNull())
                            nHenkouNo = drChuumon.HenkouNo;
                        else
                            nHenkouNo = 0;

                        // �����ԍ��A���敪�����Ƃɔ[���A���ʂ��X�V����
                        err = NoukiHenkouClass.T_NoukiHenkou_Update
                            (strYear, strHacchuuNo, nKubun, NoukiHenkouTbl, nHenkouNo, Global.GetConnection());
                        
                        // �X�V�G���[�`�F�b�N
                        if (err != null)
                        {
                            if (strErrMsg != "")
                                strErrMsg += ",";
                            strErrMsg += strHacchuuNo;
                            continue;
                        }
                        else
                        {
                            // ��L�[�ɂ���āA���[�����M�ɕK�v�f�[�^�擾
                            
                            ChumonDataSet.V_MailInfoDataTable dtMail =
                                ChumonClass.getV_MailInfoDataTable(strYear, strHacchuuNo, nKubun, Global.GetConnection());
                            // �S�Ďd����S���҂Ƀ��[�����M����
                            for (int j = 0; j < dtMail.Rows.Count; j++)
                            {
                                MailClass.MailParam p = this.GetMailParam(dtMail[j]);

                                MailClass.SendMail(p, null);
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
        private Hashtable splitNoukiHenkouData(string strNoukiKaitouData)
        {
            string[] splitKNData = strNoukiKaitouData.Split('\t');

            ArrayList arrayNouki = new ArrayList();
            ArrayList arraySuuryou = new ArrayList();
            ArrayList arrayHenkouNo = new ArrayList();

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
                        arrayHenkouNo.Add(splitKNData[i]);
                        break;
                }
            }

            Hashtable kaitouNoukiTbl = new Hashtable();
            kaitouNoukiTbl.Add("Nouki", arrayNouki);
            kaitouNoukiTbl.Add("Suuryou", arraySuuryou);
            kaitouNoukiTbl.Add("HenkouNo", arrayHenkouNo);

            return kaitouNoukiTbl;
        }


        // ���ʂ��������l���ǂ����`�F�b�N
        private bool SuuryouCheck(string strYear, string strHacchuuNo, int nKubun, Hashtable KaitouNoukiTbl)
        {
            // �����������擾
            // �ŐV�̉�No�擾
            ChumonDataSet.V_Chumon_JyouhouRow drChuumon =
                ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo, nKubun, Global.GetConnection());

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
            if (souInputSuu == drChuumon.Suuryou)
                return true;
            else
                return false;
           
        }
        // �[�����������l���ǂ����`�F�b�N
        private bool NoukiCheck(Hashtable KaitouNoukiTbl)
        {
            // �[���񓚎��̒������͔[�����擾
            ArrayList aryNouki = (ArrayList)KaitouNoukiTbl["Nouki"];
            for (int i = 0; i < aryNouki.Count; i++)
            {
                if (!this.CheckDayFormat(aryNouki[i].ToString()))
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
        // ���M���[�����
        private MailClass.MailParam GetMailParam(ChumonDataSet.V_MailInfoRow dr)
        {
            MailClass.MailParam p = new MailClass.MailParam();
            // ���M�����[���A�h���X
            p._MailFrom = dr.Mail_Y;
            // ���M�惁�[���A�h���X
            p._MailTo = dr.Mail_S;
            // ����
            p._Subject = "�w��[���ύX�̂��ē�";
            // �{��
            p._Body = MailClass.GetBody_NoukiHenkou(dr);
            // SMTP
            p._SMTP_Server = Global.SMTP_Server;
            return p;
        }
    }
}