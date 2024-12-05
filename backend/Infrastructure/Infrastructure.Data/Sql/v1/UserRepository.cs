using Domain.Entities.v1;
using Domain.Interfaces.v1.Repositories.Sql;

namespace Infrastructure.Data.Sql.v1
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlDbContext _dbContext;

        public UserRepository(SqlDbContext context)
        {
            _dbContext = context;
        }

        public async Task CreateUser(UserEntity user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
