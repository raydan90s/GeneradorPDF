using PdfSharpCore.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Pdf
{
    public class Style
    {
        public Style(XFont font, XSolidBrush brush)
        {
            this.Font = font;
            this.Brush = brush;
        }

        public XFont Font { get; }

        public XSolidBrush Brush { get; }
    }
}
