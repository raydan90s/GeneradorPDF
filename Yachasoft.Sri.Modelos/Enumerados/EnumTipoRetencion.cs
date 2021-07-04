using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Modelos.Enumerados
{
    public enum EnumTipoRetencion
    {
        [SRICodigo("1"), Display(Name = "Renta")] Renta,
        [SRICodigo("2"), Display(Name = "IVA")] IVA,
        [SRICodigo("6"), Display(Name = "ISD")] ISD,
    }
}
