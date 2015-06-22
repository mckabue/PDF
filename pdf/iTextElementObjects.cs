using iTextSharp.text;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdf
{
    public class iTextElementObjects : IElementHandler
    {
        public List<IElement> elements = new List<IElement>();

        public void Add(IWritable w) {

                    if (w is WritableElement) {

                        elements.AddRange(((WritableElement)w).Elements());

                    }
                }
    }
}
