// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Ride.Documentos.LiquidacionCompra_1_0_0
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
  public class LiquidacionCompra_1_0_0
  {
    public string GenerarRIDE(
      LiquidacionCompra_1_0_0Modelo.LiquidacionCompra documento,
      string rutaArchivoAGenerar)
    {
      if (string.IsNullOrWhiteSpace(rutaArchivoAGenerar))
        rutaArchivoAGenerar = Path.Combine(Path.GetTempPath(), Path.GetTempFileName() + ".pdf");
      if (File.Exists(rutaArchivoAGenerar))
        throw new Exception("Archivo ya existe en la ruta asignada, elimínelo antes si quiere reemplazarlo");
      LiquidacionCompra_1_0_0.GenerarPdfDocument(documento).Save(rutaArchivoAGenerar);
      return rutaArchivoAGenerar;
    }

    public MemoryStream GenerarRIDE(
      LiquidacionCompra_1_0_0Modelo.LiquidacionCompra documento)
    {
      PdfDocument pdfDocument = LiquidacionCompra_1_0_0.GenerarPdfDocument(documento);
      MemoryStream memoryStream1 = new MemoryStream();
      MemoryStream memoryStream2 = memoryStream1;
      pdfDocument.Save((Stream) memoryStream2, false);
      return memoryStream1;
    }

    private static PdfDocument GenerarPdfDocument(
      LiquidacionCompra_1_0_0Modelo.LiquidacionCompra documento)
    {
      PdfDocument document = new PdfDocument();
      using (Generator generator = Generator.Instance(document))
      {
        generator.CrearCabeceraDocumento((Documento) documento).Rectangle(new XRect(30.0, 365.0, 540.0, 75.0)).EstiloNormal().Write("Razón Social / Nombres y Apellidos:", 35.0, 380.0).Write(documento.Sujeto.RazonSocial, 250.0, 380.0).Write("Identificación", 35.0, 390.0).Write(documento.Sujeto.Identificacion, 115.0, 390.0).Write("Fecha de emisión", 35.0, 400.0).Write(documento.FechaEmision.ToSRIFecha(), 115.0, 400.0).Write("Dirección", 35.0, 410.0).Write(documento.InfoLiquidacionCompra.DireccionProveedor, 115.0, 410.0);
        
        double num1 = 90.0;
        
        // ⬅️ CAMBIO: Última columna aumentada de 50.0 a 70.0
        generator.EstiloNormal().WithTable(30.0, 450.0, new List<double>()
        {
          45.0,
          45.0,
          45.0,
          145.0,
          num1,
          50.0,
          50.0,
          70.0  // ⬅️ CAMBIO: Era 50.0
        }, defaultRowHeight: 25.0).AddRow().AddCell("Cod.\nPrincipal").AddCell("Cod.\nAuxiliar").AddCell("Cantidad").AddCell("Descripción").AddCell("Detalle Adicional").AddCell("Precio Unitario").AddCell("Descuento").AddCell("Precio Total");
        
        foreach (DetalleDocumentoItemPrecio detalle in documento.Detalles)
        {
            Impuesto impuesto = detalle.Impuestos
                .Where(x => x is Yachasoft.Sri.Modelos.Base.ImpuestoIVA)
                .FirstOrDefault();
            if (impuesto != null)
            {
                decimal tarifa = impuesto.Tarifa;
            }

            // 🔹 Calcular altura según detalle adicional (ya existía)
            Generator generador = generator;
            double anchoDetalle = num1;
            List<CampoAdicional> detallesAdicionales = detalle.DetallesAdicionales;
            List<string> lineas = detallesAdicionales != null
                ? detallesAdicionales.ConvertAll(x => x.Nombre).ToList()
                : null;
            double alturaDetalle = (double)generador.CalcularLineas(anchoDetalle, lineas) * 12.5;

            // 🔹 PREPARAR campos
            string codigoPrincipal = detalle.Item.CodigoPrincipal ?? "";
            string codigoAuxiliar = detalle.Item.CodigoAuxiliar ?? "";
            string descripcion = detalle.Item.Descripcion ?? "";
            string detalleAdicional = detalle.DetallesAdicionales == null ? "" :
                string.Join('\n', detalle.DetallesAdicionales.Select(x => x.Nombre));

            // 🔹 AJUSTE dinámico del código principal (igual que en factura)
            string codigoPrincipalModificado = codigoPrincipal;
            int lineasCodigo = 1;

            if (codigoPrincipal.Length > 10)
            {
                try
                {
                    var medida = generator.CurrentPage.CurrentGraphics.MeasureString(
                        codigoPrincipal, generator.CurrentStyle.Font);

                    // si excede el ancho de la celda (45)
                    if (medida.Width > (45.0 - 8.0))
                    {
                        // cortar en un guion o a mitad del texto
                        int puntoCorte = codigoPrincipal.IndexOf('-');
                        if (puntoCorte <= 0 || puntoCorte >= codigoPrincipal.Length - 1)
                            puntoCorte = codigoPrincipal.Length / 2;

                        codigoPrincipalModificado = codigoPrincipal.Substring(0, puntoCorte) + "\n" +
                                                    codigoPrincipal.Substring(puntoCorte);
                        lineasCodigo = 2;
                    }
                }
                catch
                {
                    // fallback en caso de error de medición
                    if (codigoPrincipal.Length > 15)
                    {
                        int puntoCorte = codigoPrincipal.Length / 2;
                        codigoPrincipalModificado = codigoPrincipal.Substring(0, puntoCorte) + "\n" +
                                                    codigoPrincipal.Substring(puntoCorte);
                        lineasCodigo = 2;
                    }
                }
            }

            // 🔹 Calcular altura final basada en líneas de texto
            int lineasDescripcion = descripcion.Length > 35 ? (int)Math.Ceiling(descripcion.Length / 35.0) : 1;
            int lineasDetalleAdicional = detalleAdicional.Contains('\n') ? detalleAdicional.Split('\n').Length : 1;

            int maxLineas = Math.Max(lineasCodigo, Math.Max(lineasDescripcion, lineasDetalleAdicional));
            double alturaFila = 18.0 + ((maxLineas - 1) * 6.0);

            // 🔹 Crear fila ajustada
            generator.AddRow(new double?(alturaFila))
                .AddCell(codigoPrincipalModificado)
                .AddCell(codigoAuxiliar)
                .AddCell(new int?(detalle.Cantidad))
                .AddCell(descripcion)
                .AddCell(detalleAdicional)
                .AddCell(new decimal?(detalle.PrecioUnitario))
                .AddCell(new decimal?(detalle.Descuento))
                .AddCell(new decimal?(detalle.PrecioTotalSinImpuesto));
        }


        GeneratorPage pagePointer;
        generator.GetPagePointer(out pagePointer).CrearInfoAdicional((Documento) documento).CrearFormasPago(documento.InfoLiquidacionCompra.Pagos);
        Decimal num3 = Convert.ToDecimal((object) documento.TipoImpuestoIVAVigente.ObtenerSRIPorcentaje());
        ImpuestoVenta impuestoVenta1 = documento.InfoLiquidacionCompra.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.Tarifa > 0M)).FirstOrDefault<ImpuestoVenta>();
        Decimal num4 = impuestoVenta1 != null ? impuestoVenta1.Tarifa : num3;
        ImpuestoVenta impuestoVenta2 = documento.InfoLiquidacionCompra.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA._0)).FirstOrDefault<ImpuestoVenta>();
        ImpuestoVenta impuestoVenta3 = documento.InfoLiquidacionCompra.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA.NoObjetoImpuesto)).FirstOrDefault<ImpuestoVenta>();
        ImpuestoVenta impuestoVenta4 = documento.InfoLiquidacionCompra.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.CodigoPorcentaje == EnumTipoImpuestoIVA.ExentoIVA)).FirstOrDefault<ImpuestoVenta>();
        ImpuestoVenta impuestoVenta5 = documento.InfoLiquidacionCompra.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIVA impuestoVentaIva && impuestoVentaIva.Tarifa > 0M)).FirstOrDefault<ImpuestoVenta>();
        documento.InfoLiquidacionCompra.TotalConImpuestos.Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>) (x => x.ValorDevolucionIVA));
        Decimal num5 = documento.InfoLiquidacionCompra.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaICE)).Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>) (x => x.Valor));
        Decimal num6 = documento.InfoLiquidacionCompra.TotalConImpuestos.Where<ImpuestoVenta>((Func<ImpuestoVenta, bool>) (x => x is ImpuestoVentaIRBPNR)).Sum<ImpuestoVenta>((Func<ImpuestoVenta, Decimal>) (x => x.Valor));
        Decimal num7 = documento.Detalles.Sum<DetalleDocumentoItemPrecio>((Func<DetalleDocumentoItemPrecio, Decimal>) (x => x.Descuento));
        
        // ⬅️ CAMBIOS: Posición X ajustada para alinear con las últimas columnas
        // Cálculo: 30 (margen) + 45 + 45 + 45 + 145 + 90 = 400 (inicio de últimas 3 columnas)
        generator.SetPagePointer(pagePointer).WithTable(400.0, generator.PointerY, new List<double>()
        {
          100.0,  // ⬅️ Columna de etiquetas (ocupa espacio de "Precio Unitario" + "Descuento")
          70.0    // ⬅️ Columna de valores alineada con "Precio Total"
        }, defaultRowHeight: 15.0).AddRow().AddCell(string.Format("SUBTOTAL {0}%", (object) num4)).AddCell(impuestoVenta5?.BaseImponible).AddRow().AddCell("SUBTOTAL 0%").AddCell(impuestoVenta2?.BaseImponible).AddRow().AddCell("SUBTOTAL NO OBJETO DE IVA").AddCell(impuestoVenta3?.BaseImponible).AddRow().AddCell("SUBTOTAL EXCENTO DE IVA").AddCell(impuestoVenta4?.BaseImponible).AddRow().AddCell("SUBTOTAL SIN IMPUESTOS").AddCell(new Decimal?(documento.InfoLiquidacionCompra.TotalSinImpuestos)).AddRow().AddCell("TOTAL DESCUENTO").AddCell(new Decimal?(num7)).AddRow().AddCell("ICE").AddCell(new Decimal?(num5)).AddRow().AddCell(string.Format("IVA {0}%", (object) num4)).AddCell(impuestoVenta5?.Valor).AddRow().AddCell("IRBPNR").AddCell(new Decimal?(num6)).AddRow().AddCell("VALOR TOTAL").AddCell(new Decimal?(documento.InfoLiquidacionCompra.ImporteTotal));
      }
      return document;
    }
  }
}