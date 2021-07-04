using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Sri.Modelos.Base
{
    public class DetalleDocumentoItemPrecio : DetalleDocumentoItem
    {
        public Decimal PrecioUnitario { get; set; }

        public Decimal Descuento { get; set; }

        public Decimal PrecioTotalSinImpuesto { get; set; }

        public List<Impuesto> Impuestos { get; set; }
    }
}
