﻿using AutoMapper;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            var userEntity = _mapper.Map<UserModel>(command);
            await _userRepository.CreateUser(userEntity);
            return Unit.Value;
        }
    }
}