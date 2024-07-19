using BusinessObjects.Enums;
using BusinessObjects;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UI.Helper;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;


namespace UI.Payload.JewelryPayload
{
    public class CreateJewelryRequest
    {
        [AllowNull]
        [HiddenInput(DisplayValue = false)]
        public int? JewelryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The field is required")]
        [MaxLength(50, ErrorMessage = "The max length of jewelry name is {0} characters")]
        [ModelBinder(BinderType = typeof(TrimAndRemoveInnerSpaceModelBinder))]
        public string JewelryName { get; set; }

        [DefaultValue("No description")]
        [AllowNull]
        [Required(ErrorMessage = "The field is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Total weight must be greater than 0")]
        public decimal TotalWeight { get; set; }

        [DefaultValue(0)]
		[Required(ErrorMessage = "The field is required")]
		public decimal LaborPrice { get; set; }

        [DefaultValue(0)]
		[Required(ErrorMessage = "The field is required")]
		public int Quantity { get; set; }

        [DefaultValue(0)]
		[Required(ErrorMessage = "The field is required")]
		[Range(0, 100, ErrorMessage = "Markup percentage must be greater or equal 0")]
        public double MarkupPercentage { get; set; }

        [Required(ErrorMessage = "The field is required")]
        public string CategoryName { get; set; }

        [RequireEnum(typeof(StatusSale), ErrorMessage = "The value for StatusSale must be one of the following: {0}")]
        [AllowNull]
        public StatusSale StatusSale { get; set; }

        [AllowNull]
        public List<IFormFile>? ImageDataFile { get; set; }

    }


    
}
