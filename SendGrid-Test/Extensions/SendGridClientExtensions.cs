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
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, GetAllKeysRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, GetExistingKeyRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, CheckBounceRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, DeleteBounceRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, GetAllBouncesRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, DeleteAllBouncesRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, DeleteAllInvalidEmailsRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, DeleteInvalidEmailRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, GetAllInvalidEmailsRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, GetInvalidEmailRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, DeleteAllSpamEmailsRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, DeleteSpamEmailRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, GetSpamEmailRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }

        public static async Task<Response> RequestAsync(this SendGridClient client, GetAllSpamEmailsRequest request)
        {
            return await client.RequestAsync(request.method, request.RequestBody, request.QueryParams, request.URLPath);
        }
    }
}
