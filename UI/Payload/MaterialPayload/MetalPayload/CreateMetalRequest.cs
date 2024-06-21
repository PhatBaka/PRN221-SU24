using BusinessObjects.Enums;
using BusinessObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UI.Payload.MaterialPayload.MetalPayload
{
    public class CreateMetalRequest
    {
        [Required()]
        public string? MaterialName { get; set; }

        [Required]
        public bool IsMetail { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double MaterialCost { get; set; }

        [Column(TypeName = "money")]
        public decimal BidPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal OfferPrice { get; set; }

        public string? Description { get; set; }

        public byte[]? MaterialImage { get; set; }

        [Required(ErrorMessage = "Metal image is required")]
        public IFormFile MetalImageData { get; set; }
    }
}
