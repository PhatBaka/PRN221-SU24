using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DTOs
{
    public class JewelryDTO
    {
        public JewelryDTO()
        {
            Materials = new HashSet<GetMaterialDTO>();
        }

        public Guid? JewelryId { get; set; }

        public string? JewelryName { get; set; }

        public decimal? ManufacturingFees { get; set; }

        public string? Status { get; set; }

        public int Quantity { get; set; }

        public decimal TotalGemWeight { get; set; }
        public decimal TotalMetalWeight { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal TotalSellMaterialPrice { get; set; }

        public decimal TotalBuyMaterialPrice { get; set; }

        public decimal SellJewelryPrice { get; set; }

        public decimal BuyJewelryPrice { get; set; }

        public string? JewelryCategory { get; set; }

        public IFormFile? JewelryImageFile { get; set; }

        public decimal TotalSellGemPrice { get; set; }

        public decimal TotalBuyGemPrice { get; set; }

        public decimal TotalSellMetalPrice { get; set; }

        public decimal TotalBuyMetalPrice { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }


        public virtual ICollection<GetMaterialDTO> Materials { get; set; }
    }

    public class GetJewelryDTO : JewelryDTO 
    {
        public GetJewelryDTO()
        {
            OrderDetails = new HashSet<GetOrderDetailDTO>();
        }

        public Guid? JewelryId { get; set; }

        public byte[]? JewelryImageData { get; set; }

        public virtual ICollection<GetOrderDetailDTO> OrderDetails { get; set; }
    }
}
