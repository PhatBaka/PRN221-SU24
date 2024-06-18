using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            WarrantyRequests = new HashSet<WarrantyRequest>();
            Orders = new HashSet<Order>();
            Promotions = new HashSet<Promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(10)]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string? FullName { get; set; }

        public double TotalPoint { get; set; }

        [Required]
        public string? ObjectStatus { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }

        public virtual ICollection<WarrantyRequest> WarrantyRequests { get; set; }
    }
}