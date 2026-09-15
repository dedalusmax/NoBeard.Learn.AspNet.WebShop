using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoBeard.Learn.AspNet.WebShop.App.Data;
using NoBeard.Learn.AspNet.WebShop.App.Models;

namespace NoBeard.Learn.AspNet.WebShop.App.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.

        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.IsEssential = true;
            options.Cookie.Name = ".Algebra.WebShop.Session";
        });

        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services
            .AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthorization(option =>
        {
            option.AddPolicy("RequireAdminRole", policy => policy
                .RequireAuthenticatedUser()
                .RequireRole("Admin")
            // Add more requirements for the policy if needed
            );
        });

        services.AddControllersWithViews();

        return services;
    }
}
