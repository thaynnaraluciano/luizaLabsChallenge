using Domain.Commands.v1.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Usuário cadastrado com sucesso!");
            }
            catch (ValidationException ex) 
            {
                return StatusCode(400, ex.ValidationResult.ErrorMessage);
            }
            catch (Exception ex) 
            {
                return StatusCode(ex.HResult, ex.Message);            
            }
        }
    }
}
