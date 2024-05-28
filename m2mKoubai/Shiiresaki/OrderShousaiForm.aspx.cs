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
        /// ��L�[1
        /// </summary>
        private string VsYear
        {
            get { return Convert.ToString(this.ViewState["VsYear"]); }
            set { this.ViewState["VsYear"] = value; }
        }
        /// <summary>
        /// ��L�[2
        /// </summary>
        private string VsHacchuuNo
        {
            get { return Convert.ToString(this.ViewState["VsHacchuuNo"]); }
            set { this.ViewState["VsHacchuuNo"] = value; }
        }
        /// <summary>
        /// ��L�[3
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
                // �����L�[���
                ChumonClass.ChumonKey key =
                    new ChumonClass.ChumonKey(strAry[0]);

                // ��L�[1
                VsYear = key.Year;
                // ��L�[2
                VsHacchuuNo = key.HacchuuNo;
                // ��L�[3
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
                    // �L�����Z�����b�Z�[�W
                    LblMsg.Text = "�L�����Z�������F" + dr.CancelBi.ToString("yy/MM/dd HH:mm");
                   
                }
                // ����No
                LitHacchuNo.Text = dr.HacchuuNo;
                // ������
                LitHacchuubi.Text = dr.HacchuuBi.ToString("yy/MM/dd");
                // �d����R�[�h
                LitShiireCode.Text = dr.ShiiresakiCode;
                // �d���於
                LitShiireName.Text = dr.ShiiresakiMei;
                // �i�ڃO���[�v
                this.LitKubun.Text = dr.BuhinKubun;
                // �R�[�h
                LitBuhinCode.Text = dr.BuhinCode;
                // �i�ږ�
                LitBuhinName.Text = dr.BuhinMei;
                // ����
                LitSuuryou.Text = dr.Suuryou.ToString("#,##0");
                // �P��
                if (dr.KaritankaFlg)
                {

                    LblTanka.Text = "(��)" + "\\" + dr.Tanka.ToString("#,##0.#0");
                    LblTanka.ForeColor = Color.Red;
                    //dr.Tanka.ToString("#,##0.#0");
                }
                else
                {
                    LblTanka.Text = "\\" + dr.Tanka.ToString("#,##0.#0");
                    LblTanka.ForeColor = Color.Black;
                }
                // �������z
                // ���őΉ�
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
                // �P��
                LitTanka.Text = dr.Tanka.ToString("#,##0.#0");
                // �������z
                decimal Kingaku = Math.Floor(dr.Suuryou * dr.Tanka);
                LitChumonKingaku.Text = Kingaku.ToString("#,##0");
                */ 
                // �P��
                LitTani.Text = dr.Tani;
                // �[���ꏊ�R�[�h
                //LitNBashoCode.Text = dr.NounyuuBashoCode;
                // �[���ꏊ��
                LitNBashoMei.Text = dr.BashoMei;
                // �����S���҃R�[�h
                LitTantoushaID.Text = dr.TantoushaCode;
                // �����S���Җ�
                LitTantoushaMei.Text = dr.Name;
                // ���l
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

        // ���C���e�[�u����\��
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
