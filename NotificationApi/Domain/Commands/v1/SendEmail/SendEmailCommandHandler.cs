using AutoMapper;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.SendEmail
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Unit>
    {
        private readonly ILogger<SendEmailCommandHandler> _logger;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public SendEmailCommandHandler(
            ILogger<SendEmailCommandHandler> logger,
            IEmailService emailService,
            IMapper mapper)
        {
            _logger = logger;
            _emailService = emailService;
            _mapper = mapper; 
        }

        public async Task<Unit> Handle(SendEmailCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Sending email to {command.ReceiverEmail}");

            var request = _mapper.Map<EmailModel>(command);
            await _emailService.SendEmail(request);

            _logger.LogInformation($"Email sent to {command.ReceiverEmail}");
            return Unit.Value;
        }
    }
}
