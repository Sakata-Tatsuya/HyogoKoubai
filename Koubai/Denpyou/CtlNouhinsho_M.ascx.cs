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
using KoubaiDAL;

namespace Koubai.Denpyou
{
    public partial class CtlNouhinsho_M : System.Web.UI.UserControl
    {
        private const int G_CELL_HACCHUU_NO = 0;
        private const int G_CELL_HACCHUUBI = 1;
        private const int G_CELL_BUHIN_KUBUN = 2;
        private const int G_CELL_BUHIN_CODE = 3;
        private const int G_CELL_HINMEI = 4;
        private const int G_CELL_SUURYO = 5;
        private const int G_CELL_TANI = 6;
        private const int G_CELL_TANKA = 7;
        private const int G_CELL_KINGAKU = 8;
        //private const int G_CELL_NOUHINBI = 9;
        private const int G_CELL_BAR_CODE = 9;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(ChumonDataSet.V_Nouhinsho_MeisaiRow[] drAry)
        {
            G.DataSource = drAry;
            G.DataBind();
            G.EnableViewState = false;
        }
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ChumonDataSet.V_Nouhinsho_MeisaiRow dr =
                    e.Row.DataItem as ChumonDataSet.V_Nouhinsho_MeisaiRow;

                // 発注No
                if (!dr.IsHacchuuNoNull())
                    e.Row.Cells[G_CELL_HACCHUU_NO].Text = dr.HacchuuNo;
                // 発注日
                if(!dr.IsHacchuubiNull())
                    e.Row.Cells[G_CELL_HACCHUUBI].Text = dr.Hacchuubi;
                // 品目グループ
                if(!dr.IsBuhinKubunNull())
                    e.Row.Cells[G_CELL_BUHIN_KUBUN].Text = dr.BuhinKubun;
                // 部品コード
                if (!dr.IsBuhinCodeNull())
                    e.Row.Cells[G_CELL_BUHIN_CODE].Text = dr.BuhinCode;

                // 部品目名  
                if (!dr.IsBuhinMeiNull())
                {
                    if (dr.BuhinMei.Length > 25)
                    {
                        int nCnt = dr.BuhinMei.Length - 25;
                        string str1 = dr.BuhinMei.Substring(0, 25);
                        string str2 = dr.BuhinMei.Substring(26, nCnt - 1);
                        e.Row.Cells[G_CELL_HINMEI].Text = str1 + "<br>" + str2;
                    }
                    else
                    {
                        e.Row.Cells[G_CELL_HINMEI].Text = dr.BuhinMei;
                    }
                }
                e.Row.Cells[G_CELL_HINMEI].CssClass = "hei30";
                // 数量
                if (!dr.IsSuuryouNull())
                    e.Row.Cells[G_CELL_SUURYO].Text = dr.Suuryou.ToString("#,##0");

                // 単位
                if (!dr.IsTaniNull())
                    e.Row.Cells[G_CELL_TANI].Text = dr.Tani;

                // 単価
                if (!dr.IsTankaNull())
                    e.Row.Cells[G_CELL_TANKA].Text = string.Format("{0:#,##0.#0}", dr.Tanka);

                // 金額
                if (!dr.IsKingakuNull())
                    e.Row.Cells[G_CELL_KINGAKU].Text = string.Format("{0:#,##0}", dr.Kingaku);
                //

                // バーコード
                if (!dr.IsBarCodeNull())
                {
                    Image Img = e.Row.FindControl("Img") as Image;
                    Img.ImageUrl = "../BarCode/BarCodeForm.aspx?BarCode=" + dr.HacchuuNo;
                }
                else
                {
                    Image Img = e.Row.FindControl("Img") as Image;
                    Img.Visible = false;
                }
            }
        }
    }
}