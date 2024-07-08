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

            services.AddAutoMapper(typeof(MapperConfig));

            services.AddSingleton(typeof(IGenericDAO<>), typeof(GenericDAO<>));
            services.AddScoped<IAccountDAO, AccountDAO>();
            services.AddScoped<ICustomerDAO, CustomerDAO>();
            services.AddScoped<IOrderDAO, OrderDAO>();
            services.AddScoped<IOrderDetailDAO, OrderDetailDAO>();
            services.AddScoped<IJewelryDAO, JewelryDAO>();
            services.AddScoped<IMaterialDAO, MaterialDAO>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IJewelryRepository, JewelryRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJewelryService, JewelryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
