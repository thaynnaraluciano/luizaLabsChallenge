using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.SendEmail
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Unit>
    {
        private readonly ILogger<SendEmailCommandHandler> _logger;

        public SendEmailCommandHandler(
            ILogger<SendEmailCommandHandler> logger)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(SendEmailCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Sending email");

            _logger.LogInformation("Email sent");
            return Unit.Value;
        }
    }
}
