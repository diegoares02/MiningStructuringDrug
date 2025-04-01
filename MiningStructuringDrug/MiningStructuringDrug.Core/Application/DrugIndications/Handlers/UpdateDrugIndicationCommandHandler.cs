using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.DrugIndications.Commands;
using MiningStructuringDrug.Core.Domain.Interfaces;

namespace MiningStructuringDrug.Core.Application.DrugIndications.Handlers
{
    public class UpdateDrugIndicationCommandHandler : ICommandHandler<UpdateDrugIndicationCommand>
    {
        private readonly IDrugIndicationRepository _drugIndicationRepository;

        public UpdateDrugIndicationCommandHandler(IDrugIndicationRepository drugIndicationRepository)
        {
            _drugIndicationRepository = drugIndicationRepository;
        }

        
        public async Task Handle(UpdateDrugIndicationCommand command)
        {
            var drugIndication = await _drugIndicationRepository.GetByIdAsync(command.Id);

            if (drugIndication == null)
            {
                throw new KeyNotFoundException($"DrugIndication with ID {command.Id} not found.");
            }

            drugIndication.DrugName = command.DrugName;
            drugIndication.Indications = command.Indications;
            drugIndication.IcD10Codes = command.IcD10Codes;

            await _drugIndicationRepository.UpdateAsync(drugIndication);
        }
    }
}
