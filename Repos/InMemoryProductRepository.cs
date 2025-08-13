//namespace Demo_Request.Repos
//{
//    public class InMemoryProductRepository
//    {
//    }
//}


using RequestLifecycleDemo.Models;

namespace RequestLifecycleDemo.Repos;

public class InMemoryProductRepository : IProductRepository
{
    private static readonly List<Product> _data = new()
    {
        new() { Id = 1, Name = "Keyboard", Price = 29.9m, Stock = 50 },
        new() { Id = 2, Name = "Mouse",    Price = 19.5m, Stock = 30 },
        new() { Id = 3, Name = "Monitor",  Price = 199m,  Stock = 5  },
    };

    public Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
        => Task.FromResult(_data.FirstOrDefault(x => x.Id == id));

    public Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)
        => Task.FromResult((IReadOnlyList<Product>)_data.ToList());

    public Task UpdateAsync(Product p, CancellationToken ct = default)
    {
        var i = _data.FindIndex(x => x.Id == p.Id);
        if (i >= 0) _data[i] = p;
        return Task.CompletedTask;
    }
}
