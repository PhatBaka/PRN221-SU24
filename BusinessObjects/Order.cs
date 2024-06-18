using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Promotions = new HashSet<Promotion>();
        }

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
        public string? OrderType { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public virtual Customer? Customer { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public virtual Account? Account { get; set; }

        [Required]
        public int CounterId { get; set; }

        [Required]
        public virtual Counter? Counter { get; set; }

        [Required]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}
