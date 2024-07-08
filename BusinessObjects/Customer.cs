using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects;

[Table("Customer")]
public class Customer
{
    public Customer()
    {
        Orders = new HashSet<Order>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid CustomerId { get; set; }

    [Required]
    public string? PhoneNumber { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}
