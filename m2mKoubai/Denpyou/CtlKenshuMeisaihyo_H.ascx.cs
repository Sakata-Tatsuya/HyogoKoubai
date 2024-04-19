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
    public partial class CtlKenshuMeisaihyo_H : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        public void Create(ShiiresakiDataSet.V_Nouhinsho_HeaderRow drShiire, string strkey)
        {

            // ヨドコウ興産会社情報取得
           /* m2mKoubaiDataSet.T_KaishaInfoRow drKaisha =
                KaishaInfoClass.getT_KaishaInfoRow(Global.GetConnection());
            if (drKaisha == null)
            {
                return;
            }
            */
            // ヨドコウ会社郵便番号
            LitYuubinY.Text = Utility.FormatYuubin(drShiire.YuubinY);
            // ヨドコウ会社住所
            LitJyushoY.Text = drShiire.AddressY;
            // ヨドコウ会社名
            LitKaishaMeiY.Text = drShiire.KaishaMei + " " + drShiire.EigyouSho;
            // ヨドコウ興産株式会社電話番号
            LitTelY.Text = Utility.FormatBanggo(drShiire.TelY);
            // ヨドコウ興産FAX
            LitFaxY.Text = Utility.FormatBanggo(drShiire.FaxY);

            LitDate.Text = Utility.FormatFromyyyyMM(strkey);

            LitShiiresakiMei.Text = drShiire.ShiiresakiMei;
            LitYuubin.Text = Utility.FormatYuubin(drShiire.YubinBangou);
            LitJyusho.Text = drShiire.Address;
            LitTel.Text = Utility.FormatBanggo(drShiire.Tel);
            LitFax.Text = Utility.FormatBanggo(drShiire.Fax);
        }
    }
}