using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Yachasoft.Sri.WebService.Response
{
    public class AutorizarComprobanteResponse
    {
        [XmlRoot(ElementName = "mensaje")]
        public class Mensaje
        {
            [XmlElement(ElementName = "identificador")]
            public string Identificador { get; set; }

            [XmlElement(ElementName = "mensaje")]
            public string Mensaje_ { get; set; }

            [XmlElement(ElementName = "tipo")]
            public string Tipo { get; set; }

            [XmlElement(ElementName = "informacionAdicional")]
            public string InformacionAdicional { get; set; }
        }

        [XmlRoot(ElementName = "mensajes")]
        public class Mensajes
        {
            [XmlElement(ElementName = "mensaje")]
            public List<AutorizarComprobanteResponse.Mensaje> Mensaje { get; set; }
        }

        [XmlRoot(ElementName = "autorizacion")]
        public class Autorizacion
        {
            [XmlElement(ElementName = "estado")]
            public string Estado { get; set; }

            [XmlElement(ElementName = "numeroAutorizacion")]
            public string NumeroAutorizacion { get; set; }

            [XmlElement(ElementName = "fechaAutorizacion")]
            public string FechaAutorizacion { get; set; }

            [XmlElement(ElementName = "ambiente")]
            public string Ambiente { get; set; }

            [XmlElement(ElementName = "comprobante")]
            public string Comprobante { get; set; }

            [XmlElement(ElementName = "mensajes")]
            public AutorizarComprobanteResponse.Mensajes Mensajes { get; set; }
        }

        [XmlRoot(ElementName = "autorizaciones")]
        public class Autorizaciones
        {
            [XmlElement(ElementName = "autorizacion")]
            public List<AutorizarComprobanteResponse.Autorizacion> Autorizacion { get; set; }
        }

        [XmlRoot(ElementName = "RespuestaAutorizacionComprobante")]
        public class RespuestaAutorizacionComprobante
        {
            [XmlElement(ElementName = "claveAccesoConsultada")]
            public string ClaveAccesoConsultada { get; set; }

            [XmlElement(ElementName = "numeroComprobantes")]
            public string NumeroComprobantes { get; set; }

            [XmlElement(ElementName = "autorizaciones")]
            public AutorizarComprobanteResponse.Autorizaciones Autorizaciones { get; set; }
        }
    }
}
