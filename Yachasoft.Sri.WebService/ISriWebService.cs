using System;
using System.Threading.Tasks;
using System.Xml;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.WebService.Response;

namespace Yachasoft.Sri.WebService
{
    public interface ISriWebService
    {
        EnumTipoAmbiente TipoAmbiente { get; set; }
        EnumTipoEsquema TipoEsquema { get; set; }

        Response<AutoAutorizarComprobanteResponse.Autorizacion> AutoAutorizacionComprobante(XmlDocument firmado, DateTime fechaAutorizacion, string numeroAutorizacion);
        Task<Response<AutorizarComprobanteResponse.RespuestaAutorizacionComprobante>> AutorizacionComprobanteAsync(string claveAcceso);
        SriWebService Using(EnumTipoAmbiente tipoAmbiente, EnumTipoEsquema tipoEsquema);
        Task<Response<ValidarComprobanteResponse.RespuestaRecepcionComprobante>> ValidarComprobanteAsync(string xmlFirmado);
        Task<Response<ValidarComprobanteResponse.RespuestaRecepcionComprobante>> ValidarComprobanteAsync(XmlDocument firmado);
    }
}