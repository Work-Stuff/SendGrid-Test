using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGrid_Test.Requests
{
    public class DeleteAllInvalidEmailsRequest : Request
    {
        public readonly SendGridClient.Method method;
        public readonly string urlPath;
        public readonly string requestBody;
        public readonly string queryParams = null;

        public DeleteAllInvalidEmailsRequest(SendGridClient.Method method, string urlPath, string requestBody)
        {
            this.method = method;
            this.urlPath = urlPath;
            this.requestBody = requestBody;
        }

        public override SendGridClient.Method Method => method;

        public override string URLPath => urlPath;

        public override string RequestBody => requestBody;

        public override string QueryParams => queryParams;
    }
}
