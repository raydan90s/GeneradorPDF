// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Ride.Documentos.GuiaRemision_1_0_0
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

namespace Yachasoft.Sri.Ride.Documentos
{
  public class GuiaRemision_1_0_0
  {
    public string GenerarRIDE(
      GuiaRemision_1_0_0Modelo.GuiaRemision documento,
      string rutaArchivoAGenerar)
    {
      if (string.IsNullOrWhiteSpace(rutaArchivoAGenerar))
        rutaArchivoAGenerar = Path.Combine(Path.GetTempPath(), Path.GetTempFileName() + ".pdf");
      if (File.Exists(rutaArchivoAGenerar))
        throw new Exception("Archivo ya existe en la ruta asignada, elimínelo antes si quiere reemplazarlo");
      GuiaRemision_1_0_0.GenerarPdfDocument(documento).Save(rutaArchivoAGenerar);
      return rutaArchivoAGenerar;
    }

    public MemoryStream GenerarRIDE(GuiaRemision_1_0_0Modelo.GuiaRemision documento)
    {
      PdfDocument pdfDocument = GuiaRemision_1_0_0.GenerarPdfDocument(documento);
      MemoryStream memoryStream1 = new MemoryStream();
      MemoryStream memoryStream2 = memoryStream1;
      pdfDocument.Save((Stream) memoryStream2, false);
      return memoryStream1;
    }

    private static PdfDocument GenerarPdfDocument(
      GuiaRemision_1_0_0Modelo.GuiaRemision documento)
    {
      PdfDocument document = new PdfDocument();
      using (Generator generator = Generator.Instance(document))
      {
        generator.CrearCabeceraDocumento((Documento) documento).Rectangle(new XRect(30.0, 365.0, 540.0, 65.0)).EstiloNormal().Write("Identificación (Transportista)", 35.0, 380.0).Write(documento.Sujeto.Identificacion, 220.0, 380.0).Write("Razón Social / Nombres y Apellidos", 35.0, 390.0).Write(documento.Sujeto.RazonSocial, 220.0, 390.0).Write("Placa:", 35.0, 400.0).Write(documento.InfoGuiaRemision.Placa, 125.0, 400.0).Write("Fecha inicio", 35.0, 410.0).Write(documento.FechaEmision.ToSRIFecha(), 125.0, 410.0).Write("Fecha fin Transporte", 280.0, 410.0).Write(documento.InfoGuiaRemision.FechaFinTransporte.ToSRIFecha(), 370.0, 410.0).Write("Punto de partida:", 35.0, 420.0).Write(documento.InfoGuiaRemision.DireccionPartida, 125.0, 420.0).SetPointerY(430.0);
        foreach (GuiaRemision_1_0_0Modelo.Destinatario destinatario in documento.Destinatarios)
        {
          Rectangle rectangle;
          generator.RectangleBegin(30.0, generator.PointerY + 10.0, 540.0, out rectangle).WithTable(35.0, generator.PointerY + 15.0, new List<double>()
          {
            180.0,
            180.0,
            90.0,
            85.0
          }, false).AddRow().AddCell("Comprobante de Venta:", (XParagraphAlignment) 1).AddCell(string.Format("{0}  {1}", (object) destinatario.DocumentoSustento.CodDocumento.ObtenerSRIDescripcion().ToUpper(), (object) destinatario.DocumentoSustento.NumDocumento), (XParagraphAlignment) 1).AddCell("Fecha de Emisión:", (XParagraphAlignment) 1).AddCell(destinatario.DocumentoSustento.FechaEmisionDocumento.ToSRIFecha()).AddRow().AddCell("Número de Autorización:", (XParagraphAlignment) 1).AddCell(destinatario.NumAutDocumentoSustento, (XParagraphAlignment) 1).AddRow().AddCell("Motivo traslado:", (XParagraphAlignment) 1).AddCell(destinatario.MotivoTraslado, (XParagraphAlignment) 1).AddRow().AddCell("Destino(Punto de llegada):", (XParagraphAlignment) 1).AddCell(destinatario.DireccionDestinatario, (XParagraphAlignment) 1).AddRow().AddCell("Identificación (Destinatario):", (XParagraphAlignment) 1).AddCell(destinatario.SujetoDestinatario.Identificacion, (XParagraphAlignment) 1).AddRow().AddCell("Razón Social/Nombres apellidos:", (XParagraphAlignment) 1).AddCell(destinatario.SujetoDestinatario.RazonSocial, (XParagraphAlignment) 1).AddRow().AddCell("Documento aduanero:", (XParagraphAlignment) 1).AddCell(destinatario.DocAduaneroUnico, (XParagraphAlignment) 1).AddRow().AddCell("Código Establecimiento Destino:", (XParagraphAlignment) 1).AddCell(destinatario.CodEstablecimientoAduanero, (XParagraphAlignment) 1).AddRow().AddCell("Ruta:", (XParagraphAlignment) 1).AddCell(destinatario.Ruta, (XParagraphAlignment) 1);
          generator.WithTable(45.0, generator.PointerY + 10.0, new List<double>()
          {
            62.0,
            190.0,
            125.0,
            125.0
          }).AddRow().AddCell("Cantidad").AddCell("Descripción").AddCell("Código Principal").AddCell("Código Auxiliar");
          foreach (DetalleDocumentoItem detalle in destinatario.Detalles)
            generator.AddRow().AddCell(new int?(detalle.Cantidad)).AddCell(detalle.Item.Descripcion, (XParagraphAlignment) 1).AddCell(detalle.Item.CodigoPrincipal).AddCell(detalle.Item.CodigoAuxiliar);
          generator.RectangleEnd(rectangle, 10.0);
        }
        generator.CrearInfoAdicional((Documento) documento);
      }
      return document;
    }
  }
}
