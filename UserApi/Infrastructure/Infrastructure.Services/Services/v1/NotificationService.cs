using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces.v1;
using System.Net.Http.Json;

namespace Infrastructure.Services.Services.v1
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _emailEndpoint;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _emailEndpoint = "api/v1/email";
        }

        public async Task<bool> SendEmail(SendEmailModel request)
        {
            var response = await _httpClient.PostAsJsonAsync(_emailEndpoint, request);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"An error occurred while sending confirmation email: {await response.Content.ReadAsStringAsync()}");

            return true;
        }
    }
}
