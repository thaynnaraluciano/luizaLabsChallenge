using Infrastructure.Data.Models;

namespace Infrastructure.Services.Interfaces.v1
{
    public interface IEmailService
    {
        Task SendEmail(EmailModel request);
    }
}
