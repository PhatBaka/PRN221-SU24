using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects;

[Table("Jewelry")]
public class Jewelry
{
    public Jewelry()
    {
        JewelryMaterials = new HashSet<JewelryMaterial>();
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

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalGemWeight { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalMetalWeight { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalWeight { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal MaterialPrice { get; set; }

    [Required]
    public string? JewelryCategory { get; set; }

    public byte[]? JewelryImageData { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalSellGemPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalBuyGemPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalSellMetalPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalBuyMetalPrice { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; } 

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } 
}
