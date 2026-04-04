using Application_ECommerce.App.Cart.Interfaces;
using Application_ECommerce.App.Cart.Mapping;
using Application_ECommerce.App.Cart.Services;
using Application_ECommerce.App.Categories.Interfaces;
using Application_ECommerce.App.Categories.Mapping;
using Application_ECommerce.App.Categories.Services;
using Application_ECommerce.App.Coupons.Interfaces;
using Application_ECommerce.App.Coupons.Mapping;
using Application_ECommerce.App.Coupons.Services;
using Application_ECommerce.App.Orders.Interfaces;
using Application_ECommerce.App.Orders.Mapping;
using Application_ECommerce.App.Orders.Services;
using Application_ECommerce.App.Products.Interfaces;
using Application_ECommerce.App.Products.Mapping;
using Application_ECommerce.App.Products.Services;
using Application_ECommerce.App.Athentification.Interfaces;
using Application_ECommerce.App.Athentification.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Extension
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            //Register Services
            services.AddScoped<ICategoryService, CategoryServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ICouponService, CouponServices>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IAuthService, AuthService>();


            //Register AutoMapper Profiles
            services.AddAutoMapper(typeof(CategoryProfile));
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(CouponProfile));
            services.AddAutoMapper(typeof(CartMappingProfile));
            services.AddAutoMapper(typeof(OrderMappingProfile));

            return services;
        }
    }
}
