using Application_ECommerce.Core.Entities.Identity;
using Application_ECommerce.Core.Not_Mapped_Entities;
using Application_ECommerce.Infrastructure.Persistence;
using Application_ECommerce.Infrastructure.Extension;
using Application_ECommerce.App.Extension;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application_ECommerce.Mapping.Auth;
using Application_ECommerce.Mapping.Category;
using Application_ECommerce.Mapping.Coupon;
using Application_ECommerce.Mapping.Home;
using Application_ECommerce.Mapping.Product;
using Application_ECommerce.App.Cart.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// Configuration des options d'email
builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSettings"));

// Dependency Injection — Infrastructure (repositories, EmailSender, FileHelper...)
builder.Services.AddInfrastructureRegistration(builder.Configuration);

// Dependency Injection — Application (services métier + AutoMapper profiles)
builder.Services.AddApplicationRegistration();

builder.Services.AddAutoMapper(typeof(CategoryMappingProfile));
builder.Services.AddAutoMapper(typeof(AuthMappingProfile));
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));
builder.Services.AddAutoMapper(typeof(CouponMappingProfile));
builder.Services.AddAutoMapper(typeof(CartMappingProfile));
builder.Services.AddAutoMapper(typeof(HomeMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

public partial class Program { }
