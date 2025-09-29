using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.BarCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Pdf.Helpers
{
    public static class DrawHelper
    {
        public static IGenerator WithPen(this IGenerator generator, XPen pen)
        {
            generator.CurrentPen = pen;
            return generator;
        }

        public static IGenerator RoundedRectangle(this IGenerator generator, XRect xRect)
        {
            if (generator.Expression)
                generator.Draw.DrawRoundedRectangle(generator.CurrentPen, xRect, new XSize(10.0, 10.0));
            return generator;
        }

        public static IGenerator Rectangle(this IGenerator generator, XRect xRect)
        {
            if (generator.Expression)
                generator.Draw.DrawRectangle(generator.CurrentPen, xRect);
            return generator;
        }

        public static IGenerator RectangleBegin(
          this IGenerator generator,
          double X,
          double Y,
          double width,
          out Yachasoft.Pdf.Rectangle rectangle)
        {
            rectangle = !generator.Expression ? (Yachasoft.Pdf.Rectangle)null : new Yachasoft.Pdf.Rectangle(generator.PagePointer.Clone(), X, Y, width);
            return generator;
        }

        public static IGenerator RectangleEnd(
          this IGenerator generator,
          Yachasoft.Pdf.Rectangle rectangle,
          double paddingBottom)
        {
            if (generator.Expression && rectangle != null)
            {
                generator.PointerY += paddingBottom;
                if (rectangle.GeneratorPage.Equals(generator.CurrentPage))
                    return generator.Rectangle(new XRect(rectangle.X, rectangle.Y, rectangle.Width, generator.PointerY - rectangle.Y));
                bool flag = false;
                foreach (GeneratorPage page in generator.Pages)
                {
                    if (page.Equals(rectangle.GeneratorPage))
                    {
                        flag = true;
                        page.CurrentGraphics.DrawLine(generator.CurrentPen, rectangle.X, rectangle.Y, rectangle.Width + rectangle.X, rectangle.Y);
                        page.CurrentGraphics.DrawLine(generator.CurrentPen, rectangle.X, rectangle.Y, rectangle.X, page.Page.Height - page.MarginBottom);
                        page.CurrentGraphics.DrawLine(generator.CurrentPen, rectangle.Width + rectangle.X, rectangle.Y, rectangle.Width + rectangle.X, page.Page.Height - page.MarginBottom);
                    }
                    else
                    {
                        if (page.Equals(generator.CurrentPage))
                        {
                            page.CurrentGraphics.DrawLine(generator.CurrentPen, rectangle.X, page.MarginTop, rectangle.X, generator.PointerY);
                            page.CurrentGraphics.DrawLine(generator.CurrentPen, rectangle.Width + rectangle.X, page.MarginTop, rectangle.Width + rectangle.X, generator.PointerY);
                            page.CurrentGraphics.DrawLine(generator.CurrentPen, rectangle.X, generator.PointerY, rectangle.Width + rectangle.X, generator.PointerY);
                            break;
                        }
                        if (flag)
                        {
                            page.CurrentGraphics.DrawLine(generator.CurrentPen, rectangle.X, page.MarginTop, rectangle.X, page.Page.Height - page.MarginBottom);
                            page.CurrentGraphics.DrawLine(generator.CurrentPen, rectangle.Width + rectangle.X, page.MarginTop, rectangle.Width + rectangle.X, page.Page.Height - page.MarginBottom);
                        }
                    }
                }
            }
            return generator;
        }

        public static IGenerator WriteBarCode39(
          this IGenerator generator,
          string text,
          double X,
          double Y,
          XSize xSize)
        {
            if (generator.Expression)
            {
                Code3of9Standard code3of9Standard1 = new Code3of9Standard(text, xSize);
                ((BarCode)code3of9Standard1).StartChar = '*';
                ((BarCode)code3of9Standard1).EndChar = '*';
                Code3of9Standard code3of9Standard2 = code3of9Standard1;
                generator.Draw.DrawBarCode((BarCode)code3of9Standard2, (XBrush)XBrushes.Black, new XPoint(X, Y));
            }
            return generator;
        }

        public static IGenerator DrawImage(
          this IGenerator generator,
          string imageFile,
          double X,
          double Y,
          XSize xSize,
          bool stretch)
        {
            if (generator.Expression)
            {
                XImage ximage = XImage.FromFile(imageFile);
                if (!stretch)
                {
                    double num1 = (double)(ximage.PixelWidth * 72) / ximage.HorizontalResolution;
                    double num2 = (double)(ximage.PixelHeight * 72) / ximage.HorizontalResolution;
                    double num3 = xSize.Width / num1;
                    double num4 = xSize.Height / num2;
                    double num5 = num3 < num4 ? num3 : num4;
                    xSize.Width = (num1 * num5);
                    xSize.Height = (num2 * num5);
                }
                generator.Draw.DrawImage(ximage, X, Y, xSize.Width, xSize.Height);
            }
            return generator;
        }
    }
}
