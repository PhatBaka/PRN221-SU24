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
        [StringLength(100)]
        public string? MaterialName { get; set; }

        [Required]
        public bool IsMetal { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal MaterialCost { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public float MaterialWeight { get; set; }

        [Required]
        public string? UnitType { get; set; }

        public int MaterialQuantity { get; set; }

        [Required]
        public byte[] MaterialImage { get; set; }

        public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; }
    }
}
