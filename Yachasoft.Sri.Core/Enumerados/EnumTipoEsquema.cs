using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Sri.Core.Enumerados
{
    public enum EnumTipoEsquema
    {
        [Display(Name = "Offline")] Offline,
        [Display(Name = "Online")] Online,
    }
}
