using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGrid_Test.Requests
{
    public class GetAllSpamEmailsRequest : Request
    {
        public readonly SendGridClient.Method method;
        public readonly string urlPath;
        public readonly string requestBody = null;
        public readonly string queryParams;

        public GetAllSpamEmailsRequest(SendGridClient.Method method, string urlPath, string queryParams)
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
