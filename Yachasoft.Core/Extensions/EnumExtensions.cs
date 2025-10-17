using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Core.Attributes;

namespace Yachasoft.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetCode(this Enum enumerate) => ExternalCodeAttributeExtensions.GetExternalCodeAttribute((object)enumerate)?.Code;

        public static List<TEnum> GetValid<TEnum>(
          DateTime date,
          Expression<Func<ExternalCodeAttribute, bool>> expression = null)
          where TEnum : Enum
        {
            Func<ExternalCodeAttribute, bool> func = (Func<ExternalCodeAttribute, bool>)null;
            if (expression != null)
                func = expression.Compile();
            List<TEnum> enumList = new List<TEnum>();
            foreach (object obj in Enum.GetValues(typeof(TEnum)))
            {
                ExternalCodeAttribute externalCodeAttribute = ExternalCodeAttributeExtensions.GetExternalCodeAttribute(obj);
                if ((string.IsNullOrWhiteSpace(externalCodeAttribute.ValidFromDate) && (string.IsNullOrWhiteSpace(externalCodeAttribute.ValidToDate) || date <= DateTime.Parse(externalCodeAttribute.ValidToDate)) || date >= DateTime.Parse(externalCodeAttribute.ValidFromDate) && (string.IsNullOrWhiteSpace(externalCodeAttribute.ValidToDate) || date <= DateTime.Parse(externalCodeAttribute.ValidToDate))) && (func == null || func(externalCodeAttribute)))
                    enumList.Add((TEnum)obj);
            }
            return enumList;
        }

        public static string GetDisplayName(this Enum enumerate) => DisplayAttributeExtensions.GetDisplayAttribute((object)enumerate)?.GetName();
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            return enumVal.GetType()
                        .GetField(enumVal.ToString())
                        .GetCustomAttributes(typeof(T), false)
                        .FirstOrDefault() as T;
        }
    }
}
