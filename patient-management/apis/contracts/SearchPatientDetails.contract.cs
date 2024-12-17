namespace patient_management.database.contracts;

using System.ComponentModel.DataAnnotations; // For validation attributes

public record SearchPatientDetails
{

  public int? Id { get; init; }

  [MaxLength(255, ErrorMessage = "Patient name cannot exceed 255 characters.")]
  public string? PatientName { get; init; } = string.Empty;


  [MaxLength(20, ErrorMessage = "Patient unique ID cannot exceed 20 characters.")]
  public string? PatientUniqueId { get; init; } = string.Empty;


  [MaxLength(15, ErrorMessage = "PatientContact cannot exceed 15 characters.")]
  public string? PatientContact { get; set; }

  public DateOnly? PatientRegDateLessThan { get; init; }

  public DateOnly? PatientRegDateGreaterThan { get; init; }

  public DateOnly? PatientLatestDateOfVisitGreaterThan { get; init; }

  public DateOnly? PatientLatestDateOfVisitLessThan { get; init; }

  public DateOnly? PatientRegDateEqualTo { get; init; }

  public DateOnly? PatientLatestDateOfVisitEqualTo { get; init; }

  public PatientRegRange? PatientRegRange { get; init; }

  public PatientLatestVisitDateRange? PatientLatestVisitRange { get; init; }

  public int? Limit { get; init; }

  public string? SortBy { get; init; }
}


public record PatientRegRange
{
  public DateOnly? StartDate { get; set; }
  public DateOnly? EndDate { get; set; }
}

public record PatientLatestVisitDateRange
{

  public DateOnly? StartDate { get; set; }
  public DateOnly? EndDate { get; set; }
}