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
using KoubaiDAL;
using System.Globalization;


namespace Koubai.Denpyou
{
    public partial class CtlNouhinsho_H : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        // ”[•i‘ƒwƒbƒ_[
        public void Create(ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader)
        {     
            // ”­’Œ³‰ïĞ—X•Ö”Ô†
            LitYuubinY.Text = Utility.FormatYuubin(drHeader.YubinH);
            // ”­’Œ³‰ïĞZŠ
            LitJyushoY.Text = drHeader.AddressH;
            // ”­’Œ³‰ïĞ–¼
            LitKaishaMeiY.Text = drHeader.KaishaMei + " " + drHeader.EigyouSho;
            // ”­’Œ³“d˜b”Ô†
            LitTelY.Text = Utility.FormatBanggo(drHeader.TelH);
            // ”­’Œ³FAX
            LitFaxY.Text = Utility.FormatBanggo(drHeader.FaxH);

            // “ú•t
            LitDate.Text = DateTime.Today.ToString("yyyy”NMMŒdd“ú");
         
            // ‰ïĞ–¼
            LitShiiresakiMei.Text = drHeader.ShiiresakiMei;
            // §
            LitYuubin.Text = drHeader.YubinBangou;
            // ZŠ
            LitJyusho.Text = drHeader.Address;
            // TEL
            LitTel.Text = drHeader.Tel;
            // FAX
            LitFax.Text = drHeader.Fax;

        }
    }
}