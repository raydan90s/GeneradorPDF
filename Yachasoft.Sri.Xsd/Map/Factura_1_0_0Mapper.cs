// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Map.Factura_1_0_0Mapper
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using Yachasoft.Sri.Core.Helpers;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Xsd.Contratos.Factura_1_0_0;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Yachasoft.Sri.Xsd.Map
{
  public static class Factura_1_0_0Mapper
  {
    public static factura Map(Factura_1_0_0Modelo.Factura documento) => new factura()
    {
      id = facturaID.comprobante,
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
      infoFactura = new facturaInfoFactura()
      {
        fechaEmision = documento.FechaEmision.ToSRIFecha(),
        dirEstablecimiento = documento.PuntoEmision.Establecimiento.DireccionEstablecimiento,
        contribuyenteEspecial = documento.PuntoEmision.Establecimiento.Emisor.ContribuyenteEspecial,
        obligadoContabilidad = documento.PuntoEmision.Establecimiento.Emisor.ObligadoContabilidad ? obligadoContabilidad.SI : obligadoContabilidad.NO,
        obligadoContabilidadSpecified = true,
        guiaRemision = documento.InfoFactura.GuiaRemision,
        placa = documento.InfoFactura.Placa,
        tipoIdentificacionComprador = documento.Sujeto.TipoIdentificador.ObtenerSRICodigo(),
        razonSocialComprador = documento.Sujeto.RazonSocial,
        identificacionComprador = documento.Sujeto.Identificacion,
        direccionComprador = documento.InfoFactura.DireccionComprador,
        totalSinImpuestos = documento.InfoFactura.TotalSinImpuestos,
        totalSubsidio = documento.InfoFactura.TotalSubsidio,
        totalDescuento = documento.InfoFactura.TotalDescuento,
        totalConImpuestos = Factura_1_0_0Mapper.Map(documento.InfoFactura.TotalConImpuestos),
        propina = documento.InfoFactura.Propina,
        propinaSpecified = true,
        importeTotal = documento.InfoFactura.ImporteTotal,
        moneda = documento.InfoFactura.Moneda,
        pagos = Factura_1_0_0Mapper.NewMethod(documento.InfoFactura.Pagos)
      },
      detalles = Factura_1_0_0Mapper.Map(documento.Detalles),
      infoAdicional = Factura_1_0_0Mapper.Map2(documento.InfoAdicional)
    };

    internal static pagosPago[] NewMethod(List<Pago> pagos) => pagos.ConvertAll<pagosPago>((Converter<Pago, pagosPago>) (pago => new pagosPago()
    {
      formaPago = pago.FormaPago.ObtenerSRICodigo(),
      total = pago.Total,
      plazo = pago.Plazo,
      plazoSpecified = pago.Plazo > 0M,
      unidadTiempo = pago.UnidadTiempo
    })).ToArray();

    internal static facturaCampoAdicional[] Map2(
      List<CampoAdicional> infoAdicional)
    {
      return infoAdicional == null || !infoAdicional.Any<CampoAdicional>() ? (facturaCampoAdicional[]) null : infoAdicional.ConvertAll<facturaCampoAdicional>((Converter<CampoAdicional, facturaCampoAdicional>) (campoAdicional => new facturaCampoAdicional()
      {
        nombre = campoAdicional.Nombre,
        Value = campoAdicional.Valor
      })).ToArray();
    }

    internal static facturaDetalle[] Map(
      List<DetalleDocumentoItemPrecioSubsidio> detalles)
    {
      return detalles.ConvertAll<facturaDetalle>((Converter<DetalleDocumentoItemPrecioSubsidio, facturaDetalle>) (facturaDetalle => new facturaDetalle()
      {
        codigoPrincipal = facturaDetalle.Item.CodigoPrincipal,
        codigoAuxiliar = facturaDetalle.Item.CodigoAuxiliar,
        descripcion = facturaDetalle.Item.Descripcion,
        cantidad = (Decimal) facturaDetalle.Cantidad,
        precioUnitario = facturaDetalle.PrecioUnitario,
        precioSinSubsidio = facturaDetalle.PrecioSinSubsidio,
        precioSinSubsidioSpecified = facturaDetalle.PrecioSinSubsidio > 0M,
        descuento = facturaDetalle.Descuento,
        precioTotalSinImpuesto = facturaDetalle.PrecioTotalSinImpuesto,
        detallesAdicionales = Factura_1_0_0Mapper.Map(facturaDetalle.DetallesAdicionales),
        impuestos = Factura_1_0_0Mapper.MapImpuesto(facturaDetalle.Impuestos)
      })).ToArray();
    }

    internal static facturaInfoFacturaTotalImpuesto[] Map(
      List<ImpuestoVenta> impuestos)
    {
      return impuestos.ConvertAll<facturaInfoFacturaTotalImpuesto>((Converter<ImpuestoVenta, facturaInfoFacturaTotalImpuesto>) (impuesto =>
      {
        facturaInfoFacturaTotalImpuesto facturaTotalImpuesto = new facturaInfoFacturaTotalImpuesto()
        {
          baseImponible = impuesto.BaseImponible,
          tarifa = impuesto.Tarifa,
          tarifaSpecified = true,
          valor = impuesto.Valor,
          valorDevolucionIva = impuesto.ValorDevolucionIVA,
          valorDevolucionIvaSpecified = impuesto.ValorDevolucionIVA > 0M,
          descuentoAdicional = impuesto.DescuentoAdicional,
          descuentoAdicionalSpecified = impuesto.DescuentoAdicional > 0M
        };
        if (impuesto is ImpuestoVentaIVA impuestoVentaIva)
        {
          facturaTotalImpuesto.codigo = impuestoVentaIva.Codigo.ObtenerSRICodigo();
          facturaTotalImpuesto.codigoPorcentaje = impuestoVentaIva.CodigoPorcentaje.ObtenerSRICodigo();
        }
        if (impuesto is ImpuestoVentaICE impuestoVentaIce)
        {
          facturaTotalImpuesto.codigo = impuestoVentaIce.Codigo.ObtenerSRICodigo();
          facturaTotalImpuesto.codigoPorcentaje = impuestoVentaIce.CodigoPorcentaje.ObtenerSRICodigo();
        }
        if (impuesto is ImpuestoVentaIRBPNR impuestoVentaIrbpnr)
        {
          facturaTotalImpuesto.codigo = impuestoVentaIrbpnr.Codigo.ObtenerSRICodigo();
          facturaTotalImpuesto.codigoPorcentaje = impuestoVentaIrbpnr.CodigoPorcentaje.ObtenerSRICodigo();
        }
        return facturaTotalImpuesto;
      })).ToArray();
    }

    internal static impuesto[] MapImpuesto(List<Impuesto> impuestos) => impuestos.ConvertAll<impuesto>((Converter<Impuesto, impuesto>) (impuesto =>
    {
      impuesto impuesto1 = new impuesto()
      {
        baseImponible = impuesto.BaseImponible,
        tarifa = impuesto.Tarifa,
        valor = impuesto.Valor
      };
      if (impuesto is Yachasoft.Sri.Modelos.Base.ImpuestoIVA impuestoIva)
      {
        impuesto1.codigo = impuestoIva.Codigo.ObtenerSRICodigo();
        impuesto1.codigoPorcentaje = impuestoIva.CodigoPorcentaje.ObtenerSRICodigo();
      }
      if (impuesto is ImpuestoICE impuestoIce)
      {
        impuesto1.codigo = impuestoIce.Codigo.ObtenerSRICodigo();
        impuesto1.codigoPorcentaje = impuestoIce.CodigoPorcentaje.ObtenerSRICodigo();
      }
      if (impuesto is ImpuestoIRBPNR impuestoIrbpnr)
      {
        impuesto1.codigo = impuestoIrbpnr.Codigo.ObtenerSRICodigo();
        impuesto1.codigoPorcentaje = impuestoIrbpnr.CodigoPorcentaje.ObtenerSRICodigo();
      }
      return impuesto1;
    })).ToArray();

    internal static facturaDetalleDetAdicional[] Map(List<CampoAdicional> detallesAdicionales)
    {
        if (detallesAdicionales == null || !detallesAdicionales.Any())
            return null; 

        return detallesAdicionales.ConvertAll<facturaDetalleDetAdicional>(detalle => new facturaDetalleDetAdicional()
        {
            nombre = detalle.Nombre,
            valor = detalle.Valor
        }).ToArray();
    }
  }
}
