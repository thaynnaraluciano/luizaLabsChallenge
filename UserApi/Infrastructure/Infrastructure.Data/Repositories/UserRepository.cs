using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlDbContext _dbContext;

        public UserRepository(SqlDbContext context)
        {
            _dbContext = context;
        }

        public async Task CreateUser(UserModel user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return;
        }

        public bool UserNameAlreadyExists(string userName) 
        {
            return _dbContext.Users.Any(user => string.Equals(user.UserName, userName));
        }

        public bool EmailAlreadyExists(string email)
        {
            return _dbContext.Users.Any(user => string.Equals(user.Email, email));
        }

        public UserModel? GetUserByUsername(string? username)
        {
            if (username != null)
                return _dbContext.Users.FirstOrDefault(x => string.Equals(x.UserName, username));

            return null;
        }

        public UserModel? GetUserByVerificationCode(string? verificationCode)
        {
            if (verificationCode != null)
                return _dbContext.Users.FirstOrDefault(x => string.Equals(x.VerificationCode, verificationCode));

            return null;
        }

        public async Task ConfirmUserEmail(UserModel user)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();

            return;
        }
    }
}
