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
    public static class NotaCredito_1_0_0Serializer
    {
        public static XmlDocument ToXml(this NotaCredito_1_0_0Modelo.NotaCredito modelo)
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
            return modelo.ToXmlDocument<NotaCredito_1_0_0Modelo.NotaCredito>(extraTypes);
        }

        public static NotaCredito_1_0_0Modelo.NotaCredito FromXml(
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
            return modelo.XmlDeserialize<NotaCredito_1_0_0Modelo.NotaCredito>(extraTypes);
        }
    }
}
