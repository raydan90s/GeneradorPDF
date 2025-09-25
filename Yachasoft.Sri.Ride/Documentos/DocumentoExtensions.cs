// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Ride.Documentos.DocumentoExtensions
// Assembly: Yachasoft.Sri.Ride, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 1BF1C7CF-2C44-4106-9FF6-9D28D32C53F0
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.ride\1.1.5\lib\net5.0\Yachasoft.Sri.Ride.dll

using Yachasoft.Pdf;
using Yachasoft.Pdf.Helpers;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Ride.Helpers;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using Yachasoft.Sri.Modelos.Extensions;

namespace Yachasoft.Sri.Ride.Documentos
{
  public static class DocumentoExtensions
  {
    public static IGenerator CrearCabeceraDocumento(
      this IGenerator generador,
      Documento documento)
    {
      Emisor emisor = documento.PuntoEmision.Establecimiento.Emisor;
      generador.NewPage();
      try
      {
        if (string.IsNullOrWhiteSpace(emisor.Logo))
          throw new Exception();
        generador.DrawImage(emisor.Logo, 30.0, 25.0, new XSize(260.0, 135.0), false);
      }
      catch //(Exception ex)
      {
        generador.EstiloNoLogo().Write("NO TIENE LOGO", 40.0, 60.0);
      }
      generador.RoundedRectangle(new XRect(30.0, 170.0, 260.0, 185.0)).EstiloNormal().Write(emisor.RazonSocial, 40.0, 180.0).EstiloMicro().Write(emisor.NombreComercial, 40.0, 200.0).EstiloNormal().Write("Dirección", 40.0, 240.0).Write("Matriz:", 40.0, 250.0).EstiloMicro().Write(emisor.DireccionMatriz, 90.0, 240.0).EstiloNormal().Write("Dirección", 40.0, 270.0).Write("Sucursal:", 40.0, 280.0).EstiloMicro().Write(documento.PuntoEmision.Establecimiento.DireccionEstablecimiento, 90.0, 270.0).EstiloNormal().If(!string.IsNullOrWhiteSpace(emisor.ContribuyenteEspecial)).Write("Contribuyente Especial Nro", 40.0, 300.0).Write(emisor.ContribuyenteEspecial, 240.0, 300.0).Endif().Write("OBLIGADO A LLEVAR CONTABILIDAD", 40.0, 310.0).Write(emisor.ObligadoContabilidad ? "SI" : "NO", 240.0, 310.0).If(emisor.RegimenMicroEmpresas).Write("CONTRIBUYENTE RÉGIMEN MICROEMPRESAS", 40.0, 320.0).Endif().If(!string.IsNullOrWhiteSpace(emisor.AgenteRetencion)).Write("Agente de Retención Resolución No.", 40.0, 330.0).Write(emisor.AgenteRetencion, 240.0, 330.0).Endif().RoundedRectangle(new XRect(300.0, 25.0, 270.0, 330.0)).EstiloTitulo().Write("R.U.C.:", 310.0, 45.0).Write(emisor.RUC, 390.0, 45.0).Write(documento.TipoDocumento.ObtenerSRIDescripcion().ToUpper(), 310.0, 65.0).EstiloNormal().Write("No.", 310.0, 90.0).Write(documento.NumeroDocumentoSRI(), 330.0, 90.0).Write("NÚMERO DE AUTORIZACIÓN", 310.0, 110.0).Write(documento.Autorizacion.Numero, 310.0, 140.0).Write("FECHA Y HORA DE", 310.0, 165.0).Write("AUTORIZACIÓN:", 310.0, 175.0).Write(documento.Autorizacion.Fecha.ToString("dd/MM/yyyy HH:mm:ss"), 440.0, 170.0).Write("AMBIENTE", 310.0, 195.0).Write(emisor.EnumTipoAmbiente.ObtenerSRIDescripcion().ToUpper(), 440.0, 195.0).Write("EMISION", 310.0, 220.0).Write(documento.InfoTributaria.EnumTipoEmision.ObtenerSRIDescripcion().ToUpper(), 440.0, 220.0).Write("CLAVE DE ACCESO", 310.0, 260.0).WriteBarCode39(documento.InfoTributaria.ClaveAcceso, 310.0, 280.0, new XSize(250.0, 40.0)).EstiloNormal().Write(documento.InfoTributaria.ClaveAcceso, 325.0, 340.0);
      return generador;
    }

    public static IGenerator CrearInfoAdicional(
      this IGenerator generador,
      Documento documento)
    {
      List<CampoAdicional> infoAdicional = documento.InfoAdicional;
      if ((infoAdicional != null ? (infoAdicional.Any<CampoAdicional>() ? 1 : 0) : 0) == 0)
        return generador;
      Rectangle rectangle;
      generador.RectangleBegin(30.0, generador.PointerY + 25.0, 280.0, out rectangle).WithTable(30.0, generador.PointerY + 25.0, new List<double>()
      {
        90.0,
        190.0
      }, false, 15.0).AddRow().EstiloNormalNegrita().AddCell("Información adicional", (XParagraphAlignment)1).EstiloNormal();
      foreach (CampoAdicional campoAdicional in documento.InfoAdicional)
        generador.AddRow().AddCell(campoAdicional.Nombre, (XParagraphAlignment)1).AddCell(campoAdicional.Valor, (XParagraphAlignment)1);
      return generador.RectangleEnd(rectangle, 0.0);
    }

    public static IGenerator CrearFormasPago(this IGenerator generador, List<Pago> pagos)
    {
      generador.WithTable(30.0, generador.PointerY + 15.0, new List<double>()
      {
        150.0,
        60.0
      }, defaultRowHeight: 15.0).AddRow().AddCell("Forma de pago").AddCell("Valor");
      foreach (Pago pago in pagos)
        generador.AddRow().AddCell(pago.FormaPago.ObtenerSRIDescripcion().ToUpper()).AddCell(new Decimal?(pago.Total));
      return generador;
    }

    public static int CalcularLineas(
      this IGenerator generador,
      double anchoTexto,
      List<string> lineas)
    {
      if (lineas == null)
        return 1;
      int num = 0;
      foreach (string linea in lineas)
      {
        if (linea != null)
        {
          XSize xsize = generador.CurrentPage.CurrentGraphics.MeasureString(linea, generador.CurrentStyle.Font);
          num += Convert.ToInt32(Math.Ceiling(xsize.Width / (anchoTexto - 4.0)));
        }
      }
      return num != 0 ? num : 1;
    }

    public static double CalcularAlturaCelda(
    this IGenerator generador,
    double anchoCelda,
    string contenido)
    {
      if (string.IsNullOrEmpty(contenido))
        return 12.5; // Altura mínima de una línea

      double anchoEfectivo = anchoCelda - 8.0; // Margen interno

      // Dividir por saltos de línea explícitos
      string[] lineas = contenido.Split('\n');
      int totalLineas = 0;

      foreach (string linea in lineas)
      {
        if (string.IsNullOrEmpty(linea))
        {
          totalLineas += 1;
          continue;
        }

        // Medir el ancho del texto
        XSize medidaTexto = generador.CurrentPage.CurrentGraphics.MeasureString(
            linea.Trim(),
            generador.CurrentStyle.Font);

        // Calcular líneas necesarias para esta línea
        int lineasNecesarias = Math.Max(1,
            Convert.ToInt32(Math.Ceiling(medidaTexto.Width / anchoEfectivo)));

        totalLineas += lineasNecesarias;
      }

      return Math.Max(12.5, totalLineas * 12.5 + 5.0); // +5.0 padding vertical
    }
  }
}
