using AutoMapper;
using Domain.Commands.v1;
using Infrastructure.Data.Models;

namespace Domain.MapperProfiles
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<EmailModel, SendEmailCommand>();
            CreateMap<SendEmailCommand, EmailModel>();
        }
    }
}
