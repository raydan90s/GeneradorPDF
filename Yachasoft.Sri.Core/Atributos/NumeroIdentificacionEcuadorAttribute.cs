using System;
using System.ComponentModel.DataAnnotations;
using Yachasoft.Sri.Core.Helpers;

namespace Yachasoft.Sri.Core.Atributos
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NumeroIdentificacionEcuadorAttribute : DataTypeAttribute
    {
        public NumeroIdentificacionEcuadorAttribute()
          : base(DataType.Custom)
        {
        }

        public override bool IsValid(object value) => NumeroIdentificacionEcuadorHelper.EsValida(value.ToString(), out string _);
    }
}
