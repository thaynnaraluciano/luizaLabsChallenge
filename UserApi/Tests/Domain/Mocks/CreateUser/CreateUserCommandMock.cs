using Domain.Commands.v1.CreateUser;

namespace Tests.Domain.Mocks.CreateUser
{
    public class CreateUserCommandMock
    {
        public static CreateUserCommand GetInstance(string? username, string? email, string? password)
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
