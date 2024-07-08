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
    public class OrderDAO : GenericDAO<Order>, IOrderDAO
    {
        private readonly AppDBContext _context;

        public OrderDAO(AppDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
