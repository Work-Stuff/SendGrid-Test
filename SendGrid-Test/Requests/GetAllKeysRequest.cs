using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;

namespace SendGrid_Test.Requests
{
    public class GetAllKeysRequest : Request
    {
        public readonly SendGridClient.Method method;
        public readonly string urlPath;
        public readonly string requestBody = string.Empty;
        public readonly string queryParams;

        public GetAllKeysRequest(SendGridClient.Method method, string urlPath, string queryParams)
        {
            this.method = method;
            this.urlPath = urlPath;
            this.queryParams = queryParams;
        }

        public override SendGridClient.Method Method => method;

        public override string URLPath => urlPath;

        public override string RequestBody => requestBody;

        public override string QueryParams => queryParams;
    }
}
