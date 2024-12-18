
using System.Reflection;
using appointments_management.database.contracts;
using Microsoft.EntityFrameworkCore;
namespace appointment_details.database.extensions;

public static class QueryExtensions
{
  public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, object filter)
  {
    // used function delegates to declare type of the key of this dictionary, note that IQueryable<T> is the return type hence it is added again at the end of the delegate function signature
    var propertyOperations = new Dictionary<string, Func<IQueryable<T>, string, object, IQueryable<T>>>
    {
        // Equality checks
        { "PatientId", (q, prop, val) => q.Where(e => EF.Property<string>(e, prop).Equals(val)) },
        { "DoctorId", (q, prop, val) => q.Where(e => EF.Property<string>(e, prop).Equals(val)) },
        { "AppointmentDate", (q, prop, val) => q.Where(e => EF.Property<DateOnly>(e, prop).Equals(val)) },

        { "AppointmentBookedOn", (q, prop, val) => q.Where(e => EF.Property<string>(e, prop).Contains(val.ToString().Trim())) },
        { "AppointmentStatus", (q, prop, val) => q.Where(e => EF.Property<string>(e, prop).Contains(val.ToString().Trim())) },


        // Less-than checks
        { "AppointmentDateLessThan", (q, prop, val) => q.Where(e => EF.Property<DateOnly>(e, "AppointmentDate") <= (DateOnly)val) },
        { "AppointmentBookedOnLessThan", (q, prop, val) => q.Where(e => EF.Property<DateTime>(e, "AppointmentBookedOn") <= (DateTime)val) },

        // Greater-than checks
        { "AppointmentDateGreaterThan", (q, prop, val) => q.Where(e => EF.Property<DateOnly>(e, "AppointmentDate") >= (DateOnly)val) },
        { "AppointmentBookedOnGreaterThan", (q, prop, val) => q.Where(e => EF.Property<DateTime>(e, "AppointmentBookedOn") >= (DateTime)val) },

        // Range checks
        { "AppointmentBookedOnRange", (q, prop, val) =>
            {
                var range = (AppointmentBookedOnRegRange)val;
                return q.Where(e => EF.Property<DateTime>(e, "AppointmentBookedOn") >= range.StartDate &&
                                    EF.Property<DateTime>(e, "AppointmentBookedOn") <= range.EndDate);
            }
        },
        { "AppointmentDateRange", (q, prop, val) =>
            {
                var range = (AppointmentDateRange)val;
                return q.Where(e => EF.Property<DateOnly>(e, "AppointmentDate") >= range.StartDate &&
                                    EF.Property<DateOnly>(e, "AppointmentDate") <= range.EndDate);
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
