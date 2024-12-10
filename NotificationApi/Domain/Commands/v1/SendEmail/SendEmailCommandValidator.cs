using FluentValidation;

namespace Domain.Commands.v1.SendEmail
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        private const string plainTextBodyType = "plain";
        private const string htmlBodyType = "html";

        public SendEmailCommandValidator()
        {
            RuleFor(x => x.ReceiverName)
                .NotEmpty().WithMessage("ReceiverName deve ser informado")
                .MinimumLength(3).WithMessage("ReceiverName deve possuir pelo menos 3 caracteres");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject deve ser informado")
                .MinimumLength(5).WithMessage("Subject deve possuir pelo menos 5 caracteres");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Body deve ser informado")
                .MinimumLength(10).WithMessage("Body deve possuir pelo menos 10 caracteres");

            RuleFor(x => x.ReceiverEmail)
                .NotEmpty().WithMessage("ReceiverEmail deve ser informado")
                .EmailAddress().WithMessage("O formato de email informado é inválido");

            RuleFor(x => x.BodyType)
                .NotEmpty().WithMessage("BodyType deve ser informado")
                .Must(bodyType => string.Equals(bodyType, plainTextBodyType) || string.Equals(bodyType, htmlBodyType)).WithMessage("Valor inválido informado para BodyType");
        }
    }
}
