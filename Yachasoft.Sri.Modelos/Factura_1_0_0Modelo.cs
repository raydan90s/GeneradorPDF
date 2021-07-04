using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos.Base;

namespace Yachasoft.Sri.Modelos
{
    public class Factura_1_0_0Modelo
    {
        public static readonly EnumTipoDocumento TipoDocumento = EnumTipoDocumento.Factura;
        public static string IdConsumidorFinal = "9999999999";

        public static Sujeto ConsumidorFinal => new Sujeto()
        {
            Identificacion = Factura_1_0_0Modelo.IdConsumidorFinal,
            TipoIdentificador = EnumTipoIdentificacion.VentaConsumidorFinal,
            RazonSocial = "CONSUMIDOR FINAL"
        };

        public class Factura : Documento
        {
            public Factura_1_0_0Modelo.InfoFactura InfoFactura { get; set; } = new Factura_1_0_0Modelo.InfoFactura();

            public List<DetalleDocumentoItemPrecioSubsidio> Detalles { get; set; } = new List<DetalleDocumentoItemPrecioSubsidio>();

            public Factura() => this.TipoDocumento = EnumTipoDocumento.Factura;
        }

        public class InfoFactura
        {
            public Decimal TotalSinImpuestos { get; set; }

            public Decimal TotalDescuento { get; set; }

            public List<ImpuestoVenta> TotalConImpuestos { get; set; } = new List<ImpuestoVenta>();

            public Decimal Propina { get; set; }

            public Decimal ImporteTotal { get; set; }

            public string Moneda { get; set; } = "DOLAR";

            public List<Pago> Pagos { get; set; } = new List<Pago>();

            public string GuiaRemision { get; set; }

            public string DireccionComprador { get; set; }

            public Decimal TotalSubsidio { get; set; }

            public string Placa { get; set; }
        }
    }
}
