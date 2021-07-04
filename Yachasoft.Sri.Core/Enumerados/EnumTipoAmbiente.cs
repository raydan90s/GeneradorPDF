using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Core.Enumerados
{
    public enum EnumTipoAmbiente
    {
        [SRICodigo("1"), Display(Name = "Pruebas")] Prueba,
        [SRICodigo("2"), Display(Name = "Producción")] Produccion,
    }
}
