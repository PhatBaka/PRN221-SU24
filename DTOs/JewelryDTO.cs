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
        [Required]
        public string? JewelryName { get; set; }

        public decimal? ManufacturingFees { get; set; }

        public string? JewelryType { get; set; }

        public string? Status { get; set; }

        public int Quantity { get; set; }

        public decimal TotalWeight { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal MaterialPrice { get; set; }

        public IFormFile? JewelryImageFile { get; set; }

        [Required]
        public string? JewelryCategory { get; set; }

        public decimal TotalGemPrice { get; set; }

        public decimal TotalMetalPrice { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }

    public class GetJewelryDTO : JewelryDTO 
    {
        public GetJewelryDTO()
        {
            Materials = new HashSet<GetMaterialDTO>();
            OrderDetails = new HashSet<GetOrderDetailDTO>();
        }

        public Guid? JewelryId { get; set; }

        public byte[]? JewelryImageData { get; set; }

        public virtual ICollection<GetMaterialDTO> Materials { get; set; }

        public virtual ICollection<GetOrderDetailDTO> OrderDetails { get; set; }
    }
}
