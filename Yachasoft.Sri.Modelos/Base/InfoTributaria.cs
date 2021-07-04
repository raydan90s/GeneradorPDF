using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;

namespace Yachasoft.Sri.Modelos.Base
{
    public class InfoTributaria
    {
        public long Secuencial { get; set; }

        public string ClaveAcceso { get; set; }

        public EnumTipoEmision EnumTipoEmision { get; set; }
    }
}
