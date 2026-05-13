using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi(); 
builder.Services.AddDbContext<BagsakNowAPI.Models.BagsakContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// This route will display the data in a nice web table
app.MapGet("/", async (BagsakNowAPI.Models.BagsakContext db) => {
    var members = await db.Members.ToListAsync();
    
    var html = @"
    <!DOCTYPE html>
    <html>
    <head>
        <title>BagsakNow Team</title>
        <style>
            body { font-family: sans-serif; background-color: #121212; color: white; padding: 40px; }
            table { width: 100%; border-collapse: collapse; margin-top: 20px; background: #1e1e1e; }
            th, td { padding: 12px; border: 1px solid #333; text-align: left; }
            th { background-color: #333; color: #00ff99; }
            tr:hover { background-color: #2a2a2a; }
            .status-active { color: #00ff99; }
            .status-inactive { color: #ff4444; }
        </style>
    </head>
    <body>
        <h1>BagsakNow Team Members 🚀</h1>
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Full Name</th>
                    <th>Email</th>
                    <th>Role</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>";

    foreach(var m in members) {
        var status = m.IsActive == 1 ? "<span class='status-active'>Active</span>" : "<span class='status-inactive'>Inactive</span>";
        html += $@"
                <tr>
                    <td>{m.Id}</td>
                    <td>{m.FullName}</td>
                    <td>{m.Email}</td>
                    <td>{m.Role}</td>
                    <td>{status}</td>
                </tr>";
    }

    html += @"
            </tbody>
        </table>
    </body>
    </html>";

    return Results.Content(html, "text/html");
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
