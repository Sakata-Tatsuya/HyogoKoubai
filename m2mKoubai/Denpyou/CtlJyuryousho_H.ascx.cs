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
            // ���t
            /* CultureInfo culture = new CultureInfo("ja-JP", true);
             culture.DateTimeFormat.Calendar = new JapaneseCalendar();
             DateTime target = DateTime.Today;
             string result = target.ToString("ggyy�NM��d��", culture);
             LitDate.Text = result;*/

            LitDate.Text = DateTime.Today.ToString("yyyy�NMM��dd��");

            /*
            // ���h�R�E���Y��Џ��擾
            m2mKoubaiDataSet.T_KaishaInfoRow drKaisha =
                KaishaInfoClass.getT_KaishaInfoRow(Global.GetConnection());
            if (drKaisha == null)
            {
                return;
            }

            // ���h�R�E��ЗX�֔ԍ�
            LitYuubinY.Text = Utility.FormatYuubin(drKaisha.Yuubin);
            // ���h�R�E��ЏZ��
            LitJyushoY.Text = drKaisha.Address;
            // ���h�R�E��Ж�
            LitKaishaMeiY.Text = drKaisha.KaishaMei;
            // ���h�R�E���Y������Гd�b�ԍ�
            LitTelY.Text = Utility.FormatBanggo(drKaisha.Tel);
            // ���h�R�E���YFAX
            LitFaxY.Text = Utility.FormatBanggo(drKaisha.Fax);
            */
            // ���h�R�E��ЗX�֔ԍ�
            LitYuubinY.Text = Utility.FormatYuubin(drHeader.YubinH);
            // ���h�R�E��ЏZ��
            LitJyushoY.Text = drHeader.AddressH;
            // ���h�R�E��Ж�
            LitKaishaMeiY.Text = drHeader.KaishaMei + " " + drHeader.EigyouSho;
            // ���h�R�E���Y������Гd�b�ԍ�
            LitTelY.Text = Utility.FormatBanggo(drHeader.TelH);
            // ���h�R�E���YFAX
            LitFaxY.Text = Utility.FormatBanggo(drHeader.FaxH);

            // ��Ж�
            LitShiiresakiMei.Text = drHeader.ShiiresakiMei;
            // ��
            LitYuubin.Text = Utility.FormatYuubin(drHeader.YubinBangou);
            // �Z��
            LitJyusho.Text = drHeader.Address;
            // TEL
            LitTel.Text = Utility.FormatBanggo(drHeader.Tel);
            // FAX
            LitFax.Text = Utility.FormatBanggo(drHeader.Fax);

        }
    }
}