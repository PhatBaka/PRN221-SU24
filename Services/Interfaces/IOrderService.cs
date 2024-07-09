using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using DTOs;
using DTOs.Carts;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        public Task<GetOrderDTO> CreateOrder(IList<JewelryCart> jewelryCarts, string PhoneNumber);
    }
}
