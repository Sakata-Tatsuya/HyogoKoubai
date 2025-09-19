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

namespace Koubai.Shiiresaki
{
    public partial class CtlTabShiire : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ログアウト                
                this.TabLO.Attributes["onclick"] =
                    string.Format("if(confirm('ログアウトしますか？'))location.href = '{0}';", Global.LoginPageURL);

                if (!SessionManager.KensyukoukaiFlg)
                {
                    // 検収情報
                    this.Tab.Tabs[(int)MainMenu.Kensyu_Jyouhou].Visible = false;
                }
                if (!SessionManager.KaishaKoushinFlg)
                {
                    // 会社情報
                    this.Tab.Tabs[(int)MainMenu.Kaisha_Jyouhou].Visible = false;
                }
            }
        }
        public enum MainMenu
        {
            // 受注情報
            Jyuchuu_Jyouhou = 0,
            // 検収情報
            Kensyu_Jyouhou = 1,
            // パスワード変更
            PassChange = 2,
            // 会社情報
            Kaisha_Jyouhou = 3,
            // ログアウト
            LogOut = 4,
        }
        public MainMenu Menu
        {
            get { return (MainMenu)this.Tab.SelectedIndex; }
            set { Tab.SelectedIndex = (int)value; }
        }
    }
}