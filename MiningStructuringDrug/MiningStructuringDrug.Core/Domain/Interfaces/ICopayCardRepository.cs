using MiningStructuringDrug.Core.Domain.Entities;

namespace MiningStructuringDrug.Core.Domain.Interfaces
{
    public interface ICopayCardRepository
    {
        Task<IEnumerable<CopayCard>> GetAllAsync();
        Task<CopayCard?> GetByIdAsync(int id);
        Task AddAsync(CopayCard copayCard);
        Task UpdateAsync(CopayCard copayCard);
        Task DeleteAsync(int id);
    }
}
