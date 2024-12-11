
using Microsoft.EntityFrameworkCore;
namespace clinical_data_grid.database.extensions;

public static class MigrationExtensions
{

public static async Task MigrateDbContextOne(this WebApplication app)
  {
    using var serviceScope = app.Services.CreateScope();
    var contextTwo = serviceScope.ServiceProvider.GetRequiredService<postgresHealthCareDbContext>();

    await contextTwo.Database.MigrateAsync();

  }
}
