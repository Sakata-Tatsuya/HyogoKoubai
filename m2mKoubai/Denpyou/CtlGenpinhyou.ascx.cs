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
            // 発注会社情報取得
            m2mKoubaiDataSet.T_KaishaInfoRow drKaisha =
                KaishaInfoClass.getT_KaishaInfoRow(drMeisai.JigyoushoKubun, Global.GetConnection());
            if (drKaisha == null)
            {
                return;
            }

            // 届け先
            this.LitTodokesaki.Text = drKaisha.KaishaMei + " " + drKaisha.EigyouSho;
            // 発注No
            this.LitHacchuuNo.Text = drMeisai.HacchuuNo;
            // 部品コード
            this.LitBuhinCode.Text = drMeisai.BuhinKubun + drMeisai.BuhinCode;
            // 品目名            
            this.LitHinmei.Text = drMeisai.BuhinMei;
            // バーコード
            this.Img1.ImageUrl = "../BarCode/BarCodeForm.aspx?BarCode=" + drMeisai.HacchuuNo;
            // 仕入先名
            this.LitShiiresaki.Text = drMeisai.ShiiresakiMei;

        }
    }
}