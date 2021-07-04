using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Sri.Modelos.Base
{
    public class DetalleDocumentoItem
    {
        public Item Item { get; set; }

        public int Cantidad { get; set; }

        public List<CampoAdicional> DetallesAdicionales { get; set; }
    }
}
