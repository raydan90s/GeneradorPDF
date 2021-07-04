using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Pdf.Helpers
{
    public static class TextHelper
    {
        public static IGenerator WithStyle(this IGenerator generator, Style style)
        {
            generator.CurrentStyle = style;
            return generator;
        }

        public static IGenerator Write(
          this IGenerator generator,
          string text,
          double x,
          double y)
        {
            if (generator.Expression)
                generator.Draw.DrawString(text ?? "", generator.CurrentStyle.Font, (XBrush)generator.CurrentStyle.Brush, x, y);
            return generator;
        }

        public static IGenerator WriteInBox(
          this IGenerator generator,
          string text,
          XParagraphAlignment alignment,
          double x,
          double y,
          double width,
          double height)
        {
            if (generator.Expression)
            {
                XTextFormatter xtextFormatter = new XTextFormatter(generator.Draw);
                xtextFormatter.Alignment = alignment;
                xtextFormatter.DrawString(text ?? "", generator.CurrentStyle.Font, (XBrush)generator.CurrentStyle.Brush, new XRect(x, y, width, height));
            }
            return generator;
        }

        public static IGenerator WriteInBox(
          this IGenerator generator,
          Decimal valor,
          double x,
          double y,
          double width,
          double height)
        {
            if (generator.Expression)
            {
                XTextFormatter xtextFormatter = new XTextFormatter(generator.Draw);
                xtextFormatter.Alignment = XParagraphAlignment.Right;
                xtextFormatter.DrawString(valor.ToString("0.00"), generator.CurrentStyle.Font, (XBrush)generator.CurrentStyle.Brush, new XRect(x, y, width, height));
            }
            return generator;
        }

        public static IGenerator WriteInBox(
          this IGenerator generator,
          long valor,
          double x,
          double y,
          double width,
          double height)
        {
            if (generator.Expression)
            {
                XTextFormatter xtextFormatter = new XTextFormatter(generator.Draw);
                xtextFormatter.Alignment = XParagraphAlignment.Right;
                xtextFormatter.DrawString(valor.ToString(), generator.CurrentStyle.Font, (XBrush)generator.CurrentStyle.Brush, new XRect(x, y, width, height));
            }
            return generator;
        }
    }
}
