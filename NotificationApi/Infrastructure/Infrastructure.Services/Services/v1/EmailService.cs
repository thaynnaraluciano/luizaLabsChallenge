using CrossCutting.Configuration;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces.v1;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Services.v1
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;

        public EmailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public async Task SendEmail(EmailModel request)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_appSettings.Smtp.MailFromName, _appSettings.Smtp.MailFromEmail));
                message.To.Add(new MailboxAddress(request.ReceiverName, request.ReceiverEmail));
                message.Subject = request.Subject;

                message.Body = new TextPart(request.BodyType)
                {
                    Text = request.Body
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(_appSettings.Smtp.Host, _appSettings.Smtp.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_appSettings.Smtp.MailFromEmail, _appSettings.Smtp.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }
    }
}
