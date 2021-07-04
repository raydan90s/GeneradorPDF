using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Yachasoft.Sri.WebService.Response
{
    public class ValidarComprobanteResponse
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
            public List<ValidarComprobanteResponse.Mensaje> Mensaje { get; set; }
        }

        [XmlRoot(ElementName = "comprobante")]
        public class Comprobante
        {
            [XmlElement(ElementName = "claveAcceso")]
            public string ClaveAcceso { get; set; }

            [XmlElement(ElementName = "mensajes")]
            public ValidarComprobanteResponse.Mensajes Mensajes { get; set; }
        }

        [XmlRoot(ElementName = "comprobantes")]
        public class Comprobantes
        {
            [XmlElement(ElementName = "comprobante")]
            public List<ValidarComprobanteResponse.Comprobante> Comprobante { get; set; }
        }

        [XmlRoot(ElementName = "RespuestaRecepcionComprobante")]
        public class RespuestaRecepcionComprobante
        {
            [XmlElement(ElementName = "estado")]
            public string Estado { get; set; }

            [XmlElement(ElementName = "comprobantes")]
            public ValidarComprobanteResponse.Comprobantes Comprobantes { get; set; }
        }
    }
}
