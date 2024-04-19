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
namespace m2mKoubai.Master
{
    public partial class LoginMsgUpForm : System.Web.UI.Page
    {
        // MsgID
        private int VsMsgID
        {
            get { return Convert.ToByte(this.ViewState["VsMsgID"]); }
            set { this.ViewState["VsMsgID"] = value; }
        }

        protected int loadFlg = 0;  // 表示元window_reload用


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Yodoko) // Yodoko側のみ表示可
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }

                if (Request.Url.Query == "")
                {
                    // 新規
                    this.Shinki();
                }
                else
                {
                    try
                    {
                        // メッセージID
                        this.VsMsgID = int.Parse(Request.QueryString["MsgID"]); // MsgID
                    }
                    catch
                    {
                        this.ShowTblMain(false);
                        this.ShowMsg(AppCommon.NO_DATA, true);
                        return;
                    }
                    // 更新
                    this.Koushin();
                }
            }

            loadFlg = 0;
        }

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.PreRender += new EventHandler(LoginMsgUpForm_PreRender);
        }

        private void LoginMsgUpForm_PreRender(object sender, EventArgs e)
        {
            // 登録ボタン
            this.BtnT.Attributes["onclick"] = "Touroku(); return false; ";
            // 更新ボタン
            this.BtnK.Attributes["onclick"] = "Koushin(); return false; ";
            // 閉じるボタン
            this.BtnC.Attributes["onclick"] = "Close(); return false; ";


            // 登録ボタン(submit)
            this.BtnTS.Style.Add("display", "none");
            // 更新ボタン(submit)
            this.BtnKS.Style.Add("display", "none");          
        }

        

        // 新規 
        private void Shinki()
        {
            this.ShinkiTouroku(true);
            this.ShowTblMain(true);
        }
        // 更新
        private void Koushin()
        {
            m2mKoubaiDataSet.M_LoginMsgRow dr =
                LoginMsgClass.getM_LoginMsgRow(VsMsgID, Global.GetConnection());
            if (dr == null)
            {
                this.ShowTblMain(false);
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            // 有効/無効
            if (dr.DelFlg)
                this.RbtnMukou.Checked = true;
            // msg
            this.TbxMsg.Text = dr.Msg;
            
           
            this.ShinkiTouroku(false);
            this.ShowTblMain(true);
        }
        // メッセージ表示
        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }

        // メインテーブル表示、非表示
        private void ShowTblMain(bool b)
        {
            this.TblMain.Visible = b;
            this.TblBtn.Visible = b;
        }
        // 登録、更新表示
        private void ShinkiTouroku(bool b)
        {
            // 登録            
            BtnT.Visible = b;
            // 更新            
            BtnK.Visible = !b;
        }

        // Row作成
        private m2mKoubaiDataSet.M_LoginMsgRow CreateRow(bool bSinki)
        {
            m2mKoubaiDataSet.M_LoginMsgRow dr = LoginMsgClass.newM_LoginMsgRow();
            if (bSinki)
            {
                // 新規時                
                // 登録日
                dr.TourokuBi = DateTime.Now;
            }
            else
            {
                // 更新時              
                // 更新日
                dr.KoushinBi = DateTime.Now;
            }
           
            // メッセージ
            dr.Msg = this.TbxMsg.Text;
            
            if (this.RbtnYukou.Checked)
            {
                dr.DelFlg = false;
            }
            else
            {
                dr.DelFlg = true;
            }
            return dr;
        }

        // 登録ボタンクリック
        protected void BtnTS_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.M_LoginMsgRow dr = this.CreateRow(true);
            if (dr == null)
                return;

            LibError err = LoginMsgClass.M_LoginMsg_Insert(dr, Global.GetConnection());
            if (err != null)
            {
                this.ShowMsg(err.Message, true);
                return;
            }
            this.ShowMsg("登録しました", false);

            loadFlg = 1;
        }

        // 更新ボタンクリック
        protected void BtnKS_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.M_LoginMsgRow dr1 = this.CreateRow(false);
            if (dr1 == null)
                return;

            LibError err1 = LoginMsgClass.M_LoginMsg_Update(VsMsgID, dr1, Global.GetConnection());
            if (err1 != null)
            {
                this.ShowMsg(err1.Message, true);
                return;
            }
            this.ShowMsg("更新しました", false);

            loadFlg = 1;
        }

    }
}
