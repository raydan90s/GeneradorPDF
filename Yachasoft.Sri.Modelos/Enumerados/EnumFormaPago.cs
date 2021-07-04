using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Modelos.Enumerados
{
    public enum EnumFormaPago
    {
        [SRICodigo("01"), Display(Name = "Sin utilización del Sistema Financiero")] SinUtilizarSistemaFinanciero,
        [SRICodigo("15"), Display(Name = "Compensación deudas")] CompensacionDeudas,
        [SRICodigo("16"), Display(Name = "Tarjeta de débito")] TarjetaDebito,
        [SRICodigo("17"), Display(Name = "Dinero electrónico")] DineroElectronico,
        [SRICodigo("18"), Display(Name = "Tarjeta Prepago")] TarjetaPrepago,
        [SRICodigo("19"), Display(Name = "Tarjeta de crédito")] TarjetaCredito,
        [SRICodigo("20"), Display(Name = "Otros con utilización del Sistema Financiero")] OtrosUtilizacionSistemaFinanciero,
        [SRICodigo("21"), Display(Name = "Endoso de Títulos")] EndosoTitulos,
    }
}
