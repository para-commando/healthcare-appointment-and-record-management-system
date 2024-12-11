using System.Text;
using clinical_data_grid.apis.extensions;
using clinical_data_grid.apis.services;
using clinical_data_grid.database;
using clinical_data_grid.database.extensions;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5001);
            });

            // making the logger object unique to every http request
            builder.Services.AddScoped(typeof(CustomLogger<>));
            // Configure logging
            builder.Services.AddLogging(configure =>
            {
                configure.ClearProviders(); // Clears all existing logging providers
                configure.AddConsole();    // Adds the Console logging provider
                configure.AddDebug();      // Adds the Debug logging provider
                configure.SetMinimumLevel(LogLevel.Debug); // Sets the minimum log level to Debug
            });

            // Add Swagger generator with proper grouping logic
            builder.AddSwaggerGenCustExt();

            // Add Controllers
            builder.Services.AddControllers();

            // Add Database Context with Connection String
            var dbConnectionString = builder.Configuration.GetConnectionString("postgresHealthCareDB")
                ?? throw new InvalidOperationException("Connection string 'postgresHealthCareDB' not found.");

            builder.Services.AddDbContext<postgresHealthCareDbContext>(options =>
                options.UseNpgsql(dbConnectionString));



            // redis cache config
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
   {
       var redisConfig = new ConfigurationOptions
       {
           EndPoints =
           {
            $"{builder.Configuration["ConnectionStrings:redis:host"]}:{builder.Configuration["ConnectionStrings:redis:port"]}"
           },
           User = builder.Configuration["ConnectionStrings:redis:username"],
           Password = builder.Configuration["ConnectionStrings:redis:password"],
       };

       return ConnectionMultiplexer.Connect(redisConfig);
   });
            builder.Services.AddTransient<AuthService>();

            // jwt authentication custom extension
            builder.ConfigureJwtAuthenticationCustExt();

            // jwt authorization custom extension
            builder.ConfigureJwtAuthorizationCustExt();

            // Build the application
            var app = builder.Build();

            // Map Controllers
            app.MapControllers();

            app.UseAuthentication();
            app.UseAuthorization();

            // Use Swagger Extensions

            app.UseSwaggerCustExt();

            // Run pending db migrations on startup
            app.MigrateDbContextOne();
            // Start the application
            app.Run();
        }
        // Found this from https://github.com/dotnet/efcore/issues/29923#issuecomment-2092619682
        // as a solution to "Microsoft.Extensions.Hosting.HostAbortedException: The host was aborted."
        catch (Exception ex) when (ex is not HostAbortedException && ex.Source != "Microsoft.EntityFrameworkCore.Design")
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
    }
}
