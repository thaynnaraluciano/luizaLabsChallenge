using Api.Utils;
using Domain.Commands.v1.CreateUser;
using FluentValidation;
using Infrastructure.Data.Interfaces;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using Tests.Domain.Mocks;
using Tests.Domain.Mocks.CreateUser;

namespace Tests.Domain.Commands.v1.CreateUser
{
    [TestFixture]
    public class CreateUserCommandHandlerTest
    {
        private Mock<ILogger<CreateUserCommandHandler>> _loggerMock;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<ICryptograpghyService> _mockCryptographyService;
        private Mock<INotificationService> _mockNotificationService;
        private Mock<IEmailTemplateService> _mockEmailTemplateService;
        private CreateUserCommandHandler _handler;
        private ValidationBehavior<CreateUserCommand, Unit> ValidationBehavior;
        private Mock<RequestHandlerDelegate<Unit>> _nextMock;

        protected CreateUserCommandHandler EstablishContext()
        {
            _loggerMock = new Mock<ILogger<CreateUserCommandHandler>>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockCryptographyService = new Mock<ICryptograpghyService>();
            _mockNotificationService = new Mock<INotificationService>();
            _mockEmailTemplateService = new Mock<IEmailTemplateService>();

            return new CreateUserCommandHandler(
                _loggerMock.Object, 
                _mockUserRepository.Object, 
                MappersMock.GetMock(), 
                _mockCryptographyService.Object,
                _mockNotificationService.Object,
                _mockEmailTemplateService.Object);
        }

        [SetUp]
        public void Setup()
        {
            _handler = EstablishContext();
            ValidationBehavior = new ValidationBehavior<CreateUserCommand, Unit>(new CreateUserCommandValidator(_mockUserRepository.Object));
            _nextMock = new Mock<RequestHandlerDelegate<Unit>>();
        }

        [TestCase(Category = "Unit", TestName = "Should CreateUserCommandHandler handle command successfully")]
        public async Task Should_CreateUserCommandHandler_Handle_Success()
        {
            var command = CreateUserCommandMock.GetInstance("validUsername", "valid@email.com", "v@l1dPassword!");
            var result = await _handler.Handle(command);

            Assert.That(result == Unit.Value);
        }

        [TestCase("", "", "", Category = "Unit", TestName = "Should CreateUserCommandHandler empty props command throw exception")]
        public void Should_CreateUserCommandHandler_EmptyProps_Handle_Exception(string username, string email, string password)
        {
            var command = CreateUserCommandMock.GetInstance(username, email, password);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O username deve ser informado", exception.Message);
            StringAssert.Contains("O email deve ser informado", exception.Message);
            StringAssert.Contains("A senha deve ser informada", exception.Message);
        }

        [TestCase("ab", "email@gmail.com", "str0ngPassword!", Category = "Unit", TestName = "Should CreateUserCommandHandler invalid username command throw exception")]
        public void Should_CreateUserCommandHandler_InvalidUsername_Handle_Exception(string username, string email, string password)
        {
            var command = CreateUserCommandMock.GetInstance(username, email, password);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O username deve possuir pelo menos 3 caracteres", exception.Message);
        }

        [TestCase("unavailableUsername", "email@gmail.com", "str0ngPassword!", Category = "Unit", TestName = "Should CreateUserCommandHandler unavailable username command throw exception")]
        public void Should_CreateUserCommandHandler_UnavailableUsername_Handle_Exception(string username, string email, string password)
        {
            var command = CreateUserCommandMock.GetInstance(username, email, password);

            _mockUserRepository.Setup(x => x.UserNameAlreadyExists(It.IsAny<string>())).Returns(true);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O username informado não está disponível", exception.Message);
        }

        [TestCase("availableUsername", "invalidEmail", "str0ngPassword!", Category = "Unit", TestName = "Should CreateUserCommandHandler invalid email command throw exception")]
        public void Should_CreateUserCommandHandler_InvalidEmail_Handle_Exception(string username, string email, string password)
        {
            var command = CreateUserCommandMock.GetInstance(username, email, password);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O formato do email informado é inválido", exception.Message);
        }

        [TestCase("availableUsername", "unavailableEmail", "str0ngPassword!", Category = "Unit", TestName = "Should CreateUserCommandHandler unavailable email command throw exception")]
        public void Should_CreateUserCommandHandler_UnavailableEmail_Handle_Exception(string username, string email, string password)
        {
            var command = CreateUserCommandMock.GetInstance(username, email, password);

            _mockUserRepository.Setup(x => x.EmailAlreadyExists(It.IsAny<string>())).Returns(true);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O email informado já está cadastrado", exception.Message);
        }

        [TestCase("availableUsername", "email@gmail.com", "invalidPassword", Category = "Unit", TestName = "Should CreateUserCommandHandler invalid password command throw exception")]
        public void Should_CreateUserCommandHandler_InvalidPassword_Handle_Exception(string username, string email, string password)
        {
            var command = CreateUserCommandMock.GetInstance(username, email, password);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("A senha deve conter pelo menos 8 caracteres, incluindo caracteres maiúsculos, minúsculos, especiais e números", exception.Message);
        }
    }
}
