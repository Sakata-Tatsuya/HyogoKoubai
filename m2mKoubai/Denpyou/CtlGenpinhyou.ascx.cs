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
    public partial class CtlGenpinhyou : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(m2mKoubaiDAL.ChumonDataSet.V_Chumon_MeisaiRow drMeisai)
        {
            // ������Џ��擾
            m2mKoubaiDataSet.T_KaishaInfoRow drKaisha =
                KaishaInfoClass.getT_KaishaInfoRow(drMeisai.JigyoushoKubun, Global.GetConnection());
            if (drKaisha == null)
            {
                return;
            }

            // �͂���
            this.LitTodokesaki.Text = drKaisha.KaishaMei + " " + drKaisha.EigyouSho;
            // ����No
            this.LitHacchuuNo.Text = drMeisai.HacchuuNo;
            // ���i�R�[�h
            this.LitBuhinCode.Text = drMeisai.BuhinKubun + drMeisai.BuhinCode;
            // �i�ږ�            
            this.LitHinmei.Text = drMeisai.BuhinMei;
            // �o�[�R�[�h
            this.Img1.ImageUrl = "../BarCode/BarCodeForm.aspx?BarCode=" + drMeisai.HacchuuNo;
            // �d���於
            this.LitShiiresaki.Text = drMeisai.ShiiresakiMei;

        }
    }
}