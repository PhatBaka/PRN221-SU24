using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Table("JewelryMaterial")]
    public class JewelryMaterial
    {
        [Required]
        public int JewelryId { get; set; }
        public virtual Jewelry? Jewelry { get; set; }

        [Required]
        public int MaterialId { get; set; }
        public virtual Material? Material { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public float MetalWeight { get; set; }
    }
}
