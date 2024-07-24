using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order, ICollection<OrderDetail> orderDetails);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
    }
}
