using Microsoft.AspNetCore.Mvc;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddDbContext<Inventory_Management_Service.InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Global error handling for ProblemDetails
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/problem+json";
        var problem = new ProblemDetails
        {
            Status = 500,
            Title = "An unexpected error occurred!",
            Detail = "An unhandled exception occurred while processing the request."
        };
        await context.Response.WriteAsJsonAsync(problem);
    });
});

// Return validation errors in ProblemDetails format
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 400 && context.Items.ContainsKey("__ValidationProblemDetails"))
    {
        var problem = context.Items["__ValidationProblemDetails"] as ValidationProblemDetails;
        if (problem != null)
        {
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
});


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory Management Service API v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();

app.MapControllers();

app.Run();
