using Services.Helpers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

        public void GetMetalSellPriceAndBuybackPriceByMetalName(string metalName, out double BIDPRICE, out double OFFERPRICE)
        {
            double bidPrice = 0;
            double offerPrice = 0;
            
            switch (metalName.ToLower())
            {
                case "gold":
                    bidPrice = this.GetPrices().FirstOrDefault(x => x.Metal.Equals("gold")).Rate.Bid;
                    offerPrice = this.GetPrices().FirstOrDefault(x => x.Metal.Equals("gold")).Rate.Ask;
                    break;
                case "silver":
                    bidPrice = this.GetPrices().FirstOrDefault(x => x.Metal.Equals("silver")).Rate.Bid;
                    offerPrice = this.GetPrices().FirstOrDefault(x => x.Metal.Equals("silver")).Rate.Ask;
                    break;
                case "palladium":
                    bidPrice = this.GetPrices().FirstOrDefault(x => x.Metal.Equals("palladium")).Rate.Bid;
                    offerPrice = this.GetPrices().FirstOrDefault(x => x.Metal.Equals("palladium")).Rate.Ask;
                    break;
            }
            BIDPRICE = bidPrice;
            OFFERPRICE = offerPrice;
            
        }
    }
}
