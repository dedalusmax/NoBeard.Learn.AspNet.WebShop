using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoBeard.Learn.AspNet.WebShop.App.Models;

namespace NoBeard.Learn.AspNet.WebShop.App.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<ProductCategory> ProductCategories { get; set; }
    
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>()
            .HasIndex(_ => _.Name)
            .IsUnique();

        builder.Entity<Product>()
            .HasIndex(_ => _.Name)
            .IsUnique();

        builder.Entity<OrderItem>()
            .HasIndex(_ => new { _.OrderId, _.ProductId } )
            .IsUnique();

        base.OnModelCreating(builder);
    }
}
