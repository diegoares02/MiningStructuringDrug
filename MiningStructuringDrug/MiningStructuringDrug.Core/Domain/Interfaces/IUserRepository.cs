using MiningStructuringDrug.Core.Domain.Entities;

namespace MiningStructuringDrug.Core.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
    }
}
