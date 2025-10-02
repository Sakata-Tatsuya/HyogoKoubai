using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
//using ExcelCreator;
//using ExcelCreator6;
using Telerik.Web.UI;
using KoubaiDAL;
using Microsoft.VisualBasic;

namespace Koubai.Master
{
    public partial class CtlKouseiMasterUpload : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void getRegData()
        {
            DateTime now = DateTime.Now;
            Core.Error ret = new Core.Error();
            this.ShowMsg("", true);

            try
            {
                Dictionary<string, Microsoft.VisualBasic.FileIO.TextFieldParser> dic = new Dictionary<string, Microsoft.VisualBasic.FileIO.TextFieldParser>();


                if (FileA.HasFile && FileA.FileBytes.Length != 0)
                {
                    Microsoft.VisualBasic.FileIO.TextFieldParser parser1 =
                        new Microsoft.VisualBasic.FileIO.TextFieldParser
                            (FileA.PostedFile.InputStream, System.Text.Encoding.GetEncoding(932));

                    if (!dic.ContainsKey(FileA.PostedFile.FileName))
                    { dic.Add(FileA.PostedFile.FileName, parser1); }
                    else
                    { throw new Exception("ファイル名が重複しています"); }
                }

                if (dic.Count == 0) { throw new Exception("ファイルを取得出来ませんでした"); }

                int nYamaseCnt = 0;
                string strFileName = FileA.PostedFile.FileName;

                if (!FileA.HasFile && FileA.FileBytes.Length == 0) { return; }

                if (!dic.ContainsKey(strFileName)) { return; }

                Microsoft.VisualBasic.FileIO.TextFieldParser parser = dic[strFileName];

                char SPRITTER = ',';
                int nRowCnt = 1;
                int nRowNo = 0;
                int nTooshiNo = 0;

                decimal decTemp = 0;

                bool isHead = false;

                MasterDataSet.M_KouseiDataTable dtMain = new MasterDataSet.M_KouseiDataTable();

                string sSeihinCode = string.Empty;

                while (!parser.EndOfData)
                {
                    if (isHead)
                    {
                        isHead = false;
                        continue;
                    }

                    try
                    {
                        DateTime dtToday = DateTime.Now; 
                        parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                        parser.SetDelimiters(SPRITTER.ToString());// ","区切り
                        string[] strAry = parser.ReadFields();
                        if (nRowCnt == 1)
                        {
                            if (strAry[0].ToString() != "Lv")
                            {
                                ShowMsg(string.Format("ファイルの様式が異なります。"), true);
                                return;
                            }
                            else
                            {
                                nRowCnt++;
                                continue;
                            }
                        }

                        if (strAry.Length <= 2)
                        { break; }

                        if (strAry[1].ToString() == "" || strAry[2].ToString() == "")
                        { continue; }

                        MasterDataSet.M_KouseiRow drMain = dtMain.NewM_KouseiRow();

                        if (nRowCnt > 0)
                        {
                            nRowNo++;
                            drMain.SeihinCode = strAry[1].ToString();
                            if (sSeihinCode != drMain.SeihinCode)
                            {
                                sSeihinCode = drMain.SeihinCode;
                                nTooshiNo = 1;
                            }
                            else
                            {
                                nTooshiNo++;
                            }
                            drMain.BuhinCode = strAry[2].ToString();
                            drMain.KouteiCode = strAry[6].ToString();
                            decTemp = 0;
                            decimal.TryParse(strAry[4].ToString(),out decTemp);
                            drMain.Insu = (int)decTemp;
                            drMain.DeleteFlag = false;
                            drMain.HinmokuBunrui = strAry[7].ToString();
                            drMain.Hinmei = strAry[8].ToString();
                            drMain.HinmokuCode = strAry[9].ToString();
                            drMain.KouteiMei = string.Empty;
                            drMain.HinmokuBunruiCode = string.Empty;

                            drMain.ORNo = strAry[10].ToString();
                            drMain.MakerCode = string.Empty;
                            drMain.Tanni = string.Empty;
                            drMain.Bikou1 = strAry[12].ToString();
                            drMain.Bikou2 = strAry[13].ToString();
                            drMain.SelectOR = false;
                            if (strAry[11].ToString() == "選択") { drMain.SelectOR = true; }
                            drMain.TourokuDateTime = dtToday;
                            //drMain.No = nRowNo;
                            drMain.No = nTooshiNo;
                            drMain.TooshiNo = nTooshiNo.ToString("0");
                            if (drMain.SeihinCode != "")
                            { drMain.HyoujiSeihinCode = sSeihinCode; }
                            else
                            { drMain.HyoujiSeihinCode = ""; }

                            dtMain.AddM_KouseiRow(drMain);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMsg(string.Format("ファイル取込に失敗しました。({0})", ex.Message), true);
                        return;
                    }

                    nYamaseCnt++;

                }
                //製品番号毎に分割処理
                var dvS = new DataView(dtMain);
                DataTable uniqueDt = dvS.ToTable(true, "SeihinCode");

                foreach (DataRow drPd in uniqueDt.Rows)
                {
                    string strPd = drPd["SeihinCode"].ToString();

                    DataRow[] drs = dtMain.Select("SeihinCode='" + strPd + "'");
                    MasterDataSet.M_KouseiDataTable newdt = new MasterDataSet.M_KouseiDataTable();
                    foreach (var drnew in drs)
                    {
                        DataRow newrow = newdt.NewRow();
                        newrow.ItemArray = drnew.ItemArray;
                        newdt.Rows.Add(newrow);
                    }
                    ret = MasterClass.Del_and_AddKousei(newdt, Global.GetConnection());
                    if (null != ret)
                    {
                        this.ShowMsg(ret.Message, true);
                        return;
                    }
                }

                ShowMsg(string.Format("登録完了致しました。"), false);

            }
            catch (Exception ex)
            {
                ShowMsg(string.Format("登録に失敗しました。({0})", ex.Message), true);
                return;
            }
        }

        private void ShowMsg(string strMsg, bool bError)
        {
            if (bError)
            {
                LblMsg.Text += strMsg;
            }
            else 
            {
                LblMsg.Text = strMsg;
            }
            LblMsg.Text += strMsg;
            LblMsg.ForeColor = (bError) ? Color.Red : Color.Blue;
        }

    }
}