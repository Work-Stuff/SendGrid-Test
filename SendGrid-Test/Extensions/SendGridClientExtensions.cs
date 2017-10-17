using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid_Test.Requests;

namespace SendGrid_Test.Extensions
{
    public static class SendGridClientExtensions
    {
        public static async Task<Response> RequestAsync(this SendGridClient client, CreateKeyRequest request)
        {
            return await client.RequestAsync(request.Method, request.URLPath, request.RequestBody);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, GetAllKeysRequest request)
        {
            return await client.RequestAsync(request.Method, request.URLPath, request.QueryParams);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, GetExistingKeyRequest request)
        {
            return await client.RequestAsync(request.Method, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, CheckBounceRequest request)
        {
            return await client.RequestAsync(request.Method, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, DeleteBounceRequest request)
        {
            return await client.RequestAsync(request.method, request.URLPath, request.QueryParams);
        }
    }
}
