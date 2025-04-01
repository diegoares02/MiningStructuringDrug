using System.Text.RegularExpressions;
using HtmlAgilityPack;
using MiningStructuringDrug.Core.Application.DrugIndications.Interfaces;

namespace MiningStructuringDrug.Infrastructure.Services.DailyMed
{
    public class DailyMedService : IDailyMedService
    {
        private readonly HttpClient _httpClient;

        public DailyMedService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> GetDrugIndicationsAsync(string drugName)
        { 

            string labelText = await GetDrugLabelAsync(drugName);
            if (string.IsNullOrEmpty(labelText))
            {
                return new List<string>();
            }

            return ExtractIndicationsFromLabel(labelText);
        }

        public async Task<string> GetDrugLabelAsync(string drugName)
        {
            string apiUrl = $"https://dailymed.nlm.nih.gov/dailymed/search.cfm?labeltype=all&query={drugName}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); 

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Error fetching drug label: {ex.Message}");
                return null;
            }
        }

        private List<string> ExtractIndicationsFromLabel(string labelText)
        {

            List<string> indications = new List<string>();
            string indicationsSectionPattern = @"(?si)(INDICATIONS AND USAGE.*?)(?:DOSAGE|CONTRAINDICATIONS|WARNINGS)";

            Match match = Regex.Match(labelText, indicationsSectionPattern);

            if (match.Success)
            {
                string indicationsText = match.Groups[1].Value;
                indications.Add(indicationsText.Trim());
            }

            return indications;
        }
    }
}
