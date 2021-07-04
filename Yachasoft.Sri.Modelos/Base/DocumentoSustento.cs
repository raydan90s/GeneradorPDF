using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;

namespace Yachasoft.Sri.Modelos.Base
{
    public class DocumentoSustento
    {
        public EnumTipoDocumento CodDocumento { get; set; }

        public string NumDocumento { get; set; }

        public DateTime FechaEmisionDocumento { get; set; }
    }
}
