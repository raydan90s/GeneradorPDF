// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Ride.Documentos.NotaCredito_1_0_0
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
using System.Linq;

namespace Yachasoft.Sri.Ride.Documentos
{
  public class NotaCredito_1_0_0
  {
    public string GenerarRIDE(
      NotaCredito_1_0_0Modelo.NotaCredito documento,
      string rutaArchivoAGenerar)
    {
      if (string.IsNullOrWhiteSpace(rutaArchivoAGenerar))
        rutaArchivoAGenerar = Path.Combine(Path.GetTempPath(), Path.GetTempFileName() + ".pdf");
      if (File.Exists(rutaArchivoAGenerar))
        throw new Exception("Archivo ya existe en la ruta asignada, elimínelo antes si quiere reemplazarlo");
      NotaCredito_1_0_0.GenerarPdfDocument(documento).Save(rutaArchivoAGenerar);
      return rutaArchivoAGenerar;
    }

    public MemoryStream GenerarRIDE(NotaCredito_1_0_0Modelo.NotaCredito documento)
    {
      PdfDocument pdfDocument = NotaCredito_1_0_0.GenerarPdfDocument(documento);
      MemoryStream memoryStream1 = new MemoryStream();
      MemoryStream memoryStream2 = memoryStream1;
      pdfDocument.Save((Stream)memoryStream2, false);
      return memoryStream1;
    }

    private static PdfDocument GenerarPdfDocument(
      NotaCredito_1_0_0Modelo.NotaCredito documento)
    {
      PdfDocument document = new PdfDocument();
      using (Generator generator = Generator.Instance(document))
      {
        generator.CrearCabeceraDocumento((Documento)documento).Rectangle(new XRect(30.0, 365.0, 540.0, 75.0)).EstiloNormal().Write("Razón Social / Nombres y Apellidos:", 35.0, 380.0).Write(documento.Sujeto.RazonSocial, 250.0, 380.0).Write("Identificación", 35.0, 390.0).Write(documento.Sujeto.Identificacion, 115.0, 390.0).Write("Fecha de emisión", 35.0, 400.0).Write(documento.FechaEmision.ToSRIFecha(), 115.0, 400.0).Write("Comprobante que se modifica", 35.0, 410.0).Write(string.Format("{0}   {1}", (object)documento.InfoNotaCredito.DocumentoModificado.CodDocumento.ObtenerSRIDescripcion(), (object)documento.InfoNotaCredito.DocumentoModificado.NumDocumento), 215.0, 410.0).Write("Fecha Emisión (Comprobante a modificar)", 35.0, 420.0).Write(documento.InfoNotaCredito.DocumentoModificado.FechaEmisionDocumento.ToSRIFecha(), 215.0, 420.0).Write("Razón de Modificación:", 35.0, 430.0).Write(documento.InfoNotaCredito.Motivo, 215.0, 430.0);
        double num1 = 110.0;
        generator.EstiloNormal().WithTable(30.0, 450.0, new List<double>()
        {
          45.0,
          45.0,
          45.0,
          145.0,
          num1,
          50.0,
          50.0,
          50.0
        }, defaultRowHeight: 25.0).AddRow().AddCell("Cod.\nPrincipal").AddCell("Cod.\nAuxiliar").AddCell("Cantidad").AddCell("Descripción").AddCell("Detalle Adicional").AddCell("Precio Unitario").AddCell("Descuento").AddCell("Precio Total");
        // Reemplazar el foreach en NotaCredito_1_0_0.cs con este código optimizado:

        foreach (DetalleDocumentoItemPrecio detalle in documento.Detalles)
        {
          // Preparar contenidos de cada celda
          string codigoPrincipal = detalle.Item.CodigoPrincipal ?? "";
          string codigoAuxiliar = detalle.Item.CodigoAuxiliar ?? "";
          string cantidad = detalle.Cantidad.ToString();
          string descripcion = detalle.Item.Descripcion ?? "";
          string detalleAdicional = detalle.DetallesAdicionales == null ? "" :
              string.Join('\n', detalle.DetallesAdicionales.Select(x => x.Nombre));

          // OPTIMIZACIÓN: Solo agregar salto de línea si realmente es necesario
          string codigoPrincipalModificado = codigoPrincipal;
          int lineasEnCodigo = 1;

          // Solo modificar códigos muy largos (ancho fijo de 45.0 en NotaCredito)
          double anchoCodigoPrincipal = 45.0;
          if (codigoPrincipal.Length > 9)
          {
            // Medir si realmente necesita salto de línea
            // Determinar cuántas líneas ocupa realmente el código
            XSize medidaTexto = generator.CurrentPage.CurrentGraphics.MeasureString(codigoPrincipal, generator.CurrentStyle.Font);
            int lineasCalculadas = (int)Math.Ceiling(medidaTexto.Width / (anchoCodigoPrincipal - 8.0));
            lineasEnCodigo = Math.Max(1, lineasCalculadas);

            // Si quieres que cada línea larga se transforme en "sub-fila" individual, puedes:
            List<string> lineasCodigo = new List<string>();
            int maxCharsPorLinea = 9; // o calcula dinámicamente según ancho
            for (int i = 0; i < codigoPrincipal.Length; i += maxCharsPorLinea)
            {
              lineasCodigo.Add(codigoPrincipal.Substring(i, Math.Min(maxCharsPorLinea, codigoPrincipal.Length - i)));
            }
            codigoPrincipalModificado = string.Join("\n", lineasCodigo);
            lineasEnCodigo = lineasCodigo.Count;

          }

          // Calcular líneas en detalles adicionales
          int lineasEnDetalleAdicional = 1;
          if (!string.IsNullOrEmpty(detalleAdicional) && detalleAdicional.Contains('\n'))
          {
            lineasEnDetalleAdicional = detalleAdicional.Split('\n').Length;
          }

          // Calcular líneas en descripción larga (ancho fijo de 145.0 en NotaCredito)
          int lineasEnDescripcion = 1;
          if (!string.IsNullOrEmpty(descripcion) && descripcion.Length > 35)
          {
            double anchoDescripcion = 145.0;
            XSize medidaDescripcion = generator.CurrentPage.CurrentGraphics.MeasureString(descripcion, generator.CurrentStyle.Font);
            if (medidaDescripcion.Width > (anchoDescripcion - 8.0))
            {
              lineasEnDescripcion = (int)Math.Ceiling(medidaDescripcion.Width / (anchoDescripcion - 8.0));
            }
          }

          // Calcular altura MÍNIMA necesaria basada en el contenido que más líneas requiere
          int maxLineas = Math.Max(Math.Max(lineasEnCodigo, lineasEnDescripcion), lineasEnDetalleAdicional);
          double alturaFila;

          if (maxLineas == 1)
          {
            alturaFila = 25.0; // Altura normal para una línea
          }
          else
          {
            // Fórmula consistente: 25px base + 6px por cada línea adicional
            alturaFila = 18.0 + ((maxLineas - 1) * 6.0);
          }

          System.Console.WriteLine($"NotaCredito - Código: '{codigoPrincipal}' -> '{codigoPrincipalModificado}', Líneas: {maxLineas}, Altura: {alturaFila}px");

          // Verificar nueva página
          generator.CheckEndOfPage(alturaFila);

          // Agregar fila con altura optimizada usando el código principal modificado
          generator.AddRow(new double?(alturaFila))
              .AddCell(codigoPrincipalModificado)  // Usar el código modificado
              .AddCell(codigoAuxiliar)
              .AddCell(new int?(detalle.Cantidad))
              .AddCell(descripcion)
              .AddCell(detalleAdicional)
              .AddCell(new Decimal?(detalle.PrecioUnitario))
              .AddCell(new Decimal?(detalle.Descuento))
              .AddCell(new Decimal?(detalle.PrecioTotalSinImpuesto));
        }
        GeneratorPage pagePointer;
        generator.GetPagePointer(out pagePointer).CrearInfoAdicional((Documento)documento);
        Decimal num3 = Convert.ToDecimal((object)documento.TipoImpuestoIVAVigente.ObtenerSRIPorcentaje());
        ImpuestoVenta impuestoVenta1 = documento.InfoNotaCredito.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>)(x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.Tarifa > 0M)).FirstOrDefault<ImpuestoVenta>();
        Decimal num4 = impuestoVenta1 != null ? impuestoVenta1.Tarifa : num3;
        ImpuestoVenta impuestoVenta2 = documento.InfoNotaCredito.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>)(x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA._0)).FirstOrDefault<ImpuestoVenta>();
        ImpuestoVenta impuestoVenta3 = documento.InfoNotaCredito.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>)(x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA.NoObjetoImpuesto)).FirstOrDefault<ImpuestoVenta>();
        ImpuestoVenta impuestoVenta4 = documento.InfoNotaCredito.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>)(x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA.ExentoIVA)).FirstOrDefault<ImpuestoVenta>();
        ImpuestoVenta impuestoVenta5 = documento.InfoNotaCredito.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>)(x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.Tarifa > 0M)).FirstOrDefault<ImpuestoVenta>();
        documento.InfoNotaCredito.TotalConImpuestos.Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>)(x => x.ValorDevolucionIVA));
        Decimal num5 = documento.InfoNotaCredito.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>)(x => x is ImpuestoVentaICE)).Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>)(x => x.Valor));
        Decimal num6 = documento.InfoNotaCredito.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>)(x => x is ImpuestoVentaIRBPNR)).Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>)(x => x.Valor));
        Decimal num7 = documento.Detalles.Sum<DetalleDocumentoItemPrecio>((Func<DetalleDocumentoItemPrecio, Decimal>)(x => x.Descuento));
        generator.SetPagePointer(pagePointer).WithTable(390.0, generator.PointerY, new List<double>()
        {
          135.0,
          45.0
        }, defaultRowHeight: 15.0).AddRow().AddCell(string.Format("SUBTOTAL {0}%", (object)num4)).AddCell(impuestoVenta5?.BaseImponible).AddRow().AddCell("SUBTOTAL 0%").AddCell(impuestoVenta2?.BaseImponible).AddRow().AddCell("SUBTOTAL NO OBJETO DE IVA").AddCell(impuestoVenta3?.BaseImponible).AddRow().AddCell("SUBTOTAL EXCENTO DE IVA").AddCell(impuestoVenta4?.BaseImponible).AddRow().AddCell("SUBTOTAL SIN IMPUESTOS").AddCell(new Decimal?(documento.InfoNotaCredito.TotalSinImpuestos)).AddRow().AddCell("TOTAL DESCUENTO").AddCell(new Decimal?(num7)).AddRow().AddCell("ICE").AddCell(new Decimal?(num5)).AddRow().AddCell(string.Format("IVA {0}%", (object)num4)).AddCell(impuestoVenta5?.Valor).AddRow().AddCell("IRBPNR").AddCell(new Decimal?(num6)).AddRow().AddCell("VALOR TOTAL").AddCell(new Decimal?(documento.InfoNotaCredito.ValorModificacion));
      }
      return document;
    }
  }
}
