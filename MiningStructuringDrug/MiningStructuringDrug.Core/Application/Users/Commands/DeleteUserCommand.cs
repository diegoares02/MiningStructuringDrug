using MiningStructuringDrug.Core.Application.Common;

namespace MiningStructuringDrug.Core.Application.Users.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public int Id { get; set; }
    }
}
