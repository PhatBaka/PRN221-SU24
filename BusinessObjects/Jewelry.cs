using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Enums;
using System.Runtime.Serialization;

namespace BusinessObjects
{
    [Table("Jewelry")]
    public class Jewelry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JewelryId { get; set; }

        [Required]
        [StringLength(300), ConcurrencyCheck]
        public string JewelryName { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Column("Weight")]
        public decimal TotalWeight { get; set; }

        [Required]
        [Column("WorkPrice")]
        public decimal LaborPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double MarkupPercentage { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public byte[]? JewelryImage { get; set; }

        [Required]
        public StatusSale StatusSale { get; set; }

        public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<PromotionDetail> PromotionDetails { get; set; }

        public virtual ICollection<Warranty> Warranties { get; set; }
        
        public virtual Category Category { get; set; }

      
    }
}
