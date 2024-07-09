using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class JewelryMaterial
    {
        public Guid JewelryId { get; set; }
        public Jewelry Jewelry { get; set; }

        public Guid MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
