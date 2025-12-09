
using AnyWareTask.Api.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AnyWareTask.Api.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException exception)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { exception.Message }));
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { exception.Message }));
        }
    }
}
