using Domain.Commands.v1.CreateUser;

namespace Tests.Domain.Mocks.CreateUser
{
    public class CreateUserCommandMock
    {
        public static CreateUserCommand GetValidInstance()
        {
            return new CreateUserCommand()
            {
                UserName = "validUsername",
                Email = "valid@email.com",
                Password = "validPassword"
            };
        }

        public static CreateUserCommand GetInvalidInstance(string? username, string? email, string? password)
        {
            return new CreateUserCommand()
            {
                UserName = username,
                Email = email,
                Password = password
            };
        }
    }
}
