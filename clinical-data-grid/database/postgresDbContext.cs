using database.models;
using Microsoft.EntityFrameworkCore;
using database.extensions;
namespace database.dbContext;

public class postgresHealthCareDbContext : DbContext
{
    public postgresHealthCareDbContext(DbContextOptions<postgresHealthCareDbContext> options)
        : base(options) { }

    // Define your DbSets (tables)
    public DbSet<Medicines> Medicines { get; set; }
    public DbSet<MedicineBenefits> MedicineBenefits { get; set; }

    public DbSet<MedicalSideEffects> MedicalSideEffects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuring the entities using extension methods
        modelBuilder.medicineEntityExt();
         modelBuilder.medicineBenefitsEntityExt();
          modelBuilder.medicineSideEffectsEntityExt();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Default configuration for fallback (useful for testing or local development)
            optionsBuilder.UseNpgsql("YourFallbackConnectionStringHere");
        }
    }
}
