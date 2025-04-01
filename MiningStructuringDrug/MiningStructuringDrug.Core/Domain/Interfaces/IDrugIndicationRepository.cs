using MiningStructuringDrug.Core.Domain.Entities;

namespace MiningStructuringDrug.Core.Domain.Interfaces
{
    public interface IDrugIndicationRepository
    {
        Task<IEnumerable<DrugIndication>> GetAllAsync();
        Task<DrugIndication?> GetByIdAsync(int id);
        Task<DrugIndication?> AddAsync(DrugIndication drugIndication);
        Task UpdateAsync(DrugIndication drugIndication);
        Task DeleteAsync(int id);
    }
}
