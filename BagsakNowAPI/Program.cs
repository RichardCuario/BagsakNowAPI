using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllers();

// 2. New .NET 10 way to add OpenAPI
builder.Services.AddOpenApi(); 

// 3. Register Database Context
builder.Services.AddDbContext<BagsakNowAPI.Models.BagsakContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 4. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true) // Force enabled for Render testing
{
    app.MapOpenApi(); // Serves the JSON file at /openapi/v1.json
}

// 5. Port configuration for Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", () => Results.Redirect("/status"));
app.MapGet("/status", () => "BagsakNowAPI is officially running! 🚀");

app.Run();
