using AutoMapper;
using Domain.Commands.v1.CreateUser;
using Domain.Entities.v1;

namespace Domain.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, CreateUserCommand>();
            CreateMap<CreateUserCommand, UserEntity>();
        }
    }
}
