using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.DrugIndications.Dtos;

namespace MiningStructuringDrug.Core.Application.DrugIndications.Queries
{
    public class GetDrugIndicationQuery : IQuery<DrugIndicationDto?>
    {
        public int Id { get; set; }
    }
}
