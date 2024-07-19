using BusinessObjects.Enums;
using BusinessObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace UI.Payload.JewelryPayload
{
    public class ProductMaterialDto
    {
        public int MaterialId { get; set; }

        [Required(ErrorMessage = "The field material weight is required")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Material weight must be greater than 0")]
        public double MaterialQuantWeight { get; set; }

    }

}
