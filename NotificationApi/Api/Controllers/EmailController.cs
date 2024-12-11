using Domain.Commands.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/email")]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendEmailCommand command)
        {
            await _mediator.Send(command);
            return Ok("Email de confirmação enviado");
        }
    }
}
