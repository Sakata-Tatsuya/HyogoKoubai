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
    public partial class NounyuBashoUpForm : System.Web.UI.Page
    {
       
        // Code
        private string VsCode
        {
            get { return Convert.ToString(this.ViewState["VsCode"]); }
            set { this.ViewState["VsCode"] = value; }
        }

        protected int loadFlg = 0;  // 表示元window_reload用


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Owner) // 発注側のみ表示可
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
                        // Code
                        this.VsCode = Request.QueryString["key"];                      
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
            // 数字のみ入力可能
            AppCommon.NumOnly(this.TbxCode);
           
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
            m2mKoubaiDataSet.M_NounyuuBashoRow dr = NounyuuBashoClass.getM_NounyuuBashoRow(VsCode, Global.GetConnection());
            if (dr == null)
            {
                this.ShowTblMain(false);
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            

            // 納入場所コード
           // TbxCode.Text = dr.NounyuuBashoCode;
            LitCode.Text = dr.BashoCode;
            // 納入場所名
            TbxName.Text = dr.BashoMei;

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

            if (!b)
            {                
                TbxCode.Visible = b;
            }
        }




        // Row作成
        private m2mKoubaiDataSet.M_NounyuuBashoRow CreateRow()
        {
            m2mKoubaiDataSet.M_NounyuuBashoRow dr = NounyuuBashoClass.newM_NounyuuBashoRow();
           
            // 納入場所コード
            dr.BashoCode = TbxCode.Text;
            // 納入場所名
            dr.BashoMei = TbxName.Text;          

            return dr;
        }
        // 新規登録重複チェック
        private bool TourokuCheck(m2mKoubaiDataSet.M_NounyuuBashoRow dr)
        {
            // データ重複チェック
            m2mKoubaiDataSet.M_NounyuuBashoRow drChk =
                NounyuuBashoClass.getM_NounyuuBashoRow(dr.BashoCode, Global.GetConnection());
            if (drChk != null)
            {
                this.ShowMsg("データが重複しています", true);
                return false;
            }

            return true;
        }
        // 登録ボタンクリック
        protected void BtnTS_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.M_NounyuuBashoRow dr = this.CreateRow();
            if (dr == null)
                return;
            if (!TourokuCheck(dr))
                return;
            LibError err = NounyuuBashoClass.M_NounyuuBasho_Insert(dr, Global.GetConnection());
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
            m2mKoubaiDataSet.M_NounyuuBashoRow dr1 = this.CreateRow();
            if (dr1 == null)
                return;

            LibError err1 = NounyuuBashoClass.M_NounyuuBasho_Update(VsCode, dr1, Global.GetConnection());
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
