using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.Users.Commands;
using MiningStructuringDrug.Core.Application.Users.Dtos;
using MiningStructuringDrug.Core.Application.Users.Interfaces;
using MiningStructuringDrug.Core.Domain.Interfaces;

namespace MiningStructuringDrug.Core.Application.Users.Handlers
{
    public class AuthenticateUserCommandHandler : ICommandHandlerResult<AuthenticateUserCommand, AuthResponseDto?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticateUserCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task<AuthResponseDto?> Handle(AuthenticateUserCommand command)
        {
            var user = await _authenticationService.AuthenticateAsync(command.Username, command.Password);

            if (user == null)
            {
                return null;
            }
            string token = _authenticationService.GenerateJwtToken(user);
            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
            };
            var authResponse = new AuthResponseDto
            {
                Token = token,
                User = userDto
            };

            return authResponse;
        }
    }
}
