using Domain.Commands.v1.Login;

namespace Tests.Domain.Mocks.Login
{
    public class LoginCommandMock
    {
        public static LoginCommand GetValidInstance()
        {
            return new LoginCommand()
            {
                UserName = "validUsername",
                Password = "123Abc!@"
            };
        }

        public static LoginCommand GetInvalidInstance(string username, string password)
        {
            return new LoginCommand()
            {
                UserName = username,
                Password = password
            };
        }
    }
}
