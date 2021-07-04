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
    public static class NotaDebito_1_0_0Serializer
    {
        public static XmlDocument ToXml(this NotaDebito_1_0_0Modelo.NotaDebito modelo)
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
            return modelo.ToXmlDocument<NotaDebito_1_0_0Modelo.NotaDebito>(extraTypes);
        }

        public static NotaDebito_1_0_0Modelo.NotaDebito FromXml(
          this XmlDocument modelo)
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
            return modelo.XmlDeserialize<NotaDebito_1_0_0Modelo.NotaDebito>(extraTypes);
        }
    }
}
