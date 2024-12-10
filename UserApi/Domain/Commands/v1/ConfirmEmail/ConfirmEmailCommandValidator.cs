using FluentValidation;

namespace Domain.Commands.v1.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.VerificationCode)
                .NotEmpty().WithMessage("VerificationCode deve ser informado");
        }
    }
}
