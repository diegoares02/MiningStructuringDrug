using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.Users.Commands;
using MiningStructuringDrug.Core.Domain.Interfaces;

namespace MiningStructuringDrug.Core.Application.Users.Handlers
{
    public class UpdateUserRoleCommandHandler : ICommandHandler<UpdateUserRoleCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserRoleCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        
        public async Task Handle(UpdateUserRoleCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.Id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {command.Id} not found.");
            }

            user.Role = command.Role;

            await _userRepository.UpdateAsync(user);
        }
    }
}
