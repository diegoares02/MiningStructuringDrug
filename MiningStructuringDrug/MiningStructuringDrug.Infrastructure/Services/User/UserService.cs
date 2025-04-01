using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.Users.Commands;
using MiningStructuringDrug.Core.Application.Users.Dtos;
using MiningStructuringDrug.Core.Application.Users.Interfaces;
using MiningStructuringDrug.Core.Application.Users.Queries;

namespace MiningStructuringDrug.Infrastructure.Services.User
{
    public class UserService : IUserService
    {
        private readonly IMessageDispatcher _messageDispatcher;

        public UserService(IMessageDispatcher messageDispatcher)
        {
            _messageDispatcher = messageDispatcher;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserCommand createUserCommand)
        {
            return await _messageDispatcher.DispatchCommand<CreateUserCommand, UserDto>(createUserCommand);
        }

        public async Task<AuthResponseDto?> AuthenticateUserAsync(AuthenticateUserCommand authenticateUserCommand)
        {
            return await _messageDispatcher.DispatchCommand<AuthenticateUserCommand, AuthResponseDto?>(authenticateUserCommand);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _messageDispatcher.Dispatch<DeleteUserCommand>(new DeleteUserCommand { Id = userId });
        }

        public async Task UpdateUserRoleAsync(UpdateUserRoleCommand updateUserRoleCommand)
        {
            await _messageDispatcher.Dispatch<UpdateUserRoleCommand>(updateUserRoleCommand);
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            return await _messageDispatcher.DispatchQuery<GetUserQuery, UserDto?>(new GetUserQuery { Id = userId });
        }
    }
}
