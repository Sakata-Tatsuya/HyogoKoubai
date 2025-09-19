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

namespace Koubai.Download
{
    public partial class CtlKenshuDownload : System.Web.UI.UserControl
    {
        public void Create()
        {
            this.SetDdlYear();
        }

        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            bool bTab = ("TAB" == this.DdlDataType.SelectedValue);
            string extension = bTab ? "txt" : "csv";

            // ■検索条件
            int year = int.Parse(this.DdlYear.SelectedValue);
            int month = int.Parse(this.DdlMonth.SelectedValue);
            int day = DateTime.DaysInMonth(year, month);

            KenshuClass.KensakuParam k = new KenshuClass.KensakuParam();
            k._FromDate = string.Format("{0:0000}{1:00}{2:00}", year, month, 1);
            k._ToDate = string.Format("{0:0000}{1:00}{2:00}", year, month, day);

            // ■元データ取得
            KenshuDataSet.V_KenshuDataTable dt = KenshuClass.getV_KenshuDataTable(k, Global.GetConnection());

            // ■ダウンロードデータ作成
            string data = DownloadClass.GetTextData(DownloadClass.EnumDataKubun.Kenshu, dt, bTab, Global.GetConnection());

            // ■ダウンロード
            Response.Clear();
            string strFileName = string.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMdd HHmm"), extension);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            Response.ContentType = "application/octet-stream";
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("Shift-JIS");
            Response.BinaryWrite(encoding.GetBytes(data));
            Response.End();
        }

        private void SetDdlYear()
        {
            DataTable dt = KenshuClass.GetNouhinYearDataTable(Global.GetConnection());

            this.DdlYear.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DdlYear.Items.Add(new ListItem(dt.Rows[i]["Year"].ToString(), dt.Rows[i]["Year"].ToString()));
            }

            // 現在の年を選択
            string thisYear = DateTime.Now.Year.ToString();
            if (0 < this.DdlYear.Items.Count && this.DdlYear.Items.FindByValue(thisYear) != null)
            {
                this.DdlYear.SelectedValue = thisYear;
            }

            // 現在の月を選択
            string thisMonth = DateTime.Now.Month.ToString();
            this.DdlMonth.SelectedValue = thisMonth;
        }
    }
}