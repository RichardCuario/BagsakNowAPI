using Microsoft.EntityFrameworkCore;
using BagsakNowAPI.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllers();

// 2. Register Swagger services
// Note: Requires Swashbuckle.AspNetCore NuGet package
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Register Database Context
builder.Services.AddDbContext<BagsakContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 4. Configure the HTTP request pipeline.
// This enables Swagger in both Development and Production (Render)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BagsakNowAPI v1");
    c.RoutePrefix = string.Empty; // This makes Swagger the landing page
});

// 5. Port configuration for Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.UseHttpsRedirection();

// 6. Root Route confirmation
app.MapGet("/status", () => "BagsakNowAPI is running 🚀");

app.MapControllers();

app.Run();
