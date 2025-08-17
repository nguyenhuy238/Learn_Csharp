// RequestLifecycleDemo.Infrastructure/DependencyInjection.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestLifecycleDemo.Infrastructure.Persistence;
using RequestLifecycleDemo.Repos;

namespace RequestLifecycleDemo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("Default")));

        services.AddScoped<IProductRepository, EfProductRepository>();
        return services;
    }
}
