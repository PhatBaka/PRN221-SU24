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
        public AccountDTO()
        {
            Orders = new HashSet<OrderDTO>();
            Promotions = new HashSet<PromotionDTO>();
            Counters = new HashSet<CounterDTO>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [StringLength(10)]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string? FullName { get; set; }

        [Required]
        public string? Role { get; set; }

        [Required]
        public string? ObjectStatus { get; set; }

        public virtual ICollection<OrderDTO> Orders { get; set; }

        public virtual ICollection<PromotionDTO> Promotions { get; set; }

        public virtual ICollection<CounterDTO> Counters { get; set; }
    }
}
