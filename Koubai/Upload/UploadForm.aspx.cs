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

namespace Koubai.Upload
{
    public partial class UploadForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                M.MenuName = "アップロード";
                //this.CtlTabMain1.Menu = CtlTabMain.MainMenu.Upload;
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
            this.DivHinmokuUpload.Visible = false;
            this.DivOrderUpload.Visible = false;

            if (this.TabUpload.SelectedTab == null) { return; }

            switch (this.TabUpload.SelectedTab.Text)
            {
                case "品目データ":
                    this.DivHinmokuUpload.Visible = true;
                    this.CtlHinmokuUpload1.Create();
                    break;

                case "発注データ":
                    this.DivOrderUpload.Visible = true;
                    this.CtlOrderUpload1.Create();
                    break;
            }
        }
    }
}
