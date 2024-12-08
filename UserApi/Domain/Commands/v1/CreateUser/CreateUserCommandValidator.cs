using FluentValidation;
using Infrastructure.Data.Interfaces;
using System.Text.RegularExpressions;

namespace Domain.Commands.v1.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("O username deve ser informado")
                .MinimumLength(3).WithMessage("O username deve possuir pelo menos 3 caracteres")
                .Must(BeAvailableUsername).WithMessage("O username informado não está disponível");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("O email deve ser informado")
                .EmailAddress().WithMessage("O formato do email informado é inválido")
                .Must(BeUniqueEmail).WithMessage("O email informado já está cadastrado");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("A senha deve ser informada")
                .Must(BeValidPassword).WithMessage("A senha deve conter pelo menos 8 caracteres, incluindo caracteres maiúsculos, minúsculos, especiais e números");
        }

        private bool BeValidPassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_])[A-Za-z\d\W_]{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        private bool BeAvailableUsername(string userName)
        {
            return !_userRepository.UserNameAlreadyExists(userName);
        }

        private bool BeUniqueEmail(string email)
        {
            return !_userRepository.EmailAlreadyExists(email);
        }
    }
}
