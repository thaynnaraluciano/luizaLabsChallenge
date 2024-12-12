using Domain.Commands.v1.SendEmailConfirmation;

namespace Tests.Domain.Mocks.SendEmailConfirmation
{
    public class SendEmailConfirmationCommandMock
    {
        public static SendEmailConfirmationCommand GetInstance(string? email)
        {
            return new SendEmailConfirmationCommand()
            {
                Email = email
            };
        }
    }
}
