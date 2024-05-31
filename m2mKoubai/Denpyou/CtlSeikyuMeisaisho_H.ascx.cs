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
            // ������ЗX�֔ԍ�
            LitYubinH.Text = Utility.FormatYuubin(drHeader.YubinH);
            // ������ЏZ��
            LitAddressH.Text = drHeader.AddressH;
            // ������Ж�
            LitKaishaMei.Text = drHeader.KaishaMei + " " + drHeader.EigyouSho;
            // ������Гd�b�ԍ�
            LitTelH.Text = Utility.FormatBanggo(drHeader.TelH);
            // �������FAX
            LitFaxH.Text = Utility.FormatBanggo(drHeader.FaxH);          
            // ���t
            LitDate.Text = DateTime.Today.ToString("yyyy�NMM��dd��");
            // �d���於
            LitShiiresakiMei.Text = drHeader.ShiiresakiMei;
            // �o�^�ԍ�
            LitInvoiceRegNo.Text = drHeader.InvoiceRegNo;
            // �X�֔ԍ�
            LitYubinBangou.Text = Utility.FormatYuubin(drHeader.YubinBangou);
            // �Z��
            LitAddress.Text = drHeader.Address;
            // TEL
            LitTel.Text = Utility.FormatBanggo(drHeader.Tel);
            // FAX
            LitFax.Text = Utility.FormatBanggo(drHeader.Fax);
          
        }
    }
}