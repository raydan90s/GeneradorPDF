using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;

namespace Yachasoft.Sri.Modelos.Atributos
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class SRIValidoEnDocumentoAttribute : Attribute
    {
        public SRIValidoEnDocumentoAttribute()
        {
        }

        public SRIValidoEnDocumentoAttribute(EnumTipoDocumento documento) => this.Documento = documento;

        public EnumTipoDocumento Documento { get; set; }
    }
}
