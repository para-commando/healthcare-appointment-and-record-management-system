namespace patient_management.database.contracts;

using System.ComponentModel.DataAnnotations; // For validation attributes

public record SearchPatientDetails
{

  [MaxLength(255, ErrorMessage = "Patient name cannot exceed 255 characters.")]
  public string PatientName { get; init; } = string.Empty;


  [MaxLength(20, ErrorMessage = "Patient unique ID cannot exceed 20 characters.")]
  public string PatientUniqueId { get; init; } = string.Empty;


  [MaxLength(15, ErrorMessage = "PatientContact cannot exceed 15 characters.")]
  public string PatientContact { get; set; }

  public DateTime PatientRegDateLessThan { get; init; }

  public DateTime PatientRegDateGreaterThan { get; init; }

  public DateTime PatientLatestDateOfVisitGreaterThan { get; init; }

  public DateTime PatientLatestDateOfVisitLessThan { get; init; }

  public DateTime PatientRegDateEqualTo { get; init; }

  public DateTime PatientLatestDateOfVisitEqualTo { get; init; }

  public PatientRegRange PatientRegRange { get; init; }

  public PatientLatestVisitDateRange PatientLatestVisitRange { get; init; }
}


public record PatientRegRange
{
  public DateTime? startDate { get; set; };
  public DateTime? EndDate { get; set; };
}

public record PatientLatestVisitDateRange
{

  public DateTime? startDate { get; set; };
  public DateTime? EndDate { get; set; };
}