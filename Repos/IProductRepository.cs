//namespace Demo_Request.Repos
//{
//    public class IProductRepository
//    {
//    }
//}


using RequestLifecycleDemo.Models;

namespace RequestLifecycleDemo.Repos;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default);
    Task UpdateAsync(Product p, CancellationToken ct = default);
}
