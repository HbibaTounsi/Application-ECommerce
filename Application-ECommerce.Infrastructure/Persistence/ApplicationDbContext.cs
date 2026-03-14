using Application_ECommerce.Core.Entities.Cart;
using Application_ECommerce.Core.Entities.Category;
using Application_ECommerce.Core.Entities.Coupon;
using Application_ECommerce.Core.Entities.Identity;
using Application_ECommerce.Core.Entities.Orders;
using Application_ECommerce.Core.Entities.Product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeader { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
