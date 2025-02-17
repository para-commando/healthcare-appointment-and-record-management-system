using clinical_data_grid.database.models;
using Microsoft.EntityFrameworkCore;
using clinical_data_grid.database.extensions;
namespace clinical_data_grid.database;
public class postgresHealthCareDbContext : DbContext
{
    private readonly ILogger<postgresHealthCareDbContext> _logger;

    public postgresHealthCareDbContext(DbContextOptions<postgresHealthCareDbContext> options, ILogger<postgresHealthCareDbContext> logger)
        : base(options) { _logger = logger; }

    // Define your DbSets (tables)
    public DbSet<Medicines> Medicines { get; set; }
    public DbSet<MedicineBenefits> MedicineBenefits { get; set; }

    public DbSet<MedicalSideEffects> MedicalSideEffects { get; set; }
    public DbSet<Diseases> Diseases { get; set; }
    public DbSet<Symptoms> Symptoms { get; set; }
    public DbSet<PrescriptionTemplate> PrescriptionTemplate { get; set; }
    public DbSet<ClinicalHealthStaticData> ClinicalHealthStaticData { get; set; }


    // Log successful database connection
    public bool TestConnection()
    {
        try
        {
            // Attempt to open a connection
            using (var connection = Database.GetDbConnection())
            {
                connection.Open();
                _logger.LogInformation("Database connection successful!");
                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database connection failed");
            return false;
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuring the entities using extension methods
        modelBuilder.medicineEntityExt();
        modelBuilder.medicineBenefitsEntityExt();
        modelBuilder.medicineSideEffectsEntityExt();
        modelBuilder.diseasesEntityExt();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Default configuration for fallback (useful for testing or local development)
            throw new InvalidOperationException("No options are configured");

        }
    }
}

