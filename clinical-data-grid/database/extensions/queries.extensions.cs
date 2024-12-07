
using clinical_data_grid.database.models;
using Microsoft.EntityFrameworkCore;
namespace clinical_data_grid.database.extensions;

public static class QueryExtensions
{
  public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, object filter)
  {
    var filterProperties = filter.GetType().GetProperties();

    foreach (var property in filterProperties)
    {
      var value = property.GetValue(filter);
      if (value != null && !string.IsNullOrEmpty(value.ToString()))
      {
        // Build a dynamic Where clause
        query = query.Where(e => EF.Property<string>(e, property.Name).Contains(value.ToString().Trim()));
      }
    }

    return query;
  }

  public static async Task<int> UpdateHealthStatic<T>(this IQueryable<T> query, int id, UpdateHealthStaticData updatePayload)
  {
    var filterProperties = updatePayload.GetType().GetProperties();
    int rowsUpdated = 0;

    foreach (var property in filterProperties)
    {
      var value = property.GetValue(updatePayload);

      if (value != null && !string.IsNullOrEmpty(value.ToString()))
      {
        // Perform the update based on the filter's property and value
        rowsUpdated += await query
            .Where(e => EF.Property<int>(e, "Id") == id) // Filtering on ID
            .ExecuteUpdateAsync(e => e
                .SetProperty(b => EF.Property<string>(b, property.Name), b => value.ToString()));
      }
    }

    return rowsUpdated; // Return the total rows updated
  }
  public static async Task<int> DeleteHealthStatic<T>(this IQueryable<T> query, int id)
  {
    int deletionResult = 0;
    // Perform the update based on the filter's property and value
    deletionResult += await query
        .Where(e => EF.Property<int>(e, "Id") == id) // Filtering on ID
        .ExecuteDeleteAsync();

    return deletionResult; // Return the total rows updated
  }

}
