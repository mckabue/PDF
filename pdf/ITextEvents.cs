using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdf
{
    public class ITextEvents : PdfPageEventHelper
    {

        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        //DateTime PrintTime = DateTime.Now;
        string PrintTime;

        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = String.Format("{0}, {1} {2}, {3}", DateTime.UtcNow.DayOfWeek, DateTime.UtcNow.ToString("MMMM", CultureInfo.InvariantCulture), DateTime.UtcNow.Day.ToString(), DateTime.UtcNow.Year.ToString());
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {

            }
            catch (System.IO.IOException ioe)
            {

            }
        }

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);

            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

            Phrase p1Header = new Phrase("Sample Header Here", baseFontNormal);

            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);

            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1
           
            String text = "Page " + writer.PageNumber + " of ";


            
            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetRight(180), document.PageSize.GetBottom(30));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 12);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));
            }


            PdfPCell pdfCell1 = new PdfPCell();
            pdfCell1.Colspan = 2;
            //pdfCell1.BackgroundColor = BaseColor.RED;



            var link = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 12f, Font.UNDERLINE, iTextSharp.text.BaseColor.BLUE);
            var site = new Chunk("Examination Pastpaper Archive", link);
            site.SetAnchor("https://twitter.com/mckabue");
            var username = new Chunk("Charles", link);
            username.SetAnchor("https://twitter.com/mckabue");

            var info = new Phrase();
            info.Font = baseFontBig;
            info.Add("Downloaded from ");
            info.Add(site);
            info.Add(" on " + PrintTime + " at " + string.Format("{0:t}", DateTime.Now) + " by ");
            info.Add(username);

            PdfPCell pdfCell2 = new PdfPCell();
            pdfCell2.PaddingLeft = 10;
            pdfCell2.AddElement(info);
            
            //pdfCell2.BackgroundColor = BaseColor.GREEN;

            //set the alignment of all three cells and set border to 0
            pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;


            pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;





            //relative col widths in proportions - 1/3 and 2/3

            float[] widths = new float[] { 2f, 2f, 3f };
            pdfTab.SetWidths(widths);

            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell2.BorderWidthLeft = 1;

            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);

            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 70;
            //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;


            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
            //set pdfContent value

            //Move the pointer and draw line to separate header section from rest of page
            cb.MoveTo(40, document.PageSize.Height - 100);
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
            cb.Stroke();

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, document.PageSize.GetBottom(50));
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
            cb.Stroke();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();


        }
    }
}
