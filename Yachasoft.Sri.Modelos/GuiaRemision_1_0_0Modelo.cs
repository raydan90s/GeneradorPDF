using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos.Base;

namespace Yachasoft.Sri.Modelos
{
    public class GuiaRemision_1_0_0Modelo
    {
        public static readonly EnumTipoDocumento TipoDocumento = EnumTipoDocumento.GuiaRemision;

        public class GuiaRemision : Documento
        {
            public GuiaRemision_1_0_0Modelo.InfoGuiaRemision InfoGuiaRemision { get; set; } = new GuiaRemision_1_0_0Modelo.InfoGuiaRemision();

            public List<GuiaRemision_1_0_0Modelo.Destinatario> Destinatarios { get; set; } = new List<GuiaRemision_1_0_0Modelo.Destinatario>();

            public GuiaRemision() => this.TipoDocumento = EnumTipoDocumento.GuiaRemision;
        }

        public class InfoGuiaRemision
        {
            public string DireccionPartida { get; set; }

            public DateTime FechaFinTransporte { get; set; }

            public string Placa { get; set; }
        }

        public class Destinatario
        {
            public Sujeto SujetoDestinatario { get; set; }

            public string DireccionDestinatario { get; set; }

            public string MotivoTraslado { get; set; }

            public string DocAduaneroUnico { get; set; }

            public string CodEstablecimientoAduanero { get; set; }

            public string Ruta { get; set; }

            public DocumentoSustento DocumentoSustento { get; set; } = new DocumentoSustento();

            public string NumAutDocumentoSustento { get; set; }

            public List<DetalleDocumentoItem> Detalles { get; set; } = new List<DetalleDocumentoItem>();
        }
    }
}
