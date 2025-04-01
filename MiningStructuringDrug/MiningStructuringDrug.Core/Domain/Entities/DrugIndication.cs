namespace MiningStructuringDrug.Core.Domain.Entities
{
    public class DrugIndication
    {
        public int Id { get; set; }
        public string DrugName { get; set; }
        public List<string> Indications { get; set; } = new List<string>();
        public List<string> IcD10Codes { get; set; } = new List<string>();
    }
}
