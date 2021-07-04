// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Map.LiquidacionCompra_1_0_0Mapper
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using Yachasoft.Sri.Core.Helpers;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Xsd.Contratos.LiquidacionCompra_1_0_0;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Yachasoft.Sri.Xsd.Map
{
  public static class LiquidacionCompra_1_0_0Mapper
  {
    public static liquidacionCompra Map(
      LiquidacionCompra_1_0_0Modelo.LiquidacionCompra documento)
    {
      return new liquidacionCompra()
      {
        id = liquidacionCompraID.comprobante,
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
        infoLiquidacionCompra = new liquidacionCompraInfoLiquidacionCompra()
        {
          fechaEmision = documento.FechaEmision.ToSRIFecha(),
          dirEstablecimiento = documento.PuntoEmision.Establecimiento.DireccionEstablecimiento,
          contribuyenteEspecial = documento.PuntoEmision.Establecimiento.Emisor.ContribuyenteEspecial,
          obligadoContabilidad = documento.PuntoEmision.Establecimiento.Emisor.ObligadoContabilidad ? obligadoContabilidad.SI : obligadoContabilidad.NO,
          obligadoContabilidadSpecified = true,
          tipoIdentificacionProveedor = documento.Sujeto.TipoIdentificador.ObtenerSRICodigo(),
          razonSocialProveedor = documento.Sujeto.RazonSocial,
          identificacionProveedor = documento.Sujeto.Identificacion,
          direccionProveedor = documento.InfoLiquidacionCompra.DireccionProveedor,
          totalSinImpuestos = documento.InfoLiquidacionCompra.TotalSinImpuestos,
          totalDescuento = documento.InfoLiquidacionCompra.TotalDescuento,
          importeTotal = documento.InfoLiquidacionCompra.ImporteTotal,
          moneda = documento.InfoLiquidacionCompra.Moneda,
          totalConImpuestos = LiquidacionCompra_1_0_0Mapper.Map(documento.InfoLiquidacionCompra.TotalConImpuestos),
          pagos = LiquidacionCompra_1_0_0Mapper.Map(documento.InfoLiquidacionCompra.Pagos)
        },
        detalles = LiquidacionCompra_1_0_0Mapper.Map2(documento.Detalles),
        infoAdicional = LiquidacionCompra_1_0_0Mapper.Map(documento.InfoAdicional)
      };
    }

    internal static pagosPago[] Map(List<Pago> pagos) => pagos.ConvertAll<pagosPago>((Converter<Pago, pagosPago>) (pago => new pagosPago()
    {
      formaPago = pago.FormaPago.ObtenerSRICodigo(),
      total = pago.Total,
      plazo = pago.Plazo,
      plazoSpecified = pago.Plazo > 0M,
      unidadTiempo = pago.UnidadTiempo
    })).ToArray();

    internal static liquidacionCompraDetalle[] Map2(
      List<DetalleDocumentoItemPrecio> detalles)
    {
      return detalles.ConvertAll<liquidacionCompraDetalle>((Converter<DetalleDocumentoItemPrecio, liquidacionCompraDetalle>) (facturaDetalle => new liquidacionCompraDetalle()
      {
        codigoPrincipal = facturaDetalle.Item.CodigoPrincipal,
        codigoAuxiliar = facturaDetalle.Item.CodigoAuxiliar,
        descripcion = facturaDetalle.Item.Descripcion,
        cantidad = (Decimal) facturaDetalle.Cantidad,
        precioUnitario = facturaDetalle.PrecioUnitario,
        descuento = facturaDetalle.Descuento,
        precioTotalSinImpuesto = facturaDetalle.PrecioTotalSinImpuesto,
        detallesAdicionales = LiquidacionCompra_1_0_0Mapper.Map2(facturaDetalle.DetallesAdicionales),
        impuestos = LiquidacionCompra_1_0_0Mapper.Map2(facturaDetalle.Impuestos)
      })).ToArray();
    }

    internal static liquidacionCompraCampoAdicional[] Map(
      List<CampoAdicional> detallesAdicionales)
    {
      return detallesAdicionales == null || !detallesAdicionales.Any<CampoAdicional>() ? (liquidacionCompraCampoAdicional[]) null : detallesAdicionales.ConvertAll<liquidacionCompraCampoAdicional>((Converter<CampoAdicional, liquidacionCompraCampoAdicional>) (detalle => new liquidacionCompraCampoAdicional()
      {
        nombre = detalle.Nombre,
        Value = detalle.Valor
      })).ToArray();
    }

    internal static impuesto[] Map2(List<Impuesto> impuestos) => impuestos.ConvertAll<impuesto>((Converter<Impuesto, impuesto>) (impuesto =>
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
      return impuesto1;
    })).ToArray();

    internal static liquidacionCompraDetalleDetAdicional[] Map2(
      List<CampoAdicional> detallesAdicionales)
    {
      return detallesAdicionales.ConvertAll<liquidacionCompraDetalleDetAdicional>((Converter<CampoAdicional, liquidacionCompraDetalleDetAdicional>) (detalle => new liquidacionCompraDetalleDetAdicional()
      {
        nombre = detalle.Nombre,
        valor = detalle.Valor
      })).ToArray();
    }

    internal static liquidacionCompraInfoLiquidacionCompraTotalImpuesto[] Map(
      List<ImpuestoVenta> impuestos)
    {
      return impuestos.ConvertAll<liquidacionCompraInfoLiquidacionCompraTotalImpuesto>((Converter<ImpuestoVenta, liquidacionCompraInfoLiquidacionCompraTotalImpuesto>) (impuesto =>
      {
        liquidacionCompraInfoLiquidacionCompraTotalImpuesto compraTotalImpuesto = new liquidacionCompraInfoLiquidacionCompraTotalImpuesto()
        {
          baseImponible = impuesto.BaseImponible,
          valor = impuesto.Valor
        };
        if (impuesto is ImpuestoVentaIVA impuestoVentaIva)
        {
          compraTotalImpuesto.codigo = impuestoVentaIva.Codigo.ObtenerSRICodigo();
          compraTotalImpuesto.codigoPorcentaje = impuestoVentaIva.CodigoPorcentaje.ObtenerSRICodigo();
        }
        if (impuesto is ImpuestoVentaICE impuestoVentaIce)
        {
          compraTotalImpuesto.codigo = impuestoVentaIce.Codigo.ObtenerSRICodigo();
          compraTotalImpuesto.codigoPorcentaje = impuestoVentaIce.CodigoPorcentaje.ObtenerSRICodigo();
        }
        return compraTotalImpuesto;
      })).ToArray();
    }
  }
}
