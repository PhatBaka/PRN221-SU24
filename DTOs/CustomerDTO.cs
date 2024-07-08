using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CustomerDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerId { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }
    }

    public class GetCustomerDTO : CustomerDTO
    {
        public GetCustomerDTO()
        {
            Orders = new HashSet<GetOrderDTO>();
        }

        public virtual ICollection<GetOrderDTO> Orders { get; set; }
    }
}
