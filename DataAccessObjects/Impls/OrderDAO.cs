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

        public Order GetOrderById(int id)
        {
            try
            {
                return _context.Orders.Include(x => x.OrderDetails).FirstOrDefault(x => x.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
