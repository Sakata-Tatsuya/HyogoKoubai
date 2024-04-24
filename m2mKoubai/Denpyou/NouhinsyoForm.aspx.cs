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
namespace m2mKoubai.Denpyou
{
    public partial class NouhinsyoForm : Core.Web.ServerViewStatePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.UserKubun != (byte)UserKubun.Shiiresaki)
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return;
                }
                try
                {
                    // �����������Z�b�g����
                    string strKey = HttpContext.Current.Request.Form["HidKey"];
                  
                    // �[�i������
                    this.Create(strKey);
                }
                catch
                {
                    return;
                }
            }
        }

     
        int nGoukei;
        private void Create(string key)
        {
            // �L�[�ɂ���āA�������[�i�����ׂ��擾
            ChumonDataSet.V_Chumon_MeisaiDataTable dt = ChumonClass.getV_Chumon_MeisaiDataTable(key, Global.GetConnection());

            if (dt.Rows.Count == 0)
            {
                this.ShowMsg(AppCommon.NO_DATA, true);
                return;
            }
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

            // ���Ə����ƂŁA�[�i�����������
            for (int nKubunCnt = 0; nKubunCnt < aryKubun.Count; nKubunCnt++)
            {
                nGoukei = 0;
                // �[�i���w�b�_�[
                ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader = ShiiresakiClass.getV_Nouhinsho_HeaderRow(SessionManager.LoginID,int.Parse(aryKubun[nKubunCnt].ToString()), Global.GetConnection());
                if (drHeader != null)
                {
                    CtlNouhinsho_H c = LoadControl("CtlNouhinsho_H.ascx") as CtlNouhinsho_H;
                    c.Create(drHeader);
                    this.T.Rows[0].Cells[0].Controls.Add(c);
                }

                //G.DataSource�e�[�u��
                ChumonDataSet.V_Nouhinsho_MeisaiDataTable dtBind =
                    new ChumonDataSet.V_Nouhinsho_MeisaiDataTable();

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt[j].JigyoushoKubun.ToString() == aryKubun[nKubunCnt].ToString())
                    {
                        // NewBindRow()
                        ChumonDataSet.V_Nouhinsho_MeisaiRow drBind = dtBind.NewV_Nouhinsho_MeisaiRow();

                        drBind.HacchuuNo = dt[j].HacchuuNo;
                        drBind.Hacchuubi = dt[j].HacchuuBi.ToString("yy/MM/dd");
                        drBind.BuhinKubun = dt[j].BuhinKubun;
                        drBind.BuhinCode = dt[j].BuhinCode;
                        drBind.BuhinMei = dt[j].BuhinMei;
                        drBind.Suuryou = dt[j].Suuryou;
                        drBind.Tani = dt[j].Tani;
                        drBind.Tanka = dt[j].Tanka;
                        // ���őΉ�
                        drBind.Kingaku = (int)Math.Round(dt[j].Suuryou * dt[j].Tanka, 0, MidpointRounding.AwayFromZero);
                        //drBind.Kingaku = (int)Math.Floor(dt[j].Suuryou * dt[j].Tanka);
                        nGoukei += drBind.Kingaku;
                        drBind.BarCode = dt[j].HacchuuNo;

                        dtBind.AddV_Nouhinsho_MeisaiRow(drBind);
                    }
                }

                //�@���s��
                int nMaxRowsCount = dtBind.Rows.Count;
                //�@1�y�[�W�ڂ̍s��
                int nFirstPageRow = int.Parse(Global.Nouhinsho_FirstPageRow);
                //�@�Q�y�[�W�ڈȍ~�̍s��
                int nElsePageRow = int.Parse(Global.Nouhinsho_ElsePageRow);

                // �@���y�[�W��
                int nPageCount = 0;
                //�@���݂̍s��
                int nNowRowCount = 0;
                //  �y�[�W������
                if (nMaxRowsCount < nFirstPageRow)
                {
                    nPageCount = 1;
                }
                else
                {
                    int nUseRowCount = nMaxRowsCount - nFirstPageRow;

                    nPageCount = 1;
                    nPageCount += nUseRowCount / nElsePageRow;
                    if (nUseRowCount % nElsePageRow != 0)
                        nPageCount++;
                }
               
                for (int nPageCnt = 0; nPageCnt < nPageCount; nPageCnt++)
                {
                    CtlA4 ctlA4 = LoadControl("CtlA4.ascx") as CtlA4;

                    ArrayList ary = new ArrayList();
                    int RowCount = 0;
                    if (nPageCnt == 0)
                        RowCount = nFirstPageRow;
                    else
                        RowCount = nElsePageRow;

                    // ���݂̍s���{1�y�[�W���̍s���������[�v
                    for (int j = nNowRowCount; j < nNowRowCount + RowCount; j++)
                    {
                        // �f�[�^������s�H��z��ɒǉ�
                        if (j < dtBind.Rows.Count)
                        {
                            ary.Add(dtBind.Rows[j]);
                        }
                        // ��̍s��ǉ�
                        else
                        {
                            ary.Add(dtBind.NewV_Nouhinsho_MeisaiRow());
                        }

                    }

                    ChumonDataSet.V_Nouhinsho_MeisaiRow[] drAry =
                        new ChumonDataSet.V_Nouhinsho_MeisaiRow[ary.Count];

                    nNowRowCount += drAry.Length;
                    ary.CopyTo(drAry);

                    //����             
                    CtlNouhinsho_M ctlMeisai = LoadControl("CtlNouhinsho_M.ascx") as CtlNouhinsho_M;
                    ctlMeisai.Create(drAry);

                    this.T.Rows[0].Cells[0].Controls.Add(ctlA4);
                    ctlA4.Table.Rows[0].Cells[0].Controls.Add(ctlMeisai);

                   
                   
                    // �[�i���̉���
                    if (nPageCnt < nPageCount - 1 )
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

                        if (nKubunCnt <= aryKubun.Count - 1 )
                        {
                            ctlA4.Table.Style.Add("page-break-after", "always");
                        }
                    }
                 
                }
            }


            // ��̏�         
            // ���Ə����ƂŁA��̏����������
            for (int nKubunCnt = 0; nKubunCnt < aryKubun.Count; nKubunCnt++)
            {
                // ��̏��w�b�_
                ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeaderJ = ShiiresakiClass.getV_Nouhinsho_HeaderRow(SessionManager.LoginID, int.Parse(aryKubun[nKubunCnt].ToString()), Global.GetConnection());

                if (drHeaderJ != null)
                {
                    CtlJyuryousho_H c = LoadControl("CtlJyuryousho_H.ascx") as CtlJyuryousho_H;
                    c.Create(drHeaderJ);
                    this.T.Rows[0].Cells[0].Controls.Add(c);
                }
                // �L�[�ɂ���āA��������̏����ׂ��擾
                KenshuDataSet.V_KenshuDataTable dtMeisai = KenshuClass.getV_Kenshu_MeisaiDataTable(key, Global.GetConnection());

                // G.DataSource�e�[�u��
                ChumonDataSet.V_Jyuryosho_MeisaiDataTable dtBindJ = new ChumonDataSet.V_Jyuryosho_MeisaiDataTable();

                nGoukei = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt[i].JigyoushoKubun.ToString() == aryKubun[nKubunCnt].ToString())
                    {
                        ChumonDataSet.V_Jyuryosho_MeisaiRow drBindJ =
                            dtBindJ.NewV_Jyuryosho_MeisaiRow();

                        drBindJ.HacchuuNo = dt[i].HacchuuNo;
                        drBindJ.Hacchuubi = dt[i].HacchuuBi.ToString("yy/MM/dd");
                        drBindJ.BuhinKubun = dt[i].BuhinKubun;
                        drBindJ.BuhinCode = dt[i].BuhinKubun + dt[i].BuhinCode;
                        drBindJ.BuhinMei = dt[i].BuhinMei;
                        drBindJ.Suuryou = dt[i].Suuryou;
                        drBindJ.Tanka = dt[i].Tanka;
                        drBindJ.Tani = dt[i].Tani;
                        // ���őΉ�
                        //drBindJ.Kingaku = (int)Math.Floor(dt[i].Tanka * dt[i].Suuryou); ;
                        drBindJ.Kingaku = (int)Math.Round(dt[i].Tanka * dt[i].Suuryou, 0, MidpointRounding.AwayFromZero);
                        nGoukei += drBindJ.Kingaku;
                        drBindJ.BuhinKubun = dt[i].BuhinKubun;
                        //drBindJ.Ukeirebi = dt[i].NouhinBi.ToString("yy/MM/dd");
                        dtBindJ.AddV_Jyuryosho_MeisaiRow(drBindJ);

                    }
 
                }
                
                // ���s��
                int nMaxRowsCountJ = dtBindJ.Rows.Count;
                // 1�y�[�W�ڂ̍s��
                int nFirstPageRowJ = int.Parse(Global.Jyuryosho_FirstPageRow);
                // 2�y�[�W�ڈȍ~�̍s��
                int nElsePageRowJ = int.Parse(Global.Jyuryosho_ElsePageRow);
                // ���y�[�W��
                int nPageCountJ = 0;
                // ���݂̍s��
                int nNowPageCountJ = 0;
                // �y�[�W������
                if (nMaxRowsCountJ < nFirstPageRowJ)
                {
                    nPageCountJ = 1;
                }
                else
                {
                    int nUseRowCountJ = nMaxRowsCountJ - nFirstPageRowJ;
                    nPageCountJ = 1;
                    nPageCountJ += nUseRowCountJ / nElsePageRowJ;
                    if (nUseRowCountJ % nElsePageRowJ != 0)
                        nPageCountJ++;
                }

               
                for (int nPageCnt = 0; nPageCnt < nPageCountJ; nPageCnt++)
                {
                    CtlA4 ctlA42 = LoadControl("CtlA4.ascx") as CtlA4;
                    ArrayList ary = new ArrayList();
                    int RowCount = 0;
                    if (nPageCnt == 0)
                        RowCount = nFirstPageRowJ;
                    else
                        RowCount = nElsePageRowJ;
                    // ���݂̍s��(�f�[�^������s��)�{�y�[�W�̍s����
                    for (int j = nNowPageCountJ; j < nNowPageCountJ + RowCount; j++)
                    {
                        // �f�[�^������s��ǉ�
                        if (j < dtBindJ.Rows.Count)
                        {
                            ary.Add(dtBindJ.Rows[j]);

                        }
                        // �Ȃ��ꍇ�͋�̍s��ǉ�
                        else
                        {
                            ary.Add(dtBindJ.NewV_Jyuryosho_MeisaiRow());
                        }

                    }
                    ChumonDataSet.V_Jyuryosho_MeisaiRow[] drAry = new ChumonDataSet.V_Jyuryosho_MeisaiRow[ary.Count];

                    nNowPageCountJ += drAry.Length;
                    ary.CopyTo(drAry);

                    // ����
                    CtlJyuryosho_M ctlMeisai = LoadControl("CtlJyuryosho_M.ascx") as CtlJyuryosho_M;
                    ctlMeisai.Create(drAry);

                    this.T.Rows[0].Cells[0].Controls.Add(ctlA42);
                    ctlA42.Table.Rows[0].Cells[0].Controls.Add(ctlMeisai);

                    //this.T.Rows[0].Cells[0].Controls.Add(ctlMeisai);

                    // ��̏��̉���                    
                    if (nPageCnt < nPageCountJ - 1)
                    {
                        ctlA42.Table.Style.Add("page-break-after", "always");
                    }
                    else if (nPageCnt == nPageCountJ - 1)
                    {
                        //���v�e�[�u��
                        // �t�b�^�[                        
                        // ����ŗ�
                        decimal dZeiRitsuJ = (decimal.Parse(Global.ShouhiZei) / 100);
                        // �����
                        int nShohizeiJ = (int)Math.Floor(nGoukei * dZeiRitsuJ);

                        CtlNouhinsho_F fFooter = LoadControl("CtlNouhinsho_F.ascx") as CtlNouhinsho_F;
                        fFooter.Create(nGoukei, nShohizeiJ);

                        this.T.Rows[0].Cells[0].Controls.Add(ctlA42);
                        ctlA42.Table.Rows[0].Cells[0].Controls.Add(fFooter);

                        if (nKubunCnt != aryKubun.Count - 1)
                        {
                            ctlA42.Table.Style.Add("page-break-after", "always");
                        }
                    }

                }
            }
        }
        // ���b�Z�[�W
        private void ShowMsg(string strMsg, bool bError)
        {
            this.LblMsg.Text = strMsg;
            this.LblMsg.ForeColor = (bError) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }
    }
}
