//namespace Demo_Request.Services
//{
//    public class IProductService
//    {
//    }
//}


using RequestLifecycleDemo.Models;

namespace RequestLifecycleDemo.Services;

public interface IProductService
{
    Task<ProductDto?> GetAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken ct = default);
}
