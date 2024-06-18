using BusinessObjects.Enums;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class JewelryDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JewelryId { get; set; }

        [Required]
        [StringLength(50)]
        public string JewelryName { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [Required]
        public decimal WorkPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public byte[] JewelryImage { get; set; }

        [Required]
        public double MarkupPercentage { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public StatusSale StatusSale { get; set; }

        [Required]
        public CategoryEnum Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; }

        public virtual ICollection<Warranty> Warranties { get; set; }
    }

    public class CreateJewelryDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name length should under 50")]
        public string JewelryName { get; set; }

        [Required(ErrorMessage = "Descriptionis required")]
        [StringLength(500, ErrorMessage = "Description should under 500")]
        public string Description { get; set; }

        [Required(ErrorMessage =)]
        public decimal Weight { get; set; }

        [Required]
        public decimal WorkPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public byte[] JewelryImage { get; set; }

        [Required]
        public double MarkupPercentage { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public StatusSale StatusSale { get; set; }

        [Required]
        public CategoryEnum Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; }

        public virtual ICollection<Warranty> Warranties { get; set; }
    }
}
