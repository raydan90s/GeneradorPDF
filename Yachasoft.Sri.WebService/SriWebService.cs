using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Core.Helpers;
using Yachasoft.Sri.DocumentosElectronicos.Configuracion;
using Yachasoft.Sri.WebService.Request;
using Yachasoft.Sri.WebService.Response;

namespace Yachasoft.Sri.WebService
{
    public class SriWebService : ISriWebService
    {
        private SRIDocumentosElectronicosOptions _options;

        public SriWebService(SRIDocumentosElectronicosOptions options)
        {
            this._options = options;
            this.TipoEsquema = options.WebService.TipoEsquema;
            this.TipoAmbiente = options.WebService.TipoAmbiente;
        }

        public EnumTipoAmbiente TipoAmbiente { get; set; }

        public EnumTipoEsquema TipoEsquema { get; set; }

        public SriWebService Using(
          EnumTipoAmbiente tipoAmbiente,
          EnumTipoEsquema tipoEsquema)
        {
            return new SriWebService(this._options)
            {
                TipoAmbiente = tipoAmbiente,
                TipoEsquema = tipoEsquema
            };
        }

        public async Task<Response<ValidarComprobanteResponse.RespuestaRecepcionComprobante>> ValidarComprobanteAsync(
          XmlDocument firmado)
        {
            return await this.ValidarComprobanteAsync(firmado.OuterXml);
        }

        public async Task<Response<ValidarComprobanteResponse.RespuestaRecepcionComprobante>> ValidarComprobanteAsync(
          string xmlFirmado)
        {
            Response<ValidarComprobanteResponse.RespuestaRecepcionComprobante> response = new Response<ValidarComprobanteResponse.RespuestaRecepcionComprobante>();
            try
            {
                XmlDocument xmlDocument1 = RequestHelper.GetRequestStream("ValidarComprobanteRequest.xml").ToXmlDocument();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(xmlDocument1.OuterXml.Replace("{xmlParameter}", SriWebService.EncodeStringToBase64(xmlFirmado)));
                HttpWebRequest webRequest = SriWebService.CreateWebRequest(this.GenerateUrlValidarComprobante(), "");
                SriWebService.InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
                string end;
                using (WebResponse responseAsync = await webRequest.GetResponseAsync())
                {
                    using (StreamReader streamReader = new StreamReader(responseAsync.GetResponseStream()))
                        end = streamReader.ReadToEnd();
                }
                XmlDocument xmlDocument2 = new XmlDocument();
                xmlDocument2.LoadXml(end);
                XmlNode firstChild = xmlDocument2.FirstChild;
                while (firstChild != null && !string.Equals(firstChild.Name, "RespuestaRecepcionComprobante", StringComparison.InvariantCultureIgnoreCase))
                    firstChild = firstChild.FirstChild;
                XmlDocument objectData = new XmlDocument();
                objectData.LoadXml(firstChild.OuterXml);
                ValidarComprobanteResponse.RespuestaRecepcionComprobante recepcionComprobante = objectData.XmlDeserialize<ValidarComprobanteResponse.RespuestaRecepcionComprobante>();
                response.Data = recepcionComprobante;
                response.Ok = recepcionComprobante.Estado == "RECIBIDA";
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
            }            
            return response;
        }

        public Yachasoft.Sri.WebService.Response.Response<AutoAutorizarComprobanteResponse.Autorizacion> AutoAutorizacionComprobante(
          XmlDocument firmado,
          DateTime fechaAutorizacion,
          string numeroAutorizacion)
        {
            if (this.TipoEsquema == EnumTipoEsquema.Online)
                throw new Exception("Solo se puede usar este método en modo Offline");
            return new Yachasoft.Sri.WebService.Response.Response<AutoAutorizarComprobanteResponse.Autorizacion>()
            {
                Ok = true,
                Data = new AutoAutorizarComprobanteResponse.Autorizacion()
                {
                    Ambiente = this.TipoAmbiente == EnumTipoAmbiente.Prueba ? "PRUEBAS" : "PRODUCCION",
                    FechaAutorizacion = fechaAutorizacion,
                    NumeroAutorizacion = numeroAutorizacion,
                    Comprobante = firmado.OuterXml,
                    Estado = "AUTORIZADO",
                    Mensajes = (string)null
                }
            };
        }

        public async Task<Yachasoft.Sri.WebService.Response.Response<AutorizarComprobanteResponse.RespuestaAutorizacionComprobante>> AutorizacionComprobanteAsync(
          string claveAcceso)
        {
            Yachasoft.Sri.WebService.Response.Response<AutorizarComprobanteResponse.RespuestaAutorizacionComprobante> response = new Yachasoft.Sri.WebService.Response.Response<AutorizarComprobanteResponse.RespuestaAutorizacionComprobante>();
            try
            {
                XmlDocument xmlDocument1 = RequestHelper.GetRequestStream("AutorizacionComprobanteRequest.xml").ToXmlDocument();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(xmlDocument1.OuterXml.Replace("{claveAccesoParameter}", claveAcceso));
                HttpWebRequest webRequest = SriWebService.CreateWebRequest(this.GenerateUrlAutorizacionComprobante(), "");
                SriWebService.InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
                string end;
                using (WebResponse responseAsync = await webRequest.GetResponseAsync())
                {
                    using (StreamReader streamReader = new StreamReader(responseAsync.GetResponseStream()))
                        end = streamReader.ReadToEnd();
                }
                XmlDocument xmlDocument2 = new XmlDocument();
                xmlDocument2.LoadXml(end);
                XmlNode firstChild = xmlDocument2.FirstChild;
                while (firstChild != null && !string.Equals(firstChild.Name, "RespuestaAutorizacionComprobante", StringComparison.InvariantCultureIgnoreCase))
                    firstChild = firstChild.FirstChild;
                XmlDocument objectData = new XmlDocument();
                objectData.LoadXml(firstChild.OuterXml);
                AutorizarComprobanteResponse.RespuestaAutorizacionComprobante autorizacionComprobante = objectData.XmlDeserialize<AutorizarComprobanteResponse.RespuestaAutorizacionComprobante>();
                response.Data = autorizacionComprobante;
                Yachasoft.Sri.WebService.Response.Response<AutorizarComprobanteResponse.RespuestaAutorizacionComprobante> response1 = response;
                AutorizarComprobanteResponse.Autorizaciones autorizaciones = autorizacionComprobante.Autorizaciones;
                string str;
                if (autorizaciones == null)
                {
                    str = (string)null;
                }
                else
                {
                    List<AutorizarComprobanteResponse.Autorizacion> autorizacion = autorizaciones.Autorizacion;
                    str = autorizacion != null ? autorizacion.FirstOrDefault<AutorizarComprobanteResponse.Autorizacion>()?.Estado : (string)null;
                }
                int num = str == "AUTORIZADO" ? 1 : 0;
                response1.Ok = num != 0;
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
            }
            Yachasoft.Sri.WebService.Response.Response<AutorizarComprobanteResponse.RespuestaAutorizacionComprobante> response2 = response;
            response = (Yachasoft.Sri.WebService.Response.Response<AutorizarComprobanteResponse.RespuestaAutorizacionComprobante>)null;
            return response2;
        }

        internal static void InsertSoapEnvelopeIntoWebRequest(
          XmlDocument soapEnvelopeXml,
          HttpWebRequest webRequest)
        {
            using (Stream requestStream = webRequest.GetRequestStream())
                soapEnvelopeXml.Save(requestStream);
        }

        internal static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "text/xml;charset=\"utf-8\"";
            httpWebRequest.Accept = "text/xml";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("SOAPAction", action);
            return httpWebRequest;
        }

        private string GenerateUrlValidarComprobante() => this.GenerarUrlPara("RecepcionComprobantes");

        private string GenerateUrlAutorizacionComprobante() => this.GenerarUrlPara("AutorizacionComprobantes");

        private string GenerarUrlPara(string metodo)
        {
            string str = this.TipoEsquema == EnumTipoEsquema.Offline ? "Offline" : "";
            return this.GenerateRootUrl() + "/comprobantes-electronicos-ws/" + metodo + str;
        }

        private string GenerateRootUrl() => "https://" + (this.TipoAmbiente == EnumTipoAmbiente.Prueba ? "celcer.sri.gob.ec" : "cel.sri.gob.ec");

        internal static string EncodeStringToBase64(string plainTextBytes) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainTextBytes));
    }
}
