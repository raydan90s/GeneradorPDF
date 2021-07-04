using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Modelos.Enumerados;

namespace Yachasoft.Sri.Modelos.Base
{
    public class Pago
    {
        public EnumFormaPago FormaPago { get; set; }

        public Decimal Total { get; set; }

        public Decimal Plazo { get; set; }

        public string UnidadTiempo { get; set; }
    }
}
