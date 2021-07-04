using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;

namespace Yachasoft.Sri.Modelos.Extensions
{
    public static class DocumentoExtensions
    {
        public static string NumeroDocumentoSRI(this Documento documento) => documento == null ? (string)null : string.Format("{0:D3}-{1:D3}-{2:D9}", (object)documento.PuntoEmision.Establecimiento.Codigo, (object)documento.PuntoEmision.Codigo, (object)documento.InfoTributaria.Secuencial);

        public static EnumTipoImpuestoIVA GetTipoImpuestoIVAGravadoVigente(
          this Documento documento)
        {
            return Yachasoft.Core.Extensions.EnumExtensions.GetValid<EnumTipoImpuestoIVA>(documento.FechaEmision).Where<EnumTipoImpuestoIVA>((Func<EnumTipoImpuestoIVA, bool>)(x =>
            {
                double? nullable = x.ObtenerSRIPorcentaje();
                double num = 0.0;
                return nullable.GetValueOrDefault() > num & nullable.HasValue;
            })).FirstOrDefault<EnumTipoImpuestoIVA>();
        }
    }
}
