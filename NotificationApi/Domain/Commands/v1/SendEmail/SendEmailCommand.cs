﻿using MediatR;

namespace Domain.Commands.v1
{
    public class SendEmailCommand : IRequest<Unit>
    {
        public string? ReceiverName { get; set; }

        public string? ReceiverEmail { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }

        public string? BodyType { get; set; }
    }
}
