using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int JewelryId { get; set; }

        public int? PromotionDetailId { get; set; }

        [Required]
        public double DiscountPercent { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double UnitPrice{ get; set; }

        public virtual Order Order { get; set; }

        public virtual Jewelry Jewelry { get; set; }

        public virtual PromotionDetail PromotionDetail { get; set; }
    }
}
