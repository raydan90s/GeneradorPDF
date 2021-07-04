using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Sri.WebService.Request
{
    public static class RequestHelper
    {
        public static Stream GetRequestStream(string relativeFileName)
        {
            string name = ((IEnumerable<string>)Assembly.GetExecutingAssembly().GetManifestResourceNames()).FirstOrDefault<string>((Func<string, bool>)(p => p.EndsWith(relativeFileName)));
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
        }
    }
}
