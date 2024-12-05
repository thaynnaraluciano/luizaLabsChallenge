using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.v1.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, HttpStatusCode>
    {
        public async Task<HttpStatusCode> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            return HttpStatusCode.OK;
        }
    }
}
