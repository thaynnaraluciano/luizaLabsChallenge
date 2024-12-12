using CrossCutting.Exceptions;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.SendEmailConfirmation
{
    public class SendEmailConfirmationCommandHandler : IRequestHandler<SendEmailConfirmationCommand, Unit>
    {
        private readonly ILogger<SendEmailConfirmationCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly IEmailTemplateService _emailTemplateService;

        public SendEmailConfirmationCommandHandler(
            ILogger<SendEmailConfirmationCommandHandler> logger,
            IUserRepository userRepository,
            INotificationService notificationService,
            IEmailTemplateService emailTemplateService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _notificationService = notificationService;
            _emailTemplateService = emailTemplateService;
        }

        public async Task<Unit> Handle(SendEmailConfirmationCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Sending email confirmation to: {command.Email}");

            var user = _userRepository.GetUserByEmail(command.Email);

            if (user == null)
            {
                _logger.LogError("User not found at database");
                throw new NotFoundException("Usuário não encontrado.");
            }

            if (user.ConfirmedAt.HasValue)
            {
                _logger.LogError("User email is already confirmed");
                throw new BadRequestException("Este email já foi validado.");
            }

            await _notificationService.SendEmail(new SendEmailModel()
            {
                ReceiverName = user.UserName,
                ReceiverEmail = command.Email,
                Subject = "Confirmação de email LuizaLabs",
                BodyType = "html",
                Body = _emailTemplateService.GenerateConfirmationEmail(user.UserName!, user.VerificationCode!)
            });

            _logger.LogInformation("Confirmation email was sent successfully");
            return Unit.Value;
        }
    }
}
