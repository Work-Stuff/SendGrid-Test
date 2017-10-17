using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;

namespace SendGrid_Test
{
    public abstract class Request
    {
        public abstract SendGridClient.Method Method { get; }
        public abstract string URLPath { get; }
        public abstract string RequestBody { get; }
        public abstract string QueryParams { get; }
    }
}
