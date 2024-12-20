namespace authentication_management.database.contracts;

using System.ComponentModel.DataAnnotations;

public record UpdateDoctorDetailsValidation
{
  [MaxLength(255, ErrorMessage = "DoctorName cannot exceed 255 characters.")]
  public string? DoctorName { get; init; }

  [MaxLength(255, ErrorMessage = "DoctorSpecialization cannot exceed 255 characters.")]
  public string? DoctorSpecialization { get; init; }

  [MaxLength(500, ErrorMessage = "DoctorAddress cannot exceed 500 characters.")]
  public string? DoctorAddress { get; init; }

  [MaxLength(50, ErrorMessage = "DoctorContact cannot exceed 50 characters.")]
  public string? DoctorContact { get; init; }

  [MaxLength(20, ErrorMessage = "DoctorUniqueId cannot exceed 20 characters.")]
  public string? DoctorUniqueId { get; init; }

  public DateOnly? DoctorDateOfJoining { get; init; }

}
