using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Helpers;
using System.Text.Json;

namespace UI.Pages.Metals
{
    public class IndexModel : PageModel
    {
        public IList<MetalDTO> MetalDTOs = new List<MetalDTO>();
        private IConfiguration Configuration { get; set; }
        public string? UpdatedDate { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void OnGetAsync()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var gold = JsonSerializer.Deserialize<MetalDTO>(Util.ReadJsonFile("gold.json"), options);
            var silver = JsonSerializer.Deserialize<MetalDTO>(Util.ReadJsonFile("silver.json"), options);
            var palladium = JsonSerializer.Deserialize<MetalDTO>(Util.ReadJsonFile("palladium.json"), options);
            if (gold != null && silver != null && palladium != null)
            {
                MetalDTOs.Add(gold);
                MetalDTOs.Add(silver);
                MetalDTOs.Add(palladium);
            }
            UpdatedDate = gold.Timestamp.ToString();
        }
    }
}
