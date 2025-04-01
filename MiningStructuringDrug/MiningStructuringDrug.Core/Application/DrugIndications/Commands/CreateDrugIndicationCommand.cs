using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.DrugIndications.Dtos;

namespace MiningStructuringDrug.Core.Application.DrugIndications.Commands
{
    public class CreateDrugIndicationCommand : ICommandResult<DrugIndicationDto>
    {
        public string DrugName { get; set; } = string.Empty;
        public List<string> Indications { get; set; } = new List<string>();
        public List<string> ICD10Codes { get; set; } = new List<string>();
    }
}
