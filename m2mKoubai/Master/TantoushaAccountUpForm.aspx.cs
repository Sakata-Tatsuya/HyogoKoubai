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
    public partial class TantoushaAccountUpForm : Core.Web.ServerViewStatePage
    {
        // LoginID
        private string VsLoginID
        {
            get { return Convert.ToString(this.ViewState["VsLoginID"]); }
            set { this.ViewState["VsLoginID"] = value; }
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
                    // 
                    ListSet.SetDdlJigyoushoKubun(SessionManager.UserKubun, DdlJigyoushoKubun);
                    // 新規
                    this.Shinki();

                }
                else
                {
                    try
                    {
                        // メッセージID
                        this.VsLoginID = Request.QueryString["key"]; // LoginID
                    }
                    catch
                    {
                        this.ShowTblMain(false);
                        this.ShowMsg(AppCommon.NO_DATA, true);
                        return;
                    }
                    // 
                    ListSet.SetDdlJigyoushoKubun(SessionManager.UserKubun, DdlJigyoushoKubun);
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
            this.PreRender += new EventHandler(Form_PreRender);
        }

        private void Form_PreRender(object sender, EventArgs e)
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

            // 数字のみ入力可
            AppCommon.NumOnly(TbxTCode);

            this.HankakuChk();

        }

        private void HankakuChk()
        {
            this.TbxID.Attributes["onfocusout"] =
               string.Format("HankakuChk('{0}','{1}');", TbxPass.ClientID, "ログインID");
            this.TbxPass.Attributes["onfocusout"] =
               string.Format("HankakuChk('{0}','{1}');", TbxPass.ClientID, "パスワード");
            this.TbxMail.Attributes["onfocusout"] =
               string.Format("HankakuChk('{0}','{1}');", TbxPass.ClientID, "E-Mail");

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
            m2mKoubaiDataSet.M_LoginRow dr =
                LoginClass.getM_LoginRow(VsLoginID, Global.GetConnection());
            if (dr == null)
            {
                this.ShowTblMain(false);
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            // ログインID
            LitLoginID.Text = dr.LoginID;
            
            //  部署
            TbxBusho.Text = dr.Busho;
            //　役職
            TbxYakushoku.Text = dr.Yakushoku;
            //　パスワード
            TbxPass.Text = dr.Password;
            
            //　担当者コード
            TbxTCode.Text = dr.TantoushaCode;
            //　担当者名
            TbxTName.Text = dr.Name;
            // メールアドレス
            TbxMail.Text = dr.Mail;
            // 管理者権限
            if (dr.KanrishaFlg)
                this.RbtnKanrisha.Checked = true;
            else
                this.RbtnKanrishaNashi.Checked = true;

            // 事業所区分
            DdlJigyoushoKubun.SelectedValue = dr.JigyoushoKubun.ToString();



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
            // 新規登録  
            if (!b)
            {
                TbxID.Visible = b;
            }
            // 登録            
            BtnT.Visible = b;
            // 更新            
            BtnK.Visible = !b;
        }

        // Row作成
        private m2mKoubaiDataSet.M_LoginRow CreateRow(bool bSinki)
        {
            m2mKoubaiDataSet.M_LoginRow dr = LoginClass.newM_LoginRow();

            if (bSinki)
            {
                // 新規時                
                // ログインID
                dr.LoginID = TbxID.Text;
            }
            else
            {
                // 更新時              
                // ログインID
                dr.LoginID = LitLoginID.Text;
            }
            //　ユーザー区分
            dr.UserKubun = (byte)UserKubun.Yodoko;
            //　仕入先コード
            dr.KaishaCode = "0";
            //  部署
            dr.Busho = TbxBusho.Text;
            //　役職
            dr.Yakushoku = TbxYakushoku.Text;
            //　パスワード
            dr.Password = TbxPass.Text;           
            //　管理者権限
            //dr.KanrishaFlg = false;
            if (this.RbtnKanrisha.Checked)
            {
                dr.KanrishaFlg = true;
            }
            else
            {
                dr.KanrishaFlg = false;
            }                       
           
            //　担当者コード
            dr.TantoushaCode = TbxTCode.Text;
            //　担当者名
            dr.Name = TbxTName.Text;
            // メールアドレス
            dr.Mail = TbxMail.Text;
            // 事業所区分
            dr.JigyoushoKubun = int.Parse(DdlJigyoushoKubun.SelectedValue);

            return dr;
        }
        // 新規登録重複チェック
        private bool TourokuCheck(m2mKoubaiDataSet.M_LoginRow dr)
        {
            // データ重複チェック
            m2mKoubaiDataSet.M_LoginRow drChk =
                LoginClass.getM_LoginRow(dr.LoginID, Global.GetConnection());
            if (drChk != null)
            {
                this.ShowMsg("ログインIDが重複しています", true);
                return false;
            }

            return true;
        }
        // 登録ボタンクリック
        protected void BtnTS_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.M_LoginRow dr = this.CreateRow(true);
            if (dr == null)
                return;
            // 新規登録重複チェック
            if (!TourokuCheck(dr))
                return;
            // 登録
            LibError err = LoginClass.M_Login_Insert(dr, Global.GetConnection());
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
            m2mKoubaiDataSet.M_LoginRow dr1 = this.CreateRow(false);
            if (dr1 == null)
                return;
            // 更新
            LibError err1 = LoginClass.M_Login_Update(VsLoginID, dr1, Global.GetConnection());
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