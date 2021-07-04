using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;

namespace Yachasoft.Sri.Modelos
{
    public class ComprobanteRetencion_1_0_0Modelo
    {
        public static readonly EnumTipoDocumento TipoDocumento = EnumTipoDocumento.ComprobanteRetencion;

        public class ComprobanteRetencion : Documento
        {
            public ComprobanteRetencion_1_0_0Modelo.InfoCompRetencion InfoCompRetencion { get; set; } = new ComprobanteRetencion_1_0_0Modelo.InfoCompRetencion();

            public List<ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion> Impuestos { get; set; } = new List<ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion>();

            public ComprobanteRetencion() => this.TipoDocumento = EnumTipoDocumento.ComprobanteRetencion;
        }

        public class InfoCompRetencion
        {
            public string PeriodoFiscal { get; set; }
        }

        public abstract class ImpuestoRetencion : Impuesto
        {
            public DocumentoSustento DocumentoSustento { get; set; } = new DocumentoSustento();
        }

        public class ImpuestoRenta : ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion
        {
            public EnumTipoRetencion Codigo;

            public EnumTipoRetencionRenta CodigoRetencion { get; set; }
        }

        public class ImpuestoIVA : ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion
        {
            public EnumTipoRetencion Codigo = EnumTipoRetencion.IVA;

            public EnumTipoRetencionIVA CodigoRetencion { get; set; }
        }

        public class ImpuestoISD : ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion
        {
            public EnumTipoRetencion Codigo = EnumTipoRetencion.ISD;

            public EnumTipoRetencionISD CodigoRetencion { get; set; }
        }
    }
}
