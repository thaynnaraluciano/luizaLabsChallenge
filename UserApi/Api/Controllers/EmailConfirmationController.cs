using Domain.Commands.v1.SendEmailConfirmation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/emailConfirmation")]
    public class EmailConfirmationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailConfirmationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendEmailConfirmationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
