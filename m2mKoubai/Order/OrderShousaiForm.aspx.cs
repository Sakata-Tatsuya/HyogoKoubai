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
using System.Drawing;

namespace m2mKoubai.Order
{
    public partial class OrderShousaiForm : System.Web.UI.Page
    {
        /// <summary>
        /// 主キー1
        /// </summary>
        private string VsYear
        {
            get { return Convert.ToString(this.ViewState["VsYear"]); }
            set { this.ViewState["VsYear"] = value; }
        }
        /// <summary>
        /// 主キー2
        /// </summary>
        private string VsHacchuuNo
        {
            get { return Convert.ToString(this.ViewState["VsHacchuuNo"]); }
            set { this.ViewState["VsHacchuuNo"] = value; }
        }
        /// <summary>
        /// 主キー3
        /// </summary>
        private int VsKubun
        {
            get { return Convert.ToInt32(this.ViewState["VsKubun"]); }
            set { this.ViewState["VsKubun"] = value; }
        }

        bool b = false;
        protected int loadFlg = 0;  // 表示元window_reload用
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
             {
                string[] strAry = HttpContext.Current.Request.Form["HidKey"].Split('\t');
                if (strAry == null)
                {
                    ShowMsg(AppCommon.NO_DATA, true);
                    this.ShowTblList(false);
                    return;
                }
                // 注文キー情報
                ChumonClass.ChumonKey key = new ChumonClass.ChumonKey(strAry[0]);

                // 主キー1
                VsYear = key.Year;
                // 主キー2
                VsHacchuuNo = key.HacchuuNo;
                // 主キー3
                VsKubun = key.JigyoushoKubun;

                if (strAry[1] != "")
                    this.Create(!b);　// 発注情報画面からの場合
                else
                {
                    this.Create(b);　// 発注情報画面以外からの場合
                    this.LblMsg.Visible = b;
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
            this.PreRender += new System.EventHandler(this.Form_PreRender);
        }

        private void Form_PreRender(object sender, EventArgs e)
        {
            // キャンセルボタン
            this.BtnCancel.Attributes["onclick"] = "Cancel(); return false;";
            // 
            this.BtnProcess.Style.Add("display", "none");
            // 更新ボタン
            this.BtnK.Attributes["onclick"] =
                string.Format("Koushin('{0}','{1}'); return false;", LitSuuryou.Text, LblTanka.Text);
        }

        private void Create(bool b)
        {
            // Hidden
            HidProcess.Value = "";

            ChumonDataSet.V_Chumon_MeisaiRow dr =
                ChumonClass.getV_Chumon_MeisaiRow(VsYear, VsHacchuuNo, VsKubun, Global.GetConnection());
            if (dr != null)
            {
                // 発注No
                LitHacchuNo.Text = dr.HacchuuNo;
                // 発注日
                LitHacchuubi.Text = dr.HacchuuBi.ToString("yy/MM/dd");
                // 仕入先コード
                LitShiireCode.Text = dr.ShiiresakiCode;
                // 仕入先名
                LitShiireName.Text = dr.ShiiresakiMei;
                // 品目グループ
                this.LitKubun.Text = dr.BuhinKubun;
                // コード
                LitBuhinCode.Text = dr.BuhinCode;
                // 品目名
                LitBuhinName.Text = dr.BuhinMei;
                // 数量
                LitSuuryou.Text = dr.Suuryou.ToString("#,##0");
                // 単価
                if (dr.KaritankaFlg)
                {
                    LblT.Text = "(仮)" + "\\";
                    LblTanka.Text = dr.Tanka.ToString();
                    LblT.ForeColor = Color.Red;
                    LblTanka.ForeColor = Color.Red;
                    //dr.Tanka.ToString("#,##0.#0");
                }
                else
                {
                    LblT.Text = "\\";
                    LblTanka.Text = dr.Tanka.ToString();
                   // LblT.ForeColor = Color.Black;
                    //LblTanka.ForeColor = Color.Black;
                }
                // 注文金額
                // 増税対応 小数部は切り捨てではなく四捨五入
                //decimal Kingaku = Math.Floor(dr.Suuryou * dr.Tanka);
                decimal Kingaku = Math.Round(dr.Suuryou * dr.Tanka, 0, MidpointRounding.AwayFromZero);

                if (dr.KaritankaFlg)
                {

                    LblKingaku.Text = "\\" + Kingaku.ToString("#,##0");
                    LblKingaku.ForeColor = Color.Red;
                }
                else
                {
                    LblKingaku.Text = "\\" + Kingaku.ToString("#,##0");
                    LblKingaku.ForeColor = Color.Black;
                }
                // 単位
                LitTani.Text = dr.Tani;
                // 納入場所コード
                //LitNBashoCode.Text = dr.NounyuuBashoCode;
                // 納入場所名
                LitNBashoMei.Text = dr.BashoMei;
                // 発注担当者コード
                LitTantoushaID.Text = dr.TantoushaCode;
                // 発注担当者名
                LitTantoushaMei.Text = dr.Name;
                // 備考
                TbxBikou.Text = dr.Bikou;
                // 
                if (b)
                {
                    if (!dr.IsCancelBiNull())
                    {
                        // キャンセルメッセージ
                        LblMsg.Text = "キャンセル日時：" + dr.CancelBi.ToString("yy/MM/dd HH:mm");
                        // 変更コントロールの非表示
                        ShowHouji(!b);
                    }
                    else
                    {
                        // 変更コントロールの表示
                        ShowHouji(b);
                    }
                }
                else
                {
                    ShowHouji(b);
                }

                this.ShowTblList(true);
            }
            else
            {
                ShowMsg(AppCommon.NO_DATA, true);
                this.ShowTblList(false);
                return;
            }
        }
        // 変更コントロールの表示
        private void ShowHouji(bool b)
        {
            // キャンセルメッセージの表示
            LblMsg.Visible = !b;
            // キャンセルボタン
            BtnCancel.Visible = b;
            // 更新ボタン
            BtnK.Visible = b;
            // 数量変更テーブル
            TblS.Visible = b;
            // 単価変更テーブル
            TblT.Visible = b;
        }
        // メインテーブル非表示
        private void ShowTblList(bool b)
        {
            TblAll.Visible = b;
        }

        private void ShowMsg(string strMsg, bool bError)
        {
            LblErrMsg.Text = strMsg;
            LblErrMsg.ForeColor = (bError) ? Color.Red : Color.Black;
        }

        protected void BtnProcess_Click(object sender, EventArgs e)
        {
            // 処理の内容
            string strProcess = this.HidProcess.Value;
            if (strProcess == "cancel")
            {

                m2mKoubaiDataSet.T_ChumonRow dr = ChumonClass.newT_ChumonRow();
                // キャンセル日
                dr.CancelBi = DateTime.Now;
                // キャンセル日の登録
                LibError err = ChumonClass.T_Chumon_Update_CancelBi(VsYear, VsHacchuuNo, VsKubun, dr, Global.GetConnection());
                if (err == null)
                {
                    // 主キーによって、メール送信に必要データ取得
                   ChumonDataSet.V_MailInfoDataTable dtMail =
                                              ChumonClass.getV_MailInfoDataTable(VsYear, VsHacchuuNo, VsKubun, Global.GetConnection());
                    
                    for (int i = 0; i < dtMail.Rows.Count; i++)
                    {
                        MailClass.MailParam p = this.GetMailParam(dtMail[i], strProcess);

                        MailClass.SendMail(p, null);
                    }
                }
                else
                {
                    this.ShowMsg(e.ToString(), true);
                    return;
                }
                this.Create(true);
                this.ShowMsg("キャンセルしました", false);

                loadFlg = 1;
            }

            else if (strProcess == "koushin")
            {
                // NewRow
                m2mKoubaiDataSet.T_ChumonRow dr = ChumonClass.newT_ChumonRow();


                if (TbxT.Text != "")
                    dr.Tanka = decimal.Parse(TbxT.Text);
                else
                    dr.Tanka = -1;


                if (TbxS.Text != "")
                    dr.Suuryou = int.Parse(TbxS.Text.Replace(",",""));
                else
                    dr.Suuryou = -1;
                // 数量、単価を更新する
                LibError err = ChumonClass.T_Chumon_Update(VsYear, VsHacchuuNo, VsKubun, dr, Global.GetConnection());
                if (err == null)
                {

                    // 数量変更のみメール送信
                    if (TbxS.Text != "")
                    {
                        // 主キーによって、メール送信に必要データ取得                     
                        ChumonDataSet.V_MailInfoDataTable dtMail =
                           ChumonClass.getV_MailInfoDataTable(VsYear, VsHacchuuNo, VsKubun, Global.GetConnection());
                        for (int i = 0; i < dtMail.Rows.Count; i++)
                        {
                            MailClass.MailParam p = this.GetMailParam(dtMail[i], strProcess);

                            MailClass.SendMail(p, null);
                        }
                    }
                }
                else
                {
                    this.ShowMsg(e.ToString(), true);
                    return;
                }
                this.Create(true);

                ShowMsg("更新しました", false);
                TbxT.Text = "";
                TbxS.Text = "";

                loadFlg = 1;
            }
        }
        private MailClass.MailParam GetMailParam(ChumonDataSet.V_MailInfoRow dr, string strMsg)
        {

            MailClass.MailParam p = new MailClass.MailParam();
            // 送信元メールアドレス
            p._MailFrom = dr.Mail_Y;
            // 送信先メールアドレス
            p._MailTo = dr.Mail_S;

            if (strMsg == "cancel")
            {
                // 件名
                p._Subject = "発注取消しのご案内";
                // 本文
                p._Body = MailClass.GetBody_Cancel(dr);
            }
            else
            {
                // 件名
                p._Subject = "発注数量変更のご案内";
                // 本文
                p._Body = MailClass.GetBody_Suuryou(dr);
            }

            // SMTP
            p._SMTP_Server = Global.SMTP_Server;
            return p;
        }
    }
}