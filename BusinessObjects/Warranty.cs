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
    public class Warranty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WarrantyId { get; set; }

        public double WarrantyPeriod { get; set; }

		public TimeEnum PeriodUnitmeasure { get; set; }

		public DateTime ActiveDate { get; set; }

		public DateTime EndDate { get; set; }

		public int JewelryId { get; set; }

        public int OrderId { get; set; }

        public WarrantyStatus WarrantyStatus { get; set; }

        public virtual Order Order { get; set; }

        public virtual Jewelry Jewelry { get; set; }

        public virtual ICollection<WarrantyHistory> WarrantyHistories { get; set; }

    }
}
