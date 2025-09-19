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
using KoubaiDAL;

namespace Koubai.Upload
{
    public partial class CtlHinmokuUpload : System.Web.UI.UserControl
    {
        private const byte BuhinCode = 0;
        private const byte BuhinKubun = 1;
        private const byte BuhinMei = 2;
        private const byte Tani = 3;
        private const byte LT_Suuji = 4;
        private const byte LT_Tani = 5;
        private const byte Tanka = 6;
        private const byte Lot = 7;
        private const byte ShiiresakiCode1 = 8;
        private const byte ShiiresakiCode2 = 9;
        private const byte KanjyouKamokuCode = 10;
        private const byte HiyouKamokuCode = 11;
        private const byte HojyoKamokuNo = 12;

        public void Create()
        {
            this.RdpUploadDate.SelectedDate = DateTime.Now;

            this.BtnUpload.OnClientClick = "return confirm('アップロードしますか？');";
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (this.RdpUploadDate.SelectedDate == null || !this.RdpUploadDate.SelectedDate.HasValue)
            {
                this.ShowMsg("取込み日を選択して下さい。", true);
                return;
            }

            if (!this.FileUpload1.HasFile)
            {
                this.ShowMsg("取込みファイルを選択して下さい。", true);
                return;
            }

            System.IO.StreamReader sr = new System.IO.StreamReader
                (this.FileUpload1.PostedFile.InputStream, System.Text.Encoding.GetEncoding(932));

            // -- 仕入先存在確認用のデータテーブル
            KoubaiDataSet.M_ShiiresakiDataTable dtShiiresaki = ShiiresakiClass.getM_ShiiresakiDataTable(Global.GetConnection());
            DataView dvShiiresaki = dtShiiresaki.DefaultView;


            KoubaiDataSet.M_BuhinDataTable dt = new KoubaiDataSet.M_BuhinDataTable();

            int rowNo = 1;

            try
            {
                char spliter = ("TAB" == this.DdlDataType.SelectedValue) ? '\t' : ',';

                while (sr.Peek() != -1)
                {
                    string[] strAry = sr.ReadLine().Split(spliter);

                    if (strAry.Length != 13)
                    {
                        throw new Exception("列数が不正です。");
                    }

                    if (rowNo.Equals(1) && this.ChkHeader.Checked) { rowNo++; continue; }

                    this.HissuCheck(strAry);

                    this.ConvertBlank(strAry);

                    this.FormatCheck(strAry);

                    this.DbExistCheck(dvShiiresaki, strAry);

                    KoubaiDataSet.M_BuhinRow dr = dt.NewM_BuhinRow();
                    if (dt.FindByBuhinCode(strAry[BuhinCode]) != null)
                    {
                        throw new Exception("同一の部品コードを含むレコードが複数行存在します。");
                    }

                    dr.BuhinCode = strAry[BuhinCode];
                    dr.BuhinKubun = strAry[BuhinKubun];
                    dr.BuhinMei = strAry[BuhinMei];
                    dr.Tani = strAry[Tani];
                    dr.LT_Suuji = int.Parse(strAry[LT_Suuji]);
                    dr.LT_Tani = byte.Parse(strAry[LT_Tani]);
                    dr.Tanka = decimal.Parse(strAry[Tanka]);
                    dr.Lot = int.Parse(strAry[Lot]);
                    dr.ShiiresakiCode1 = strAry[ShiiresakiCode1];
                    dr.ShiiresakiCode2 = strAry[ShiiresakiCode2];
                    dr.KanjyouKamokuCode = int.Parse(strAry[KanjyouKamokuCode]);
                    dr.HiyouKamokuCode = int.Parse(strAry[HiyouKamokuCode]);
                    dr.HojyoKamokuNo = int.Parse(strAry[HojyoKamokuNo]);
                    dt.AddM_BuhinRow(dr);

                    rowNo++;
                }

                UploadClass.SaveM_Buhin(dt, this.RdpUploadDate.SelectedDate.Value, System.IO.Path.GetFileName(this.FileUpload1.PostedFile.FileName), Global.GetConnection());

                this.ShowMsg("アップロードが完了しました。", false);
            }
            catch (Exception ex)
            {
                this.ShowMsg(ex.Message + string.Format(" エラー発生行：{0}", rowNo), true);
            }
        }

        private void HissuCheck(string[] strAry)
        {
            // ■必須項目チェック
            if (strAry[BuhinCode].Equals(""))
            {
                throw new Exception("品番がブランクのため、アップロードできません。");
            }

            if (strAry[BuhinKubun].Equals(""))
            {
                throw new Exception("品目グループがブランクのため、アップロードできません。");
            }

            if (strAry[BuhinMei].Equals(""))
            {
                throw new Exception("品名がブランクのため、アップロードできません。");
            }

            if (strAry[Tani].Equals(""))
            {
                throw new Exception("単位がブランクのため、アップロードできません。");
            }

            if (strAry[ShiiresakiCode1].Equals(""))
            {
                throw new Exception("仕入先コード１がブランクのため、アップロードできません。");
            }
        }

        private void ConvertBlank(string[] strAry)
        {
            if (strAry[Tanka].Equals(""))
            {
                strAry[Tanka] = "0";
            }

            if (strAry[Lot].Equals(""))
            {
                strAry[Lot] = "0";
            }

            if (strAry[LT_Suuji].Equals(""))
            {
                strAry[LT_Suuji] = "0";
            }

            if (strAry[LT_Tani].Equals(""))
            {
                strAry[LT_Tani] = "0";
            }

            if (strAry[KanjyouKamokuCode].Equals(""))
            {
                strAry[KanjyouKamokuCode] = "0";
            }

            if (strAry[HiyouKamokuCode].Equals(""))
            {
                strAry[HiyouKamokuCode] = "0";
            }

            if (strAry[HojyoKamokuNo].Equals(""))
            {
                strAry[HojyoKamokuNo] = "0";
            }
        }

        private void FormatCheck(string[] strAry)
        {
            // ■長さチェック
            KoubaiDataSet.M_BuhinDataTable dt = new KoubaiDataSet.M_BuhinDataTable();

            if (dt.BuhinCodeColumn.MaxLength < strAry[BuhinCode].Length)
            {
                throw new Exception(string.Format("品番が{0}文字以上なので、アップロードできません。", dt.BuhinCodeColumn.MaxLength));
            }

            if (dt.BuhinKubunColumn.MaxLength < strAry[BuhinKubun].Length)
            {
                throw new Exception(string.Format("品目グループが{0}文字以上なので、アップロードできません。", dt.BuhinKubunColumn.MaxLength));
            }

            if (dt.BuhinMeiColumn.MaxLength < strAry[BuhinMei].Length)
            {
                throw new Exception(string.Format("品名が{0}文字以上なので、アップロードできません。", dt.BuhinMeiColumn.MaxLength));
            }

            if (dt.TaniColumn.MaxLength < strAry[Tani].Length)
            {
                throw new Exception(string.Format("単位が{0}文字以上なので、アップロードできません。", dt.TaniColumn.MaxLength));
            }

            if (int.MaxValue.ToString().Length < strAry[LT_Suuji].Length)
            {
                throw new Exception(string.Format("リードタイム（数値）が{0}文字以上なので、アップロードできません。", int.MaxValue.ToString().Length));
            }

            if (byte.MaxValue.ToString().Length < strAry[LT_Tani].Length)
            {
                throw new Exception(string.Format("リードタイム（単位）が{0}文字以上なので、アップロードできません。", byte.MaxValue.ToString().Length));
            }

            string pattern = "^(0|[-+]?[1-9][0-9]{0,7}(\\.[0-9]{0,2}$|$))";
            if (!Regex.IsMatch(strAry[Tanka], pattern))
            {
                throw new Exception("単価が整数部8桁以内、小数部2桁以内でないため、アップロードできません。");
            }

            if (int.MaxValue.ToString().Length < strAry[Lot].Length)
            {
                throw new Exception(string.Format("ロットが{0}文字以上なので、アップロードできません。", int.MaxValue.ToString().Length));
            }

            if (dt.ShiiresakiCode1Column.MaxLength < strAry[ShiiresakiCode1].Length)
            {
                throw new Exception(string.Format("仕入先コード１が{0}文字以上なので、アップロードできません。", dt.ShiiresakiCode1Column.MaxLength));
            }

            if (dt.ShiiresakiCode2Column.MaxLength < strAry[ShiiresakiCode2].Length)
            {
                throw new Exception(string.Format("仕入先コード２が{0}文字以上なので、アップロードできません。", dt.ShiiresakiCode2Column.MaxLength));
            }

            if (int.MaxValue.ToString().Length < strAry[KanjyouKamokuCode].Length)
            {
                throw new Exception(string.Format("勘定科目コードが{0}文字以上なので、アップロードできません。", int.MaxValue.ToString().Length));
            }

            if (int.MaxValue.ToString().Length < strAry[KanjyouKamokuCode].Length)
            {
                throw new Exception(string.Format("費用科目コードが{0}文字以上なので、アップロードできません。", int.MaxValue.ToString().Length));
            }

            if (int.MaxValue.ToString().Length < strAry[KanjyouKamokuCode].Length)
            {
                throw new Exception(string.Format("補助科目番号が{0}文字以上なので、アップロードできません。", int.MaxValue.ToString().Length));
            }

            int tmp_int;
            decimal tmp_decimal;

            // ■数値書式チェック
            if (!int.TryParse(strAry[LT_Suuji], out tmp_int))
            {
                throw new Exception("リードタイム（数値）が数値書式でないため、アップロードできません。");
            }

            if (!int.TryParse(strAry[LT_Tani], out tmp_int))
            {
                throw new Exception("リードタイム（単位）が数値書式でないため、アップロードできません。");
            }

            if (!decimal.TryParse(strAry[Tanka], out tmp_decimal))
            {
                throw new Exception("単価が数値書式でないため、アップロードできません。");
            }

            if (!int.TryParse(strAry[Lot], out tmp_int))
            {
                throw new Exception("ロットが数値書式でないため、アップロードできません。");
            }

            if (!int.TryParse(strAry[KanjyouKamokuCode], out tmp_int))
            {
                throw new Exception("勘定科目コードが数値書式でないため、アップロードできません。");
            }

            if (!int.TryParse(strAry[HiyouKamokuCode], out tmp_int))
            {
                throw new Exception("費用科目コードが数値書式でないため、アップロードできません。");
            }

            if (!int.TryParse(strAry[HojyoKamokuNo], out tmp_int))
            {
                throw new Exception("補助科目コードが数値書式でないため、アップロードできません。");
            }
        }

        private void DbExistCheck(DataView dvShiiresaki, string[] strAry)
        {
            dvShiiresaki.RowFilter = string.Format("ShiiresakiCode = '{0}'", strAry[ShiiresakiCode1]);
            if (dvShiiresaki.Count == 0)
            {
                throw new Exception("仕入先コード１がマスタに存在しません。");
            }

            if (!strAry[ShiiresakiCode2].Equals(""))
            {
                dvShiiresaki.RowFilter = string.Format("ShiiresakiCode = '{0}'", strAry[ShiiresakiCode2]);
                if (dvShiiresaki.Count == 0)
                {
                    throw new Exception("仕入先コード２がマスタに存在しません。");
                }
            }

            if (!(strAry[LT_Tani].Equals("0") || strAry[LT_Tani].Equals("1") || strAry[LT_Tani].Equals("2") || strAry[LT_Tani].Equals("3") || strAry[LT_Tani].Equals("4")))
            {
                throw new Exception("リードタイム（単位）はブランクまたは0～4を入力して下さい。<br>（「0：未指定」「1：day」「2：week」「3：month」「4：year」）");
            }

            if (!(strAry[KanjyouKamokuCode].Equals("0") || strAry[KanjyouKamokuCode].Equals("174") || strAry[KanjyouKamokuCode].Equals("176")))
            {
                throw new Exception("勘定科目コードはブランクまたは0、174、176を入力して下さい。<br>（「0：未指定」「174：原材料」「176：貯蔵品」）");
            }

            if (!(strAry[HiyouKamokuCode].Equals("0") || strAry[HiyouKamokuCode].Equals("722") || strAry[HiyouKamokuCode].Equals("723") || strAry[HiyouKamokuCode].Equals("740") || strAry[HiyouKamokuCode].Equals("752")))
            {
                throw new Exception("費用科目コードはブランクまたは0、722、723、740、752を入力して下さい。<br>（「0：未指定」「722：補助材料費」「723：原料費」「740：原燃料費」「752：消耗品費」）");
            }

            if (!(strAry[HojyoKamokuNo].Equals("0") || strAry[HojyoKamokuNo].Equals("1") || strAry[HojyoKamokuNo].Equals("2") || strAry[HojyoKamokuNo].Equals("3") || strAry[HojyoKamokuNo].Equals("4") || strAry[HojyoKamokuNo].Equals("5") || strAry[HojyoKamokuNo].Equals("6")))
            {
                throw new Exception("補助科目コードはブランクまたは0～6を入力して下さい。<br>（0：未指定」「1：主原料費」「2：副原料費」「3：原燃料費」「4：梱包材料費」「5：SP付属品」「6：直接消耗品費」）");
            }
        }

        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Blue;
        }

        protected void LnkCsvSample_Click(object sender, EventArgs e)
        {
            this.FileDownload("品目データサンプル.csv");
        }

        protected void LnkTabSample_Click(object sender, EventArgs e)
        {
            this.FileDownload("品目データサンプル.txt");
        }

        private void FileDownload(string strFileName)
        {
            this.Response.Clear();
            this.Response.ContentType = "application/octet-stream";
            this.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
            this.Response.Flush();
            this.Response.WriteFile(strFileName);
            this.Response.End();
        }
    }
}