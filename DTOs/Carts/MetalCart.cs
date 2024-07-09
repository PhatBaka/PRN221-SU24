using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Carts
{
    public class MetalCart
    {
        public string? Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Weight { get; set; }
        public decimal TotalPrice { get; set; }
        public string? ImagePath { get; set; }
    }
}
