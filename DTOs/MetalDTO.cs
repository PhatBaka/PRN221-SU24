using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs
{
    public class MetalDTO
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
        public decimal Price { get; set; }
        public decimal Ask { get; set; }
        public decimal Bid { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Change { get; set; }
        [JsonPropertyName("change_percent")]
        public decimal ChangePercent { get; set; }
    }
}
