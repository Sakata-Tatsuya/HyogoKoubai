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

                // 検索リスト作成
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

            // 検索ボタン
            BtnK.Attributes["onclick"] = "Kensaku();";


            // 行数変更
            this.DdlRow.Attributes["onchange"] = "RowChange(); return false;";
            // ----- カレンダー -----
            CtlHacchuubi.SharedCalendar = CtlNouki.SharedCalendar = CtlKNouki.SharedCalendar = CtlNouhinbi.SharedCalendar = this.SC;         
               
        }


        // ページチェンジ
        private void OnPageIndexChanged(int nNewPageIndex)
        {
            VsCurrentPageIndex = nNewPageIndex;
            this.Create();
        }
        // クリエート
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

            this.ShowMsg(dt.Rows.Count + "件", false);
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

            //ページング            
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

                // 現在の表示行(何行目～何行目)
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

        // メッセージ表示
        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }

        // GridView表示
        private void ShowTblMain(bool b)
        {            
            G.Visible = b;
            TblRow.Visible = b;
            this.BtnI.Visible = b;
            BtnI.Visible = b;

        }

        private void SetList()
        {
            // 仕入先
            ListSet.SetDdlShiiresaki_C(DdlShiire);
            //　納入場所
            ListSet.SetDdlNounyuuBasho_C(DdlNBasho);
            // 納期回答状況
            ListSet.SetDdlNoukiKaitouJyoukyou(DdlNKJyoukyou, SessionManager.UserKubun);
            // 納品状況
            ListSet.SetDdlNouhinJyoukyou(DdlNJyoukyou);
            // 部品区分
            ListSet.SetDdlBuhinKubun_C(DdlBuhinKubun,null);
            // 部品
            this.DdlBuhinKubun.Attributes["onchange"] = "OnBuhin(); return false";
            // 発注担当者
            ListSet.SetDdlHacchuTantousha_C(DdlHacchuTantousha);
            // キャンセル日
            ListSet.SetDdlCancel(DdlCancel);
            // メッセージ
            ListSet.SetDdlMsg(DdlMsg);

        }
        private ChumonClass.KensakuParam GetKensakuParam()
        {
            ChumonClass.KensakuParam k = new ChumonClass.KensakuParam();
            // ユーザー区分
            k._userKubun = (byte)SessionManager.UserKubun;
            // 事業所区分（追加09-07-29 呉）
            //k._JigyoushoKubun = SessionManager.JigyoushoKubun;
            // 発注No
            if (TbxHacchuNo.Text != "")
            {
                k._HacchuNo = TbxHacchuNo.Text;
            }
            // 仕入先
            if (DdlShiire.SelectedIndex > 0)
            {
                k._SCode = DdlShiire.SelectedValue;
            }
            // 納入場所
            if (DdlNBasho.SelectedIndex > 0)
            {
                k._NBasho = DdlNBasho.SelectedValue;
            }
            // 納期回答状況
            if (DdlNKJyoukyou.SelectedIndex > 0)
            {
                k._NkJyoukyou = int.Parse(DdlNKJyoukyou.SelectedValue);
            }
            // 納品状況
            if (DdlNJyoukyou.SelectedIndex > 0)
            {
                k._NHJyoukyou = int.Parse(DdlNJyoukyou.SelectedValue);
            }
            // 部品区分
            if (DdlBuhinKubun.SelectedIndex > 0)
            {
                k._Kubun = DdlBuhinKubun.SelectedValue;
            }
            // 部品
            if (DdlBuhin.SelectedIndex > 0)
            {
                k._BuhinCode = DdlBuhin.SelectedValue;
            }
            // 発注担当者
            if (DdlHacchuTantousha.SelectedIndex > 0)
            {
                k._TantoushaCode = DdlHacchuTantousha.SelectedValue;
            }
            // 納期
            Common.CtlNengappiFromTo ctlNouki = FindControl("CtlNouki") as Common.CtlNengappiFromTo;
            if (ctlNouki.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._Nouki = ctlNouki.GetNengappiKikan();
            }

            // 発注日
            Common.CtlNengappiFromTo ctlHacchuubi = FindControl("CtlHacchuubi") as Common.CtlNengappiFromTo;
            if (ctlHacchuubi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._Hacchuubi = ctlHacchuubi.GetNengappiKikan();
            }
            // 納品日
            Common.CtlNengappiFromTo ctlNouhinbi = FindControl("CtlNouhinbi") as Common.CtlNengappiFromTo;
            if (ctlNouhinbi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._NouhinBi = ctlNouhinbi.GetNengappiKikan();
            }
            // 回答納期
            Common.CtlNengappiFromTo ctlKaitouNouki = FindControl("CtlKNouki") as Common.CtlNengappiFromTo;
            if (ctlKaitouNouki.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k._KaitouNouki = ctlKaitouNouki.GetNengappiKikan();
            }
            // キャンセル
            k._Cancelbi = int.Parse(DdlCancel.SelectedValue);
            if (k._Cancelbi == 0)
                b = true;
          
            
            // メッセージ
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

                // 発注書印刷
                HtmlInputCheckBox chk = e.Row.FindControl("ChkI") as HtmlInputCheckBox;
                ChumonClass.ChumonKey key = new ChumonClass.ChumonKey(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);

                // chk.idを格納
                if (HidChkID.Value != "") HidChkID.Value += ",";
                HidChkID.Value += chk.ClientID;

                // キー情報格納
                chk.Value = key.ToString();
                // キャンセル
                if (!dr.IsCancelBiNull())
                {
                    chk.Visible = false;

                    e.Row.Cells[G_CELL_CANCEL].Text = "キャン<br>セル";
                    e.Row.Cells[G_CELL_CANCEL].ForeColor = System.Drawing.Color.Red;
                }

                // 発注NO                
                (e.Row.FindControl("LitHacchuuNo") as Literal).Text = Utility.LinkToHacchuuNo(key.ToString(), dr.HacchuuNo);
                // 発注日
                (e.Row.FindControl("LitHacchuuBi") as Literal).Text = dr.HacchuuBi.ToString("yy/MM/dd");
                // 発注担当者コード                
                (e.Row.FindControl("LitHacchuuTantoushaCode") as Literal).Text = dr.TantoushaCode;
                // 発注担当者名
                (e.Row.FindControl("LitHacchuuTantoushaMei") as Literal).Text = dr.Name;
                // 仕入先コード
                (e.Row.FindControl("LitShiireCode") as Literal).Text = dr.ShiiresakiCode;
                // 仕入先名
                (e.Row.FindControl("LitShiireName") as Literal).Text = dr.ShiiresakiMei;
                // 部品コード
                (e.Row.FindControl("LitCode") as Literal).Text = dr.BuhinKubun + dr.BuhinCode;
                // 部品目名
                (e.Row.FindControl("LitName") as Literal).Text = dr.BuhinMei;
                // 数量
                (e.Row.FindControl("LitSuuryou") as Literal).Text = dr.Suuryou.ToString("#,##0");
                // 単価
                //(e.Row.FindControl("LitTanka") as Literal).Text = "\\" + dr.Tanka.ToString("#,##0.#0");
                // 単価   変更 09/07/28
                if (!dr.KaritankaFlg)
                {
                    (e.Row.FindControl("LitTanka") as Literal).Text = "\\" + dr.Tanka.ToString("#,##0.#0");
                }
                else
                {
                    (e.Row.FindControl("LitTanka") as Literal).Text =
                        string.Format("<font color =\"red\">{0}</font>", "(仮) \\" + dr.Tanka.ToString("#,##0.#0"));
                }
                // 注文金額
                // 増税対応
                decimal dKingaku_Round = Math.Round(dr.Kingaku, 0, MidpointRounding.AwayFromZero);

                // decimal Kingaku = Math.Floor(dr.Suuryou * dr.Tanka);
               // (e.Row.FindControl("LitChumonKingaku") as Literal).Text = "\\" + dr.Kingaku.ToString("#,##");
                // 注文金額   変更 09/07/28
                if (!dr.KaritankaFlg)
                {
                    (e.Row.FindControl("LitChumonKingaku") as Literal).Text = "\\" + dKingaku_Round.ToString("#,##0");
                }
                else
                {
                    (e.Row.FindControl("LitChumonKingaku") as Literal).Text =
                        string.Format("<font color =\"red\">{0}</font>", "\\" + dKingaku_Round.ToString("#,##0"));
                }

                // 増税対応
                decimal Zeigaku = Math.Round(dKingaku_Round * dr.Zeiritu / 100, 0, MidpointRounding.AwayFromZero);
                (e.Row.FindControl("LitZeigaku") as Literal).Text = "\\" + Zeigaku.ToString("#,##0");

                // 単位
                e.Row.Cells[G_CELL_TANI].Text = dr.Tani;
                // 納入場所
                e.Row.Cells[G_CELL_NOUNYUU_BASHO].Text = dr.BashoMei;
                // 納期変更
                DataView dvNouki = this.m_dvNoukiHenkou;
                dvNouki.RowFilter = string.Format("Year = '{0}' AND HacchuuNo = '{1}' AND JigyoushoKubun = '{2}'", dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);

                NoukiHenkouInfo infoNoki = null;
                // 保存用納期
                string strNoukiHenkou = "";
                if (dr.IsHenkouShouninFlgNull())
                {
                    this.GetNoukiHenkouNullInfo(dr, out infoNoki, out strNoukiHenkou);
                }
                else if (!dr.IsHenkouShouninFlgNull() && dr.HenkouShouninFlg)
                {
                    // 変更納期が承認済の場合
                    this.GetNoukiInfo(dr, dvNouki, out infoNoki, true, out strNoukiHenkou);
                }
                else
                {
                    // 変更納期が未承認の場合
                    this.GetMiNoukiInfo(dr, dvNouki, out infoNoki, true, out strNoukiHenkou);
                }


                CtlNouki k = this.LoadControl("../CtlNouki.ascx") as CtlNouki;
                k.ID = "NKM_" + dr.Year + "_" + dr.HacchuuNo + "_" + dr.JigyoushoKubun;

                this.DivNoukiHenkou.Controls.Add(k);

                this.m_objStringCols["HenkouNoukiData"].Add(
                    string.Format("{0}_{1}_{2}\t{3}\t{4}\t{5}",
                    dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, k.HidNouki.ClientID, k.HidSuuryou.ClientID, k.HidKaitouNo.ClientID));

                this.DivNoukiHenkou.Controls.Clear();


                // 最新の変更納期
                Label lblNyuryoku = (Label)e.Row.Cells[G_CELL_NOUKI_HENKOU].FindControl("LblNouki");
                lblNyuryoku.Text = infoNoki.strNoukiHenkouHtml;


                // 回答納期
                this.m_dvNoukiKaitou.RowFilter = string.Format("Year = '{0}' AND HacchuuNo = '{1}' AND JigyoushoKubun = '{2}' ", dr.Year, dr.HacchuuNo, dr.JigyoushoKubun);

                NoukiKaitouInfo info = null;
                // 納期回答が承認済の場合                
                if (!dr.IsKaitouShouninFlgNull() && dr.KaitouShouninFlg)
                {
                    this.GetNoukiKaitouInfo(dr, m_dvNoukiKaitou, out info);
                }
                else
                {
                    // 納期回答が未承認の場合
                    this.GetMiNoukiKaitouInfo(dr, m_dvNoukiKaitou, out info);
                }
                e.Row.Cells[G_CELL_KAITOU_NOUKI].Text = info.strNoukiKaitouHtml;

                // 納期回答数量が注文数量より少ないか、回答納期が納期より遅れた場合、背景色変更
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
                // 納品日
                if (!dr.IsNouhinNoNull())
                {
                    m2mKoubaiDataSet.T_NouhinDataTable dt = NouhinClass_N.getT_NouhinDataTable(dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, Global.GetConnection());
                    string strNouhinbi = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strNouhinbi +=
                            string.Format("{0} {1}:{2}<br>", dt[i].NouhinBi.ToString("yy/MM/dd"),
                                     "数量", dt[i].Suuryou.ToString("#,##0"));
                    }
                    e.Row.Cells[G_CELL_NOUHINBI].Text = strNouhinbi;
                }
                // メッセージ
                HtmlInputButton btnM = e.Row.FindControl("BM") as HtmlInputButton;
                btnM.Attributes["onclick"] = string.Format("Msg('{0}');", key.ToString());

                if (!dr.IsOpenedFlgNull())
                {
                    if (dr.OpenedFlg)
                        btnM.Value = "履歴";
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
        //納期変更無
        private void GetNoukiHenkouNullInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, out NoukiHenkouInfo info, out string strNouki)
        {
            info = new NoukiHenkouInfo();

            info.strNoukiHenkouHtml = string.Format("{0}<br>", Utility.FormatToyyMMdd(dr.Nouki.ToString()));

            info.strNoukiHenkouHtml += string.Format(
                   "<a href=\"javascript:void(0);\" onclick=\"YN('{0}_{1}_{2}');\"><font color =\"blue\">{3}</font></a>",
                    dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, "入力欄表示");
            
            strNouki = dr.Nouki;

        }
        // 納期変更が未承認の場合      
        private void GetMiNoukiInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, DataView dvHN,
                                        out NoukiHenkouInfo info, bool bCreateHenkou, out string strNouki)
        {

            info = new NoukiHenkouInfo();
            string[] strNoukiAry = null;
            string str = "";
            if (dvHN.Count > 0)
            {
                strNoukiAry = new string[dvHN.Count];

                // 保存用変更納期No
                int nHenkouNo = 0;

                // 保存用変更納期
                string strNouki1 = "";
                string strNouki2 = "";

                // すべての変更納期情報データ取得
                for (int i = 0; i < dvHN.Count; i++)
                {
                    if (i == 0 || nHenkouNo == int.Parse(dvHN[i]["HenkouNo"].ToString()))
                    {
                        // 最新登録の場合赤字で表示                           
                        strNouki1 +=
                             string.Format("<font color = \"red\"> {0} {1}:{2}<br>", Utility.FormatToyyMMdd(dvHN[i]["Nouki"].ToString()),
                                "数量", dvHN[i]["Suuryou"].ToString());

                        nHenkouNo = int.Parse(dvHN[i]["HenkouNo"].ToString());

                        if (dvHN.Count - 1 == i)
                        {
                            strNouki1 +=
                                         string.Format("({0} {1})<br> ", Utility.FormatToyyMMddHHmm(dvHN[i]["Tourokubi"].ToString()), "登録");
                        }
                        else if (i + 1 < dvHN.Count)
                        {
                            if (nHenkouNo != int.Parse(dvHN[i + 1]["HenkouNo"].ToString()))
                            {
                                strNouki1 +=
                                    string.Format("<font color = \"red\">({0} {1})</font><br> ",
                                   Utility.FormatToyyMMddHHmm(dvHN[i]["Tourokubi"].ToString()), "登録");
                            }
                        }

                        if (str != "") str += ",";
                        str += dvHN[i]["Nouki"].ToString();
                    }
                    else if (nHenkouNo - 1 == int.Parse(dvHN[i]["HenkouNo"].ToString()))
                    {
                        strNouki2 +=
                                string.Format("{0} {1}:{2}<br>", Utility.FormatToyyMMdd(dvHN[i]["Nouki"].ToString()),
                                                "数量", dvHN[i]["Suuryou"].ToString());

                        if ((i > 0 && i == dvHN.Count - 1) || ((nHenkouNo - 1) != int.Parse(dvHN[i + 1]["HenkouNo"].ToString())))
                        {
                            strNouki2 +=
                                string.Format("({0} {1})<br> ",
                                Utility.FormatToyyMMddHHmm(dvHN[i]["Tourokubi"].ToString()), "登録");
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

            // 変更納期
            info.strNoukiHenkouHtml = Utility.ToInnerRowsHTML_NoLine(strNoukiAry);

            strNouki = str;
            if (bCreateHenkou)
            {

                info.strNoukiHenkouHtml += string.Format(
                    "<a href=\"javascript:void(0);\" onclick=\"YN('{0}_{1}_{2}');\"><font color =\"blue\">{3}</font></a>",
                     dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, "入力欄表示");
            }



        }
        // 変更納期が承認済の場合
        private void GetNoukiInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, DataView dvHN,
                                    out NoukiHenkouInfo info, bool bCreateHenkou, out string strNoukiHenkou)
        {
            info = new NoukiHenkouInfo();
            string[] strNoukiAry = null;
            string str = "";
            strNoukiAry = new string[dvHN.Count];

            // 変更No保存用
            int nHenkouNo = 0;
            // 変更納期保存用
            string strNouki = "";

            // すべての回答納期情報データ取得
            for (int i = 0; i < dvHN.Count; i++)
            {
                if ((dr.HenkouShouninFlg && i == 0) ||
                    nHenkouNo == int.Parse(dvHN[i]["HenkouNo"].ToString()))
                {
                    // 納期回答が承認済の場合黒字で表示
                    strNouki +=
                         string.Format("{0} {1}:{2}<br>",
                                Utility.FormatToyyMMdd(dvHN[i]["Nouki"].ToString()), "数量", dvHN[i]["Suuryou"].ToString());
                    // 変更No
                    nHenkouNo = int.Parse(dvHN[i]["HenkouNo"].ToString());

                    if (dvHN.Count - 1 == i || nHenkouNo != int.Parse(dvHN[i + 1]["HenkouNo"].ToString()))
                    {
                        strNouki +=
                                     string.Format("({0} {1})<br> ",
                                     Utility.FormatToyyMMddHHmm(dvHN[i]["Tourokubi"].ToString()), "登録");
                    }
                    if (str != "") str += ",";
                    str += dvHN[i]["Nouki"].ToString();

                }
                else
                    break;

                strNoukiAry[0] = strNouki;

            }
            // 納期回答の納期と数量
            info.strNoukiHenkouHtml = Utility.ToInnerRowsHTML_NoLine(strNoukiAry);
            strNoukiHenkou = str;

            if (bCreateHenkou)
            {
                info.strNoukiHenkouHtml += string.Format(
                        "<a href=\"javascript:void(0);\" onclick=\"YN('{0}_{1}_{2}');\"><font color =\"blue\">{3}</font></a>",
                     dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, "入力欄表示");
            }
        }
        // 回答納期
        private class NoukiKaitouInfo
        {
            public string strNoukiKaitouHtml = "";
        }
        // 変更納期
        private class NoukiHenkouInfo
        {
            public string strNoukiHenkouHtml = "";
        }
        // メッセージボタンのテキスト表示
        private string BtnMsgText(Byte mFlag)
        {
            string strText = "";
            if (mFlag == (byte)UserKubun.Owner)
                strText = "送信";
            else
                strText = "受信";
            return strText;
        }
        // 回答納期未承認
        private void GetMiNoukiKaitouInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, DataView dvNK, out NoukiKaitouInfo info)
        {
            info = new NoukiKaitouInfo();
            string[] strNKaitouAry = new string[dvNK.Count];

            if (0 < dvNK.Count)
            {
                // 保存用回答No
                int nKaitouNo = 0;
                // 保存用納期回答
                string strNoukiKaitou = "";
                string strNoukiKaitou2 = "";

                for (int i = 0; i < dvNK.Count; i++)
                {
                    if (i == 0 || nKaitouNo == int.Parse(dvNK[i]["KaitouNo"].ToString()))
                    {

                        // 初回登録の場合黒字で表示
                        if (int.Parse(dvNK[i]["KaitouNo"].ToString()) == 1)
                            strNoukiKaitou += string.Format("{0} {1}:{2:N0}</font><br>",
                                                            Utility.FormatToyyMMdd(dvNK[i]["Nouki"].ToString()),
                                                            "数量", dvNK[i]["Suuryou"]);
                        else
                        {
                            // 最新登録の場合赤字で表示
                            strNoukiKaitou += string.Format("<font color = \"red\">{0} {1}:{2:N0}</font><br>",
                                                            Utility.FormatToyyMMdd(dvNK[i]["Nouki"].ToString()),
                                                            "数量", dvNK[i]["Suuryou"]);
                            //
                        }
                        // 保存用回答No
                        nKaitouNo = int.Parse(dvNK[i]["KaitouNo"].ToString());

                        if (dvNK.Count - 1 == i)
                        {
                            strNoukiKaitou +=
                                         string.Format("({0} {1})<br> ",
                                         Utility.FormatToyyMMddHHmm(dvNK[i]["Tourokubi"].ToString()), "登録");
                        }
                        else if (i + 1 < dvNK.Count)
                        {
                            if (nKaitouNo != int.Parse(dvNK[i + 1]["KaitouNo"].ToString()))
                            {
                                strNoukiKaitou +=
                                    string.Format("<font color = \"red\">({0} {1})</font><br> ",
                                    Utility.FormatToyyMMddHHmm(dvNK[i]["Tourokubi"].ToString()), "登録");
                            }
                        }




                    }
                    else if (nKaitouNo - 1 == int.Parse(dvNK[i]["KaitouNo"].ToString()))
                    {
                        strNoukiKaitou2 +=
                            string.Format("{0} {1}:{2:N0}</font><br>",
                                                            Utility.FormatToyyMMdd(dvNK[i]["Nouki"].ToString()),
                                                            "数量", dvNK[i]["Suuryou"]);


                        if ((i > 0 && i == dvNK.Count - 1) || ((nKaitouNo - 1) != int.Parse(dvNK[i + 1]["KaitouNo"].ToString())))
                        {
                            strNoukiKaitou2 +=
                                string.Format("({0} {1})<br> ",
                                Utility.FormatToyyMMddHHmm(dvNK[i]["Tourokubi"].ToString()), "登録");
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
                info.strNoukiKaitouHtml += string.Format("<font color = \"red\">{0}<br></font>", "未回答");
            if (!dr.IsKaitouShouninFlgNull() && !dr.KaitouShouninFlg && dr.KaitouNo >= 1)
            {
                info.strNoukiKaitouHtml +=
                    string.Format("<input type=button class='f8' onclick=Shounin('{0}_{1}_{2}_{3}_{4}'); return false;  value={5} />",
                    dr.Year, dr.HacchuuNo, dr.JigyoushoKubun, dr.ShiiresakiCode, dr.KaitouNo, "承認");


            }
        }
        // 回答納期承認済
        private void GetNoukiKaitouInfo(ChumonDataSet.V_Chumon_JyouhouRow dr, DataView dvNK,
                                       out NoukiKaitouInfo info)
        {
            info = new NoukiKaitouInfo();
            string[] strNKaitouAry = new string[dvNK.Count];

            if (0 < dvNK.Count)
            {
                // 保存用回答No
                int nKaitouNo = 0;
                // 保存用納期回答
                string strNoukiKaitou = "";

                for (int i = 0; i < dvNK.Count; i++)
                {
                    // 最新登録の場合赤字で表示
                    if (i == 0 || nKaitouNo == int.Parse(dvNK[i]["KaitouNo"].ToString()))
                    {
                        strNoukiKaitou += string.Format("{0} {1}:{2:N0}<br>",
                           Utility.FormatToyyMMdd(dvNK[i]["Nouki"].ToString()),
                                                          "数量", dvNK[i]["Suuryou"]);
                        // 保存用回答No
                        nKaitouNo = int.Parse(dvNK[i]["KaitouNo"].ToString());

                        if (dvNK.Count - 1 == i || nKaitouNo != int.Parse(dvNK[i + 1]["KaitouNo"].ToString()))
                        {
                            strNoukiKaitou +=
                                         string.Format("({0}{1})<br> ",
                                         Utility.FormatToyyMMddHHmm(dvNK[i]["Tourokubi"].ToString()), "登録");
                        }
                    }
                    else
                        break;
                }
                strNKaitouAry[0] = strNoukiKaitou;
            }
            info.strNoukiKaitouHtml = Utility.ToInnerRowsHTML_NoLine(strNKaitouAry);

            if (info.strNoukiKaitouHtml == "")
                info.strNoukiKaitouHtml += string.Format("<font color = \"red\">{0}<br></font>", "未回答");


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

            // 納期
            string strNouki = "";
            string Suuryou = "";

            // 納期回答No
            // int nKaitouNo = 0;
            // 納期変更No
            int nHenkouNo = 0;
            //
            //int nSuuryou = 0;

            switch (strCmd)
            {
                case "page":

                    // ページ切り替え
                    this.VsCurrentPageIndex = int.Parse(strArgs[1]);
                    this.Create();
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    break;

                case "kensaku":
                    // 検索
                    this.VsCurrentPageIndex = 0;
                    this.Create();
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    break;

                case "row":
                case "reload":
                    // 行数変更
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
                    this.ShowMsg("承認しました", false);
                    this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);
                    break;

                case "nyuryoku_open":
                    // 納期調整コントロールの表示
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

                        // 最新の回答No取得
                        ChumonDataSet.V_Chumon_JyouhouRow dr =
                            ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo, nKubun, Global.GetConnection());

                        // 　
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
                    // 仮テキストボックス（納期回答の納期と数量を保存するため）
                    TbxHenkouNouki.Text = "";
                    // 仮テキストボックス（納期回答の指定納期ボタンを保存するため）
                    //TbxShiteiNouki.Text = "";

                    // 行数の取得
                    strChumonKeyArray = strArgs[1].Split('\t');

                    for (int i = 0; i < strChumonKeyArray.Length; i++)
                    {

                        // 主キーの取得
                        strChumonKey = strChumonKeyArray[i].Split('_');
                        strYear = strChumonKey[0];
                        strHacchuuNo = strChumonKey[1];
                        nKubun = int.Parse(strChumonKey[2]);



                        // 最新の回答No取得
                        ChumonDataSet.V_Chumon_JyouhouRow drChumon =
                            ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo, nKubun, Global.GetConnection());

                        // 回答納期
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

                    // 主キーの取得
                    strYear = strChumonKey[0];
                    strHacchuuNo = strChumonKey[1];
                    nKubun = int.Parse(strChumonKey[2]);

                    // 納期1\t数量2\t調整NO\t納期2\t数量2\t納期調整NO2・・・
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
                        // 行の追加の場合は、一番最後に追加
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
                    // 入力チェック用文字列
                    string strNouKiChkMsg = "";
                    string strSuuryouChkMsg = "";
                    string strErrMsg = "";

                    for (int i = 0; i < strChumonKeyArray.Length; i++)
                    {
                        // 主キーの取得
                        strChumonKey = strChumonKeyArray[i].Split('_');
                        strYear = strChumonKey[0];
                        strHacchuuNo = strChumonKey[1];
                        nKubun = int.Parse(strChumonKey[2]);

                        Hashtable NoukiHenkouTbl =
                            this.splitNoukiHenkouData(strNoukiHenkouData[i]);
                        // 納期チェック
                        bool chkN = this.NoukiCheck(NoukiHenkouTbl);
                        if (!chkN)
                        {
                            if (strNouKiChkMsg != "")
                                strNouKiChkMsg += ",";
                            strNouKiChkMsg += strHacchuuNo;
                        }
                        // 数量チェック
                        bool chkS = this.SuuryouCheck(strYear, strHacchuuNo, nKubun, NoukiHenkouTbl);
                        if (!chkS)
                        {
                            if (strSuuryouChkMsg != "")
                                strSuuryouChkMsg += ",";
                            strSuuryouChkMsg += strHacchuuNo;
                        }
                        if (chkN == false || chkS == false)
                            continue;

                        // 最新の回答Noと行数取得
                        // 最新の回答No取得
                        ChumonDataSet.V_Chumon_JyouhouRow drChuumon =
                            ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo, nKubun, Global.GetConnection());

                        if (!drChuumon.IsHenkouNoNull())
                            nHenkouNo = drChuumon.HenkouNo;
                        else
                            nHenkouNo = 0;

                        // 注文番号、情報区分をもとに納期、数量を更新する
                        err = NoukiHenkouClass.T_NoukiHenkou_Update
                            (strYear, strHacchuuNo, nKubun, NoukiHenkouTbl, nHenkouNo, Global.GetConnection());
                        
                        // 更新エラーチェック
                        if (err != null)
                        {
                            if (strErrMsg != "")
                                strErrMsg += ",";
                            strErrMsg += strHacchuuNo;
                            continue;
                        }
                        else
                        {
                            // 主キーによって、メール送信に必要データ取得
                            
                            ChumonDataSet.V_MailInfoDataTable dtMail =
                                ChumonClass.getV_MailInfoDataTable(strYear, strHacchuuNo, nKubun, Global.GetConnection());
                            // 全て仕入先担当者にメール送信する
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
                                "[注文番号]" + strNouKiChkMsg + "の納期が不正です";
                        }
                        if (strSuuryouChkMsg != "")
                        {
                            if (strNouKiChkMsg != "") msg += "<br>";
                            msg +=
                                "[注文番号]" + strSuuryouChkMsg + "の数量が不正です";
                        }
                        if (strErrMsg != "")
                        {
                            msg +=
                                "[注文番号]" + strErrMsg + "の更新に失敗しました";
                        }
                        this.ShowMsg(msg, true);
                        return;
                    }

                    this.ShowMsg("更新しました", false);

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


        // 数量が正しい値かどうかチェック
        private bool SuuryouCheck(string strYear, string strHacchuuNo, int nKubun, Hashtable KaitouNoukiTbl)
        {
            // 総注文数を取得
            // 最新の回答No取得
            ChumonDataSet.V_Chumon_JyouhouRow drChuumon =
                ChumonClass.getV_Chumon_JyouhouRow(strYear, strHacchuuNo, nKubun, Global.GetConnection());

            // 納期回答時の注文入力数量を取得
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
                    // 数値書式でない場合不正
                    return false;
                }
            }

            // 総注文数と注文入力数量が異なっていたら不正
            if (souInputSuu == drChuumon.Suuryou)
                return true;
            else
                return false;
           
        }
        // 納期が正しい値かどうかチェック
        private bool NoukiCheck(Hashtable KaitouNoukiTbl)
        {
            // 納期回答時の注文入力納期を取得
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
            // 文字数チェック
            if (yyMMdd.Length != 8)
                return false;
            else
                return true;          
        }
        // 送信メール情報
        private MailClass.MailParam GetMailParam(ChumonDataSet.V_MailInfoRow dr)
        {
            MailClass.MailParam p = new MailClass.MailParam();
            // 送信元メールアドレス
            p._MailFrom = dr.Mail_Y;
            // 送信先メールアドレス
            p._MailTo = dr.Mail_S;
            // 件名
            p._Subject = "指定納期変更のご案内";
            // 本文
            p._Body = MailClass.GetBody_NoukiHenkou(dr);
            // SMTP
            p._SMTP_Server = Global.SMTP_Server;
            return p;
        }
    }
}