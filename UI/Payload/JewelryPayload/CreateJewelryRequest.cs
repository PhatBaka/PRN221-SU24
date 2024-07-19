using BusinessObjects.Enums;
using BusinessObjects;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UI.Payload.JewelryPayload
{
    public class CreateJewelryRequest
    {
        // Uncomment or adjust as needed
        // [AllowNull]
        // [HiddenInput(DisplayValue = false)]
        // public int? JewelryId { get; set; }

        // Removed [Required] and [MaxLength] data annotations
        public string JewelryName { get; set; }

        // Removed [Required] data annotation
        public string? Description { get; set; }

        // Removed [Range] data annotation
        // public decimal TotalWeight { get; set; }

        // Removed [Required] and [DefaultValue] data annotations
        public decimal LaborPrice { get; set; }

        // Removed [Required] and [DefaultValue] data annotations
        public int Quantity { get; set; }

        // Removed [Required] data annotation
        // public double MarkupPercentage { get; set; }

        // Removed [Required] data annotation
        // public string CategoryName { get; set; }

        // Removed [RequireEnum] data annotation
        // public StatusSale StatusSale { get; set; }

        // Removed [AllowNull] data annotation
        public IFormFile? ImageDataFile { get; set; }

        // TODO: public virtual Warranty Warranties { get; set; }

        public int CategoryId { get; set; }
    }
}
