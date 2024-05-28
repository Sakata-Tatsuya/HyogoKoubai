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
    public partial class KaishaInfoUpForm : System.Web.UI.Page
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
                if (SessionManager.UserKubun != (byte)UserKubun.Owner) // 発注側
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
                loadFlg = 0;

            }
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
            this.BtnT.Attributes["onclick"] = "Touroku(); return false;";
            // 更新ボタン
            this.BtnK.Attributes["onclick"] = "Koushin(); return false;";
            // 閉じるボタン
            this.BtnC.Attributes["onclick"] = "Close(); return false;";

            // 登録ボタン(submit)
            this.BtnTS.Style.Add("display", "none");
            // 更新ボタン(submit)
            this.BtnKS.Style.Add("display", "none");



          
            AppCommon.NumOnly(TbxKaishaID);

            
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
            m2mKoubaiDataSet.T_KaishaInfoRow dr = KaishaInfoClass.getT_KaishaInfoRow(int.Parse(VsCode), Global.GetConnection());
            if (dr == null)
            {
                this.ShowTblMain(false);
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            // 事業所コード
            LitCode.Text = dr.KaishaID.ToString();
            
            // 会社名
            //TbxKaishaMei.Text = dr.KaishaMei;
            // 事業所名
            TbxEigyousho.Text = dr.EigyouSho;
            // 住所
            TbxJyusho.Text = dr.Address;
            // 郵便番号
            TbxYubin.Text = dr.Yuubin;
            // TEL
            TbxTel.Text = dr.Tel;
            // FAX
            TbxFax.Text = dr.Fax;
            // E-Mail
            TbxMail.Text = dr.Mail;
            // 適格請求書発行事業者
            if (dr.InvoiceRegFlg)
            { this.RbtSumi.Checked = true; }
            else
            { this.RbtMi.Checked = true; }
            // 適格請求書発行事業者番号
            this.TbxInvoiceNo.Text = dr.InvoiceRegNo;

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
        // 登録・更新表示
        private void ShinkiTouroku(bool b)
        {
            // 登録
            BtnT.Visible = b;
            // 更新
            BtnK.Visible = !b;

            if (!b)
            {
                TbxKaishaID.Visible = b;
            }
        }

        // Row作成
        private m2mKoubaiDataSet.T_KaishaInfoRow CreateRow(bool bShinki)
        {
            m2mKoubaiDataSet.T_KaishaInfoRow dr = KaishaInfoClass.newT_KaishaInfoRow();

            // 事業所コード
            if (bShinki)
            {
                // 新規                
                dr.KaishaID = Convert.ToInt32(this.TbxKaishaID.Text);
            }
            else
            {
                // 更新
                dr.KaishaID = Convert.ToInt32(LitCode.Text);
            }
            // 会社名
            dr.KaishaMei = "";
           
            // 事業所名
            dr.EigyouSho = this.TbxEigyousho.Text;
            // 住所
            dr.Address = this.TbxJyusho.Text;
            // 郵便番号
            dr.Yuubin =this.TbxYubin.Text;
            // TEL
            dr.Tel = this.TbxTel.Text;
            // FAX
            dr.Fax = this.TbxFax.Text;
            // E-Mail
            dr.Mail = this.TbxMail.Text;
            // 適格請求書発行事業者
            if (this.RbtSumi.Checked)
            { dr.InvoiceRegFlg = true; }
            else
            { dr.InvoiceRegFlg = false; }
            // 適格請求書発行事業者番号
            dr.InvoiceRegNo = this.TbxInvoiceNo.Text;

            return dr;
        }
        // 新規登録重複チェック
        private bool TourokuCheck(m2mKoubaiDataSet.T_KaishaInfoRow dr)
        {
            // データ重複チェック
            m2mKoubaiDataSet.T_KaishaInfoRow drChk = KaishaInfoClass.getT_KaishaInfoRow(dr.KaishaID, Global.GetConnection());
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
            m2mKoubaiDataSet.T_KaishaInfoRow dr = this.CreateRow(true);
            if (dr == null)
                return;
            if (!TourokuCheck(dr))
                return;
            // 登録
            LibError err = KaishaInfoClass.T_KaishaInfo_Insert(dr, Global.GetConnection());
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
            m2mKoubaiDataSet.T_KaishaInfoRow dr1 = this.CreateRow(false);
            if (dr1 == null)
                return;

            LibError err1 = KaishaInfoClass.T_KaishaInfo_Update(int.Parse(VsCode), dr1, Global.GetConnection());
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
