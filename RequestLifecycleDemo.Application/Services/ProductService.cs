//namespace Demo_Request.Services
//{
//    public class ProductService
//    {
//    }
//}


using RequestLifecycleDemo.Models;
using RequestLifecycleDemo.Repos;

namespace RequestLifecycleDemo.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    public ProductService(IProductRepository repo) => _repo = repo;

    public async Task<ProductDto?> GetAsync(int id, CancellationToken ct = default)
    {
        var p = await _repo.GetByIdAsync(id, ct);
        return p is null ? null : new ProductDto(p.Id, p.Name, p.Price);
    }

    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken ct = default)
    {
        var all = await _repo.GetAllAsync(ct);
        return all.Select(p => new ProductDto(p.Id, p.Name, p.Price)).ToList();
    }
}
