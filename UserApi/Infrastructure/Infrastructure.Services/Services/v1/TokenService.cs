using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CrossCutting.Configuration;
using Infrastructure.Services.Interfaces.v1;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Services.v1
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;

        public TokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public string GenerateToken(string username)
        {
            try
            {
                var jwtSettings = _appSettings.Jwt;

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new JwtSecurityToken(
                    issuer: jwtSettings.Issuer,
                    audience: jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationInMinutes),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            }
            catch (Exception ex) 
            {
                throw new Exception($"An error occurred while generating jwt token: {ex.Message}");
            }
        }
    }
}
