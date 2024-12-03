using clinical_data_grid.extensions;
using database.dbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Register Swagger generator with proper grouping logic
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
var connectionString =
    builder.Configuration.GetConnectionString("postgresHealthCareDB")
        ?? throw new InvalidOperationException("Connection string"
        + "'postgresHealthCareDB' not found.");
builder.Services.AddDbContext<postgresHealthCareDbContext>(options =>
    options.UseNpgsql(connectionString));
// configuring logger services
builder.Services.AddLogging(configure =>
{
    // Clears all existing logging providers
    configure.ClearProviders();

    // Adds the Console logging provider
    configure.AddConsole();

    // Adds the Debug logging provider
    configure.AddDebug();

    // Sets the minimum log level to Debug
    configure.SetMinimumLevel(LogLevel.Debug);
});
builder.AddSwaggerGenCustExt();
var app = builder.Build();
app.MapControllers();
app.UseSwaggerCustExt();


app.Run();
