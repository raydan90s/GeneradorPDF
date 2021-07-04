using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Pdf
{
    public class Rectangle
    {
        public GeneratorPage GeneratorPage { get; init; }

        public double X { get; init; }

        public double Y { get; init; }

        public double Width { get; init; }

        public Rectangle(GeneratorPage generatorPage, double x, double y, double width)
        {
            this.GeneratorPage = generatorPage;
            this.X = x;
            this.Y = y;
            this.Width = width;
        }
    }
}
