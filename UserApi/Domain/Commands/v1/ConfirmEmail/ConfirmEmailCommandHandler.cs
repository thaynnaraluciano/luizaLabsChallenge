using CrossCutting.Exceptions;
using Infrastructure.Data.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Domain.Commands.v1.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Unit>
    {
        private readonly ILogger<ConfirmEmailCommandHandler> _logger;
        private readonly IUserRepository _userRepository;

        public ConfirmEmailCommandHandler(ILogger<ConfirmEmailCommandHandler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Confirming user email");

            var user = _userRepository.GetUserByVerificationCode(command.VerificationCode);

            if (user == null)
                throw new NotFoundException("Usuário não encontrado.");

            if (user.ConfirmedAt.HasValue)
                throw new BadRequestException("Este email já foi validado.");

            if (string.Equals(user.VerificationCode, command.VerificationCode))
            {
                user.ConfirmedAt = DateTime.Now;
                await _userRepository.ConfirmUserEmail(user);

                _logger.LogInformation("User email confirmed");
            }

            return Unit.Value;
        }
    }
}
