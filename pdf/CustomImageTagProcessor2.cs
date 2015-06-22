using iTextSharp.text;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdf
{
    public class CustomImageTagProcessor2 : iTextSharp.tool.xml.html.Image
    {
        public override IList<IElement> End(IWorkerContext ctx, Tag tag, IList<IElement> currentContent)
        {
            IDictionary<string, string> attributes = tag.Attributes;
            string src;
            if (!attributes.TryGetValue(HTML.Attribute.SRC, out src))
                return new List<IElement>(1);

            if (string.IsNullOrEmpty(src))
                return new List<IElement>(1);


            var image = iTextSharp.text.Image.GetInstance(Program.appRootDir + src);

                var list = new List<IElement>();
                var htmlPipelineContext = GetHtmlPipelineContext(ctx);
                list.Add(GetCssAppliers().Apply(new Chunk((iTextSharp.text.Image)GetCssAppliers().Apply(image, tag, htmlPipelineContext), 0, 0, true), tag, htmlPipelineContext));
                return list;
            
        }
    }
}
