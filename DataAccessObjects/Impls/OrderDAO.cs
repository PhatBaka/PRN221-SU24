using BusinessObjects;
using DataAccessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Impls
{
    public class OrderDAO : BaseDAO<Order>, IOrderDAO
    {
        private readonly AppDBContext _context;

        public OrderDAO(AppDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
