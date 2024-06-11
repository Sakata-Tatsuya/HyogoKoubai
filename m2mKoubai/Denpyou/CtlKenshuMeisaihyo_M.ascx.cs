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
    public partial class CtlKenshuMeisaihyo_M : System.Web.UI.UserControl
    {
        private const int G_CELL_HACCHUU_NO = 0;
        private const int G_CELL_BUHIN_CODE = 1;
        private const int G_CELL_HINMEI = 2;
        private const int G_CELL_SUURYO = 3;
        private const int G_CELL_TANKA = 4;
        private const int G_CELL_KINGAKU = 5;
        private const int G_CELL_BASHO = 6;
        private const int G_CELL_UKEIREBI = 7;
        private const int G_CELL_NYUKA_SUURYO = 8;
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
       public void Create(KenshuDataSet.V_KenshuBindRow[] drAry)
        {
            G.DataSource = drAry;
            G.DataBind();
            G.EnableViewState = false;
        }
        int nGoukei = 0;
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                KenshuDataSet.V_KenshuBindRow dr = e.Row.DataItem as KenshuDataSet.V_KenshuBindRow;
                  // 発注No
                if (!dr.IsHacchuuNoNull())
                      e.Row.Cells[G_CELL_HACCHUU_NO].Text = dr.HacchuuNo;

                  // 部品コード
                  if (!dr.IsBuhinCodeNull())
                      e.Row.Cells[G_CELL_BUHIN_CODE].Text = dr.BuhinCode;

                  // 部品名
                  if (!dr.IsBuhinMeiNull())
                  {
                      e.Row.Cells[G_CELL_HINMEI].Text = dr.BuhinMei;
                  }
                  // 行の高さ
                  e.Row.Cells[G_CELL_HINMEI].CssClass = "hei30";

                  // 数量
                  if (!dr.IsSuuryouNull())
                      e.Row.Cells[G_CELL_SUURYO].Text = dr.ChumonSuuryou.ToString("#,##0");
                  // 単価               
                  if (!dr.IsTankaNull())
                      e.Row.Cells[G_CELL_TANKA].Text = string.Format("\\{0:#,##0.#0}", dr.Tanka);
                  // 金額  
                  if (!dr.IsChumonSuuryouNull())
                  {
                      nGoukei = (int)Math.Round(dr.ChumonSuuryou * dr.Tanka, 0, MidpointRounding.AwayFromZero);
                      e.Row.Cells[G_CELL_KINGAKU].Text = string.Format("\\{0:#,##0}", nGoukei);
                  }
                  // 納入場所
                  if (!dr.IsBashoMeiNull())
                      e.Row.Cells[G_CELL_BASHO].Text = dr.BashoMei;
                  // 受入日
                  if (!dr.IsNouhinBiNull())
                      e.Row.Cells[G_CELL_UKEIREBI].Text = dr.NouhinBi.ToString("yyyy/MM/dd");
                  // 入荷数量              
                  if (!dr.IsSuuryouNull())
                      e.Row.Cells[G_CELL_NYUKA_SUURYO].Text = dr.Suuryou.ToString("#,##0");                             
                
            }
          
        }

       
    }
}