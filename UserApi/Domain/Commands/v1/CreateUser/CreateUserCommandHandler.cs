using AutoMapper;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Infrastructure.Services.Interfaces.v1;

namespace Domain.Commands.v1.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICryptograpghyService _cryptograpghyService;
        private readonly INotificationService _notificationService;
        private readonly IEmailTemplateService _emailTemplateService;

        public CreateUserCommandHandler(
            ILogger<CreateUserCommandHandler> logger, 
            IUserRepository userRepository, 
            IMapper mapper, 
            ICryptograpghyService cryptograpghyService,
            INotificationService notificationService,
            IEmailTemplateService emailTemplateService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
            _cryptograpghyService = cryptograpghyService;
            _notificationService = notificationService;
            _emailTemplateService = emailTemplateService;
        }

        public async Task<Unit> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Creating user");

            var hashedPassword = _cryptograpghyService.HashPassword(command.Password);
            command.Password = hashedPassword;

            // TO DO: gerar codigo de confirmação e inserir no model pra salvar

            var userEntity = _mapper.Map<UserModel>(command);
            await _userRepository.CreateUser(userEntity);

            _logger.LogInformation("Sending email confirmation");

            await _notificationService.SendEmail(new SendEmailModel()
            {
                ReceiverName = command.UserName,
                ReceiverEmail = command.Email,
                Subject = "Confirmação de email LuizaLabs",
                BodyType = "html",
                Body = _emailTemplateService.GenerateConfirmationEmail(command.UserName!)
            });

            _logger.LogInformation("User created successfully");
            return Unit.Value;
        }
    }
}
