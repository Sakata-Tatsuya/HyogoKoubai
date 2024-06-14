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
using Core.Type;
using m2mKoubaiDAL;

namespace m2mKoubai.Denpyou
{
    public partial class CtlHacchuusho : System.Web.UI.UserControl
    {

        private const int G_CELL_HACCHUUNO = 0;
        private const int G_CELL_CODE = 1;
        private const int G_CELL_SUURYOU = 2;
        private const int G_CELL_GOUKEI = 3;
        private const int G_CELL_TANI = 4;
        private const int G_CELL_NOUKI = 5;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(HacchuDataSet_M.V_HacchuBindRow[] drAry, int cnt)
        {
            G.DataSource = drAry;
            G.DataBind();
            G.EnableViewState = false;
            Create_Date();
            Create_Top(drAry);
            if (cnt > 1)
            {
                this.T.Visible = false;
                //AppCommon.ShowTable(false, this.T);
            }


        }

        // 注文年月日設定
        private void Create_Date()
        {
            LitDate.Text = DateTime.Today.ToString("yyyy年MM月dd日");
        }

        public void Create_Top(HacchuDataSet_M.V_HacchuBindRow[] drAry)
        {
            // 仕入先   
            this.LitKaisha.Text = drAry[0].ShiiresakiMei;
            // 郵便番号
            this.LitYubin.Text = Utility.FormatYuubin(drAry[0].YubinBangou);
            // 仕入先住所
            this.LitJyusyo.Text = drAry[0].Address;
            // 仕入先TEL
            this.LitShiireTEL.Text = Utility.FormatBanggo(drAry[0].Tel);
            // 仕入先FAX
            this.LitShiireFAX.Text = Utility.FormatBanggo(drAry[0].Fax);
            // 発注会社名
            LitKaishaMeiH.Text = drAry[0].KaishameiY + " " + drAry[0].Eigyousho;
            // 発注会社電話番号
            LitTelH.Text = Utility.FormatBanggo(drAry[0].TelY);
            // 発注会社FAX
            LitFaxH.Text = Utility.FormatBanggo(drAry[0].FaxY);
            ////発注担当者
            this.LitTantousha.Text = drAry[0].Name;
        }

        // 保存用
        //string strHacchuuNo = "";
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HacchuDataSet_M.V_HacchuBindRow dr =
                   e.Row.DataItem as HacchuDataSet_M.V_HacchuBindRow;

                // 発注Noが一致しない場合
                if (!dr.IsHacchuuNoNull())
                {
                    (e.Row.FindControl("LitNo") as Literal).Text = dr.HacchuuNo;
                }
                // 部品コード
                if (!dr.IsBuhinCodeNull())
                {
                    (e.Row.FindControl("LitCode") as Literal).Text = dr.BuhinCode;
                }
                // 部品名
                if (!dr.IsBuhinMeiNull())
                {
                    (e.Row.FindControl("LitHinmei") as Literal).Text = dr.BuhinMei;
                }
                // 数量
                if (!dr.IsSuuryouNull())
                {
                    (e.Row.FindControl("LitSuu") as Literal).Text = dr.Suuryou.ToString();
                }
                // 単位
                if (!dr.IsTaniNull())
                {
                    (e.Row.FindControl("LitTani") as Literal).Text = dr.Tani;
                }
                // 単価
                if (!dr.IsTankaNull())
                {
                    (e.Row.FindControl("LitTanka") as Literal).Text = String.Format("\\{0:#,##0.00}", dr.Tanka);
                }
                //合計
                if (!dr.IsKingakuNull())
                {
                    (e.Row.FindControl("LitKei") as Literal).Text = String.Format("\\{0:#,##0}", (int)dr.Kingaku);
                }
                // 納期
                if (!dr.IsNoukiNull())
                {
                    (e.Row.FindControl("LitNouki") as Literal).Text =
                        (new Nengappi(int.Parse(dr.Nouki))).ToString("yyyy/MM/dd");
                }
                //納入場所
                if (!dr.IsBashoMeiNull())
                {
                    (e.Row.FindControl("LitBasyo") as Literal).Text = dr.BashoMei;
                }
                //備考
                if (!dr.IsBikouNull())
                {
                    (e.Row.FindControl("LitBikou") as Literal).Text = dr.Bikou.Replace("\r\n", "　");
                }
            }
        }
    }
}