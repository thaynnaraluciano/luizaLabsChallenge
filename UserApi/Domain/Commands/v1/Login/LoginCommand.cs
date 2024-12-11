using MediatR;

namespace Domain.Commands.v1.Login
{
    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }
    }
}
