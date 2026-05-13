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
app.MapGet("/", () => "BagsakNowAPI is running 🚀 ('Richard Cuario', 'richardcuario6363@gmail.com', 'chad123', 'Leader', 1),
('Jhoyet Laygo', 'jhoyetlaygo@gmail.com', 'jhoyet123', 'Assitant',1),
('Bryan Buella', 'bryanbuella@gmail.com', 'bryan123', 'Member', 1),
('Chrisjerico Lucañas', 'jericochris@gmail.com', 'chris123', 'Member', 1),
('Mikail Catli', 'catlimikail@gmail.com', 'catli123', 'Member', 0);
('John Carl Consorte', 'carl1568.gmail.com', 'carl123', 'Member', 0);
-- Verify the data
");

// Controllers
app.MapControllers();

app.Run();
