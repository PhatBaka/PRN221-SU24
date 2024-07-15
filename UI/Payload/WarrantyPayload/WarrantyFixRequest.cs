using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;

namespace UI.Payload.WarrantyPayload
{
    public class WarrantyFixRequest
    {
        [Required]
        public DateTime ReceivedDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        public string RequireDescription { get; set; }

        [StringLength(int.MaxValue)]
        [NullSubstitute("Has not report yet")]
        public string ResultReport { get; set; }

        [Required]
        public int WarrantyId { get; set; }

		[Required]
		public string CustomerName { get; set; }

		[Required]
		[RegularExpression(@"^\+?(\d{1,3})?[-. \s]?(\d{3})?[-. \s]?(\d{3})?[-. \s]?(\d{4})$", ErrorMessage = "Invalid phone number format.")]
		public string CustomerPhone { get; set; }
	}
}
