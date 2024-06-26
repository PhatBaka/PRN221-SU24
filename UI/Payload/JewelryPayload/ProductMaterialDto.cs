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

        public double MaterialQuantWeight { get; set; }

    }

}
