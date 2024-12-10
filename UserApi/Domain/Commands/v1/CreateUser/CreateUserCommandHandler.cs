using AutoMapper;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Infrastructure.Services.Interfaces.v1;
using System.Text;
using System.Security.Cryptography;

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

            var userEntity = _mapper.Map<UserModel>(command);
            userEntity.VerificationCode = GenerateVerificationCode(command.Email!);

            await _userRepository.CreateUser(userEntity);

            _logger.LogInformation($"Sending email confirmation to {command.Email}");

            await _notificationService.SendEmail(new SendEmailModel()
            {
                ReceiverName = command.UserName,
                ReceiverEmail = command.Email,
                Subject = "Confirmação de email LuizaLabs",
                BodyType = "html",
                Body = _emailTemplateService.GenerateConfirmationEmail(command.UserName!)
            });

            _logger.LogInformation("Confirmation email was sent and user was created successfully");
            return Unit.Value;
        }

        private string GenerateVerificationCode(string email)
        {
            string input = $"{email}-{Guid.NewGuid()}";

            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
