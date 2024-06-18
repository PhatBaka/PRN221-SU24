using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("WarrantyJewelry")]
    public class WarrantyJewelry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WarrantyJewelryId { get; set; }

        [Required]
        public int WarrantyMonths { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public int JewelryId { get; set; }

        public virtual Jewelry? Jewelry { get; set; }
    }
}
