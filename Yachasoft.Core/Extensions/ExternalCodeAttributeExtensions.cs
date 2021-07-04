using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Core.Attributes;

namespace Yachasoft.Core.Extensions
{
    public static class ExternalCodeAttributeExtensions
    {
        public static ExternalCodeAttribute GetExternalCodeAttribute(object value) => ExternalCodeAttributeExtensions.GetExternalCodeAttribute(value.GetType(), value);

        public static ExternalCodeAttribute GetExternalCodeAttribute(
          Type type,
          object value)
        {
            Type type1 = Nullable.GetUnderlyingType(type);
            if ((object)type1 == null)
                type1 = type;
            type = type1;
            return ((IEnumerable<MemberInfo>)type.GetMember(value.ToString())).FirstOrDefault<MemberInfo>().GetCustomAttribute<ExternalCodeAttribute>();
        }

        public static ExternalCodeAttribute GetExternalCodeAttribute(
          PropertyInfo property)
        {
            return property.GetCustomAttribute<ExternalCodeAttribute>();
        }
    }
}
