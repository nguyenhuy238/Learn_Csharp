
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RequestLifecycleDemo.Filters;
using RequestLifecycleDemo.Middlewares;
using RequestLifecycleDemo.Repos;
using RequestLifecycleDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>(); // map exception -> ProblemDetails/status code
    options.Filters.Add<LogActionFilter>();    // log thời gian chạy action
    options.Filters.Add<MyAuthorizationFilter>();
})
.ConfigureApiBehaviorOptions(o =>
{
    // Giữ nguyên mặc định [ApiController] => tự trả 400 khi ModelState invalid
});

// Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RequestLifecycleDemo", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Description = "Nhập token dạng: Bearer {token} (VD: Bearer 123)"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});
// Business & Repos (DI)
builder.Services.AddScoped<IProductRepository, InMemoryProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Tối ưu & quan sát
builder.Services.AddOutputCache();     // cache output demo
builder.Services.AddHealthChecks();    // /health

var app = builder.Build();

// ===== PIPELINE (thứ tự quan trọng) =====
app.UseMiddleware<ExceptionHandlingMiddleware>(); // 1) bắt lỗi toàn cục, ProblemDetails
app.UseMiddleware<RequestLoggingMiddleware>();    // 2) log method/path/status/duration

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthentication();
// app.UseAuthorization();

app.UseOutputCache(); // cache ở phía response
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();



//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
