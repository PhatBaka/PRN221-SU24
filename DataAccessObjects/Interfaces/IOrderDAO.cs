using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Interfaces
{
    public interface IOrderDAO : IGenericDAO<Order>
    {
        public Order GetOrderById(int id);
    }
}
