using Microsoft.EntityFrameworkCore;
// We removed the conflicting 'using' lines here

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllers();

// 2. Register Swagger services (Fixed using full path to avoid CS0234)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BagsakNowAPI", Version = "v1" });
});

// 3. Register Database Context (Ensure this namespace matches your folder structure)
builder.Services.AddDbContext<BagsakNowAPI.Models.BagsakContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 4. Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BagsakNowAPI v1");
    c.RoutePrefix = string.Empty; 
});

// 5. Port configuration for Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.UseHttpsRedirection();

app.MapControllers();

// Confirming the app is running
app.MapGet("/status", () => "BagsakNowAPI is running successfully! 🚀");

app.Run();
