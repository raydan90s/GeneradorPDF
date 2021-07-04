using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yachasoft.Sri.Core.Helpers;
using Yachasoft.Sri.Modelos.Base;

namespace Yachasoft.Sri.Modelos.Xml
{
    public static class ComprobanteRetencion_1_0_0Serializer
    {
        public static XmlDocument ToXml(
          this ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion modelo)
        {
            Type[] extraTypes = new Type[5]
            {
        typeof (ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion),
        typeof (ComprobanteRetencion_1_0_0Modelo.ImpuestoRenta),
        typeof (ComprobanteRetencion_1_0_0Modelo.ImpuestoIVA),
        typeof (ComprobanteRetencion_1_0_0Modelo.ImpuestoISD),
        typeof (Impuesto)
            };
            return modelo.ToXmlDocument<ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion>(extraTypes);
        }

        public static ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion FromXml(
          XmlDocument modelo)
        {
            Type[] extraTypes = new Type[5]
            {
        typeof (ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion),
        typeof (ComprobanteRetencion_1_0_0Modelo.ImpuestoRenta),
        typeof (ComprobanteRetencion_1_0_0Modelo.ImpuestoIVA),
        typeof (ComprobanteRetencion_1_0_0Modelo.ImpuestoISD),
        typeof (Impuesto)
            };
            return modelo.XmlDeserialize<ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion>(extraTypes);
        }
    }
}
