using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Modelos.Enumerados
{
    public enum EnumTipoRetencionIVA
    {
        [SRICodigo("9", Porcentaje = 10.0), Display(Name = "10%")] _10,
        [SRICodigo("10", Porcentaje = 20.0), Display(Name = "20%")] _20,
        [SRICodigo("1", Porcentaje = 30.0), Display(Name = "30%")] _30,
        [SRICodigo("11", Porcentaje = 50.0), Display(Name = "50%")] _50,
        [SRICodigo("2", Porcentaje = 70.0), Display(Name = "70%")] _70,
        [SRICodigo("3", Porcentaje = 100.0), Display(Name = "100%")] _100,
        [SRICodigo("7", Porcentaje = 0.0), Display(Name = "Retención en Cero")] RetencionEnCero,
        [SRICodigo("8", Porcentaje = 0.0), Display(Name = "No procede retención")] NoProcedeRetencion,
    }
}
