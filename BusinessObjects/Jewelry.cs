using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("Jewelry")]
    public class Jewelry
    {
        public Jewelry()
        {
            OrderDetails = new HashSet<OrderDetail>();
            JewelryMaterials = new HashSet<JewelryMaterial>();
            Promotions = new HashSet<Promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JewelryId { get; set; }

        [Required]
        [StringLength(50)]
        public string? JewelryName { get; set; }

        [Required]
        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public decimal Weight { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal WorkPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public byte[]? JewelryImage { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double MarkupPercentage { get; set; }

        [Required]
        public string? Category { get; set; }

        [Required]
        public string? ObjectStatus { get; set; }

        public virtual WarrantyJewelry? WarrantyJewelry { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}
