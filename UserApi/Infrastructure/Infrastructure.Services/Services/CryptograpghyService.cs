using Infrastructure.Services.Interfaces.v1;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services.Services
{
    public class CryptograpghyService : ICryptograpghyService
    {
        public string HashPassword(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("A senha não pode ser nula ou vazia.", nameof(password));

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                StringBuilder hashBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashBuilder.Append(b.ToString("x2"));
                }

                return hashBuilder.ToString();
            }
        }
    }
}
