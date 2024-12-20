﻿using Domain.Commands.v1.ConfirmEmail;
using Domain.Commands.v1.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
