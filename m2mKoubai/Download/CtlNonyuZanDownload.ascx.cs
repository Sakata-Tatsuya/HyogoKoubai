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

namespace m2mKoubai.Download
{
    public partial class CtlNonyuZanDownload : System.Web.UI.UserControl
    {
        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            bool bTab = ("TAB" == this.DdlDataType.SelectedValue);
            string extension = bTab ? "txt" : "csv";

            // ■元データ取得
            ChumonClass.KensakuParam k = new ChumonClass.KensakuParam();
            k._userKubun = (byte)SessionManager.UserKubun;
            k._NHJyoukyou = 1;  // 未完納
            k._Cancelbi = 0;

            ChumonDataSet.V_Chumon_JyouhouDataTable dt =
                ChumonClass.getV_Chumon_JyouhouDataTable(k, Global.GetConnection());

            // --元データ加工
            DownloadDataSet.V_NounyuZanDataTable dtDown = new DownloadDataSet.V_NounyuZanDataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DownloadDataSet.V_NounyuZanRow drDown = dtDown.NewV_NounyuZanRow();
                drDown.HacchuuNo = dt[i].HacchuuNo;
                drDown.ShiiresakiMei = dt[i].ShiiresakiMei;
                drDown.BuhinCode = dt[i].BuhinCode;
                drDown.BuhinMei = dt[i].BuhinMei;
                drDown.Nouki = dt[i].Nouki;
                drDown.BashoMei = dt[i].BashoMei;
                int nouhinSuuryou = dt[i].IsNouhinSuuryouNull() ? 0 : dt[i].NouhinSuuryou;
                drDown.NounyuZan = dt[i].Suuryou - nouhinSuuryou;
                dtDown.AddV_NounyuZanRow(drDown);
            }

            // ■ダウンロードデータ作成
            string data = DownloadClass.GetTextData(DownloadClass.EnumDataKubun.NounyuZan, dtDown, bTab, Global.GetConnection());

            // ■ダウンロード
            Response.Clear();
            string strFileName = string.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMdd HHmm"), extension);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            Response.ContentType = "application/octet-stream";
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("Shift-JIS");
            Response.BinaryWrite(encoding.GetBytes(data));
            Response.End();
        }
    }
}