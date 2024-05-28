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
namespace m2mKoubai.Denpyou
{
    public partial class KenshuMeisaihyoForm : System.Web.UI.Page
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

                string strKey = "";
                try
                {
                    // 検索条件をセットする
                    strKey = HttpContext.Current.Request.Form["HidKey"];
                }
                catch
                {

                    return;
                }
                // 検収明細表
                this.Create(strKey);
            }
        }
        decimal nGoukei = 0;
        decimal nGoukei_Tax = 0;
        decimal dZeiRitsu = 0;
        private void Create(string strKey)
        {

            m2mKoubaiDataSet.M_ShiiresakiRow drShiire =
                ShiiresakiClass.getM_ShiiresakiRow(SessionManager.KaishaCode, Global.GetConnection());
            if (drShiire == null)
            {
                ShowMsg(AppCommon.NO_DATA, true);
                return;
            }

            int nFrom = 0;
            int nTo = 0;
            int nYear = int.Parse(strKey.Substring(0, 4));
            int nMonth = int.Parse(strKey.Substring(4, 2));

            AppCommon.CreateKikan(nYear, nMonth,
                drShiire.ShiharaiShimebi, ref nFrom, ref nTo);
            // 条件
            KenshuClass.KensakuParam k = this.GetKensakuParam(nFrom, nTo);
            if (k == null)
            {
                this.ShowMsg("", true);
                return;
            }

            // 
            KenshuDataSet.V_KenshuDataTable dt =
              KenshuClass.getV_Kenshu2DataTable(k, Global.GetConnection());

           
            // 事業所配列
            ArrayList aryKubun = new ArrayList();
            int nKubun = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt[i].JigyoushoKubun != nKubun)
                {
                    nKubun = dt[i].JigyoushoKubun;
                    aryKubun.Add(dt[i].JigyoushoKubun);
                }
            }

            for (int nKubunCnt = 0; nKubunCnt < aryKubun.Count; nKubunCnt++)
            {
                nGoukei = 0;
                nGoukei_Tax = 0;

                // ヘッダ
                ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader =
                   ShiiresakiClass.getV_Nouhinsho_HeaderRow
                   (SessionManager.LoginID, int.Parse(aryKubun[nKubunCnt].ToString()), Global.GetConnection());

                CtlKenshuMeisaihyo_H ctlHeader = LoadControl("CtlKenshuMeisaihyo_H.ascx") as CtlKenshuMeisaihyo_H;
                ctlHeader.Create(drHeader, strKey);

                this.T.Rows[0].Cells[0].Controls.Add(ctlHeader);

                // G DataBind
                KenshuDataSet.V_KenshuBindDataTable dtBind =
                 new KenshuDataSet.V_KenshuBindDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt[i].JigyoushoKubun.ToString() == aryKubun[nKubunCnt].ToString())
                    {
                        KenshuDataSet.V_KenshuBindRow drBind =
                            dtBind.NewV_KenshuBindRow();
                        drBind.Year = dt[i].Year;
                        drBind.HacchuuBi = dt[i].HacchuuBi;
                        drBind.HacchuuNo = dt[i].HacchuuNo;
                        drBind.NouhinNo = dt[i].NouhinNo;
                        drBind.ChumonSuuryou = dt[i].ChumonSuuryou;
                        drBind.Suuryou = dt[i].NouhinSuuryou;
                        drBind.ShiiresakiCode = dt[i].ShiiresakiCode;
                        drBind.BuhinKubun = dt[i].BuhinKubun;
                        drBind.BuhinCode = dt[i].BuhinCode;
                        drBind.BuhinMei = dt[i].BuhinMei;
                        drBind.Tanka = dt[i].Tanka;
                        drBind.Tani = dt[i].Tani;
                        drBind.BashoMei = dt[i].BashoMei;
                        drBind.NouhinBi = dt[i].NouhinBi;

                        dtBind.AddV_KenshuBindRow(drBind);

                        // 増税対応 小数切り捨てではなく四捨五入に変更
                        //nGoukei += (int)Math.Floor(drBind.Tanka * drBind.Suuryou);

                        decimal dGoukei = Math.Round(drBind.Tanka * drBind.Suuryou, 0, MidpointRounding.AwayFromZero);
                        int Zeiritu = dt[i].Zeiritu;
                        //decimal ZeiGaku = Math.Round((dGoukei * Zeiritu) / 100, 0, MidpointRounding.AwayFromZero);
                        decimal ZeiGaku = dGoukei * Zeiritu / 100;

                        nGoukei += dGoukei;
                        nGoukei_Tax += ZeiGaku;
                    }
                }

                /* 2014/04/07 石岡
                 * 消費税合計の計算を明細毎の消費税額を
                 * 合算した後に四捨五入するよう変更
                 */
                nGoukei_Tax = Math.Round(nGoukei_Tax, 0, MidpointRounding.AwayFromZero);

                //　総行数
                int nMaxRowsCount = dtBind.Rows.Count;
                //　1ページ目の行数
                int nFirstPageRow = int.Parse(Global.Nouhinsho_FirstPageRow);
                //　２ページ目以降の行数
                int nElsePageRow = int.Parse(Global.Nouhinsho_ElsePageRow);

                // 　総ページ数
                int nPageCount = 0;
                //　現在の行数
                int nNowRowCount = 0;
                //  ページ数決定
                if (nMaxRowsCount < nFirstPageRow)
                {
                    nPageCount = 1;
                }
                else
                {
                    int nUseRowCount = nMaxRowsCount - nFirstPageRow;

                    nPageCount = 1;
                    nPageCount += nUseRowCount / nElsePageRow;
                    if (nUseRowCount % nElsePageRow != 0)
                        nPageCount++;
                }
                for (int nPageCnt = 0; nPageCnt < nPageCount; nPageCnt++)
                {
                    CtlA4 ctlA4 = LoadControl("CtlA4.ascx") as CtlA4;
                    ArrayList ary = new ArrayList();

                    int RowCount = 0;
                    if (nPageCnt == 0)
                        RowCount = nFirstPageRow;
                    else
                        RowCount = nElsePageRow;
                    for (int j = nNowRowCount; j < nNowRowCount + RowCount; j++)
                    {
                        // データがある行を追加
                        if (j < dtBind.Rows.Count)
                        {
                            ary.Add(dtBind.Rows[j]);
                        }
                        // ない場合は空の行を追加                    
                        else
                        {
                            ary.Add(dtBind.NewV_KenshuBindRow());
                        }
                    }

                    KenshuDataSet.V_KenshuBindRow[] drAry =
                       new KenshuDataSet.V_KenshuBindRow[ary.Count];

                    nNowRowCount += drAry.Length;
                    ary.CopyTo(drAry);
                    //明細   

                    CtlKenshuMeisaihyo_M ctlMeisai = LoadControl("CtlKenshuMeisaihyo_M.ascx") as CtlKenshuMeisaihyo_M;
                    ctlMeisai.Create(drAry);
                   
                    this.T.Rows[0].Cells[0].Controls.Add(ctlA4);
                    ctlA4.Table.Rows[0].Cells[0].Controls.Add(ctlMeisai);



                    if (nPageCnt < nPageCount - 1)
                    {
                        ctlA4.Table.Style.Add("page-break-after", "always");
                    }
                    else if (nPageCnt == nPageCount - 1)
                    {
                        //合計テーブル
                        // フッター
                        // 消費税
                        // 消費税率
                        //dZeiRitsu = (decimal.Parse(Global.ShouhiZei) / 100);
                        // 消費税
                        //int nShohizei = (int)Math.Floor(nGoukei * dZeiRitsu);
                        int nShohizei = (int)nGoukei_Tax;

                        CtlJyuryousho_F ctlFooter = LoadControl("CtlJyuryousho_F.ascx") as CtlJyuryousho_F;
                        ctlFooter.Create((int)nGoukei, nShohizei);

                        this.T.Rows[0].Cells[0].Controls.Add(ctlA4);
                        ctlA4.Table.Rows[0].Cells[0].Controls.Add(ctlFooter);

                        if (nKubunCnt != aryKubun.Count - 1)
                        {
                            ctlA4.Table.Style.Add("page-break-after", "always");
                        }
                    }
                    
                }
            }
        }
        // メッセージ表示
        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }
        private KenshuClass.KensakuParam GetKensakuParam(int nfrom, int nto)
        {
            KenshuClass.KensakuParam k = new KenshuClass.KensakuParam();

            k._FromDate = nfrom.ToString();
            k._ToDate = nto.ToString();
            // 納品年月
            //k._NouhinYearMonth = strKey;

            // 仕入先
            k._SCode = SessionManager.KaishaCode;

            //k._Shimebi = nShimebi;

            return k;
        }
    }
}
