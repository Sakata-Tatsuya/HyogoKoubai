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
using System.Text.RegularExpressions;
using m2mKoubaiDAL;

namespace m2mKoubai.Upload
{
    public partial class CtlOrderUpload : System.Web.UI.UserControl
    {
        private const byte ShiiresakiCode = 0;
        private const byte BuhinCode = 1;
        private const byte Tanka = 2;
        private const byte Suuryou = 3;
        private const byte Nouki = 4;
        private const byte NounyuuBashoCode = 5;
        private const byte Bikou = 6;

        public void Create()
        {
            ListSet.SetDdlTantoushaValueIsId((byte)UserKubun.Yodoko, DdlHachuTantousha);

            if (this.DdlHachuTantousha.Items.FindByValue(SessionManager.LoginID) != null)
            {
                this.DdlHachuTantousha.SelectedValue = SessionManager.LoginID;
            }

            this.RdpHachuBi.SelectedDate = DateTime.Now;

            this.BtnUpload.OnClientClick = "return confirm('アップロードしますか？');";
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (this.DdlHachuTantousha.SelectedIndex == 0)
            {
                this.ShowMsg("発注担当者を選択して下さい。", true);
                return;
            }

            if (this.RdpHachuBi.SelectedDate == null || !this.RdpHachuBi.SelectedDate.HasValue)
            {
                this.ShowMsg("発注日を選択して下さい。", true);
                return;
            }

            if (!this.FileUpload1.HasFile)
            {
                this.ShowMsg("取込みファイルを選択して下さい。", true);
                return;
            }

            System.IO.StreamReader sr = new System.IO.StreamReader
                (this.FileUpload1.PostedFile.InputStream, System.Text.Encoding.GetEncoding(932));

            m2mKoubaiDataSet.T_ChumonDataTable dt = new m2mKoubaiDataSet.T_ChumonDataTable();

            int rowNo = 1;

            m2mKoubaiDataSet.M_ShiiresakiDataTable dtShiiresaki = ShiiresakiClass.getM_ShiiresakiDataTable(Global.GetConnection());
            DataView dvShiiresaki = dtShiiresaki.DefaultView;

            m2mKoubaiDataSet.M_BuhinDataTable dtBuhin = BuhinClass.getM_BuhinDataTable(Global.GetConnection());
            DataView dvBuhin = dtBuhin.DefaultView;

            m2mKoubaiDataSet.M_NounyuuBashoDataTable dtNounyuuBasho = NounyuuBashoClass.getM_NounyuuBashoDataTable(Global.GetConnection());
            DataView dvNounyuuBasho = dtNounyuuBasho.DefaultView;

            try
            {
                char spliter = ("TAB" == this.DdlDataType.SelectedValue) ? '\t' : ',';

                while (sr.Peek() != -1)
                {
                    string[] strAry = sr.ReadLine().Split(spliter);

                    if (strAry.Length != 7)
                    {
                        throw new Exception("列数が不正です。");
                    }

                    if (rowNo.Equals(1) && this.ChkHeader.Checked) { rowNo++; continue; }

                    this.HissuCheck(strAry);

                    this.FormatCheck(strAry);

                    this.DbExistCheck(dvShiiresaki, dvBuhin, dvNounyuuBasho, strAry);

                    m2mKoubaiDataSet.T_ChumonRow dr = dt.NewT_ChumonRow();

                    dr.Year = DateTime.Now.ToString("yy");
                    dr.HacchuuNo = rowNo.ToString();   // ダミー　本当の値はDB登録直前に取得する
                    dr.JigyoushoKubun = SessionManager.JigyoushoKubun;
                    dr.ShiiresakiCode = strAry[ShiiresakiCode];
                    dr.BuhinKubun = ""; // ダミー　本当の値はDB登録直前に取得する
                    dr.BuhinCode = strAry[BuhinCode];
                    dr.Tanka = decimal.Parse(strAry[Tanka]);
                    dr.Suuryou = int.Parse(strAry[Suuryou]);
                    dr.Kingaku = 0; // ダミー　本当の値はDB登録直前に取得する
                    dr.Nouki = strAry[Nouki];
                    dr.NounyuuBashoCode = strAry[NounyuuBashoCode];
                    dr.Bikou = strAry[Bikou];
                    dr.HacchuuBi = this.RdpHachuBi.SelectedDate.Value;
                    dr.HacchushaID = this.DdlHachuTantousha.SelectedValue;
                    dr.KannouFlg = false;
                    dr.KaritankaFlg = false;
                    dr.Zeiritu = Convert.ToInt32(this.DdlTax.SelectedValue);
                    dt.AddT_ChumonRow(dr);

                    rowNo++;
                }

                UploadClass.SaveT_Chumon(dt, System.IO.Path.GetFileName(this.FileUpload1.PostedFile.FileName), Global.GetConnection());

                this.ShowMsg("アップロードが完了しました。", false);
            }
            catch(Exception ex)
            {
                this.ShowMsg(ex.Message + string.Format(" エラー発生行：{0}", rowNo), true);
            }
        }

        private void HissuCheck(string[] strAry)
        {
            // ■必須項目チェック
            if (strAry[ShiiresakiCode].Equals(""))
            {
                throw new Exception("仕入先コードがブランクのため、アップロードできません。");
            }

            if (strAry[BuhinCode].Equals(""))
            {
                throw new Exception("品番がブランクのため、アップロードできません。");
            }

            if (strAry[Tanka].Equals(""))
            {
                throw new Exception("単価がブランクのため、アップロードできません。");
            }

            if (strAry[Suuryou].Equals(""))
            {
                throw new Exception("数量がブランクのため、アップロードできません。");
            }

            if (strAry[Nouki].Equals(""))
            {
                throw new Exception("納期がブランクのため、アップロードできません。");
            }

            if (strAry[NounyuuBashoCode].Equals(""))
            {
                throw new Exception("納入場所コードがブランクのため、アップロードできません。");
            }
        }

        private void FormatCheck(string[] strAry)
        {
            // ■長さチェック
            m2mKoubaiDataSet.T_ChumonDataTable dt = new m2mKoubaiDataSet.T_ChumonDataTable();

            if (dt.ShiiresakiCodeColumn.MaxLength < strAry[ShiiresakiCode].Length)
            {
                throw new Exception(string.Format("仕入先コードが{0}文字以上なので、アップロードできません。", dt.ShiiresakiCodeColumn.MaxLength));
            }

            if (dt.BuhinCodeColumn.MaxLength < strAry[BuhinCode].Length)
            {
                throw new Exception(string.Format("品番が{0}文字以上なので、アップロードできません。", dt.BuhinCodeColumn.MaxLength));
            }

            string pattern = "^(0|[-+]?[1-9][0-9]{0,7}(\\.[0-9]{0,2}$|$))";
            if (!Regex.IsMatch(strAry[Tanka], pattern))
            {
                throw new Exception("単価が整数部8桁以内、小数部2桁以内でないため、アップロードできません。");
            }

            if (int.MaxValue.ToString().Length < strAry[Suuryou].Length)
            {
                throw new Exception(string.Format("数量が{0}文字以上なので、アップロードできません。", int.MaxValue.ToString().Length));
            }

            if (dt.NounyuuBashoCodeColumn.MaxLength < strAry[NounyuuBashoCode].Length)
            {
                throw new Exception(string.Format("納品場所コードが{0}文字以上なので、アップロードできません。", dt.NounyuuBashoCodeColumn.MaxLength));
            }

            if (dt.BikouColumn.MaxLength < strAry[Bikou].Length)
            {
                throw new Exception(string.Format("備考が{0}文字以上なので、アップロードできません。", dt.BikouColumn.MaxLength));
            }

            // ■書式チェック
            int tmp_int;
            decimal tmp_decimal;
            DateTime tmp_date;

            if (!decimal.TryParse(strAry[Tanka], out tmp_decimal))
            {
                throw new Exception("単価が数値書式でないため、アップロードできません。");
            }

            if (!int.TryParse(strAry[Suuryou], out tmp_int))
            {
                throw new Exception("数量が数値書式でないため、アップロードできません。");
            }

            if (!DateTime.TryParseExact(strAry[Nouki], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out tmp_date))
            {
                throw new Exception("納期がYYYYMMDD書式(YYYY:年 MM:月 DD:日)でないため、アップロードできません。");
            }
        }

        private void DbExistCheck(DataView dvShiiresaki, DataView dvBuhin, DataView dvNounyuuBasho, string[] strAry)
        {
            dvShiiresaki.RowFilter = string.Format("ShiiresakiCode = '{0}'", strAry[ShiiresakiCode]);
            if (dvShiiresaki.Count == 0)
            {
                throw new Exception("仕入先コードがデータベースに存在しません。");
            }

            dvBuhin.RowFilter = string.Format("BuhinCode = '{0}'", strAry[BuhinCode]);
            if (dvBuhin.Count == 0)
            {
                throw new Exception("品番がデータベースに存在しません。");
            }

            dvNounyuuBasho.RowFilter = string.Format("BashoCode = '{0}'", strAry[NounyuuBashoCode]);
            if (dvNounyuuBasho.Count == 0)
            {
                throw new Exception("納入場所コードがデータベースに存在しません。");
            }
        }


        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Blue;
        }

        protected void LnkCsvSample_Click(object sender, EventArgs e)
        {
            this.FileDownload("発注データサンプル.csv");
        }

        protected void LnkTabSample_Click(object sender, EventArgs e)
        {
            this.FileDownload("発注データサンプル.txt");
        }

        private void FileDownload(string strFileName)
        {
            Response.Clear();
            this.Response.ContentType = "application/octet-stream";
            this.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            this.Response.Flush();
            this.Response.WriteFile(strFileName);
            this.Response.End();
        }
    }
}