namespace patient_management.database.contracts;

using System.ComponentModel.DataAnnotations; // For validation attributes

public record UpdatePatientDetails
{

  [MaxLength(255, ErrorMessage = "Patient name cannot exceed 255 characters.")]
  public string? PatientName { get; init; } = string.Empty;


  [MaxLength(20, ErrorMessage = "Patient unique ID cannot exceed 20 characters.")]
  public string? PatientUniqueId { get; init; } = string.Empty;


  [MaxLength(15, ErrorMessage = "PatientContact cannot exceed 15 characters.")]
  public string? PatientContact { get; set; }

  [MaxLength(500)]
  public required string PatientAddress { get; set; } = string.Empty;
  
  public DateOnly PatientLatestDateOfVisit { get; set; }
}


