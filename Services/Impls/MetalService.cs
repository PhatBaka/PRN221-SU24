using Services.Helpers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services.Impls
{
    public class MetalService : IMetalService
    {
        public IList<MetalResponse> GetPrices()
        {
            IList<MetalResponse> metals = new List<MetalResponse>();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var gold = JsonSerializer.Deserialize<MetalResponse>(Util.ReadJsonFile("gold.json"), options);
            var silver = JsonSerializer.Deserialize<MetalResponse>(Util.ReadJsonFile("silver.json"), options);
            var palladium = JsonSerializer.Deserialize<MetalResponse>(Util.ReadJsonFile("palladium.json"), options);
            if (gold != null && silver != null && palladium != null)
            {
                metals.Add(gold);
                metals.Add(silver);
                metals.Add(palladium);
            }
            return metals;
        }
    }
}
