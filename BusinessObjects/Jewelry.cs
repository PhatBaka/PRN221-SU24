using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects;

[Table("Jewelry")]
public class Jewelry
{
    public Jewelry()
    {
        Materials = new HashSet<Material>();
        OrderDetails = new HashSet<OrderDetail>();
    }

    public Guid? JewelryId { get; set; }

    [Required]
    public string? JewelryName { get; set; }

    [Column(TypeName = "money")]
    public decimal? ManufacturingFees { get; set; }

    public string? JewelryType { get; set; }

    public string? Status { get; set; }

    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalWeight { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal UnitPrice { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal MaterialPrice { get; set; }

    [Required]
    public string? JewelryCategory { get; set; }

    public byte[]? JewelryImageData { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalGemPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalMetalPrice { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Material> Materials { get; set; } 

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } 
}
