using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.DrugIndications.Dtos;
using MiningStructuringDrug.Core.Application.DrugIndications.Queries;
using MiningStructuringDrug.Core.Domain.Interfaces;

namespace MiningStructuringDrug.Core.Application.DrugIndications.Handlers
{
    public class GetDrugIndicationQueryHandler : IQueryHandler<GetDrugIndicationQuery, DrugIndicationDto?>
    {
        private readonly IDrugIndicationRepository _drugIndicationRepository;

        public GetDrugIndicationQueryHandler(IDrugIndicationRepository drugIndicationRepository)
        {
            _drugIndicationRepository = drugIndicationRepository;
        }

        public async Task<DrugIndicationDto?> Handle(GetDrugIndicationQuery query)
        {
            var drugIndication = await _drugIndicationRepository.GetByIdAsync(query.Id);

            if (drugIndication == null)
            {
                return null;
            }

            var dto = new DrugIndicationDto
            {
                Id = drugIndication.Id,
                DrugName = drugIndication.DrugName,
                Indications = drugIndication.Indications,
                IcD10Codes = drugIndication.IcD10Codes,
            };

            return dto;
        }
    }
}
