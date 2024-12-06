using Api.Utils;
using Domain.Commands.v1.CreateUser;
using FluentValidation;
using Infrastructure.Data.Interfaces;
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
        private CreateUserCommandHandler _handler;
        private ValidationBehavior<CreateUserCommand, Unit> ValidationBehavior;
        private Mock<RequestHandlerDelegate<Unit>> _nextMock;

        protected CreateUserCommandHandler EstablishContext()
        {
            _loggerMock = new Mock<ILogger<CreateUserCommandHandler>>();
            _mockUserRepository = new Mock<IUserRepository>();

            return new CreateUserCommandHandler(_loggerMock.Object, _mockUserRepository.Object, MappersMock.GetMock());
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
            var command = CreateUserCommandMock.GetValidInstance();
            var result = await _handler.Handle(command);

            Assert.That(result == Unit.Value);
        }

        [TestCase("", "", "", Category = "Unit", TestName = "Should CreateUserCommandHandler handle command throw exception")]
        public async Task Should_CreateUserCommandHandler_Handle_Exception(string username, string email, string password)
        {
            var command = CreateUserCommandMock.GetInvalidInstance(username, email, password);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O username deve ser informado", exception.Message);
        }
    }
}
