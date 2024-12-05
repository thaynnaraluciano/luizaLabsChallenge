using Domain.Commands.v1.CreateUser;
using Domain.Entities.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.v1.Repositories.Sql
{
    public interface IUserRepository
    {
        Task CreateUser(UserEntity user);
    }
}
