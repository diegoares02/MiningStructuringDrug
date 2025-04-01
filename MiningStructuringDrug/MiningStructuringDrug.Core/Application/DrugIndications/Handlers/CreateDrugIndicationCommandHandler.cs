using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.DrugIndications.Commands;
using MiningStructuringDrug.Core.Application.DrugIndications.Dtos;
using MiningStructuringDrug.Core.Domain.Entities;
using MiningStructuringDrug.Core.Domain.Interfaces;

namespace MiningStructuringDrug.Core.Application.DrugIndications.Handlers
{
    public class CreateDrugIndicationCommandHandler : ICommandHandlerResult<CreateDrugIndicationCommand, DrugIndicationDto>
    {
        private readonly IDrugIndicationRepository _drugIndicationRepository;

        public CreateDrugIndicationCommandHandler(IDrugIndicationRepository drugIndicationRepository)
        {
            _drugIndicationRepository = drugIndicationRepository;
        }

        public async Task<DrugIndicationDto> Handle(CreateDrugIndicationCommand command)
        {
            var drugIndication = new DrugIndication
            {
                DrugName = command.DrugName,
                Indications = command.Indications,
                IcD10Codes = command.ICD10Codes,
            };

            drugIndication = await _drugIndicationRepository.AddAsync(drugIndication);

            var drugIndicationDto = new DrugIndicationDto
            {
                DrugName = command.DrugName,
                Indications = command.Indications,
                IcD10Codes = command.ICD10Codes,
            };
            return drugIndicationDto;
        }
    }
}
