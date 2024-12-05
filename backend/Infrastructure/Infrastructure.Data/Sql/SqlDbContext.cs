using Domain.Entities.v1;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Sql
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserEntity> Users {  get; set; }
    }
}
