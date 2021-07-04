using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos.Base;

namespace Yachasoft.Sri.Modelos
{
    public class LiquidacionCompra_1_0_0Modelo
    {
        public static readonly EnumTipoDocumento TipoDocumento = EnumTipoDocumento.LiquidacionCompra;

        public class LiquidacionCompra : Documento
        {
            public LiquidacionCompra_1_0_0Modelo.InfoLiquidacionCompra InfoLiquidacionCompra { get; set; } = new LiquidacionCompra_1_0_0Modelo.InfoLiquidacionCompra();

            public List<DetalleDocumentoItemPrecio> Detalles { get; set; } = new List<DetalleDocumentoItemPrecio>();

            public LiquidacionCompra() => this.TipoDocumento = EnumTipoDocumento.LiquidacionCompra;
        }

        public class InfoLiquidacionCompra
        {
            public Decimal TotalSinImpuestos { get; set; }

            public Decimal TotalDescuento { get; set; }

            public Decimal ImporteTotal { get; set; }

            public string Moneda { get; set; } = "DOLAR";

            public List<ImpuestoVenta> TotalConImpuestos { get; set; } = new List<ImpuestoVenta>();

            public List<Pago> Pagos { get; set; } = new List<Pago>();

            public string DireccionProveedor { get; set; }
        }
    }
}
