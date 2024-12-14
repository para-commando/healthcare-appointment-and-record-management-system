using clinical_data_grid.database.models;
using Microsoft.EntityFrameworkCore;
using clinical_data_grid.database.extensions;
namespace clinical_data_grid.database;
public class postgresHealthCareDbContext : DbContext
{

    public postgresHealthCareDbContext(DbContextOptions<postgresHealthCareDbContext> options, ILogger<postgresHealthCareDbContext> logger)
        : base(options) { }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Default configuration for fallback (useful for testing or local development)
            throw new InvalidOperationException("No options are configured");

        }
    }
}

