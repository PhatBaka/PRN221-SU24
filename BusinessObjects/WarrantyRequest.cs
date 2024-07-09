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
        public Guid WarrantyRequestId { get; set; }

        public String? Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Guid AccountId { get; set; }

        public virtual Account? Account { get; set; }

        public Guid WarrantyId { get; set; }

        public virtual Warranty? Warranty { get; set; }
    }
}
