using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Table("WarrantyHistory")]
    public class WarrantyHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WarrantyHistoryId { get; set; }

        [Required]
        public DateTime ReceivedDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        public int WarrantyId { get; set; }

        public virtual Warranty Warranty { get; set; }
    }
}
