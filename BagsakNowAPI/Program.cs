var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Render port setup
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

// ALWAYS enable Swagger (not only dev)
app.UseSwagger();
app.UseSwaggerUI();

// Optional: HTTPS redirect (can keep, but Render already handles HTTPS)
app.UseHttpsRedirection();

// ⭐ ADD ROOT ROUTE (THIS FIXES YOUR 404)
app.MapGet("/", () => "BagsakNowAPI is running 🚀 Members are : Richard Cuario, Jhoyet Laygo, Bryan Buella, Chrisjerico Lucañas, Mikail Catli and John Carl Consorte");

// Controllers
app.MapControllers();

app.Run();
