using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;

namespace Yachasoft.Sri.DocumentosElectronicos.Configuracion
{
    public class WebServiceOptions
    {
        public EnumTipoAmbiente TipoAmbiente { get; set; } = EnumTipoAmbiente.Produccion;

        public EnumTipoEsquema TipoEsquema { get; set; }
    }
}
