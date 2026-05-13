using Microsoft.EntityFrameworkCore;
using BagsakNowAPI.Models;
var builder = WebApplication.CreateBuilder(args);

// Register the Controllers
builder.Services.AddControllers();
// Register the Database Context with the SQL Server connection string
builder.Services.AddDbContext<BagsakContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
// Port configuration for hosting platforms like Render
// Comment this out when testing locally, uncomment before deploying
 var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
 app.Urls.Add($"http://+:{port}");
app.MapControllers();
app.Run();s