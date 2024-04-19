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

            // ���h�R�E���Y��Џ��擾
           /* m2mKoubaiDataSet.T_KaishaInfoRow drKaisha =
                KaishaInfoClass.getT_KaishaInfoRow(Global.GetConnection());
            if (drKaisha == null)
            {
                return;
            }
            */
            // ���h�R�E��ЗX�֔ԍ�
            LitYuubinY.Text = Utility.FormatYuubin(drShiire.YuubinY);
            // ���h�R�E��ЏZ��
            LitJyushoY.Text = drShiire.AddressY;
            // ���h�R�E��Ж�
            LitKaishaMeiY.Text = drShiire.KaishaMei + " " + drShiire.EigyouSho;
            // ���h�R�E���Y������Гd�b�ԍ�
            LitTelY.Text = Utility.FormatBanggo(drShiire.TelY);
            // ���h�R�E���YFAX
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