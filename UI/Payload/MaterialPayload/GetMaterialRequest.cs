using BusinessObjects.Enums;
using BusinessObjects;
using System.ComponentModel.DataAnnotations.Schema;
using UI.Payload.JewerlyMaterialPayload;

namespace UI.Payload.MaterialPayload
{
    public class GetMaterialRequest
    {
        public int MaterialId { get; set; }

        public string? MaterialName { get; set; }

        public bool IsMetail { get; set; }

        public double MaterialCost { get; set; }

        public ClarityEnum Clarity { get; set; }

        public float Purity { get; set; }

        public string? Color { get; set; }

        public string? Sharp { get; set; }

        public decimal BidPrice { get; set; }

        public decimal OfferPrice { get; set; }

        public string? Description { get; set; }

        public byte[]? MaterialImage { get; set; }

        public byte[]? GemCertificate { get; set; }

        //public virtual ICollection<GetJewelryMaterialRequest> JewelryMaterials { get; set; }
    }
}
