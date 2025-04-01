using MiningStructuringDrug.Core.Application.Common;

namespace MiningStructuringDrug.Core.Application.DrugIndications.Commands
{
    public class UpdateDrugIndicationCommand : ICommand
    {
        
        public int Id { get; set; }

        
        public string DrugName { get; set; }

        
        public List<string> Indications { get; set; } = new List<string>();
                
        public List<string> IcD10Codes { get; set; } = new List<string>();

    }
}
