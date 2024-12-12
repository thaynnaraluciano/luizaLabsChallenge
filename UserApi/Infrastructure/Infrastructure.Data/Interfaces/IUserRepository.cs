using Infrastructure.Data.Models;

namespace Infrastructure.Data.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(UserModel user);

        bool UserNameAlreadyExists(string userName);

        bool EmailAlreadyExists(string email);

        UserModel? GetUserByUsername(string? username);

        UserModel? GetUserByVerificationCode(string?  verificationCode);

        Task ConfirmUserEmail(UserModel user);

        UserModel? GetUserByEmail(string? email);
    }
}
