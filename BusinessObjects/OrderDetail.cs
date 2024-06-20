using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int OrderId { get; set; }

        public int JewelryId { get; set; }

        [Required]
        public decimal Dicount { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal DiscountPrice { get; set;  }

        [Required]
        public decimal FinalPrice { get; set; }

        public int PromotionId { get; set; }

        public virtual Promotion Promotion { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public virtual Jewelry Jewelry { get; set; }
    }
}
