using AnyWareTask.Api.Extensions;
using AnyWareTask.Api.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.AddWebApi();
var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
