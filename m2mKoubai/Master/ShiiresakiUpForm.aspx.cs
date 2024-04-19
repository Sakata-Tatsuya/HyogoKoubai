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
    public partial class ShiiresakiUpForm : System.Web.UI.Page
    {

        private string VsShiiresakiCode
        {
            get
            {
                return Convert.ToString(this.ViewState["VsShiiresakiCode"]);
            }
            set
            {
                this.ViewState["VsShiiresakiCode"] = value;
            }
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
                    this.Create_Shinki();

                }
                else
                {
                    try
                    {
                        // Code
                        this.VsShiiresakiCode = Request.QueryString["key"];
                    }
                    catch
                    {
                        this.ShowTblMain(false);
                        this.ShowMsg(AppCommon.NO_DATA, true);
                        return;
                    }

                    // 更新
                    this.CreateKoushin();
                }

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
            this.BtnT.Attributes["onclick"] = "Touroku(); return false; ";
            // 更新ボタン
            this.BtnK.Attributes["onclick"] = "Koushin(); return false; ";
            // 閉じるボタン
            this.BtnC.Attributes["onclick"] = "Close(); return false; ";
            
            // 登録ボタン(submit)
            this.BtnTouroku.Style.Add("display", "none");
            // 更新ボタン(submit)
            this.BtnKoushin.Style.Add("display", "none");
        }

        private void ShowMsg(string strMsg, bool bErrorMsg)
        {
            this.LblMsg.ForeColor = (bErrorMsg) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
            this.LblMsg.Text = strMsg;
        }

        // 新規登録
        private void Create_Shinki()
        {
            this.ShinkiTouroku(true);
            this.ShowTblMain(true);
        }

        private void CreateKoushin()
        {
            this.ShinkiTouroku(false);
            this.ShowTblMain(true);

            m2mKoubaiDataSet.M_ShiiresakiRow drShiiresaki =
                ShiiresakiClass.GetV_SHiiresakiRow(VsShiiresakiCode, Global.GetConnection());

            if (drShiiresaki == null)
            {
                this.ShowMsg("仕入先が見つかりませんでした", true);
                this.ShowTblMain(false);
                return;
            }

            // 仕入先コード
           //this.TbxShiireCode.Text = drShiiresaki.ShiiresakiCode;
            this.LitCode.Text =  drShiiresaki.ShiiresakiCode;
            // 仕入先名
            this.TbxShiireName.Text = drShiiresaki.ShiiresakiMei;
            // 郵便番号            
            this.TbxYuubin.Text = drShiiresaki.YubinBangou;
            // 住所
            this.TbxJyusho.Text = drShiiresaki.Address;
            // TEL
            this.TbxTel.Text = drShiiresaki.Tel;
            // FAX
            this.TbxFax.Text = drShiiresaki.Fax;
            // 振込先
           // this.TbxFurikomisaki.Text = drShiiresaki.FurikomiSaki;
            // 口座名義
            this.TbxKouzameigi.Text = drShiiresaki.KouzaMeigi;
            // 金融機関名
            this.TbxKinyuukikanMei.Text = drShiiresaki.KinyuuKikanMei;
            // 口座番号
            this.TbxKouzaBangou.Text = drShiiresaki.KouzaBangou;
            // 支払締日
            this.DdlShimebi.SelectedValue = drShiiresaki.ShiharaiShimebi.ToString();
            // 支払予定日1
            this.DdlYotebi1.SelectedValue = AppCommon.YoteibiDdl1Select(drShiiresaki.ShiharaiYoteibi);
            // 支払予定日2
            this.DdlYotebi2.SelectedValue = AppCommon.YoteibiDdl2Select(drShiiresaki.ShiharaiYoteibi);
            // 検収情報公開
            if (drShiiresaki.KensyukoukaiFlg == true)
                this.RbtnKoukai.Checked = true;
            else
                this.RbtnKoukaiNashi.Checked = true;
            // 納期回答催促メール
            if (drShiiresaki.SaisokuMailFlg == true)
                this.RbtnSoushin.Checked = true;
            else
                this.RbtnSoushinNashi.Checked = true;
            // 仕入先情報更新許可
            if (drShiiresaki.KousinKyokaFlg)
                this.RbtnKyoka.Checked = true;
            else
                this.RbtnKyokaNashi.Checked = true;
        }

        // 登録、更新ボタン表示
        private void ShinkiTouroku(bool b)
        {
            // 登録            
            BtnT.Visible = b;
            // 更新            
            BtnK.Visible = !b;
            if (!b)
            {
                TbxShiireCode.Visible = b;
            }
        }

        private void ShowTblMain(bool b)
        {
            this.TblMain.Visible = b;
            for (int i = 0; i < TblMain.Controls.Count; i++)
                TblMain.Controls[i].EnableViewState = b;
            this.TblBtn.Visible = b;
        }


        private m2mKoubaiDataSet.M_ShiiresakiRow CreateRow(bool b)
        {
            m2mKoubaiDataSet.M_ShiiresakiRow dr = ShiiresakiClass.newM_ShiiresakiRow();
            
            //仕入先コード
            dr.ShiiresakiCode = this.TbxShiireCode.Text;
            // 仕入先名
            dr.ShiiresakiMei = this.TbxShiireName.Text;
            // 郵便番号
            dr.YubinBangou = this.TbxYuubin.Text;
            // 住所
            dr.Address = this.TbxJyusho.Text;
            // TEL
            dr.Tel = this.TbxTel.Text;
            // FAX
            dr.Fax = this.TbxFax.Text;
            // 振込先
           // dr.FurikomiSaki = this.TbxFurikomisaki.Text;
            // 口座名義
            dr.KouzaMeigi = this.TbxKouzameigi.Text;
            // 金融機関名
            dr.KinyuuKikanMei = this.TbxKinyuukikanMei.Text;
            // 口座番号
            dr.KouzaBangou = this.TbxKouzaBangou.Text;
            // 支払締日
            dr.ShiharaiShimebi = int.Parse(DdlShimebi.SelectedValue);
                //AppCommon.ShiharaiShimebiUp(DdlShimebi.SelectedValue);
            // 支払予定日
            dr.ShiharaiYoteibi = AppCommon.ShiharaiYoteibiUp(DdlYotebi1.SelectedValue + DdlYotebi2.SelectedValue);
            //検収情報詳細
            if (this.RbtnKoukai.Checked)
            {
                dr.KensyukoukaiFlg = true;
            }
            else
            {
                dr.KensyukoukaiFlg = false;
            }
            //納期回答催促メール
            if (this.RbtnSoushin.Checked)
            {
                dr.SaisokuMailFlg = true;
            }
            else
            {
                dr.SaisokuMailFlg = false;
            }
            //仕入先情報更新許可
            if (this.RbtnKyoka.Checked)
            {
                dr.KousinKyokaFlg = true;
            }
            else
            {
                dr.KousinKyokaFlg = false;
            }

            return dr;
        }

        // 新規登録重複チェック
        private bool TourokuCheck(m2mKoubaiDataSet.M_ShiiresakiRow dr)
        {
            // データ重複チェック
            m2mKoubaiDataSet.M_ShiiresakiRow drChk =
                ShiiresakiClass.getM_ShiiresakiRow(dr.ShiiresakiCode, Global.GetConnection());
            if (drChk != null)
            {
                this.ShowMsg("データが重複しています", true);
                return false;
            }

            return true;
        }
        // 登録ボタンクリック
        protected void BtnTouroku_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.M_ShiiresakiRow dr = this.CreateRow(true);
            if (dr == null)
                return;
            if (!TourokuCheck(dr))
                return;
            LibError err = ShiiresakiClass.M_Shiiresaki_Insert(dr, Global.GetConnection());
            if (err != null)
            {
                this.ShowMsg(err.Message, true);
                return;
            }

            this.ShowMsg("登録しました", false);

            loadFlg = 1;
        }

        // 更新ボタンクリック
        protected void BtnKoushin_Click(object sender, EventArgs e)
        {
            m2mKoubaiDataSet.M_ShiiresakiRow dr1 = this.CreateRow(false);
            if (dr1 == null)
                return;

            LibError err1 = ShiiresakiClass.M_Shiiresaki_Update(VsShiiresakiCode, dr1, Global.GetConnection());
            if (err1 != null)
            {
                this.ShowMsg(err1.Message, true);
                return;
            }
            //CreateKoushin();
            this.ShowMsg("更新しました", false);

            loadFlg = 1;
        }

       
    }
}
