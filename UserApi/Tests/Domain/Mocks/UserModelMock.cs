using Infrastructure.Data.Models;

namespace Tests.Domain.Mocks
{
    public class UserModelMock
    {
        public static UserModel GetInstance(string email, DateTime? confirmedAt, string password, string username, string verificationCode = null)
        {
            return new UserModel()
            {
                Email = email,
                ConfirmedAt = confirmedAt,
                Password = password,
                UserName = username,
                VerificationCode = verificationCode
            };
        }
    }
}
