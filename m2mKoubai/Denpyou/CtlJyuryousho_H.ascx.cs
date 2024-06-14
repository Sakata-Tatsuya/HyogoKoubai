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
    public partial class CtlJyuryousho_H : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader)
        {
            // “ú•t
            LitDate.Text = DateTime.Today.ToString("yyyy”NMMŒdd“ú");
            // ”­’‰ïĞ—X•Ö”Ô†
            LitYuubinY.Text = Utility.FormatYuubin(drHeader.YubinH);
            // ”­’‰ïĞZŠ
            LitJyushoY.Text = drHeader.AddressH;
            // ”­’‰ïĞ–¼
            LitKaishaMeiY.Text = drHeader.KaishaMei + " " + drHeader.EigyouSho;
            // ”­’‰ïĞ“d˜b”Ô†
            LitTelY.Text = Utility.FormatBanggo(drHeader.TelH);
            // ”­’‰ïĞFAX
            LitFaxY.Text = Utility.FormatBanggo(drHeader.FaxH);
            // ‰ïĞ–¼
            LitShiiresakiMei.Text = drHeader.ShiiresakiMei;
            // §
            LitYuubin.Text = Utility.FormatYuubin(drHeader.YubinBangou);
            // ZŠ
            LitJyusho.Text = drHeader.Address;
            // TEL
            LitTel.Text = Utility.FormatBanggo(drHeader.Tel);
            // FAX
            LitFax.Text = Utility.FormatBanggo(drHeader.Fax);

        }
    }
}