using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public OrderDetail()
        {
            Promotions = new HashSet<Promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }

        [Required]
        [ForeignKey("Jewelry")]
        public int JewelryId { get; set; }
        public virtual Jewelry? Jewelry { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public decimal DiscountValue { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal DiscountPrice { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal FinalPrice { get; set; }

        [Required]
        public int WarrantyOrderId { get; set; }
        public virtual WarrantyOrder? WarrantyOrder { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}
