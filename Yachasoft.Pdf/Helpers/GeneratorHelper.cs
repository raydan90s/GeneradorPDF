using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Pdf.Helpers
{
    public static class GeneratorHelper
    {
        public static IGenerator Margins(
          this IGenerator generator,
          double top,
          double bottom)
        {
            generator.MarginTop = top;
            generator.MarginBottom = bottom;
            return generator;
        }

        public static IGenerator GetPagePointer(
          this IGenerator generator,
          out GeneratorPage pagePointer)
        {
            pagePointer = generator.PagePointer.Clone();
            return generator;
        }

        public static IGenerator SetPagePointer(
          this IGenerator generator,
          GeneratorPage pagePointer)
        {
            generator.PagePointer = pagePointer;
            return generator;
        }

        public static IGenerator SetPointerY(this IGenerator generator, double Y)
        {
            generator.PagePointer.PointY = Y;
            return generator;
        }

        public static IGenerator NewPage(this IGenerator generator)
        {
            generator.AddPage();
            return generator;
        }

        public static IGenerator If(this IGenerator generator, bool expression)
        {
            generator.Expression = expression;
            return generator;
        }

        public static IGenerator Endif(this IGenerator generator)
        {
            generator.Expression = true;
            return generator;
        }
    }
}
