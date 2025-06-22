using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Back.Modules.Notification.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _username;
        private readonly string _password;
        private readonly string _from;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
            var emailConfig = _configuration.GetSection("Email");

            _smtpHost = emailConfig["SmtpHost"];
            _smtpPort = int.Parse(emailConfig["SmtpPort"]);
            _username = emailConfig["Username"];
            _password = emailConfig["Password"];
            _from = emailConfig["FromAddress"];
        }

        public async Task SendNotificationAsync(NotificationType type, string toEmail, object dataModel)
        {
            var subject = GetSubject(type);
            var bodyHtml = await GetHtmlContentAsync(type, dataModel);

            using var smtpClient = new SmtpClient(_smtpHost, _smtpPort)
            {
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress(_from),
                Subject = subject,
                Body = bodyHtml,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8
            };

            message.To.Add(toEmail);

            await smtpClient.SendMailAsync(message);
        }

        private string GetSubject(NotificationType type) => type switch
        {
            NotificationType.ProductExpiration => "Product Expiration Notice",
            NotificationType.LicensePaymentSuccess => "License Payment Successful",
            NotificationType.SubscriptionPaymentSuccess => "Subscription Payment Successful",
            NotificationType.PaymentReminder => "Upcoming Payment Reminder",
            _ => "Notification"
        };

        private async Task<string> GetHtmlContentAsync(NotificationType type, object model)
        {
            var basePath = AppContext.BaseDirectory;
            var templatePath = Path.Combine(basePath, "Modules", "Notification", "Templates", $"{type}.html");

            if (!File.Exists(templatePath))
                return $"<html><body><p>Template for {type} not found.</p></body></html>";

            var htmlContent = await File.ReadAllTextAsync(templatePath);
            return ReplacePlaceholders(htmlContent, model);
        }


        private string ReplacePlaceholders(string template, object model)
        {
            var result = template;
            foreach (var prop in model.GetType().GetProperties())
            {
                var value = prop.GetValue(model)?.ToString() ?? "";
                result = result.Replace($"{{{{{prop.Name}}}}}", value);
            }
            return result;
        }
    }
}