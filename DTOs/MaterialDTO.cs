using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DTOs
{
    public class MaterialDTO
    {
        public MaterialDTO()
        {
            JewelryMaterials = new HashSet<JewelryMaterialDTO>();
        }

        public int MaterialId { get; set; }

        public string? MaterialName { get; set; }

        public bool IsMetal { get; set; }

        public decimal MaterialCost { get; set; }

        public float MaterialWeight { get; set; }

        public string? UnitType { get; set; }

        public int MaterialQuantity { get; set; }

        public byte[]? MaterialImage { get; set; }

        public virtual ICollection<JewelryMaterialDTO> JewelryMaterials { get; set; }
    }
}
