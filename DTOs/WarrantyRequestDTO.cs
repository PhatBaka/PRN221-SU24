using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class WarrantyRequestDTO
    {
        public String? Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Guid AccountId { get; set; }

        public Guid WarrantyId { get; set; }
    }

    public class GetWarrantyRequestDTO : WarrantyDTO
    {
        public Guid WarrantyRequestId { get; set; }
        public virtual AccountDTO? Account { get; set; }
        public virtual WarrantyDTO? Warranty { get; set; }
    }
}
