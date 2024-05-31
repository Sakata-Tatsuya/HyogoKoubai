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
            // ”­’‰ïĞ—X•Ö”Ô†
            LitYubinH.Text = Utility.FormatYuubin(drHeader.YubinH);
            // ”­’‰ïĞZŠ
            LitAddressH.Text = drHeader.AddressH;
            // ”­’‰ïĞ–¼
            LitKaishaMei.Text = drHeader.KaishaMei + " " + drHeader.EigyouSho;
            // ”­’‰ïĞ“d˜b”Ô†
            LitTelH.Text = Utility.FormatBanggo(drHeader.TelH);
            // ”­’‰ïĞFAX
            LitFaxH.Text = Utility.FormatBanggo(drHeader.FaxH);          
            // “ú•t
            LitDate.Text = DateTime.Today.ToString("yyyy”NMMŒdd“ú");
            // d“üæ–¼
            LitShiiresakiMei.Text = drHeader.ShiiresakiMei;
            // “o˜^”Ô†
            LitInvoiceRegNo.Text = drHeader.InvoiceRegNo;
            // —X•Ö”Ô†
            LitYubinBangou.Text = Utility.FormatYuubin(drHeader.YubinBangou);
            // ZŠ
            LitAddress.Text = drHeader.Address;
            // TEL
            LitTel.Text = Utility.FormatBanggo(drHeader.Tel);
            // FAX
            LitFax.Text = Utility.FormatBanggo(drHeader.Fax);
          
        }
    }
}