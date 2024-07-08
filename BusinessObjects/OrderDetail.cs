using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects;

[Table("OrderDetail")]
public class OrderDetail
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public Guid OrderDetailId { get; set; }

    public Guid OrderId { get; set; }

    public Guid? JewelryId { get; set; }

    [Column(TypeName = "money")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal? DiscountPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal FinalPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountValue { get; set; }

    [Required]
    public int Quantity { get; set; }

    public virtual Jewelry? Jewelry { get; set; }

    public virtual Order? Order { get; set; }
}
