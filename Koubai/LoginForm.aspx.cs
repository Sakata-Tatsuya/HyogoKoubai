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

using KoubaiDAL;
namespace Koubai
{
    public partial class LoginForm : System.Web.UI.Page
    {
        //public const int G_CELL_DATE = 0;
        public const int G_CELL_MESSAGE = 0;

        KoubaiDataSet.M_LoginMsgDataTable _dt = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Session.Clear();
                SessionManager.Logout();
                this.Create();
            }
        }

        // メッセージ表示
        private void ShowErrMsg(string strMsg)
        {
            LblErrMsg.Text = strMsg;
        }
        private void Create()
        {
            _dt = LoginMsgClass.getM_LoginMsgDataTable(Global.GetConnection());
            if (_dt.Rows.Count > 0)
            {
                G.DataSource = _dt;
                G.DataBind();
                //G.Attributes.Add("bordercolor", "#00A0FF"); // 水色
            }
            else
            {
                G.DataSource = new int[1];
                G.DataBind();
            }
        }

        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (_dt.Rows.Count > 0)
                {
                    KoubaiDataSet.M_LoginMsgRow dr = (KoubaiDataSet.M_LoginMsgRow)((DataRowView)e.Row.DataItem).Row;

                    e.Row.Cells[G_CELL_MESSAGE].Text = dr.Msg.Replace("\r\n", "<br>");
                }
            }
        }

        protected void BtnTouroku_Click(object sender, EventArgs e)
        {
            string strId = TbxID.Text.Trim();
            string strPass = TbxPass.Text.Trim();

            if (strId == "")
            {
                this.ShowErrMsg("ログインIDを入力して下さい");
                return;
            }
            if (strPass == "")
            {
                this.ShowErrMsg("パスワードを入力して下さい");
                return;
            }
            //
            // 認証
            KoubaiDataSet.M_LoginRow dr = LoginClass.getM_LoginRow(strId, strPass, Global.GetConnection());

            if (dr == null)
            {
                this.ShowErrMsg("ログインできませんでした<br>ログインID又はパスワードをお確かめ下さい");
                return;
            }

            // 仕入先の場合
            if (dr.UserKubun == (byte)UserKubun.Shiiresaki)
            {         
                // 会社情報を取得
                LoginDataSet.V_Shiiresaki_FlgRow drShiire = LoginClass.getV_Shiiresaki_FlgRow(strId, strPass, Global.GetConnection());
                // 会社情報が存在する場合
                if (drShiire != null)
                {
                    SessionManager.LoginShiiresaki(drShiire);
                }
                else
                {
                    // ログイン不可
                    this.ShowErrMsg("ログインできませんでした<br>ログインID又はパスワードをお確かめ下さい");
                    return;
                }
            }

            SessionManager.Login(dr,"ja");

            Session["SESSION_HOME_PATH"] = HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped) + System.Web.HttpContext.Current.Request.ApplicationPath;
            Session["SESSION_APP_ROOT"] = Global.AppRootURL;

            if (dr.UserKubun == (byte)UserKubun.Owner)
            {
                // 発注元
                this.Response.Redirect("~/Order/OrderInfoForm.aspx");
            }
            else
            {
                // 仕入先
                this.Response.Redirect("~/Shiiresaki/OrderInfoForm.aspx");
            }
        }
    }
}
