using Domain.Commands.v1.ConfirmEmail;

namespace Tests.Domain.Mocks.ConfirmEmail
{
    public class ConfirmEmailCommandMock
    {
        public static ConfirmEmailCommand GetInstance(string? verificationCode)
        {
            return new ConfirmEmailCommand()
            {
                VerificationCode = verificationCode
            };
        }
    }
}
