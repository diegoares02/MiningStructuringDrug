using MiningStructuringDrug.Core.Application.Users.Commands;
using MiningStructuringDrug.Core.Application.Users.Dtos;

namespace MiningStructuringDrug.Core.Application.Users.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserCommand createUserCommand);
        Task<AuthResponseDto?> AuthenticateUserAsync(AuthenticateUserCommand authenticateUserCommand);
        Task DeleteUserAsync(int userId);
        Task UpdateUserRoleAsync(UpdateUserRoleCommand updateUserRoleCommand);
        Task<UserDto?> GetUserByIdAsync(int userId);
    }
}
