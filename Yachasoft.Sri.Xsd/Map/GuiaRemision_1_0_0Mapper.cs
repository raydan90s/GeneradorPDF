// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.GuiaRemisioncion.Map.GuiaRemision_1_0_0Mapper
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using Yachasoft.Sri.Core.Helpers;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Xsd.Contratos.GuiaRemision_1_0_0;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Yachasoft.Sri.GuiaRemisioncion.Map
{
  public static class GuiaRemision_1_0_0Mapper
  {
    public static guiaRemision Map(GuiaRemision_1_0_0Modelo.GuiaRemision documento) => new guiaRemision()
    {
      id = guiaRemisionID.comprobante,
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
      infoGuiaRemision = new guiaRemisionInfoGuiaRemision()
      {
        fechaIniTransporte = documento.FechaEmision.ToSRIFecha(),
        dirEstablecimiento = documento.PuntoEmision.Establecimiento.DireccionEstablecimiento,
        contribuyenteEspecial = documento.PuntoEmision.Establecimiento.Emisor.ContribuyenteEspecial,
        obligadoContabilidad = documento.PuntoEmision.Establecimiento.Emisor.ObligadoContabilidad ? obligadoContabilidad.SI : obligadoContabilidad.NO,
        obligadoContabilidadSpecified = true,
        dirPartida = documento.InfoGuiaRemision.DireccionPartida,
        fechaFinTransporte = documento.InfoGuiaRemision.FechaFinTransporte.ToSRIFecha(),
        placa = documento.InfoGuiaRemision.Placa,
        tipoIdentificacionTransportista = documento.Sujeto.TipoIdentificador.ObtenerSRICodigo(),
        rucTransportista = documento.Sujeto.Identificacion,
        razonSocialTransportista = documento.Sujeto.RazonSocial
      },
      destinatarios = new guiaRemisionDestinatarios()
      {
        destinatario = GuiaRemision_1_0_0Mapper.Map(documento.Destinatarios)
      },
      infoAdicional = GuiaRemision_1_0_0Mapper.Map(documento.InfoAdicional)
    };

    private static destinatario[] Map(
      List<GuiaRemision_1_0_0Modelo.Destinatario> destinatarios)
    {
      return destinatarios.ConvertAll<destinatario>((Converter<GuiaRemision_1_0_0Modelo.Destinatario, destinatario>) (destinatario => new destinatario()
      {
        identificacionDestinatario = destinatario.SujetoDestinatario.Identificacion,
        razonSocialDestinatario = destinatario.SujetoDestinatario.RazonSocial,
        dirDestinatario = destinatario.DireccionDestinatario,
        motivoTraslado = destinatario.MotivoTraslado,
        docAduaneroUnico = destinatario.DocAduaneroUnico,
        codEstabDestino = destinatario.CodEstablecimientoAduanero,
        ruta = destinatario.Ruta,
        codDocSustento = destinatario.DocumentoSustento.CodDocumento.ObtenerSRICodigo(),
        numDocSustento = destinatario.DocumentoSustento.NumDocumento,
        fechaEmisionDocSustento = destinatario.DocumentoSustento.FechaEmisionDocumento.ToSRIFecha(),
        numAutDocSustento = destinatario.NumAutDocumentoSustento,
        detalles = new destinatarioDetalles()
        {
          detalle = GuiaRemision_1_0_0Mapper.Map(destinatario.Detalles)
        }
      })).ToArray();
    }

    internal static guiaRemisionCampoAdicional[] Map(
      List<CampoAdicional> detallesAdicionales)
    {
      return detallesAdicionales == null || !detallesAdicionales.Any<CampoAdicional>() ? (guiaRemisionCampoAdicional[]) null : detallesAdicionales.ConvertAll<guiaRemisionCampoAdicional>((Converter<CampoAdicional, guiaRemisionCampoAdicional>) (detalle => new guiaRemisionCampoAdicional()
      {
        nombre = detalle.Nombre,
        Value = detalle.Valor
      })).ToArray();
    }

    internal static detalle[] Map(List<DetalleDocumentoItem> detalles) => detalles.ConvertAll<detalle>((Converter<DetalleDocumentoItem, detalle>) (detalle => new detalle()
    {
      codigoInterno = detalle.Item.CodigoPrincipal,
      codigoAdicional = detalle.Item.CodigoAuxiliar,
      descripcion = detalle.Item.Descripcion,
      cantidad = (Decimal) detalle.Cantidad,
      detallesAdicionales = GuiaRemision_1_0_0Mapper.Map(detalle)
    })).ToArray();

    private static detalleDetAdicional[] Map(DetalleDocumentoItem detalle) => detalle.DetallesAdicionales.ConvertAll<detalleDetAdicional>((Converter<CampoAdicional, detalleDetAdicional>) (detalleDetallesAdicionale => new detalleDetAdicional()
    {
      nombre = detalleDetallesAdicionale.Nombre,
      valor = detalleDetallesAdicionale.Valor
    })).ToArray();
  }
}
