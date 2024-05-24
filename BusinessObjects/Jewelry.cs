using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Jewelry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JewelryId { get; set; }

        [Required]
        [StringLength(50)]
        public string JewelryName { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [Required]
        public decimal WorkPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public byte[] JewelryImage { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        
        [Required]
        public virtual Category Category { get; set; }

        public virtual Promotion Promotion { get; set; }
    }
}
