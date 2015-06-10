using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace pdf
{
    class Program
    {

        static void Main(string[] args)
        {
            var data = new data();
            var questions = data.Questions();

            var imageWaterMark = new ImageWaterMark();
            
            //HTMLQuestionsToPDF();

            //string html = "<span><b>bold</b> and <em>hardsome</em> plus image <img style=\" align:right; \" src=\"C:\\Users\\us1\\Documents\\Visual Studio 2013\\Projects\\pdf\\pdf\\WP_20150504_089.jpg\"/></span>";
            //questions.ForEach(q => iTextElement(q.Message));
            iTextElement();
            //imageWaterMark.WaterMark();
            Console.WriteLine("Done...");
            Console.Read();
        }



        public static void iTextElement()
	    {
            var data = new data();
            var questions = data.Questions();

            string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName + "/Contents/";



            var doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(appRootDir + "test.pdf", FileMode.Create));
            writer.PageEvent = new PDFWriterEvents("Background Text");


            List list = new List(List.ORDERED);

            doc.Open();



















            foreach (var question in questions)
            {
                var iteo = new iTextElementObjects();

                TextReader tr = new StringReader(question.Message);

		        XMLWorkerHelper.GetInstance().ParseXHtml(iteo, tr);

                var paragraph = new Paragraph();

                foreach (var element in iteo.elements)
                {
                    Console.WriteLine("element => " + element);
                    paragraph.Add(element);

                    /*
                    foreach (var chunk in element.Chunks)
                    {
                        doc.Add(chunk);
                        Console.WriteLine("chunk => " + chunk); 
                    }
                     */
                }

                list.Add(new ListItem(paragraph));
            }








            Paragraph fp = new Paragraph("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Suspendisse blandit blandit turpis. Nam in lectus ut dolor consectetuer bibendum. Morbi neque ipsum, laoreet id; dignissim et, viverra id, mauris. Nulla mauris elit, consectetuer sit amet, accumsan eget, congue ac, libero. Vivamus suscipit. Nunc dignissim consectetuer lectus. Fusce elit nisi; commodo non, facilisis quis, hendrerit eu, dolor? Suspendisse eleifend nisi ut magna. Phasellus id lectus! Vivamus laoreet enim et dolor. Integer arcu mauris, ultricies vel, porta quis, venenatis at, libero. Donec nibh est, adipiscing et, ullamcorper vitae, placerat at, diam. Integer ac turpis vel ligula rutrum auctor! Morbi egestas erat sit amet diam. Ut ut ipsum? Aliquam non sem. Nulla risus eros, mollis quis, blandit ut; luctus eget, urna. Vestibulum vestibulum dapibus erat. Proin egestas leo a metus?");
            fp.Alignment = Element.ALIGN_JUSTIFIED;
            
            Image jpg = Image.GetInstance(appRootDir + "WP_20150504_089.jpg");
            jpg.ScaleToFit(250f, 250f);
            jpg.Alignment = Image.TEXTWRAP | Image.ALIGN_RIGHT;
            jpg.IndentationLeft = 9f;
            jpg.SpacingAfter = 9f;
            jpg.BorderWidthTop = 100f;
            jpg.BorderColorTop = BaseColor.WHITE;


            

            doc.Add(jpg);
            doc.Add(fp);
            doc.NewPage();
            doc.Add(list);
            doc.Close();
	    }


        }
}
