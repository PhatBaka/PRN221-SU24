using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly IWarrantyRepository _warrantyRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository,
                                ICustomerRepository customerRepository,
                                IOrderDetailRepository orderDetailRepository,
                                IWarrantyRepository warrantyRepository,
                                IMapper mapper)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderDetailRepository = orderDetailRepository;
            _warrantyRepository = warrantyRepository;
            _mapper = mapper;
        }

        public async Task<GetOrderDTO> CreateOrder(IList<JewelryCart> jewelryCarts, string PhoneNumber)
        {
            try
            {
                Order entity = new Order();
                
                entity.CustomerId = Guid.Parse("BD61F5E0-F667-4F2D-6415-08DCA027CC20");
                entity.AccountId = Guid.Parse("bd61f5e0-f667-4f2d-6415-08dca027cc20");
                entity.OrderDate = DateTime.Now;
                entity.OrderType = OrderEnum.NEW.ToString();
                entity.TotalPrice = jewelryCarts.Sum(x => x.GetJewelryDTO.SellJewelryPrice);
                var newOrder = await _orderRepository.AddAsync(entity);
                
                // create order detail
                foreach (var jewelry in jewelryCarts)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderId = newOrder.OrderId,
                        JewelryId = jewelry.GetJewelryDTO.JewelryId,
                        UnitPrice = jewelry.GetJewelryDTO.SellJewelryPrice,
                        FinalPrice = jewelry.GetJewelryDTO.SellJewelryPrice
                    };
                    var newOrderDetail = await _orderDetailRepository.AddAsync(orderDetail);
                    // create warranty base on category
                    Warranty warranty = new Warranty()
                    {
                        OrderDetailId = newOrderDetail.OrderDetailId
                    };
                    // EARRING = 12 months
                    if (jewelry.GetJewelryDTO.JewelryCategory.Equals(JewelryCategoryEnum.EARRINGS.ToString()))
                    {
                        warranty.FromDate = DateTime.Now;
                        warranty.EndDate = DateTime.Now.AddMonths(12);
                    } 
                    // BRACELETS = 14 months
                    else if (jewelry.GetJewelryDTO.JewelryCategory.Equals(JewelryCategoryEnum.BRACELETS.ToString()))
                    {
                        warranty.FromDate = DateTime.Now;
                        warranty.EndDate = DateTime.Now.AddMonths(14);
                    }
                    await _warrantyRepository.AddAsync(warranty);
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