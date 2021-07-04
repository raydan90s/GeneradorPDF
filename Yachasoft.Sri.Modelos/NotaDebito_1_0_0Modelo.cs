using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos.Base;

namespace Yachasoft.Sri.Modelos
{
    public class NotaDebito_1_0_0Modelo
    {
        public static readonly EnumTipoDocumento TipoDocumento = EnumTipoDocumento.NotaDebito;

        public class NotaDebito : Documento
        {
            public NotaDebito_1_0_0Modelo.InfoNotaDebito InfoNotaDebito { get; set; } = new NotaDebito_1_0_0Modelo.InfoNotaDebito();

            public List<Motivo> Motivos { get; set; } = new List<Motivo>();

            public NotaDebito() => this.TipoDocumento = EnumTipoDocumento.NotaDebito;
        }

        public class InfoNotaDebito
        {
            public DocumentoSustento DocumentoModificado { get; set; } = new DocumentoSustento();

            public Decimal TotalSinImpuestos { get; set; }

            public List<ImpuestoVenta> Impuestos { get; set; } = new List<ImpuestoVenta>();

            public Decimal ValorTotal { get; set; }

            public List<List<Pago>> Pagos { get; set; } = new List<List<Pago>>();
        }
    }
}
