using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order, List<OrderDetail> orderDetails);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetAllOrdersAsync();
    }
}
