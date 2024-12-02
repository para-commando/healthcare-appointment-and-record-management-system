using database.models;
using Microsoft.EntityFrameworkCore;
namespace database.dbContext;

public class postgresHealthCareDbContext : DbContext
{
    public postgresHealthCareDbContext(DbContextOptions<postgresHealthCareDbContext> options)
        : base(options) { }

    // Define your DbSets (tables)
    public DbSet<Medicines> medicines { get; set; }
}
