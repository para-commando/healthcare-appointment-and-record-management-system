using patient_management.database.models;
using Microsoft.EntityFrameworkCore;
using patient_management.database.extensions;
namespace patient_management.database;
public class postgresHealthCareDbContext : DbContext
{

    public postgresHealthCareDbContext(DbContextOptions<postgresHealthCareDbContext> options, ILogger<postgresHealthCareDbContext> logger)
        : base(options) { }

    public DbSet<PatientDetails> PatientDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Default configuration for fallback (useful for testing or local development)
            throw new InvalidOperationException("No options are configured");

        }
    }
}

