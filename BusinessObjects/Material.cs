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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialId { get; set; }

        [Required]
        [StringLength(100)]
        public string MaterialName { get; set; }

        [Required]
        public bool IsMetail { get; set; }

        [Required]
        public double MaterialCost { get; set; }

        public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; }
    }
}
