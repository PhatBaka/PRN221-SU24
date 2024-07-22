using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Impls;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Impls
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IGenericRepository<Order> _genericOrderRepository;

        public OrderService(IOrderRepository orderRepository, IGenericRepository<Order> genericOrderRepository)
        {
            this.orderRepository = orderRepository;
            _genericOrderRepository = genericOrderRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                order.OrderDetails = orderDetails;
                await _genericOrderRepository.InsertAsync(order);
                return order;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return orderRepository.GetOrderById(orderId);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return (await _genericOrderRepository.GetAllAsync()).ToList();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _genericOrderRepository.GetWhereAsync(o => o.CustomerId == userId);
        }
    }
}
