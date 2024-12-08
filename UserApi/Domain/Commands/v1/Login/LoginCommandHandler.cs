using AutoMapper;
using Domain.Commands.v1.CreateUser;
using Infrastructure.Data.Interfaces;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Domain.Commands.v1.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;

        private readonly IMapper _mapper;
        private readonly ICryptograpghyService _cryptograpghyService;
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(
            ILogger<CreateUserCommandHandler> logger,
            IMapper mapper,
            ICryptograpghyService cryptograpghyService,
            IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _cryptograpghyService = cryptograpghyService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting user login");

            var user = _userRepository.GetUserByUsername(command.UserName);

            if (user == null)
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

            if(!user.IsEmailConfirmed)
                throw new UnauthorizedAccessException("Validação de email pendente.");

            var hashedPassword = _cryptograpghyService.HashPassword(command.Password);

            if (hashedPassword != user.Password)
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

            var token = GenerateFakeToken(command.UserName);

            _logger.LogInformation("Login ended successfully");

            return token;
        }

        private string GenerateFakeToken(string username)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{DateTime.UtcNow}"));
        }
    }
}
