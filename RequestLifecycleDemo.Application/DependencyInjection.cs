// RequestLifecycleDemo.Application/DependencyInjection.cs
using Microsoft.Extensions.DependencyInjection;
using RequestLifecycleDemo.Services;

namespace RequestLifecycleDemo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Đăng ký các service nghiệp vụ (use-cases)
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();

        // Tuỳ chọn: các tiện ích thuần Application
        // services.AddMediatR(typeof(DependencyInjection).Assembly);
        // services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        // services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
