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
using System.Globalization;
using m2mKoubaiDAL;
using System.Collections.Generic;
using System.Drawing;

namespace m2mKoubai.Denpyou
{
    public partial class CtlSeikyusho : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(m2mKoubaiDataSet.M_ShiiresakiRow drShiire, ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader, int nMonth)
        {
            // 発注会社名
            LitOwner.Text = drHeader.KaishaMei +  " " + drHeader.EigyouSho;
            // 日付
            LitDate.Text = DateTime.Today.ToString("yyyy年MM月dd日");
            // 仕入先会社名
            LitShiiresakiMei.Text = drHeader.ShiiresakiMei;
            LitInvoiceRegNo.Text = drHeader.InvoiceRegNo;
            // 仕入先郵便番号
            LitYubinBangou.Text = Utility.FormatYuubin(drHeader.YubinBangou);
            // 仕入先住所
            LitAddress.Text = drHeader.Address;
            // 仕入先TEL
            LitTel.Text = Utility.FormatBanggo(drHeader.Tel);
            // 仕入先FAX
            LitFax.Text = Utility.FormatBanggo(drHeader.Fax);
            // 月分
            LitMonth1.Text = nMonth.ToString() + "月分";
            // 月度分
            LitMonth2.Text = nMonth.ToString() + "月度分";
            // 口座名義
            LitKouzamei.Text = drShiire.KouzaMeigi;
            // 金融機関名
            LitKinyuuKikanmei.Text = drShiire.KinyuuKikanMei;
            // 口座番号
            LitKouzaBanggo.Text = drShiire.KouzaBangou;

        }
        public void SetGoukei(int nGoukei, int nShohizei)
        {
            //this.LitGoukei.Text = string.Format("\\{0:#,##0}", nGoukei);
            //this.LitSyouhizei.Text = string.Format("\\{0:#,##0}", nShohizei);

            // ご請求金額
            int nSouGoukei = nGoukei + nShohizei;
            LitKingaku.Text = string.Format("\\{0:#,##0}", nSouGoukei);
        }

        public void SetGoukei(List<KenshuClass.ZeirituShukei> lst)
        {
            int nGoukei = 0;
            int nShohizei = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                nGoukei += lst[i].iKingaku;
                nShohizei += lst[i].iZeigaku;
            }
            // ご請求金額
            int nSouGoukei = nGoukei + nShohizei; 
            LitKingaku.Text = string.Format("\\{0:#,##0}", nSouGoukei);
            GZ.DataSource = lst;
            GZ.DataBind();
            GZ.BorderColor = Color.White;
        }

        protected void GZ_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                KenshuClass.ZeirituShukei mi = e.Row.DataItem as KenshuClass.ZeirituShukei;
                Literal LitShubetu = e.Row.FindControl("LitShubetu") as Literal;
                Literal LitKingaku = e.Row.FindControl("LitKingaku") as Literal;
                Literal LitZeigaku = e.Row.FindControl("LitZeigaku") as Literal;
                LitKingaku.Text = mi.iKingaku.ToString("#,##0");
                LitZeigaku.Text = mi.iZeigaku.ToString("#,##0");
                LitShubetu.Text = mi.iZeiritu.ToString("#0") + "%対象";
                if (mi.bKeigenZeirituFlg)
                {
                    LitShubetu.Text += "(軽減税率)";
                }


            }

        }



























        }
    }