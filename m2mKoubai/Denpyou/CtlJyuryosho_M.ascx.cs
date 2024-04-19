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
    public partial class CtlJyuryosho_M : System.Web.UI.UserControl
    {
        private const int G_CELL_HACCHUU_NO = 0;
        private const int G_CELL_HACCHUU_BI = 1;
        private const int G_CELL_BUHIN_KUBUN = 2;
        private const int G_CELL_BUHIN_CODE = 3;
        private const int G_CELL_HINMEI = 4;
        private const int G_CELL_SUURYO = 5;
        private const int G_CELL_TANI = 6;
        private const int G_CELL_TANKA = 7;
        private const int G_CELL_KINGAKU = 8;
      //  private const int G_CELL_UKEIREBI = 9;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(m2mKoubaiDAL.ChumonDataSet.V_Jyuryosho_MeisaiRow[] drAry)
        {
            G.DataSource = drAry;
            G.DataBind();
            G.EnableViewState = false;
        }
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ChumonDataSet.V_Jyuryosho_MeisaiRow dr =
                    e.Row.DataItem as ChumonDataSet.V_Jyuryosho_MeisaiRow;

                // ����No
                if (!dr.IsHacchuuNoNull())
                    e.Row.Cells[G_CELL_HACCHUU_NO].Text = dr.HacchuuNo;
                // ������
                if(!dr.IsHacchuubiNull())
                    e.Row.Cells[G_CELL_HACCHUU_BI].Text = dr.Hacchuubi;
                // �i�ڃO���[�v
                if(!dr.IsBuhinKubunNull())
                    e.Row.Cells[G_CELL_BUHIN_KUBUN].Text = dr.BuhinKubun;
                // ���i�R�[�h
                if (!dr.IsBuhinCodeNull())
                    e.Row.Cells[G_CELL_BUHIN_CODE].Text = dr.BuhinCode;
                
                // ���i��
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
                //����
                e.Row.Cells[G_CELL_HINMEI].CssClass = "hei30";

                // ����
                if (!dr.IsSuuryouNull())
                    e.Row.Cells[G_CELL_SUURYO].Text = dr.Suuryou.ToString("#,##0");

                // �P��
                if (!dr.IsTaniNull())
                    e.Row.Cells[G_CELL_TANI].Text = dr.Tani;

                // �P��
                if (!dr.IsTankaNull())
                    e.Row.Cells[G_CELL_TANKA].Text = string.Format("\\{0:#,##0.#0}", dr.Tanka);

                // ���z
                if (!dr.IsKingakuNull())
                    e.Row.Cells[G_CELL_KINGAKU].Text = string.Format("\\{0:#,##0}", dr.Kingaku);
                           
                // �����
              //  if(!dr.IsUkeirebiNull())
                //    e.Row.Cells[G_CELL_UKEIREBI].Text = dr.Ukeirebi;
               
            }
        }
    }
}