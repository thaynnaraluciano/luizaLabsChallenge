using AutoMapper;
using Domain.Commands.v1.CreateUser;
using Infrastructure.Data.Models;

namespace Domain.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, CreateUserCommand>();
            CreateMap<CreateUserCommand, UserModel>();
        }
    }
}
