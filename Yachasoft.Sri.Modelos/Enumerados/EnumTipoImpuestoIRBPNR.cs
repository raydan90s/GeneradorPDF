using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Modelos.Enumerados
{
    public enum EnumTipoImpuestoIRBPNR
    {
        [SRICodigo("5001", Porcentaje = 0.0), Display(Name = "Botellas plásticas no retornables")] _5001,
    }
}
