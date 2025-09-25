// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Ride.Documentos.ComprobanteRetencion_1_0_0
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
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace Yachasoft.Sri.Ride.Documentos
{
  public class ComprobanteRetencion_1_0_0
  {
    public string GenerarRIDE(
      ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion documento,
      string rutaArchivoAGenerar)
    {
      if (string.IsNullOrWhiteSpace(rutaArchivoAGenerar))
        rutaArchivoAGenerar = Path.Combine(Path.GetTempPath(), Path.GetTempFileName() + ".pdf");
      if (File.Exists(rutaArchivoAGenerar))
        throw new Exception("Archivo ya existe en la ruta asignada, elimínelo antes si quiere reemplazarlo");
      ComprobanteRetencion_1_0_0.GenerarPdfDocument(documento).Save(rutaArchivoAGenerar);
      return rutaArchivoAGenerar;
    }

    public MemoryStream GenerarRIDE(
      ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion documento)
    {
      PdfDocument pdfDocument = ComprobanteRetencion_1_0_0.GenerarPdfDocument(documento);
      MemoryStream memoryStream1 = new MemoryStream();
      MemoryStream memoryStream2 = memoryStream1;
      pdfDocument.Save((Stream) memoryStream2, false);
      return memoryStream1;
    }

    private static PdfDocument GenerarPdfDocument(
      ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion documento)
    {
      PdfDocument document = new PdfDocument();
      using (Generator generator = Generator.Instance(document))
      {
        generator.CrearCabeceraDocumento((Documento) documento).Rectangle(new XRect(30.0, 365.0, 540.0, 45.0)).EstiloNormal().Write("Razón Social / Nombres y Apellidos:", 35.0, 380.0).Write(documento.Sujeto.RazonSocial, 250.0, 380.0).Write("Identificación", 35.0, 390.0).Write(documento.Sujeto.Identificacion, 95.0, 390.0).Write("Fecha", 35.0, 400.0).Write(documento.FechaEmision.ToSRIFecha(), 95.0, 400.0).EstiloNormal().WithTable(30.0, 420.0, new List<double>()
        {
          65.0,
          75.0,
          70.0,
          60.0,
          85.0,
          65.0,
          55.0,
          65.0
        }).AddRow(new double?(25.0)).AddCell("Comprobante").AddCell("Número").AddCell("Fecha Emisión").AddCell("Ejercicio\nFiscal").AddCell("Base imponible para\nla Retención").AddCell("IMPUESTO").AddCell("Porcentaje\nRetención").AddCell("Valor\nRetención");
        foreach (ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion impuesto in documento.Impuestos)
        {
          string text1 = impuesto.DocumentoSustento.CodDocumento.ObtenerSRIDescripcion();
          string text2 = "";
          if (impuesto is ComprobanteRetencion_1_0_0Modelo.ImpuestoRenta impuestoRenta)
            text2 = impuestoRenta.Codigo.ObtenerSRIDescripcion();
          if (impuesto is ComprobanteRetencion_1_0_0Modelo.ImpuestoIVA impuestoIva)
            text2 = impuestoIva.Codigo.ObtenerSRIDescripcion();
          if (impuesto is ComprobanteRetencion_1_0_0Modelo.ImpuestoISD impuestoIsd)
            text2 = impuestoIsd.Codigo.ObtenerSRIDescripcion();
          generator.AddRow(new double?(25.0)).AddCell(text1).AddCell(impuesto.DocumentoSustento.NumDocumento).AddCell(impuesto.DocumentoSustento.FechaEmisionDocumento.ToSRIFecha()).AddCell(documento.InfoCompRetencion.PeriodoFiscal).AddCell(new Decimal?(impuesto.BaseImponible)).AddCell(text2).AddCell(new Decimal?(impuesto.Tarifa)).AddCell(new Decimal?(impuesto.Valor));
        }
        generator.CrearInfoAdicional((Documento) documento);
      }
      return document;
    }
  }
}
