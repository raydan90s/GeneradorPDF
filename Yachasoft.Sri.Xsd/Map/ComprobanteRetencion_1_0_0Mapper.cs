// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Map.ComprobanteRetencion_1_0_0Mapper
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using Yachasoft.Sri.Core.Helpers;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Xsd.Contratos.ComprobanteRetencion_1_0_0;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Yachasoft.Sri.Xsd.Map
{
  public static class ComprobanteRetencion_1_0_0Mapper
  {
    public static comprobanteRetencion Map(
      ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion documento)
    {
      return new comprobanteRetencion()
      {
        id = comprobanteRetencionID.comprobante,
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
        infoCompRetencion = new comprobanteRetencionInfoCompRetencion()
        {
          fechaEmision = documento.FechaEmision.ToSRIFecha(),
          dirEstablecimiento = documento.PuntoEmision.Establecimiento.DireccionEstablecimiento,
          contribuyenteEspecial = documento.PuntoEmision.Establecimiento.Emisor.ContribuyenteEspecial,
          obligadoContabilidad = documento.PuntoEmision.Establecimiento.Emisor.ObligadoContabilidad ? obligadoContabilidad.SI : obligadoContabilidad.NO,
          obligadoContabilidadSpecified = true,
          tipoIdentificacionSujetoRetenido = documento.Sujeto.TipoIdentificador.ObtenerSRICodigo(),
          razonSocialSujetoRetenido = documento.Sujeto.RazonSocial,
          identificacionSujetoRetenido = documento.Sujeto.Identificacion,
          periodoFiscal = documento.InfoCompRetencion.PeriodoFiscal
        },
        impuestos = ComprobanteRetencion_1_0_0Mapper.Map(documento.Impuestos),
        infoAdicional = ComprobanteRetencion_1_0_0Mapper.Map(documento.InfoAdicional)
      };
    }

    internal static comprobanteRetencionCampoAdicional[] Map(
      List<CampoAdicional> detallesAdicionales)
    {
      return detallesAdicionales == null || !detallesAdicionales.Any<CampoAdicional>() ? (comprobanteRetencionCampoAdicional[]) null : detallesAdicionales.ConvertAll<comprobanteRetencionCampoAdicional>((Converter<CampoAdicional, comprobanteRetencionCampoAdicional>) (detalle => new comprobanteRetencionCampoAdicional()
      {
        nombre = detalle.Nombre,
        Value = detalle.Valor
      })).ToArray();
    }

    internal static impuesto[] Map(
      List<ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion> impuestos)
    {
      return impuestos.ConvertAll<impuesto>((Converter<ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion, impuesto>) (impuesto =>
      {
        impuesto impuesto1 = new impuesto()
        {
          baseImponible = impuesto.BaseImponible,
          porcentajeRetener = impuesto.Tarifa,
          valorRetenido = impuesto.Valor,
          codDocSustento = impuesto.DocumentoSustento.CodDocumento.ObtenerSRICodigo(),
          numDocSustento = impuesto.DocumentoSustento.NumDocumento,
          fechaEmisionDocSustento = impuesto.DocumentoSustento.FechaEmisionDocumento.ToSRIFecha()
        };
        if (impuesto is ComprobanteRetencion_1_0_0Modelo.ImpuestoIVA impuestoIva)
        {
          impuesto1.codigo = impuestoIva.Codigo.ObtenerSRICodigo();
          impuesto1.codigoRetencion = impuestoIva.CodigoRetencion.ObtenerSRICodigo();
        }
        if (impuesto is ComprobanteRetencion_1_0_0Modelo.ImpuestoRenta impuestoRenta)
        {
          impuesto1.codigo = impuestoRenta.Codigo.ObtenerSRICodigo();
          impuesto1.codigoRetencion = impuestoRenta.CodigoRetencion.ObtenerSRICodigo();
        }
        if (impuesto is ComprobanteRetencion_1_0_0Modelo.ImpuestoISD impuestoIsd)
        {
          impuesto1.codigo = impuestoIsd.Codigo.ObtenerSRICodigo();
          impuesto1.codigoRetencion = impuestoIsd.CodigoRetencion.ObtenerSRICodigo();
        }
        return impuesto1;
      })).ToArray();
    }
  }
}
