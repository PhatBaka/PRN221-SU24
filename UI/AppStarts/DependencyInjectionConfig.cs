using BusinessObjects;
using DataAccessObjects;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Repositories;
using Services.Impls;
using Services.Interfaces;

namespace UI.AppStarts
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(AppDBContext));
            services.AddScoped(typeof(GenericDAO<>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MapperConfig));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJewelryService, JewelryService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
