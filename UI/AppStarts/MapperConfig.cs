using AutoMapper;
using BusinessObjects;
using DTOs;

namespace UI.AppStarts
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            #region ACCOUNT
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, GetAccountDTO>().ReverseMap();
            #endregion

            #region CUSTOMER
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, GetCustomerDTO>().ReverseMap();
            #endregion

            #region JEWELRY
            CreateMap<Jewelry, JewelryDTO>().ReverseMap();
            CreateMap<Jewelry, GetJewelryDTO>().ReverseMap();
            #endregion

            #region GEM
            CreateMap<Material, GemDTO>().ReverseMap();
            CreateMap<Material, GetGemDTO>().ReverseMap();
            #endregion

            #region
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, GetOrderDTO>().ReverseMap();
            #endregion

            #region
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<OrderDetail, GetOrderDetailDTO>().ReverseMap();
            #endregion
        }
    }
}
