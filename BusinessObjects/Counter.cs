using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Table("Counter")]
    public class Counter
    {
        public Counter()
        {
            Accounts = new HashSet<Account>();
            Jewelries = new HashSet<Jewelry>();
            Materials = new HashSet<Material>();
            Orders = new HashSet<Order>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CounterId { get; set; }

        [Required]
        public string? CounterName { get; set; }

        [Column(TypeName = "money")]
        public decimal Revenue { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    
        public virtual ICollection<Jewelry> Jewelries { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
    }
}
