using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class MetalDTO
    {
        public string? Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public decimal SellPrice { get; set; }

        public decimal BuyPrice { get; set; }

        public bool IsMetal { get; set; }

        public decimal Weight { get; set; }

        public decimal BidPrice { get; set; }

        public decimal AskPrice { get; set; }
    }

    public class GetMetalDTO : MetalDTO
    {
        public GetMetalDTO()
        {
            Jewelries = new HashSet<GetJewelryDTO>();
        }

        public Guid MaterialId { get; set; }


        public virtual ICollection<GetJewelryDTO> Jewelries { get; set; }
    }
}
