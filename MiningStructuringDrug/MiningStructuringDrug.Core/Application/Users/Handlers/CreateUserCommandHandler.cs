using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.Users.Commands;
using MiningStructuringDrug.Core.Application.Users.Dtos;
using MiningStructuringDrug.Core.Application.Users.Interfaces;
using MiningStructuringDrug.Core.Domain.Entities;
using MiningStructuringDrug.Core.Domain.Interfaces;

namespace MiningStructuringDrug.Core.Application.Users.Handlers
{
    public class CreateUserCommandHandler : ICommandHandlerResult<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public CreateUserCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        
        public async Task<UserDto> Handle(CreateUserCommand command)
        {
            var user = new User
            {
                Username = command.Username,
                Email = command.Email,
                Role = command.Role
            };

            user = await _authenticationService.RegisterAsync(user, command.Password);

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
            };

            return userDto;
        }
    }
}
