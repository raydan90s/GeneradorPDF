using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Modelos.Enumerados;

namespace Yachasoft.Sri.Modelos.Base
{
    public class ImpuestoICE : Impuesto
    {
        public EnumTipoImpuesto Codigo = EnumTipoImpuesto.ICE;

        public EnumTipoImpuestoICE CodigoPorcentaje { get; set; }

        public Decimal TarifaEspecifica { get; set; }
    }
}
