using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Interfaces;

namespace Services.Impls
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;

        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                await _orderRepository.InsertAsync(order);
                foreach (var detail in orderDetails)
                {
                    detail.OrderId = order.OrderId;
                    await _orderDetailRepository.InsertAsync(detail);
                }
                await _orderRepository.SaveChagesAysnc();
                await _orderDetailRepository.SaveChagesAysnc();
                return order;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return (await _orderRepository.GetAllAsync()).ToList();
        }
    }
}
