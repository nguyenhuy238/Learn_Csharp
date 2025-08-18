using Microsoft.EntityFrameworkCore;
using RequestLifecycleDemo.Infrastructure.Persistence;
using RequestLifecycleDemo.Models;

namespace RequestLifecycleDemo.Repos;

public class EfProductRepository : IProductRepository
{
    private readonly AppDbContext _db;
    public EfProductRepository(AppDbContext db) => _db = db;

    public Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
        => _db.Products.FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)
        => await _db.Products.AsNoTracking().ToListAsync(ct);

    public async Task UpdateAsync(Product p, CancellationToken ct = default)
    {
        _db.Products.Update(p);
        await _db.SaveChangesAsync(ct);
    }
}
