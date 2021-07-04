using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Modelos.Enumerados
{
    public enum EnumTipoImpuestoIVAsinPorcentaje
    {
        [SRICodigo("0", Porcentaje = 0.0), Display(Name = "0%")] _0,
        [SRICodigo("2"), Display(Name = "Grava IVA")] GravaIVA,
        [SRICodigo("6", Porcentaje = 0.0), Display(Name = "No objeto de impuesto")] NoObjetoImpuesto,
        [SRICodigo("7", Porcentaje = 0.0), Display(Name = "Exento de IVA")] ExentoIVA,
    }
}
