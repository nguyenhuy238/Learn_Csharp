//namespace Demo_Request.Controllers
//{
//    public class ProductsController
//    {
//    }
//}


using Microsoft.AspNetCore.Mvc;
using RequestLifecycleDemo.Models;
using RequestLifecycleDemo.Services;

namespace RequestLifecycleDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _svc;
    public ProductsController(IProductService svc) => _svc = svc;

    [HttpGet]
    [ResponseCache(NoStore = true)] // demo: tắt cache HTTP chuẩn, dùng output cache layer
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll(CancellationToken ct)
        => Ok(await _svc.GetAllAsync(ct));

    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> Get(int id, CancellationToken ct)
    {
        var p = await _svc.GetAsync(id, ct);
        return p is null ? NotFound() : Ok(p);
    }
}
