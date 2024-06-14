using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using m2mKoubaiDAL;
using System.Data.SqlClient;
using System.Web.Services.Description;
using Telerik.Web.Design;
//using Core.Type;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Policy;

namespace m2mKoubai
{
    public class CreatePDF
    {
        internal static MemoryStream CreateInvoicePDF(string sLoginID, string sKaishaCode,int nMonth, KenshuDataSet.V_Kenshu2DataTable dtK)
        {
            m2mKoubaiDataSet.M_ShiiresakiRow drShiire = ShiiresakiClass.getM_ShiiresakiRow(sKaishaCode, Global.GetConnection());

            //明細最大行数
            int MaxRow = 36;
            string strTemp=string.Empty;

            // 事業所配列
            ArrayList aryKubun = new ArrayList();
            int nKubun = 0;
            for (int i = 0; i < dtK.Rows.Count; i++)
            {
                if (dtK[i].JigyoushoKubun != nKubun)
                {
                    nKubun = dtK[i].JigyoushoKubun;
                    aryKubun.Add(dtK[i].JigyoushoKubun);
                }
            }
            //PDF の準備
            var doc = new Document(PageSize.A4);
            var stream = new MemoryStream();
            //ファイルの出力先を設定
            var pw = PdfWriter.GetInstance(doc, stream);
            //ドキュメントを開く
            doc.Open();
            PdfContentByte pdfContentByte = pw.DirectContent;

            string fontFolder = Environment.SystemDirectory.Replace("system32", "fonts");
            string fontName = fontFolder + @"\msgothic.ttc,0";
            BaseFont baseFont = BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //フォントサイズ
            float fontSize = 3.0f;
            //フォントとフォントサイズの指定
            pdfContentByte.SetFontAndSize(baseFont, fontSize);

            //テキスト描画開始
            //pdfContentByte.BeginText();

            Font font = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            Font font08 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font08.Size = 8;
            Font font09 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font09.Size = 9;
            Font font10 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font10.Size = 10;
            Font font12 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font12.Size = 12;
            Font font14 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font14.Size = 14;

            //事業所単位にページ編集
            for (int nKubunCnt = 0; nKubunCnt < aryKubun.Count; nKubunCnt++)
            {
                string strJC = aryKubun[nKubunCnt].ToString();

                DataRow[] drD = dtK.Select("JigyoushoKubun='" + strJC + "'");
                KenshuDataSet.V_Kenshu2DataTable dtD = new KenshuDataSet.V_Kenshu2DataTable();
                foreach (var drnew in drD)
                {
                    DataRow newrow = dtD.NewRow();
                    newrow.ItemArray = drnew.ItemArray;
                    dtD.Rows.Add(newrow);
                }
                //1事業所の編集開始
                //表紙
                //ヘッダー部分
                ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader = ShiiresakiClass.getV_Nouhinsho_HeaderRow(sLoginID, dtD[0].JigyoushoKubun, Global.GetConnection());

                //float w = doc.PageSize.Width / 2 + 100;
                PdfPTable tbl = new PdfPTable(1);
                strTemp = drHeader.KaishaMei + " 御中";
                PdfPCell cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                tbl = new PdfPTable(3);
                tbl.SetTotalWidth(new float[] { 200, 140, 200 });
                //1
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                strTemp = DateTime.Today.ToString("yyyy年MM月dd日");
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //2
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                strTemp = drHeader.ShiiresakiMei;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //3
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                strTemp = "登録番号　" + drHeader.InvoiceRegNo;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //4
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                strTemp = "〒" + Utility.FormatYuubin(drHeader.YubinBangou);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //5
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                strTemp = drHeader.Address;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //6
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                strTemp = "TEL：" + Utility.FormatBanggo(drHeader.Tel) + " FAX：" + Utility.FormatBanggo(drHeader.Fax);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                tbl = new PdfPTable(1);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                strTemp = "拝啓 貴社益々ご清栄のこととお慶び申し上げます。";
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "さて、" + nMonth.ToString() + "月分の請求書を添付いたします。ご査收のほど宜しくお願いします。";
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "なお、お振込みは下記口座までお願いします。";
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "記";
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                doc.Add(tbl);

                List<KenshuClass.ZeirituShukei> lst = new List<KenshuClass.ZeirituShukei>();

                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    bool IsUpdate = false;
                    bool mKeigenZeirituFlg = dtD[i].KeigenZeirituFlg;
                    int mZeiritu = dtD[i].Zeiritu;
                    decimal mKingaku = Math.Round(dtD[i].Tanka * dtD[i].ChumonSuuryou, 0, MidpointRounding.AwayFromZero);
                    decimal mZeigaku = mKingaku * mZeiritu / 100;

                    if (lst.Count > 0)
                    {
                        for (int ix = 0; ix < lst.Count; ix++)
                        {
                            if (lst[ix].iZeiritu == mZeiritu && lst[ix].bKeigenZeirituFlg == mKeigenZeirituFlg)
                            {
                                IsUpdate = true;
                                lst[ix].iKingaku += (int)mKingaku;
                                lst[ix].iZeigaku += (int)mZeigaku;
                            }
                        }
                    }
                    if (!IsUpdate)
                    {
                        KenshuClass.ZeirituShukei m = new KenshuClass.ZeirituShukei();
                        m.iZeiritu = mZeiritu;
                        m.bKeigenZeirituFlg = mKeigenZeirituFlg;
                        m.iKingaku += (int)mKingaku;
                        m.iZeigaku += (int)mZeigaku;
                        lst.Add(m);
                    }
                }
                int nGoukei = 0;
                int nZeigaku = 0;
                for (int j = 0; j < lst.Count; j++)
                {
                    nGoukei += lst[j].iKingaku;
                    nZeigaku += lst[j].iZeigaku;
                }
                int nSouGoukei = nGoukei + nZeigaku;
                tbl = new PdfPTable(3);
                tbl.SetTotalWidth(new float[] { 240, 120, 120 });

                strTemp = "御請求金額(" + nMonth.ToString() + "月度分)";
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = string.Format("\\{0:#,##0}", nSouGoukei);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);

                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "合計金額";
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "消費税額";
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);

                for (int j = 0; j < lst.Count; j++)
                {
                    strTemp = lst[j].iZeiritu.ToString("#0") + "%対象";
                    if (lst[j].bKeigenZeirituFlg)
                    {
                        strTemp += "(軽減税率)";
                    }
                    cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                    tbl.AddCell(cell);
                    strTemp = lst[j].iKingaku.ToString("#,##0");
                    cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                    tbl.AddCell(cell);
                    strTemp = lst[j].iZeigaku.ToString("#,##0");
                    cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                    tbl.AddCell(cell);
                }
                doc.Add(tbl);

                //振込先
                tbl = new PdfPTable(1);
                cell = new PdfPCell(new Paragraph("", font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "振　込　先";
                cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                tbl = new PdfPTable(2);
                tbl.SetTotalWidth(new float[] { 120, 240 });

                strTemp = "口座名義";
                cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = drShiire.KouzaMeigi;
                cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "金融機関名";
                cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = drShiire.KinyuuKikanMei;
                cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "口座番号";
                cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = drShiire.KouzaBangou;
                cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                doc.NewPage();

                tbl = new PdfPTable(2);
                tbl.SetTotalWidth(new float[] { 100, 460 });
                strTemp = "請　求　書";
                cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font14)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                tbl = new PdfPTable(3);
                tbl.SetTotalWidth(new float[] { 200, 140, 200 });
                //1
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                strTemp = DateTime.Today.ToString("yyyy年MM月dd日");
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //2
                strTemp = "〒" + Utility.FormatYuubin(drHeader.YubinH);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                //3
                strTemp = drHeader.AddressH;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = drHeader.ShiiresakiMei;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //4
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "登録番号　" + drHeader.InvoiceRegNo;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //5
                strTemp = drHeader.KaishaMei + " 御中";
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "〒" + Utility.FormatYuubin(drHeader.YubinBangou);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //6
                strTemp = "TEL：" + Utility.FormatBanggo(drHeader.TelH);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = drHeader.Address;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //7
                strTemp = "FAX：" + Utility.FormatBanggo(drHeader.FaxH);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "TEL：" + Utility.FormatBanggo(drHeader.Tel) + " FAX：" + Utility.FormatBanggo(drHeader.Fax);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //8
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "FAX：" + Utility.FormatBanggo(drHeader.Fax);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                tbl.AddCell(cell);

                doc.Add(tbl);

                tbl = new PdfPTable(2);
                tbl.SetTotalWidth(new float[] { 100, 460 });
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "毎度ありがとうございます下記の通り御請求申し上げます。";
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                //明細
                tbl = new PdfPTable(7);
                tbl.SetTotalWidth(new float[] { 68, 82, 174, 42, 42, 64, 72 });
                cell = new PdfPCell(new Paragraph("発注No", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("品目コード", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("品目名", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("数量", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("単位", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("単価", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("金額", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);

                //明細詳細
                int LineCnt = 0;
                for (int i = 0; i < dtD.Count; i++)
                {
                    if (LineCnt >= MaxRow)
                    {
                        doc.Add(tbl);
                        doc.NewPage();
                        LineCnt = 1;
                        tbl = new PdfPTable(7);
                        tbl.SetTotalWidth(new float[] { 68, 82, 174, 42, 42, 64, 72 });
                        cell = new PdfPCell(new Paragraph("発注No", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("品目コード", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("品目名", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("数量", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("単位", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("単価", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("金額", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                    }
                    else
                    {
                        LineCnt++;
                    }

                    strTemp = dtD[i].HacchuuNo;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].BuhinCode;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    if (dtD[i].KeigenZeirituFlg)
                    {
                        strTemp = "* " + dtD[i].BuhinMei;
                    }
                    else
                    {
                        strTemp = "　" + dtD[i].BuhinMei;
                    }
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].ChumonSuuryou.ToString("#,##0");
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].Tani;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = string.Format("\\{0:#,##0.#0}", dtD[i].Tanka);
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                    strTemp = string.Format("\\{0:#,##0.#0}", dtD[i].Kingaku);
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                }

                if (LineCnt <= MaxRow)
                {
                    doc.Add(tbl);
                }

                //明細表描画終了
                tbl = new PdfPTable(2);
                tbl.SetTotalWidth(new float[] { 160, 400 });
                cell = new PdfPCell(new Paragraph("", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "* 軽減税率対象";
                cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                doc.Add(tbl);

                tbl = new PdfPTable(7);
                tbl.SetTotalWidth(new float[] { 140, 60, 100, 60, 100, 60, 100 });
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("合計", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                strTemp = string.Format("\\{0:#,##0}", nGoukei);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("消費税", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                strTemp = string.Format("\\{0:#,##0}", nZeigaku);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("総合計", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                strTemp = string.Format("\\{0:#,##0}", nSouGoukei);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                tbl.AddCell(cell);
                doc.Add(tbl);
                //1事業所の編集終了
            }

            //テキスト描画終了
            //pdfContentByte.EndText();

            doc.Close();
            return stream;
        }

        internal static MemoryStream CreateAcceptancePDF(string sLoginID, string sKaishaCode, int nMonth, KenshuDataSet.V_Kenshu2DataTable dtK)
        {
            m2mKoubaiDataSet.M_ShiiresakiRow drShiire = ShiiresakiClass.getM_ShiiresakiRow(sKaishaCode, Global.GetConnection());
            //明細最大行数
            int MaxRow = 36;
            string strTemp = string.Empty;

            // 事業所配列
            ArrayList aryKubun = new ArrayList();
            int nKubun = 0;
            for (int i = 0; i < dtK.Rows.Count; i++)
            {
                if (dtK[i].JigyoushoKubun != nKubun)
                {
                    nKubun = dtK[i].JigyoushoKubun;
                    aryKubun.Add(dtK[i].JigyoushoKubun);
                }
            }
            //PDF の準備
            var doc = new Document(PageSize.A4);
            var stream = new MemoryStream();
            //ファイルの出力先を設定
            var pw = PdfWriter.GetInstance(doc, stream);
            //ドキュメントを開く
            doc.Open();
            PdfContentByte pdfContentByte = pw.DirectContent;

            string fontFolder = Environment.SystemDirectory.Replace("system32", "fonts");
            string fontName = fontFolder + @"\msgothic.ttc,0";
            BaseFont baseFont = BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //フォントサイズ
            float fontSize = 3.0f;
            //フォントとフォントサイズの指定
            pdfContentByte.SetFontAndSize(baseFont, fontSize);

            Font font = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));

            Font font08 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font08.Size = 8;
            Font font09 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font09.Size = 9;
            Font font10 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font10.Size = 10;
            Font font12 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font12.Size = 12;
            Font font14 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font14.Size = 14;

            //事業所単位にページ編集
            for (int nKubunCnt = 0; nKubunCnt < aryKubun.Count; nKubunCnt++)
            {
                string strJC = aryKubun[nKubunCnt].ToString();

                DataRow[] drD = dtK.Select("JigyoushoKubun='" + strJC + "'");
                KenshuDataSet.V_Kenshu2DataTable dtD = new KenshuDataSet.V_Kenshu2DataTable();
                foreach (var drnew in drD)
                {
                    DataRow newrow = dtD.NewRow();
                    newrow.ItemArray = drnew.ItemArray;
                    dtD.Rows.Add(newrow);
                }
                //1事業所の編集開始
                //ヘッダー部分
                ShiiresakiDataSet.V_Nouhinsho_HeaderRow drHeader = ShiiresakiClass.getV_Nouhinsho_HeaderRow(sLoginID, dtD[0].JigyoushoKubun, Global.GetConnection());

                //float w = doc.PageSize.Width / 2 + 100;
                PdfPTable tbl = new PdfPTable(1);
                strTemp = drHeader.ShiiresakiMei + " 御中";
                PdfPCell cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = Utility.FormatFromyyyyMM(nMonth.ToString());
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                tbl = new PdfPTable(3);
                tbl.SetTotalWidth(new float[] { 200, 140, 200 });
                //1
                strTemp = drHeader.ShiiresakiMei;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "〒" + Utility.FormatYuubin(drHeader.YubinH);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //2
                strTemp = "〒" + Utility.FormatYuubin(drHeader.YubinBangou);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = drHeader.AddressH;
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //3
                strTemp = drHeader.Address;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = drHeader.KaishaMei;
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //4
                strTemp = "TEL：" + Utility.FormatBanggo(drHeader.Tel);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "TEL：" + Utility.FormatBanggo(drHeader.TelH);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //5
                strTemp = "FAX：" + Utility.FormatBanggo(drHeader.Fax);
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "FAX：" + Utility.FormatBanggo(drHeader.FaxH);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);

                doc.Add(tbl);

                tbl = new PdfPTable(2);
                tbl.SetTotalWidth(new float[] { 550, 10 });
                cell = new PdfPCell(new Paragraph("", font08)) { FixedHeight = 12f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                strTemp = "下記の通り検収し、買い掛け金として貴口座に計上いたしましたのでご連絡申し上げます。";
                cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 12f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font08)) { FixedHeight = 12f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                tbl.AddCell(cell);

                doc.Add(tbl);

                List<KenshuClass.ZeirituShukei> lst = new List<KenshuClass.ZeirituShukei>();

                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    bool IsUpdate = false;
                    bool mKeigenZeirituFlg = dtD[i].KeigenZeirituFlg;
                    int mZeiritu = dtD[i].Zeiritu;
                    decimal mKingaku = Math.Round(dtD[i].Tanka * dtD[i].ChumonSuuryou, 0, MidpointRounding.AwayFromZero);
                    decimal mZeigaku = mKingaku * mZeiritu / 100;

                    if (lst.Count > 0)
                    {
                        for (int ix = 0; ix < lst.Count; ix++)
                        {
                            if (lst[ix].iZeiritu == mZeiritu && lst[ix].bKeigenZeirituFlg == mKeigenZeirituFlg)
                            {
                                IsUpdate = true;
                                lst[ix].iKingaku += (int)mKingaku;
                                lst[ix].iZeigaku += (int)mZeigaku;
                            }
                        }
                    }
                    if (!IsUpdate)
                    {
                        KenshuClass.ZeirituShukei m = new KenshuClass.ZeirituShukei();
                        m.iZeiritu = mZeiritu;
                        m.bKeigenZeirituFlg = mKeigenZeirituFlg;
                        m.iKingaku += (int)mKingaku;
                        m.iZeigaku += (int)mZeigaku;
                        lst.Add(m);
                    }
                }
                int nGoukei = 0;
                int nZeigaku = 0;
                for (int j = 0; j < lst.Count; j++)
                {
                    nGoukei += lst[j].iKingaku;
                    nZeigaku += lst[j].iZeigaku;
                }
                int nSouGoukei = nGoukei + nZeigaku;

                //明細
                tbl = new PdfPTable(9);
                tbl.SetTotalWidth(new float[] { 47, 57, 112, 54, 48, 60, 54, 66, 54 });
                cell = new PdfPCell(new Paragraph("発注No", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("品目コード", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("品目名", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("注文数量", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("単価", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("注文金額", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("納入場所", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("受入日", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("入荷数量", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);

                int LineCnt = 0;
                for (int i = 0; i < dtD.Count; i++)
                {
                    if (LineCnt >= MaxRow)
                    {
                        doc.Add(tbl);
                        doc.NewPage();
                        LineCnt = 1;
                        tbl = new PdfPTable(9);
                        tbl.SetTotalWidth(new float[] { 47, 57, 112, 54, 48, 60, 54, 66, 54 });
                        cell = new PdfPCell(new Paragraph("発注No", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("品目ｺｰﾄﾞ", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("品目名", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("注文数量", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("単価", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("注文金額", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("納入場所", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("受入日", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("入荷数量", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                    }
                    else
                    {
                        LineCnt++;
                    }

                    strTemp = dtD[i].HacchuuNo;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].BuhinCode;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    if (dtD[i].KeigenZeirituFlg)
                    {
                        strTemp = "* " + dtD[i].BuhinMei;
                    }
                    else
                    {
                        strTemp = "　" + dtD[i].BuhinMei;
                    }
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].ChumonSuuryou.ToString("#,##0");
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                    strTemp = string.Format("\\{0:#,##0.#0}", dtD[i].Tanka);
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                    strTemp = string.Format("\\{0:#,##0}", dtD[i].Kingaku);
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].BashoMei;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].NouhinBi.ToString("yyyy/MM/dd");
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].NouhinSuuryou.ToString("#,##0");
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                }

                if (LineCnt <= MaxRow)
                {
                    doc.Add(tbl);
                }

                //明細表描画終了
                tbl = new PdfPTable(2);
                tbl.SetTotalWidth(new float[] { 110, 450 });
                cell = new PdfPCell(new Paragraph("", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "* 軽減税率対象";
                cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                doc.Add(tbl);

                tbl = new PdfPTable(7);
                tbl.SetTotalWidth(new float[] { 140, 60, 100, 60, 100, 60, 100 });
                cell = new PdfPCell(new Paragraph("", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("合計", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                strTemp = string.Format("\\{0:#,##0}", nGoukei);
                cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("消費税", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                strTemp = string.Format("\\{0:#,##0}", nZeigaku);
                cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("総合計", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                strTemp = string.Format("\\{0:#,##0}", nSouGoukei);
                cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                tbl.AddCell(cell);
                doc.Add(tbl);

                //1事業所の編集終了
            }

            doc.Close();
            return stream;
        }

        internal static MemoryStream CreateOrderPDF(string sLoginID, HacchuDataSet_M.V_Hacchu2DataTable dtH)
        {
            //m2mKoubaiDataSet.M_ShiiresakiRow drShiire = ShiiresakiClass.getM_ShiiresakiRow(sKaishaCode, Global.GetConnection());
            //明細最大行数
            int MaxRow = 8;
            string strTemp = string.Empty;
            int intTemp = 0;

            // 仕入先配列を作成
            ArrayList aryShiire = new ArrayList();
            string strShiire = "";
            for (int i = 0; i < dtH.Rows.Count; i++)
            {
                if (dtH[i].ShiiresakiCode != strShiire)
                {
                    strShiire = dtH[i].ShiiresakiCode;
                    aryShiire.Add(dtH[i].ShiiresakiCode);
                }
            }
            //PDF の準備
            var doc = new Document(PageSize.A4);
            var stream = new MemoryStream();
            //ファイルの出力先を設定
            var pw = PdfWriter.GetInstance(doc, stream);
            //ドキュメントを開く
            doc.Open();
            PdfContentByte pdfContentByte = pw.DirectContent;

            string fontFolder = Environment.SystemDirectory.Replace("system32", "fonts");
            string fontName = fontFolder + @"\msgothic.ttc,0";
            BaseFont baseFont = BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //フォントサイズ
            float fontSize = 3.0f;
            //フォントとフォントサイズの指定
            pdfContentByte.SetFontAndSize(baseFont, fontSize);

            Font font = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));

            Font font08 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font08.Size = 8;
            Font font09 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font09.Size = 9;
            Font font10 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font10.Size = 10;
            Font font12 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font12.Size = 12;
            Font font14 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font14.Size = 14;

            //仕入先単位にページ編集
            for (int nShiireCnt = 0; nShiireCnt < aryShiire.Count; nShiireCnt++)
            {
                string strDC = aryShiire[nShiireCnt].ToString();

                DataRow[] drD = dtH.Select("ShiiresakiCode='" + strDC + "'");
                HacchuDataSet_M.V_Hacchu2DataTable dtD = new HacchuDataSet_M.V_Hacchu2DataTable();
                foreach (var drnew in drD)
                {
                    DataRow newrow = dtD.NewRow();
                    newrow.ItemArray = drnew.ItemArray;
                    dtD.Rows.Add(newrow);
                }
                //1仕入先の編集開始
                //ヘッダー部分

                PdfPTable tbl = new PdfPTable(1);
                strTemp = "発　注　書";
                PdfPCell cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                tbl = new PdfPTable(3);
                tbl.SetTotalWidth(new float[] { 200, 140, 200 });
                //1
                strTemp = "〒" + Utility.FormatYuubin(dtD[0].YubinBangou);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = DateTime.Today.ToString("yyyy年MM月dd日");
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //2
                strTemp = dtD[0].Address;
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = dtD[0].KaishaMei;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //3
                strTemp = dtD[0].ShiiresakiMei + " 御中";
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "TEL：" + Utility.FormatBanggo(dtD[0].TelH);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //4
                strTemp = "TEL：" + Utility.FormatBanggo(dtD[0].Tel);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "FAX：" + Utility.FormatBanggo(dtD[0].FaxH);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //5
                strTemp = "FAX：" + Utility.FormatBanggo(dtD[0].Fax);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "発注担当者：" + dtD[0].Name;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                tbl = new PdfPTable(2);
                tbl.SetTotalWidth(new float[] { 550, 10 });
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                strTemp = "いつも大変お世話になっております。";
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "下記発注致しますので、ご手配のほど宜しくお願い致します。";
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                tbl.AddCell(cell);

                doc.Add(tbl);

                int nGoukei = 0;
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    //nGoukei += (int)Math.Round(dtD[i].Tanka * dtD[i].Suuryou, 0, MidpointRounding.AwayFromZero);
                    nGoukei += dtD[i].Kingaku;
                }
                //int nSouGoukei = nGoukei + nZeigaku;

                //明細
                tbl = new PdfPTable(6);
                tbl.SetTotalWidth(new float[] { 64, 160, 64, 64, 32, 160 });
                cell = new PdfPCell(new Paragraph("発注No", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("品目ｺｰﾄﾞ", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("数量", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("合計", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                cell.Rowspan = 2;
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("単位", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                cell.Rowspan = 2;
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("納期", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("発注日", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("品目名", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("単価", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("納入場所", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);

                int LineCnt = 0;
                for (int i = 0; i < dtD.Count; i++)
                {
                    if (LineCnt >= MaxRow)
                    {
                        doc.Add(tbl);
                        doc.NewPage();
                        LineCnt = 1;
                        tbl = new PdfPTable(6);
                        tbl.SetTotalWidth(new float[] { 64, 160, 64, 64, 32, 160 });
                        cell = new PdfPCell(new Paragraph("発注No", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("品目ｺｰﾄﾞ", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("数量", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("合計", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        cell.Rowspan = 2;
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("単位", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        cell.Rowspan = 2;
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("納期", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("発注日", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("品目名", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("単価", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("納入場所", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                    }
                    else
                    {
                        LineCnt++;
                    }

                    strTemp = dtD[i].HacchuuNo;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].BuhinCode;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].Suuryou.ToString("#,##0");
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                    strTemp = string.Format("\\{0:#,##0}", dtD[i].Kingaku);
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    cell.Rowspan = 2;
                    tbl.AddCell(cell);
                    strTemp = dtD[i].Tani;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    cell.Rowspan = 2;
                    tbl.AddCell(cell);
                    intTemp = 0;
                    int.TryParse(dtD[i].Nouki, out intTemp);
                    strTemp = intTemp.ToString("0000/00/00");
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].HacchuuBi.ToString("yyyy/MM/dd");
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].BuhinMei;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT };
                    tbl.AddCell(cell);
                    strTemp = string.Format("\\{0:#,##0.#0}", dtD[i].Tanka);
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].BashoMei;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    cell = new PdfPCell(new Paragraph("備考", font08)) { FixedHeight = 28f, VerticalAlignment = Element.ALIGN_TOP, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].Bikou;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 28f, VerticalAlignment = Element.ALIGN_TOP, HorizontalAlignment = Element.ALIGN_LEFT };
                    cell.Colspan = 5;
                    tbl.AddCell(cell);

                }

                if (LineCnt <= MaxRow)
                {
                    doc.Add(tbl);
                }
                //明細表描画終了

                tbl = new PdfPTable(3);
                tbl.SetTotalWidth(new float[] { 400, 60, 100 });
                cell = new PdfPCell(new Paragraph("", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("合計", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                strTemp = string.Format("\\{0:#,##0}", nGoukei);
                cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                tbl.AddCell(cell);
                doc.Add(tbl);

                //1仕入先の編集終了
            }

            doc.Close();
            return stream;
        }
        internal static MemoryStream CreateDeliveryPDF(string sLoginID, HacchuDataSet_M.V_Hacchu2DataTable dtH)
        {
            //明細最大行数
            int MaxRow = 20;
            string strTemp = string.Empty;

            // 仕入先配列を作成
            ArrayList aryShiire = new ArrayList();
            string strShiire = "";
            for (int i = 0; i < dtH.Rows.Count; i++)
            {
                if (dtH[i].ShiiresakiCode != strShiire)
                {
                    strShiire = dtH[i].ShiiresakiCode;
                    aryShiire.Add(dtH[i].ShiiresakiCode);
                }
            }
            //PDF の準備
            //var doc = new Document(PageSize.A4);
            var doc = new Document(PageSize.A4,12f,8f,36f,36f);
            var stream = new MemoryStream();
            //ファイルの出力先を設定
            var pw = PdfWriter.GetInstance(doc, stream);
            //ドキュメントを開く
            doc.Open();
            PdfContentByte pdfContentByte = pw.DirectContent;

            string fontFolder = Environment.SystemDirectory.Replace("system32", "fonts");
            string fontName = fontFolder + @"\msgothic.ttc,0";
            BaseFont baseFont = BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //フォントサイズ
            float fontSize = 3.0f;
            //フォントとフォントサイズの指定
            pdfContentByte.SetFontAndSize(baseFont, fontSize);

            Font font = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));

            Font font08 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font08.Size = 8;
            Font font09 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font09.Size = 9;
            Font font10 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font10.Size = 10;
            Font font12 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font12.Size = 12;
            Font font14 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font14.Size = 14;

            //仕入先単位にページ編集
            for (int nShiireCnt = 0; nShiireCnt < aryShiire.Count; nShiireCnt++)
            {
                string strDC = aryShiire[nShiireCnt].ToString();

                DataRow[] drD = dtH.Select("ShiiresakiCode='" + strDC + "'");
                HacchuDataSet_M.V_Hacchu2DataTable dtD = new HacchuDataSet_M.V_Hacchu2DataTable();
                foreach (var drnew in drD)
                {
                    DataRow newrow = dtD.NewRow();
                    newrow.ItemArray = drnew.ItemArray;
                    dtD.Rows.Add(newrow);
                }
                //1仕入先の編集開始
                //ヘッダー部分

                PdfPTable tbl = new PdfPTable(1);
                strTemp = "納　品　書";
                PdfPCell cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                tbl = new PdfPTable(3);
                tbl.SetTotalWidth(new float[] { 200, 140, 200 });
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                strTemp = DateTime.Today.ToString("yyyy年MM月dd日");
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //1
                strTemp = "〒" + Utility.FormatYuubin(dtD[0].YuubinH);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = dtD[0].ShiiresakiMei;
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //2
                strTemp = dtD[0].AddressH;
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "〒" + Utility.FormatYuubin(dtD[0].YubinBangou);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //3
                strTemp = dtD[0].KaishaMei + " 御中";
                cell = new PdfPCell(new Paragraph(strTemp, font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = dtD[0].Address;
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //4
                strTemp = "TEL：" + Utility.FormatBanggo(dtD[0].TelH);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "TEL：" + Utility.FormatBanggo(dtD[0].Tel);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                //5
                strTemp = "FAX：" + Utility.FormatBanggo(dtD[0].FaxH);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                strTemp = "FAX：" + Utility.FormatBanggo(dtD[0].Fax);
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                tbl = new PdfPTable(2);
                tbl.SetTotalWidth(new float[] { 100, 460 });
                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);

                tbl.AddCell(cell);
                strTemp = "毎度ありがとうございます下記の通り納品致しますのでご収査ください。";
                cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidth = 0 };
                tbl.AddCell(cell);

                cell = new PdfPCell(new Paragraph("", font12)) { FixedHeight = 16f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);

                doc.Add(tbl);

                int nGoukei = 0;
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    nGoukei += dtD[i].Kingaku;
                }

                //明細
                tbl = new PdfPTable(9);
                tbl.SetTotalWidth(new float[] { 50, 58, 60, 120, 32, 30, 48, 54, 120 });
                cell = new PdfPCell(new Paragraph("発注No", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("発注日", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("品目ｺｰﾄﾞ", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("品目名", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("数量", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("単位", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("単価", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("金額", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("  ", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);

                int LineCnt = 0;
                for (int i = 0; i < dtD.Count; i++)
                {
                    if (LineCnt >= MaxRow)
                    {
                        doc.Add(tbl);
                        doc.NewPage();
                        LineCnt = 1;
                        tbl = new PdfPTable(9);
                        tbl.SetTotalWidth(new float[] { 50, 58, 60, 120, 30, 30, 44, 50, 120 });
                        cell = new PdfPCell(new Paragraph("発注No", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("発注日", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("品目ｺｰﾄﾞ", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("品目名", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("数量", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("単位", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("単価", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("金額", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                        cell = new PdfPCell(new Paragraph("  ", font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                        tbl.AddCell(cell);
                    }
                    else
                    {
                        LineCnt++;
                    }

                    strTemp = dtD[i].HacchuuNo;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].HacchuuBi.ToString("yyyy/MM/dd");
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].BuhinCode;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].BuhinMei;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].Suuryou.ToString("#,##0");
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                    strTemp = dtD[i].Tani;
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                    strTemp = string.Format("\\{0:#,##0}", dtD[i].Tanka);
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                    strTemp = string.Format("\\{0:#,##0}", dtD[i].Kingaku);
                    cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 24f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                    tbl.AddCell(cell);
                    strTemp = "http://localhost:51122/BarCode/BarCodeForm.aspx?BarCode=" + dtD[i].HacchuuNo;
                    iTextSharp.text.Image imageTemp = iTextSharp.text.Image.GetInstance(strTemp);
                    cell = new PdfPCell(imageTemp,true) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                    tbl.AddCell(cell);
                }

                if (LineCnt <= MaxRow)
                {
                    doc.Add(tbl);
                }
                //明細表描画終了

                tbl = new PdfPTable(4);
                tbl.SetTotalWidth(new float[] { 280, 60, 100,120 });
                cell = new PdfPCell(new Paragraph("", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("合計", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
                tbl.AddCell(cell);
                strTemp = string.Format("\\{0:#,##0}", nGoukei);
                cell = new PdfPCell(new Paragraph(strTemp, font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
                tbl.AddCell(cell);
                cell = new PdfPCell(new Paragraph("", font08)) { FixedHeight = 14f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
                tbl.AddCell(cell);
                doc.Add(tbl);

                //1仕入先の編集終了
            }

            doc.Close();
            return stream;
        }

        internal abstract class AbstractLineDash : ILineDash
        {
            private float lineWidth;
            public AbstractLineDash(float lineWidth) => this.lineWidth = lineWidth;
            public virtual void ApplyLineDash(PdfContentByte canvas) => canvas.SetLineWidth(lineWidth);
        }

        // 普通の
        internal class SolidLine : AbstractLineDash
        {
            public SolidLine(float lineWidth) : base(lineWidth) { }
        }

        internal interface ILineDash
        {
            void ApplyLineDash(PdfContentByte canvas);
        }

        internal class CustomBorder : IPdfPCellEvent
        {
            protected ILineDash left;
            protected ILineDash right;
            protected ILineDash top;
            protected ILineDash bottom;
            public CustomBorder(ILineDash left, ILineDash right, ILineDash top, ILineDash bottom)
            {
                this.left = left;
                this.right = right;
                this.top = top;
                this.bottom = bottom;
            }

            public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
            {
                var canvas = canvases[PdfPTable.LINECANVAS];
                if (top != null)
                {
                    canvas.SaveState();
                    top.ApplyLineDash(canvas);
                    canvas.MoveTo(position.Right, position.Top);
                    canvas.LineTo(position.Left, position.Top);
                    canvas.Stroke();
                    canvas.RestoreState();
                }
                if (bottom != null)
                {
                    canvas.SaveState();
                    bottom.ApplyLineDash(canvas);
                    canvas.MoveTo(position.Right, position.Bottom);
                    canvas.LineTo(position.Left, position.Bottom);
                    canvas.Stroke();
                    canvas.RestoreState();
                }
                if (right != null)
                {
                    canvas.SaveState();
                    right.ApplyLineDash(canvas);
                    canvas.MoveTo(position.Right, position.Top);
                    canvas.LineTo(position.Right, position.Bottom);
                    canvas.Stroke();
                    canvas.RestoreState();
                }
                if (left != null)
                {
                    canvas.SaveState();
                    left.ApplyLineDash(canvas);
                    canvas.MoveTo(position.Left, position.Top);
                    canvas.LineTo(position.Left, position.Bottom);
                    canvas.Stroke();
                    canvas.RestoreState();
                }
            }
        }
    }
}