// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Map.NotaDebito_1_0_0Mapper
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using Yachasoft.Sri.Core.Helpers;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Xsd.Contratos.NotaDebito_1_0_0;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Yachasoft.Sri.Xsd.Map
{
  public static class NotaDebito_1_0_0Mapper
  {
    public static notaDebito Map(NotaDebito_1_0_0Modelo.NotaDebito documento) => new notaDebito()
    {
      id = notaDebitoID.comprobante,
      idSpecified = true,
      version = "1.0.0",
      infoTributaria = new infoTributaria()
      {
        ambiente = documento.PuntoEmision.Establecimiento.Emisor.EnumTipoAmbiente.ObtenerSRICodigo(),
        tipoEmision = documento.InfoTributaria.EnumTipoEmision.ObtenerSRICodigo(),
        razonSocial = documento.PuntoEmision.Establecimiento.Emisor.RazonSocial,
        nombreComercial = documento.PuntoEmision.Establecimiento.Emisor.NombreComercial,
        ruc = documento.PuntoEmision.Establecimiento.Emisor.RUC,
        claveAcceso = documento.InfoTributaria.ClaveAcceso,
        codDoc = documento.TipoDocumento.ObtenerSRICodigo(),
        estab = documento.PuntoEmision.Establecimiento.Codigo.ToString("D3"),
        ptoEmi = documento.PuntoEmision.Codigo.ToString("D3"),
        secuencial = documento.InfoTributaria.Secuencial.ToString("D9"),
        dirMatriz = documento.PuntoEmision.Establecimiento.Emisor.DireccionMatriz,
        regimenMicroempresas = documento.PuntoEmision.Establecimiento.Emisor.RegimenMicroEmpresas ? "CONTRIBUYENTE RÉGIMEN MICROEMPRESAS" : (string) null,
        agenteRetencion = documento.PuntoEmision.Establecimiento.Emisor.AgenteRetencion
      },
      infoNotaDebito = new notaDebitoInfoNotaDebito()
      {
        fechaEmision = documento.FechaEmision.ToSRIFecha(),
        dirEstablecimiento = documento.PuntoEmision.Establecimiento.DireccionEstablecimiento,
        contribuyenteEspecial = documento.PuntoEmision.Establecimiento.Emisor.ContribuyenteEspecial,
        obligadoContabilidad = documento.PuntoEmision.Establecimiento.Emisor.ObligadoContabilidad ? obligadoContabilidad.SI : obligadoContabilidad.NO,
        obligadoContabilidadSpecified = true,
        tipoIdentificacionComprador = documento.Sujeto.TipoIdentificador.ObtenerSRICodigo(),
        razonSocialComprador = documento.Sujeto.RazonSocial,
        identificacionComprador = documento.Sujeto.Identificacion,
        codDocModificado = documento.InfoNotaDebito.DocumentoModificado.CodDocumento.ObtenerSRICodigo(),
        numDocModificado = documento.InfoNotaDebito.DocumentoModificado.NumDocumento,
        fechaEmisionDocSustento = documento.InfoNotaDebito.DocumentoModificado.FechaEmisionDocumento.ToSRIFecha(),
        totalSinImpuestos = documento.InfoNotaDebito.TotalSinImpuestos,
        valorTotal = documento.InfoNotaDebito.ValorTotal,
        pagos = NotaDebito_1_0_0Mapper.Map(documento.InfoNotaDebito.Pagos),
        impuestos = NotaDebito_1_0_0Mapper.Map(documento.InfoNotaDebito.Impuestos)
      },
      motivos = new notaDebitoMotivos()
      {
        motivo = NotaDebito_1_0_0Mapper.Map(documento.Motivos)
      },
      infoAdicional = NotaDebito_1_0_0Mapper.Map(documento.InfoAdicional)
    };

    private static notaDebitoMotivosMotivo[] Map(List<Motivo> motivos) => motivos.ConvertAll<notaDebitoMotivosMotivo>((Converter<Motivo, notaDebitoMotivosMotivo>) (motivo => new notaDebitoMotivosMotivo()
    {
      razon = motivo.Razon,
      valor = motivo.Valor
    })).ToArray();

    internal static notaDebitoCampoAdicional[] Map(
      List<CampoAdicional> detallesAdicionales)
    {
      return detallesAdicionales == null || !detallesAdicionales.Any<CampoAdicional>() ? (notaDebitoCampoAdicional[]) null : detallesAdicionales.ConvertAll<notaDebitoCampoAdicional>((Converter<CampoAdicional, notaDebitoCampoAdicional>) (detalle => new notaDebitoCampoAdicional()
      {
        nombre = detalle.Nombre,
        Value = detalle.Valor
      })).ToArray();
    }

    internal static pagos[] Map(List<List<Pago>> pagos1) => pagos1.ConvertAll<pagos>((Converter<List<Pago>, pagos>) (pagos2 => NotaDebito_1_0_0Mapper.Map(pagos2))).ToArray();

    internal static pagos Map(List<Pago> pagos) => new pagos()
    {
      pago = pagos.ConvertAll<pago>((Converter<Pago, pago>) (pagoElement => new pago()
      {
        formaPago = pagoElement.FormaPago.ObtenerSRICodigo(),
        total = pagoElement.Total,
        plazo = pagoElement.Plazo,
        plazoSpecified = pagoElement.Plazo > 0M,
        unidadTiempo = pagoElement.UnidadTiempo
      })).ToArray()
    };

    internal static impuesto[] Map(List<ImpuestoVenta> impuestos) => impuestos?.ConvertAll<impuesto>((Converter<ImpuestoVenta, impuesto>) (impuesto =>
    {
      impuesto impuesto1 = new impuesto()
      {
        baseImponible = impuesto.BaseImponible,
        tarifa = impuesto.Tarifa,
        valor = impuesto.Valor,
        valorDevolucionIva = impuesto.ValorDevolucionIVA,
        valorDevolucionIvaSpecified = impuesto.ValorDevolucionIVA > 0M
      };
      if (impuesto is ImpuestoVentaIVA impuestoVentaIva)
      {
        impuesto1.codigo = impuestoVentaIva.Codigo.ObtenerSRICodigo();
        impuesto1.codigoPorcentaje = impuestoVentaIva.CodigoPorcentaje.ObtenerSRICodigo();
      }
      if (impuesto is ImpuestoVentaICE impuestoVentaIce)
      {
        impuesto1.codigo = impuestoVentaIce.Codigo.ObtenerSRICodigo();
        impuesto1.codigoPorcentaje = impuestoVentaIce.CodigoPorcentaje.ObtenerSRICodigo();
      }
      return impuesto1;
    })).ToArray();
  }
}
