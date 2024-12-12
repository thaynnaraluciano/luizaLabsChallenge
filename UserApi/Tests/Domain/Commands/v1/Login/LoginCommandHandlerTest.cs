using Api.Utils;
using CrossCutting.Exceptions;
using Domain.Commands.v1.Login;
using FluentValidation;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.Domain.Mocks;
using Tests.Domain.Mocks.Login;

namespace Tests.Domain.Commands.v1.Login
{
    [TestFixture]
    public class LoginCommandHandlerTest
    {
        private Mock<ILogger<LoginCommandHandler>> _loggerMock;

        private Mock<IUserRepository> _mockUserRepository;
        
        private Mock<ICryptograpghyService> _mockCryptographyService;
        private Mock<ITokenService> _mockTokenService;

        private LoginCommandHandler _handler;

        private ValidationBehavior<LoginCommand, LoginCommandResponse> ValidationBehavior;
        private Mock<RequestHandlerDelegate<LoginCommandResponse>> _nextMock;

        protected LoginCommandHandler EstablishContext()
        {
            _loggerMock = new Mock<ILogger<LoginCommandHandler>>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockCryptographyService = new Mock<ICryptograpghyService>();
            _mockTokenService = new Mock<ITokenService>();

            return new LoginCommandHandler(
                _loggerMock.Object,
                _mockCryptographyService.Object,
                _mockUserRepository.Object,
                _mockTokenService.Object
            );
        }

        [SetUp]
        public void Setup()
        {
            _handler = EstablishContext();
            ValidationBehavior = new ValidationBehavior<LoginCommand, LoginCommandResponse>(new LoginCommandValidator());
            _nextMock = new Mock<RequestHandlerDelegate<LoginCommandResponse>>();
        }

        [TestCase(Category = "Unit", TestName = "Should LoginCommandHandler handle command successfully")]
        public async Task Should_LoginCommandHandler_Handle_Success()
        {
            var command = LoginCommandMock.GetInstance("validUsername", "123Abc!@");

            var hashedPassword = "2404cbb78967d0674be472180d5cb8ee47177d33cac71ff3e9d443f44a7fe46a";

            var userModel = UserModelMock.GetInstance("validEmail@gmail.com", DateTime.Now, hashedPassword, "validUsername");

            _mockUserRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(userModel);

            _mockCryptographyService.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(hashedPassword);

            _mockTokenService.Setup(x => x.GenerateToken(It.IsAny<string>())).Returns("validJwtToken");

            var result = await _handler.Handle(command);

            Assert.That(result.GetType() == typeof(LoginCommandResponse));
            Assert.That(result.Token, Is.EqualTo("validJwtToken"));
        }

        [TestCase("", "", Category = "Unit", TestName = "Should LoginCommandHandler handle command throw exception")]
        public void Should_LoginCommandHandler_EmptyProps_Handle_Exception(string username, string password)
        {
            var command = LoginCommandMock.GetInstance(username, password);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O usuário deve ser informado", exception.Message);
            StringAssert.Contains("A senha deve ser informada", exception.Message);
        }

        [TestCase(Category = "Unit", TestName = "Should LoginCommandHandler not confirmed email command throw exception")]
        public void Should_LoginCommandHandler_NotConfirmedEmail_Handle_Exception()
        {
            var command = LoginCommandMock.GetInstance("validUsername", "123Abc!@");

            var hashedPassword = "2404cbb78967d0674be472180d5cb8ee47177d33cac71ff3e9d443f44a7fe46a";

            var userModel = UserModelMock.GetInstance("validEmail@gmail.com", null, hashedPassword, "validUsername");

            _mockUserRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(userModel);

            _mockCryptographyService.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(hashedPassword);

            var exception = Assert.ThrowsAsync<NotAuthorizedException>(async () =>
            {
                await _handler.Handle(command);
            });

            Assert.That(exception.Message, Is.EqualTo("Validação de email pendente."));
        }

        [TestCase(Category = "Unit", TestName = "Should LoginCommandHandler user not found command throw exception")]
        public void Should_LoginCommandHandler_UserNotFound_Handle_Exception()
        {
            var command = LoginCommandMock.GetInstance("validUsername", "123Abc!@");

            var hashedPassword = "2404cbb78967d0674be472180d5cb8ee47177d33cac71ff3e9d443f44a7fe46a";

            _mockUserRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns((UserModel?)null);

            _mockCryptographyService.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(hashedPassword);

            var exception = Assert.ThrowsAsync<NotAuthorizedException>(async () =>
            {
                await _handler.Handle(command);
            });

            Assert.That(exception.Message, Is.EqualTo("Usuário ou senha inválidos."));
        }

        [TestCase(Category = "Unit", TestName = "Should LoginCommandHandler wrong password command throw exception")]
        public void Should_LoginCommandHandler_WrongPassword_Handle_Exception()
        {
            var command = LoginCommandMock.GetInstance("validUsername", "123Abc!@");

            var hashedPassword = "2404cbb78967d0674be472180d5cb8ee47177d33cac71ff3e9d443f44a7fe46a";
            var invalidHashedPassword = "invalidHashedPassword";

            var userModel = UserModelMock.GetInstance("validEmail@gmail.com", null, hashedPassword, "validUsername");

            _mockUserRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(userModel);

            _mockCryptographyService.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(invalidHashedPassword);

            var exception = Assert.ThrowsAsync<NotAuthorizedException>(async () =>
            {
                await _handler.Handle(command);
            });

            Assert.That(exception.Message, Is.EqualTo("Usuário ou senha inválidos."));
        }
    }
}
