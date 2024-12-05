using Domain.Commands.v1.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var retorno = await _mediator.Send(command);
            return Ok(retorno);
        }
    }
}
