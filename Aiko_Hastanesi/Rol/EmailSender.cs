using Microsoft.AspNetCore.Identity.UI.Services;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace Aiko_Hastanesi.Rol
{
    public class EmailSender : IEmailSender
    {
        private readonly string _apiKey;
        private readonly string _apiSecret;
        private readonly IConfiguration _conf;

        public EmailSender(IConfiguration config)
        {
            _apiKey = config.GetValue<string>("Mailjet:ApiKey") ?? "29bb69f812abf4a6411dfc4e55894285";
            _apiSecret = config.GetValue<string>("Mailjet:ApiSecret") ?? "b89ea0880aac24264c20ef72463a5b35";
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                MailjetClient client = new MailjetClient(_apiKey, _apiSecret);
                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource,
                }
                    .Property(Send.FromEmail, "lussnanith777@gmail.com")
                    .Property(Send.FromName, "Aiko Hastanesi")
                    .Property(Send.Subject, subject)
                    .Property(Send.HtmlPart, htmlMessage)
                    .Property(Send.Recipients, new JArray {
                        new JObject {
                            {"Email", email}
                        }
                    });

                MailjetResponse response = await client.PostAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Email sent successfully to {email}");
                }
                else
                {
                    Console.WriteLine($"Email sending failed. Status code: {response.StatusCode}, Error: {response.GetErrorMessage()}");
                    throw new Exception($"Email sending failed: {response.GetErrorMessage()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                throw;
            }
        }
    }
}