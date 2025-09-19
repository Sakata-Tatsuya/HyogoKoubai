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

namespace Koubai.Denpyou
{
    public partial class CtlJyuryousho_F : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(int nGoukei, int nShohizei)
        {
            // çáåv
            this.LitGoukei.Text = string.Format("\\{0:#,##0}", nGoukei);
            // è¡îÔê≈   
            //decimal dZeiRitsu = (decimal.Parse(Global.ShouhiZei) / 100);
            //int nShohizei = (int)Math.Floor(nGoukei * dZeiRitsu);
            this.LitShohizei.Text = string.Format("\\{0:#,##0}", nShohizei);

            // ëççáåv          
            LitSouGoukei.Text = string.Format("\\{0:#,##0}", nGoukei + nShohizei);
        }
    }
}