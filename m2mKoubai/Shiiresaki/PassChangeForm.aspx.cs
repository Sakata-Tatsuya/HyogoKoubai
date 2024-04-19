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
namespace m2mKoubai.Shiiresaki
{
    public partial class PassChangeForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Shiiresaki)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, false);
                    return;
                }

                CtlTabShiire tab = FindControl("Tab") as CtlTabShiire;
                tab.Menu = CtlTabShiire.MainMenu.PassChange;
               
            }
            
        }
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.PreRender += new EventHandler(PassChangeForm_PreRender);
        }

        private void PassChangeForm_PreRender(object sender, EventArgs e)
        {
            // 登録ボタン(input)
            this.BtnT.Attributes["onclick"] = "Check(); return false;";
            // ロードimg
            this.Img1.Style.Add("display", "none");
            // 半角チェック
            this.Hankaku();


        }
        private void Hankaku()
        {
            //
            TbxPass.Attributes["onfocusout"] =
                string.Format("HankakuChk('{0}','{1}'); ", TbxPass.ClientID, "パスワード");
            //
            TbxPass2.Attributes["onfocusout"] =
                string.Format("HankakuChk('{0}','{1}'); ", TbxPass2.ClientID, "確認用パスワード");
        }

        // エラーメッセージ表示用
        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;

        }

        protected void Ram_AjaxRequest(object sender, Telerik.WebControls.AjaxRequestEventArgs e)
        {
            this.Ram.AjaxSettings.Clear();

            string[] strArgs = e.Argument.Split(':');
            string strCmd = strArgs[0];

            if (strCmd == "henkou")
            {
                string strPass = this.TbxPass.Text;

                // ログインIDによって、パスワード、親仕入先、メールアドレスを変更
                LibError err =
                    LoginClass.M_Login_Update_Password(SessionManager.LoginID, strPass, Global.GetConnection());
                if (err != null)
                {
                    this.ShowMsg(err.Message, true);
                    return;
                }
                this.ShowMsg("変更しました", false);
                this.Ram.AjaxSettings.AddAjaxSetting(this.Ram, this.TblList);

            }
        }
    }
}
