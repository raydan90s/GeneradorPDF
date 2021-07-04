using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Core.Enumerados
{
    public enum EnumTipoIdentificacionSinConsumidorFinal
    {
        [SRICodigo("04"), Display(Name = "R.U.C.")] RUC = 4,
        [SRICodigo("05"), Display(Name = "Cédula")] Cedula = 5,
        [SRICodigo("06"), Display(Name = "Pasaporte")] Pasaporte = 6,
        [SRICodigo("08"), Display(Name = "Identificación del exterior")] IdentificacionExterior = 8,
    }
}
