using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.Enums;
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
        private readonly IMaterialService _materialService;
        private readonly IOrderRepository orderRepository;
        private readonly IGenericRepository<Order> _genericOrderRepository;

        public OrderService(IOrderRepository orderRepository, IGenericRepository<Order> genericOrderRepository, IMaterialService materialService)
        {
            _materialService = materialService;
            this.orderRepository = orderRepository;
            _genericOrderRepository = genericOrderRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                if (order.OrderType == OrderEnum.OLD)
                {
                    foreach (var item in orderDetails)
                    {
                        // if is metal, increase the quantity
                        if (item.Material.IsMetail)
                        {
                            Material material = _materialService.GetMaterialById((int)item.MaterialId);
                            material.StockQuantity += item.MetalWeight;
                            _materialService.UpdateMaterial(material);
                        }
                    }
                }
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
