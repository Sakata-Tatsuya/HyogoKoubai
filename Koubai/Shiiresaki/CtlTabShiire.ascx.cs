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
                // ���O�A�E�g                
                this.TabLO.Attributes["onclick"] =
                    string.Format("if(confirm('���O�A�E�g���܂����H'))location.href = '{0}';", Global.LoginPageURL);

                if (!SessionManager.KensyukoukaiFlg)
                {
                    // �������
                    this.Tab.Tabs[(int)MainMenu.Kensyu_Jyouhou].Visible = false;
                }
                if (!SessionManager.KaishaKoushinFlg)
                {
                    // ��Џ��
                    this.Tab.Tabs[(int)MainMenu.Kaisha_Jyouhou].Visible = false;
                }
            }
        }
        public enum MainMenu
        {
            // �󒍏��
            Jyuchuu_Jyouhou = 0,
            // �������
            Kensyu_Jyouhou = 1,
            // �p�X���[�h�ύX
            PassChange = 2,
            // ��Џ��
            Kaisha_Jyouhou = 3,
            // ���O�A�E�g
            LogOut = 4,
        }
        public MainMenu Menu
        {
            get { return (MainMenu)this.Tab.SelectedIndex; }
            set { Tab.SelectedIndex = (int)value; }
        }
    }
}