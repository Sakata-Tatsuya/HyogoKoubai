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

            this.BtnUpload.OnClientClick = "return confirm('�A�b�v���[�h���܂����H');";
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (this.RdpUploadDate.SelectedDate == null || !this.RdpUploadDate.SelectedDate.HasValue)
            {
                this.ShowMsg("�捞�ݓ���I�����ĉ������B", true);
                return;
            }

            if (!this.FileUpload1.HasFile)
            {
                this.ShowMsg("�捞�݃t�@�C����I�����ĉ������B", true);
                return;
            }

            System.IO.StreamReader sr = new System.IO.StreamReader
                (this.FileUpload1.PostedFile.InputStream, System.Text.Encoding.GetEncoding(932));

            // -- �d���摶�݊m�F�p�̃f�[�^�e�[�u��
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
                        throw new Exception("�񐔂��s���ł��B");
                    }

                    if (rowNo.Equals(1) && this.ChkHeader.Checked) { rowNo++; continue; }

                    this.HissuCheck(strAry);

                    this.ConvertBlank(strAry);

                    this.FormatCheck(strAry);

                    this.DbExistCheck(dvShiiresaki, strAry);

                    KoubaiDataSet.M_BuhinRow dr = dt.NewM_BuhinRow();
                    if (dt.FindByBuhinCode(strAry[BuhinCode]) != null)
                    {
                        throw new Exception("����̕��i�R�[�h���܂ރ��R�[�h�������s���݂��܂��B");
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

                this.ShowMsg("�A�b�v���[�h���������܂����B", false);
            }
            catch (Exception ex)
            {
                this.ShowMsg(ex.Message + string.Format(" �G���[�����s�F{0}", rowNo), true);
            }
        }

        private void HissuCheck(string[] strAry)
        {
            // ���K�{���ڃ`�F�b�N
            if (strAry[BuhinCode].Equals(""))
            {
                throw new Exception("�i�Ԃ��u�����N�̂��߁A�A�b�v���[�h�ł��܂���B");
            }

            if (strAry[BuhinKubun].Equals(""))
            {
                throw new Exception("�i�ڃO���[�v���u�����N�̂��߁A�A�b�v���[�h�ł��܂���B");
            }

            if (strAry[BuhinMei].Equals(""))
            {
                throw new Exception("�i�����u�����N�̂��߁A�A�b�v���[�h�ł��܂���B");
            }

            if (strAry[Tani].Equals(""))
            {
                throw new Exception("�P�ʂ��u�����N�̂��߁A�A�b�v���[�h�ł��܂���B");
            }

            if (strAry[ShiiresakiCode1].Equals(""))
            {
                throw new Exception("�d����R�[�h�P���u�����N�̂��߁A�A�b�v���[�h�ł��܂���B");
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
            // �������`�F�b�N
            KoubaiDataSet.M_BuhinDataTable dt = new KoubaiDataSet.M_BuhinDataTable();

            if (dt.BuhinCodeColumn.MaxLength < strAry[BuhinCode].Length)
            {
                throw new Exception(string.Format("�i�Ԃ�{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", dt.BuhinCodeColumn.MaxLength));
            }

            if (dt.BuhinKubunColumn.MaxLength < strAry[BuhinKubun].Length)
            {
                throw new Exception(string.Format("�i�ڃO���[�v��{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", dt.BuhinKubunColumn.MaxLength));
            }

            if (dt.BuhinMeiColumn.MaxLength < strAry[BuhinMei].Length)
            {
                throw new Exception(string.Format("�i����{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", dt.BuhinMeiColumn.MaxLength));
            }

            if (dt.TaniColumn.MaxLength < strAry[Tani].Length)
            {
                throw new Exception(string.Format("�P�ʂ�{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", dt.TaniColumn.MaxLength));
            }

            if (int.MaxValue.ToString().Length < strAry[LT_Suuji].Length)
            {
                throw new Exception(string.Format("���[�h�^�C���i���l�j��{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", int.MaxValue.ToString().Length));
            }

            if (byte.MaxValue.ToString().Length < strAry[LT_Tani].Length)
            {
                throw new Exception(string.Format("���[�h�^�C���i�P�ʁj��{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", byte.MaxValue.ToString().Length));
            }

            string pattern = "^(0|[-+]?[1-9][0-9]{0,7}(\\.[0-9]{0,2}$|$))";
            if (!Regex.IsMatch(strAry[Tanka], pattern))
            {
                throw new Exception("�P����������8���ȓ��A������2���ȓ��łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }

            if (int.MaxValue.ToString().Length < strAry[Lot].Length)
            {
                throw new Exception(string.Format("���b�g��{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", int.MaxValue.ToString().Length));
            }

            if (dt.ShiiresakiCode1Column.MaxLength < strAry[ShiiresakiCode1].Length)
            {
                throw new Exception(string.Format("�d����R�[�h�P��{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", dt.ShiiresakiCode1Column.MaxLength));
            }

            if (dt.ShiiresakiCode2Column.MaxLength < strAry[ShiiresakiCode2].Length)
            {
                throw new Exception(string.Format("�d����R�[�h�Q��{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", dt.ShiiresakiCode2Column.MaxLength));
            }

            if (int.MaxValue.ToString().Length < strAry[KanjyouKamokuCode].Length)
            {
                throw new Exception(string.Format("����ȖڃR�[�h��{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", int.MaxValue.ToString().Length));
            }

            if (int.MaxValue.ToString().Length < strAry[KanjyouKamokuCode].Length)
            {
                throw new Exception(string.Format("��p�ȖڃR�[�h��{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", int.MaxValue.ToString().Length));
            }

            if (int.MaxValue.ToString().Length < strAry[KanjyouKamokuCode].Length)
            {
                throw new Exception(string.Format("�⏕�Ȗڔԍ���{0}�����ȏ�Ȃ̂ŁA�A�b�v���[�h�ł��܂���B", int.MaxValue.ToString().Length));
            }

            int tmp_int;
            decimal tmp_decimal;

            // �����l�����`�F�b�N
            if (!int.TryParse(strAry[LT_Suuji], out tmp_int))
            {
                throw new Exception("���[�h�^�C���i���l�j�����l�����łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }

            if (!int.TryParse(strAry[LT_Tani], out tmp_int))
            {
                throw new Exception("���[�h�^�C���i�P�ʁj�����l�����łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }

            if (!decimal.TryParse(strAry[Tanka], out tmp_decimal))
            {
                throw new Exception("�P�������l�����łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }

            if (!int.TryParse(strAry[Lot], out tmp_int))
            {
                throw new Exception("���b�g�����l�����łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }

            if (!int.TryParse(strAry[KanjyouKamokuCode], out tmp_int))
            {
                throw new Exception("����ȖڃR�[�h�����l�����łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }

            if (!int.TryParse(strAry[HiyouKamokuCode], out tmp_int))
            {
                throw new Exception("��p�ȖڃR�[�h�����l�����łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }

            if (!int.TryParse(strAry[HojyoKamokuNo], out tmp_int))
            {
                throw new Exception("�⏕�ȖڃR�[�h�����l�����łȂ����߁A�A�b�v���[�h�ł��܂���B");
            }
        }

        private void DbExistCheck(DataView dvShiiresaki, string[] strAry)
        {
            dvShiiresaki.RowFilter = string.Format("ShiiresakiCode = '{0}'", strAry[ShiiresakiCode1]);
            if (dvShiiresaki.Count == 0)
            {
                throw new Exception("�d����R�[�h�P���}�X�^�ɑ��݂��܂���B");
            }

            if (!strAry[ShiiresakiCode2].Equals(""))
            {
                dvShiiresaki.RowFilter = string.Format("ShiiresakiCode = '{0}'", strAry[ShiiresakiCode2]);
                if (dvShiiresaki.Count == 0)
                {
                    throw new Exception("�d����R�[�h�Q���}�X�^�ɑ��݂��܂���B");
                }
            }

            if (!(strAry[LT_Tani].Equals("0") || strAry[LT_Tani].Equals("1") || strAry[LT_Tani].Equals("2") || strAry[LT_Tani].Equals("3") || strAry[LT_Tani].Equals("4")))
            {
                throw new Exception("���[�h�^�C���i�P�ʁj�̓u�����N�܂���0�`4����͂��ĉ������B<br>�i�u0�F���w��v�u1�Fday�v�u2�Fweek�v�u3�Fmonth�v�u4�Fyear�v�j");
            }

            if (!(strAry[KanjyouKamokuCode].Equals("0") || strAry[KanjyouKamokuCode].Equals("174") || strAry[KanjyouKamokuCode].Equals("176")))
            {
                throw new Exception("����ȖڃR�[�h�̓u�����N�܂���0�A174�A176����͂��ĉ������B<br>�i�u0�F���w��v�u174�F���ޗ��v�u176�F�����i�v�j");
            }

            if (!(strAry[HiyouKamokuCode].Equals("0") || strAry[HiyouKamokuCode].Equals("722") || strAry[HiyouKamokuCode].Equals("723") || strAry[HiyouKamokuCode].Equals("740") || strAry[HiyouKamokuCode].Equals("752")))
            {
                throw new Exception("��p�ȖڃR�[�h�̓u�����N�܂���0�A722�A723�A740�A752����͂��ĉ������B<br>�i�u0�F���w��v�u722�F�⏕�ޗ���v�u723�F������v�u740�F���R����v�u752�F���Օi��v�j");
            }

            if (!(strAry[HojyoKamokuNo].Equals("0") || strAry[HojyoKamokuNo].Equals("1") || strAry[HojyoKamokuNo].Equals("2") || strAry[HojyoKamokuNo].Equals("3") || strAry[HojyoKamokuNo].Equals("4") || strAry[HojyoKamokuNo].Equals("5") || strAry[HojyoKamokuNo].Equals("6")))
            {
                throw new Exception("�⏕�ȖڃR�[�h�̓u�����N�܂���0�`6����͂��ĉ������B<br>�i0�F���w��v�u1�F�匴����v�u2�F��������v�u3�F���R����v�u4�F����ޗ���v�u5�FSP�t���i�v�u6�F���ڏ��Օi��v�j");
            }
        }

        private void ShowMsg(string strMsg, bool bError)
        {
            LblMsg.Text = strMsg;
            LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Blue;
        }

        protected void LnkCsvSample_Click(object sender, EventArgs e)
        {
            this.FileDownload("�i�ڃf�[�^�T���v��.csv");
        }

        protected void LnkTabSample_Click(object sender, EventArgs e)
        {
            this.FileDownload("�i�ڃf�[�^�T���v��.txt");
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