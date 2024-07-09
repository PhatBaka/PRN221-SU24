using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class WarrantyDTO
    {
        public Guid WarrantyId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid OrderDetailId { get; set; }
    }

    public class GetWarrantyDTO : WarrantyDTO
    {
        public GetWarrantyDTO()
        {
            WarrantyRequests = new HashSet<GetWarrantyRequestDTO>();
        }

        public virtual GetOrderDetailDTO? OrderDetail { get; set; }

        public virtual ICollection<GetWarrantyRequestDTO> WarrantyRequests { get; set; }
    }
}
