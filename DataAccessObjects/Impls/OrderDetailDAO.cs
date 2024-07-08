using BusinessObjects;
using DataAccessObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Impls
{
    public class OrderDetailDAO : GenericDAO<OrderDetail>, IOrderDetailDAO
    {
        private readonly AppDBContext _context;

        public OrderDetailDAO(AppDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
