using MediatR;

namespace Domain.Commands.v1.CreateUser
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }
    }
}
