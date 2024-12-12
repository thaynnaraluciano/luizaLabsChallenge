using Api.Utils;
using CrossCutting.Exceptions;
using Domain.Commands.v1.SendEmailConfirmation;
using FluentValidation;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.Domain.Mocks;
using Tests.Domain.Mocks.SendEmailConfirmation;

namespace Tests.Domain.Commands.v1.SendEmailConfirmation
{
    [TestFixture]
    public class SendEmailConfirmationCommandHandlerTest
    {
        private Mock<ILogger<SendEmailConfirmationCommandHandler>> _loggerMock;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<INotificationService> _mockNotificationService;
        private Mock<IEmailTemplateService> _mockEmailTemplateService;
        
        private SendEmailConfirmationCommandHandler _handler;
        private ValidationBehavior<SendEmailConfirmationCommand, Unit> ValidationBehavior;
        private Mock<RequestHandlerDelegate<Unit>> _nextMock;

        protected SendEmailConfirmationCommandHandler EstablishContext()
        {
            _loggerMock = new Mock<ILogger<SendEmailConfirmationCommandHandler>>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockNotificationService = new Mock<INotificationService>();
            _mockEmailTemplateService = new Mock<IEmailTemplateService>();

            return new SendEmailConfirmationCommandHandler(
                _loggerMock.Object,
                _mockUserRepository.Object,
                _mockNotificationService.Object,
                _mockEmailTemplateService.Object);
        }

        [SetUp]
        public void Setup()
        {
            _handler = EstablishContext();
            ValidationBehavior = new ValidationBehavior<SendEmailConfirmationCommand, Unit>(new SendEmailConfirmationCommandValidator());
            _nextMock = new Mock<RequestHandlerDelegate<Unit>>();
        }

        [TestCase(Category = "Unit", TestName = "Should SendEmailConfirmationCommand handle command successfully")]
        public async Task Should_SendEmailConfirmationCommand_Handle_Success()
        {
            var email = "valid@email.com";
            var command = SendEmailConfirmationCommandMock.GetInstance(email);

            _mockUserRepository.Setup(x => x.GetUserByEmail(email)).Returns(
                UserModelMock.GetInstance(email, null, "", "", ""));

            _mockNotificationService.Setup(x => x.SendEmail(It.IsAny<SendEmailModel>())).ReturnsAsync(true);

            var result = await _handler.Handle(command);

            Assert.That(result == Unit.Value);
        }

        [TestCase("", Category = "Unit", TestName = "Should SendEmailConfirmationCommand empty email command throw exception")]
        public void Should_SendEmailConfirmationCommand_EmptyEmail_Handle_Exception(string email)
        {
            var command = SendEmailConfirmationCommandMock.GetInstance(email);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O email deve ser informado", exception.Message);
        }

        [TestCase("invalidEmail", Category = "Unit", TestName = "Should SendEmailConfirmationCommand invalid email command throw exception")]
        public void Should_SendEmailConfirmationCommand_InvalidEmail_Handle_Exception(string email)
        {
            var command = SendEmailConfirmationCommandMock.GetInstance(email);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O formato do email informado é inválido", exception.Message);
        }

        [TestCase(Category = "Unit", TestName = "Should SendEmailConfirmationCommand confirmed email command throw exception")]
        public void Should_SendEmailConfirmationCommand_ConfirmedEmail_Handle_Exception()
        {
            var email = "validEmail@gmail.com";
            var command = SendEmailConfirmationCommandMock.GetInstance(email);

            var userModel = UserModelMock.GetInstance(email, DateTime.Now, "", "");

            _mockUserRepository.Setup(x => x.GetUserByEmail(email)).Returns(userModel);

            var exception = Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                await _handler.Handle(command);
            });

            Assert.That(exception.Message, Is.EqualTo("Este email já foi validado."));
        }

        [TestCase(Category = "Unit", TestName = "Should SendEmailConfirmationCommand user not found command throw exception")]
        public void Should_SendEmailConfirmationCommand_UserNotFound_Handle_Exception()
        {
            var email = "validEmail@gmail.com";
            var command = SendEmailConfirmationCommandMock.GetInstance(email);

            _mockUserRepository.Setup(x => x.GetUserByEmail(email)).Returns((UserModel?)null);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await _handler.Handle(command);
            });

            Assert.That(exception.Message, Is.EqualTo("Usuário não encontrado."));
        }
    }
}
