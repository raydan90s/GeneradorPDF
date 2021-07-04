using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Core.Extensions;
using Yachasoft.Sri.Core.Atributos;
using Yachasoft.Sri.Core.Extensions;

namespace Yachasoft.Sri.Modelos.Enumerados
{
    public static class EnumExtensions
    {
        public static string ObtenerSRICodigo(this Enum enumerado) => enumerado.GetCode();

        public static double? ObtenerSRIPorcentaje(this Enum enumerado) => SRICodigoAttributeExtensions.GetSRICodigoAttribute((object)enumerado)?.Porcentaje;

        public static string ObtenerSRITarifaEspecifica(this Enum enumerado)
        {
            SRICodigoAttribute sriCodigoAttribute = SRICodigoAttributeExtensions.GetSRICodigoAttribute((object)enumerado);
            string especifica9MayAdic2020 = sriCodigoAttribute?.TarifaEspecifica9MayADic2020;
            if (especifica9MayAdic2020 != null)
                return especifica9MayAdic2020;
            return sriCodigoAttribute?.TarifaEspecificaEnDic2020;
        }

        public static string ObtenerSRIDescripcion(this Enum enumerado) => enumerado.GetDisplayName() ?? enumerado.ToString();

        public static double ObtenerSRIValor(this Enum enumerado)
        {
            SRICodigoAttribute sriCodigoAttribute = SRICodigoAttributeExtensions.GetSRICodigoAttribute((object)enumerado);
            return sriCodigoAttribute != null ? sriCodigoAttribute.Porcentaje : 0.0;
        }
    }
}
