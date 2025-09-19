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

namespace Koubai.Download
{
    public partial class DownloadForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                M.MenuName = "ダウンロード";
                //this.CtlTabMain1.Menu = CtlTabMain.MainMenu.Download;
                this.TabUpload.SelectedIndex = 0;

                this.Create();
            }
        }

        protected void TabUpload_TabClick(object sender, Telerik.WebControls.TabStripEventArgs e)
        {
            this.Create();
        }

        private void Create()
        {
            this.DivNonyuZan.Visible = false;
            this.DivKenshu.Visible = false;

            if (this.TabUpload.SelectedTab == null) { return; }

            switch (this.TabUpload.SelectedTab.Text)
            {
                case "納入残情報":
                    this.DivNonyuZan.Visible = true;
                    break;

                case "検収情報":
                    this.DivKenshu.Visible = true;
                    this.CtlKenshuDownload1.Create();
                    break;
            }
        }
    }
}
