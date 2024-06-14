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
using Core.Type;
using m2mKoubaiDAL;

namespace m2mKoubai.Denpyou
{
    public partial class CtlHacchuusho : System.Web.UI.UserControl
    {

        private const int G_CELL_HACCHUUNO = 0;
        private const int G_CELL_CODE = 1;
        private const int G_CELL_SUURYOU = 2;
        private const int G_CELL_GOUKEI = 3;
        private const int G_CELL_TANI = 4;
        private const int G_CELL_NOUKI = 5;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(HacchuDataSet_M.V_HacchuBindRow[] drAry, int cnt)
        {
            G.DataSource = drAry;
            G.DataBind();
            G.EnableViewState = false;
            Create_Date();
            Create_Top(drAry);
            if (cnt > 1)
            {
                this.T.Visible = false;
                //AppCommon.ShowTable(false, this.T);
            }


        }

        // �����N�����ݒ�
        private void Create_Date()
        {
            LitDate.Text = DateTime.Today.ToString("yyyy�NMM��dd��");
        }

        public void Create_Top(HacchuDataSet_M.V_HacchuBindRow[] drAry)
        {
            // �d����   
            this.LitKaisha.Text = drAry[0].ShiiresakiMei;
            // �X�֔ԍ�
            this.LitYubin.Text = Utility.FormatYuubin(drAry[0].YubinBangou);
            // �d����Z��
            this.LitJyusyo.Text = drAry[0].Address;
            // �d����TEL
            this.LitShiireTEL.Text = Utility.FormatBanggo(drAry[0].Tel);
            // �d����FAX
            this.LitShiireFAX.Text = Utility.FormatBanggo(drAry[0].Fax);
            // ������Ж�
            LitKaishaMeiH.Text = drAry[0].KaishameiY + " " + drAry[0].Eigyousho;
            // ������Гd�b�ԍ�
            LitTelH.Text = Utility.FormatBanggo(drAry[0].TelY);
            // �������FAX
            LitFaxH.Text = Utility.FormatBanggo(drAry[0].FaxY);
            ////�����S����
            this.LitTantousha.Text = drAry[0].Name;
        }

        // �ۑ��p
        //string strHacchuuNo = "";
        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HacchuDataSet_M.V_HacchuBindRow dr =
                   e.Row.DataItem as HacchuDataSet_M.V_HacchuBindRow;

                // ����No����v���Ȃ��ꍇ
                if (!dr.IsHacchuuNoNull())
                {
                    (e.Row.FindControl("LitNo") as Literal).Text = dr.HacchuuNo;
                }
                // ���i�R�[�h
                if (!dr.IsBuhinCodeNull())
                {
                    (e.Row.FindControl("LitCode") as Literal).Text = dr.BuhinCode;
                }
                // ���i��
                if (!dr.IsBuhinMeiNull())
                {
                    (e.Row.FindControl("LitHinmei") as Literal).Text = dr.BuhinMei;
                }
                // ����
                if (!dr.IsSuuryouNull())
                {
                    (e.Row.FindControl("LitSuu") as Literal).Text = dr.Suuryou.ToString();
                }
                // �P��
                if (!dr.IsTaniNull())
                {
                    (e.Row.FindControl("LitTani") as Literal).Text = dr.Tani;
                }
                // �P��
                if (!dr.IsTankaNull())
                {
                    (e.Row.FindControl("LitTanka") as Literal).Text = String.Format("\\{0:#,##0.00}", dr.Tanka);
                }
                //���v
                if (!dr.IsKingakuNull())
                {
                    (e.Row.FindControl("LitKei") as Literal).Text = String.Format("\\{0:#,##0}", (int)dr.Kingaku);
                }
                // �[��
                if (!dr.IsNoukiNull())
                {
                    (e.Row.FindControl("LitNouki") as Literal).Text =
                        (new Nengappi(int.Parse(dr.Nouki))).ToString("yyyy/MM/dd");
                }
                //�[���ꏊ
                if (!dr.IsBashoMeiNull())
                {
                    (e.Row.FindControl("LitBasyo") as Literal).Text = dr.BashoMei;
                }
                //���l
                if (!dr.IsBikouNull())
                {
                    (e.Row.FindControl("LitBikou") as Literal).Text = dr.Bikou.Replace("\r\n", "�@");
                }
            }
        }
    }
}