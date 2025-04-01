using MiningStructuringDrug.Core.Application.Common;

namespace MiningStructuringDrug.Core.Application.DrugIndications.Commands
{
    public class ParseDrugIndicationCommand : ICommandResult<List<string>>
    {
        
        public string Text { get; set; }
        
        public string SourceUrl { get; set; }
    }
}
