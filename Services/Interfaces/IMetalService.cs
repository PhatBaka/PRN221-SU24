using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMetalService
    {
        public IList<MetalResponse> GetPrices();
        public void GetMetalSellPriceAndBuybackPriceByMetalName(string metalName, out double BIDPRICE, out double OFFERPRICE);

    }

    public class MetalResponse
    {
        public string? Status { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Currency { get; set; }
        public string? Unit { get; set; }
        public string? Metal { get; set; }
        public Rate? Rate { get; set; }
    }

    public class Rate
    {
        public double Price { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Change { get; set; }
        [JsonPropertyName("change_percent")]
        public double ChangePercent { get; set; }
    }
}
