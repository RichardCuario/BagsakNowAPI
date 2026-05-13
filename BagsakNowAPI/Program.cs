using Microsoft.EntityFrameworkCore;
using BagsakNowAPI.Models;
using Microsoft.OpenApi.Models; // Essential for Swagger

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllers();

// 2. Register Swagger services (Fixes the CS1061 build errors)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Restore your Database Context (Crucial for your API to actually work)
builder.Services.AddDbContext<BagsakContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 4. Configure the HTTP request pipeline.
// This ensures Swagger is available on Render (Production)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BagsakNowAPI v1");
    c.RoutePrefix = string.Empty; // Makes Swagger the landing page (Fixes 404)
});

// 5. Port configuration for Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.UseHttpsRedirection();

// 6. Custom Root Route (Optional: confirms the app is running)
app.MapGet("/", () => "BagsakNowAPI is running 🚀 Members are: Richard Cuario, Jhoyet Laygo, Bryan Buella, Chrisjerico Lucañas, Mikail Catli and John Carl Consorte");

app.MapControllers();

app.Run();
