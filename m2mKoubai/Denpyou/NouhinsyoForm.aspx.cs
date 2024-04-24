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
    public partial class NouhinsyoForm : Core.Web.ServerViewStatePage
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
                try
                {
                    // 検索条件をセットする
                    string strKey = HttpContext.Current.Request.Form["HidKey"];
                  
                    // 納品書明細
                    this.Create(strKey);
                }
                catch
                {
                    return;
                }
            }
        }

     
        int nGoukei;
        private void Create(string key)
        {
            // キーによって、印刷する納品書明細を取得
            ChumonDataSet.V_Chumon_MeisaiDataTable dt = ChumonClass.getV_Chumon_MeisaiDataTable(key, Global.GetConnection());

            if (dt.Rows.Count == 0)
            {
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
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

            // 事業所ことで、納品書を印刷する
            for (int nKubunCnt = 0; nKubunCnt < aryKubun.Count; nKubunCnt++)
            {
                nGoukei = 0;
                // 納品書ヘッダー
                ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader = ShiiresakiClass.getV_Nouhinsho_HeaderRow(SessionManager.LoginID,int.Parse(aryKubun[nKubunCnt].ToString()), Global.GetConnection());
                if (drHeader != null)
                {
                    CtlNouhinsho_H c = LoadControl("CtlNouhinsho_H.ascx") as CtlNouhinsho_H;
                    c.Create(drHeader);
                    this.T.Rows[0].Cells[0].Controls.Add(c);
                }

                //G.DataSourceテーブル
                ChumonDataSet.V_Nouhinsho_MeisaiDataTable dtBind =
                    new ChumonDataSet.V_Nouhinsho_MeisaiDataTable();

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt[j].JigyoushoKubun.ToString() == aryKubun[nKubunCnt].ToString())
                    {
                        // NewBindRow()
                        ChumonDataSet.V_Nouhinsho_MeisaiRow drBind = dtBind.NewV_Nouhinsho_MeisaiRow();

                        drBind.HacchuuNo = dt[j].HacchuuNo;
                        drBind.Hacchuubi = dt[j].HacchuuBi.ToString("yy/MM/dd");
                        drBind.BuhinKubun = dt[j].BuhinKubun;
                        drBind.BuhinCode = dt[j].BuhinCode;
                        drBind.BuhinMei = dt[j].BuhinMei;
                        drBind.Suuryou = dt[j].Suuryou;
                        drBind.Tani = dt[j].Tani;
                        drBind.Tanka = dt[j].Tanka;
                        // 増税対応
                        drBind.Kingaku = (int)Math.Round(dt[j].Suuryou * dt[j].Tanka, 0, MidpointRounding.AwayFromZero);
                        //drBind.Kingaku = (int)Math.Floor(dt[j].Suuryou * dt[j].Tanka);
                        nGoukei += drBind.Kingaku;
                        drBind.BarCode = dt[j].HacchuuNo;

                        dtBind.AddV_Nouhinsho_MeisaiRow(drBind);
                    }
                }

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

                    // 現在の行数＋1ページ分の行数分をループ
                    for (int j = nNowRowCount; j < nNowRowCount + RowCount; j++)
                    {
                        // データがある行？を配列に追加
                        if (j < dtBind.Rows.Count)
                        {
                            ary.Add(dtBind.Rows[j]);
                        }
                        // 空の行を追加
                        else
                        {
                            ary.Add(dtBind.NewV_Nouhinsho_MeisaiRow());
                        }

                    }

                    ChumonDataSet.V_Nouhinsho_MeisaiRow[] drAry =
                        new ChumonDataSet.V_Nouhinsho_MeisaiRow[ary.Count];

                    nNowRowCount += drAry.Length;
                    ary.CopyTo(drAry);

                    //明細             
                    CtlNouhinsho_M ctlMeisai = LoadControl("CtlNouhinsho_M.ascx") as CtlNouhinsho_M;
                    ctlMeisai.Create(drAry);

                    this.T.Rows[0].Cells[0].Controls.Add(ctlA4);
                    ctlA4.Table.Rows[0].Cells[0].Controls.Add(ctlMeisai);

                   
                   
                    // 納品書の改頁
                    if (nPageCnt < nPageCount - 1 )
                    {
                        ctlA4.Table.Style.Add("page-break-after", "always");
                    }
                    else if (nPageCnt == nPageCount - 1)
                    {
                        //合計テーブル
                        // フッター
                        // 消費税
                        // 消費税率
                        decimal dZeiRitsu = (decimal.Parse(Global.ShouhiZei) / 100);
                        // 消費税
                        int nShohizei = (int)Math.Floor(nGoukei * dZeiRitsu);

                        CtlNouhinsho_F ctlNouhinsho = LoadControl("CtlNouhinsho_F.ascx") as CtlNouhinsho_F;
                        ctlNouhinsho.Create((int)nGoukei, nShohizei);

                        this.T.Rows[0].Cells[0].Controls.Add(ctlA4);
                        ctlA4.Table.Rows[0].Cells[0].Controls.Add(ctlNouhinsho);

                        if (nKubunCnt <= aryKubun.Count - 1 )
                        {
                            ctlA4.Table.Style.Add("page-break-after", "always");
                        }
                    }
                 
                }
            }


            // 受領書         
            // 事業所ことで、受領書を印刷する
            for (int nKubunCnt = 0; nKubunCnt < aryKubun.Count; nKubunCnt++)
            {
                // 受領書ヘッダ
                ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeaderJ = ShiiresakiClass.getV_Nouhinsho_HeaderRow(SessionManager.LoginID, int.Parse(aryKubun[nKubunCnt].ToString()), Global.GetConnection());

                if (drHeaderJ != null)
                {
                    CtlJyuryousho_H c = LoadControl("CtlJyuryousho_H.ascx") as CtlJyuryousho_H;
                    c.Create(drHeaderJ);
                    this.T.Rows[0].Cells[0].Controls.Add(c);
                }
                // キーによって、印刷する受領書明細を取得
                KenshuDataSet.V_KenshuDataTable dtMeisai = KenshuClass.getV_Kenshu_MeisaiDataTable(key, Global.GetConnection());

                // G.DataSourceテーブル
                ChumonDataSet.V_Jyuryosho_MeisaiDataTable dtBindJ = new ChumonDataSet.V_Jyuryosho_MeisaiDataTable();

                nGoukei = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt[i].JigyoushoKubun.ToString() == aryKubun[nKubunCnt].ToString())
                    {
                        ChumonDataSet.V_Jyuryosho_MeisaiRow drBindJ =
                            dtBindJ.NewV_Jyuryosho_MeisaiRow();

                        drBindJ.HacchuuNo = dt[i].HacchuuNo;
                        drBindJ.Hacchuubi = dt[i].HacchuuBi.ToString("yy/MM/dd");
                        drBindJ.BuhinKubun = dt[i].BuhinKubun;
                        drBindJ.BuhinCode = dt[i].BuhinKubun + dt[i].BuhinCode;
                        drBindJ.BuhinMei = dt[i].BuhinMei;
                        drBindJ.Suuryou = dt[i].Suuryou;
                        drBindJ.Tanka = dt[i].Tanka;
                        drBindJ.Tani = dt[i].Tani;
                        // 増税対応
                        //drBindJ.Kingaku = (int)Math.Floor(dt[i].Tanka * dt[i].Suuryou); ;
                        drBindJ.Kingaku = (int)Math.Round(dt[i].Tanka * dt[i].Suuryou, 0, MidpointRounding.AwayFromZero);
                        nGoukei += drBindJ.Kingaku;
                        drBindJ.BuhinKubun = dt[i].BuhinKubun;
                        //drBindJ.Ukeirebi = dt[i].NouhinBi.ToString("yy/MM/dd");
                        dtBindJ.AddV_Jyuryosho_MeisaiRow(drBindJ);

                    }
 
                }
                
                // 総行数
                int nMaxRowsCountJ = dtBindJ.Rows.Count;
                // 1ページ目の行数
                int nFirstPageRowJ = int.Parse(Global.Jyuryosho_FirstPageRow);
                // 2ページ目以降の行数
                int nElsePageRowJ = int.Parse(Global.Jyuryosho_ElsePageRow);
                // 総ページ数
                int nPageCountJ = 0;
                // 現在の行数
                int nNowPageCountJ = 0;
                // ページ数決定
                if (nMaxRowsCountJ < nFirstPageRowJ)
                {
                    nPageCountJ = 1;
                }
                else
                {
                    int nUseRowCountJ = nMaxRowsCountJ - nFirstPageRowJ;
                    nPageCountJ = 1;
                    nPageCountJ += nUseRowCountJ / nElsePageRowJ;
                    if (nUseRowCountJ % nElsePageRowJ != 0)
                        nPageCountJ++;
                }

               
                for (int nPageCnt = 0; nPageCnt < nPageCountJ; nPageCnt++)
                {
                    CtlA4 ctlA42 = LoadControl("CtlA4.ascx") as CtlA4;
                    ArrayList ary = new ArrayList();
                    int RowCount = 0;
                    if (nPageCnt == 0)
                        RowCount = nFirstPageRowJ;
                    else
                        RowCount = nElsePageRowJ;
                    // 現在の行数(データがある行数)＋ページの行数分
                    for (int j = nNowPageCountJ; j < nNowPageCountJ + RowCount; j++)
                    {
                        // データがある行を追加
                        if (j < dtBindJ.Rows.Count)
                        {
                            ary.Add(dtBindJ.Rows[j]);

                        }
                        // ない場合は空の行を追加
                        else
                        {
                            ary.Add(dtBindJ.NewV_Jyuryosho_MeisaiRow());
                        }

                    }
                    ChumonDataSet.V_Jyuryosho_MeisaiRow[] drAry = new ChumonDataSet.V_Jyuryosho_MeisaiRow[ary.Count];

                    nNowPageCountJ += drAry.Length;
                    ary.CopyTo(drAry);

                    // 明細
                    CtlJyuryosho_M ctlMeisai = LoadControl("CtlJyuryosho_M.ascx") as CtlJyuryosho_M;
                    ctlMeisai.Create(drAry);

                    this.T.Rows[0].Cells[0].Controls.Add(ctlA42);
                    ctlA42.Table.Rows[0].Cells[0].Controls.Add(ctlMeisai);

                    //this.T.Rows[0].Cells[0].Controls.Add(ctlMeisai);

                    // 受領書の改頁                    
                    if (nPageCnt < nPageCountJ - 1)
                    {
                        ctlA42.Table.Style.Add("page-break-after", "always");
                    }
                    else if (nPageCnt == nPageCountJ - 1)
                    {
                        //合計テーブル
                        // フッター                        
                        // 消費税率
                        decimal dZeiRitsuJ = (decimal.Parse(Global.ShouhiZei) / 100);
                        // 消費税
                        int nShohizeiJ = (int)Math.Floor(nGoukei * dZeiRitsuJ);

                        CtlNouhinsho_F fFooter = LoadControl("CtlNouhinsho_F.ascx") as CtlNouhinsho_F;
                        fFooter.Create(nGoukei, nShohizeiJ);

                        this.T.Rows[0].Cells[0].Controls.Add(ctlA42);
                        ctlA42.Table.Rows[0].Cells[0].Controls.Add(fFooter);

                        if (nKubunCnt != aryKubun.Count - 1)
                        {
                            ctlA42.Table.Style.Add("page-break-after", "always");
                        }
                    }

                }
            }
        }
        // メッセージ
        private void ShowMsg(string strMsg, bool bError)
        {
            this.LblMsg.Text = strMsg;
            this.LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }
    }
}
