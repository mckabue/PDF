using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdf
{
    public class ImageWaterMark
    {
        public void WaterMark()
		{
            string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName + "/Contents/";

            //var doc = new Document();
            PdfReader reader = new PdfReader(appRootDir + "test.pdf");
            FileStream fileStream = new FileStream(appRootDir + "watermarked_test.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            PdfStamper stamper = new PdfStamper(reader, fileStream);

            PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);

            Rectangle rect = reader.GetPageSize(1);

            PdfContentByte cb = stamper.GetUnderContent(1);

            //doc.Open();
            cb.BeginLayer(layer);

            PdfGState gState = new PdfGState();
            gState.FillOpacity = 0.25f;
            cb.SetGState(gState);


            Image jpg = Image.GetInstance(appRootDir + "WP_20150504_089.jpg");
            jpg.SetAbsolutePosition(30f, 40f);
            

            cb.AddImage(jpg);

            // Close the layer
            cb.EndLayer();
            //doc.Close();
            Console.WriteLine("Watermark Done...");

        }
    }
}
