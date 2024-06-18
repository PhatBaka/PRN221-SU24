using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Table("WarrantyRequest")]
    public class WarrantyRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WarrantyRequestId { get; set; }

        [Required]
        public DateTime? ReceivedDate { get; set; }

        [Required]
        public DateTime? ReturnDate { get; set; }

        [Required]
        public string? WarrantyStatus { get; set; }

        [Required]
        public int WarrantyOrderId { get; set; }

        public virtual WarrantyOrder? WarrantyOrder { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
