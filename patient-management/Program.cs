using patient_management.apis.extensions;
using patient_management.apis.services;
using patient_management.database;
using patient_management.database.extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
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
                options.ListenAnyIP(5004);
            });
            // jwt authentication custom extension
            builder.ConfigureJwtAuthenticationCustExt();

            // jwt authorization custom extension
            builder.ConfigureJwtAuthorizationCustExt();

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
            builder.Services.AddControllers(options =>
                {
                    var defaultPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(defaultPolicy));
                });

            // Add Database Context with Connection String
            var dbConnectionString = builder.Configuration.GetConnectionString("postgresHealthCareDB_cloud")
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
            $"{builder.Configuration["ConnectionStrings:redis_cloud:host"]}:{builder.Configuration["ConnectionStrings:redis_cloud:port"]}"
           },
           User = builder.Configuration["ConnectionStrings:redis_cloud:username"],
           Password = builder.Configuration["ConnectionStrings:redis_cloud:password"],
       };

       return ConnectionMultiplexer.Connect(redisConfig);
   });
            builder.Services.AddTransient<AuthService>();

            // Build the application
            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            // Map Controllers
            app.MapControllers();

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
