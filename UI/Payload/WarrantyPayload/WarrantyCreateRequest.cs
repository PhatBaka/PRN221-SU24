using BusinessObjects.Enums;
using BusinessObjects;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UI.Payload.WarrantyPayload
{
	public class WarrantyCreateRequest
	{

		[Required(ErrorMessage = "Warranty period is required")]
		public double WarrantyPeriod { get; set; }

		[Required(ErrorMessage = "Period unit measure is required")]
		public TimeEnum PeriodUnitmeasure { get; set; }

        public DateTime ActiveDate { get;  set; }

        public DateTime EndDate { get;  set; }

		[Required(ErrorMessage = "Jewelry ID is required")]
		public int JewelryId { get; set; }

        [Required(ErrorMessage = "Order ID is required")]
		public int OrderId { get; set; }

        public WarrantyStatus WarrantyStatus { get; set; }




    }
}
