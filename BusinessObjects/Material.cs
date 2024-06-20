using BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Table("Material")]
    public class Material
    {
        public Material()
        {
            JewelryMaterials = new HashSet<JewelryMaterial>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialId { get; set; }

        [Required]
        public string? MaterialName { get; set; }

        [Required]
        public bool IsMetail { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double MaterialCost { get; set; }

        public ClarityEnum Clarity { get; set; }

        [Column(TypeName = "float")]
        public decimal Purity { get; set; }

        public string? Color { get; set; }

        public string? Sharp { get; set; }

        [Column(TypeName = "money")]
        public decimal BidPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal OfferPrice { get; set; }

        public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; }
    }
}
