using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Sri.Modelos.Base
{
    public abstract class ImpuestoVenta
    {
        public Decimal BaseImponible { get; set; }

        public Decimal Tarifa { get; set; }

        public Decimal Valor { get; set; }

        public Decimal ValorDevolucionIVA { get; set; }

        public Decimal DescuentoAdicional { get; set; }

        public static explicit operator ImpuestoVenta(ImpuestoIVA v)
        {
            throw new NotImplementedException();
        }
    }
}
