using AnyWareTask.Api.Data;
using AnyWareTask.Api.Interfaces;
using AnyWareTask.Api.Middlewares;
using AnyWareTask.Api.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;

namespace AnyWareTask.Api.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddWebApi(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration looger) =>
            {
                looger.ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services);
            });
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost"));
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddTransient<ErrorHandlingMiddleware>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            builder.Services.AddFluentValidationAutoValidation();

        }
    }
}
