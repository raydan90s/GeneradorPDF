using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Atributos;

namespace Yachasoft.Sri.Core.Extensions
{
    public static class SRICodigoAttributeExtensions
    {
        public static SRICodigoAttribute GetSRICodigoAttribute(object value) => SRICodigoAttributeExtensions.GetSRICodigoAttribute(value.GetType(), value);

        public static SRICodigoAttribute GetSRICodigoAttribute(Type type, object value) => ((IEnumerable<MemberInfo>)type.GetMember(value.ToString())).FirstOrDefault<MemberInfo>().GetCustomAttribute<SRICodigoAttribute>();

        public static SRICodigoAttribute GetSRICodigoAttribute(PropertyInfo property) => property.GetCustomAttribute<SRICodigoAttribute>();
    }
}
