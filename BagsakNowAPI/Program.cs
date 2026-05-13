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
app.MapGet("/status", () => "BagsakNowAPI is officially running! ('Richard Cuario', 'richardcuario6363@gmail.com', 'chad123', 'Leader', 1),
('Jhoyet Laygo', 'jhoyetlaygo@gmail.com', 'jhoyet123', 'Assitant',1),
('Bryan Buella', 'bryanbuella@gmail.com', 'bryan123', 'Member', 1),
('Chrisjerico Lucañas', 'jericochris@gmail.com', 'chris123', 'Member', 1),
('Mikail Catli', 'catlimikail@gmail.com', 'catli123', 'Member', 0);
('John Carl Consorte', 'carl1568.gmail.com', 'carl123', 'Member', 0); 🚀");

app.Run();
