
using Microsoft.EntityFrameworkCore;
namespace clinical_data_grid.database.extensions;

public static class MigrationExtensions
{

  public static async Task MigrateDbContextOne(this WebApplication app)
  {
    using var serviceScope = app.Services.CreateScope();
    // since postgresHealthCareDbContext is a scoped dependency as it inherits DbContext its disposal needs to be handled properly hence "using" keyword is used while creating a scope for its usage which will dispose the resources tied to postgresHealthCareDbContext post the work is done
    var contextTwo = serviceScope.ServiceProvider.GetRequiredService<postgresHealthCareDbContext>();

    await contextTwo.Database.MigrateAsync();

  }
}
