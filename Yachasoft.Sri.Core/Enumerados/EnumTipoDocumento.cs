using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Core.Enumerados
{
    public enum EnumTipoDocumento
    {
        [SRICodigo("01"), Display(Name = "Factura")] Factura,
        [SRICodigo("03"), Display(Name = "Liquidación de compra")] LiquidacionCompra,
        [SRICodigo("04"), Display(Name = "Nota de Crédito")] NotaCredito,
        [SRICodigo("05"), Display(Name = "Nota de Débito")] NotaDebito,
        [SRICodigo("06"), Display(Name = "Guía de remisión")] GuiaRemision,
        [SRICodigo("07"), Display(Name = "Comprobante de Retención")] ComprobanteRetencion,
    }
}
