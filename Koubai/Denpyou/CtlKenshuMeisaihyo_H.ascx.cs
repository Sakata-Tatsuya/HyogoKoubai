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
namespace Koubai.Denpyou
{
    public partial class CtlKenshuMeisaihyo_H : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        public void Create(ShiiresakiDataSet.V_Nouhinsho_HeaderRow drShiire, string strkey)
        {
            // î≠íçâÔé–óXï÷î‘çÜ
            LitYuubinY.Text = Utility.FormatYuubin(drShiire.YubinH);
            // î≠íçâÔé–èZèä
            LitJyushoY.Text = drShiire.AddressH;
            // î≠íçâÔé–ñº
            LitKaishaMeiY.Text = drShiire.KaishaMei + " " + drShiire.EigyouSho;
            // î≠íçâÔé–ìdòbî‘çÜ
            LitTelY.Text = Utility.FormatBanggo(drShiire.TelH);
            // î≠íçâÔé–FAX
            LitFaxY.Text = Utility.FormatBanggo(drShiire.FaxH);
            LitDate.Text = Utility.FormatFromyyyyMM(strkey);
            LitShiiresakiMei.Text = drShiire.ShiiresakiMei;
            LitYuubin.Text = Utility.FormatYuubin(drShiire.YubinBangou);
            LitJyusho.Text = drShiire.Address;
            LitTel.Text = Utility.FormatBanggo(drShiire.Tel);
            LitFax.Text = Utility.FormatBanggo(drShiire.Fax);
        }
    }
}