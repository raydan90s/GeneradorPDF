using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ExternalCodeAttribute : Attribute
    {
        public string Code { get; }

        public string ValidFromDate { get; set; }

        public string ValidToDate { get; set; }

        public ExternalCodeAttribute(string code) => this.Code = code;
    }
}
