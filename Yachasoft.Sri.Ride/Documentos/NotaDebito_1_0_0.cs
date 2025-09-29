// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Ride.Documentos.NotaDebito_1_0_0
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
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Yachasoft.Sri.Ride.Documentos
{
  public class NotaDebito_1_0_0
  {
    public string GenerarRIDE(
      NotaDebito_1_0_0Modelo.NotaDebito documento,
      string rutaArchivoAGenerar)
    {
      if (string.IsNullOrWhiteSpace(rutaArchivoAGenerar))
        rutaArchivoAGenerar = Path.Combine(Path.GetTempPath(), Path.GetTempFileName() + ".pdf");
      if (File.Exists(rutaArchivoAGenerar))
        throw new Exception("Archivo ya existe en la ruta asignada, elimínelo antes si quiere reemplazarlo");
      NotaDebito_1_0_0.GenerarPdfDocument(documento).Save(rutaArchivoAGenerar);
      return rutaArchivoAGenerar;
    }

    public MemoryStream GenerarRIDE(NotaDebito_1_0_0Modelo.NotaDebito documento)
    {
      PdfDocument pdfDocument = NotaDebito_1_0_0.GenerarPdfDocument(documento);
      MemoryStream memoryStream1 = new MemoryStream();
      MemoryStream memoryStream2 = memoryStream1;
      pdfDocument.Save((Stream) memoryStream2, false);
      return memoryStream1;
    }

    private static PdfDocument GenerarPdfDocument(
      NotaDebito_1_0_0Modelo.NotaDebito documento)
    {
      PdfDocument document = new PdfDocument();
      using (Generator generator = Generator.Instance(document))
      {
        generator.CrearCabeceraDocumento((Documento) documento).Rectangle(new XRect(30.0, 365.0, 540.0, 75.0)).EstiloNormal().Write("Razón Social / Nombres y Apellidos:", 35.0, 380.0).Write(documento.Sujeto.RazonSocial, 250.0, 380.0).Write("Identificación", 35.0, 390.0).Write(documento.Sujeto.Identificacion, 95.0, 390.0).Write("Fecha", 35.0, 400.0).Write(documento.FechaEmision.ToSRIFecha(), 95.0, 400.0).Write("Comprobante que se modifica", 35.0, 410.0).Write(string.Format("{0}   {1}", (object) documento.InfoNotaDebito.DocumentoModificado.CodDocumento.ObtenerSRIDescripcion(), (object) documento.InfoNotaDebito.DocumentoModificado.NumDocumento), 215.0, 410.0).Write("Fecha Emisión (Comprobante a modificar)", 35.0, 420.0).Write(documento.InfoNotaDebito.DocumentoModificado.FechaEmisionDocumento.ToSRIFecha(), 215.0, 420.0);
        generator.WithTable(30.0, 450.0, new List<double>()
        {
          330.0,
          210.0
        }, defaultRowHeight: 20.0).AddRow().EstiloTitulo().AddCell("RAZÓN DE LA MODIFICACIÓN").AddCell("VALOR DE LA MODIFICACIÓN").EstiloNormal();
        foreach (Motivo motivo in documento.Motivos)
          generator.AddRow().AddCell(motivo.Razon, (XParagraphAlignment) 1).AddCell(new Decimal?(motivo.Valor));
        GeneratorPage pagePointer;
        generator.GetPagePointer(out pagePointer);
        generator.CrearInfoAdicional((Documento) documento);
        generator.WithTable(30.0, generator.PointerY + 15.0, new List<double>()
        {
          150.0,
          60.0
        }, defaultRowHeight: 15.0).AddRow().AddCell("Forma de pago").AddCell("Valor");
        foreach (List<Pago> pago1 in documento.InfoNotaDebito.Pagos)
        {
          foreach (Pago pago2 in pago1)
            generator.AddRow().AddCell(pago2.FormaPago.ObtenerSRIDescripcion().ToUpper()).AddCell(new Decimal?(pago2.Total));
        }
        Decimal num1 = Convert.ToDecimal((object) documento.TipoImpuestoIVAVigente.ObtenerSRIPorcentaje());
        ImpuestoVenta impuestoVenta1 = documento.InfoNotaDebito.Impuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.Tarifa > 0M)).FirstOrDefault<ImpuestoVenta>();
        Decimal num2 = impuestoVenta1 != null ? impuestoVenta1.Tarifa : num1;
        List<ImpuestoVenta> impuestos1 = documento.InfoNotaDebito.Impuestos;
        ImpuestoVenta impuestoVenta2 = impuestos1 != null ? impuestos1.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA._0)).FirstOrDefault<ImpuestoVenta>() : (ImpuestoVenta) null;
        List<ImpuestoVenta> impuestos2 = documento.InfoNotaDebito.Impuestos;
        ImpuestoVenta impuestoVenta3 = impuestos2 != null ? impuestos2.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA.NoObjetoImpuesto)).FirstOrDefault<ImpuestoVenta>() : (ImpuestoVenta) null;
        List<ImpuestoVenta> impuestos3 = documento.InfoNotaDebito.Impuestos;
        ImpuestoVenta impuestoVenta4 = impuestos3 != null ? impuestos3.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA.ExentoIVA)).FirstOrDefault<ImpuestoVenta>() : (ImpuestoVenta) null;
        List<ImpuestoVenta> impuestos4 = documento.InfoNotaDebito.Impuestos;
        ImpuestoVenta impuestoVenta5 = impuestos4 != null ? impuestos4.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.Tarifa > 0M)).FirstOrDefault<ImpuestoVenta>() : (ImpuestoVenta) null;
        List<ImpuestoVenta> impuestos5 = documento.InfoNotaDebito.Impuestos;
        if (impuestos5 != null)
          impuestos5.Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>) (x => x.ValorDevolucionIVA));
        List<ImpuestoVenta> impuestos6 = documento.InfoNotaDebito.Impuestos;
        Decimal? valor1 = impuestos6 != null ? new Decimal?(impuestos6.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaICE)).Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>) (x => x.Valor))) : new Decimal?();
        List<ImpuestoVenta> impuestos7 = documento.InfoNotaDebito.Impuestos;
        Decimal? valor2 = impuestos7 != null ? new Decimal?(impuestos7.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIRBPNR)).Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>) (x => x.Valor))) : new Decimal?();
        generator.SetPagePointer(pagePointer).WithTable(390.0, generator.PointerY, new List<double>()
        {
          135.0,
          45.0
        }, defaultRowHeight: 15.0).AddRow().AddCell(string.Format("SUBTOTAL {0}%", (object) num2)).AddCell(impuestoVenta5?.BaseImponible).AddRow().AddCell("SUBTOTAL 0%").AddCell(impuestoVenta2?.BaseImponible).AddRow().AddCell("SUBTOTAL NO OBJETO DE IVA").AddCell(impuestoVenta3?.BaseImponible).AddRow().AddCell("SUBTOTAL EXCENTO DE IVA").AddCell(impuestoVenta4?.BaseImponible).AddRow().AddCell("SUBTOTAL SIN IMPUESTOS").AddCell(new Decimal?(documento.InfoNotaDebito.TotalSinImpuestos)).AddRow().AddCell("ICE").AddCell(valor1).AddRow().AddCell(string.Format("IVA {0}%", (object) num2)).AddCell(impuestoVenta5?.Valor).AddRow().AddCell("IRBPNR").AddCell(valor2).AddRow().AddCell("VALOR TOTAL").AddCell(new Decimal?(documento.InfoNotaDebito.ValorTotal));
      }
      return document;
    }
  }
}
