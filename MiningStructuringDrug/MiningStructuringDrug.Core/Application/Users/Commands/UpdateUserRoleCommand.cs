using MiningStructuringDrug.Core.Application.Common;

namespace MiningStructuringDrug.Core.Application.Users.Commands
{
    public class UpdateUserRoleCommand : ICommand
    {
        public int Id { get; set; }
        public string Role { get; set; }
    }
}
