using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Modelos.Enumerados
{
    public enum EnumTipoImpuestoIVA
    {
        [SRICodigo("0", Porcentaje = 0.0), Display(Name = "0%")] _0,
        [SRICodigo("2", Porcentaje = 12.0, ValidFromDate = "2017/06/01"), Display(Name = "12%")] _12,
        [SRICodigo("3", Porcentaje = 14.0, ValidFromDate = "2016/06/01", ValidToDate = "2017/05/31"), Display(Name = "14%")] _14,
        [SRICodigo("4", Porcentaje = 15.0, ValidFromDate = "2017/06/01"), Display(Name = "15%")] _15,
        [SRICodigo("6", Porcentaje = 0.0), Display(Name = "No objeto de impuesto")] NoObjetoImpuesto,
        [SRICodigo("7", Porcentaje = 0.0), Display(Name = "Exento de IVA")] ExentoIVA,
    }
}
