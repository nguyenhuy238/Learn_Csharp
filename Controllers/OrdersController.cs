//namespace Demo_Request.Controllers
//{
//    public class OrdersController
//    {
//    }
//}


using Microsoft.AspNetCore.Mvc;
using RequestLifecycleDemo.Models;
using RequestLifecycleDemo.Services;

namespace RequestLifecycleDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orders;
    public OrdersController(IOrderService orders) => _orders = orders;

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto, CancellationToken ct)
    {
        // Thực tế: lấy userId từ JWT claims. Demo: mock cứng.
        int userId = 123;
        var orderId = await _orders.CreateAsync(userId, dto.ProductId, dto.Quantity, ct);

        return CreatedAtAction(nameof(Status), new { id = orderId }, new { orderId });
    }

    [HttpGet("{id:int}/status")]
    public IActionResult Status(int id) => Ok(new { id, status = "Processing" });
}
