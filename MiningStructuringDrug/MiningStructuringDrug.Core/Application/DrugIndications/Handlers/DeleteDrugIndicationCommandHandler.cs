using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.DrugIndications.Commands;
using MiningStructuringDrug.Core.Domain.Interfaces;

namespace MiningStructuringDrug.Core.Application.DrugIndications.Handlers
{
    public class DeleteDrugIndicationCommandHandler : ICommandHandler<DeleteDrugIndicationCommand>
    {
        private readonly IDrugIndicationRepository _drugIndicationRepository;

        public DeleteDrugIndicationCommandHandler(IDrugIndicationRepository drugIndicationRepository)
        {
            _drugIndicationRepository = drugIndicationRepository;
        }

        public async Task Handle(DeleteDrugIndicationCommand command)
        {
            await _drugIndicationRepository.DeleteAsync(command.Id);
        }
    }
}
