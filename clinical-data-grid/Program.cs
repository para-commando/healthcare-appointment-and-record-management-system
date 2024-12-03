using clinical_data_grid.extensions;
using database.dbContext;
using Microsoft.EntityFrameworkCore;

try
{
    var builder = WebApplication.CreateBuilder(args);
    // Configure logging
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

    // Add Swagger generator with proper grouping logic
    builder.AddSwaggerGenCustExt();

    // Add Controllers
    builder.Services.AddControllers();

    // Add Database Context with Connection String
    var connectionString = builder.Configuration.GetConnectionString("postgresHealthCareDB")
        ?? throw new InvalidOperationException("Connection string 'postgresHealthCareDB' not found.");

    builder.Services.AddDbContext<postgresHealthCareDbContext>(options =>
        options.UseNpgsql(connectionString));

    // Build the application
    var app = builder.Build();

    // Map Controllers
    app.MapControllers();

    // Use Swagger Extensions
    app.UseSwaggerCustExt();

    // Start the application
    app.Run();
}
catch (Exception ex)
{
    // Log any critical startup errors
    var loggerFactory = LoggerFactory.Create(logging =>
    {
        logging.AddConsole();
        logging.AddDebug();
    });
    var logger = loggerFactory.CreateLogger<Program>();

    logger.LogCritical(ex, "An error occurred during application startup.");
    throw; // Re-throw the exception to terminate the application
}
