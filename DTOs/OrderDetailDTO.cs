﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class OrderDetailDTO
    {
        public DateTime? OrderDate { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? DiscountPrice { get; set; }

        public decimal? FinalPrice { get; set; }

        public string? Status { get; set; }

        public string? OrderType { get; set; }

        public Guid AccountId { get; set; }

        public Guid CustomerId { get; set; }
    }

    public class GetOrderDetailDTO : OrderDetailDTO
    {
        public GetOrderDetailDTO()
        {
            OrderDetails = new HashSet<GetOrderDetailDTO>();
        }

        public Guid OrderDetailId { get; set; }

        public virtual GetAccountDTO? Account { get; set; }

        public virtual GetCustomerDTO? Customer { get; set; }

        public virtual ICollection<GetOrderDetailDTO> OrderDetails { get; set; }
    }
}