using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Modelos.Enumerados
{
    public enum EnumTipoImpuesto
    {
        [SRICodigo("2")] IVA,
        [SRICodigo("3")] ICE,
        [SRICodigo("5")] IRBPNR,
    }
}
