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
using System.Globalization;
using m2mKoubaiDAL;

namespace m2mKoubai.Denpyou
{
    public partial class CtlSeikyusho : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(m2mKoubaiDataSet.M_ShiiresakiRow drShiire, ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader, int nMonth)
        {
            // ������Ж�
            LitOwner.Text = drHeader.KaishaMei +  " " + drHeader.EigyouSho;
            // ���t
            LitDate.Text = DateTime.Today.ToString("yyyy�NMM��dd��");
            // �d�����Ж�
            LitShiiresakiMei.Text = drHeader.ShiiresakiMei;
            LitInvoiceRegNo.Text = drHeader.InvoiceRegNo;
            // �d����X�֔ԍ�
            LitYubinBangou.Text = Utility.FormatYuubin(drHeader.YubinBangou);
            // �d����Z��
            LitAddress.Text = drHeader.Address;
            // �d����TEL
            LitTel.Text = Utility.FormatBanggo(drHeader.Tel);
            // �d����FAX
            LitFax.Text = Utility.FormatBanggo(drHeader.Fax);
            // ����
            LitMonth1.Text = nMonth.ToString() + "����";
            // ���x��
            LitMonth2.Text = nMonth.ToString() + "���x��";
            // �������`
            LitKouzamei.Text = drShiire.KouzaMeigi;
            // ���Z�@�֖�
            LitKinyuuKikanmei.Text = drShiire.KinyuuKikanMei;
            // �����ԍ�
            LitKouzaBanggo.Text = drShiire.KouzaBangou;

        }

        public void SetGoukei(int nGoukei, int nShohizei)
        {
            this.LitGoukei.Text = string.Format("\\{0:#,##0}", nGoukei);
            this.LitSyouhizei.Text = string.Format("\\{0:#,##0}", nShohizei);

            // ���������z
            int nSouGoukei = nGoukei + nShohizei; 
            LitKingaku.Text = string.Format("\\{0:#,##0}", nSouGoukei);
        }
    }
}