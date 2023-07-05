
using Microsoft.EntityFrameworkCore;
using ShopCarrs.Models;
using ShopCarrs.Repository;
using Microsoft.AspNetCore.Identity;
using ShopCarrs.Data;
using ShopCarrs.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//
builder.Services.AddDbContext<CarShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarShopDB"));

});
builder.Services.AddDbContext<ShopCarrsDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopCarrsDB"));
});


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ShopCarrsDBContext>();


builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

});

//DI
builder.Services.AddTransient<IProductRepository, ProductReposistory>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();

builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();;
app.UseAuthorization();

app.MapAreaControllerRoute(
     name: "MyAreas",
     areaName: "Admin",
     pattern: "Admin/{action=Index}/{id?}",
      defaults: new { controller = "Admin", action = "Index" }
    );
app.MapAreaControllerRoute(
     name: "MyAreas",
     areaName: "Admin",
     pattern: "Admin/{action=Index}/{id?}",
      defaults: new { controller = "User", action = "Index" }
    );
app.MapAreaControllerRoute(
     name: "MyAreas",
     areaName: "Admin",
     pattern: "Admin/{action=Index}/{id?}",
      defaults: new { controller = "Category", action = "Index" }
    );
app.MapAreaControllerRoute(
     name: "MyAreas",
     areaName: "Admin",
     pattern: "Admin/{action=Index}/{id?}",
      defaults: new { controller = "Brand", action = "Index" }
    );
app.MapAreaControllerRoute(
     name: "MyAreas",
     areaName: "Admin",
     pattern: "Admin/{action=Index}/{id?}",
      defaults: new { controller = "Order", action = "Index" }
    );
app.MapAreaControllerRoute(
     name: "MyAreas",
     areaName: "Admin",
     pattern: "Admin/{action=Index}/{id?}",
      defaults: new { controller = "OrderDetail", action = "Index" }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//
app.MapRazorPages();
//
app.Run();
