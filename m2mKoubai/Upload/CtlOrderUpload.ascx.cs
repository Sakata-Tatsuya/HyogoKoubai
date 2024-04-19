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

            this.BtnUpload.OnClientClick = "return confirm('�A�b�v���[�h���܂����H');";
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (this.DdlHachuTantousha.SelectedIndex == 0)
            {
                this.ShowMsg("�����S���҂�I�����ĉ������B", true);
                return;
            }

            if (this.RdpHachuBi.SelectedDate == null || !this.RdpHachuBi.SelectedDate.HasValue)
            {
                this.ShowMsg("��������I�����ĉ������B", true);
                return;
            }

            if (!this.FileUpload1.HasFile)
            {
                this.ShowMsg("�捞�݃t�@�C����I�����ĉ������B", true);
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
                        throw new Exception("�񐔂��s���ł��B");
                    }

                    if (rowNo.Equals(1) && this.ChkHeader.Checked) { rowNo++; continue; }

                    this.HissuCheck(strAry);

                    this.FormatCheck(strAry);

                    this.DbExistCheck(dvShiiresaki, dvBuhin, dvNounyuuBasho, strAry);

                    m2mKoubaiDataSet.T_ChumonRow dr = dt.NewT_ChumonRow();

                    dr.Year = DateTime.Now.ToString("yy");
                    dr.HacchuuNo = rowNo.ToString();   // �_�~�[�@�{���̒l��DB�o�^���O�Ɏ擾����
                    dr.JigyoushoKubun = SessionManager.JigyoushoKubun;
                    dr.ShiiresakiCode = strAry[ShiiresakiCode];
                    dr.BuhinKubun = ""; // �_�~�[�@�{���̒l��DB�o�^���O�Ɏ擾����
                    dr.BuhinCode = strAry[BuhinCode];
                    dr.Tanka = decimal.Parse(strAry[Tanka]);
                    dr.Suuryou = int.Parse(strAry[Suuryou]);
                    dr.Kingaku = 0; // �_�~�[�@�{���̒l��DB�o�^���O�Ɏ擾����
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

                this.ShowMsg("�A�b�v���[�h���������܂����B", false);
            }
            catch(Exception ex)
            {
                this.ShowMsg(ex.Message + string.Format(" �G���[�����s�F{0}", rowNo), true);
            }
        }

        private void HissuCheck(string[] strAry)
        {
            // ���K�{���ڃ`�F�b�N
            if (strAry[ShiiresakiCode].Equals(""))
            {
                throw new Exception("�d����R�[�h���u�����N�̂��߁A�A�b�v���[�h�ł��܂���B");
            }

            if (strAry[BuhinCode].Equals(""))
            {
                throw new Exception("�i�Ԃ��u�����N�̂��߁A�A�b�v���[�h�ł��܂���B");
            }

            if (strAry[Tanka].Equals(""))
            {
                throw new Exception("�P�����u�����N�̂��߁A�A�b�v���[�h�ł��܂���B");
            }

            if (strAry[Suuryou].Equals(""))
            {
                throw new Exception("���ʂ��u�����N�̂��߁A�A�b�v���[�h�ł��܂���B");
            }

            if (strAry[Nouki].Equals(""))
            {
                throw new Exception("�[�����u�����N�̂��߁A�A�b�v���[�h�ł��܂���B");
            }

            if (strAry[NounyuuBashoCode].Equals(""))
            {
                throw new Exception("�[���ꏊ�R�[�h���u�����N�̂��߁A�A�b�v���[�h�ł��܂���B");
            }
        }

        private void FormatCheck(string[] strAry)
        {
            // �������`�F�b�N
            m2mKoubaiDataSet.T_ChumonDataTable dt = new m2mKoubaiDataSet.T_ChumonDataTable();

            if (dt.ShiiresakiCodeColumn.MaxLength < strAry[ShiiresakiCode].Length)
            {
                throw new Exception(string.Format("�d����R�[�h��{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", dt.ShiiresakiCodeColumn.MaxLength));
            }

            if (dt.BuhinCodeColumn.MaxLength < strAry[BuhinCode].Length)
            {
                throw new Exception(string.Format("�i�Ԃ�{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", dt.BuhinCodeColumn.MaxLength));
            }

            string pattern = "^(0|[-+]?[1-9][0-9]{0,7}(\\.[0-9]{0,2}$|$))";
            if (!Regex.IsMatch(strAry[Tanka], pattern))
            {
                throw new Exception("�P����������8���ȓ��A������2���ȓ��łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }

            if (int.MaxValue.ToString().Length < strAry[Suuryou].Length)
            {
                throw new Exception(string.Format("���ʂ�{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", int.MaxValue.ToString().Length));
            }

            if (dt.NounyuuBashoCodeColumn.MaxLength < strAry[NounyuuBashoCode].Length)
            {
                throw new Exception(string.Format("�[�i�ꏊ�R�[�h��{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", dt.NounyuuBashoCodeColumn.MaxLength));
            }

            if (dt.BikouColumn.MaxLength < strAry[Bikou].Length)
            {
                throw new Exception(string.Format("���l��{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", dt.BikouColumn.MaxLength));
            }

            // �������`�F�b�N
            int tmp_int;
            decimal tmp_decimal;
            DateTime tmp_date;

            if (!decimal.TryParse(strAry[Tanka], out tmp_decimal))
            {
                throw new Exception("�P�������l�����łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }

            if (!int.TryParse(strAry[Suuryou], out tmp_int))
            {
                throw new Exception("���ʂ����l�����łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }

            if (!DateTime.TryParseExact(strAry[Nouki], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out tmp_date))
            {
                throw new Exception("�[����YYYYMMDD����(YYYY:�N MM:�� DD:��)�łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }
        }

        private void DbExistCheck(DataView dvShiiresaki, DataView dvBuhin, DataView dvNounyuuBasho, string[] strAry)
        {
            dvShiiresaki.RowFilter = string.Format("ShiiresakiCode = '{0}'", strAry[ShiiresakiCode]);
            if (dvShiiresaki.Count == 0)
            {
                throw new Exception("�d����R�[�h���f�[�^�x�[�X�ɑ��݂��܂���B");
            }

            dvBuhin.RowFilter = string.Format("BuhinCode = '{0}'", strAry[BuhinCode]);
            if (dvBuhin.Count == 0)
            {
                throw new Exception("�i�Ԃ��f�[�^�x�[�X�ɑ��݂��܂���B");
            }

            dvNounyuuBasho.RowFilter = string.Format("BashoCode = '{0}'", strAry[NounyuuBashoCode]);
            if (dvNounyuuBasho.Count == 0)
            {
                throw new Exception("�[���ꏊ�R�[�h���f�[�^�x�[�X�ɑ��݂��܂���B");
            }
        }


        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Blue;
        }

        protected void LnkCsvSample_Click(object sender, EventArgs e)
        {
            this.FileDownload("�����f�[�^�T���v��.csv");
        }

        protected void LnkTabSample_Click(object sender, EventArgs e)
        {
            this.FileDownload("�����f�[�^�T���v��.txt");
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