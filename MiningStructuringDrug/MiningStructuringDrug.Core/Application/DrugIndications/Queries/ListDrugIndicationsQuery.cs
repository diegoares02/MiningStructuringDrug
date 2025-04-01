using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.DrugIndications.Dtos;

namespace MiningStructuringDrug.Core.Application.DrugIndications.Queries
{
    public class ListDrugIndicationsQuery : IQuery<List<DrugIndicationDto>>
    {
        public string? DrugName { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
