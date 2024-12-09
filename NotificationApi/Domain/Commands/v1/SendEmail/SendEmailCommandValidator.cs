using FluentValidation;

namespace Domain.Commands.v1.SendEmail
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            
        }
    }
}
