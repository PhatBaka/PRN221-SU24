using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("Promotion")]
    public class Promotion
    {
        public Promotion()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Jewelries = new HashSet<Jewelry>();
            Orders = new HashSet<Order>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionId { get; set; }

        [StringLength(100)]
        public string? PromotionCode { get; set; }

        [StringLength(50)]
        public string? PromotionName { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal DiscountValue { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal AcceptedPrice { get; set; }

        public string? PromotionType { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } 

        public virtual ICollection<Jewelry> Jewelries { get; set; } 

        public virtual ICollection<Order> Orders { get; set; }
    }
}
