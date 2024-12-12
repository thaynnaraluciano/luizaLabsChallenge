using MediatR;

namespace Domain.Commands.v1.SendEmailConfirmation
{
    public class SendEmailConfirmationCommand : IRequest<Unit>
    {
        public string? Email { get; set; }
    }
}
