using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.DrugIndications.Dtos;
using MiningStructuringDrug.Core.Application.DrugIndications.Queries;
using MiningStructuringDrug.Core.Domain.Interfaces;

namespace MiningStructuringDrug.Core.Application.DrugIndications.Handlers
{
    public class ListDrugIndicationsQueryHandler : IQueryHandler<ListDrugIndicationsQuery, List<DrugIndicationDto>>
    {
        private readonly IDrugIndicationRepository _drugIndicationRepository;

        public ListDrugIndicationsQueryHandler(IDrugIndicationRepository drugIndicationRepository)
        {
            _drugIndicationRepository = drugIndicationRepository;
        }

        public async Task<List<DrugIndicationDto>> Handle(ListDrugIndicationsQuery query)
        {
            var drugIndications = await _drugIndicationRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(query.DrugName))
            {
                drugIndications = drugIndications.Where(d => d.DrugName.Contains(query.DrugName, StringComparison.OrdinalIgnoreCase));
            }

            drugIndications = drugIndications.Skip((query.PageNumber - 1) * query.PageSize)
                                           .Take(query.PageSize);

            var dtos = drugIndications.Select(drugIndication => new DrugIndicationDto
            {
                Id = drugIndication.Id,
                DrugName = drugIndication.DrugName,
                Indications = drugIndication.Indications,
                IcD10Codes = drugIndication.IcD10Codes,
            }).ToList();

            return dtos;
        }
    }
}
