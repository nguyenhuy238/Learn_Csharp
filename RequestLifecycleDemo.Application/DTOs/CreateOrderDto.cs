//namespace Demo_Request.Models
//{
//    public class CreateOrderDto
//    {
//    }
//}


using System.ComponentModel.DataAnnotations;

namespace RequestLifecycleDemo.Models;

public class CreateOrderDto
{
    [Required] public int ProductId { get; set; }
    [Range(1, 1000)] public int Quantity { get; set; }
}
