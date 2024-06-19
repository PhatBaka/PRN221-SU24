using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class JewelryMaterialDTO
    {
        public int JewelryId { get; set; }
        public virtual JewelryDTO? Jewelry { get; set; }

        public int MaterialId { get; set; }
        public virtual MaterialDTO? Material { get; set; }

        public float MetalWeight { get; set; }

        public int NumberOfGem { get; set; }
    }
}
