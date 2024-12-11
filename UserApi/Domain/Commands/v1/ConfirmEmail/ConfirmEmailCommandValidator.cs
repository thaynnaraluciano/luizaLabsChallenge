using FluentValidation;

namespace Domain.Commands.v1.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.VerificationCode)
                .NotEmpty().WithMessage("O código de verificação deve ser informado");
        }
    }
}
