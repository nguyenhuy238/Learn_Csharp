using Microsoft.EntityFrameworkCore;
using RequestLifecycleDemo.Models;

namespace RequestLifecycleDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Product>(e =>
        {
            e.ToTable("Products");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(200).IsRequired();
            e.Property(x => x.Price).HasColumnType("decimal(18,2)");
            e.Property(x => x.Stock).IsRequired();
        });

        // Seed dữ liệu ban đầu (tùy chọn)
        b.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Keyboard", Price = 29.9m, Stock = 50 },
            new Product { Id = 2, Name = "Mouse", Price = 19.5m, Stock = 30 },
            new Product { Id = 3, Name = "Monitor", Price = 199m, Stock = 5 }
        );
    }
}
