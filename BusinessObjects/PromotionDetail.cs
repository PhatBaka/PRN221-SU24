using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("PromotionDetail")]
    public class PromotionDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionDetailId { get; set; }

        public int JewelryId { get; set; }

        public int PromotionId { get; set; }

        [Required]
        public double DiscountPercent { get; set; }

        public virtual Jewelry Jewelry { get; set; }

        public virtual Promotion Promotion { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}