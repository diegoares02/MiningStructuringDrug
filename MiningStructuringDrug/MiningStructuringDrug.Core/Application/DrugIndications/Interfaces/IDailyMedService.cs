namespace MiningStructuringDrug.Core.Application.DrugIndications.Interfaces
{
    public interface IDailyMedService
    {
        Task<List<string>> GetDrugIndicationsAsync(string drugName);

        Task<string> GetDrugLabelAsync(string drugName);
    }
}
