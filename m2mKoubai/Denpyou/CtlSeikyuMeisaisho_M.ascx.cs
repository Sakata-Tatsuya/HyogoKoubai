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
    public partial class CtlSeikyuMeisaisho_M : System.Web.UI.UserControl
    {
        private const int G_CELL_HACCHUU_NO = 0;
        private const int G_CELL_BUHIN_CODE = 1;
        private const int G_CELL_HINMEI = 2;
        private const int G_CELL_SUURYO = 3;
        private const int G_CELL_TANI = 4;
        private const int G_CELL_TANKA = 5;
        private const int G_CELL_KINGAKU = 6;
      
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        int nGoukei = 0;
       // public void Create(m2mKoubaiDAL.KenshuDataSet.V_KenshuRow[] drAry)
        public void Create(KenshuDataSet.V_KenshuBindRow[] drAry)
        {
            G.DataSource = drAry;
            G.DataBind();
            G.EnableViewState = false;
            
        }

        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                KenshuDataSet.V_KenshuBindRow dr =
                    e.Row.DataItem as KenshuDataSet.V_KenshuBindRow;

                // ����No
                if (!dr.IsHacchuuNoNull())
                    e.Row.Cells[G_CELL_HACCHUU_NO].Text = dr.HacchuuNo;

                // ���i�R�[�h
                if (!dr.IsBuhinCodeNull())
                    e.Row.Cells[G_CELL_BUHIN_CODE].Text = dr.BuhinCode;

                // ���i��
                if (!dr.IsBuhinMeiNull())
                {
                    e.Row.Cells[G_CELL_HINMEI].Text = dr.BuhinMei;
                }
                // �s�̍����ݒ�
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
                if (!dr.IsChumonSuuryouNull())
                {
                    // ���őΉ��@�؂�̂Ăł͂Ȃ��l�̌ܓ��ɕύX
                    //nGoukei = (int)Math.Floor(dr.Suuryou * dr.Tanka);
                    nGoukei = (int)Math.Round(dr.Suuryou * dr.Tanka, 0, MidpointRounding.AwayFromZero);
                    e.Row.Cells[G_CELL_KINGAKU].Text = string.Format("\\{0:#,##0}", nGoukei);
                }

            }

        }

       
    }
}