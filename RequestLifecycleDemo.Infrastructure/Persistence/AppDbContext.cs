// RequestLifecycleDemo.Infrastructure/Persistence/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using RequestLifecycleDemo.Models;

namespace RequestLifecycleDemo.Infrastructure.Persistence;

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
        });
    }
}
