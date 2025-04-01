using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.Users.Dtos;

namespace MiningStructuringDrug.Core.Application.Users.Commands
{
    public class AuthenticateUserCommand : ICommandResult<AuthResponseDto?>
    {
        
        public string Username { get; set; }

        
        public string Password { get; set; }
    }
}
