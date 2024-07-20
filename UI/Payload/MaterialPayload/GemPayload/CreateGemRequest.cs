using BusinessObjects.Enums;
using BusinessObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UI.Payload.MaterialPayload.GemPayload
{
    public class CreateGemRequest
    {
        public string? MaterialName { get; set; }

        public double MaterialCost { get; set; }

        public ClarityEnum Clarity { get; set; }

        public decimal Purity { get; set; }

        public string? Color { get; set; }

        public string? Sharp { get; set; }

        public string? Description { get; set; }

        public IFormFile? MaterialImageData { get; set; }

        public IFormFile? GemCertificateData { get; set; }

        public byte[]? MaterialImage { get; set; }

        public byte[]? GemCertificateImage { get; set; }
    }
}
