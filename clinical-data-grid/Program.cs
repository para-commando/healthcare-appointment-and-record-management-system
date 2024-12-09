using System.Text;
using clinical_data_grid.apis.extensions;
using clinical_data_grid.apis.services;
using clinical_data_grid.database;
using clinical_data_grid.database.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);
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
            var connectionString = builder.Configuration.GetConnectionString("postgresHealthCareDB")
                ?? throw new InvalidOperationException("Connection string 'postgresHealthCareDB' not found.");

            builder.Services.AddDbContext<postgresHealthCareDbContext>(options =>
                options.UseNpgsql(connectionString));

            // redis cache config
            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
            builder.Services.AddTransient<AuthService>();

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bAafd@A7d9#@F4*V!LHZs#ebKQrkE6pad2f3kj34c3dXy@")),
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateAudience = false
                };
            });

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("tech", p => p.RequireRole("developer"));
            });
            // Build the application
            var app = builder.Build();

            // Map Controllers
            app.MapControllers();

            app.UseAuthentication();
            app.UseAuthorization();

            // Use Swagger Extensions

            app.UseSwaggerCustExt();

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
