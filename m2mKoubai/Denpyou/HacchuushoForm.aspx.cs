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
using System.Globalization;

namespace m2mKoubai.Denpyou
{
    public partial class HacchuushoForm : Core.Web.ServerViewStatePage
    {
        byte bKubun = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ���[�U�[�敪�m�F
                bKubun = SessionManager.UserKubun;

                // ���h�R�E���A�d���摤�̂ݕ\����
                if (bKubun != (byte)UserKubun.Yodoko && bKubun != (byte)UserKubun.Shiiresaki)
                {
                    HttpContext.Current.Response.Redirect(Global.LoginPageURL, false);
                    return;
                }
                string strKey = "";
                try
                {
                    // �����������Z�b�g����
                    strKey = HttpContext.Current.Request.Form["HidKey"];
                }
                catch
                {
                    ShowMsg(AppCommon.NO_DATA, true);
                    return;
                }

                Create(strKey);
            }
        }

        private void ShowMsg(string strMsg, bool bError)
        {
            this.LblMsg.Text = strMsg;
            this.LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }

        // �ۑ��p
        string ShiiresakiCode = "";
        int cnt = 0;
        private void Create(string key)
        {
            HacchuDataSet_M.V_HacchuDataTable dt =
                         HacchuClass.getV_HacchuDataTable(key, Global.GetConnection());

            if (dt.Rows.Count == 0)
            {
                ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
            if (bKubun == (byte)UserKubun.Yodoko)
            {
                // �d����z����쐬
                ArrayList aryShiire = new ArrayList();
                string strShiire = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt[i].ShiiresakiCode != strShiire)
                    {
                        strShiire = dt[i].ShiiresakiCode;
                        aryShiire.Add(dt[i].ShiiresakiCode);
                    }
                }

                for (int nKubunCnt = 0; nKubunCnt < aryShiire.Count; nKubunCnt++)
                {
                    decimal nGoukei = 0;
                    cnt = 0;

                    // G_DataBind(�\���p�f�[�^�擾)
                    HacchuDataSet_M.V_HacchuBindDataTable dtBind =
                        new HacchuDataSet_M.V_HacchuBindDataTable();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt[i].ShiiresakiCode == aryShiire[nKubunCnt].ToString())
                        {
                            HacchuDataSet_M.V_HacchuBindRow drBind =
                            (HacchuDataSet_M.V_HacchuBindRow)dtBind.NewV_HacchuBindRow();

                            drBind.Year = dt[i].Year;
                            drBind.ShiiresakiCode = dt[i].ShiiresakiCode;
                            drBind.HacchuuNo = dt[i].HacchuuNo;
                            drBind.HacchuuBi = dt[i].HacchuuBi;
                            drBind.BuhinCode = dt[i].BuhinCode;
                            drBind.BuhinMei = dt[i].BuhinMei;
                            drBind.Suuryou = dt[i].Suuryou;
                            drBind.Tani = dt[i].Tani;
                            drBind.Tanka = dt[i].Tanka;
                            drBind.Kingaku = dt[i].Kingaku;
                            drBind.Nouki = dt[i].Nouki;
                            drBind.BashoMei = dt[i].BashoMei;
                            drBind.Bikou = dt[i].Bikou;
                            drBind.ShiiresakiMei = dt[i].ShiiresakiMei;

                            drBind.Name = dt[i].Name;
                            drBind.YubinBangou = dt[i].YubinBangou;
                            drBind.KaishameiY = dt[i].KaishaMei;
                            drBind.Eigyousho = dt[i].EigyouSho;

                            drBind.Address = dt[i].Address;

                            drBind.Tel = dt[i].Tel;

                            drBind.Fax = dt[i].Fax;
                            drBind.YuubinY = dt[i].YuubinY;
                            drBind.TelY = dt[i].TelY;
                            drBind.FaxY = dt[i].FaxY;
                            drBind.AddressY = dt[i].AddressY;

                            nGoukei += dt[i].Kingaku;

                            dtBind.AddV_HacchuBindRow(drBind);
                            ShiiresakiCode = drBind.ShiiresakiCode;

                        }
                    }

                    // ���s��
                    int nMaxRowsCount = dtBind.Rows.Count;
                    //�@�P�y�[�W�ڍs��
                    int nFirstPageRow = int.Parse(Global.Hacchuusho_FirstPageRow);
                    // 2�y�[�W�ڂ���s��
                    int nElsePageRow = int.Parse(Global.Hacchuusho_ElsePageRow);
                    // ��������y�[�W
                    int nPageCount = 0;
                    // ���݂̍s��
                    int nNowPageCount = 0;
                    // ��������y�[�W�����Z�b�g����
                    if (nMaxRowsCount < nFirstPageRow)
                    {
                        nPageCount = 1;
                    }
                    else
                    {
                        // 1�y�[�W�ڕ�����
                        int nUseRowCount = nMaxRowsCount - nFirstPageRow;
                        // 1�y�[�W�ڕ�����������A1���Z�b�g
                        nPageCount = 1;
                        // �y�[�W������
                        nPageCount += nUseRowCount / nElsePageRow;
                        if (nUseRowCount % nElsePageRow != 0)
                        {
                            nPageCount++;
                        }
                    }
                    for (int nPageCnt = 0; nPageCnt < nPageCount; nPageCnt++)
                    {
                        CtlA4 ctlA4 = this.LoadControl("CtlA4.ascx") as CtlA4;
                        ArrayList ary = new ArrayList();
                        int RowCnt = 0;
                        if (nPageCnt == 0)
                            RowCnt = nFirstPageRow;
                        else
                            RowCnt = nElsePageRow;

                        for (int s = nNowPageCount; s < nNowPageCount + RowCnt; s++)
                        {                           
                            if (s < dtBind.Rows.Count)
                            {
                                ary.Add(dtBind.Rows[s]);
                            }
                            else
                            {
                                ary.Add(dtBind.NewV_HacchuBindRow());
                            }
                        }

                        if (nPageCount != 1) { cnt++; }

                        HacchuDataSet_M.V_HacchuBindRow[] drAry =
                            new HacchuDataSet_M.V_HacchuBindRow[ary.Count];
                        nNowPageCount += drAry.Length;
                        ary.CopyTo(drAry);


                        // ����p�f�[�^�擾
                        CtlHacchuusho ctlHacchuusho = LoadControl("CtlHacchuusho.ascx") as CtlHacchuusho;
                        ctlHacchuusho.Create(drAry, cnt);

                        this.T.Rows[0].Cells[0].Controls.Add(ctlA4);
                        ctlA4.Table.Rows[0].Cells[0].Controls.Add(ctlHacchuusho);
                        if (nPageCnt < nPageCount - 1)
                        {
                            ctlA4.Table.Style.Add("page-break-after", "always");
                        }
                        else if (nPageCnt == nPageCount - 1)
                        {
                            //���v�e�[�u��
                            // �t�b�^�[
                            // �����
                            // ����ŗ�
                            decimal dZeiRitsu = (decimal.Parse(Global.ShouhiZei) / 100);
                            // �����
                            int nShohizei = (int)Math.Floor(nGoukei * dZeiRitsu);

                            CtlNouhinsho_F ctlNouhinsho = LoadControl("CtlNouhinsho_F.ascx") as CtlNouhinsho_F;
                            ctlNouhinsho.Create((int)nGoukei, nShohizei);

                            this.T.Rows[0].Cells[0].Controls.Add(ctlA4);
                            ctlA4.Table.Rows[0].Cells[0].Controls.Add(ctlNouhinsho);
                            if (nKubunCnt != aryShiire.Count - 1)
                            {
                                ctlA4.Table.Style.Add("page-break-after", "always");
                            }
                        }
                    }
                }
            }
            else
            {
                // ���Ə��z��
                ArrayList aryKubun = new ArrayList();

                int nKubun = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt[i].JigyoushoKubun != nKubun)
                    {
                        nKubun = dt[i].JigyoushoKubun;
                        aryKubun.Add(dt[i].JigyoushoKubun);
                    }
                }

                for (int nKubunCnt = 0; nKubunCnt < aryKubun.Count; nKubunCnt++)
                {
                    decimal nGoukei = 0;
                    cnt = 0;

                    // G_DataBind(�\���p�f�[�^�擾)
                    HacchuDataSet_M.V_HacchuBindDataTable dtBind =
                        new HacchuDataSet_M.V_HacchuBindDataTable();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt[i].JigyoushoKubun.ToString() == aryKubun[nKubunCnt].ToString())
                        {
                            HacchuDataSet_M.V_HacchuBindRow drBind =
                                (HacchuDataSet_M.V_HacchuBindRow)dtBind.NewV_HacchuBindRow();

                            drBind.Year = dt[i].Year;
                            drBind.ShiiresakiCode = dt[i].ShiiresakiCode;
                            drBind.HacchuuNo = dt[i].HacchuuNo;
                            drBind.HacchuuBi = dt[i].HacchuuBi;
                            drBind.BuhinCode = dt[i].BuhinCode;
                            drBind.BuhinMei = dt[i].BuhinMei;
                            drBind.Suuryou = dt[i].Suuryou;
                            drBind.Tani = dt[i].Tani;
                            drBind.Tanka = dt[i].Tanka;
                            drBind.Kingaku = dt[i].Kingaku;
                            drBind.Nouki = dt[i].Nouki;
                            drBind.BashoMei = dt[i].BashoMei;
                            drBind.Bikou = dt[i].Bikou;
                            drBind.ShiiresakiMei = dt[i].ShiiresakiMei;

                            drBind.Name = dt[i].Name;
                            drBind.YubinBangou = dt[i].YubinBangou;
                            drBind.KaishameiY = dt[i].KaishaMei;
                            drBind.Eigyousho = dt[i].EigyouSho;

                            drBind.Address = dt[i].Address;

                            drBind.Tel = dt[i].Tel;

                            drBind.Fax = dt[i].Fax;
                            drBind.YuubinY = dt[i].YuubinY;
                            drBind.TelY = dt[i].TelY;
                            drBind.FaxY = dt[i].FaxY;
                            drBind.AddressY = dt[i].AddressY;

                            nGoukei += dt[i].Kingaku;

                            dtBind.AddV_HacchuBindRow(drBind);
                            ShiiresakiCode = drBind.ShiiresakiCode;

                        }
                    }

                    // ���s��
                    int nMaxRowsCount = dtBind.Rows.Count;
                    //�@�P�y�[�W�ڍs��
                    int nFirstPageRow = int.Parse(Global.Hacchuusho_FirstPageRow);
                    // 2�y�[�W�ڂ���s��
                    int nElsePageRow = int.Parse(Global.Hacchuusho_ElsePageRow);
                    // ��������y�[�W
                    int nPageCount = 0;
                    // ���݂̍s��
                    int nNowPageCount = 0;
                    // ��������y�[�W�����Z�b�g����
                    if (nMaxRowsCount < nFirstPageRow)
                    {
                        nPageCount = 1;
                    }
                    else
                    {
                        // 1�y�[�W�ڕ�����
                        int nUseRowCount = nMaxRowsCount - nFirstPageRow;
                        // 1�y�[�W�ڕ�����������A1���Z�b�g
                        nPageCount = 1;
                        // �y�[�W������
                        nPageCount += nUseRowCount / nElsePageRow;
                        if (nUseRowCount % nElsePageRow != 0)
                        {
                            nPageCount++;
                        }
                    }
                    for (int nPageCnt = 0; nPageCnt < nPageCount; nPageCnt++)
                    {
                        CtlA4 ctlA4 = this.LoadControl("CtlA4.ascx") as CtlA4;
                        ArrayList ary = new ArrayList();
                        int RowCnt = 0;
                        if (nPageCnt == 0)
                            RowCnt = nFirstPageRow;
                        else
                            RowCnt = nElsePageRow;

                        for (int s = nNowPageCount; s < nNowPageCount + RowCnt; s++)
                        {
                            if (s < dtBind.Rows.Count)
                            {
                                ary.Add(dtBind.Rows[s]);
                            }
                            else
                            {
                                ary.Add(dtBind.NewV_HacchuBindRow());
                            }
                        }

                        if (nPageCount != 1) { cnt++; }
                        HacchuDataSet_M.V_HacchuBindRow[] drAry =
                            new HacchuDataSet_M.V_HacchuBindRow[ary.Count];
                        nNowPageCount += drAry.Length;
                        ary.CopyTo(drAry);


                        // ����p�f�[�^�擾
                        CtlHacchuusho ctlHacchuusho = LoadControl("CtlHacchuusho.ascx") as CtlHacchuusho;
                        ctlHacchuusho.Create(drAry, cnt);

                        this.T.Rows[0].Cells[0].Controls.Add(ctlA4);
                        ctlA4.Table.Rows[0].Cells[0].Controls.Add(ctlHacchuusho);

                        if (nPageCnt < nPageCount - 1)
                        {
                            ctlA4.Table.Style.Add("page-break-after", "always");
                        }
                        else if (nPageCnt == nPageCount - 1)
                        {
                            //���v�e�[�u��
                            // �t�b�^�[
                            // �����
                            // ����ŗ�
                            decimal dZeiRitsu = (decimal.Parse(Global.ShouhiZei) / 100);
                            // �����
                            int nShohizei = (int)Math.Floor(nGoukei * dZeiRitsu);

                            CtlNouhinsho_F ctlNouhinsho = LoadControl("CtlNouhinsho_F.ascx") as CtlNouhinsho_F;
                            ctlNouhinsho.Create((int)nGoukei, nShohizei);

                            this.T.Rows[0].Cells[0].Controls.Add(ctlA4);
                            ctlA4.Table.Rows[0].Cells[0].Controls.Add(ctlNouhinsho);

                            if (nKubunCnt != aryKubun.Count - 1)
                            {
                                ctlA4.Table.Style.Add("page-break-after", "always");
                            }
                        }
                    }
                }
            }
        }
    }
}