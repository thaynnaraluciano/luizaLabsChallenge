using Api.Utils;
using Domain.Commands.v1.Login;
using FluentValidation;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
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

        private ValidationBehavior<LoginCommand, string> ValidationBehavior;
        private Mock<RequestHandlerDelegate<string>> _nextMock;

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
            ValidationBehavior = new ValidationBehavior<LoginCommand, string>(new LoginCommandValidator());
            _nextMock = new Mock<RequestHandlerDelegate<string>>();
        }

        [TestCase(Category = "Unit", TestName = "Should LoginCommandHandler handle command successfully")]
        public async Task Should_LoginCommandHandler_Handle_Success()
        {
            var command = LoginCommandMock.GetValidInstance();

            var hashedPassword = "2404cbb78967d0674be472180d5cb8ee47177d33cac71ff3e9d443f44a7fe46a";

            var userModel = new UserModel() 
            { 
                Email = "validEmail@gmail.com",
                IsEmailConfirmed = true,
                Password = hashedPassword,
                UserName = "validUsername"
            };

            _mockUserRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(userModel);

            _mockCryptographyService.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(hashedPassword);

            _mockTokenService.Setup(x => x.GenerateToken(It.IsAny<string>())).Returns("validJwtToken");

            var result = await _handler.Handle(command);

            Assert.That(result.GetType() == typeof(string));
        }

        [TestCase("", "", Category = "Unit", TestName = "Should LoginCommandHandler handle command throw exception")]
        public async Task Should_LoginCommandHandler_EmptyProps_Handle_Exception(string username, string password)
        {
            var command = LoginCommandMock.GetInvalidInstance(username, password);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O username deve ser informado", exception.Message);
            StringAssert.Contains("A senha deve ser informada", exception.Message);
        }
    }
}
