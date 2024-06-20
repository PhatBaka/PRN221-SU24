using BusinessObjects.Enums;
using BusinessObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UI.Payload.MaterialPayload.GemPayload
{
    public class CreateGemRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string? MaterialName { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        [Column(TypeName = "money")]
        public double MaterialCost { get; set; }

        [Required(ErrorMessage = "Clarity is required")]
        public ClarityEnum Clarity { get; set; }

        [Required(ErrorMessage = "Purity is required")]
        [Column(TypeName = "float")]
        public decimal Purity { get; set; }

        [Required(ErrorMessage = "Color is required")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Sharp is required")]
        public string? Sharp { get; set; }

        public string? Description { get; set; }

        [Required]
        public IFormFile? MaterialImageData { get; set; }

        [Required]
        public IFormFile? GemCertificateData { get; set; }

        public byte[]? MaterialImage { get; set; }

        public byte[]? GemCertificateImage { get; set; }
    }
}
