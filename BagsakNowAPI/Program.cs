using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BagsakNowAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllers();

// 2. Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Register Database Context
builder.Services.AddDbContext<BagsakContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 4. Configure the HTTP request pipeline.
// Always enable Swagger so it works on Render
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BagsakNowAPI v1");
    c.RoutePrefix = string.Empty; // Set Swagger as the home page
});

// 5. Port configuration for Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.UseHttpsRedirection();

// 6. Map routes
app.MapControllers();

// Root route status check
app.MapGet("/status", () => "BagsakNowAPI is running 🚀");

app.Run();
