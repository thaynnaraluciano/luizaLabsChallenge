using Domain.Commands.v1.SendEmail;
using Infrastructure.Services.Interfaces.v1;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.Domain.Mocks;

namespace Tests.Domain.Commands.v1.SendEmail
{
    [TestFixture]
    public class SendEmailCommandHandlerTest
    {
        private Mock<ILogger<SendEmailCommandHandler>> _loggerMock;
        private Mock<IEmailService> _emailServiceMock;

        protected SendEmailCommandHandler EstablishContext()
        {
            _loggerMock = new Mock<ILogger<SendEmailCommandHandler>>();
            _emailServiceMock = new Mock<IEmailService>();

            return new SendEmailCommandHandler(
                _loggerMock.Object,
                _emailServiceMock.Object,
                MappersMock.GetMock());
        }
    }
}
