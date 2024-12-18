
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using staff_management.database.contracts;
namespace staff_management.database.extensions;

public static class QueryExtensions
{
  public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, object filter)
  {
    // used function delegates to declare type of the key of this dictionary, note that IQueryable<T> is the return type hence it is added again at the end of the delegate function signature
    var propertyOperations = new Dictionary<string, Func<IQueryable<T>, string, object, IQueryable<T>>>
    {
        // Equality checks
        { "Id", (q, prop, val) => q.Where(e => EF.Property<string>(e, prop).Equals(val)) },
        { "PatientName", (q, prop, val) => q.Where(e => EF.Property<string>(e, prop).Contains(val.ToString().Trim())) },
        { "PatientUniqueId", (q, prop, val) => q.Where(e => EF.Property<string>(e, prop).Contains(val.ToString().Trim())) },
        { "PatientContact", (q, prop, val) => q.Where(e => EF.Property<string>(e, prop).Contains(val.ToString().Trim())) },

        // Less-than checks
        { "PatientRegDateLessThan", (q, prop, val) => q.Where(e => EF.Property<DateOnly>(e, "PatientRegistrationDate") <= (DateOnly)val) },
        { "PatientLatestDateOfVisitLessThan", (q, prop, val) => q.Where(e => EF.Property<DateOnly>(e, "PatientLatestDateOfVisit") <= (DateOnly)val) },

        // Greater-than checks
        { "PatientRegDateGreaterThan", (q, prop, val) => q.Where(e => EF.Property<DateOnly>(e, "PatientRegistrationDate") >= (DateOnly)val) },
        { "PatientLatestDateOfVisitGreaterThan", (q, prop, val) => q.Where(e => EF.Property<DateOnly>(e, "PatientLatestDateOfVisit") >= (DateOnly)val) },

        // Range checks
        { "PatientRegRange", (q, prop, val) =>
            {
                var range = (PatientRegRange)val;
                return q.Where(e => EF.Property<DateOnly>(e, "PatientRegistrationDate") >= range.StartDate &&
                                    EF.Property<DateOnly>(e, "PatientRegistrationDate") <= range.EndDate);
            }
        },
        { "PatientLatestVisitRange", (q, prop, val) =>
            {
                var range = (PatientLatestVisitDateRange)val;
                return q.Where(e => EF.Property<DateOnly>(e, "PatientLatestDateOfVisit") >= range.StartDate &&
                                    EF.Property<DateOnly>(e, "PatientLatestDateOfVisit") <= range.EndDate);
            }
        }
    };

    // Iterate over filter properties using reflections
    var allFilterProperties = filter.GetType().GetProperties();
    foreach (var property in allFilterProperties)
    {
      var value = property.GetValue(filter);
      if (value != null && !string.IsNullOrEmpty(value.ToString()))
      {
        // TryGetValue returns true if the Dictionary<TKey, TValue> contains an element with the specified key; otherwise, false.
        if (propertyOperations.TryGetValue(property.Name, out var operation))
        {
          query = operation(query, property.Name, value);
        }
      }
    }
    // Apply limit if provided
    if (filter.GetType().GetProperty("Limit") is PropertyInfo limitProperty)
    {
      var limitValue = limitProperty.GetValue(filter);
      if (limitValue is int limit && limit > 0)
      {
        query = query.Take(limit);
      }
    }

    if (filter.GetType().GetProperty("SortBy") is PropertyInfo sortByProperty)
    {
      var sortByValue = sortByProperty.GetValue(filter)?.ToString();
      if (!string.IsNullOrEmpty(sortByValue))
      {
        var sortDirection = "asc"; // Default to ascending

        if (sortByValue == "desc")
        {
          sortDirection = "desc";
          query = query.OrderByDescending(e => e);

        }
        else
        {
          query = query.OrderBy(e => e);
        }


      }
    }
    return query;
  }

}
