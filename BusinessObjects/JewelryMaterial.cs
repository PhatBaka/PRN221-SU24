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
        public int JewelryId { get; set; }

        public int MaterialId { get; set; }

        [Required]
        public double JewelryWeight { get; set; }

        public virtual Jewelry Jewelry { get; set; }

        public virtual Material Material { get; set; }
    }
}
