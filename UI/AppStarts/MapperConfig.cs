using AutoMapper;
using BusinessObjects;
using BusinessObjects.FilterModels;
using Services.RequestModels.Account;
using Services.RequestModels.Category;
using Services.RequestModels.Order;
using Services.RequestModels.Promotion;
using Services.ResponseModels;
using UI.Payload.JewelryPayload;
using UI.Payload.MaterialPayload.GemPayload;
using UI.Payload.MaterialPayload.MetalPayload;
using UI.Payload.WarrantyPayload;

namespace UI.AppStarts
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            #region ACCOUNT
            CreateMap<Account, AccountResponse>().ReverseMap();
            CreateMap<Account, CreateAccountRequest>().ReverseMap();
            CreateMap<Account, UpdateAccountRequest>().ReverseMap();
            CreateMap<Account, LoginRequest>().ReverseMap();
            CreateMap<AccountResponse, CreateAccountRequest>().ReverseMap();
            CreateMap<AccountResponse, UpdateAccountRequest>().ReverseMap();
            CreateMap<AccountResponse, AccountFilter>().ReverseMap();
            #endregion

            #region CATEGORY
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Category, CreateCategoryRequest>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequest>().ReverseMap();
            CreateMap<CategoryResponse, CreateCategoryRequest>().ReverseMap();
            CreateMap<CategoryResponse, UpdateCategoryRequest>().ReverseMap();
            CreateMap<CategoryResponse, CategoryFilter>().ReverseMap();
            #endregion

            #region ORDER
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<Order, CreateOrderRequest>().ReverseMap();
            CreateMap<Order, UpdateOrderRequest>().ReverseMap();
            CreateMap<OrderResponse, CreateOrderRequest>().ReverseMap();
            CreateMap<OrderResponse, UpdateOrderRequest>().ReverseMap();
            CreateMap<OrderResponse, OrderFilter>().ReverseMap();
            #endregion

            #region JEWELRY
            CreateMap<Jewelry, JewelryResponse>().ReverseMap();
            CreateMap<Jewelry, CreateJewelryRequest>().ReverseMap();
            CreateMap<JewelryResponse, CreateJewelryRequest>().ReverseMap();
            CreateMap<JewelryResponse, JewelryFilter>().ReverseMap();
            #endregion

            #region PROMOTION
            CreateMap<Promotion, PromotionResponse>().ReverseMap();
            CreateMap<Promotion, CreatePromotionRequest>().ReverseMap();
            CreateMap<Promotion, UpdatePromotionRequest>().ReverseMap();
            CreateMap<PromotionResponse, CreatePromotionRequest>().ReverseMap();
            CreateMap<PromotionResponse, UpdatePromotionRequest>().ReverseMap();
            CreateMap<PromotionResponse, PromotionFilter>().ReverseMap();
            #endregion

            #region MATERIAL
            CreateMap<Material, CreateGemRequest>().ReverseMap();
            CreateMap<Material, CreateMetalRequest>().ReverseMap();
        
			#endregion

			#region WARRANTY
            CreateMap<Warranty, WarrantyCreateRequest>().ReverseMap();
            CreateMap<WarrantyHistory, WarrantyFixRequest>().ReverseMap();
			#endregion

		}
	}
}
