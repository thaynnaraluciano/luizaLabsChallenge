using System.Security.Claims;

namespace Infrastructure.Services.Interfaces.v1
{
    public interface ITokenService
    {
        string GenerateToken(string username);
    }
}
