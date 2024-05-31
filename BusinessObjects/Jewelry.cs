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
    [Table("Jewelry")]
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
        public double MarkupPercentage { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public StatusSale StatusSale { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; }

        public virtual ICollection<Warranty> Warranties { get; set; }
        
        [Required]
        public virtual Category Category { get; set; }
    }
}
