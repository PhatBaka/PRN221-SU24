using BusinessObjects;
using UI.Payload.JewelryPayload;
using UI.Payload.MaterialPayload;

namespace UI.Payload.JewerlyMaterialPayload
{
    public class GetJewelryMaterialRequest
    {
        public int JewelryId { get; set; }

        public int MaterialId { get; set; }

        public double JewelryWeight { get; set; }

        //public virtual GetJewelryRequest Jewelry { get; set; }

        public virtual GetMaterialRequest Material { get; set; }
    }
}
