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
using KoubaiDAL;
namespace Koubai.Shiiresaki
{
    public partial class CtlNoukiKaitou : System.Web.UI.UserControl
    {
        public Button TourokuButton
        {
            get
            {
                return this.BtnTouroku;
            }
        }

        public HtmlInputHidden HidNouki
        {
            get
            {
                return this.HN;
            }
        }
        public HtmlInputHidden HidSuuryou
        {
            get
            {
                return this.HS;
            }
        }
        public HtmlInputHidden HidKaitouNo
        {
            get
            {
                return this.KTNO;
            }
        }
        private const int G_CELL_NOUKI = 0;
        private const int G_CELL_SUURYOU = 1;
        private const int G_CELL_SAKUJO = 2;

        private string strYear;
        private string strHacchuuNo;
        private int nKubun;

        //private string strKaitouNo;
        private TextBox m_tbxSum = null;



        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.PreRender += new System.EventHandler(this.CtlKaitouNouki_PreRender);
        }
        private void CtlKaitouNouki_PreRender(object sender, System.EventArgs e)
        {
            string[] strNouki = new string[G.Rows.Count];
            string[] strSuuryou = new string[G.Rows.Count];
            string[] strKaitouNo = new string[G.Rows.Count];

            for (int i = 0; i < G.Rows.Count; i++)
            {
                TextBox tbxNouki = G.Rows[i].FindControl("TbxN") as TextBox;
                TextBox tbxSuuryou = G.Rows[i].FindControl("TbxS") as TextBox;
                TextBox tbxKaitouNo = G.Rows[i].FindControl("TbxKTNO") as TextBox;
                // 納期回答欄の納期、数量は数字のみ入力可能
                AppCommon.NumOnly(tbxNouki);
                AppCommon.NumOnly(tbxSuuryou);

                strNouki[i] = tbxNouki.ClientID;
                strSuuryou[i] = tbxSuuryou.ClientID;
                strKaitouNo[i] = tbxKaitouNo.ClientID;

                tbxKaitouNo.Style["display"] = "none";
            }
            this.HN.Value = string.Join(",", strNouki);
            this.HS.Value = string.Join(",", strSuuryou);
            this.KTNO.Value = string.Join(",", strKaitouNo);

            string strGetNk = string.Format("document.getElementById('{0}').value.split(\",\")", this.HN.ClientID);
            string strGetSuu = string.Format("document.getElementById('{0}').value.split(\",\")", this.HS.ClientID);
            string strGetKn = string.Format("document.getElementById('{0}').value.split(\",\")", this.KTNO.ClientID);
            //　登録
            this.BtnTouroku.Attributes["onclick"] =
                string.Format("NKM_Touroku(this, '{0}_{1}_{2}',{3},{4},{5});return false",
                strYear, strHacchuuNo, nKubun, strGetNk, strGetSuu, strGetKn);
            //  閉じる
            this.BtnClose.Attributes["onclick"] =
                string.Format("NKM_Close('{0}_{1}_{2}'); return false;", strYear, strHacchuuNo, nKubun);

            for (int i = 0; i < G.Rows.Count; i++)
            {
                // リンクボタン（閉じる）
                HyperLink lnkDelete = G.Rows[i].FindControl("L") as HyperLink;
                lnkDelete.ToolTip = "削除";

                lnkDelete.NavigateUrl = "javascript:void(0);";
                lnkDelete.Attributes["onclick"] = string.Format("NKM_DeleteRow('{0}_{1}_{2}',{3},{4},{5},{6});",
                    this.strYear, this.strHacchuuNo, this.nKubun, i, strGetNk, strGetSuu, strGetKn);

                // 合計数量
                TextBox tbxSuuryou = G.Rows[i].FindControl("TbxS") as TextBox;
                tbxSuuryou.Attributes["onblur"] =
                    string.Format("NKM_Sum({0},'{1}');", strGetSuu, this.m_tbxSum.ClientID);
            }
            // 行の追加
            G.Columns[2].Visible = (1 < G.Rows.Count);
            this.BtnRowAdd.Attributes["onclick"] =
                string.Format("NKM_AddRow('{0}_{1}_{2}',{3},{4},{5});return false",
                 strYear, strHacchuuNo, nKubun, strGetNk, strGetSuu, strGetKn);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public void Create(NoukiKaitouDataSet.KaitouNoukiDataTable dt, bool isNoData)
        {
            this.strYear = dt[0].Year;
            this.strHacchuuNo = dt[0].HacchuuNo;
            this.nKubun = dt[0].JigyoushoKubun;


            if (isNoData == false)
            {
                // 納期、数量の入力欄に値が引き込まれてくる時、空行を追加しておく
                dt = this.insertNewBrankRow(dt);
            }
            this.G.DataSource = dt;
            this.G.DataBind();
            for (int i = 0; i < G.Rows.Count; i++)
            {
                TextBox tbxNouki = G.Rows[i].FindControl("TbxN") as TextBox;
                TextBox tbxSuuryou = G.Rows[i].FindControl("TbxS") as TextBox;
                TextBox tbxKaitouNo = G.Rows[i].FindControl("TbxKTNO") as TextBox;

                tbxNouki.Text = dt[i].Nouki.Substring(2,6);
                tbxSuuryou.Text = dt[i].Suuryou.ToString();
                tbxKaitouNo.Text = dt[i].KaitouNo;
                tbxKaitouNo.Style["display"] = "none";
            }
            SetSum();
        }

        public void Create(string Year, string HacchuuNo, int Kubun, string[] strNoukiArray, string[] strSuuryouArray, string[] strKaitouNoArray)
        {
            this.G.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(this.G_RowDataBound);
            this.strYear = Year;
            this.strHacchuuNo = HacchuuNo;
            this.nKubun = Kubun;

            this.G.DataSource = strNoukiArray;
            this.G.DataBind();
            for (int i = 0; i < G.Rows.Count; i++)
            {
                TextBox tbxNouki = G.Rows[i].FindControl("TbxN") as TextBox;
                TextBox tbxSuuryou = G.Rows[i].FindControl("TbxS") as TextBox;
                TextBox tbxKaitouNo = G.Rows[i].FindControl("TbxKTNO") as TextBox;

                tbxNouki.Text = strNoukiArray[i];
                tbxSuuryou.Text = strSuuryouArray[i];
                tbxKaitouNo.Text = strKaitouNoArray[i];
            }
            SetSum();
        }
        private void SetSum()
        {
            this.m_tbxSum.Text = "";

            decimal dSum = 0;
            for (int i = 0; i < G.Rows.Count; i++)
            {
                TextBox tbxSuuryou = G.Rows[i].FindControl("TbxS") as TextBox;
                tbxSuuryou.Text = tbxSuuryou.Text.Trim();
                if ("" == tbxSuuryou.Text)
                    continue;

                try
                {
                    dSum += decimal.Parse(tbxSuuryou.Text);
                }
                catch
                {

                }
            }
            this.m_tbxSum.Text = string.Format("{0:#,##0}", dSum);
            this.m_tbxSum.CssClass = "tr";
        }


        // 空行を追加
        private NoukiKaitouDataSet.KaitouNoukiDataTable
            insertNewBrankRow(NoukiKaitouDataSet.KaitouNoukiDataTable dt)
        {
            NoukiKaitouDataSet.KaitouNoukiDataTable dtNew = dt;

            NoukiKaitouDataSet.KaitouNoukiRow drNew = dtNew.NewKaitouNoukiRow();

            drNew.Year = "";
            drNew.HacchuuNo = "";
            drNew.JigyoushoKubun = 0;
            drNew.KaitouNo = "";
            drNew.RowNo = "";
            drNew.Nouki = "";

            drNew.Suuryou = "";
            drNew.Tourokubi = "";

            dtNew.AddKaitouNoukiRow(drNew);

            return dtNew;
        }

        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                this.m_tbxSum = e.Row.FindControl("TbxSum") as TextBox;
                e.Row.Cells[0].Text = "合計";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = "";
            }
        }
    }
}