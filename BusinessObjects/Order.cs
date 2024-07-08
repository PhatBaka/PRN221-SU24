using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BusinessObjects;

namespace BusinessObjects;

[Table("Order")]
public class Order
{
    public Order()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }

    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

    public Guid OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal? DiscountPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal? FinalPrice { get; set; }

    public string? Status { get; set; }

    public string? OrderType { get; set; }

    public Guid AccountId { get; set; }

    public Guid CustomerId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}
