﻿using Infrastructure.Data.Interfaces;
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
        }
    }
}