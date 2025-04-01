namespace MiningStructuringDrug.Core.Domain.Entities
{
    public class CopayCard
    {
        public int Id { get; set; }
        public string ProgramName { get; set; }
        public List<string> CoverageEligibilities { get; set; } = new List<string>();
        public string ProgramType { get; set; }
        public List<Requirement> Requirements { get; set; } = new List<Requirement>();
        public List<Benefit> Benefits { get; set; } = new List<Benefit>();
        public List<Form> Forms { get; set; } = new List<Form>();
        public Funding Funding { get; set; }
        public List<Detail> Details { get; set; } = new List<Detail>();
    }


    public class Requirement
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class Benefit
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }


    public class Form
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }


    public class Funding
    {
        public string Evergreen { get; set; }
        public string CurrentFundingLevel { get; set; }
    }


    public class Detail
    {

        public string Eligibility { get; set; }
        public string Program { get; set; }
        public string Renewal { get; set; }
        public string Income { get; set; }
    }
}
