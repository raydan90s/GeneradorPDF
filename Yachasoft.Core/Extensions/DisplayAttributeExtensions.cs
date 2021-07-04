using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Core.Extensions
{
    public static class DisplayAttributeExtensions
    {
        public static DisplayAttribute GetDisplayAttribute(object value) => DisplayAttributeExtensions.GetDisplayAttribute(value.GetType(), value);

        public static DisplayAttribute GetDisplayAttribute(Type type, object value)
        {
            return ((IEnumerable<MemberInfo>)type.GetMember(value.ToString())).FirstOrDefault<MemberInfo>().GetCustomAttribute<DisplayAttribute>();
        }

        public static DisplayAttribute GetDisplayAttribute(PropertyInfo property) => property.GetCustomAttribute<DisplayAttribute>();
    }
}
