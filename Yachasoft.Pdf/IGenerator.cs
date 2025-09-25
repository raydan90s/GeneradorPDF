using PdfSharpCore.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Pdf
{
    public interface IGenerator
    {
        void CheckEndOfPage(double alto);

        bool Expression { get; set; }

        void AddPage();

        List<GeneratorPage> Pages { get; }

        GeneratorPage CurrentPage { get; }

        XPen CurrentPen { get; set; }

        Style CurrentStyle { get; set; }

        XGraphics Draw { get; }

        Table CurrentTable { get; set; }

        double PointerY { get; set; }

        GeneratorPage PagePointer { get; set; }

        double MarginTop { get; set; }

        double MarginBottom { get; set; }
    }
}
