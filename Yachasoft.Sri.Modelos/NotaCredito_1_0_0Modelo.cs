using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos.Base;

namespace Yachasoft.Sri.Modelos
{
    public class NotaCredito_1_0_0Modelo
    {
        public static readonly EnumTipoDocumento TipoDocumento = EnumTipoDocumento.NotaCredito;

        public class NotaCredito : Documento
        {
            public NotaCredito_1_0_0Modelo.InfoNotaCredito InfoNotaCredito { get; set; } = new NotaCredito_1_0_0Modelo.InfoNotaCredito();

            public List<DetalleDocumentoItemPrecio> Detalles { get; set; } = new List<DetalleDocumentoItemPrecio>();

            public NotaCredito() => this.TipoDocumento = EnumTipoDocumento.NotaCredito;
        }

        public class InfoNotaCredito
        {
            public DocumentoSustento DocumentoModificado { get; set; } = new DocumentoSustento();

            public Decimal TotalSinImpuestos { get; set; }

            public Decimal ValorModificacion { get; set; }

            public string Moneda { get; set; } = "DOLAR";

            public List<ImpuestoVenta> TotalConImpuestos { get; set; } = new List<ImpuestoVenta>();

            public string Motivo { get; set; }
        }
    }
}
