using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Modelos.Extensions;

namespace Yachasoft.Sri.Modelos.Base
{
    public class Documento : InfoDocumento
    {
        public EnumTipoImpuestoIVA TipoImpuestoIVAVigente => this.GetTipoImpuestoIVAGravadoVigente();

        public EnumTipoDocumento TipoDocumento { get; init; }

        public Autorizacion Autorizacion { get; set; } = new Autorizacion();

        public PuntoEmision PuntoEmision { get; set; } = new PuntoEmision();

        public InfoTributaria InfoTributaria { get; set; } = new InfoTributaria();

        public List<CampoAdicional> InfoAdicional { get; set; } = new List<CampoAdicional>();
    }
}
