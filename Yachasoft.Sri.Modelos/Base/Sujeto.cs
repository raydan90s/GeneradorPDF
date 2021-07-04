using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;

namespace Yachasoft.Sri.Modelos.Base
{
    public class Sujeto
    {
        public EnumTipoIdentificacion TipoIdentificador { get; set; }

        public string RazonSocial { get; set; }

        public string Identificacion { get; set; }
    }
}
