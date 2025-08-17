//namespace Demo_Request.Services
//{
//    public class IOrderService
//    {
//    }
//}


namespace RequestLifecycleDemo.Services;

public interface IOrderService
{
    Task<int> CreateAsync(int userId, int productId, int qty, CancellationToken ct = default);
}
