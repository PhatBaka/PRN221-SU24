using BusinessObjects;
using DataAccessObjects;
using DataAccessObjects.Interfaces;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impls
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly IOrderDAO _orderDAO;

        public OrderRepository(IGenericDAO<Order> dao, IOrderDAO orderDAO) : base(dao)
        {
            _orderDAO = orderDAO;
        }

        public Order GetOrderById(int id)
        {
            try
            {
                return _orderDAO.GetByIdAsync(id).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
