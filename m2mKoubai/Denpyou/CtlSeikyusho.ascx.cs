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

namespace m2mKoubai.Denpyou
{
    public partial class CtlSeikyusho : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(m2mKoubaiDataSet.M_ShiiresakiRow drShiire, ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader, int nMonth)
        {
            // ヨドコウ興産株式会社
            LitYodoko.Text = drHeader.KaishaMei +  " " + drHeader.EigyouSho;
            // 日付
            LitDate.Text = DateTime.Today.ToString("yyyy年MM月dd日");
         
            // 仕入先会社名
            LitShiiresakiMei.Text = drShiire.ShiiresakiMei;
            // 仕入先郵便番号
            LitYuubin.Text = Utility.FormatYuubin(drShiire.YubinBangou);
            // 仕入先住所
            LitJyusho.Text = drShiire.Address;
            // 仕入先TEL
            LitTel.Text = Utility.FormatBanggo(drShiire.Tel);
            // 仕入先FAX
            LitFax.Text = Utility.FormatBanggo(drShiire.Fax);
            // 月分
            LitMonth1.Text = nMonth.ToString() + "月分";
            // 月度分
            LitMonth2.Text = nMonth.ToString() + "月度分";
            // 振込先
           // LitHurikomi.Text = drShiire.FurikomiSaki;
            // 口座名義
            LitKouzamei.Text = drShiire.KouzaMeigi;
            // 金融機関名
            LitKinyuuKikanmei.Text = drShiire.KinyuuKikanMei;
            // 口座番号
            LitKouzaBanggo.Text = drShiire.KouzaBangou;
          

        }

        public void SetGoukei(int nGoukei, int nShohizei)
        {
//2011.10.31　請求金額修正依頼に伴い追加＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            this.LitGoukei.Text = string.Format("\\{0:#,##0}", nGoukei);
            this.LitSyouhizei.Text = string.Format("\\{0:#,##0}", nShohizei);
//＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

            // ご請求金額
            int nSouGoukei = nGoukei + nShohizei; 
            LitKingaku.Text = string.Format("\\{0:#,##0}", nSouGoukei);
        }
    }
}