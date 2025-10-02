using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using KoubaiDAL;

namespace Koubai.Common
{/// <summary>
 /// DownloadDataForm の概要の説明です。
 /// </summary>
    public partial class DownloadDataForm : System.Web.UI.Page
    {
        private enum EnumDataType
        {
            File, Text, Binary, AllFile
        }

        private class FileDataInfo
        {
            public EnumDataType type = EnumDataType.File;
            public string strDataCacheKey = "";
            public string strFileName = null;
            public string strFilePath;
            public int nTextEncodingCodePage = System.Text.Encoding.UTF8.CodePage;
            public bool bDeleteFile = false;
        }

        public static string GetQueryString4File(string strFileName, string strFilePath, bool bDeleteFile)
        {
            FileDataInfo fi = new FileDataInfo();
            fi.type = EnumDataType.File;
            fi.strFilePath = strFilePath;
            fi.strFileName = strFileName;
            fi.bDeleteFile = bDeleteFile;
            return GetQueryString(fi);
        }

        public static string GetQueryString4FileAll(string strFileName, string strFilePath, bool bDeleteFile)
        {
            FileDataInfo fi = new FileDataInfo();
            fi.type = EnumDataType.AllFile;
            fi.strFilePath = strFilePath;
            fi.strFileName = strFileName;
            fi.bDeleteFile = bDeleteFile;
            return GetQueryString(fi);
        }

        public static string GetQueryString4Text(
        string strFileName, string strTextData)
        {
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(932);
            switch (SessionManager.User.LanguageCode)
            {
                case "zh":
                case "en":
                    enc = System.Text.Encoding.UTF8;
                    break;
            }

            return GetQueryString4Text(strFileName, strTextData, enc);
        }

        public static string GetQueryString4Text(
            Core.Data.DataTable2Text.EnumDataFormat fmt, string strFileName, string strTextData)
        {
            System.Text.Encoding enc = System.Text.Encoding.UTF8;

            if (SessionManager.User.LanguageCode == "ja" && fmt == Core.Data.DataTable2Text.EnumDataFormat.Csv)
            {
                enc = System.Text.Encoding.GetEncoding(932);
            }
            else if (SessionManager.User.LanguageCode == "zh" && fmt == Core.Data.DataTable2Text.EnumDataFormat.Csv)
            {
                enc = System.Text.Encoding.GetEncoding(936);
            }

            return GetQueryString4Text(strFileName, strTextData, enc);
        }

        private static string GetQueryString4Text(
            string strFileName, string strTextData, System.Text.Encoding enc)
        {
            FileDataInfo fi = new FileDataInfo();
            fi.type = EnumDataType.Text;
            fi.strFileName = strFileName;
            fi.strDataCacheKey = SessionManager.User.AddCacheData(strTextData, 3);
            fi.nTextEncodingCodePage = enc.CodePage;
            return GetQueryString(fi);
        }

        public static string GetQueryString4Binary(
            string strFileName, byte[] bData)
        {
            FileDataInfo fi = new FileDataInfo();
            fi.type = EnumDataType.Binary;
            fi.strFileName = strFileName;
            fi.strDataCacheKey = SessionManager.User.AddCacheData(bData, 3);
            return GetQueryString(fi);
        }

        private static string GetQueryString(FileDataInfo fi)
        {
            return SessionManager.User.Encode(new string[] {
            fi.bDeleteFile.ToString(),
            fi.nTextEncodingCodePage.ToString(),
            fi.strDataCacheKey,
            fi.strFileName,
            fi.strFilePath,
            ((int)fi.type).ToString()
            });
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Response.Clear();

            FileDataInfo fi = new FileDataInfo();

            try
            {
                string[] str = SessionManager.User.Decode(this.Request.Url.Query.Substring(1));
                if (null == str)
                {
                    ShowErrMsg("不正なアクセスです。");
                    return;
                }

                fi.bDeleteFile = Convert.ToBoolean(str[0]);
                fi.nTextEncodingCodePage = Convert.ToInt32(str[1]);
                fi.strDataCacheKey = str[2];
                fi.strFileName = str[3];
                fi.strFilePath = str[4];
                fi.type = (EnumDataType)int.Parse(str[5]);

                if (null == fi) throw new Exception("");

                string strFileName = "";
                switch (fi.type)
                {
                    case EnumDataType.File:
                        if (!string.IsNullOrEmpty(fi.strFileName))
                            strFileName = fi.strFileName;
                        else
                            strFileName = System.IO.Path.GetFileName(fi.strFilePath);
                        break;
                    default:
                        strFileName = fi.strFileName;
                        break;
                }

                // 半角空白が+に変わるので.Replace("+", "%20")
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(strFileName).Replace("+", "%20"));
                Response.ContentType = "application/octet-stream";

                switch (fi.type)
                {
                    case EnumDataType.File:
                        {
                            this.Response.WriteFile(fi.strFilePath);
                            this.Response.Flush();	// ★ Flush()しないとファイルが削除された後にファイルを読みこんでしまう

                            if (fi.bDeleteFile)
                            {
                                System.IO.File.Delete(fi.strFilePath);
                            }
                        }
                        break;
                    case EnumDataType.AllFile:
                        {
                            System.IO.Stream iStream = null;
                            // Buffer to read 10K bytes in chunk:
                            byte[] buffer = new Byte[10000];
                            // Length of the file:
                            int length;
                            // Total bytes to read:
                            long dataToRead;

                            // Open the file.
                            try
                            {
                                iStream = new System.IO.FileStream(fi.strFilePath, System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read, System.IO.FileShare.Read);
                            }
                            catch
                            {
                                // ダウンロード失敗の場合tempfile.zip（予備）を作成しておく
                                fi.strFilePath = Global.TempPath + "tempfile";
                                iStream = new System.IO.FileStream(fi.strFilePath, System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read, System.IO.FileShare.Read);
                            }
                            // Total bytes to read:
                            dataToRead = iStream.Length;
                            // Read the bytes.
                            while (dataToRead > 0)
                            {
                                // Verify that the client is connected.
                                if (Response.IsClientConnected)
                                {
                                    // Read the data in buffer.
                                    length = iStream.Read(buffer, 0, 10000);

                                    // Write the data to the current output stream.
                                    Response.OutputStream.Write(buffer, 0, length);

                                    // Flush the data to the HTML output.
                                    Response.Flush();

                                    buffer = new Byte[10000];
                                    dataToRead = dataToRead - length;
                                }
                                else
                                {
                                    //prevent infinite loop if user disconnects
                                    dataToRead = -1;
                                }
                            }

                            if (iStream != null)
                            {
                                //Close the file.
                                iStream.Close();
                            }

                            if (fi.bDeleteFile)
                            {
                                System.IO.File.Copy(fi.strFilePath, Global.TempPath + "tempfile", true);
                                System.IO.File.Delete(fi.strFilePath);
                            }
                        }
                        break;
                    case EnumDataType.Text:
                        {
                            byte[] bom = System.Text.Encoding.UTF8.GetPreamble();
                            this.Response.BinaryWrite(bom);

                            this.Response.Write((string)SessionManager.User.GetCacheData(fi.strDataCacheKey));
                        }
                        break;
                    case EnumDataType.Binary:
                        this.Response.BinaryWrite((byte[])SessionManager.User.GetCacheData(fi.strDataCacheKey));
                        break;
                }
            }
            catch (Exception ex)
            {
                this.Response.Clear();
                this.Response.Write(ex.Message);
            }
            finally
            {
                this.Response.End();    // 必要
                if (null != fi)
                {
                    if (!string.IsNullOrEmpty(fi.strDataCacheKey))
                        SessionManager.User.RemoveCacheData(fi.strDataCacheKey);
                }
            }
        }

        private void ShowErrMsg(string strMsg)
        {
            Response.Clear();
            Response.Write(string.Format("<script>window.alert('{0}');</script>", strMsg));
            Response.End();
        }
    }
}
