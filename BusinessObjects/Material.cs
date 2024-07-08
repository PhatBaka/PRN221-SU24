using BusinessObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects;

[Table("Material")]
public class Material
{
    public Material()
    {
        Jewelries = new HashSet<Jewelry>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid MaterialId { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [Column(TypeName = "money")]
    public decimal CurrentPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal SellPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal BuyPrice { get; set; }

    public byte[] ?MaterialImageData { get; set; }

    public byte[]? CertificateImageData { get; set; } 

    public bool IsMetal { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Weight { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Purity { get; set; }

    public string? Clarity { get; set; }

    public string? Color { get; set; }

    public string? Sharp { get; set; }

    public string? MaterialStatus { get; set; }

    public virtual ICollection<Jewelry> Jewelries { get; set; }
}
