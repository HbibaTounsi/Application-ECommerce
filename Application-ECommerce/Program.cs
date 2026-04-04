using Application_ECommerce.Core.Entities.Identity;
using Application_ECommerce.Core.Interfaces;
using Application_ECommerce.Core.Interfaces.Repositories.Base;
using Application_ECommerce.Core.Interfaces.Repositories;
using Application_ECommerce.Infrastructure.Persistence;
using Application_ECommerce.Infrastructure.Persistence.Repositories.Base;
using Application_ECommerce.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application_ECommerce.App.Categories.Mapping;
using Application_ECommerce.App.Categories.Interfaces;
using Application_ECommerce.App.Categories.Services;
using Application_ECommerce.App.Products.Interfaces;
using Application_ECommerce.App.Products.Services;
using Application_ECommerce.App.Cart.Interfaces;
using Application_ECommerce.App.Cart.Services;
using Application_ECommerce.App.Coupons.Interfaces;
using Application_ECommerce.App.Coupons.Services;
using Application_ECommerce.App.Orders.Interfaces;
using Application_ECommerce.App.Orders.Services;
using Application_ECommerce.App.Athentification.Interfaces;
using Application_ECommerce.App.Athentification.Services;
using Application_ECommerce.Core.Interfaces.External;
using Application_ECommerce.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddRazorPages();

//Dependancy Injection 


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ICategoryService, CategoryServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICouponService, CouponServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IFileHelper, FileHelper>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();


builder.Services.AddAutoMapper(typeof(CategoryProfile), typeof(Application_ECommerce.Mapping.Category.CategoryMappingProfile));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
