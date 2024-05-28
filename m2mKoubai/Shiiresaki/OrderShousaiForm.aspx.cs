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

namespace m2mKoubai.Shiiresaki
{
    public partial class OrderShousaiForm : System.Web.UI.Page
    {   /// <summary>
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
                ChumonClass.ChumonKey key =
                    new ChumonClass.ChumonKey(strAry[0]);

                // 主キー1
                VsYear = key.Year;
                // 主キー2
                VsHacchuuNo = key.HacchuuNo;
                // 主キー3
                VsKubun = key.JigyoushoKubun ;               
            }
            this.Create();
        }
        private void Create()
        {
            ChumonDataSet.V_Chumon_MeisaiRow dr =
                ChumonClass.getV_Chumon_MeisaiRow(VsYear, VsHacchuuNo, VsKubun, Global.GetConnection());
            if (dr != null)
            {
                if (!dr.IsCancelBiNull())
                {
                    // キャンセルメッセージ
                    LblMsg.Text = "キャンセル日時：" + dr.CancelBi.ToString("yy/MM/dd HH:mm");
                   
                }
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

                    LblTanka.Text = "(仮)" + "\\" + dr.Tanka.ToString("#,##0.#0");
                    LblTanka.ForeColor = Color.Red;
                    //dr.Tanka.ToString("#,##0.#0");
                }
                else
                {
                    LblTanka.Text = "\\" + dr.Tanka.ToString("#,##0.#0");
                    LblTanka.ForeColor = Color.Black;
                }
                // 注文金額
                // 増税対応
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
                /*
                // 単価
                LitTanka.Text = dr.Tanka.ToString("#,##0.#0");
                // 注文金額
                decimal Kingaku = Math.Floor(dr.Suuryou * dr.Tanka);
                LitChumonKingaku.Text = Kingaku.ToString("#,##0");
                */ 
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
                this.ShowTblList(true);

            }
            else
            {
                ShowMsg(AppCommon.NO_DATA, true);
                this.ShowTblList(false);
                return;

            }
        }

        // メインテーブル非表示
        private void ShowTblList(bool b)
        {
            TblAll.Visible = b;
        }

        private void ShowMsg(string strMsg, bool bError)
        {

            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? Color.Red : Color.Black;

        }

    }
}
