using Api.Utils;
using Domain.Commands.v1;
using Domain.Commands.v1.SendEmail;
using FluentValidation;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.Domain.Mocks;
using Tests.Domain.Mocks.SendEmail;

namespace Tests.Domain.Commands.v1.SendEmail
{
    [TestFixture]
    public class SendEmailCommandHandlerTest
    {
        private Mock<ILogger<SendEmailCommandHandler>> _loggerMock;
        private Mock<IEmailService> _emailServiceMock;
        private SendEmailCommandHandler _handler;

        private ValidationBehavior<SendEmailCommand, Unit> ValidationBehavior;
        private Mock<RequestHandlerDelegate<Unit>> _nextMock;


        protected SendEmailCommandHandler EstablishContext()
        {
            _loggerMock = new Mock<ILogger<SendEmailCommandHandler>>();
            _emailServiceMock = new Mock<IEmailService>();

            return new SendEmailCommandHandler(
                _loggerMock.Object,
                _emailServiceMock.Object,
                MappersMock.GetMock());
        }

        [SetUp]
        public void Setup()
        {
            _handler = EstablishContext();
            ValidationBehavior = new ValidationBehavior<SendEmailCommand, Unit>(new SendEmailCommandValidator());
            _nextMock = new Mock<RequestHandlerDelegate<Unit>>();
        }

        [TestCase("teste", "plain", Category = "Unit", TestName = "Should SendEmailCommandHandler plain body handle command successfully")]
        [TestCase("<html>teste<html>", "html", Category = "Unit", TestName = "Should SendEmailCommandHandler html body handle command successfully")]
        public async Task Should_SendEmailCommandHandler_Handle_Success(string body, string bodyType)
        {
            var command = SendEmailCommandMock.GetInstance(body, bodyType, "receiverEmail@gmail.com", "receiverName", "subject");

            _emailServiceMock.Setup(x => x.SendEmail(It.IsAny<EmailModel>())).Returns(Task.CompletedTask);

            var result = await _handler.Handle(command);

            Assert.That(result.GetType() == typeof(Unit));
        }

        [TestCase("", "", "", "", "", Category = "Unit", TestName = "Should SendEmailCommandHandler empty props command throw exception")]
        [TestCase(null, null, null, null, null, Category = "Unit", TestName = "Should SendEmailCommandHandler null prop command throw exception")]
        public void Should_SendEmailCommandHandler_EmptyProps_Handle_Exception(
            string? body,
            string? bodyType,
            string? receiverEmail,
            string? receiverName,
            string? subject)
        {
            var command = SendEmailCommandMock.GetInstance(body, bodyType, receiverEmail, receiverName, subject);

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("ReceiverName deve ser informado", exception.Message);
            StringAssert.Contains("Subject deve ser informado", exception.Message);
            StringAssert.Contains("Body deve ser informado", exception.Message);
            StringAssert.Contains("ReceiverEmail deve ser informado", exception.Message);
            StringAssert.Contains("BodyType deve ser informado", exception.Message);
        }

        [TestCase(Category = "Unit", TestName = "Should SendEmailCommandHandler invalid receiver name command throw exception")]
        public void Should_SendEmailCommandHandler_InvalidReceiverName_Handle_Exception()
        {
            var command = SendEmailCommandMock.GetInstance("body valido para teste", "plain", "validEmail@gmail.com", "A", "subject valido");

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("ReceiverName deve possuir pelo menos 3 caracteres", exception.Message);
        }

        [TestCase(Category = "Unit", TestName = "Should SendEmailCommandHandler invalid receiver email command throw exception")]
        public void Should_SendEmailCommandHandler_InvalidReceiverEmail_Handle_Exception()
        {
            var command = SendEmailCommandMock.GetInstance("body valido para teste", "plain", "invalidEmail", "validReceiverName", "subject valido");

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("O formato de email informado é inválido", exception.Message);
        }

        [TestCase(Category = "Unit", TestName = "Should SendEmailCommandHandler invalid body command throw exception")]
        public void Should_SendEmailCommandHandler_InvalidBody_Handle_Exception()
        {
            var command = SendEmailCommandMock.GetInstance("body", "plain", "validEmail@gmail.com", "validReceiverName", "subject valido");

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("Body deve possuir pelo menos 10 caracteres", exception.Message);
        }

        [TestCase(Category = "Unit", TestName = "Should SendEmailCommandHandler invalid bodyType command throw exception")]
        public void Should_SendEmailCommandHandler_InvalidBodyType_Handle_Exception()
        {
            var command = SendEmailCommandMock.GetInstance("body valido para teste", "texto", "validEmail@gmail.com", "validReceiverName", "subject valido");

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("Valor inválido informado para BodyType", exception.Message);
        }

        [TestCase(Category = "Unit", TestName = "Should SendEmailCommandHandler invalid subject command throw exception")]
        public void Should_SendEmailCommandHandler_InvalidSubject_Handle_Exception()
        {
            var command = SendEmailCommandMock.GetInstance("body valido para teste", "plain", "validEmail@gmail.com", "validReceiverName", "subj");

            var exception = Assert.Throws<ValidationException>(() =>
            {
                ValidationBehavior.Handle(command, _nextMock.Object, CancellationToken.None);
            });

            StringAssert.Contains("Subject deve possuir pelo menos 5 caracteres", exception.Message);
        }
    }
}
