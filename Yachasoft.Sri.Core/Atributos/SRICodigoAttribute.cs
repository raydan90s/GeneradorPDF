using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Core.Attributes;

namespace Yachasoft.Sri.Core.Atributos
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SRICodigoAttribute : ExternalCodeAttribute
    {
        public SRICodigoAttribute(string code)
          : base(code)
        {
        }

        public double Porcentaje { get; set; }

        public string TarifaEspecificaEnDic2020 { get; set; }

        public string TarifaEspecifica9MayADic2020 { get; set; }

        public string PorcentajesVigentes { get; set; }
    }
}
