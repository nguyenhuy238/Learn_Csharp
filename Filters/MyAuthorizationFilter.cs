using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RequestLifecycleDemo.Filters;

public class MyAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Console.WriteLine("- Authorization Filter: Checking token...");

        // Lấy header Authorization
        var header = context.HttpContext.Request.Headers["Authorization"].ToString();

        // YÊU CẦU: phải có header "Authorization: Bearer 123"
        // (đơn giản để demo; prod dùng JWT)
        var ok = !string.IsNullOrEmpty(header) &&
                 header.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) &&
                 header.Substring("Bearer ".Length).Trim() == "123";

        if (!ok)
        {
            context.Result = new UnauthorizedResult(); // 401, short-circuit, không vào Action
        }
    }
}
