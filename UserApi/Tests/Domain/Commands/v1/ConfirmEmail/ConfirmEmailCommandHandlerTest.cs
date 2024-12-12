using Api.Utils;
using CrossCutting.Exceptions;
using Domain.Commands.v1.ConfirmEmail;
using FluentValidation;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.Domain.Mocks;
using Tests.Domain.Mocks.ConfirmEmail;

namespace Tests.Domain.Commands.v1.ConfirmEmail
{
    [TestFixture]
    public class ConfirmEmailCommandHandlerTest
    {
        private ConfirmEmailCommandHandler _handler;

        private Mock<ILogger<ConfirmEmailCommandHandler>> _loggerMock;
        private Mock<IUserRepository> _mockUserRepository;

        private ValidationBehavior<ConfirmEmailCommand, Unit> ValidationBehavior;
        private Mock<RequestHandlerDelegate<Unit>> _nextMock;

        protected ConfirmEmailCommandHandler EstablishContext()
        {
            _loggerMock = new Mock<ILogger<ConfirmEmailCommandHandler>>();
            _mockUserRepository = new Mock<IUserRepository>();
          
            return new ConfirmEmailCommandHandler(
                _loggerMock.Object,
                _mockUserRepository.Object
            );
        }

        [SetUp]
        public void Setup()
        {
            _handler = EstablishContext();
            ValidationBehavior = new ValidationBehavior<ConfirmEmailCommand, Unit>(new ConfirmEmailCommandValidator());
            _nextMock = new Mock<RequestHandlerDelegate<Unit>>();
        }

        [TestCase(Category = "Unit", TestName = "Should ConfirmEmailCommandHandler handle command successfully")]
        public async Task Should_ConfirmEmailCommandHandler_Handle_Success()
        {
            var command = ConfirmEmailCommandMock.GetInstance("validVerificationCode");

            var userModel = UserModelMock.GetInstance("validEmail@gmail.com", null, "v@lidPassw0rd", "validUsername", "validVerificationCode");

            _mockUserRepository.Setup(x => x.GetUserByVerificationCode(It.IsAny<string>())).Returns(userModel);

            var result = await _handler.Handle(command);

            Assert.That(result.GetType() == typeof(Unit));
        }

        [TestCase("", Category = "Unit", TestName = "Should ConfirmEmailCommandHandler empty prop command throw exception")]
        [TestCase(null, Category = "Unit", TestName = "Should ConfirmEmailCommandHandler null prop command throw exception")]
        public void Should_ConfirmEmailCommandHandler_InvalidVerificationCode_Handle_Exception(string? invalidVerificationCode)
        {
            var command = ConfirmEmailCommandMock.GetInstance(invalidVerificationCode);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O código de verificação deve ser informado", exception.Message);
        }

        [TestCase(Category = "Unit", TestName = "Should ConfirmEmailCommandHandler confirmed email command throw exception")]
        public void Should_ConfirmEmailCommandHandler_ConfirmedEmail_Handle_Exception()
        {
            var command = ConfirmEmailCommandMock.GetInstance("validVerificationCode");

            var userModel = UserModelMock.GetInstance("validEmail@gmail.com", DateTime.Now, "validPassw0rd!", "validUsername");

            _mockUserRepository.Setup(x => x.GetUserByVerificationCode(It.IsAny<string>())).Returns(userModel);

            var exception = Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                await _handler.Handle(command);
            });

            Assert.That(exception.Message, Is.EqualTo("Este email já foi validado."));
        }

        [TestCase(Category = "Unit", TestName = "Should ConfirmEmailCommandHandler user not found command throw exception")]
        public void Should_ConfirmEmailCommandHandler_UserNotFound_Handle_Exception()
        {
            var command = ConfirmEmailCommandMock.GetInstance("verificationCode");

            _mockUserRepository.Setup(x => x.GetUserByVerificationCode(It.IsAny<string>())).Returns((UserModel?)null);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await _handler.Handle(command);
            });

            Assert.That(exception.Message, Is.EqualTo("Usuário não encontrado."));
        }
    }
}
