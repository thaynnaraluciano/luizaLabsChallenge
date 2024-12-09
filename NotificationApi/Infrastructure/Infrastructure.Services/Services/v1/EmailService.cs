using CrossCutting.Configuration;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces.v1;
using MailKit.Net.Smtp;
using MimeKit;

namespace Infrastructure.Services.Services.v1
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(EmailModel request)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(AppSettings.Settings.Smtp.MailFromName, AppSettings.Settings.Smtp.MailFromEmail));
                message.To.Add(new MailboxAddress(request.ReceiverName, request.ReceiverEmail));
                message.Subject = request.Subject;

                message.Body = new TextPart(request.BodyType)
                {
                    Text = request.Body
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(AppSettings.Settings.Smtp.Host, AppSettings.Settings.Smtp.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(AppSettings.Settings.Smtp.MailFromEmail, AppSettings.Settings.Smtp.Password);
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
