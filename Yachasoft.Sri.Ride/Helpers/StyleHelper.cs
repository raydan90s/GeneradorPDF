// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Ride.Helpers.StyleHelper
// Assembly: Yachasoft.Sri.Ride, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 1BF1C7CF-2C44-4106-9FF6-9D28D32C53F0
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.ride\1.1.5\lib\net5.0\Yachasoft.Sri.Ride.dll

using Yachasoft.Pdf;
using Yachasoft.Pdf.Helpers;
using PdfSharp.Drawing;

namespace Yachasoft.Sri.Ride.Helpers
{
  public static class StyleHelper
  {
    private static readonly Style NoLogoStyle = new Style(new XFont("Verdana", 25.0, (XFontStyle) 1), XBrushes.Red);
    private static readonly Style NormalStyle = new Style(new XFont("Verdana", 7.0, (XFontStyle) 0), XBrushes.Black);
    private static readonly Style NormalNegritaStyle = new Style(new XFont("Verdana", 7.0, (XFontStyle) 1), XBrushes.Black);
    private static readonly Style MicroStyle = new Style(new XFont("Verdana", 6.0, (XFontStyle) 0), XBrushes.Black);
    private static readonly Style TitleStyle = new Style(new XFont("Verdana", 12.0, (XFontStyle) 1), XBrushes.Black);

    public static IGenerator EstiloNoLogo(this IGenerator generator) => generator.WithStyle(StyleHelper.NoLogoStyle);

    public static IGenerator EstiloNormal(this IGenerator generator) => generator.WithStyle(StyleHelper.NormalStyle);

    public static IGenerator EstiloNormalNegrita(this IGenerator generator) => generator.WithStyle(StyleHelper.NormalNegritaStyle);

    public static IGenerator EstiloMicro(this IGenerator generator) => generator.WithStyle(StyleHelper.MicroStyle);

    public static IGenerator EstiloTitulo(this IGenerator generator) => generator.WithStyle(StyleHelper.TitleStyle);
  }
}
