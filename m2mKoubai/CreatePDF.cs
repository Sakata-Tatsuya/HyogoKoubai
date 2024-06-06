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

namespace m2mKoubai
{
    public class CreatePDF
    {
        internal static MemoryStream CreateInvoicePDF(string sLoginID, string sKaishaCode, KenshuDataSet.V_KenshuDataTable dtD)
        {
            m2mKoubaiDataSet.M_ShiiresakiRow drShiire = ShiiresakiClass.getM_ShiiresakiRow(sKaishaCode, Global.GetConnection());

            //MasterDataSet.M_UserRow drU = MasterClass.GetM_UserRow(sLoginID, Global.GetConnection());
            //MasterDataSet.M_ZigyousyoRow drZ = MasterClass.GetM_ZigyousyoRow(sKaishaCode, Global.GetConnection());
            //明細最大行数
            int MaxRow = 20;

            string strTemp=string.Empty;

            var doc = new Document(PageSize.A4);
            var stream = new MemoryStream();
            //ファイルの出力先を設定
            var pw = PdfWriter.GetInstance(doc, stream);
            //ドキュメントを開く
            doc.Open();

            string fontFolder = Environment.SystemDirectory.Replace("system32", "fonts");
            string fontName = fontFolder + @"\msgothic.ttc,0";
            BaseFont baseFont = BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //フォントサイズ
            float fontSize = 3.0f;
            float fontSmall = 8;
            float fontS09 = 9;
            float fontS10 = 10;
            float fontS14 = 14;
            float fontS16 = 16;
            PdfContentByte pdfContentByte = pw.DirectContent;
            //フォントとフォントサイズの指定
            pdfContentByte.SetFontAndSize(baseFont, fontSize);

            //テキスト描画開始
            pdfContentByte.BeginText();

            Font font = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            Font fontsml = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            fontsml.Size = fontSmall;
            Font font09 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font09.Size = fontS09;
            Font font10 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font10.Size = fontS10;
            Font font14 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font14.Size = fontS14;
            Font font16 = new Font(BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED));
            font16.Size = fontS16;

            //備考
            PdfPTable tbl = new PdfPTable(1);
            tbl.SetTotalWidth(new float[] { 510 });

            //PdfPCell cell = new PdfPCell(new Paragraph("備考\n" + dtH[0].Remarks, font)) { FixedHeight = 70f, VerticalAlignment = Element.ALIGN_TOP, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cell = new PdfPCell(new Paragraph("備考\n")) { FixedHeight = 70f, VerticalAlignment = Element.ALIGN_TOP, HorizontalAlignment = Element.ALIGN_LEFT };
            tbl.AddCell(cell);
            tbl.WriteSelectedRows(0, -1, 50, 110, pw.DirectContent);

            //内訳
            //線
            var solid = new SolidLine(1);

            //MasterDataSet.M_TaxRateDataTable dtT = MasterClass.GetTaxRateList(Global.GetConnection());
            //InvoiceDataSet.DetailAmountDataTable dtTemp = new InvoiceDataSet.DetailAmountDataTable();

            ////消費税毎の合計額、合計税額を計算
            //for (int i = 0; i < dtT.Count; i++)
            //{
            //    int GridTotalAmount = 0;
            //    int GridTotalTax = 0;
            //    for (int c = 0; c < dtD.Count; c++)
            //    {
            //        if (dtD[c].RowType != "text")
            //        {
            //            if (dtT[i].TaxRate == dtD[c].TaxRate)
            //            {
            //                GridTotalAmount += dtD[c].Amount;
            //                GridTotalTax += dtD[c].Tax;
            //            }
            //        }
            //    }

            //    if (GridTotalAmount != 0)
            //    {
            //        InvoiceDataSet.DetailAmountRow drTemp = dtTemp.NewDetailAmountRow();
            //        drTemp.TaxRate = dtT[i].TaxRate;
            //        drTemp.TotalAmount = GridTotalAmount;
            //        drTemp.TotalTax = GridTotalTax;
            //        dtTemp.AddDetailAmountRow(drTemp);
            //    }
            //}

            tbl = new PdfPTable(3);
            tbl.SetTotalWidth(new float[] { 50, 100, 50 });

            //内訳フォント
            Font fontutiwake = new Font(baseFont, 8);

            //for (int i = 0; i < dtTemp.Count; i++)
            //{
            //    //1行目
            //    if (i == 0)
            //    {
            //        //税抜き金額
            //        cell = new PdfPCell(new Paragraph("内訳", fontutiwake)) { FixedHeight = 12f, VerticalAlignment = Element.ALIGN_BOTTOM, HorizontalAlignment = Element.ALIGN_LEFT };
            //        cell.Border = 0;
            //        // 左,右,上,下
            //        cell.CellEvent = new CustomBorder(solid, null, solid, null);
            //        tbl.AddCell(cell);
            //    }
            //    else
            //    {
            //        //税抜き金額
            //        cell = new PdfPCell(new Paragraph("", fontutiwake)) { FixedHeight = 12f, VerticalAlignment = Element.ALIGN_BOTTOM, HorizontalAlignment = Element.ALIGN_LEFT };
            //        cell.Border = 0;
            //        // 左,右,上,下
            //        cell.CellEvent = new CustomBorder(solid, null, null, null);
            //        tbl.AddCell(cell);
            //    }

            //    cell = new PdfPCell(new Paragraph(dtTemp[i].TaxRate + "対象(税抜)", fontutiwake)) { FixedHeight = 12f, VerticalAlignment = Element.ALIGN_BOTTOM, HorizontalAlignment = Element.ALIGN_LEFT };
            //    cell.Border = 0;
            //    // 左,右,上,下
            //    if (i == 0) //1行目
            //        cell.CellEvent = new CustomBorder(null, null, solid, null);
            //    else
            //        cell.CellEvent = new CustomBorder(null, null, null, null);
            //    tbl.AddCell(cell);

            //    cell = new PdfPCell(new Paragraph(String.Format("{0:#,0}", dtTemp[i].TotalAmount) + "円", fontutiwake)) { FixedHeight = 12f, VerticalAlignment = Element.ALIGN_BOTTOM, HorizontalAlignment = Element.ALIGN_RIGHT };
            //    cell.Border = 0;
            //    // 左,右,上,下
            //    if (i == 0)
            //        cell.CellEvent = new CustomBorder(null, solid, solid, null);
            //    else
            //        cell.CellEvent = new CustomBorder(null, solid, null, null);
            //    tbl.AddCell(cell);

            //    //消費税額
            //    cell = new PdfPCell(new Paragraph("", fontutiwake)) { FixedHeight = 12f, VerticalAlignment = Element.ALIGN_BOTTOM, HorizontalAlignment = Element.ALIGN_LEFT };
            //    cell.Border = 0;
            //    // 左,右,上,下
            //    if ((i == 0 && dtTemp.Count == 1) || (i + 1) == dtTemp.Count)
            //        cell.CellEvent = new CustomBorder(solid, null, null, solid);
            //    else if (i > 0 && (i + 1) != dtTemp.Count)
            //        cell.CellEvent = new CustomBorder(solid, null, null, null);
            //    else
            //        cell.CellEvent = new CustomBorder(solid, null, null, null);

            //    tbl.AddCell(cell);

            //    cell = new PdfPCell(new Paragraph(dtTemp[i].TaxRate + "消費税", fontutiwake)) { FixedHeight = 12f, VerticalAlignment = Element.ALIGN_BOTTOM, HorizontalAlignment = Element.ALIGN_LEFT };
            //    cell.Border = 0;
            //    // 左,右,上,下
            //    if ((i == 0 && dtTemp.Count == 1) || (i + 1) == dtTemp.Count)
            //        cell.CellEvent = new CustomBorder(null, null, null, solid);
            //    else if (i > 0 && (i + 1) != dtTemp.Count)
            //        cell.CellEvent = new CustomBorder(null, null, null, null);
            //    tbl.AddCell(cell);

            //    cell = new PdfPCell(new Paragraph(String.Format("{0:#,0}", dtTemp[i].TotalTax) + "円", fontutiwake)) { FixedHeight = 12f, VerticalAlignment = Element.ALIGN_BOTTOM, HorizontalAlignment = Element.ALIGN_RIGHT };
            //    cell.Border = 0;
            //    // 左,右,上,下
            //    if ((i == 0 && dtTemp.Count == 1) || (i + 1) == dtTemp.Count)
            //        cell.CellEvent = new CustomBorder(null, solid, null, solid);
            //    else if (i > 0 && (i + 1) != dtTemp.Count)
            //        cell.CellEvent = new CustomBorder(null, solid, null, null);
            //    else
            //        cell.CellEvent = new CustomBorder(null, solid, null, null);
            //    tbl.AddCell(cell);
            //}
            tbl.WriteSelectedRows(0, -1, 350, doc.PageSize.Height - 670, pw.DirectContent);


            //明細
            //tbl.WriteSelectedRows(0, -1, 350, 150 + 15 * dtTemp.Columns.Count, pw.DirectContent);
            tbl = new PdfPTable(4);

            tbl.SetTotalWidth(new float[] { 250, 80, 80, 100 });

            //明細ヘッダー
            cell = new PdfPCell(new Paragraph("摘要", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            tbl.AddCell(cell);
            cell = new PdfPCell(new Paragraph("数量", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            tbl.AddCell(cell);
            cell = new PdfPCell(new Paragraph("単価", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            tbl.AddCell(cell);
            cell = new PdfPCell(new Paragraph("明細金額", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            tbl.AddCell(cell);

            //明細全体のトータル金額、消費税
            int TotalAmount = 0;
            int TotalTax = 0;

            //明細詳細
            //for (int i = 0; i < dtD.Count; i++)
            //{
            //    if (dtD[i].RowType != "text")
            //    {
            //        TotalAmount += dtD[i].Amount;
            //        TotalTax += dtD[i].Tax;
            //    }

            //    if (dtD[i].RowType == "text")
            //    {
            //        cell = new PdfPCell(new Paragraph(dtD[i].Abstract, font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4 };
            //        tbl.AddCell(cell);
            //    }
            //    else
            //    {
            //        if (dtD[i].TaxRate.Contains("軽減税率"))
            //        {
            //            cell = new PdfPCell(new Paragraph(dtD[i].Abstract.ToString() + "※", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT };
            //            tbl.AddCell(cell);
            //        }
            //        else
            //        {
            //            cell = new PdfPCell(new Paragraph(dtD[i].Abstract.ToString(), font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT };
            //            tbl.AddCell(cell);
            //        }

            //        cell = new PdfPCell(new Paragraph(dtD[i].Quantity.ToString() + dtD[i].Unit, font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            //        tbl.AddCell(cell);

            //        cell = new PdfPCell(new Paragraph(String.Format("{0:#,0}", dtD[i].UnitPrice), font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
            //        tbl.AddCell(cell);

            //        cell = new PdfPCell(new Paragraph(String.Format("{0:#,0}", dtD[i].Amount), font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
            //        tbl.AddCell(cell);
            //    }
            //}

            ////1枚目の明細数が規定より少ない場合は空欄行を挿入
            //if (dtD.Count < FirstMaxRow)
            //{
            //    for (int i = 0; i < (FirstMaxRow - dtD.Count) * 4; i++)
            //    {
            //        cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE };
            //        tbl.AddCell(cell);
            //    }
            //}
            //tbl.WriteSelectedRows(0, -1, 50, 480, pw.DirectContent);

            //tbl = new PdfPTable(2);
            //tbl.SetTotalWidth(new float[] { 100, 200 });

            ////入金期日＆振込先
            //cell = new PdfPCell(new Paragraph("入金期日", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("振込先", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            //tbl.AddCell(cell);

            //if (!dtH[0].IsPaymentDateNull())
            //{
            //    cell = new PdfPCell(new Paragraph(dtH[0].PaymentDate.ToString("yyyy/MM/dd"), font)) { FixedHeight = 36f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            //    tbl.AddCell(cell);
            //}
            //else
            //{
            //    cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 36f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            //    tbl.AddCell(cell);
            //}
            //if (!dtH[0].IsFurikomiSakiNull())
            //{
            //    cell = new PdfPCell(new Paragraph(dtH[0].FurikomiSaki.ToString(), fontsml)) { FixedHeight = 36f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT };
            //    tbl.AddCell(cell);
            //}
            //else
            //{
            //    cell = new PdfPCell(new Paragraph("", fontsml)) { FixedHeight = 36f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT };
            //    tbl.AddCell(cell);
            //}
            //tbl.WriteSelectedRows(0, -1, 50, doc.PageSize.Height - 280, pw.DirectContent);

            ////小計＆消費税＆請求金額
            //tbl = new PdfPTable(3);
            //tbl.SetTotalWidth(new float[] { 100, 100, 200 });

            //cell = new PdfPCell(new Paragraph("小計", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("消費税", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("請求金額", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER };
            //tbl.AddCell(cell);

            //cell = new PdfPCell(new Paragraph(String.Format("{0:#,0}", TotalAmount) + "円", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph(String.Format("{0:#,0}", TotalTax) + "円", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph(String.Format("{0:#,0}", (TotalAmount + TotalTax)) + "円", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_RIGHT };
            //tbl.AddCell(cell);
            //tbl.WriteSelectedRows(0, -1, 50, doc.PageSize.Height - 215, pw.DirectContent);

            ////ヘッダー部分
            //float w = doc.PageSize.Width / 2 + 100;
            ////int fontsize1 = 9;
            //tbl = new PdfPTable(3);
            //tbl.SetTotalWidth(new float[] { 200, 140, 200 });
            ////1
            //cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 32f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth=0 };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("請求書", font16)) { FixedHeight = 32f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 32f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
            //tbl.AddCell(cell);
            ////2
            //strTemp = dtH[0].CustomerName + " " + dtH[0].HonorificTitle;
            //cell = new PdfPCell(new Paragraph(strTemp, font14)) { FixedHeight = 30f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 30f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //strTemp = "請求日: " + dtH[0].BillingDate.ToString("yyyy/MM/dd");
            //cell = new PdfPCell(new Paragraph(strTemp, font09)) { FixedHeight = 30f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            ////3
            //cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //strTemp = "請求書番号: " + dtH[0].InvoiceID;
            //cell = new PdfPCell(new Paragraph(strTemp, font09)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            ////4
            //cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //strTemp = "登録番号: " + drZ.InvoiceRegNo;
            //cell = new PdfPCell(new Paragraph(strTemp, font09)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            ////5 空行
            //strTemp = string.Empty;
            //cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 18f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            ////6
            //strTemp = "件名　　" + dtH[0].SubjectName;
            //cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("", font)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //strTemp = drZ.ZigyousyoName;
            //cell = new PdfPCell(new Paragraph(strTemp, font10)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("", font09)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //cell = new PdfPCell(new Paragraph("", font09)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 };
            //tbl.AddCell(cell);
            //strTemp = "担当者 " + dtH[0].CompanyPIC;
            //cell = new PdfPCell(new Paragraph(strTemp, font09)) { FixedHeight = 20f, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            //tbl.AddCell(cell);

            tbl.WriteSelectedRows(0, -1, 50, doc.PageSize.Height - 30, pw.DirectContent);


            //テキスト描画終了
            pdfContentByte.EndText();


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