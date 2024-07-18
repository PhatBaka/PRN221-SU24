using BusinessObjects.Enums;
using BusinessObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UI.Payload.JewerlyMaterialPayload;

namespace UI.Payload.JewelryPayload
{
    public class GetJewelryRequest
    {
        public int JewelryId { get; set; }

        public string JewelryName { get; set; }

        public string Description { get; set; }

        public double TotalWeight { get; set; }

        public double LaborPrice { get; set; }

        public int Quantity { get; set; }

        public double MarkupPercentage { get; set; }

        public int CategoryId { get; set; }

        public byte[]? JewelryImage { get; set; }

        public StatusSale StatusSale { get; set; }

        public virtual ICollection<GetJewelryMaterialRequest> JewelryMaterials { get; set; }

        //public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        //public virtual ICollection<PromotionDetail> PromotionDetails { get; set; }

        //public virtual Warranty Warranties { get; set; }

        //public virtual Category Category { get; set; }

    }
}
