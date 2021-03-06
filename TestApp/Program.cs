﻿using SendGrid;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using SendGrid_Test;
using SendGrid_Test.Requests;
using SendGrid_Test.Extensions;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SendGrid_Test.Responses;

namespace TestApp
{
     public class Program
    {
        private static void Main()
        {
            string address = "bounce0@test.com";
            // DeleteAllBounces(null).Wait();
            SendEmail(address).Wait();
            string bounces = GetAllBounces(DateTime.MinValue, DateTime.MaxValue).Result;
            BounceResponse[] response = JsonConvert.DeserializeObject<BounceResponse[]>(bounces);
            bool bounced = DidEmailBounce(address, DateTime.MinValue, DateTime.MaxValue).Result;
            if (bounced)
            {
                Console.WriteLine("Email Bounced Because: " + response.Last(x => x.Email.Equals(address)).Reason);
                DeleteBounce(address).Wait();
            }
            else
            {
                Console.WriteLine("Email Did Not Bounce");
            }
            EnumEmailState state = GetEmailState(address, DateTime.MinValue, DateTime.MaxValue).Result;
            Console.WriteLine("The State of the Email is: " + state);
            Console.ReadKey();
        }

        static async Task<EnumEmailState> GetEmailState(string email, DateTime startTime, DateTime endTime)
        {
            bool bounced = await DidEmailBounce(email, startTime, endTime);
            bool invalid = !(await IsEmailValid(email, startTime, endTime));
            bool spam = await IsEmailSpam(email, startTime, endTime);

            if (bounced)
                return EnumEmailState.BOUNCED;
            else if (invalid)
                return EnumEmailState.INVALID;
            else if (spam)
                return EnumEmailState.SPAM;
            else
                return EnumEmailState.SENT;
        }

        static async Task<string> GetAllBounces(DateTime startTime, DateTime endTime)
        {
            string key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            SendGridClient client = new SendGridClient(key);
            TimeSpan t = DateTime.UtcNow.AddDays(1) - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            string queryParams = "{ 'end_time': " + secondsSinceEpoch + ", 'start_time': 1 }";
            SendGridClient.Method method = SendGridClient.Method.GET;
            GetAllBouncesRequest request = new GetAllBouncesRequest(method, @"suppression/bounces", queryParams);
            Response response = await client.RequestAsync(request);
            string responseString = await response.Body.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
                return responseString;
            return "";
        }

        static async Task<string> GetAllSpamEmails(DateTime startTime, DateTime endTime)
        {
            string key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            SendGridClient client = new SendGridClient(key);
            TimeSpan t = DateTime.UtcNow.AddDays(1) - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            string queryParams = "{ 'end_time': " + secondsSinceEpoch + ", 'start_time': 1 }";
            SendGridClient.Method method = SendGridClient.Method.GET;
            GetAllSpamEmailsRequest request = new GetAllSpamEmailsRequest(method, @"suppression/spam_reports", queryParams);
            Response response = await client.RequestAsync(request);
            string responseString = await response.Body.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
                return responseString;
            return "";
        }


        static async Task<string> GetAllInvalidEmails(DateTime startTime, DateTime endTime)
        {
            string key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            SendGridClient client = new SendGridClient(key);
            TimeSpan t = DateTime.UtcNow.AddDays(1) - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            string queryParams = "{ 'end_time': " + secondsSinceEpoch + ", 'start_time': 1 }";
            SendGridClient.Method method = SendGridClient.Method.GET;
            GetAllInvalidEmailsRequest request = new GetAllInvalidEmailsRequest(method, @"suppression/invalid_emails", queryParams);
            Response response = await client.RequestAsync(request);
            string responseString = await response.Body.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
                return responseString;
            return "";
        }
        static async Task<bool> DidEmailBounce(string email, DateTime startTime, DateTime endTime)
        {
            string bounces = await GetAllBounces(startTime, endTime);
            BounceResponse[] response = JsonConvert.DeserializeObject<BounceResponse[]>(bounces);
            return response.Any(x => x.Email.Equals(email));
        }

        static async Task<bool> IsEmailSpam(string email, DateTime startTime, DateTime endTime)
        {
            string bounces = await GetAllBounces(startTime, endTime);
            SpamEmailResponse[] response = JsonConvert.DeserializeObject<SpamEmailResponse[]>(bounces);
            return response.Any(x => x.Email.Equals(email));
        }

        static async Task<bool> IsEmailValid(string email, DateTime startTime, DateTime endTime)
        {
            string bounces = await GetAllBounces(startTime, endTime);
            InvalidEmailResponse[] response = JsonConvert.DeserializeObject<InvalidEmailResponse[]>(bounces);
            return !response.Any(x => x.Email.Equals(email));
        }

        static async Task DeleteBounce(string email)
        {
            string key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            SendGridClient client = new SendGridClient(key);

            SendGridClient.Method method = SendGridClient.Method.GET;
            string queryParams = @"{
                'email-address': '" + email + "'" +
                "}";
            DeleteBounceRequest request = new DeleteBounceRequest(method, @"suppression/bounces/" + email, queryParams);
            Response response = await client.RequestAsync(request);
            string responseString = await response.Body.ReadAsStringAsync();
        }

        static async Task DeleteInvalidEmail(string email)
        {
            string key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            SendGridClient client = new SendGridClient(key);

            SendGridClient.Method method = SendGridClient.Method.GET;
            string queryParams = @"{
                'email-address': '" + email + "'" +
                                 "}";
            DeleteInvalidEmailRequest request = new DeleteInvalidEmailRequest(method, @"suppression/invalid_emails/" + email);
            Response response = await client.RequestAsync(request);
            string responseString = await response.Body.ReadAsStringAsync();
        }

        static async Task DeleteSpamEmail(string email)
        {
            string key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            SendGridClient client = new SendGridClient(key);

            SendGridClient.Method method = SendGridClient.Method.GET;
            string queryParams = @"{
                'email-address': '" + email + "'" +
                                 "}";
            DeleteSpamEmailRequest request = new DeleteSpamEmailRequest(method, @"suppression/spam_reports/" + email);
            Response response = await client.RequestAsync(request);
            string responseString = await response.Body.ReadAsStringAsync();
        }
        static async Task DeleteAllBounces(params string[] emails)
        {
            string key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            SendGridClient client = new SendGridClient(key);

            SendGridClient.Method method = SendGridClient.Method.DELETE;
            string requestBody;
            if (emails == null)
            {
                requestBody = "{ \"delete_all\": true }";
            }
            else
            {
                requestBody = "{ 'emails': [ ";
                for (int i = 0; i < emails.Length; i++)
                {
                    if (i == emails.Length - 1)
                        requestBody += "'" + emails[i] + "'";
                    else
                        requestBody += "'" + emails[i] + "'" + ", ";
                }
                requestBody += " ] }";
            }
            DeleteAllBouncesRequest request = new DeleteAllBouncesRequest(method, @"suppression/bounces", requestBody);
            Response response = await client.RequestAsync(request);
            string responseString = await response.Body.ReadAsStringAsync();
        }

        static async Task DeleteAllInvalidEmails(params string[] emails)
        {
            string key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            SendGridClient client = new SendGridClient(key);

            SendGridClient.Method method = SendGridClient.Method.DELETE;
            string requestBody;
            if (emails == null)
            {
                requestBody = "{ \"delete_all\": true }";
            }
            else
            {
                requestBody = "{ 'emails': [ ";
                for (int i = 0; i < emails.Length; i++)
                {
                    if (i == emails.Length - 1)
                        requestBody += "'" + emails[i] + "'";
                    else
                        requestBody += "'" + emails[i] + "'" + ", ";
                }
                requestBody += " ] }";
            }
            DeleteAllInvalidEmailsRequest request = new DeleteAllInvalidEmailsRequest(method, @"suppression/bounces", requestBody);
            Response response = await client.RequestAsync(request);
            string responseString = await response.Body.ReadAsStringAsync();
        }

        static async Task DeleteAllSpamEmails(params string[] emails)
        {
            string key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            SendGridClient client = new SendGridClient(key);

            SendGridClient.Method method = SendGridClient.Method.DELETE;
            string requestBody;
            if (emails == null)
            {
                requestBody = "{ \"delete_all\": true }";
            }
            else
            {
                requestBody = "{ 'emails': [ ";
                for (int i = 0; i < emails.Length; i++)
                {
                    if (i == emails.Length - 1)
                        requestBody += "'" + emails[i] + "'";
                    else
                        requestBody += "'" + emails[i] + "'" + ", ";
                }
                requestBody += " ] }";
            }
            DeleteAllSpamEmailsRequest request = new DeleteAllSpamEmailsRequest(method, @"suppression/spam_reports", requestBody);
            Response response = await client.RequestAsync(request);
            string responseString = await response.Body.ReadAsStringAsync();
        }
        static async Task SendEmail(string address)
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            SendGridClient client = new SendGridClient(apiKey);
            EmailAddress from = new EmailAddress(address, "Example User");
            string subject = "Sending with SendGrid is Fun";
            EmailAddress to = new EmailAddress(address, "Example User");
            string plainTextContent = "and easy to do anywhere, even with C#";
            string htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            Response response = await client.SendEmailAsync(msg);
            if(response.StatusCode == HttpStatusCode.Accepted)
                Console.WriteLine("Email Was Successfully Sent!");
            else
                Console.WriteLine("Error Sending Email: " + response.StatusCode);
        }
    }
}