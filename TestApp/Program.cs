using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid_Test;
using SendGrid_Test.Requests;
using SendGrid_Test.Extensions;
using System;
using System.Threading.Tasks;

namespace TestApp
{
     public class Program
    {
        private static void Main()
        {
            SendEmail().Wait();
            string bounces = GetAllBounces(DateTime.MinValue, DateTime.MaxValue).Result;
            bool bounced = DidEmailBounce("bounce1@test.com").Result;
            Console.WriteLine("Did Email Bounce: " + bounced);
            DeleteBounce("bounce1@test.com").Wait();
            bounced = DidEmailBounce("bounce1@test.com").Result;
            Console.WriteLine("Did Email Bounce: " + bounced);
            Console.ReadKey();
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
            return "";
        }

        static async Task<bool> DidEmailBounce(string email)
        {
            string key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            SendGridClient client = new SendGridClient(key);

            SendGridClient.Method method = SendGridClient.Method.GET;
            CheckBounceRequest request = new CheckBounceRequest(method, @"suppression/bounces/" + email);
            Response response = await client.RequestAsync(request);
            string responseString = await response.Body.ReadAsStringAsync();
            return false;
            
        }

        static async Task DeleteBounce(string email)
        {
            string key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            SendGridClient client = new SendGridClient(key);

            SendGridClient.Method method = SendGridClient.Method.POST;
            string queryParams = @"{
                'email-address': '" + email + "'" +
                "}";
            DeleteBounceRequest request = new DeleteBounceRequest(method, @"suppression/bounces/" + email, queryParams);
        }

        static async Task SendEmail()
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            SendGridClient client = new SendGridClient(apiKey);
            EmailAddress from = new EmailAddress("bounce1@test.com", "Example User");
            string subject = "Sending with SendGrid is Fun";
            EmailAddress to = new EmailAddress("bounce1@test.com", "Example User");
            string plainTextContent = "and easy to do anywhere, even with C#";
            string htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            Response response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
            
        }
    }
}