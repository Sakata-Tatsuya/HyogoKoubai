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
    public partial class BuhinUpForm : System.Web.UI.Page
    {
        /*
        // 部品区分
        private string VsKubun
        {
            get { return Convert.ToString(this.ViewState["VsKubun"]); }
            set { this.ViewState["VsKubun"] = value; }
        }
        */
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
                    this.SetList();
                    
                }
                else
                {
                    try
                    {
                        //string[] strKey = Request.QueryString["key"].Split('_');
                        string strKey = Request.QueryString["key"];
                        //this.VsKubun = strKey[0];
                        // Code
                        this.VsCode = strKey;
                    }
                    catch
                    {
                        this.ShowTblMain(false);
                        this.ShowMsg(AppCommon.NO_DATA, true);
                        return;
                    }

                    this.SetList();
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
            //AppCommon.NumOnly(this.TbxCode);
            //AppCommon.NumOnly(this.TbxLeadTime);

            // 登録ボタン(submit)
            this.BtnTS.Style.Add("display", "none");
            // 更新ボタン(submit)
            this.BtnKS.Style.Add("display", "none");
        }

        public void SetList()
        {
            //ListSet.SetDdlTani(DdlTani);
            ListSet.SetDdlLeadTime(DdlLeadTime);

            // 仕入先1
            ListSet.SetDdlShiiresaki(DdlShiire1);
            // 仕入先2
            ListSet.SetDdlShiiresaki(DdlShiire2);

            // 科目名
            ListSet.SetKamokuMei(DdlKamoku);

            // 費用科目名
            ListSet.SetHiyouMei(DdlHiyou);

            // 補助科目名
            ListSet.SetHojyoMei(DdlHojyo);
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
            m2mKoubaiDataSet.M_BuhinRow dr =
                BuhinClass.getM_BuhinRow(VsCode, Global.GetConnection());
            if (dr == null)
            {
                this.ShowTblMain(false);
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            // 品目グループ
            TbxKubun.Text = dr.BuhinKubun;
            // 品目コード            
            LitCode.Text = dr.BuhinCode;
            // 品目名
            TbxHinmei.Text = dr.BuhinMei;
            // 単価
            if (dr.Tanka != 0)
            {
                this.TbxTanka.Text = dr.Tanka.ToString();             
            }
            // 単位
            TbxTani.Text = dr.Tani;
            // ロット
            if (dr.Lot != 0)
                this.TbxLot.Text = dr.Lot.ToString();
            // リードタイム
            if (dr.LT_Suuji != 0 && dr.LT_Tani != 0)
            {
                TbxLeadTime.Text = dr.LT_Suuji.ToString();
                DdlLeadTime.SelectedValue = dr.LT_Tani.ToString();
            }
            // 仕入先
            DdlShiire1.SelectedValue = dr.ShiiresakiCode1;
            if (dr.ShiiresakiCode2 != "")
                DdlShiire2.SelectedValue = dr.ShiiresakiCode2;

            // 科目
            DdlKamoku.SelectedValue = dr.KanjyouKamokuCode.ToString();            
            // 費用
            DdlHiyou.SelectedValue = dr.HiyouKamokuCode.ToString();
            // 補助
            DdlHojyo.SelectedValue = dr.HojyoKamokuNo.ToString();

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
                //                
                TbxCode.Visible = b; 
            }
        }




        // Row作成
        private m2mKoubaiDataSet.M_BuhinRow CreateRow(bool bShinki)
        {
            m2mKoubaiDataSet.M_BuhinRow dr = BuhinClass.newM_BuhinRow();

            // コード
            if (bShinki)　
            {      
                // 新規登録                
                dr.BuhinCode = TbxCode.Text;
            }
            else 
            {
                // 更新                
                dr.BuhinCode = LitCode.Text;
            }
            // 品目グループ
            dr.BuhinKubun = TbxKubun.Text;            
            // 品目名
            dr.BuhinMei = TbxHinmei.Text;
            // 単価              
            if (TbxTanka.Text != "")
                dr.Tanka = decimal.Parse(this.TbxTanka.Text);
            else
                dr.Tanka = 0;
            // 単位            
            dr.Tani = TbxTani.Text;
            // ロット
            if (this.TbxLot.Text != "")
                dr.Lot = int.Parse(this.TbxLot.Text);
            else
            {
                dr.Lot = 0;
            }
            // リードタイム
            if (this.TbxLeadTime.Text != "" && this.DdlLeadTime.SelectedIndex > 0)
            {
                dr.LT_Suuji = int.Parse(TbxLeadTime.Text);
                dr.LT_Tani = byte.Parse(DdlLeadTime.SelectedValue);
            }
            else
            {
                dr.LT_Suuji = 0;
                dr.LT_Tani = 0;
            }

            // 仕入先1
            dr.ShiiresakiCode1 = this.DdlShiire1.SelectedValue;
            // 仕入先2
            if (this.DdlShiire2.SelectedIndex > 0)
            {
                dr.ShiiresakiCode2 = this.DdlShiire2.SelectedValue;
            }
            else
            {
                dr.ShiiresakiCode2 = "";
            }

            // 科目
            dr.KanjyouKamokuCode = int.Parse(this.DdlKamoku.SelectedValue);
            // 費用
            dr.HiyouKamokuCode = int.Parse(this.DdlHiyou.SelectedValue);
            // 補助
            dr.HojyoKamokuNo = int.Parse(this.DdlHojyo.SelectedValue);

            return dr;
        }
        // 新規登録重複チェック
        private bool TourokuCheck(m2mKoubaiDataSet.M_BuhinRow dr)
        {
            // データ重複チェック
            m2mKoubaiDataSet.M_BuhinRow drChk =
                BuhinClass.getM_BuhinRow(dr.BuhinCode, Global.GetConnection());
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
            m2mKoubaiDataSet.M_BuhinRow dr = this.CreateRow(true);
            if (dr == null)
                return;
            if (!TourokuCheck(dr))
                return;
            LibError err = BuhinClass.M_Buhin_Insert(dr, Global.GetConnection());
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
            m2mKoubaiDataSet.M_BuhinRow dr1 = this.CreateRow(false);
            if (dr1 == null)
                return;

            LibError err1 = BuhinClass.M_Buhin_Update(VsCode, dr1, Global.GetConnection());
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
