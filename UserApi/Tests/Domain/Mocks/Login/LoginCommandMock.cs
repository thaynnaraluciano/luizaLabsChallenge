using Domain.Commands.v1.Login;

namespace Tests.Domain.Mocks.Login
{
    public class LoginCommandMock
    {
        public static LoginCommand GetInstance(string username, string password)
        {
            return new LoginCommand()
            {
                UserName = username,
                Password = password
            };
        }
    }
}
