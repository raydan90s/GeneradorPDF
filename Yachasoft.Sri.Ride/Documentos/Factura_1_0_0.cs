// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Ride.Documentos.Factura_1_0_0
// Assembly: Yachasoft.Sri.Ride, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 1BF1C7CF-2C44-4106-9FF6-9D28D32C53F0
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.ride\1.1.5\lib\net5.0\Yachasoft.Sri.Ride.dll

using Yachasoft.Pdf;
using Yachasoft.Pdf.Helpers;
using Yachasoft.Sri.Core.Helpers;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Ride.Helpers;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Yachasoft.Sri.Ride.Documentos
{
  public class Factura_1_0_0
  {
    public string GenerarRIDE(Factura_1_0_0Modelo.Factura documento, string rutaArchivoAGenerar)
    {
      if (string.IsNullOrWhiteSpace(rutaArchivoAGenerar))
        rutaArchivoAGenerar = Path.Combine(Path.GetTempPath(), Path.GetTempFileName() + ".pdf");
      if (File.Exists(rutaArchivoAGenerar))
        throw new Exception("Archivo ya existe en la ruta asignada, elimínelo antes si quiere reemplazarlo");
      Factura_1_0_0.GenerarPdfDocument(documento).Save(rutaArchivoAGenerar);
      return rutaArchivoAGenerar;
    }

    public MemoryStream GenerarRIDE(Factura_1_0_0Modelo.Factura documento)
    {
      PdfDocument pdfDocument = Factura_1_0_0.GenerarPdfDocument(documento);
      MemoryStream memoryStream1 = new MemoryStream();
      MemoryStream memoryStream2 = memoryStream1;
      pdfDocument.Save((Stream) memoryStream2, false);
      return memoryStream1;
    }

    private static PdfDocument GenerarPdfDocument(Factura_1_0_0Modelo.Factura documento)
    {
      PdfDocument document = new PdfDocument();
      using (Generator generator = Generator.Instance(document))
      {
        generator.CrearCabeceraDocumento((Documento) documento).Rectangle(new XRect(30.0, 365.0, 540.0, 75.0)).EstiloNormal().Write("Razón Social / Nombres y Apellidos:", 35.0, 380.0).Write(documento.Sujeto.RazonSocial, 250.0, 380.0).Write("Identificación", 35.0, 390.0).Write(documento.Sujeto.Identificacion, 95.0, 390.0).Write("Fecha", 35.0, 400.0).Write(documento.FechaEmision.ToSRIFecha(), 95.0, 400.0).Write("Placa / Matrícula:", 180.0, 400.0).Write(documento.InfoFactura.Placa, 290.0, 400.0).Write("Guía", 380.0, 400.0).Write(documento.InfoFactura.GuiaRemision, 425.0, 400.0).Write("Dirección:", 35.0, 410.0).Write(documento.InfoFactura.DireccionComprador, 95.0, 410.0).EstiloNormal();
        bool flag = documento.Detalles.Exists((Predicate<DetalleDocumentoItemPrecioSubsidio>) (x => x.PrecioSinSubsidio > 0M));
        if (flag)
          generator.WithTable(30.0, 450.0, new List<double>()
          {
            35.0,
            35.0,
            35.0,
            120.0,
            90.0,
            45.0,
            45.0,
            45.0,
            45.0,
            45.0
          }, defaultRowHeight: 25.0).AddRow().AddCell("Cod.\nPrincipal").AddCell("Cod.\nAuxiliar").AddCell("Cantidad").AddCell("Descripción").AddCell("Detalle Adicional").AddCell("Precio Unitario").AddCell("Subsidio").AddCell("Precio sin\nSubsidio").AddCell("Descuento").AddCell("Precio Total");
        else
          generator.WithTable(30.0, 450.0, new List<double>()
          {
            40.0,
            40.0,
            40.0,
            140.0,
            130.0,
            50.0,
            50.0,
            50.0
          }, defaultRowHeight: 25.0).AddRow().AddCell("Cod.\nPrincipal").AddCell("Cod.\nAuxiliar").AddCell("Cantidad").AddCell("Descripción").AddCell("Detalle Adicional").AddCell("Precio Unitario").AddCell("Descuento").AddCell("Precio Total");
        Decimal num1 = 0M;
        Decimal num2 = 0M;
        foreach (DetalleDocumentoItemPrecioSubsidio detalle in documento.Detalles)
        {
          Impuesto impuesto = detalle.Impuestos.Where<Impuesto>((Func<Impuesto, bool>) (x => x is Yachasoft.Sri.Modelos.Base.ImpuestoIVA)).FirstOrDefault<Impuesto>();
          Decimal num3 = impuesto != null ? impuesto.Tarifa : 0M;
          num2 += Math.Round((Decimal) detalle.Cantidad * detalle.PrecioUnitario * (1M + num3 / 100M), 2);
          num1 += Math.Round((Decimal) detalle.Cantidad * (detalle.PrecioSinSubsidio > 0M ? detalle.PrecioSinSubsidio : detalle.PrecioUnitario) * (1M + num3 / 100M), 2);
          generator.AddRow().AddCell(detalle.Item.CodigoPrincipal).AddCell(detalle.Item.CodigoAuxiliar).AddCell(new int?(detalle.Cantidad)).AddCell(detalle.Item.Descripcion).AddCell(detalle.DetallesAdicionales == null ? "" : string.Join<string>('\n', detalle.DetallesAdicionales.Select<CampoAdicional, string>((Func<CampoAdicional, string>) (x => x.Nombre + " " + x.Valor)))).AddCell(new Decimal?(detalle.PrecioUnitario));
          if (flag)
            generator.AddCell(new Decimal?(detalle.PrecioSinSubsidio == 0M ? 0M : detalle.PrecioSinSubsidio - detalle.PrecioUnitario)).AddCell(new Decimal?(detalle.PrecioSinSubsidio));
          generator.AddCell(new Decimal?(detalle.Descuento)).AddCell(new Decimal?(detalle.PrecioTotalSinImpuesto));
        }
        GeneratorPage pagePointer;
        generator.GetPagePointer(out pagePointer).CrearInfoAdicional((Documento) documento).CrearFormasPago(documento.InfoFactura.Pagos);
        documento.InfoFactura.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA)).Select<ImpuestoVenta, ImpuestoVentaIVA>((Func<ImpuestoVenta, ImpuestoVentaIVA>) (x => (ImpuestoVentaIVA) x));
        Decimal num4 = Convert.ToDecimal((object) documento.TipoImpuestoIVAVigente.ObtenerSRIPorcentaje());
        ImpuestoVenta impuestoVenta1 = documento.InfoFactura.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.Tarifa > 0M)).FirstOrDefault<ImpuestoVenta>();
        Decimal num5 = impuestoVenta1 != null ? impuestoVenta1.Tarifa : num4;
        ImpuestoVenta impuestoVenta2 = documento.InfoFactura.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA._0)).FirstOrDefault<ImpuestoVenta>();
        ImpuestoVenta impuestoVenta3 = documento.InfoFactura.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA.NoObjetoImpuesto)).FirstOrDefault<ImpuestoVenta>();
        ImpuestoVenta impuestoVenta4 = documento.InfoFactura.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA.ExentoIVA)).FirstOrDefault<ImpuestoVenta>();
        ImpuestoVenta impuestoVenta5 = documento.InfoFactura.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.Tarifa > 0M)).FirstOrDefault<ImpuestoVenta>();
        Decimal num6 = documento.InfoFactura.TotalConImpuestos.Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>) (x => x.ValorDevolucionIVA));
        Decimal num7 = documento.InfoFactura.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaICE)).Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>) (x => x.Valor));
        Decimal num8 = documento.InfoFactura.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIRBPNR)).Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>) (x => x.Valor));
        generator.SetPagePointer(pagePointer).WithTable(390.0, generator.PointerY, new List<double>()
        {
          135.0,
          45.0
        }, defaultRowHeight: 15.0).AddRow().AddCell(string.Format("SUBTOTAL {0}%", (object) num5)).AddCell(impuestoVenta5?.BaseImponible).AddRow().AddCell("SUBTOTAL 0%").AddCell(impuestoVenta2?.BaseImponible).AddRow().AddCell("SUBTOTAL NO OBJETO DE IVA").AddCell(impuestoVenta3?.BaseImponible).AddRow().AddCell("SUBTOTAL EXCENTO DE IVA").AddCell(impuestoVenta4?.BaseImponible).AddRow().AddCell("SUBTOTAL SIN IMPUESTOS").AddCell(new Decimal?(documento.InfoFactura.TotalSinImpuestos)).AddRow().AddCell("TOTAL DESCUENTO").AddCell(new Decimal?(documento.InfoFactura.TotalDescuento)).AddRow().AddCell("ICE").AddCell(new Decimal?(num7)).AddRow().AddCell(string.Format("IVA {0}%", (object) num5)).AddCell(impuestoVenta5?.Valor).AddRow().AddCell("TOTAL DEVOLUCION IVA").AddCell(new Decimal?(num6)).AddRow().AddCell("IRBPNR").AddCell(new Decimal?(num8)).AddRow().AddCell("PROPINA").AddCell(new Decimal?(documento.InfoFactura.Propina)).AddRow().AddCell("VALOR TOTAL").AddCell(new Decimal?(documento.InfoFactura.ImporteTotal));
        if (flag)
          generator.WithTable(390.0, generator.PointerY + 5.0, new List<double>()
          {
            135.0,
            45.0
          }).AddRow(new double?(15.0)).AddCell("VALOR TOTAL SIN SUBSIDIO").AddCell(new Decimal?(num1)).AddRow(new double?(30.0)).AddCell("AHORRO POR SUBSIDIO:\n(Incluye IVA cuando corresponda)").AddCell(new Decimal?(num1 - num2));
      }
      return document;
    }
  }
}
