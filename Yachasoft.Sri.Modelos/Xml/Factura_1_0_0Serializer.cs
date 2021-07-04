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
    public static class Factura_1_0_0Serializer
    {
        public static XmlDocument ToXml(this Factura_1_0_0Modelo.Factura modelo)
        {
            Type[] extraTypes = new Type[6]
            {
        typeof (Yachasoft.Sri.Modelos.Base.ImpuestoIVA),
        typeof (ImpuestoICE),
        typeof (ImpuestoIRBPNR),
        typeof (ImpuestoVentaIVA),
        typeof (ImpuestoVentaICE),
        typeof (ImpuestoVentaIRBPNR)
            };
            return modelo.ToXmlDocument<Factura_1_0_0Modelo.Factura>(extraTypes);
        }

        public static Factura_1_0_0Modelo.Factura FromXml(XmlDocument modelo)
        {
            Type[] extraTypes = new Type[6]
            {
        typeof (Yachasoft.Sri.Modelos.Base.ImpuestoIVA),
        typeof (ImpuestoICE),
        typeof (ImpuestoIRBPNR),
        typeof (ImpuestoVentaIVA),
        typeof (ImpuestoVentaICE),
        typeof (ImpuestoVentaIRBPNR)
            };
            return modelo.XmlDeserialize<Factura_1_0_0Modelo.Factura>(extraTypes);
        }
    }
}
