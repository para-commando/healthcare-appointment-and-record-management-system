using appointment_details.database.models;
using Microsoft.EntityFrameworkCore;
using appointment_details.database.extensions;
namespace appointment_details.database;
public class postgresHealthCareDbContext : DbContext
{

    public postgresHealthCareDbContext(DbContextOptions<postgresHealthCareDbContext> options, ILogger<postgresHealthCareDbContext> logger)
        : base(options) { }

    public DbSet<AppointmentDetails> AppointmentDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Default configuration for fallback (useful for testing or local development)
            throw new InvalidOperationException("No options are configured");

        }
    }
}

