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
            // ���h�R�E���Y��Џ��擾
            m2mKoubaiDataSet.T_KaishaInfoRow drKaisha =
                KaishaInfoClass.getT_KaishaInfoRow(Global.GetConnection());
            if (drKaisha == null)
            {
                return;
            }*/

            // ���h�R�E��ЗX�֔ԍ�
            LitYuubinY.Text = Utility.FormatYuubin(drHeader.YuubinY);
            // ���h�R�E��ЏZ��
            LitJyushoY.Text = drHeader.AddressY;
            // ���h�R�E��Ж�
            LitKaishaMeiY.Text = drHeader.KaishaMei + " " + drHeader.EigyouSho;
            // ���h�R�E���Y������Гd�b�ԍ�
            LitTelY.Text = Utility.FormatBanggo(drHeader.TelY);
            // ���h�R�E���YFAX
            LitFaxY.Text = Utility.FormatBanggo(drHeader.FaxY);          

            // ���t
            LitDate.Text = DateTime.Today.ToString("yyyy�NMM��dd��");
            /*
            CultureInfo culture = new CultureInfo("ja-JP", true);
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            DateTime target = DateTime.Today;
            string result = target.ToString("ggyy�NM��d��", culture);
            LitDate.Text = result;
            */ 
            // �d���於
            LitShiiresakiMei.Text = drShiire.ShiiresakiMei;
            // ��т�ԍ�
            LitYuubin.Text = Utility.FormatYuubin(drShiire.YubinBangou);
            // �Z��
            LitJyusho.Text = drShiire.Address;
            // TEL
            LitTel.Text = Utility.FormatBanggo(drShiire.Tel);
            // FAX
            LitFax.Text = Utility.FormatBanggo(drShiire.Fax);
          
        }
    }
}