using CrossCutting.Exceptions;
using Infrastructure.Data.Interfaces;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly ILogger<LoginCommandHandler> _logger;

        private readonly ITokenService _tokenService;
        private readonly ICryptograpghyService _cryptograpghyService;
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(
            ILogger<LoginCommandHandler> logger,
            ICryptograpghyService cryptograpghyService,
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _logger = logger;
            _cryptograpghyService = cryptograpghyService;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting user login");

            var user = _userRepository.GetUserByUsername(command.UserName);
            var hashedPassword = _cryptograpghyService.HashPassword(command.Password);

            if (user == null || hashedPassword != user.Password)
                throw new NotAuthorizedException("Usuário ou senha inválidos.");

            if(!user.ConfirmedAt.HasValue)
                throw new NotAuthorizedException("Validação de email pendente.");

            var token = _tokenService.GenerateToken(command.UserName!);

            _logger.LogInformation("Login ended successfully");

            return token;
        }
    }
}
