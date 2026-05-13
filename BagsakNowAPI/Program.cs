using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllers();

// 2. Simplified Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Uses default settings to avoid namespace errors

// 3. Register Database Context
// Using the full path to your context to ensure it finds it
builder.Services.AddDbContext<BagsakNowAPI.Models.BagsakContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 4. Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BagsakNowAPI v1");
    c.RoutePrefix = string.Empty; // Sets Swagger as the home page
});

// 5. Port configuration for Render environment
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.UseHttpsRedirection();
app.MapControllers();

// Health check route
app.MapGet("/status", () => "BagsakNowAPI is online 🚀");

app.Run();
