﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Table("Account")]
    public class Account
    {
        public Account()
        {
            Orders = new HashSet<Order>();
            Promotions = new HashSet<Promotion>();
            Counters = new HashSet<Counter>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [StringLength(10)]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string? FullName { get; set; }

        [Required]
        public string? Role { get; set; }

        [Required]
        public string? ObjectStatus { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }

        public virtual ICollection<Counter> Counters { get; set; }
    }
}
