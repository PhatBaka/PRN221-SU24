using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("WarrantyOrder")]
    public class WarrantyOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WarrantyOrderId { get; set; }

        public double WarrantyPeriod { get; set; }

        [Required]
        public int JewelryId { get; set; }
        public virtual Jewelry? Jewelry { get; set; }

        [Required]
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }

        public int WarrantyRequestId { get; set; }
        public virtual WarrantyRequest? WarrantyRequest { get; set; }

        public int OrderDetailId { get; set; }
        public virtual OrderDetail? OrderDetail { get; set; }
    }
}
