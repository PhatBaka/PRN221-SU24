using BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class WarrantyHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WarrantyHistoryId { get; set; }

        [Required]
        public DateTime ReceivedDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public WarrantyFixStatus status { get; set; }
		
        [Required]
        [StringLength(int.MaxValue)]
        public string RequireDescription { get; set; }

        [AllowNull]
        [StringLength(int.MaxValue)]
		public string ResultReport { get; set; }

        [Required]
		public int WarrantyId { get; set; }

        public virtual Warranty Warranty { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

		[Required]
		public string CustomerName { get; set; }

		[Required]
		public string CustomerPhone { get; set; }



	}
}
