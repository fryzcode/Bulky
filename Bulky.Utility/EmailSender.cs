using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace Bulky.Utility;

public class EmailSender : IEmailSender
{
    private readonly string BrevoApiKey;

    public EmailSender(IConfiguration _config) {
        BrevoApiKey = _config.GetValue<string>("Brevo:ApiKey");
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("api-key", BrevoApiKey);

            var emailData = new
            {
                sender = new { email = "feryazhajimuradov18@gmail.com", name = "Bulky Book" },
                to = new[] { new { email = email } },
                subject = subject,
                htmlContent = htmlMessage
            };

            var content = new StringContent(JsonSerializer.Serialize(emailData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.brevo.com/v3/smtp/email", content);

            if (!response.IsSuccessStatusCode)
            {
                // Логирование ошибки или дополнительная обработка
                throw new Exception($"Ошибка при отправке email: {response.StatusCode}");
            }
        }
    }
}