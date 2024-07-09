using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects;
using DataAccessObjects;
using DataAccessObjects.Interfaces;
using DTOs;
using DTOs.Carts;
using DTOs.Enums;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Impls
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository,
                                ICustomerRepository customerRepository,
                                IOrderDetailRepository orderDetailRepository,
                                IMapper mapper)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<GetOrderDTO> CreateOrder(IList<JewelryCart> jewelryCarts, string PhoneNumber)
        {
            try
            {
                Order entity = new Order();

                Customer customer = _customerRepository.GetFirstOrDefaultAsync(x => x.PhoneNumber.Equals(PhoneNumber)).Result;
                entity.CustomerId = customer.CustomerId;
                entity.AccountId = Guid.Parse("bd61f5e0-f667-4f2d-6415-08dca027cc20");
                entity.OrderDate = DateTime.Now;
                entity.OrderType = OrderEnum.NEW.ToString();

                var newOrder = await _orderRepository.AddAsync(entity);
                
                foreach (var jewelry in jewelryCarts)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderId = newOrder.OrderId,
                        JewelryId = jewelry.GetJewelryDTO.JewelryId,
                        UnitPrice = jewelry.GetJewelryDTO.SellJewelryPrice,
                        FinalPrice = jewelry.GetJewelryDTO.SellJewelryPrice
                    };
                    await _orderDetailRepository.AddAsync(orderDetail);
                }

                return _mapper.Map<GetOrderDTO>(newOrder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}