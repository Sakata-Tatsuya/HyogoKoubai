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
using System.Globalization;

namespace m2mKoubai.Denpyou
{
    public partial class CtlSeikyuMeisaisho_H : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader,m2mKoubaiDataSet.M_ShiiresakiRow drShiire )
        {
            /*
            // ヨドコウ興産会社情報取得
            m2mKoubaiDataSet.T_KaishaInfoRow drKaisha =
                KaishaInfoClass.getT_KaishaInfoRow(Global.GetConnection());
            if (drKaisha == null)
            {
                return;
            }*/

            // ヨドコウ会社郵便番号
            LitYuubinY.Text = Utility.FormatYuubin(drHeader.YuubinY);
            // ヨドコウ会社住所
            LitJyushoY.Text = drHeader.AddressY;
            // ヨドコウ会社名
            LitKaishaMeiY.Text = drHeader.KaishaMei + " " + drHeader.EigyouSho;
            // ヨドコウ興産株式会社電話番号
            LitTelY.Text = Utility.FormatBanggo(drHeader.TelY);
            // ヨドコウ興産FAX
            LitFaxY.Text = Utility.FormatBanggo(drHeader.FaxY);          

            // 日付
            LitDate.Text = DateTime.Today.ToString("yyyy年MM月dd日");
            /*
            CultureInfo culture = new CultureInfo("ja-JP", true);
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            DateTime target = DateTime.Today;
            string result = target.ToString("ggyy年M月d日", culture);
            LitDate.Text = result;
            */ 
            // 仕入先名
            LitShiiresakiMei.Text = drShiire.ShiiresakiMei;
            // ゆびん番号
            LitYuubin.Text = Utility.FormatYuubin(drShiire.YubinBangou);
            // 住所
            LitJyusho.Text = drShiire.Address;
            // TEL
            LitTel.Text = Utility.FormatBanggo(drShiire.Tel);
            // FAX
            LitFax.Text = Utility.FormatBanggo(drShiire.Fax);
          
        }
    }
}