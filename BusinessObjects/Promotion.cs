using BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Table("Promotion")]
    public class Promotion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionId { get; set; }

        [Required]
        [StringLength(50)]
        public string PromotionName { get; set; }

        [Required]
        [StringLength(50)]
        public string PromotionCode { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal DiscountValue { get; set; }

        [Required]
        public decimal AcceptedPrice { get; set; }

        [Required]
        public DiscountEnum DiscountStatus { get; set; }

        public virtual ICollection<Jewelry> Jewelries { get; set; }
     }
}
