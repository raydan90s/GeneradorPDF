using PdfSharpCore.Drawing.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Pdf.Helpers
{
    public static class TableHelper
    {
        public static IGenerator WithTable(
          this IGenerator generator,
          double X,
          double Y,
          List<double> columnsWidth,
          bool drawBorders = true,
          double defaultRowHeight = 10.0)
        {
            if (generator.Expression)
                generator.CurrentTable = new Table(generator, X, Y, columnsWidth)
                {
                    DrawBorders = drawBorders,
                    DefaultRowHeight = defaultRowHeight
                };
            return generator;
        }

        public static IGenerator AddRow(this IGenerator generator, double? height = null)
        {
            if (generator.Expression)
                generator.CurrentTable.AddRow(height);
            return generator;
        }

        public static IGenerator AddCell(
          this IGenerator generator,
          string text,
          XParagraphAlignment alignment = XParagraphAlignment.Center)
        {
            if (generator.Expression)
                generator.CurrentTable.CurrentRow.AddCell(text, alignment);
            return generator;
        }

        public static IGenerator AddCell(this IGenerator generator, Decimal? valor)
        {
            if (generator.Expression)
                generator.CurrentTable.CurrentRow.AddCell(valor);
            return generator;
        }

        public static IGenerator AddCell(this IGenerator generator, int? valor)
        {
            if (generator.Expression)
                generator.CurrentTable.CurrentRow.AddCell(valor);
            return generator;
        }
    }
}
