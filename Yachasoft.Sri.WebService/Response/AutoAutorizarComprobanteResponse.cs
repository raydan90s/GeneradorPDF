using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Yachasoft.Sri.WebService.Response
{
    public class AutoAutorizarComprobanteResponse
    {
        [XmlRoot(ElementName = "autorizacion")]
        public class Autorizacion
        {
            [XmlElement(ElementName = "estado")]
            public string Estado { get; set; }

            [XmlElement(ElementName = "numeroAutorizacion")]
            public string NumeroAutorizacion { get; set; }

            [XmlElement(ElementName = "fechaAutorizacion")]
            public DateTime FechaAutorizacion { get; set; }

            [XmlElement(ElementName = "ambiente")]
            public string Ambiente { get; set; }

            [XmlIgnore]
            public string Comprobante { get; set; }

            [XmlElement(ElementName = "comprobante")]
            [XmlText]
            public XmlNode[] MensajesCData
            {
                get => new XmlNode[1]
                {
          (XmlNode) new XmlDocument().CreateCDataSection(this.Comprobante)
                };
                set
                {
                    if (value == null)
                        this.Comprobante = (string)null;
                    else
                        this.Comprobante = value.Length == 1 ? value[0].Value : throw new InvalidOperationException(string.Format("Invalid array length {0}", (object)value.Length));
                }
            }

            [XmlElement(ElementName = "mensajes")]
            public string Mensajes { get; set; }
        }
    }
}
