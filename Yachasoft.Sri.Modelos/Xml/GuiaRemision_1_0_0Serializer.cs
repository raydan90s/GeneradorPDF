using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yachasoft.Sri.Core.Helpers;

namespace Yachasoft.Sri.Modelos.Xml
{
    public static class GuiaRemision_1_0_0Serializer
    {
        public static XmlDocument ToXml(this GuiaRemision_1_0_0Modelo.GuiaRemision modelo)
        {
            Type[] extraTypes = (Type[])null;
            return modelo.ToXmlDocument<GuiaRemision_1_0_0Modelo.GuiaRemision>(extraTypes);
        }

        public static GuiaRemision_1_0_0Modelo.GuiaRemision FromXml(
          XmlDocument modelo)
        {
            Type[] extraTypes = (Type[])null;
            return modelo.XmlDeserialize<GuiaRemision_1_0_0Modelo.GuiaRemision>(extraTypes);
        }
    }
}
