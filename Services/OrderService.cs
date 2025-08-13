//namespace Demo_Request.Services
//{
//    public class OrderService
//    {
//    }
//}


using RequestLifecycleDemo.Repos;

namespace RequestLifecycleDemo.Services;

public class OrderService : IOrderService
{
    private readonly IProductRepository _products;
    public OrderService(IProductRepository products) => _products = products;

    public async Task<int> CreateAsync(int userId, int productId, int qty, CancellationToken ct = default)
    {
        if (qty <= 0) throw new DomainValidationException("Quantity must be > 0");
        var p = await _products.GetByIdAsync(productId, ct) ?? throw new DomainNotFoundException("Product not found");
        if (p.Stock < qty) throw new OutOfStockException();

        // giả lập trừ tồn kho và tạo order
        p.Stock -= qty;
        await _products.UpdateAsync(p, ct);

        // trả về orderId ngẫu nhiên (demo)
        return Random.Shared.Next(1000, 9999);
    }
}
