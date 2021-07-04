using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Sri.WebService.Response
{
    public class Response<TResponse>
    {
        public TResponse Data { get; set; }

        public string Error { get; set; }

        public bool Ok { get; set; }
    }
}
