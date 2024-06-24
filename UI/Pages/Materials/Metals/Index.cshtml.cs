using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using Services.Interfaces;
using Newtonsoft.Json;
using UI.Payload;

namespace UI.Pages.Materials.Metals
{
    public class IndexModel : PageModel
    {
        private readonly IMaterialService _materialService;

        public IndexModel(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        public IList<Material> Material { get;set; } = default!;
        string apiKey = "goldapi-h8ho5slwgpx92p-io";
        string symbol = "XAU";
        string curr = "USD";

        public async Task OnGetAsync()
        {
            LoadData();
        }

        public async void OnPostUpdatePrice()
        {
            LoadData();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-access-token", apiKey);

                string url = $"https://www.goldapi.io/api/{symbol}/{curr}";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();
                    
                    // MetalPriceDto metalPrice = JsonConvert.DeserializeObject<MetalPriceDto>(result);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void LoadData()
        {
            Material = _materialService.GetMaterials().Where(x => x.IsMetail == true).ToList();
        }
    }
}
