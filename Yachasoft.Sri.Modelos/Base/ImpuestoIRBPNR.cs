using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Modelos.Enumerados;

namespace Yachasoft.Sri.Modelos.Base
{
    public class ImpuestoIRBPNR : Impuesto
    {
        public EnumTipoImpuesto Codigo = EnumTipoImpuesto.IRBPNR;

        public EnumTipoImpuestoIRBPNR CodigoPorcentaje { get; set; }
    }
}
