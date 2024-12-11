using Domain.Commands.v1;
namespace Tests.Domain.Mocks.SendEmail
{
    public class SendEmailCommandMock
    {
        public static SendEmailCommand GetInstance(string? body, string? bodyType, string? receiverEmail, string? receiverName, string? subject)
        {
            return new SendEmailCommand()
            {
                Body = body,
                BodyType = bodyType,
                ReceiverEmail = receiverEmail,
                ReceiverName = receiverName,
                Subject = subject
            };
        }
    }
}
