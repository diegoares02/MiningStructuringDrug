using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.Users.Dtos;

namespace MiningStructuringDrug.Core.Application.Users.Queries
{
    public class GetUserQuery : IQuery<UserDto?>
    {
        public int Id { get; set; }
    }
}