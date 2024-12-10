using MediatR;

namespace Domain.Commands.v1.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<Unit>
    {
        public string? VerificationCode { get; set; }
    }
}
