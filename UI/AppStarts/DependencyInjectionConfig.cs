using BusinessObjects;
using DataAccessObjects;
using DataAccessObjects.Impls;
using DataAccessObjects.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Repositories;
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
            services.AddDbContext<AppDBContext>();
            //services.AddScoped(typeof(GenericDAO<>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MapperConfig));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJewelryService, JewelryService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMaterialService, MaterialService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountDAO, AccountDAO>();
            services.AddScoped(typeof(IGenericDAO<>), typeof(GenericDAO<>));
		}
	}
}
