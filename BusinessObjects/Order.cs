using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Enums;

namespace BusinessObjects
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public double DiscountPrice { get; set; }

        [Required]
        public double FinalPrice { get; set; }

        [Required]
        public OrderEnum OrderType { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public int PromotionId { get; set; }

        public virtual Promotion Promotion { get; set; }

        [Required]
        public virtual Account Customer { get; set; }

        [Required]
        public virtual ICollection<Warranty> Warranties { get; set; }

        [Required]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
