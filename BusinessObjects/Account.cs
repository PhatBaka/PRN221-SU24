using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Enums;

namespace BusinessObjects
{
    [Table("Account")]
    public class Account
    {
        public Account()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? Password { get; set; }

        [Required]
        [StringLength(10)]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        public AccountRole? Role { get; set; }

        [Required]
        public ObjectStatus? ObjectStatus { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
