using Microsoft.Extensions.DependencyInjection;
using Sales_EcommerceRepository.IRepository;
using Sales_EcommerceRepository.Repository;
using Sales_EcommerceService.IService;
using Sales_EcommerceService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceUtility.DI
{
    public static class AddApplicationDIService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IClaimRepository, ClaimRepository>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IExecutiveService, ExecutiveService>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<ICommonService, CommonService>();
            return services;
        }
    }
}