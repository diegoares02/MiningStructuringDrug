using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.Users.Dtos;

namespace MiningStructuringDrug.Core.Application.Users.Commands
{
    public class CreateUserCommand : ICommandResult<UserDto>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
