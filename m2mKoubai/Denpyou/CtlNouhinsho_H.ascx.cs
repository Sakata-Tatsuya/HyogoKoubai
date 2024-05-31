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
    public partial class CtlNouhinsho_H : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        // 納品書ヘッダー
        public void Create(ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader)
        {     
            /*
            // ヨドコウ興産会社情報取得
            m2mKoubaiDataSet.T_KaishaInfoRow drKaisha =
                KaishaInfoClass.getT_KaishaInfoRow(Global.GetConnection());
            if (drKaisha == null)
            {
                return;
            }
            */
            // ヨドコウ会社郵便番号
            LitYuubinY.Text = Utility.FormatYuubin(drHeader.YubinH);
            // ヨドコウ会社住所
            LitJyushoY.Text = drHeader.AddressH;
            // ヨドコウ会社名
            LitKaishaMeiY.Text = drHeader.KaishaMei + " " + drHeader.EigyouSho;
            // ヨドコウ興産株式会社電話番号
            LitTelY.Text = Utility.FormatBanggo(drHeader.TelH);
            // ヨドコウ興産FAX
            LitFaxY.Text = Utility.FormatBanggo(drHeader.FaxH);



            // 日付
            LitDate.Text = DateTime.Today.ToString("yyyy年MM月dd日");
         
            // 会社名
            LitShiiresakiMei.Text = drHeader.ShiiresakiMei;
            // 〒
            LitYuubin.Text = drHeader.YubinBangou;
            // 住所
            LitJyusho.Text = drHeader.Address;
            // TEL
            LitTel.Text = drHeader.Tel;
            // FAX
            LitFax.Text = drHeader.Fax;

        }
    }
}