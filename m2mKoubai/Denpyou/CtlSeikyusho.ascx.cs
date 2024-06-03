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
using System.Globalization;
using m2mKoubaiDAL;
using System.Collections.Generic;
using System.Drawing;

namespace m2mKoubai.Denpyou
{
    public partial class CtlSeikyusho : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Create(m2mKoubaiDataSet.M_ShiiresakiRow drShiire, ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader, int nMonth)
        {
            // ������Ж�
            LitOwner.Text = drHeader.KaishaMei +  " " + drHeader.EigyouSho;
            // ���t
            LitDate.Text = DateTime.Today.ToString("yyyy�NMM��dd��");
            // �d�����Ж�
            LitShiiresakiMei.Text = drHeader.ShiiresakiMei;
            LitInvoiceRegNo.Text = drHeader.InvoiceRegNo;
            // �d����X�֔ԍ�
            LitYubinBangou.Text = Utility.FormatYuubin(drHeader.YubinBangou);
            // �d����Z��
            LitAddress.Text = drHeader.Address;
            // �d����TEL
            LitTel.Text = Utility.FormatBanggo(drHeader.Tel);
            // �d����FAX
            LitFax.Text = Utility.FormatBanggo(drHeader.Fax);
            // ����
            LitMonth1.Text = nMonth.ToString() + "����";
            // ���x��
            LitMonth2.Text = nMonth.ToString() + "���x��";
            // �������`
            LitKouzamei.Text = drShiire.KouzaMeigi;
            // ���Z�@�֖�
            LitKinyuuKikanmei.Text = drShiire.KinyuuKikanMei;
            // �����ԍ�
            LitKouzaBanggo.Text = drShiire.KouzaBangou;

        }
        public void SetGoukei(int nGoukei, int nShohizei)
        {
            //this.LitGoukei.Text = string.Format("\\{0:#,##0}", nGoukei);
            //this.LitSyouhizei.Text = string.Format("\\{0:#,##0}", nShohizei);

            // ���������z
            int nSouGoukei = nGoukei + nShohizei;
            LitKingaku.Text = string.Format("\\{0:#,##0}", nSouGoukei);
        }

        public void SetGoukei(List<KenshuClass.ZeirituShukei> lst)
        {
            int nGoukei = 0;
            int nShohizei = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                nGoukei += lst[i].iKingaku;
                nShohizei += lst[i].iZeigaku;
            }
            // ���������z
            int nSouGoukei = nGoukei + nShohizei; 
            LitKingaku.Text = string.Format("\\{0:#,##0}", nSouGoukei);
            GZ.DataSource = lst;
            GZ.DataBind();
            GZ.BorderColor = Color.White;
        }

        protected void GZ_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                KenshuClass.ZeirituShukei mi = e.Row.DataItem as KenshuClass.ZeirituShukei;
                Literal LitShubetu = e.Row.FindControl("LitShubetu") as Literal;
                Literal LitKingaku = e.Row.FindControl("LitKingaku") as Literal;
                Literal LitZeigaku = e.Row.FindControl("LitZeigaku") as Literal;
                LitKingaku.Text = mi.iKingaku.ToString("#,##0");
                LitZeigaku.Text = mi.iZeigaku.ToString("#,##0");
                LitShubetu.Text = mi.iZeiritu.ToString("#0") + "%�Ώ�";
                if (mi.bKeigenZeirituFlg)
                {
                    LitShubetu.Text += "(�y���ŗ�)";
                }


            }

        }



























        }
    }