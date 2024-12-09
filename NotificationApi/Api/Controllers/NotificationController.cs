using Domain.Commands.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmail(SendEmailCommand command)
        {
            await _mediator.Send(command);
            return Ok("Email de confirmação enviado");
        }
    }
}
