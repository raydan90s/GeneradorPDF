using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Sri.Core.Helpers
{
    public static class StringHelper
    {
        public static string ToSRIFecha(this DateTime fecha) => fecha.ToString("dd/MM/yyyy");
    }
}
