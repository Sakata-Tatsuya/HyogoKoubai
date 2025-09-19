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



        // �[�i���w�b�_�[
        public void Create(ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader)
        {     
            // ��������ЗX�֔ԍ�
            LitYuubinY.Text = Utility.FormatYuubin(drHeader.YubinH);
            // ��������ЏZ��
            LitJyushoY.Text = drHeader.AddressH;
            // ��������Ж�
            LitKaishaMeiY.Text = drHeader.KaishaMei + " " + drHeader.EigyouSho;
            // �������d�b�ԍ�
            LitTelY.Text = Utility.FormatBanggo(drHeader.TelH);
            // ������FAX
            LitFaxY.Text = Utility.FormatBanggo(drHeader.FaxH);

            // ���t
            LitDate.Text = DateTime.Today.ToString("yyyy�NMM��dd��");
         
            // ��Ж�
            LitShiiresakiMei.Text = drHeader.ShiiresakiMei;
            // ��
            LitYuubin.Text = drHeader.YubinBangou;
            // �Z��
            LitJyusho.Text = drHeader.Address;
            // TEL
            LitTel.Text = drHeader.Tel;
            // FAX
            LitFax.Text = drHeader.Fax;

        }
    }
}