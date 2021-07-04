using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Modelos.Enumerados
{
    public enum EnumTipoRetencionISD
    {
        [SRICodigo("4580", Porcentaje = 5.0), Display(Name = "5%")] _5,
    }
}
