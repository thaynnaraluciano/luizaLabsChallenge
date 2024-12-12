using FluentValidation;

namespace Domain.Commands.v1.SendEmailConfirmation
{
    public class SendEmailConfirmationCommandValidator : AbstractValidator<SendEmailConfirmationCommand>
    {
        public SendEmailConfirmationCommandValidator()
        {
            RuleFor(user => user.Email)
               .NotEmpty().WithMessage("O email deve ser informado")
               .EmailAddress().WithMessage("O formato do email informado é inválido");
        }
    }
}
