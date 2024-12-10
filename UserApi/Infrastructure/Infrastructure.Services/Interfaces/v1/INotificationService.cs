using Infrastructure.Data.Models;

namespace Infrastructure.Services.Interfaces.v1
{
    public interface INotificationService
    {
        Task<bool> SendEmail(SendEmailModel request);
    }
}
