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
            CreateMap<CreateUserCommand, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmedAt, opt => opt.Ignore())
                .ForMember(dest => dest.VerificationCode, opt => opt.Ignore());
        }
    }
}
