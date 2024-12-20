namespace staff_management.database.contracts;

using System.ComponentModel.DataAnnotations;

public record CreateDoctorDetailsValidation
{
  
  [Required(ErrorMessage = "DoctorName is required.")]
  [MaxLength(255, ErrorMessage = "DoctorName cannot exceed 255 characters.")]
  public required string DoctorName { get; init; }

  [Required(ErrorMessage = "DoctorSpecialization is required.")]
  [MaxLength(255, ErrorMessage = "DoctorSpecialization cannot exceed 255 characters.")]
  public required string DoctorSpecialization { get; init; }

  [Required(ErrorMessage = "DoctorAddress is required.")]
  [MaxLength(500, ErrorMessage = "DoctorAddress cannot exceed 500 characters.")]
  public required string DoctorAddress { get; init; }

  [Required(ErrorMessage = "DoctorContact is required.")]
  [MaxLength(50, ErrorMessage = "DoctorContact cannot exceed 50 characters.")]
  public required string DoctorContact { get; init; }

  [Required(ErrorMessage = "DoctorUniqueId is required.")]
  [MaxLength(20, ErrorMessage = "DoctorUniqueId cannot exceed 20 characters.")]
  public required string DoctorUniqueId { get; init; }


  [Required(ErrorMessage = "DoctorDateOfJoining is required.")]
  public required DateOnly DoctorDateOfJoining { get; init; }


}
