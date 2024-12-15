namespace patient_management.database.contracts;

using System.ComponentModel.DataAnnotations;

public record CreatePatientDetailsValidation
{
  [Required(ErrorMessage = "Patient name is required.")]
  [MaxLength(255, ErrorMessage = "Patient name cannot exceed 255 characters.")]
  public string PatientName { get; init; } = string.Empty;

  [Required(ErrorMessage = "Patient address is required.")]
  [MaxLength(500, ErrorMessage = "Patient address cannot exceed 500 characters.")]
  public string PatientAddress { get; init; } = string.Empty;

  [Required(ErrorMessage = "Patient contact is required.")]
  [PhoneNumberListValidation(3)]
  public List<string>? PatientContact { get; set; }


  [Required(ErrorMessage = "Patient unique ID is required.")]
  [MaxLength(20, ErrorMessage = "Patient unique ID cannot exceed 20 characters.")]
  public string PatientUniqueId { get; init; } = string.Empty;

  [Required(ErrorMessage = "Patient registration date is required.")]
  public DateOnly PatientRegistrationDate { get; init; }
  public DateOnly PatientLatestDateOfVisit { get; init; }
}
