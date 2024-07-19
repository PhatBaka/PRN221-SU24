using BusinessObjects.Enums;
using BusinessObjects;
using System.ComponentModel.DataAnnotations.Schema;
using UI.Payload.JewelryPayload;

namespace UI.Payload.MaterialPayload.GemPayload
{
    public class GetGemRequest
    {
        public int MaterialId { get; set; }

        public string? MaterialName { get; set; }


        public double MaterialCost { get; set; }

        public String? Clarity { get; set; }

        public float Purity { get; set; }

        public string? Color { get; set; }

        public string? Sharp { get; set; }

        public string? Description { get; set; }

        public byte[]? MaterialImage { get; set; }

        public byte[]? GemCertificate { get; set; }

        public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; }
    }
}
