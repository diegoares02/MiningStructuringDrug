using MiningStructuringDrug.Core.Application.Common;

namespace MiningStructuringDrug.Core.Application.DrugIndications.Commands
{
    public class DeleteDrugIndicationCommand : ICommand
    {
        public int Id { get; set; }
    }
}
