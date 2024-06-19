using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class JewelryDTO
    {
        public JewelryDTO()
        {
            OrderDetails = new HashSet<OrderDetailDTO>();
            JewelryMaterials = new HashSet<JewelryMaterialDTO>();
            Promotions = new HashSet<PromotionDTO>();
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

        public virtual WarrantyJewelryDTO? WarrantyJewelry { get; set; }

        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; }

        public virtual ICollection<JewelryMaterialDTO> JewelryMaterials { get; set; }

        public virtual ICollection<PromotionDTO> Promotions { get; set; }
    }

    public class CreateJewelryDTO
    {

    }
}
