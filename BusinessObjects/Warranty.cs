using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Table("Warranty")]
    public class Warranty
    {
        public Warranty()
        {
            WarrantyRequests = new HashSet<WarrantyRequest>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WarrantyId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid OrderDetailId { get; set; }

        public virtual OrderDetail? OrderDetail { get; set; }

        public virtual ICollection<WarrantyRequest> WarrantyRequests { get; set; }
    }
}
