using Application_ECommerce.Core.Interfaces.Repositories.Base;
using Application_ECommerce.Core.Interfaces.Repositories;
using Application_ECommerce.Core.Interfaces;
using Application_ECommerce.Infrastructure.External;
using Application_ECommerce.Infrastructure.Persistence.Repositories.Base;
using Application_ECommerce.Infrastructure.Persistence.Repositories;
using Application_ECommerce.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_ECommerce.Core.Interfaces.External;

namespace Application_ECommerce.Infrastructure.Extension
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            //Register EmailSender 
            services.AddScoped<IEmailSender, EmailSender>();

            //Register Repository
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IFileHelper, FileHelper>();

            return services;
        }
    }
}