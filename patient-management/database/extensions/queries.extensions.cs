
using Microsoft.EntityFrameworkCore;
namespace patient_management.database.extensions;

public static class QueryExtensions
{
  public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, object filter)
  {
    var filterProperties = filter.GetType().GetProperties();
    var equalityCheckProperties = new List<string> {"PatientName",
  "PatientUniqueId",
  "PatientContact",
  "PatientRegistrationDate",
  "PatientLatestDateOfVisit"};
    var lessThanCheckProperties = new List<string> {
  "PatientRegDateLessThan",
  "PatientLatestDateOfVisitLessThan",
   };
    var greaterThanCheckProperties = new List<string> {
  "PatientRegDateGreaterThan",
  "PatientLatestDateOfVisitGreaterThan",
  };
    var rangeCheckProperties = new List<string> {
  "patientRegRange",
  "patientLatestVisitRange",
  };
    foreach (var property in filterProperties)
    {
      object incomingObject = new { StartDate = new DateOnly(2024, 12, 14), EndDate = new DateOnly(2024, 12, 15) };

      var value = property.GetValue(filter);
      if (value != null && !string.IsNullOrEmpty(value.ToString()))
      {
        if (equalityCheckProperties
    .FirstOrDefault(stringToCheck => stringToCheck.Contains(property.Name)) != null)
        { query = query.Where(e => EF.Property<string>(e, property.Name).Contains(value.ToString().Trim())); }

        else if (lessThanCheckProperties
     .FirstOrDefault(stringToCheck => stringToCheck.Contains(property.Name)) != null && value is DateOnly dateTimeValue)
        {
          if (property.Name == "PatientRegDateLessThan")
          {
            query = query.Where(e => EF.Property<DateOnly>(e, "PatientRegistrationDate") <= dateTimeValue);
          }
          else if (property.Name == "patientLatestDateOfVisitLessThan")
          {
            query = query.Where(e => EF.Property<DateOnly>(e, "PatientLatestDateOfVisit") <= dateTimeValue);
          }
        }
        else if (greaterThanCheckProperties
    .FirstOrDefault(stringToCheck => stringToCheck.Contains(property.Name)) != null && value is DateOnly dateTimeValue2)
        {
          if (property.Name == "patientRegDateGreaterThan")
          {
            query = query.Where(e => EF.Property<DateOnly>(e, "PatientRegistrationDate") <= dateTimeValue2);
          }
          else if (property.Name == "patientLatestDateOfVisitGreaterThan")
          {
            query = query.Where(e => EF.Property<DateOnly>(e, "PatientLatestDateOfVisit") <= dateTimeValue2);
          }
        }
        else if (rangeCheckProperties
    .FirstOrDefault(stringToCheck => stringToCheck.Contains(property.Name)) != null && value is { StartDate: DateOnly startDate, EndDate: DateOnly })
        {
          if (property.Name == "patientRegRange")
          {
            query = query.Where(e => EF.Property<DateOnly>(e, "PatientRegistrationDate") <= value.StartDate && EF.Property<DateOnly>(e, "PatientRegistrationDate") >= value.EndDate);
          }
          else if (property.Name == "patientLatestVisitRange")
          {
            query = query.Where(e => EF.Property<DateOnly>(e, "PatientLatestDateOfVisit") <= value.StartDate && EF.Property<DateOnly>(e, "PatientLatestDateOfVisit") >= value.EndDate);
          }
        }
      }



    }
    return query;
  }
 
}
