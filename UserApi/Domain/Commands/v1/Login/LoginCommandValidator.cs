using FluentValidation;

namespace Domain.Commands.v1.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("O username deve ser informado");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("A senha deve ser informada");
        }
    }
}
