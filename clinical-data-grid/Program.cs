using database.dbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<postgresHealthCareDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgresHealthCareDB")));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
