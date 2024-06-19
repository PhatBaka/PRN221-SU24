using BusinessObjects;
using DataAccessObjects;
using DataAccessObjects.Impls;
using DataAccessObjects.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Repositories.Impls;
using Repositories.Interfaces;
using Services.Impls;
using Services.Interfaces;

namespace UI.AppStarts
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(AppDBContext));

            services.AddScoped<IAccountDAO, AccountDAO>();
            services.AddScoped<IJewelryDAO, JewelryDAO>();
            services.AddScoped<IMaterialDAO, MaterialDAO>();
            services.AddScoped<IOrderDAO, OrderDAO>();
            services.AddScoped<IOrderDetailDAO, OrderDetailDAO>();
            services.AddScoped<IPromotionDAO, PromotionDAO>();
            services.AddScoped<IWarrantyDAO, WarrantyDAO>();
            services.AddScoped<IWarrantyHistoryDAO, WarrantyHistoryDAO>();
            services.AddScoped<IJewelryMaterialDAO, JewelryMaterialDAO>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IJewelryRepository, JewelryRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IWarrantyRepository, WarrantyRepository>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJewelryService, JewelryService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMaterialService, MaterialService>();
        }
    }
}
