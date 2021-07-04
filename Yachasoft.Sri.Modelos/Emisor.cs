using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;

namespace Yachasoft.Sri.Modelos
{
    public class Emisor
    {
        public EnumTipoAmbiente EnumTipoAmbiente { get; set; }

        public string RUC { get; set; }

        public string RazonSocial { get; set; }

        public string NombreComercial { get; set; }

        public string DireccionMatriz { get; set; }

        public string ContribuyenteEspecial { get; set; }

        public bool ObligadoContabilidad { get; set; }

        public bool RegimenMicroEmpresas { get; set; }

        public string AgenteRetencion { get; set; }

        public string Logo { get; set; }
    }
}
