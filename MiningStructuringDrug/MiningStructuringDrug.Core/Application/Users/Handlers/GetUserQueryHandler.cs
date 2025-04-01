using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.Users.Dtos;
using MiningStructuringDrug.Core.Application.Users.Queries;
using MiningStructuringDrug.Core.Domain.Interfaces;

namespace MiningStructuringDrug.Core.Application.Users.Handlers
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto?> Handle(GetUserQuery query)
        {
            var user = await _userRepository.GetByIdAsync(query.Id);
            if (user == null)
            {
                return null;
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };

            return userDto;
        }
    }
}
