using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AccountDTO
    {
        [Required(ErrorMessage = "Account is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Status { get; set; }

        public string? Role { get; set; }
    }

    public class GetAccountDTO : AccountDTO
    {
        public GetAccountDTO()
        {
            Orders = new HashSet<GetOrderDTO>();
        }

        public Guid AccountId { get; set; }

        public virtual ICollection<GetOrderDTO> Orders { get; set; }
    }
}
