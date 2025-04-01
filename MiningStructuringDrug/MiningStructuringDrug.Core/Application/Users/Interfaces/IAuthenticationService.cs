using MiningStructuringDrug.Core.Domain.Entities;

namespace MiningStructuringDrug.Core.Application.Users.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(User user, string password);
        string GenerateJwtToken(User user);
    }
}
